# -*- coding: utf-8 -*-

import xlrd
import traceback

def intToBase26(value):
	asciiA = ord('A')

	value += 1

	ret = ""
	while value != 0:
		mod = value % 26
		value = value // 26
		if mod == 0:
			mod = 26
			value -= 1

		ret = chr(asciiA + mod - 1) + ret

	return ret

def base26ToInt(value):
	asciiA = ord('A')

	ret = 0
	for s in value:
		ret = ret * 26 + ord(s) - asciiA + 1

	return ret - 1

class Parser(object):

	def __init__(self, filename, key, config, multi_key=False, force_run=False, sheet_index=0):
		super(Parser, self).__init__()

		self.filename = filename
		self.key_name = key
		self.config = config
		self.is_multi_key = multi_key
		self.is_force_run = force_run

		self.header = None
		self.converters = None
		self.sheet = None

		self.sheet_index = sheet_index
		self.header_row_index = 0
		self.data_row_index = self.header_row_index + 1
		self.header_data = None

	def parse_header(self, header):
		pass

	def convert_cell(self, row, col, value, current_row_data):
		converter = self.converters[col]
		if converter is None: return True

		key = converter[0]
		fun = converter[1]
		is_default = converter[2] if len(converter) > 2 else False
		ret = None

		if value == "":
			if not is_default:
				raise ValueError, "the field must be set."

		else:
			ret = fun(value)

		if ret is None and is_default:
			if len(converter) > 3:
				ret = converter[3]

			else:
				return True

		current_row_data[key] = ret
		return True

	def do_parse(self):
		book = xlrd.open_workbook(self.filename)

		if self.sheet_index >= book.nsheets:
			return "The file has no sheet '%d'" % self.sheet_index

		table = book.sheet_by_index(self.sheet_index)
		if self.data_row_index >= table.nrows:
			return "The sheet is empty."

		# the row 0 is table header
		self.header_data = table.row_values(self.header_row_index)
		self.parse_header(self.header_data)
		cols = min(table.ncols, len(self.header_data))

		self.sheet = {}

		# the remain rows is raw data.
		for r in xrange(self.data_row_index, table.nrows):
			if table.cell_value(r, 0) == '': break

			current_row_data = {}
			for c in xrange(cols):
				cell = table.cell(r, c)
				value = cell.value
				if type(value) == unicode:
					value = value.strip()

				ret = False
				try:
					ret = self.convert_cell(r, c, value, current_row_data)
				except Exception, e:
					traceback.print_exc()

				if not ret:
					msg = "The cell(%d, %s) = [%s] is invalid." % (r + 1, intToBase26(c), unicode(value))
					if self.is_force_run:
						print msg
						continue
					else:
						return msg

			self.add_row(current_row_data)

		return None

	def add_row(self, current_row_data):
		key_value = current_row_data.pop(self.key_name)
		
		if self.is_multi_key:
			row = self.sheet.setdefault(key_value, [])
			row.append(current_row_data)

		else:
			if key_value in self.sheet:
				print "warn: duplicated key '%s' was found" % key_value

			self.sheet[key_value] = current_row_data

	def get_comments(self):
		ret = []

		ret.append("key = %s" % self.key_name)

		for col, field in enumerate(self.header_data):
			converter = self.converters.get(col)
			if converter is None: continue

			comment = "%s\t%-20s%s" %(intToBase26(col), converter[0],  field.encode("utf-8"))
			ret.append(comment)

		return ret


class ParserByFieldName(Parser):

	def parse_header(self, header):
		self.converters = {}
		name_set = set()
		for col, col_name in enumerate(header):
			if isinstance(col_name, unicode):
				col_name = col_name.encode("utf-8")
			elif isinstance(col_name, float): #doens't support the float value as key.
				col_name = int(col_name)

			if col_name == '':
				self.converters[col] = None
				continue

			index = 2
			unique_name = col_name
			while unique_name in name_set:
				unique_name = col_name + str(index)
				index = index + 1
			col_name = unique_name
			name_set.add(col_name)

			converter = self.config.get(col_name)
			self.converters[col] = converter

			if converter is None:
				print "warn: the field '%s' at col(%s) was ignored, file: %s" % (col_name, intToBase26(col), self.filename, )

		return


class ParserByIndex(Parser):

	def parse_header(self, header):
		self.converters = {}

		for col, col_name in enumerate(header):
			key = intToBase26(col)
			converter = self.config.get(key)
			self.converters[col] = converter

			col_name = col_name.encode("utf-8")
			if col_name != "" and converter is None:
				print "warn: the field '%s' at col(%s) was ignored, file: %s" % (col_name, intToBase26(col), self.filename, )

		return

def createParser(input_file, module, force_run=False, sheet_index = 0):
	dict = module.__dict__
	multi_key = dict.get("MULTI_KEY", False)

	cls = None
	config = None

	if "CONFIG_INDEX" in dict:
		cls = ParserByIndex
		config = module.CONFIG_INDEX
	else:
		cls = ParserByFieldName
		config = module.CONFIG

	return cls(input_file, module.KEY_NAME, config, multi_key, force_run, sheet_index)

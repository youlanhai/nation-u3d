# -*- coding: utf-8 -*-

import writer

class LuaWriter(writer.Writer):

	def begin_write(self):
		output = self.output

		output("local _G = _G\n")
		output("local module_name = ...\n")
		output("module(module_name)\n\n")

	def end_write(self):
		pass
		# output = self.output
		# output("_G.print('model loaded:', module_name)\n")


	def write_sheet(self, name, sheet):
		output = self.output

		output(name)
		output(" = {\n")

		keys = sheet.keys()
		keys.sort()

		key_format = "\t[%d] = "
		if len(keys) > 0 and isinstance(keys[0], basestring):
			key_format = "\t[\"%s\"] = "

		for k in keys:
			output(key_format % k)
			self.write(sheet[k])
			output(",\n")

			self.flush()

		output("}\n\n")
		self.flush()

	def write_value(self, name, value):
		output = self.output

		output(name)
		output(" = ")
		self.write(value)
		output("\n\n")

		self.flush()

	def write_comment(self, comment):
		self.output("-- ")
		self.output(comment)
		self.output("\n")

	def write(self, value):
		output = self.output

		if value is None:
			return output("nil")

		tp = type(value)
		if tp == bool:
			output("true" if value else "false")

		elif tp == int:
			output("%d" % (value, ))

		elif tp == float:
			output("%g" % (value, ))

		elif tp == str:
			output('"%s"' %(value, ))

		elif tp == unicode:
			output('"%s"' % (value.encode("utf-8"), ))

		elif tp == tuple or tp == list or tp == set or tp == frozenset:
			output("{")
			for v in value:
				self.write(v)
				output(", ")
			output("}")

		elif tp == dict:
			output("{")

			keys = value.keys()
			keys.sort()
			for k in keys:
				output("[")
				self.write(k)
				output("] = ")
				self.write(value[k])
				output(", ")

			output("}")

		else:
			raise TypeError, "unsupported type %s" % (str(tp), )

		return

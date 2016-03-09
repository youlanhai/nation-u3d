# -*- coding: utf-8 -*-

import writer
from struct import pack

TP_ZERO 	= 0
TP_ONE		= 1
TP_TRUE		= 2
TP_FALSE	= 3
TP_NULL		= 4

TP_INT8 	= 10
TP_INT16	= 11
TP_INT32	= 12
TP_INT64	= 13
TP_FLOAT	= 14
TP_DOUBLE	= 15

TP_STRING8 	= 20
TP_STRING16	= 21
TP_STRING_EMPTY = 22
TP_STRING_INDEX8 = 23
TP_STRING_INDEX16 = 24

TP_LIST_EMPTY = 26
TP_LIST8	= 27
TP_LIST16	= 28

TP_DICT_EMPTY  = 30
TP_DICT8	= 31
TP_DICT16	= 32


def bool2str(v):
	return pack("B", TP_TRUE if v else TP_FALSE)

def int2str(v):
	if v == 0: return pack("B", TP_ZERO)
	if v == 1: return pack("B", TP_ONE)
	if -128 <= v <= 127: return pack("Bb", TP_INT8, v)
	if -32768 <= v <= 32767: return pack("Bh", TP_INT16, v)
	if -2147483648 <= v <= 2147483647: return pack("Bi", TP_INT32, v)
	return pack("Bq", TP_INT64, v)

def float2str(v):
	iv = int(v)
	if abs(v - iv) < 0.0000001:
		return int2str(iv)
	else:
		return pack("Bf", TP_FLOAT, v)

def str2str_real(v):
	size = len(v)
	if size == 0: return pack("B", TP_STRING_EMPTY)
	if size <= 256: return pack("BB", TP_STRING8, size) + v
	if size <= 65536: return pack("BH", TP_STRING16, size) + v

	raise RuntimeError, "The string size is too long."

def list_header2str(v):
	size = len(v)
	if size == 0: return pack("B", TP_LIST_EMPTY)
	if size <= 256: return pack("BB", TP_LIST8, size)
	if size <= 65536: return pack("BH", TP_LIST16, size)

	raise RuntimeError, "The list length is too long."

def dict_header2str(v):
	size = len(v)
	if size == 0: return pack("B", TP_DICT_EMPTY)
	if size <= 256: return pack("BB", TP_DICT8, size)
	if size <= 65536: return pack("BH", TP_DICT16, size)

	raise RuntimeError, "The dict length is too long."


class BinaryWriter(writer.Writer):

	def __init__(self, handle):
		super(BinaryWriter, self).__init__(handle)

		self.string_cache = {}

	def add_string(self, s):
		index = self.string_cache.get(s)
		if index is None:
			index = len(self.string_cache)
			self.string_cache[s] = index
		return index

	def begin_write(self): pass
	def end_write(self):
		size = len(self.string_cache)

		strings = [None for i in xrange(size)]
		for k, v in self.string_cache.iteritems():
			strings[v] = k

		self.write(strings)

	def str2str_index(self, v):
		index = self.add_string(v)
		if index <= 256: return pack("BB", TP_STRING_INDEX8, index)
		if index <= 65536: return pack("BH", TP_STRING_INDEX16, index)

		raise RuntimeError, "The number of string is too much."

	def write_sheet(self, name, sheet):
		self.write_value(name, sheet)

	def write_value(self, name, value):
		self.write(name)
		self.write(value)

	def write(self, value):
		output = self.output

		if value is None:
			return output(pack("B", TP_NULL))

		tp = type(value)
		if tp == bool:
			output(bool2str(value))

		elif tp == int:
			output(int2str(value))

		elif tp == float:
			output(float2str(value))

		elif tp == str:
			output(self.str2str_index(value))

		elif tp == unicode:
			v = value.encode("utf-8")
			output(self.str2str_index(v))

		elif tp == tuple or tp == list or tp == set or tp == frozenset:
			output(list_header2str(value))
			for v in value: self.write(v)

		elif tp == dict:
			output(dict_header2str(value))

			keys = value.keys()
			keys.sort()
			for k in keys:
				self.write(k)
				self.write(value[k])

		else:
			raise TypeError, "unsupported type %s" % (str(tp), )

		return

# -*- coding: utf-8 -*-

class Writer(object):

	def __init__(self, handle):
		super(Writer, self).__init__()
		self.handle = handle
		self.cache = []

	def begin_write(self): pass
	def end_write(self): pass

	def write_sheet(self, name, sheet): pass
	def write_value(self, name, value): pass
	def write_comment(self, comment): pass

	def output(self, *args):
		for v in args: assert(type(v) == str)
		self.cache.extend(args)

	def write_module(self, module):
		output = self.output

		for k in sorted(module.iterkeys()):
			v = module[k]
			if isinstance(v, dict):
				self.write_sheet(k, v)
			else:
				self.write_value(k, v)

			output("\n")

		self.flush()

	def flush(self):
		if len(self.cache) == 0: return

		text = "".join(self.cache)
		self.cache = []

		self.handle.write(text)

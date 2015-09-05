# -*- coding: utf-8 -*-

import os
import sys

USAGE = """
usage:
python xxx.py path extension [remove_old_file]
"""

def usage(): print USAGE

def convert(path, src_ext, rm_old=False):
	print "root path:", path

	src_ext_lenght = len(src_ext)

	for dirpath, dirnames, filenames in os.walk(path):

		for fname in filenames:
			if len(fname) > src_ext_lenght and fname[-src_ext_lenght:].lower() == src_ext:
				name = fname[:-src_ext_lenght]

				srcFullPath = os.path.join(dirpath, fname)
				dstFullPath = os.path.join(dirpath, name + ".png")

				cmd = """TexturePacker.exe --opt RGBA8888  --size-constraints AnySize --padding 0 --sheet "%s" "%s" """ % (dstFullPath, srcFullPath, )

				print "convert: ", srcFullPath
				if 0 == os.system(cmd):
					if rm_old: os.remove(os.path.join(dirpath, fname))

def main():
	if len(sys.argv) < 3:
		return usage()

	rm_old = sys.argv[3] == "true" if len(sys.argv) > 3 else False

	convert(sys.argv[1], sys.argv[2], rm_old)

if __name__ == "__main__":
	main()

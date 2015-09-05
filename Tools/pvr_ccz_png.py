# -*- coding: utf-8 -*-

import os
import sys


def main(path):
	print "path:", path

	for dirpath, dirnames, filenames in os.walk(path):
		#print "path:", dirpath

		for fname in filenames:
			if len(fname) > 8 and fname[-8:] == ".pvr.ccz":
				name = fname[:-8]

				fullPath = os.path.join(dirpath, name)

				cmd = """TexturePacker.exe --opt RGBA8888 --sheet "%s.png" "%s.pvr.ccz" """ % (fullPath, fullPath)

				print "convert: ", fullPath
				os.system(cmd)

if __name__ == "__main__":
	if len(sys.argv) >= 2:
		main(sys.argv[1])

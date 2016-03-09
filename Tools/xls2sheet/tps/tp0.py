
import math

def use_origin(arg): return arg
def use_empty(arg): return None

def to_bool(arg):
	if isinstance(arg, unicode) or isinstance(arg, str):
		if arg == "true": return True
		elif arg == "false": return False
		return bool(to_int(arg))
	else:
		return bool(arg)

def to_int(arg):
	return int(to_float(arg))

def to_float(arg):
	if arg == "": return 0
	return float(arg)

def to_dict(arg):
	if type(arg) != unicode:
		raise ValueError, "string type needed."

	return eval("{%s}" % arg)


def to_list(arg, converter=None):
	ret = eval("[%s]" % arg)
	if not isinstance(ret, list):
		raise ValueError, "list type needed, '%s' was given" % arg

	if converter is not None:
		for i, v in enumerate(ret):
			ret[i] = converter(v)

	return ret

def to_int_list(arg): return to_list(arg, to_int)
def to_float_list(arg): return to_list(arg, to_float)

def to_yaw(arg):
	arg = to_float(arg)
	return arg * math.pi / 180

def to_point(arg):
	lst = to_float_list(arg)
	if len(lst) != 2:
		raise ValueError, "point type need 2 float elemnt, '%s' was given" % (arg)
	return tuple(lst)

def to_point_list(args):
	ret = []
	groups = args.split(';')
	for group in groups:
		group = group.strip()
		if len(group) == 0: continue

		ret.append(to_point(group))

	return ret

def to_string_list(args):
	ret = []

	images = args.split(',')
	for image in images:
		image = image.strip()
		if len(image) > 0:
			ret.append(image)

	return ret if len(ret) > 0 else None

def to_float_group(args):
	ret = []

	groups = args.split(';')
	for group in groups:
		group = group.strip()
		if len(group) == 0: continue

		ret.append(to_float_list(group))

	return ret

def to_images(args):
	ret = to_string_list(args)
	if ret != None:
		return [x + ".png" for x in ret]
		
	return ret

def to_float_list_2(args):
	if type(args) != unicode: return None
	ret = []

	groups = args.split(';')
	for group in groups:
		group = group.strip()
		if len(group) == 0: continue

		ret.append(to_float(group))

	return ret

def to_string_list_2(args):
	ret = []

	images = args.split(';')
	for image in images:
		image = image.strip()
		if len(image) > 0:
			ret.append(image)

	return ret if len(ret) > 0 else None

def to_text(args):
	return args.replace('\n','\\n')

def to_amstr(args):
	return args + ".am"
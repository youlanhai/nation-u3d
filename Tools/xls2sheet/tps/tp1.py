#-*- coding: utf-8 -*-

import tp0

#1秒 = 60帧
FRAME_PER_SECOND = 60

# 速度原来的单位是 像素/帧，现在转换成 像素/秒。
# 1秒 = 60帧
def frame_to_speed(args):
	return float(args) * FRAME_PER_SECOND

# 将帧做单位的时间，转成成实际时间
def frame_to_time(args):
	return float(args) / FRAME_PER_SECOND

# 将窗口坐标系，转换为cocos2d坐标系
def to_cc_point(arg):
	lst = tp0.to_float_list(arg)
	if len(lst) != 2:
		raise ValueError, "point type need 2 float elemnt, '%s' was given" % (arg)
	return (lst[0], -lst[1])

def to_cc_point_list(args):
	ret = []
	groups = args.split(';')
	for group in groups:
		group = group.strip()
		if len(group) == 0: continue

		ret.append(to_cc_point(group))

	return ret

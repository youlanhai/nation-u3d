# -*- coding: utf-8 -*-

KEY_NAME = "ID"

#表里是否存在多个相同的key值
MULTI_KEY = False

"""
CONFIG是一个dict，描述了如何将excel表格导出成脚本表文件。
key: 对应excel表的表头（excel表中的第一行）
value: 是一个最多可以有4个元素的tuple
	tuple 0：脚本中的列名
	tuple 1：转换函数。将excel表中的单元格转换成脚本对象
	tuple 2：表示此列是否可缺省，即是否可不填
	tuple 3：缺省值。脚本中使用此值替代excel中没有填的位置，如果没有指定缺省值，脚本中就不会出现该单元格的数据。
"""
CONFIG = {
	"编号": ("ID", int),
	"名称": ("name", unicode),
	"描述": ("describe", unicode, True),
	"品质": ("quality", int, True, 0),
}

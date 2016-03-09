# -*- coding: utf-8 -*-

"""
(excel文件名正则表达式，转换器文件名称 |，新名称的正则表达式，表索引)
新名称	可以缺省，默认是原名称去除后缀
表索引	可以缺省，默认是0
eg.
缺省：(r"level/\d+/enemy.xls", "enemy"), -> level/\d+/enemy.json
改名：(r"(level/\d+/)enemy.xls", "enemy", r"\1d_enemy", 0), -> level/\d+/d_enemy.lua
"""

CONVENTION_TABLE = (
	("test_sheet.xls", "test_sheet"),
)

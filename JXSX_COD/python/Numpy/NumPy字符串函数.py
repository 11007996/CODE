'''
函数	描述
add()	对两个数组的逐个字符串元素进行连接
multiply()	返回按元素多重连接后的字符串
center()	居中字符串
capitalize()	将字符串第一个字母转换为大写
title()	将字符串的每个单词的第一个字母转换为大写
lower()	数组元素转换为小写
upper()	数组元素转换为大写
split()	指定分隔符对字符串进行分割，并返回数组列表
splitlines()	返回元素中的行列表，以换行符分割
strip()	移除元素开头或者结尾处的特定字符
join()	通过指定分隔符来连接数组中的元素
replace()	使用新字符串替换字符串中的所有子字符串
decode()	数组元素依次调用str.decode
encode()	数组元素依次调用str.encode
'''



import numpy as np 
 
'''
print ('连接两个字符串：')
print (np.char.add(['hello'],[' xyz']))
print ('\n')
print ('连接示例：')
print (np.char.add(['hello', 'hi'],[' abc', ' xyz']))
'''


# print (np.char.multiply('Runoob ',3))   # 执行多重连接,重复3遍Runoob



# np.char.center(str , width,fillchar) ：
# str: 字符串，width: 长度，fillchar: 填充字符
# print (np.char.center('Runoob', 20,fillchar = '*'))    # 用于将字符串居中，并使用指定字符在左侧和右侧进行填充。


# print (np.char.capitalize('runoob'))   # 首字母大写


# print (np.char.title('i like runoob'))   # 首字母大写



# print (np.char.lower(['RUNOOB','GOOGLE']))    # 转小写



# print (np.char.upper(['runoob','google']))    # 大写


# print (np.char.split ('www.runoob.com', sep = '.'))    # 分割字符串


# print (np.char.splitlines('i\nlike runoob?'))   # 以换行符分割字符串


# 移除字符串头尾的 a 字符
# print (np.char.strip('ashok arunooba','a'))
# 移除数组元素头尾的 a 字符
# print (np.char.strip(['arunooba','admin','java'],'a'))


# 指定多个分隔符操作数组元素
# print (np.char.join(':','runoob'))
# print (np.char.join([':','-'],['runoob','google']))


# print (np.char.replace ('i like runoob', 'oo', 'cc'))    # 替换字符


# a = np.char.encode('runoob', 'cp500')    # 改变字符编码
# print (a)    # 改变字符编码
# print (np.char.decode(a,'cp500'))    #  对字符进行解码



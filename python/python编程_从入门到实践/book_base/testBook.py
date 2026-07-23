'''
# 字符串可以用单引号也可以用双引号
message = 'hello"" world'
print(message)
'''

'''
# 字符串大小写
message = 'hello woRld'
print(message.title())   #首字母大写,其余小写
print(message.upper())   #字母大写
print(message.lower())   #字母小写
'''

'''
# 字符串拼接
a = "aaa"
b="bbb"
print(a+" "+b)
'''

'''
# 制表符和换行符
print("hello" +'\t'+ 'world')
print("hello" +'\n'+ 'world')
print("hello\nworld")
'''

'''
a = " aaaa "
print(a)
print(a.rstrip())   #删除末尾的空格
b = a + "bbb"
print(b)
b = a.rstrip() + "bbb"
print(b)
print(a.lstrip())   #删除开头的空格
print(a.strip())    #删除前后的空格
'''

'''
# 数字类型
a = 3
b = 2
print(a*b)   #乘法
print(a**b)  #平方
print(a==b)  #相等
a = 0.1
b = 0.2
print(a+b)   #浮点数计算存在偏差
print("这是数字"+str(a))  #数字类型转为字符串
'''


# 列表
'''
b = ["a",'b','c','d','e']
print(b)
print(b[0])
print(b[-1])  #最后一个元素
b.append('f') #添加元素
print(b)
b.insert(1,'a') #插入一个元素
print(b)
del b[0]   #删除一个元素
print(b)
idx = b.pop(0)  #从列表中取出一个元素
print(idx)
idx = b.pop()  #从列表中取出最后一个元素
print(idx)
print(b)
b.remove('c')  #删除特定的值
print(b)
'''
'''
a = [3,6,23,6,34,69,5,32]
a.sort()  #列表排序
print(a)
# a.sort(reverse=True)  #反向排序
# print(a)
print(sorted(a))   #临时排序
# print(a)
print(sorted(a,reverse=True))   #临时排序
# print(a)
'''

'''
a = [3,6,23,6,34,69,5,32]
a.reverse()  #反转列表
print(a)
print(len(a))  #列表长度
'''

'''
#for循环
a = [3,6,23,6,34,69,5,32]
for b in a:
  print(b)
  print("数据：" + str(b))   #通过缩近来表示代码段，所以缩近的都属性于for循环内
'''

'''
for a in range(1,5):
  print(a)

b = list(range(1,11))   #range创建数字列表
print(b)
b = list(range(1,11,2))   #range创建数字列表,可以指定步长
print(b)
print(max(b))  #最大
print(min(b))  #最小
print(sum(b))  #合计
'''

'''
#列表解析[表达式，数据源]，解析for循环，由数据源提供值，表达式结果为列表的值
a = [b*2 for b in range(1,5)]
print(a)
'''

'''
#列表切片
a = list(range(1,11))
print(a[1:5])    #切片，获取列表中的一部分
print(a[8:])
print(a[:3])
for b in a[3:]:   #遍历切片
  print(b)
'''

'''
#列表复制(引用复制)
a = list(range(1,11))
b=a   #a和b为同一个引用
print(b)
b = a[8:]
print(b)
'''
'''
#列表复制(数值复制)
a = list(range(1,11))
b=a[:]   #b是a的副本，不是同一个引用
b.append(22)
print(a)
print(b)
'''

'''
#元组，不可变的列表
a = (1,2,3,4,5)
print(a)
# a[0] = 0  #语法错误，元素不要修改
a = (11,22,33,44,55)  #但元组可以重新赋值
print(a)
'''

#if条件语句
'''
a = "aa"
if a =="aA":   #区分大小写
  print(True)
elif a != "aa":
  print("不等于")
else:
  print(False)
'''
'''
a = 20
if a>1 and a<20:
  print(0)
elif a<1 or a>100:
  print(1)
else:
  print(3)
'''
'''
a = (1,2,3,4,5)
if 1 in a:   #包含
  print(1)
else:
  print(0)
if 6 not in a:   #不包含
  print(2)
'''
'''
a =[]
if a:
  print("有值")
else:
  print("列表为空")

a =[]
if len(a)>0:
  print("有值")
else:
  print("列表为空")
'''


#字典
'''
a = {1:'a',2:'b',3:'c',4:'d',"five":1.5}
print(a[1])
print(a["five"])  #按key取值
print(len(a))  #字典长度
a["six"] = 6  #添加键-值对
print(a)
del a["six"]  #删除键-值对
print(a)
'''
'''
ss = {}
a = {'name':"JJ",'age':12}
b = {'name':"GG",'age':22}
ss['JJ'] = a  #字典嵌套字典
ss['GG'] = b
print(ss)
ss = [a,b]  #列表嵌套字典
print(ss)
print(ss[1])
a = {'a':[1,2,3],'b':[4,5,6]}  #字典嵌套列表
print(a)
'''
'''
#遍历字典
a = {1:'a',2:'b',3:'c',4:'d',"five":1.5}
for key,value in a.items():   #遍历字典
  print(str(key) + ":"+ str(value))
'''
'''
#遍历字典所有的键
a = {1:'a',2:'b',3:'c',4:'d',"five":1.5}
for key in a.keys():
  print(key)
'''
'''
#对键进行排序
a = {1:'a',6:'b',3:'c',4:'d',5:1.5}
for key in sorted(a.keys()):
  print(key)
'''
'''
#遍历字典的值
a = {1:'a',6:'b',3:'c',4:'d',5:1.5}
for value in a.values():
  print(value)
'''
'''
#利用set集合去重
a = [1,1,2,2,3,3,4,4,5]
b = set(a)  #sset集合，值是唯一的
print(b)
'''


#用户输入字符串
# a = "用户输入:"
# a = input(a)
# print(a)
# a = '12'
# print(a)
# print(int(a)) #转换为数字类型

'''
#while循环
a=0
while a < 5:
  print(a)
  a+=1
  if a>3:
    break
print("exit")
'''

'''
a = list(range(1,6))
i=0
for b in a:    #for循环以索引的方式去读取数组，在循环中修改数组会导致部分元素遍历不到
  print(b)
  del a[0]
print(a)

a = list(range(1,6))
b=0
while b < len(a):   #while循环中修改数组,不会影响到元素遍历，说明while读取的是数组副本
  print(a[b])
  del a[0]
print(a)
'''

'''
a = ['1','1','2','2','3','3','4','4','5','5']
while '1' in a:  #删除列表中所有符合条件的值
  a.remove('1')
print(a)
'''

#函数
'''
def printf(a):
  print(a)

printf("hello world")
'''
'''
def printf(a,b):
  print(str(a) + "+" + str(b) + "=" + str(a+b))
printf(b=2,a=1)  #关键字实参
'''
'''
def printf(a,b=3):     #默认参数
  print(str(a) + "+" + str(b) + "=" + str(a+b))
printf(1)
'''
'''
def add(a,b):
  return a+b    #函数返回值
print(add(1,2))
'''
'''
a = [1,2,3,4,5]
def app(b):
  b.append(6)
  print(b)

app(a[:])  #以切片的方式传入一个列表副本，函数中修改的只是副本，不影响a列表
print(a)
'''
'''
def add(l, *a):   #任意多个参数，*a表示创建一个空元组来接收参数
  c = 0
  for b in a:
    c += b
  print(l)
  print(a)
  print(c)

add("参数",1,2,3,4,5)
cc = [3,44,55,66]
add("参数",11,2,22,33,44)
'''
'''
def show(**a):  #任意多个参数，**a表示创建一个空字典来接收参数
  print(a)
show(name="aaa",age=12,sex="nan")
'''



#类
'''
class Dog():   #定义类
  def __init__(self,name,age):   #构造函数，self参数为必须
    self.name=name
    self.age=age
    self.type = "dog"   #构造函数默认值，调用构造函数时可不传参

  def show(self):    #方法，访问自身属性时必须要有self参数
    print(self.name + "-" + str(self.age))

# myDog = Dog("div",12)   #实例化
# print(myDog.name)   #访问属性
# print(myDog.type)
# myDog.show()   #调用类方法


class Dog_a(Dog):   #继承Dog类
  def __init__(self,name,age):  #子类的构造函数
    super().__init__(name,age)   #必须先初始化父类
    self.host = "roundom"  #子类的属性
  def printf(self):   #子类的方法
    print(self.host)
  def show(self):    #重写父类方法
    print("hello world")

doga = Dog_a("div",12)
doga.printf()  #调用子类方法
doga.show()   #调用继承自父类的方法
'''

'''
from collections import OrderedDict  #Python标准库，导入OrderedDict类为一个有序字典
dic = OrderedDict()  #实例化一个空字典
dic['a'] = 'aaa'
dic['b'] = 'bbb'
dic['c'] = 'ccc'
dic['d'] = 'ddd'
for key,value in dic.items():
  print(key + " - " + value)
'''

'''
from random import randint  #导入随机数类
x = randint(0,6)   #随机生成0-6，投骰子机制
print(x)
'''

'''
# with open(".\\Module\\read.txt", 'r', encoding='utf-8') as fileText:   #打开文本文件，with表示使用完文件后自动关闭，也可以用close()手动关闭
with open("read.txt", 'r', encoding='utf-8') as fileText:
  contents = fileText.read()   #一次性读取
  print(contents)
'''

'''
with open("read.txt", 'r', encoding='utf-8') as fileText:
  for line in fileText:   #逐行读取文件
    print(line.rstrip())
'''

'''
with open("read.txt", 'r', encoding='utf-8') as fileText:
  contents = fileText.readlines()   #逐行读取文件
  for line in contents:   #逐行读取文件
    print(line.rstrip())


with open("read.txt", 'w', encoding='utf-8') as fileText:
  fileText.write("新加的内容")   #覆盖原来的内容重新写入

with open("read.txt", 'a', encoding='utf-8') as fileText:    #r表示只读，w表示可写，a表示附加，a+表示可读可写
  fileText.write("\n新加的内容")   #在原来的内容后新加入内容
  fileText.write(str(1234))   #在数值类型要先转换为字符串
'''

#异常捕捉
'''
#除零异常捕捉
try:
  a = 5/0
except ZeroDivisionError:    #除零异常捕捉
  print("被除数不为0")
else:     #没有异常则执行else
  print(a)
print("0000")
'''
'''
try:
  with open("read.txt", 'r', encoding='utf-8') as fileText:
    contents = fileText.read()   
    print(contents)
    a = 5/0
except FileNotFoundError:  #异常捕捉未找到此文件
  print("未找到此文件")
except ZeroDivisionError:  #多个异常捕捉
  print("被除数不为0")
'''
'''
try:
  a = 5/0
except ZeroDivisionError:    #除零异常捕捉
  pass    #python关键字，表示程序什么也不做
print("0000")

'''


'''
try:
  with open("read.txt", 'r', encoding='utf-8') as fileText:
    contents = fileText.read()
    words = contents.split()   #以空格或换行符分隔字符串，存入一个列表内
    print(len(words))
except FileNotFoundError:  #异常捕捉未找到此文件
  print("未找到此文件")
'''

'''
# json文件读写
import json
num = [1,2,3,4,5]
with open("jsonRead.json","w") as j_obj:
  json.dump(num,j_obj)  #以覆盖的方式写入json文件

with open("jsonRead.json","r") as j_obj:
  num1 = json.load(j_obj)   #读取json文件
  print(num1)
'''


a = 23
b = a.copy()
b -= 1
print(a)
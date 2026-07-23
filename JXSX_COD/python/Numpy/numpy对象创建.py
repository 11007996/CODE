import numpy as np
# print(np.__version__)


'''
 array 函数
 numpy.array(object, dtype = None, copy = True, order = None, subok = False, ndmin = 0)
 参数说明：
名称	描述
object	数组或嵌套的数列
dtype	数组元素的数据类型，可选
copy	对象是否需要复制，可选
order	创建数组的样式，C为行方向，F为列方向，A为任意方向（默认）
subok	默认返回一个与基类类型一致的数组
ndmin	指定生成数组的最小维度
'''

# array = np.array([[1,2],[3,4]],dtype="int64")
# print(array)

# a = np.array([1,2,3]) 
# print(a)

# a = np.array([1, 2, 3, 4, 5], ndmin =  2)   #设置最小维度
# print (a)

# a = np.array([1,  2,  3], dtype = complex)     #设置数据类型
# print (a)




'''
NumPy 数据类型
名称	描述
bool_	布尔型数据类型（True 或者 False）
int_	默认的整数类型（类似于 C 语言中的 long，int32 或 int64）
intc	与 C 的 int 类型一样，一般是 int32 或 int 64
intp	用于索引的整数类型（类似于 C 的 ssize_t，一般情况下仍然是 int32 或 int64）
int8	字节（-128 to 127）
int16	整数（-32768 to 32767）
int32	整数（-2147483648 to 2147483647）
int64	整数（-9223372036854775808 to 9223372036854775807）
uint8	无符号整数（0 to 255）
uint16	无符号整数（0 to 65535）
uint32	无符号整数（0 to 4294967295）
uint64	无符号整数（0 to 18446744073709551615）
float_	float64 类型的简写
float16	半精度浮点数，包括：1 个符号位，5 个指数位，10 个尾数位
float32	单精度浮点数，包括：1 个符号位，8 个指数位，23 个尾数位
float64	双精度浮点数，包括：1 个符号位，11 个指数位，52 个尾数位
complex_	complex128 类型的简写，即 128 位复数
complex64	复数，表示双 32 位浮点数（实数部分和虚数部分）
complex128	复数，表示双 64 位浮点数（实数部分和虚数部分）
'''

'''
a = np.array([1,  2,  3], dtype = "i4")     # int8, int16, int32, int64 四种数据类型可以使用字符串 'i1', 'i2','i4','i8' 代替
print (a.dtype)
'''


 # <意味着小端法(最小值存储在最小的地址，即低位组放在最前面)。>意味着大端法(最重要的字节存储在最小的地址，即高位组放在最前面
 # 使用标量类型
# dt = np.dtype('>i4')
# dt = np.dtype('<i4')
# print(dt)


'''
# 创建结构化数据类型,类似于结构体
dt = np.dtype([('age',np.int8),('M','S20')]) 
a = np.array([(10,'M'),(20,'F'),(30,'M')], dtype = dt) 
# a = np.array([(10),(20),(30)], dtype = dt) 
# a = np.array([10,20,30], dtype = dt) 
print(a)
print(a.dtype)
print(a["age"])
print(a["M"])
'''


'''
字符	对应类型
b	布尔型
i	(有符号) 整型
u	无符号整型 integer
f	浮点型
c	复数浮点型
m	timedelta（时间间隔）
M	datetime（日期时间）
O	(Python) 对象
S, a	(byte-)字符串
U	Unicode
V	原始数据 (void)
'''



'''
numpy.empty 方法用来创建一个指定形状（shape）、数据类型（dtype）且未初始化的数组
numpy.empty(shape, dtype = float, order = 'C')
参数说明：
参数  描述
shape   数组形状
dtype   数据类型，可选
order   有"C"和"F"两个选项,分别代表，行优先和列优先，在计算机内存中的存储元素的顺序。
'''
# x = np.empty([3,2], dtype = int) 
# print (x)   # 未初始化，所以数组数据是随机的
# x[0][0] = 1
# x[1][0] = 2
# print(x)


'''
创建指定大小的数组，数组元素以 0 来填充：
numpy.zeros(shape, dtype = float, order = 'C')
参数说明：
参数  描述
shape   数组形状
dtype   数据类型，可选
order   'C' 用于 C 的行数组，或者 'F' 用于 FORTRAN 的列数组
'''
'''
# 默认为浮点数
x = np.zeros(5) 
print(x)
# 设置类型为整数
y = np.zeros((5,), dtype = int) 
print(y)
# 自定义类型
z = np.zeros((2,2), dtype = [('x', 'i4'), ('y', 'i4')])  
print(z)
'''




'''
创建指定形状的数组，数组元素以 1 来填充：
numpy.ones(shape, dtype = None, order = 'C')
参数说明：
参数  描述
shape   数组形状
dtype   数据类型，可选
order   'C' 用于 C 的行数组，或者 'F' 用于 FORTRAN 的列数组
'''
'''
# 默认为浮点数
x = np.ones(5) 
print(x)
# 自定义类型
x = np.ones([2,2], dtype = int)
print(x)
'''




'''
numpy.zeros_like 用于创建一个与给定数组具有相同形状的数组，数组元素以0来填充。
numpy.zeros_like(a, dtype=None, order='K', subok=True, shape=None)
参数说明：
参数  描述
a   给定要创建相同形状的数组
dtype   创建的数组的数据类型
order   数组在内存中的存储顺序，可选值为 'C'（按行优先）或 'F'（按列优先），默认为 'K'（保留输入数组的存储顺序）
subok   是否允许返回子类，如果为 True，则返回一个子类对象，否则返回一个与 a 数组具有相同数据类型和存储顺序的数组
shape   创建的数组的形状，如果不指定，则默认为 a 数组的形状。
'''
'''
# 创建一个 3x3 的二维数组
arr = np.array([[1, 2, 3], [4, 5, 6], [7, 8, 9]])
# 创建一个与 arr 形状相同的，所有元素都为 0 的数组
zeros_arr = np.zeros_like(arr)
print(zeros_arr)
'''




'''
numpy.ones_like 用于创建一个与给定数组具有相同形状的数组，数组元素以 1 来填充。
numpy.ones_like(a, dtype=None, order='K', subok=True, shape=None)
参数  描述
a   给定要创建相同形状的数组
dtype   创建的数组的数据类型
order   数组在内存中的存储顺序，可选值为 'C'（按行优先）或 'F'（按列优先），默认为 'K'（保留输入数组的存储顺序）
subok   是否允许返回子类，如果为 True，则返回一个子类对象，否则返回一个与 a 数组具有相同数据类型和存储顺序的数组
shape   创建的数组的形状，如果不指定，则默认为 a 数组的形状。
'''
'''
# 创建一个 3x3 的二维数组
arr = np.array([[1, 2, 3], [4, 5, 6], [7, 8, 9]])
# 创建一个与 arr 形状相同的，所有元素都为 1 的数组
ones_arr = np.ones_like(arr)
print(ones_arr)
'''





'''
从已有的数组创建数组
numpy.asarray(a, dtype = None, order = None)
参数说明：
参数  描述
a   任意形式的输入参数，可以是，列表, 列表的元组, 元组, 元组的元组, 元组的列表，多维数组
dtype   数据类型，可选
order   可选，有"C"和"F"两个选项,分别代表，行优先和列优先，在计算机内存中的存储元素的顺序。
'''
'''
x =  [1,2,3] 
a = np.asarray(x)  
print (a)
'''
'''
x =  (1,2,3) 
a = np.asarray(x)  # 将元组转换为 ndarray:
print (a)
'''
'''
x =  [(1,2,3),(4,5,6)] 
# a = np.asarray(x)    # 将元组列表转换为 ndarray
a = np.asarray(x, dtype =  float)   # 设置ndarray数据类型
print(a)
'''




'''
numpy.frombuffer 用于实现动态数组。 接受 buffer 输入参数，以流的形式读入转化成 ndarray 对象。
numpy.frombuffer(buffer, dtype = float, count = -1, offset = 0)
参数  描述
buffer  可以是任意对象，会以流的形式读入。
dtype   返回数组的数据类型，可选
count   读取的数据数量，默认为-1，读取所有数据。
offset  读取的起始位置，默认为0。
'''
'''
s =  b'Hello World'   # 字节流
a = np.frombuffer(s, dtype =  'S1')  
print (a)
'''




'''
numpy.fromiter 方法从可迭代对象中建立 ndarray 对象，返回一维数组。
numpy.fromiter(iterable, dtype, count=-1)
参数  描述
iterable    可迭代对象
dtype   返回数组的数据类型
count   读取的数据数量，默认为-1，读取所有数据
'''
'''
# 使用 range 函数创建列表对象  
list=range(5)
it=iter(list)
# 使用迭代器创建 ndarray 
x=np.fromiter(it, dtype=float)
print(x)
'''




'''
numpy 包中的使用 arange 函数创建数值范围并返回 ndarray 对象
numpy.arange(start, stop, step, dtype)
参数说明：
参数  描述
start   起始值，默认为0
stop    终止值（不包含）
step    步长，默认为1
dtype   返回ndarray的数据类型，如果没有提供，则会使用输入数据的类型。
'''

# x = np.arange(5)    # 生成 0 到 4 长度为 5 的数组
# print (x)


# x = np.arange(10,20,2)    # 设置了起始值、终止值及步长
# print (x)








'''
numpy.linspace 函数用于创建一个一维数组，数组是一个等差数列构成的
np.linspace(start, stop, num=50, endpoint=True, retstep=False, dtype=None)
参数说明：
参数  描述
start   序列的起始值
stop    序列的终止值，如果endpoint为true，该值包含于数列中
num 要生成的等步长的样本数量，默认为50
endpoint    该值为 true 时，数列中包含stop值，反之不包含，默认是True。
retstep 如果为 True 时，生成的数组中会显示间距，反之不显示。
dtype   ndarray 的数据类型
'''
# a = np.linspace(1,10,20)
# print(a)
# a = np.linspace(10, 20,  5, endpoint =  False)   # 产生一个范围从10到20的等差数列，元素包含5个元素
# print(a)






'''
numpy.logspace 函数用于创建一个于等比数列
np.logspace(start, stop, num=50, endpoint=True, base=10.0, dtype=None)
参数  描述
start   序列的起始值为：base ** start
stop    序列的终止值为：base ** stop。如果endpoint为true，该值包含于数列中
num 要生成的等步长的样本数量，默认为50
endpoint    该值为 true 时，数列中中包含stop值，反之不包含，默认是True。
base    对数 log 的底数。
dtype   ndarray 的数据类型
'''
# 默认底数是 10
# a = np.logspace(1.0,  2.0, num =  10)   # 产生一个范围从1到2的等比数列，元素包含10个元素
# print (a)


import numpy as np
 

'''
a = np.arange(12).reshape(3,4)
print ('原数组：')
print (a)
print ('\n')
print ('转置数组：')    # 转换行列，行转列
print (a.T)
'''




'''
返回一个新的矩阵
numpy.matlib.empty(shape, dtype, order)
参数说明：
shape: 定义新矩阵形状的整数或整数元组
Dtype: 可选，数据类型
order: C（行序优先） 或者 F（列序优先）
'''
# print (np.matlib.empty((2,2)))  # 生成一个随机矩阵
# print (np.matlib.zeros((2,2)))   # 以0填充矩阵
# print (np.matlib.ones((2,2)))  # 以1填充矩阵



'''
numpy.matlib.eye(n, M,k, dtype)
参数说明：
n: 返回矩阵的行数
M: 返回矩阵的列数，默认为 n
k: 对角线的索引
dtype: 数据类型
'''
# print (np.matlib.eye(n =  3, M =  4, k =  0, dtype =  float))   #  返回一个矩阵，对角线元素为 1，其他位置为零





'''
返回给定大小的单位矩阵。
单位矩阵是个方阵，从左上角到右下角的对角线（称为主对角线）上的元素均为 1，除此以外全都为 0。
'''
# print (np.matlib.identity(5, dtype =  float))     # 单位矩阵

# print (np.matlib.rand(3,3))   # 建一个给定大小的矩阵，数据是随机填充的







'''
i = np.matrix('1,2;3,4')     # 创建矩阵
print (i)
j = np.asarray(i)   # 矩阵转换为二维数组
print (j)
k = np.asmatrix (j)     # 二维数组转换为矩阵
print (k)
'''






'''
函数名称	参数说明	功能描述
matrix(data[, dtype, copy])	data：类数组或字符串，dtype：数据类型，copy：是否复制	从数组或字符串创建矩阵对象
asmatrix(data[, dtype])	data：输入数据，dtype：数据类型（可选）	将输入转换为矩阵对象
bmat(obj[, ldict, gdict])	obj：字符串或嵌套序列，ldict、gdict：命名空间字典（可选）	从字符串、嵌套序列或数组构建矩阵
empty(shape[, dtype, order])	shape：形状，dtype：数据类型，order：存储顺序	创建未初始化元素的矩阵
zeros(shape[, dtype, order])	shape：形状，dtype：数据类型，order：存储顺序	创建全零矩阵
ones(shape[, dtype, order])	shape：形状，dtype：数据类型，order：存储顺序	创建全一矩阵
eye(n[, M, k, dtype, order])	n：行数，M：列数，k：对角线偏移，dtype：数据类型，order：存储顺序	创建对角线为1，其余为0的矩阵
identity(n[, dtype])	n：大小，dtype：数据类型（可选）	创建单位方阵
repmat(a, m, n)	a：数组或矩阵，m、n：重复次数	将数组或矩阵重复 m 行 n 列
rand(args)	args：指定矩阵形状	生成指定形状的均匀分布随机矩阵
randn(args)	args：指定矩阵形状	生成指定形状的标准正态分布随机矩阵
'''

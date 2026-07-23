import numpy as np
 



'''
a = np.arange(8)
print ('原始数组：')
print (a)
print ('\n')
b = a.reshape(2,4)    # 两行四列
print ('修改后的数组：')
print (b)
'''




'''
a = np.arange(9).reshape(3,3) 
print ('原始数组：')
for row in a:
    print (row)
#对数组中每个元素都进行处理，可以使用flat属性，该属性是一个数组元素迭代器：
print ('迭代后的数组：')
for element in a.flat:    # 迭代铺平数据
    print (element)
'''



'''
a = np.arange(8).reshape(2,4)
print ('原数组：')
print (a)
print ('\n')
# 默认按行
print ('展开的数组：')
print (a.flatten())    # 返回一份数组拷贝，对拷贝所做的修改不会影响原始数组
print ('\n')
print ('以 F 风格顺序展开的数组：')   # order：'C' -- 按行，'F' -- 按列，'A' -- 原顺序，'K' -- 元素在内存中的出现顺序
print (a.flatten(order = 'F'))
'''






'''
a = np.arange(8).reshape(2,4)
print ('原数组：')
print (a)
print ('\n')
print ('调用 ravel 函数之后：')
print (a.ravel())     # 功能与flatten类似，但返回一份数组的引用，对拷贝所做的修改会影响原始数组
print ('\n')
print ('以 F 风格顺序调用 ravel 函数之后：')
print (a.ravel(order = 'F'))
'''





'''
a = np.arange(12).reshape(3,4)
print ('原数组：')
print (a )
print ('\n')
print ('对换数组：')
print (np.transpose(a))  # 用于对换数组的维度,行转列
'''



'''
a = np.arange(12).reshape(3,4)
print ('原数组：')
print (a)
print ('\n')
print ('转置数组：')
print (a.T)   # numpy.ndarray.T 类似 numpy.transpose , 用于对换数组的维度,行转列
'''



'''
numpy.rollaxis(arr, axis, start)
参数说明：
arr：数组
axis：要向后滚动的轴，其它轴的相对位置不会改变
start：默认为零，表示完整的滚动。会滚动到特定位置。
'''
'''
# 创建了三维的 ndarray
a = np.arange(8).reshape(2,2,2)
print ('原数组：')
print (a)
print ('获取数组中一个值：')
print(np.where(a==6))   # 返回6这个值的索引
print(a[1,1,0])  # 为 6
print ('\n')
# 将轴 2 滚动到轴 0（宽度到深度）
print ('调用 rollaxis 函数：')
b = np.rollaxis(a,2,0)    # 向后滚动特定的轴到一个特定位置，xyz轴翻转
print (b)
# 查看元素 a[1,1,0]，即 6 的坐标，变成 [0, 1, 1]
# 最后一个 0 移动到最前面
print(np.where(b==6))   
print ('\n')
# 将轴 2 滚动到轴 1：（宽度到高度）
print ('调用 rollaxis 函数：')
c = np.rollaxis(a,2,1)
print (c)
# 查看元素 a[1,1,0]，即 6 的坐标，变成 [1, 0, 1]
# 最后的 0 和 它前面的 1 对换位置
print(np.where(c==6))   
print ('\n')
'''



'''
# 创建了三维的 ndarray
a = np.arange(8).reshape(2,2,2)
print ('原数组：')
print (a)
print ('\n')
# 现在交换轴 0（深度方向）到轴 2（宽度方向）
print ('调用 swapaxes 函数后的数组：')
print (np.swapaxes(a, 2, 0))   # 用于交换数组的两个轴
'''






'''
x = np.array([[1], [2], [3]])
y = np.array([4, 5, 6])  
# 对 y 广播 x
b = np.broadcast(x,y)  
# 它拥有 iterator 属性，基于自身组件的迭代器元组
print ('对 y 广播 x：')
r,c = b.iters
# Python3.x 为 next(context) ，Python2.x 为 context.next()
print (next(r), next(c))
print (next(r), next(c))
print ('\n')
# shape 属性返回广播对象的形状
print ('广播对象的形状：')
print (b.shape)
print ('\n')
# 手动使用 broadcast 将 x 与 y 相加
b = np.broadcast(x,y)
c = np.empty(b.shape)
print ('手动使用 broadcast 将 x 与 y 相加：')
print (c.shape)
print ('\n')
c.flat = [u + v for (u,v) in b]
print ('调用 flat 函数：')
print (c)
print ('\n')
# 获得了和 NumPy 内建的广播支持相同的结果
print ('x 与 y 的和：')
print (x + y)
'''






 
'''
x = np.array(([1,2],[3,4]))
print ('数组 x：')
print (x)
print ('\n')
y = np.expand_dims(x, axis = 0)   # 通过在指定位置插入新的轴来扩展数组形状
print ('数组 y：')
print (y)
print ('\n')
print ('数组 x 和 y 的形状：')
print (x.shape, y.shape)
print ('\n')
# 在位置 1 插入轴
y = np.expand_dims(x, axis = 1)
print ('在位置 1 插入轴之后的数组 y：')
print (y)
print ('\n')
print ('x.ndim 和 y.ndim：')
print (x.ndim,y.ndim)
print ('\n')
print ('x.shape 和 y.shape：')
print (x.shape, y.shape)
'''




'''
x = np.arange(9).reshape(1,3,3)
print ('数组 x：')
print (x)
print ('\n')
y = np.squeeze(x)   # 从给定数组的形状中删除一维的条目
print ('数组 y：')
print (y)
print ('\n')
print ('数组 x 和 y 的形状：')
print (x.shape, y.shape)
'''





'''
a = np.array([[1,2],[3,4]])
print ('第一个数组：')
print (a)
print ('\n')
b = np.array([[5,6],[7,8]])
print ('第二个数组：')
print (b)
print ('\n')
# 两个数组的维度相同
print ('沿轴 0 连接两个数组：')
print (np.concatenate((a,b)))    # 连接两个数组
print ('\n')
print ('沿轴 1 连接两个数组：')
print (np.concatenate((a,b),axis = 1))
'''




'''
a = np.array([[1,2],[3,4]])
print ('第一个数组：')
print (a)
print ('\n')
b = np.array([[5,6],[7,8]])
print ('第二个数组：')
print (b)
print ('\n')
print ('沿轴 0 堆叠两个数组：')
print (np.stack((a,b),0))    # 用于沿新轴连接数组序列,增加一个轴
print ('\n')
print ('沿轴 1 堆叠两个数组：')
print (np.stack((a,b),1))
'''



'''

a = np.array([[1,2],[3,4]])
print ('第一个数组：')
print (a)
print ('\n')
b = np.array([[5,6],[7,8]])
print ('第二个数组：')
print (b)
print ('\n')
print ('水平堆叠：')
c = np.hstack((a,b))    # 水平展开增加合并数据
print (c)
print ('\n')
'''






'''
a = np.array([[1,2],[3,4]])
print ('第一个数组：')
print (a)
print ('\n')
b = np.array([[5,6],[7,8]])
print ('第二个数组：')
print (b)
print ('\n')
print ('竖直堆叠：')
c = np.vstack((a,b))    # 垂直合并数据
print (c)
'''




'''
a = np.arange(9)
print ('第一个数组：')
print (a)
print ('\n')
print ('将数组分为三个大小相等的子数组：')
b = np.split(a,3)    # 分割数组
print (b)
print ('\n')
print ('将数组在一维数组中表明的位置分割：')
b = np.split(a,[4,7])
print (b)
'''




# numpy.split(ary, indices_or_sections, axis)
# 参数说明：
# ary：被分割的数组
# indices_or_sections：如果是一个整数，就用该数平均切分，如果是一个数组，为沿轴切分的位置（左开右闭）
# axis：设置沿着哪个方向进行切分，默认为 0，横向切分，即水平方向。为 1 时，纵向切分，即竖直方向。
'''
a = np.arange(16).reshape(4, 4)
print('第一个数组：')
print(a)
print('\n')
print('默认分割（0轴）：')
b = np.split(a,2)
print(b)
print('\n')
print('沿水平方向分割：')
c = np.split(a,2,1)   # axis 为 0 时在水平方向分割，axis 为 1 时在垂直方向分割
print(c)
print('\n')
print('沿水平方向分割：')
d= np.hsplit(a,2)
print(d)
'''




'''
harr = np.floor(10 * np.random.random((2, 6)))
print ('原array：')
print(harr)
print ('拆分后：')
print(np.hsplit(harr, 3))   # 用于水平分割数组，通过指定要返回的相同形状的数组数量来拆分原数组
'''




'''
a = np.arange(16).reshape(4,4)
print ('第一个数组：')
print (a)
print ('\n')
print ('竖直分割：')
b = np.vsplit(a,2)    # 沿着垂直轴分割，通过指定要返回的相同形状的数组数量来拆分原数组
print (b)
'''





'''
a = np.array([[1,2,3],[4,5,6]])
print ('第一个数组：')
print (a)
print ('\n')
print ('第一个数组的形状：')
print (a.shape)
print ('\n')
b = np.resize(a, (3,2))
print ('第二个数组：')
print (b)
print ('\n')
print ('第二个数组的形状：')
print (b.shape)
print ('\n')
# 要注意 a 的第一行在 b 中重复出现，因为尺寸变大了
print ('修改第二个数组的大小：')
b = np.resize(a,(3,3))    # 修改数组形状
print (b)
'''




'''
a = np.array([[1,2,3],[4,5,6]])
print ('第一个数组：')
print (a)
print ('\n')
print ('向数组添加元素：')
print (np.append(a, [7,8,9]))
print ('\n')
print ('沿轴 0 添加元素：')
print (np.append(a, [[7,8,9]],axis = 0))
print ('\n')
print ('沿轴 1 添加元素：')
print (np.append(a, [[5,5,5],[7,8,9]],axis = 1))
'''



'''
# 在给定索引之前，沿给定轴在输入数组中插入值。
numpy.insert(arr, obj, values, axis)
参数说明：
arr：输入数组
obj：在其之前插入值的索引
values：要插入的值
axis：沿着它插入的轴，如果未提供，则输入数组会被展开
'''
'''
a = np.array([[1,2],[3,4],[5,6]])
print ('第一个数组：')
print (a)
print ('\n')
print ('未传递 Axis 参数。 在删除之前输入数组会被展开。')
print (np.insert(a,3,[11,12]))
print ('\n')
print ('传递了 Axis 参数。 会广播值数组来配输入数组。')
print ('沿轴 0 广播：')
print (np.insert(a,1,[11],axis = 0))
print ('\n')
print ('沿轴 1 广播：')
print (np.insert(a,1,11,axis = 1))
'''






# 返回从输入数组中删除指定子数组的新数组
'''
Numpy.delete(arr, obj, axis)
参数说明：
arr：输入数组
obj：可以被切片，整数或者整数数组，表明要从输入数组删除的子数组
axis：沿着它删除给定子数组的轴，如果未提供，则输入数组会被展开
'''
'''
a = np.arange(12).reshape(3,4)
print ('第一个数组：')
print (a)
print ('\n')
print ('未传递 Axis 参数。 在插入之前输入数组会被展开。')
print (np.delete(a,5))    # 删除5
print ('\n')
print ('删除第二列：')
print (np.delete(a,1,axis = 1))
print ('\n')
print ('包含从数组中删除的替代值的切片：')
a = np.array([1,2,3,4,5,6,7,8,9,10])
print (np.delete(a, np.s_[::2]))    # np.s_[start:stop:step]等价于 slice(start, stop, step)
'''




'''
# 用于去除数组中的重复元素。
numpy.unique(arr, return_index, return_inverse, return_counts)
arr：输入数组，如果不是一维数组则会展开
return_index：如果为true，返回新列表元素在旧列表中的位置（下标），并以列表形式储
return_inverse：如果为true，返回旧列表元素在新列表中的位置（下标），并以列表形式储
return_counts：如果为true，返回去重数组中的元素在原数组中的出现次数
'''


'''
a = np.array([5,2,6,2,7,5,6,8,2,9])
print ('第一个数组：')
print (a)
print ('\n')
print ('第一个数组的去重值：')
u = np.unique(a)
print (u)
print ('\n')
print ('去重数组的索引数组：')
u,indices = np.unique(a, return_index = True)   # 返回索引值
print (indices)
print ('\n')
print ('我们可以看到每个和原数组下标对应的数值：')
print (a)
print ('\n')
print ('去重数组的下标：')
u,indices = np.unique(a,return_inverse = True)
print (u)
print ('\n')
print ('下标为：')
print (indices)
print ('\n')
print ('使用下标重构原数组：')
print (u[indices])
print ('\n')
print ('返回去重元素的重复数量：')
u,indices = np.unique(a,return_counts = True)  # 统计各元素重复了多少次
print (u)
print (indices)
'''





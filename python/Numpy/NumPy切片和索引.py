import numpy as np
 
# a = np.arange(10)
# s = slice(2,7,2)   # 从索引 2 开始到索引 7 停止，间隔为2
# print (a[s])
# b = a[2:7:2]   # 从索引 2 开始到索引 7 停止，间隔为 2   通过冒号分隔切片参数 start:stop:step 来进行切片操作
# print(b)


# a = np.arange(10)  # [0 1 2 3 4 5 6 7 8 9]
# b = a[3] 
# b = a[3:5]    # 获取索引[3,4]闭区间的值
# b = a[3:]    # 获取索引[3,+∞)区间的值
# print(b)


'''
a = np.array([[1,2,3],[3,4,5],[4,5,6]])  
print (a[...,1])   # 第2列元素
print (a[1,...])   # 第2行元素
print (a[...,1:])  # 第2列及剩下的所有元素
'''




'''
# 整数数组索引
x = np.array([[1,  2],  [3,  4],  [5,  6]]) 
y = x[[0,1,2], [0,1,0]]    # 整数数组索引是指使用一个数组来访问另一个数组的元素。这个数组中的每个元素都是目标数组中某个维度上的索引值
print (y)
'''



'''
x = np.array([[  0,  1,  2],[  3,  4,  5],[  6,  7,  8],[  9,  10,  11]])  
print ('我们的数组是：' )
print (x)
print ('\n')
rows = np.array([[0,0],[3,3]]) 
cols = np.array([[0,2],[0,2]]) 
y = x[rows,cols]  
print  ('这个数组的四个角元素是：')
print (y)
'''



'''
a = np.array([[1,2,3], [4,5,6],[7,8,9]])
b = a[1:3, 1:3]
c = a[1:3,[1,2]]
d = a[...,1:]
print(b)
print(c)
print(d)
'''





'''
x = np.array([[  0,  1,  2],[  3,  4,  5],[  6,  7,  8],[  9,  10,  11]])  
print ('我们的数组是：')
print (x)
print ('\n')
# 现在我们会打印出大于 5 的元素  
print  ('大于 5 的元素是：')
print (x[x >  5])   # 数据过滤，转成了1维数组
'''



'''
a = np.array([np.nan,  1,2,np.nan,3,4,5])  
print (a[~np.isnan(a)])
'''


'''
a = np.array([1,  2+6j,  5,  3.5+5j])  
print (a[np.iscomplex(a)])    # 是否是复数
'''





'''
x = np.arange(9)
print(x)
# 一维数组读取指定下标对应的元素
print("-------读取下标对应的元素-------")
x2 = x[[0, 6]] # 使用花式索引
print(x2)
print(x2[0])
print(x2[1])
'''



'''
x=np.arange(32).reshape((8,4))   # 创建二维数组
print(x)
# 二维数组读取指定下标对应的行
print("-------读取下标对应的行-------")
print (x[[4,2,1,7]])    # 传入顺序索引数组
'''



'''
x=np.arange(32).reshape((8,4))
print(x)
print (x[[-4,-2,-1,-7]])    # 传入倒序索引数组
'''




'''
x=np.arange(32).reshape((8,4))
print(x)
print("-------读取下标对应的行-------")
print (x[np.ix_([1,5,7,2],[0,3,1,2])])    传入多个索引数组,产生笛卡尔积的映射关系
'''

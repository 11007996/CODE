import numpy as np
 
# 迭代器对象 numpy.nditer 提供了一种灵活访问一个或者多个数组元素的方式


'''
a = np.arange(6).reshape(2,3)    # 不改变数据的条件下修改形状
print ('原始数组是：')
print (a)
print ('\n')
print ('迭代输出元素：')
for x in np.nditer(a):
    print (x, end=", " )
print ('\n')
'''






'''
a = np.arange(6).reshape(2,3)
print(a)
for x in np.nditer(a.T):
    print (x, end=", " )
print ('\n')
for x in np.nditer(a.T.copy(order='C')):    #以列序循环
    print (x, end=", " )
print ('\n')
for x in np.nditer(a.T.copy(order='F')):    #以行序循环
    print (x, end=", " )
print ('\n')
'''




'''
flags 参数
参数  描述
c_index 可以跟踪 C 顺序的索引
f_index 可以跟踪 Fortran 顺序的索引
multi_index 每次迭代可以跟踪一种索引类型
external_loop   给出的值是具有多个值的一维数组，而不是零维数组
'''




'''
a = np.arange(0,60,5) 
a = a.reshape(3,4)  
print ('原始数组是：')
print (a)
print ('\n')
for x in np.nditer(a, op_flags=['readwrite']):     # 实现对数组元素值的修改，必须指定 readwrite 或者 writeonly 的模式
    x[...]=2*x 
print ('修改后的数组是：')
print (a)
'''




'''
a = np.arange(0,60,5) 
a = a.reshape(3,4)  
print ('原始数组是：')
print (a)
print ('\n')
print ('修改后的数组是：')
for x in np.nditer(a, flags =  ['external_loop'], order =  'F'):  
   print (x, end=", " )
'''




'''
a = np.arange(0,60,5) 
a = a.reshape(3,4)  
print  ('第一个数组为：')
print (a)
print  ('\n')
print ('第二个数组为：')
b = np.array([1,  2,  3,  4], dtype =  int)  
print (b)
print ('\n')
print ('修改后的数组为：')
for x,y in np.nditer([a,b]):      # 同时迭代两个数组，并广播
    print ("%d:%d"  %  (x,y), end=", " )
'''






'''
a = np.arange(9).reshape(3,3) 
print ('原始数组：')
for row in a:
    print (row)
#对数组中每个元素都进行处理，可以使用flat属性，该属性是一个数组元素迭代器：
print ('迭代后的数组：')
for element in a.flat:    # flat数组迭代器雩㻬
    print (element)
'''

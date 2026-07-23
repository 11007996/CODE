import numpy as np 
 
'''
numpy.amin(a, axis=None, out=None, keepdims=<no value>, initial=<no value>, where=<no value>)
参数说明：
a: 输入的数组，可以是一个NumPy数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算最小值。如果不提供此参数，则返回整个数组的最小值。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
out: 可选参数，用于指定结果的存储位置。
keepdims: 可选参数，如果为True，将保持结果数组的维度数目与输入数组相同。如果为False（默认值），则会去除计算后维度为1的轴。
initial: 可选参数，用于指定一个初始值，然后在数组的元素上计算最小值。
where: 可选参数，一个布尔数组，用于指定仅考虑满足条件的元素。
'''





'''
numpy.amax(a, axis=None, out=None, keepdims=<no value>, initial=<no value>, where=<no value>)
参数说明：
a: 输入的数组，可以是一个NumPy数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算最大值。如果不提供此参数，则返回整个数组的最大值。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
out: 可选参数，用于指定结果的存储位置。
keepdims: 可选参数，如果为True，将保持结果数组的维度数目与输入数组相同。如果为False（默认值），则会去除计算后维度为1的轴。
initial: 可选参数，用于指定一个初始值，然后在数组的元素上计算最大值。
where: 可选参数，一个布尔数组，用于指定仅考虑满足条件的元素。
'''





'''
a = np.array([[3,7,5],[8,4,3],[2,4,9]])  
print ('我们的数组是：')
print (a)
print ('\n')
print ('调用 amin() 函数：')   # 最小值
print (np.amin(a,1))
print ('\n')
print ('再次调用 amin() 函数：')   # 这里存在一个广播
print (np.amin(a,0))
print ('\n')
print ('调用 amax() 函数：')    # 最大值
print (np.amax(a))
print ('\n')
print ('再次调用 amax() 函数：')
print (np.amax(a, axis =  0))
'''









'''
# 计算数组中元素最大值与最小值的差（最大值 - 最小值）
numpy.ptp(a, axis=None, out=None, keepdims=<no value>, initial=<no value>, where=<no value>)
参数说明：
a: 输入的数组，可以是一个 NumPy 数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算峰-峰值。如果不提供此参数，则返回整个数组的峰-峰值。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
out: 可选参数，用于指定结果的存储位置。
keepdims: 可选参数，如果为 True，将保持结果数组的维度数目与输入数组相同。如果为 False（默认值），则会去除计算后维度为1的轴。
initial: 可选参数，用于指定一个初始值，然后在数组的元素上计算峰-峰值。
where: 可选参数，一个布尔数组，用于指定仅考虑满足条件的元素。
'''




'''
a = np.array([[3,7,5],[8,4,3],[2,4,9]])  
print ('我们的数组是：')
print (a)
print ('\n')
print ('调用 ptp() 函数：')
print (np.ptp(a))    # 计算数组中元素最大值与最小值的差
print ('\n')
print ('沿轴 1 调用 ptp() 函数：')
print (np.ptp(a, axis =  1))
print ('\n')
print ('沿轴 0 调用 ptp() 函数：')
print (np.ptp(a, axis =  0))
'''




# 百分位数是统计中使用的度量，表示小于这个值的观察值的百分比
'''
numpy.percentile(a, q, axis)
参数说明：
a: 输入数组
q: 要计算的百分位数，在 0 ~ 100 之间
axis: 沿着它计算百分位数的轴
'''
'''
a = np.array([[10, 7, 4], [3, 2, 1]])
print ('我们的数组是：')
print (a)
print ('调用 percentile() 函数：')
# 50% 的分位数，就是 a 里排序之后的中位数
print (np.percentile(a, 50)) 
# axis 为 0，在纵列上求
print (np.percentile(a, 50, axis=0)) 
# axis 为 1，在横行上求
print (np.percentile(a, 50, axis=1)) 
# 保持维度不变
print (np.percentile(a, 50, axis=1, keepdims=True))
'''



# 用于计算数组 a 中元素的中位数（中值）
'''
numpy.median(a, axis=None, out=None, overwrite_input=False, keepdims=<no value>)
参数说明：
a: 输入的数组，可以是一个 NumPy 数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算中位数。如果不提供此参数，则计算整个数组的中位数。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
out: 可选参数，用于指定结果的存储位置。
overwrite_input: 可选参数，如果为True，则允许在计算中使用输入数组的内存。这可能会在某些情况下提高性能，但可能会修改输入数组的内容。
keepdims: 可选参数，如果为True，将保持结果数组的维度数目与输入数组相同。如果为False（默认值），则会去除计算后维度为1的轴。
'''


'''
a = np.array([[30,65,70],[80,95,10],[50,90,60]])  
print ('我们的数组是：')
print (a)
print ('\n')
print ('调用 median() 函数：')
print (np.median(a))    # 获取中位数
print ('\n')
print ('沿轴 0 调用 median() 函数：')
print (np.median(a, axis =  0))
print ('\n')
print ('沿轴 1 调用 median() 函数：')
print (np.median(a, axis =  1))
'''





# 返回数组中元素的算术平均值，如果提供了轴，则沿其计算。
'''
numpy.mean(a, axis=None, dtype=None, out=None, keepdims=<no value>)
参数说明：
a: 输入的数组，可以是一个 NumPy 数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算平均值。如果不提供此参数，则计算整个数组的平均值。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
dtype: 可选参数，用于指定输出的数据类型。如果不提供，则根据输入数据的类型选择合适的数据类型。
out: 可选参数，用于指定结果的存储位置。
keepdims: 可选参数，如果为True，将保持结果数组的维度数目与输入数组相同。如果为False（默认值），则会去除计算后维度为1的轴。
'''
'''
a = np.array([[1,2,3],[3,4,5],[4,5,6]])  
print ('我们的数组是：')
print (a)
print ('\n')
print ('调用 mean() 函数：')
print (np.mean(a))
print ('\n')
print ('沿轴 0 调用 mean() 函数：')
print (np.mean(a, axis =  0))
print ('\n')
print ('沿轴 1 调用 mean() 函数：')
print (np.mean(a, axis =  1))
'''






# 根据在另一个数组中给出的各自的权重计算数组中元素的加权平均值
# 数组[1,2,3,4]和相应的权重[4,3,2,1]，通过将相应元素的乘积相加，并将和除以权重的和，来计算加权平均值
# 加权平均值 = (1*4+2*3+3*2+4*1)/(4+3+2+1)
'''
numpy.average(a, axis=None, weights=None, returned=False)
参数说明：
a: 输入的数组，可以是一个 NumPy 数组或类似数组的对象。
axis: 可选参数，用于指定在哪个轴上计算加权平均值。如果不提供此参数，则计算整个数组的加权平均值。可以是一个整数表示轴的索引，也可以是一个元组表示多个轴。
weights: 可选参数，用于指定对应数据点的权重。如果不提供权重数组，则默认为等权重。
returned: 可选参数，如果为True，将同时返回加权平均值和权重总和。
'''
'''
a = np.array([1,2,3,4])  
print ('我们的数组是：')
print (a)
print ('\n')
print ('调用 average() 函数：')
print (np.average(a))
print ('\n')
# 不指定权重时相当于 mean 函数
wts = np.array([4,3,2,1])  
print ('再次调用 average() 函数：')
print (np.average(a,weights = wts))
print ('\n')
# 如果 returned 参数设为 true，则返回权重的和  
print ('权重的和：')
print (np.average([1,2,3,  4],weights =  [4,3,2,1], returned =  True))
'''



# 标准差
# print (np.std([1,2,3,4]))


# 方差，标准差是方差的平方根。
# print (np.var([1,2,3,4]))
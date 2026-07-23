'''
函数	描述
dot	两个数组的点积，即元素对应相乘。
vdot	两个向量的点积
inner	两个数组的内积
matmul	两个数组的矩阵积
determinant	数组的行列式
solve	求解线性矩阵方程
inv	计算矩阵的乘法逆矩阵
'''

import numpy.matlib
import numpy as np



'''
numpy.dot(a, b, out=None) 
参数说明：
a : ndarray 数组
b : ndarray 数组
out : ndarray, 可选，用来保存dot()的计算结果
'''


 
'''
a = np.array([[1,2],[3,4]])
b = np.array([[11,12],[13,14]])
print(np.dot(a,b))     # 数组的点积 [[1*11+2*13, 1*12+2*14],[3*11+4*13, 3*12+4*14]]
'''




'''
a = np.array([[1,2],[3,4]]) 
b = np.array([[11,12],[13,14]]) 
# vdot 将数组展开计算内积
print (np.vdot(a,b))    # 向量的点积 1*11 + 2*12 + 3*13 + 4*14 = 130
'''



# print (np.inner(np.array([1,2,3]),np.array([0,1,0])))    # 一维数组的向量内积  1*0+2*1+3*0




'''
a = np.array([[1,2], [3,4]]) 
print ('数组 a：')
print (a)
b = np.array([[11, 12], [13, 14]]) 
print ('数组 b：')
print (b)
print ('内积：')
print (np.inner(a,b))    # 数组的内积 1*11+2*12, 1*13+2*14 3*11+4*12, 3*13+4*14
'''





'''
a = [[1,0],[0,1]] 
b = [[4,1],[2,2]] 
print (np.matmul(a,b))    # 矩阵乘积
'''




'''
a = np.array([[1,2], [3,4]])
print (np.linalg.det(a))    # 计算输入矩阵的行列式,是左上和右下元素的乘积与其他两个的乘积的差

b = np.array([[6,1,1], [4, -2, 5], [2,8,7]]) 
print (b)
print (np.linalg.det(b))
print (6*(-2*7 - 5*8) - 1*(4*7 - 5*2) + 1*(4*8 - -2*2))
'''





# 设A是数域上的一个n阶矩阵，若在相同数域上存在另一个n阶矩阵B，使得： AB=BA=E ，则我们称B是A的逆矩阵，而A则被称为可逆矩阵。
'''
x = np.array([[1,2],[3,4]]) 
y = np.linalg.inv(x)     # 计算矩阵的乘法逆矩阵
print (x)
print (y)
print (np.dot(x,y))
'''
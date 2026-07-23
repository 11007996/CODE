# 稀疏矩阵（英语：sparse matrix）指的是在数值分析中绝大多数数值为零的矩阵。反之，如果大部分元素都非零，则这个矩阵是稠密的(Dense)。



import numpy as np
from scipy.sparse import csr_matrix



# arr = np.array([[0, 0, 0], [0, 0, 1], [1, 0, 2]])

# print(csr_matrix(arr))   # CSR - 压缩稀疏行（Compressed Sparse Row），按行压缩。
# print(csr_matrix(arr).data)    # data 属性查看存储的数据（不含 0 元素）
# print(csr_matrix(arr).count_nonzero())   # 计算非 0 元素的总数
'''
mat = csr_matrix(arr)
mat.eliminate_zeros()
print(mat)    # 删除矩阵中 0 元素
'''
'''
mat = csr_matrix(arr)
mat.sum_duplicates()    # 删除重复项
print(mat)
'''
# print(csr_matrix(arr).tocsc())    #  CSC - 压缩稀疏列（Compressed Sparse Column），按列压缩。



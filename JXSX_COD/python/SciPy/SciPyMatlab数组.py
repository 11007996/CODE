'''
以 Matlab 格式导出数据
savemat() 方法可以导出 Matlab 格式的数据。
该方法参数有：
filename - 保存数据的文件名。
mdict - 包含数据的字典。
do_compression - 布尔值，指定结果数据是否压缩。默认为 False。
'''

from scipy import io
import numpy as np
arr = np.array([0, 1, 2, 3, 4, 5, 6, 7, 8, 9,])
# 导出
io.savemat('arr.mat', {"vec": arr})   # 以 Matlab 格式导出数据
# 导入
mydata = io.loadmat('arr.mat')   # 以导入Matlab格式数据
# mydata = io.loadmat('arr.mat', squeeze_me=True)     # 以导入Matlab格式数据的一维数组
print(mydata)
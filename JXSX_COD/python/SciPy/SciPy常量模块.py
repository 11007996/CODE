'''
模块名	功能	参考文档
scipy.cluster	向量量化	cluster API
scipy.constants	数学常量	constants API
scipy.fft	快速傅里叶变换	fft API
scipy.integrate	积分	integrate API
scipy.interpolate	插值	interpolate API
scipy.io	数据输入输出	io API
scipy.linalg	线性代数	linalg API
scipy.misc	图像处理	misc API
scipy.ndimage	N 维图像	ndimage API
scipy.odr	正交距离回归	odr API
scipy.optimize	优化算法	optimize API
scipy.signal	信号处理	signal API
scipy.sparse	稀疏矩阵	sparse API
scipy.spatial	空间数据结构和算法	spatial API
scipy.special	特殊数学函数	special API
scipy.stats	统计函数	stats.mstats API
'''



from scipy import constants

print(constants.pi)    # PI
print(constants.golden)   # 黄金比例
print(dir(constants))   # constants包含的所有常量
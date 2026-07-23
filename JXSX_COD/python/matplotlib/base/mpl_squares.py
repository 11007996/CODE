import matplotlib.pyplot as plt
import numpy as np
'''
# 拆线图
squares = [1,4,9,16,25]   # y轴的值
input_values = [1,2,3,4,5] # x轴的值
plt.plot(input_values,squares,linewidth = 5)   # 画线，并设置线条粗细
plt.title("Square Numbers",fontsize = 24)  # 设置标题，并字体大小
plt.xlabel("Value",fontsize = 14)   # X轴标题并字体大小
plt.ylabel("Square of Value",fontsize = 14)  # Y轴标题并字体大小
plt.tick_params(axis="both",labelsize=10)   # 设置高刻度样式
plt.show()   # 打开matplotlib查看器，并绘制图形
'''

'''
xpoints = np.array([1, 2, 6, 8])
ypoints = np.array([3, 8, 1, 10])
plt.plot(xpoints, ypoints)  # 拆线图
plt.show()
'''

'''
xpoints = np.array([1, 8])
ypoints = np.array([3, 10])
plt.plot(xpoints, ypoints, 'o')  # 散点图
plt.show()
'''

'''
x = np.arange(0,4*np.pi,0.1)   # start,stop,step
y = np.sin(x)
z = np.cos(x)
plt.plot(x,y,x,z)   # 正弦和余弦图
plt.show()
'''
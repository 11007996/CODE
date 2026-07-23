'''
bar() 方法语法格式如下：
matplotlib.pyplot.bar(x, height, width=0.8, bottom=None, *, align='center', data=None, **kwargs)
参数说明：
x：浮点型数组，柱形图的 x 轴数据。
height：浮点型数组，柱形图的高度。
width：浮点型数组，柱形图的宽度。
bottom：浮点型数组，底座的 y 坐标，默认 0。
align：柱形图与 x 坐标的对齐方式，'center' 以 x 位置为中心，这是默认值。 'edge'：将柱形图的左边缘与 x 位置对齐。要对齐右边缘的条形，可以传递负数的宽度值及 align='edge'。
**kwargs：：其他参数。
'''


import matplotlib.pyplot as plt
import numpy as np

x = np.array(["Runoob-1", "Runoob-2", "Runoob-3", "C-RUNOOB"])
y = np.array([12, 22, 6, 18])
# plt.bar(x,y)  # X轴方向柱形图
# plt.barh(x,y)  # Y轴方向柱形图
# plt.bar(x, y, color = "#4CAF50")   # 设置柱状图的颜色
# plt.bar(x, y,  color = ["#4CAF50","red","hotpink","#556B2F"])   # 设置各柱形的颜色
# plt.bar(x, y, width = 0.2)  # 设置和柱形的宽度
plt.show()



import matplotlib.pyplot as plt
import numpy as np
import matplotlib

# 折线图
# ypoints = np.array([1,3,4,5,8,9,6,1,3,4,5,2,4])
# xpoints = np.array([1,2,3,4,5,6,7,8,9,10,11,12,13])  # X轴可省略，则自动生成X轴
# 参数样式：marker = '[marker][line][color]'  # 绘图标记，线样式，线颜色
# plt.plot(ypoints, marker = 'P')   # marker绘图标记
# plt.plot(ypoints, 'o:r')   # o表示绘图标记为实心圆，：表示虚线，r表示红色
# plt.plot(ypoints, color = 'r')  # color设置线颜色
# plt.plot(ypoints, linewidth = '5')   # linewidth设置线宽度
# plt.plot(ypoints, 'o-.r', ms = 10,mfc='b',mec='r')   # ms：定义标记的大小。mfc：定义标记内部的颜色。mec：定义标记边框的颜色。
# plt.plot(ypoints, '', ms = 10, mec = '#4CAF50', mfc = '#CC0000')   # 没有绘图标记
# plt.plot(ypoints, 'o-.r', ms = 10, mec = '#4CAF50', mfc = '#CC0000')
# plt.plot(xpoints,ypoints, 'o-.r', ms = 10, mec = '#4CAF50', mfc = '#CC0000')
# plt.show()



# 画两条拆线
'''  # X轴为默认值
y1 = np.array([3, 7, 5, 9])
y2 = np.array([6, 2, 13, 10])
plt.plot(y1)
plt.plot(y2)
'''
'''   # 设置X轴
x1 = np.array([0, 1, 2, 3])
y1 = np.array([3, 7, 5, 9])
x2 = np.array([0, 1, 2, 3])
y2 = np.array([6, 2, 13, 10])
plt.plot(x1, y1, x2, y2)
'''
# plt.show()



# 轴标签和标题
'''
ypoints = np.array([1,3,4,5,8,9,6,1,3,4,5,2,4])
plt.title("RUNOOB TEST TITLE")
plt.xlabel("x - label")
# plt.ylabel("y - label")
zhfont1 = matplotlib.font_manager.FontProperties(fname="C:\Windows\Fonts\simhei.ttf")    # matplotlib默认不支持中文，需手动设置中文字体
plt.ylabel("y - 轴",fontproperties=zhfont1)  # 指定中文字体
plt.plot(ypoints)
plt.show()
'''
'''
ypoints = np.array([1,3,4,5,8,9,6,1,3,4,5,2,4])
zhfont1 = matplotlib.font_manager.FontProperties(fname="C:\Windows\Fonts\simhei.ttf", size=18)  # size 参数设置标题字体大小
font1 = {'color':'blue','size':20}
font2 = {'color':'darkred','size':15}
plt.title("菜鸟教程 - 测试", fontproperties=zhfont1, fontdict = font2)  # fontdict 可以使用css来设置字体样式
plt.xlabel("x 轴", fontproperties=zhfont1,loc="left")  # fontproperties 设置中文显示，fontsize 设置字体大小，loc参数设置X轴标题的位置left,right,center
plt.ylabel("y 轴", fontproperties=zhfont1,loc="bottom")  # loc参数设置Y轴标题的位置，center,bottom,top，默认为center
plt.plot(ypoints)
plt.show()
'''
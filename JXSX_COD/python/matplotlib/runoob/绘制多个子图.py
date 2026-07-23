import matplotlib.pyplot as plt
import numpy as np
'''
# subplot方法在绘图时需要指定位置
# 子图表编号按从或到右，从上到下顺序排列
#plot 1:
xpoints = np.array([0,4, 6])
ypoints = np.array([0,20, 100])
plt.subplot(1, 2, 1)  # 生成1*2个子图表，当前为(1,1)子图表，第三个参数表示索引
plt.plot(xpoints,ypoints)
plt.title("plot 1")   # 子图表1标题
#plot 2:
x = np.array([1, 2, 3, 4])
y = np.array([1, 4, 9, 16])
plt.subplot(1, 2, 2)   # 生成1*2个子图表，当前为(1,2)子图表，第三个参数表示索引
plt.plot(x,y)
plt.title("plot 2")# 子图表2标题
plt.suptitle("RUNOOB subplot Test")   # 总标题
plt.show()
'''

'''
# subplots方法可以一次生成多个，在调用时只需要调用生成对象的 ax 即可
# 创建一些测试数据 -- 图1
x = np.linspace(0, 2*np.pi, 400)   # 从0到2*np.pi均匀取400个值
y = np.sin(x**2)
# 创建一个画像和子图 -- 图2
fig, ax = plt.subplots()   # fig表示返回的Figure对象，代表整个图形，ax表示返回的Axes对象或Axes对象的数组，代表子图
ax.plot(x, y)
ax.set_title('Simple plot')
# 创建两个子图 -- 图3
f, (ax1, ax2) = plt.subplots(1, 2, sharey=True)  # 直接在返回值中解包，作用与ax[0]、ax[1]一样
ax1.plot(x, y)
ax1.set_title('Sharing Y axis')
ax2.scatter(x, y)   # 散点图
# 创建四个子图 -- 图4
fig, axs = plt.subplots(2, 2, subplot_kw=dict(projection="polar"))  # 极坐标
axs[0, 0].plot(x, y)
axs[1, 1].scatter(x, y)
# 共享 x 轴
# plt.subplots(2, 2, sharex='col')
# 共享 y 轴
# plt.subplots(2, 2, sharey='row')
# 共享 x 轴和 y 轴
# plt.subplots(2, 2, sharex='all', sharey='all')
# 这个也是共享 x 轴和 y 轴
# plt.subplots(2, 2, sharex=True, sharey=True) 
# 创建标识为 10 的图，已经存在的则删除
# fig, ax = plt.subplots(num=10, clear=True)
plt.tight_layout()  #自动调整子图之间的间距
plt.show()
'''


'''
subplots() 方法语法格式如下：
matplotlib.pyplot.subplots(nrows=1, ncols=1, *, sharex=False, sharey=False, squeeze=True, subplot_kw=None, gridspec_kw=None, **fig_kw)
说明：
参数说明：
nrows：默认为 1，设置图表的行数。
ncols：默认为 1，设置图表的列数。
sharex、sharey：设置 x、y 轴是否共享属性，默认为 false，可设置为 'none'、'all'、'row' 或 'col'。 False 或 none 每个子图的 x 轴或 y 轴都是独立的，True 或 'all'：所有子图共享 x 轴或 y 轴，'row' 设置每个子图行共享一个 x 轴或 y 轴，'col'：设置每个子图列共享一个 x 轴或 y 轴。
squeeze：布尔值，默认为 True，表示额外的维度从返回的 Axes(轴)对象中挤出，对于 N*1 或 1*N 个子图，返回一个 1 维数组，对于 N*M，N>1 和 M>1 返回一个 2 维数组。如果设置为 False，则不进行挤压操作，返回一个元素为 Axes 实例的2维数组，即使它最终是1x1。
subplot_kw：可选，字典类型。把字典的关键字传递给 add_subplot() 来创建每个子图。
gridspec_kw：可选，字典类型。把字典的关键字传递给 GridSpec 构造函数创建子图放在网格里(grid)。
**fig_kw：把详细的关键字参数传给 figure() 函数。
'''
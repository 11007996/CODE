'''
imshow() 函数常用于绘制二维的灰度图像或彩色图像。
imshow() 函数可用于绘制矩阵、热力图、地图等。
imshow() 方法语法格式如下：
imshow(X, cmap=None, norm=None, aspect=None, interpolation=None, alpha=None, vmin=None, vmax=None, origin=None, extent=None, shape=None, filternorm=1, filterrad=4.0, imlim=None, resample=None, url=None, *, data=None, **kwargs)
参数说明：
X：输入数据。可以是二维数组、三维数组、PIL图像对象、matplotlib路径对象等。
cmap：颜色映射。用于控制图像中不同数值所对应的颜色。可以选择内置的颜色映射，如gray、hot、jet等，也可以自定义颜色映射。
norm：用于控制数值的归一化方式。可以选择Normalize、LogNorm等归一化方法。
aspect：控制图像纵横比（aspect ratio）。可以设置为auto或一个数字。
interpolation：插值方法。用于控制图像的平滑程度和细节程度。可以选择nearest、bilinear、bicubic等插值方法。
alpha：图像透明度。取值范围为0~1。
origin：坐标轴原点的位置。可以设置为upper或lower。
extent：控制显示的数据范围。可以设置为[xmin, xmax, ymin, ymax]。
vmin、vmax：控制颜色映射的值域范围。
filternorm 和 filterrad：用于图像滤波的对象。可以设置为None、antigrain、freetype等。
imlim： 用于指定图像显示范围。
resample：用于指定图像重采样方式。
url：用于指定图像链接。
'''


import matplotlib.pyplot as plt
import numpy as np
from PIL import Image
'''
# 生成一个二维随机数组
img = np.random.rand(10, 10)
# plt.imshow(img, cmap='gray')  # 绘制灰度图像
plt.imshow(img, cmap='hot')   # 热力图像
# 显示图像
plt.show()
'''
'''
# 生成一个三维随机数组
img = np.random.rand(10, 10, 3)
# 绘制彩色图像
plt.imshow(img)
# 显示图像
plt.show()
'''

'''
# 加载本地图片
img = Image.open('D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\PixPin_2025-10-22_11-35-19.png')
# 转换为数组
data = np.array(img)
# 绘制地图
plt.imshow(data)
# 隐藏坐标轴
plt.axis('off')
# 显示图像
plt.show()
'''


'''
n = 5
# 创建一个 n x n 的二维numpy数组
a = np.reshape(np.linspace(0,1,n**2), (n,n))
plt.figure(figsize=(12,4.5))
# 第一张图展示灰度的色彩映射方式，并且没有进行颜色的混合
plt.subplot(1,3,1)
plt.imshow(a, cmap='gray', interpolation='nearest')
plt.xticks(range(n))
plt.yticks(range(n))
# 灰度映射，无混合
plt.title('Gray color map, no blending', y=1.02, fontsize=12)
# 第二张图展示使用viridis颜色映射的图像，同样没有进行颜色的混合
plt.subplot(1,3,2)
plt.imshow(a, cmap='viridis', interpolation='nearest')
plt.yticks([])
plt.xticks(range(n))
# Viridis映射，无混合
plt.title('Viridis color map, no blending', y=1.02, fontsize=12)
# 第三张图展示使用viridis颜色映射的图像，并且使用了双立方插值方法进行颜色混合
plt.subplot(1,3,3)
plt.imshow(a, cmap='viridis', interpolation='bicubic')
plt.yticks([])
plt.xticks(range(n))
# Viridis 映射，双立方混合
plt.title('Viridis color map, bicubic blending', y=1.02, fontsize=12)
plt.show()
'''
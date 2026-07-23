'''
imsave() 方法是 Matplotlib 库中用于将图像数据保存到磁盘上的函数。
imsave() 方法的语法如下:
matplotlib.pyplot.imsave(fname, arr, **kwargs)
参数说明：
fname：保存图像的文件名，可以是相对路径或绝对路径。
arr：表示图像的NumPy数组。
kwargs：可选参数，用于指定保存的图像格式以及图像质量等参数。
'''

import matplotlib.pyplot as plt
import numpy as np

'''
# 创建一个二维的图像数据
img_data = np.random.random((100, 100))
# 显示图像
plt.imshow(img_data)
# 保存图像到磁盘上
plt.imsave('runoob-test.png', img_data)
'''
'''
# 创建一幅灰度图像
img_gray = np.random.random((100, 100))
# 创建一幅彩色图像
img_color = np.zeros((100, 100, 3))
img_color[:, :, 0] = np.random.random((100, 100))
img_color[:, :, 1] = np.random.random((100, 100))
img_color[:, :, 2] = np.random.random((100, 100))
# 显示灰度图像
plt.imshow(img_gray, cmap='gray')
# 保存灰度图像到磁盘上
plt.imsave('test_gray.png', img_gray, cmap='gray')
# 显示彩色图像
plt.imshow(img_color)
# 保存彩色图像到磁盘上
plt.imsave('test_color.jpg', img_color)
'''
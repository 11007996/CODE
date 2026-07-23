'''
imread() 方法是 Matplotlib 库中的一个函数，用于从图像文件中读取图像数据。
imread() 方法返回一个 numpy.ndarray 对象，其形状是 (nrows, ncols, nchannels)，表示读取的图像的行数、列数和通道数：
如果图像是灰度图像，则 nchannels 为 1。
如果是彩色图像，则 nchannels 为 3 或 4，分别表示红、绿、蓝三个颜色通道和一个 alpha 通道。
imread() 方法的语法如下：
matplotlib.pyplot.imread(fname, format=None)
参数说明：
fname：指定了要读取的图像文件的文件名或文件路径，可以是相对路径或绝对路径。
format ：参数指定了图像文件的格式，如果不指定，则默认根据文件后缀名来自动识别格式。
'''


import matplotlib.pyplot as plt

'''
img = plt.imread('D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\PixPin_2025-10-22_11-35-19.png')
# 显示图像
plt.imshow(img)
plt.show()
'''

'''  # 调整亮度
img_array = plt.imread('D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\PixPin_2025-10-22_11-35-19.png')
tiger = img_array/255
#print(tiger)
# 显示图像
plt.figure(figsize=(10,6))
for i in range(1,5):
    plt.subplot(2,2,i)
    x = 1 - 0.2*(i-1)
    plt.axis('off')
    plt.title('x={:.1f}'.format(x))
    plt.imshow(tiger*x)
plt.show()
'''

''' # 裁剪图像
img_array = plt.imread('D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\PixPin_2025-10-22_11-35-19.png')
tiger = img_array/255
# 显示图像
plt.figure(figsize=(6,6))
plt.imshow(tiger[:300,100:400,:])
plt.axis('off')
plt.show()
'''


''' # 调整色相
img_array = plt.imread('D:\work\Chroma-MES数据备份\FA100图表导入\FA100资料图片\PixPin_2025-10-22_11-35-19.png')
tiger = img_array/255
#print(tiger)
# 显示图像
red_tiger = tiger.copy()
red_tiger[:, :,[1,2]] = 0
plt.figure(figsize=(10,10))
plt.imshow(red_tiger)
plt.axis('off')
plt.show()
'''
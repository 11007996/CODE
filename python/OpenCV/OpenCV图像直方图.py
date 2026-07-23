'''
OpenCV 提供了丰富的直方图计算和操作函数：
功能	函数	说明
计算直方图	cv2.calcHist()	计算图像的直方图。
直方图均衡化	cv2.equalizeHist()	增强图像的对比度。
直方图比较	cv2.compareHist()	比较两个直方图的相似度。
绘制直方图	matplotlib.pyplot.plot()	使用 Matplotlib 绘制直方图。
'''







import cv2
import numpy as np
import matplotlib.pyplot as plt




# 读取图像
img = cv2.imread('opencvBase.jpg')





'''
可以使用 cv2.calcHist() 函数来计算图像的直方图
cv2.calcHist(images, channels, mask, histSize, ranges[, hist[, accumulate]])
参数说明
images: 输入的图像列表，通常是一个包含单通道或多通道图像的列表。例如 [img]。
channels: 需要计算直方图的通道索引。对于灰度图像，使用 [0]；对于彩色图像，可以使用 [0]、[1]、[2] 分别计算蓝色、绿色和红色通道的直方图。
mask: 掩码图像。如果指定了掩码，则只计算掩码区域内的像素。如果不需要掩码，可以传入 None。
histSize: 直方图的 bin 数量。对于灰度图像，通常设置为 [256]，表示将灰度级分为 256 个 bin。
ranges: 像素值的范围。对于灰度图像，通常设置为 [0, 256]，表示像素值的范围是 0 到 255。
hist: 输出的直方图数组。
accumulate: 是否累积直方图。如果设置为 True，则直方图不会被清零，而是在每次调用时累积。
'''


# 计算直方图
hist = cv2.calcHist([img], [0], None, [256], [0, 256])
# 绘制直方图
plt.plot(hist)
plt.title('Grayscale Histogram')
plt.xlabel('Pixel Value')
plt.ylabel('Frequency')
plt.show()



'''
# 直方图均衡化是一种增强图像对比度的方法，通过重新分配像素强度值，使直方图更加均匀
# 直方图均衡化
equalized_image = cv2.equalizeHist(img)
# 显示结果
cv2.imshow("Equalized Image", equalized_image)
# cv2.imwrite("zft.jpg", equalized_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''







'''
# 计算 BGR 各通道的直方图
colors = ('b', 'g', 'r')
for i, color in enumerate(colors):
    hist = cv2.calcHist([img], [i], None, [256], [0, 256])
    plt.plot(hist, color=color)
# 绘制直方图
plt.title("Color Histogram")
plt.xlabel("Pixel Intensity")
plt.ylabel("Pixel Count")
plt.show()
'''






'''
# 分离通道
b, g, r = cv2.split(img)
# 对每个通道进行直方图均衡化
b_eq = cv2.equalizeHist(b)
g_eq = cv2.equalizeHist(g)
r_eq = cv2.equalizeHist(r)
# 合并通道
equalized_image = cv2.merge([b_eq, g_eq, r_eq])
# 显示结果
cv2.imshow("Equalized Color Image", equalized_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''



'''
用于比较两个直方图的相似度
similarity = cv2.compareHist(hist1, hist2, method)
hist1: 第一个直方图。
hist2: 第二个直方图。
method: 比较方法，例如 cv2.HISTCMP_CORREL（相关性比较）。
'''
'''
image1 = cv2.imread('opencvBase.jpg')
image2 = cv2.imread('opencvBaseGray.jpg')
# 计算两个图像的直方图
hist1 = cv2.calcHist([image1], [0], None, [256], [0, 256])
hist2 = cv2.calcHist([image2], [0], None, [256], [0, 256])
# 比较直方图
similarity = cv2.compareHist(hist1, hist2, cv2.HISTCMP_CORREL)
print("Histogram Similarity:", similarity)
'''



'''
# 读取灰度图像
image = cv2.imread("opencvBase.jpg", cv2.IMREAD_GRAYSCALE)
# 计算灰度直方图
hist = cv2.calcHist([image], [0], None, [256], [0, 256])
# 绘制直方图
plt.plot(hist)
plt.title("Grayscale Histogram")
plt.xlabel("Pixel Intensity")
plt.ylabel("Pixel Count")
plt.show()
# 直方图均衡化
equalized_image = cv2.equalizeHist(image)
# 显示结果
cv2.imshow("Equalized Image", equalized_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''

'''
函数	算法	说明	适用场景
cv2.Canny()	Canny 边缘检测	多阶段算法，检测效果较好，噪声抑制能力强。	通用边缘检测，适合大多数场景。
cv2.Sobel()	Sobel 算子	基于一阶导数的边缘检测，可以检测水平和垂直边缘。	检测水平和垂直边缘。    # Sobel算子使用两个 3x3 的卷积核分别计算图像在水平和垂直方向上的梯度
cv2.Scharr()	Scharr 算子	Sobel 算子的改进版本，对边缘的响应更强。	检测细微的边缘。
cv2.Laplacian()	Laplacian 算子	基于二阶导数的边缘检测，对噪声敏感。	检测边缘和角点。
'''


'''
以下是 OpenCV 中常用边缘检测函数的对比：
函数	算法	优点	缺点	适用场景
cv2.Canny()	Canny 边缘检测	噪声抑制能力强，边缘检测效果好。	参数调节较为复杂。	通用边缘检测，适合大多数场景。
cv2.Sobel()	Sobel 算子	计算简单，适合检测水平和垂直边缘。	对噪声敏感，边缘检测效果一般。	检测水平和垂直边缘。
cv2.Scharr()	Scharr 算子	对边缘的响应更强，适合检测细微边缘。	对噪声敏感。	检测细微的边缘。
cv2.Laplacian()	Laplacian 算子	可以检测边缘和角点。	对噪声非常敏感。	检测边缘和角点。
'''












'''
edges = cv2.Canny(image, threshold1, threshold2, apertureSize=3, L2gradient=False)
image：输入图像，必须是单通道的灰度图像。
threshold1：低阈值。    # 使用两个阈值（低阈值和高阈值）来确定真正的边缘。高于高阈值的像素点被认为是强边缘，低于低阈值的像素点被抑制，介于两者之间的像素点如果与强边缘相连则保留。
threshold2：高阈值。
apertureSize：Sobel 算子的孔径大小，默认为 3。
L2gradient：是否使用 L2 范数计算梯度幅值，默认为 False（使用 L1 范数）。
'''



import cv2
import numpy as np

# 读取图像
image = cv2.imread('opencvBase.jpg', cv2.IMREAD_GRAYSCALE)





# Canny 边缘检测算法主要包括以下几个步骤：
# 噪声抑制：使用高斯滤波器对图像进行平滑处理，以减少噪声的影响。
# 计算梯度：使用 Sobel 算子计算图像的梯度幅值和方向。
# 非极大值抑制：沿着梯度方向，保留局部梯度最大的像素点，抑制其他像素点。
# 双阈值检测：使用两个阈值（低阈值和高阈值）来确定真正的边缘。高于高阈值的像素点被认为是强边缘，低于低阈值的像素点被抑制，介于两者之间的像素点如果与强边缘相连则保留。
# 边缘连接：通过滞后阈值处理，将弱边缘与强边缘连接起来，形成完整的边缘。
# 应用 Canny 边缘检测
# edges = cv2.Canny(image, 100, 200)   # 阈值越小检测到的边缘越多
# cv2.imshow('Canny Edges', edges)






# Sobel 算子使用两个 3x3 的卷积核分别计算图像在水平和垂直方向上的梯度：
# 水平方向的卷积核：
# [-1, 0, 1]
# [-2, 0, 2]
# [-1, 0, 1]
# 垂直方向的卷积核：
# [-1, -2, -1]
# [ 0,  0,  0]
# [ 1,  2,  1]
# 通过这两个卷积核，可以分别得到图像在水平和垂直方向上的梯度 Gx 和 Gy。最终的梯度幅值可以通过以下公式计算：
# G = sqrt(Gx^2 + Gy^2)
'''
dst = cv2.Sobel(src, ddepth, dx, dy, ksize=3, scale=1, delta=0, borderType=cv2.BORDER_DEFAULT)
src：输入图像。
ddepth：输出图像的深度，通常使用 cv2.CV_64F。
dx：x 方向上的导数阶数。
dy：y 方向上的导数阶数。
ksize：Sobel 核的大小，默认为 3。
scale：缩放因子，默认为 1。
delta：可选的 delta 值，默认为 0。
borderType：边界填充类型，默认为 cv2.BORDER_DEFAULT。
'''
'''
# 计算 x 方向的梯度
sobel_x = cv2.Sobel(image, cv2.CV_64F, 1, 0, ksize=13,scale=1)
# 计算 y 方向的梯度
sobel_y = cv2.Sobel(image, cv2.CV_64F, 0, 1, ksize=13,scale=1)
# 计算梯度幅值
sobel_combined = np.sqrt(sobel_x**2 + sobel_y**2)
# 显示结果
cv2.imshow('Sobel X', sobel_x)
# cv2.imshow('Sobel Y', sobel_y)
# cv2.imshow('Sobel Combined', sobel_combined)
'''







'''
Laplacian 算子使用以下卷积核来计算图像的二阶导数：
[ 0,  1,  0]
[ 1, -4,  1]
[ 0,  1,  0]
通过这个卷积核，可以得到图像的 Laplacian 值。Laplacian 值较大的区域通常对应于图像的边缘。
dst = cv2.Laplacian(src, ddepth, ksize=1, scale=1, delta=0, borderType=cv2.BORDER_DEFAULT)
src：输入图像。
ddepth：输出图像的深度，通常使用 cv2.CV_64F。
ksize：Laplacian 核的大小，默认为 1。
scale：缩放因子，默认为 1。
delta：可选的 delta 值，默认为 0。
borderType：边界填充类型，默认为 cv2.BORDER_DEFAULT。
'''
'''
# 应用 Laplacian 算子
laplacian = cv2.Laplacian(image, cv2.CV_64F)
# 显示结果
cv2.imshow('Laplacian', laplacian)
'''











# 显示结果

cv2.waitKey(0)
cv2.destroyAllWindows()
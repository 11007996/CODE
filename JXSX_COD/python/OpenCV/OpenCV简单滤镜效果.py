'''
OpenCV提供了丰富的图像处理和计算机视觉算法，广泛应用于图像处理、视频分析、物体检测等领域
以下是主要滤镜效果：
滤镜效果	实现方法
灰度滤镜	cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
怀旧滤镜	通过调整色彩通道的权重，模拟老照片效果。
浮雕滤镜	使用卷积核 [[-2, -1, 0], [-1, 1, 1], [0, 1, 2]] 进行卷积操作。
模糊滤镜	cv2.GaussianBlur(image, (15, 15), 0)
锐化滤镜	使用卷积核 [[0, -1, 0], [-1, 5, -1], [0, -1, 0]] 进行卷积操作。
边缘检测滤镜	cv2.Canny(gray_image, 100, 200)
'''



import cv2
import numpy as np



'''
# 灰度滤镜
# 读取图像
image = cv2.imread('opencvBase.jpg')
# 转换为灰度图像
gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# 保存灰度图像
cv2.imwrite('gray_output.jpg', gray_image)
# 显示灰度图像
cv2.imshow('Gray Image', gray_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''




'''
# 怀旧滤镜会增加红色和绿色通道的强度，同时减少蓝色通道的强度
# 读取图像
image = cv2.imread('R-C.jpg')
# 分离 BGR 通道
b, g, r = cv2.split(image)
# 调整通道强度
r = np.clip(r * 0.393 + g * 0.769 + b * 0.189, 0, 255).astype(np.uint8)
g = np.clip(r * 0.349 + g * 0.686 + b * 0.168, 0, 255).astype(np.uint8)
b = np.clip(r * 0.272 + g * 0.534 + b * 0.131, 0, 255).astype(np.uint8)
# 合并通道
vintage_image = cv2.merge((b, g, r))
# 保存怀旧图像
cv2.imwrite('vintage_output.jpg', vintage_image)
# 显示怀旧图像
cv2.imshow('Vintage Image', vintage_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''




'''
浮雕滤镜通过计算图像中相邻像素的差值，生成一种类似于浮雕的效果，这种滤镜通常用于增强图像的边缘和纹理。
实现步骤
读取图像。
将图像转换为灰度图像。
使用卷积核计算浮雕效果。
'''


'''
# 读取图像
image = cv2.imread('add2.jpg')
# 转换为灰度图像
gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# 定义卷积核
kernel = np.array([[-2, -1, 0],
                   [-1,  1, 1],
                   [ 0,  1, 2]])
# 应用卷积核
emboss_image = cv2.filter2D(gray_image, -1, kernel)
# 保存浮雕图像
# cv2.imwrite('emboss_output.jpg', emboss_image)
# 显示浮雕图像
cv2.imshow('Emboss Image', emboss_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''



'''
# 模糊滤镜通过平滑图像，减少图像中的噪声和细节
# 读取图像
image = cv2.imread("add1.png")
# 模糊滤镜
blurred_image = cv2.GaussianBlur(image, (15, 15), 0)
# 显示结果
cv2.imshow("Blur Filter", blurred_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''




'''
# 锐化滤镜通过增强图像的边缘，使图像更加清晰
# 读取图像
image = cv2.imread("add1.png")
# 锐化滤镜
sharpen_kernel = np.array([[0, -1, 0],
                           [-1, 5, -1],
                           [0, -1, 0]])
sharpened_image = cv2.filter2D(image, -1, sharpen_kernel)
# 显示结果
cv2.imshow("Sharpen Filter", sharpened_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''




'''
# 边缘检测滤镜通过检测图像中的边缘，突出显示物体的轮廓
# 读取图像
image = cv2.imread("opencvBase.jpg")
# 转换为灰度图
gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# 边缘检测滤镜
edges_image = cv2.Canny(gray_image, 100, 200)
# 显示结果
cv2.imshow("Edges Filter", edges_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''

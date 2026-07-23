# 阈值处理用于将图像转换为二值图像（即黑白图像），  通过设定一个阈值，可以将图像中的像素分为两类：高于阈值的像素和低于阈值的像素
import cv2
import numpy as np

'''
# 简单阈值处理
retval, dst = cv2.threshold(src, thresh, maxval, type)
参数说明
src: 输入图像，通常为灰度图像。
thresh: 设定的阈值。
maxval: 当像素值超过（或小于，根据类型）阈值时，赋予的新值。
type: 阈值处理的类型，常见的类型有：
cv2.THRESH_BINARY: 如果像素值大于阈值，则赋予 maxval，否则赋予 0。
cv2.THRESH_BINARY_INV: 与 cv2.THRESH_BINARY 相反，如果像素值大于阈值，则赋予 0，否则赋予 maxval。
cv2.THRESH_TRUNC: 如果像素值大于阈值，则赋予阈值，否则保持不变。
cv2.THRESH_TOZERO: 如果像素值大于阈值，则保持不变，否则赋予 0。
cv2.THRESH_TOZERO_INV: 与 cv2.THRESH_TOZERO 相反，如果像素值大于阈值，则赋予 0，否则保持不变。
返回值
retval: 实际使用的阈值（在某些情况下可能与设定的阈值不同）。
dst: 处理后的图像。
'''



# 读取图像
img = cv2.imread('opencvBaseGray.jpg', 0)




# 简单阈值处理
# ret, thresh1 = cv2.threshold(img, 127, 255, cv2.THRESH_BINARY)  # 大于中度灰就为白，否则为黑   类似PS的阈值功能
# ret, thresh1 = cv2.threshold(img, 127, 255, cv2.THRESH_BINARY_INV)  # 大于中度灰就为黑，否则为白    类似PS的反相再阈值功能
# ret, thresh1 = cv2.threshold(img, 127, 255, cv2.THRESH_TRUNC)  # 大于中度灰就为中度灰，否则不变    类似PS的变暗模式，设定一个最暗值
# ret, thresh1 = cv2.threshold(img, 127, 255, cv2.THRESH_TOZERO)   # 大于中度灰不变，其它都变黑，   也就是把暗部统归为0
# ret, thresh1 = cv2.threshold(img, 127, 255, cv2.THRESH_TOZERO_INV)   # 大于中度灰变黑，其它都不变，   也就是把亮部统归为0





'''
# 自适应阈值处理
dst = cv2.adaptiveThreshold(src, maxValue, adaptiveMethod, thresholdType, blockSize, C)
参数说明
src: 输入图像，通常为灰度图像。
maxValue: 当像素值超过（或小于，根据类型）阈值时，赋予的新值。
adaptiveMethod: 自适应阈值计算方法，常见的类型有：
cv2.ADAPTIVE_THRESH_MEAN_C: 阈值是邻域的平均值减去常数 C。
cv2.ADAPTIVE_THRESH_GAUSSIAN_C: 阈值是邻域的加权平均值减去常数 C，权重由高斯函数确定。
thresholdType: 阈值处理的类型，通常为 cv2.THRESH_BINARY 或 cv2.THRESH_BINARY_INV。   # 两个类型相反，一个变亮一个变暗
blockSize: 计算阈值时使用的邻域大小，必须为奇数。
C: 从平均值或加权平均值中减去的常数。
'''

# 自适应阈值处理
# thresh1 = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY, 11, 2)
# thresh1 = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 11, 2)
# thresh1 = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV, 11, 2)





'''
# Otsu's 二值化是一种自动确定阈值的方法。它通过最大化类间方差来找到最佳的全局阈值，适用于双峰图像（即图像直方图有两个明显的峰值）
retval, dst = cv2.threshold(src, thresh, maxval, type)
参数说明
src: 输入图像，通常为灰度图像。
thresh: 由于 Otsu's 方法会自动确定阈值，因此该参数通常设置为 0。
maxval: 当像素值超过（或小于，根据类型）阈值时，赋予的新值。
type: 阈值处理的类型，通常为 cv2.THRESH_BINARY 或 cv2.THRESH_BINARY_INV，并加上 cv2.THRESH_OTSU。
返回值
retval: 自动确定的阈值。
dst: 处理后的图像。
'''
# Otsu's 二值化
# ret, thresh1 = cv2.threshold(img, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
# cv2.imwrite("output_path.jpg", thresh1)






# 显示结果
cv2.imshow('Binary Threshold', thresh1)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''
图像形态学操作是图像处理中的一种重要技术，主要用于处理二值图像（即黑白图像）。
OpenCV 中的图像形态学操作是图像处理中的重要工具，通过腐蚀、膨胀、开运算、闭运算和形态学梯度等操作，可以实现对图像的噪声去除、对象分离、边缘检测等效果。掌握这些操作有助于更好地处理和分析图像数据
操作	函数	说明	应用场景
腐蚀	cv2.erode()	用结构元素扫描图像，如果结构元素覆盖的区域全是前景，则保留中心像素。	去除噪声、分离物体。
膨胀	cv2.dilate()	用结构元素扫描图像，如果结构元素覆盖的区域存在前景，则保留中心像素。	连接断裂的物体、填充空洞。
开运算	cv2.morphologyEx()	先腐蚀后膨胀。	去除小物体、平滑物体边界。
闭运算	cv2.morphologyEx()	先膨胀后腐蚀。	填充小孔洞、连接邻近物体。
形态学梯度	cv2.morphologyEx()	膨胀图减去腐蚀图。	提取物体边缘。
顶帽运算	cv2.morphologyEx()	原图减去开运算结果。	提取比背景亮的细小物体。
黑帽运算	cv2.morphologyEx()	闭运算结果减去原图。	提取比背景暗的细小物体。
'''



import cv2
import numpy as np


# 读取图像
image = cv2.imread('opencvBaseGray.jpg', 0)



'''
腐蚀
腐蚀操作是一种缩小图像中前景对象的过程。
腐蚀操作通过将结构元素与图像进行卷积，只有当结构元素完全覆盖图像中的前景像素时，中心像素才会被保留，否则会被腐蚀掉。
cv2.erode(src, kernel, iterations=1)
src: 输入图像，通常是二值图像。
kernel: 结构元素，可以自定义或使用 cv2.getStructuringElement() 生成。
iterations: 腐蚀操作的次数，默认为1。
'''


# # 定义结构元素
# kernel = np.ones((5,5), np.uint8)
# # 腐蚀操作
# result = cv2.erode(image, kernel, iterations=1)    # 腐蚀操作会使图像中的前景对象变小，边缘被腐蚀掉，常用于去除噪声或分离连接的对象。








'''
膨胀
膨胀操作与腐蚀相反，它是一种扩大图像中前景对象的过程。
膨胀操作通过将结构元素与图像进行卷积，只要结构元素与图像中的前景像素有重叠，中心像素就会被保留。
cv2.dilate(src, kernel, iterations=1)
src: 输入图像，通常是二值图像。
kernel: 结构元素，可以自定义或使用 cv2.getStructuringElement() 生成。
iterations: 膨胀操作的次数，默认为1。
'''
'''
# 定义结构元素
kernel = np.ones((5,5), np.uint8)
# 膨胀操作
result = cv2.dilate(image, kernel, iterations=1)  # 膨胀操作会使图像中的前景对象变大，边缘被扩展，常用于填补前景对象中的空洞或连接断裂的对象
'''







'''
开运算
开运算是先腐蚀后膨胀的组合操作。
开运算主要用于去除图像中的小噪声或分离连接的对象。
cv2.morphologyEx(src, op, kernel)
src: 输入图像，通常是二值图像。
op: 形态学操作类型，开运算使用 cv2.MORPH_OPEN。
kernel: 结构元素，可以自定义或使用 cv2.getStructuringElement() 生成。
开运算可以去除图像中的小噪声，同时保留图像中的主要前景对象。
'''

# 定义结构元素
kernel = np.ones((5,5), np.uint8)
# 开运算
result = cv2.morphologyEx(image, cv2.MORPH_OPEN, kernel)





'''
闭运算
闭运算是先膨胀后腐蚀的组合操作。
闭运算主要用于填补前景对象中的小孔或连接断裂的对象。
cv2.morphologyEx(src, op, kernel)
src: 输入图像，通常是二值图像。
op: 形态学操作类型，闭运算使用 cv2.MORPH_CLOSE。
kernel: 结构元素，可以自定义或使用 cv2.getStructuringElement() 生成。
'''
'''
# 定义结构元素
kernel = np.ones((5,5), np.uint8)
# 闭运算
result = cv2.morphologyEx(image, cv2.MORPH_CLOSE, kernel)    # 闭运算可以填补前景对象中的小孔，同时保留图像中的主要前景对象
'''





'''
形态学梯度
形态学梯度是膨胀图像与腐蚀图像的差值。
形态学梯度主要用于提取图像中前景对象的边缘。
cv2.morphologyEx(src, op, kernel)
src: 输入图像，通常是二值图像。
op: 形态学操作类型，形态学梯度使用 cv2.MORPH_GRADIENT。
kernel: 结构元素，可以自定义或使用 cv2.getStructuringElement() 生成。
'''
'''
# 定义结构元素
kernel = np.ones((5,5), np.uint8)
# 形态学梯度
result = cv2.morphologyEx(image, cv2.MORPH_GRADIENT, kernel)    # 形态学梯度可以提取图像中前景对象的边缘，常用于边缘检测
'''









# 显示结果
cv2.imshow('Eroded Image', result)
cv2.waitKey(0)
cv2.destroyAllWindows()
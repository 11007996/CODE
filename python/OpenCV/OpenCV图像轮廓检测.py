'''
函数名称	功能描述
cv2.findContours()	查找图像中的轮廓
cv2.drawContours()	在图像上绘制轮廓
cv2.contourArea()	计算轮廓的面积
cv2.arcLength()	计算轮廓的周长或弧长
cv2.boundingRect()	计算轮廓的边界矩形
cv2.minAreaRect()	计算轮廓的最小外接矩形
cv2.minEnclosingCircle()	计算轮廓的最小外接圆
cv2.approxPolyDP()	对轮廓进行多边形近似
'''



'''
contours, hierarchy = cv2.findContours(image, mode, method[, contours[, hierarchy[, offset]]])
该函数用于在二值图像中查找轮廓。轮廓是图像中具有相同颜色或强度的连续点的曲线。
参数说明:
image: 输入的二值图像（通常为经过阈值处理或边缘检测后的图像）。
mode: 轮廓检索模式，常用的有：
cv2.RETR_EXTERNAL: 只检测最外层轮廓。
cv2.RETR_LIST: 检测所有轮廓，但不建立层次关系。
cv2.RETR_TREE: 检测所有轮廓，并建立完整的层次结构。
method: 轮廓近似方法，常用的有：
cv2.CHAIN_APPROX_NONE: 存储所有的轮廓点。
cv2.CHAIN_APPROX_SIMPLE: 压缩水平、垂直和对角线段，只保留端点。
contours: 输出的轮廓列表，每个轮廓是一个点集。
hierarchy: 输出的层次结构信息。
offset: 可选参数，轮廓点的偏移量。
返回值:
contours: 检测到的轮廓列表。
hierarchy: 轮廓的层次结构信息。
'''






'''
cv2.drawContours(image, contours, contourIdx, color[, thickness[, lineType[, hierarchy[, maxLevel[, offset]]]]])
参数说明:
image: 要绘制轮廓的图像。
contours: 轮廓列表。
contourIdx: 要绘制的轮廓索引，如果为负数，则绘制所有轮廓。
color: 轮廓的颜色。
thickness: 轮廓线的厚度，如果为负数，则填充轮廓内部。
lineType: 线型。
hierarchy: 轮廓的层次结构信息。
maxLevel: 绘制的最大层次深度。
offset: 轮廓点的偏移量。
返回值:
无返回值，直接在输入图像上绘制轮廓。
'''


'''
area = cv2.contourArea(contour[, oriented])
参数说明:
contour: 输入的轮廓点集。
oriented: 可选参数，如果为True，返回有符号的面积。
返回值:
轮廓的面积
'''



'''
该函数用于计算轮廓的周长或弧长。
length = cv2.arcLength(curve, closed)
参数说明:
curve: 输入的轮廓点集。
closed: 布尔值，表示轮廓是否闭合。
返回值:
轮廓的周长或弧长。
'''




'''
该函数用于计算轮廓的边界矩形。
x, y, w, h = cv2.boundingRect(points)
参数说明:
points: 输入的轮廓点集。
返回值:
边界矩形的左上角坐标 (x, y) 和宽度 w、高度 h。
'''



'''
该函数用于计算轮廓的最小外接矩形（旋转矩形）。
rect = cv2.minAreaRect(points)
参数说明:
points: 输入的轮廓点集。
返回值:
返回一个旋转矩形，包含中心点 (x, y)、宽度、高度和旋转角度。
'''






import cv2
import numpy as np

# 读取图像并转换为灰度图
# image = cv2.imread('opencvBaseGray.jpg', 0)
# 二值化处理
# _, binary = cv2.threshold(image, 127, 255, cv2.THRESH_BINARY)
# 查找轮廓
# contours, hierarchy = cv2.findContours(binary, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
# 创建一个空白图像
# output = np.zeros_like(image)
# 绘制所有轮廓
# cv2.drawContours(output, contours, -1, (255, 0, 0), 1)    # 绘制轮廓
# cv2.imshow('Contours', output)

'''
for contour in contours:
    area = cv2.contourArea(contour)    # 计算轮廓的面积
    print(f"Contour area: {area}")
'''


'''
for contour in contours:
    perimeter = cv2.arcLength(contour, True)    # 轮廓的周长
    print(f"Contour perimeter: {perimeter}")
'''

'''
for contour in contours:
    x, y, w, h = cv2.boundingRect(contour)
    cv2.rectangle(image, (x, y), (x + w, y + h), (0, 255, 0), 2)    # 用于计算轮廓的边界矩形
cv2.imshow('Bounding Rectangles', image)
cv2.waitKey(0)
'''



'''
for contour in contours:
    rect = cv2.minAreaRect(contour)    # 该函数用于计算轮廓的最小外接矩形（旋转矩形）
    box = cv2.boxPoints(rect)
    box = np.int0(box)
    cv2.drawContours(image, [box], 0, (0, 0, 255), 2)
cv2.imshow('Min Area Rectangles', image)
'''


'''
该函数用于计算轮廓的最小外接圆。
(center, radius) = cv2.minEnclosingCircle(points)
参数说明:
points: 输入的轮廓点集。
返回值:
返回圆心 (x, y) 和半径 radius。
'''
'''
for contour in contours:
    (x, y), radius = cv2.minEnclosingCircle(contour)     # 计算轮廓的最小外接圆
    center = (int(x), int(y))
    radius = int(radius)
    cv2.circle(image, center, radius, (255, 0, 0), 2)
cv2.imshow('Min Enclosing Circles', image)
'''



'''
该函数用于对轮廓进行多边形近似。
approx = cv2.approxPolyDP(curve, epsilon, closed)
参数说明:
curve: 输入的轮廓点集。
epsilon: 近似精度，值越小，近似越精确。
closed: 布尔值，表示轮廓是否闭合。
返回值:
返回近似后的多边形点集。
'''
'''
for contour in contours:
    epsilon = 0.01 * cv2.arcLength(contour, True)
    approx = cv2.approxPolyDP(contour, epsilon, True)     # 对轮廓进行多边形近似
    cv2.drawContours(image, [approx], 0, (0, 255, 0), 2)
cv2.imshow('Approx Polygons', image)
'''



# 读取图像
image = cv2.imread("opencvBase.jpg")
# 转换为灰度图
gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# 二值化处理
ret, binary = cv2.threshold(gray, 127, 255, cv2.THRESH_BINARY)
# 查找轮廓
contours, hierarchy = cv2.findContours(binary, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
# 绘制轮廓
cv2.drawContours(image, contours, -1, (0, 255, 0), 2)
# 显示结果
cv2.imshow("Contours", image)





cv2.waitKey(0)






'''
以下是轮廓检测的主要步骤和函数：
步骤	函数	说明
图像预处理	cv2.cvtColor()	将图像转换为灰度图。
二值化处理	cv2.threshold()	将灰度图转换为二值图像。
查找轮廓	cv2.findContours()	查找图像中的轮廓。
绘制轮廓	cv2.drawContours()	绘制检测到的轮廓。
计算轮廓面积	cv2.contourArea()	计算轮廓的面积。
计算轮廓周长	cv2.arcLength()	计算轮廓的周长。
计算边界矩形	cv2.boundingRect()	计算轮廓的边界矩形。
计算最小外接矩形	cv2.minAreaRect()	计算轮廓的最小外接矩形。
计算最小外接圆	cv2.minEnclosingCircle()	计算轮廓的最小外接圆。
多边形逼近	cv2.approxPolyDP()	对轮廓进行多边形逼近。
'''

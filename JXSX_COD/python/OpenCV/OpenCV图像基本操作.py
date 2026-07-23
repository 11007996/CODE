'''
常用方法：
操作	函数/方法	说明
访问像素值	image[y, x]	获取或修改像素值。
图像 ROI	image[y1:y2, x1:x2]	获取或修改图像中的矩形区域。
通道分离与合并	cv2.split() / cv2.merge()	分离或合并图像通道。
图像缩放	cv2.resize()	调整图像大小。
图像旋转	cv2.getRotationMatrix2D()	旋转图像。
图像平移	cv2.warpAffine()	平移图像。
图像翻转	cv2.flip()	翻转图像。
图像加法	cv2.add()	对两幅图像进行加法运算。
图像减法	cv2.subtract()	对两幅图像进行减法运算。
图像混合	cv2.addWeighted()	对两幅图像进行加权混合。
阈值处理	cv2.threshold()	对图像进行阈值处理。
平滑处理	cv2.blur() / cv2.GaussianBlur()	对图像进行平滑处理。
'''


import cv2 as cv
import numpy as np



# 假设我们有一个灰度图像 img，可以通过 img[y, x] 来访问位于 (x, y) 位置的像素值。对于彩色图像，可以通过 img[y, x, c] 来访问特定通道 c 的像素值，其中 c 为 0（蓝色）、1（绿色）或 2（红色）
# 读取图像
img = cv.imread('opencvBase.jpg')
# # 访问像素值
# pixel_value = img[100, 150]  # 访问 (150, 100) 位置的像素值
# img[100, 150] = [255, 255, 255]  # 将 (150, 100) 位置的像素值设置为白色
# print(pixel_value)

# roi = img[0:100, 0:100]    # 获取某个区域
# roi[:, :] = roi[:, :]*0.5  # 修改这个区域的颜色
# img[0:100, 0:100] = roi



# b, g, r = cv.split(img)    # 分离通道
# merged_img = cv.merge([b, g, r])   # 合并通道
# cv.imshow("jpg",merged_img)


# cv.imshow("jpg",255 - img[:,:])  # 反相



# resized_img = cv.resize(img, (200, 200))  # 将图像缩放为 200x200 像素
# cv.imshow("jpg",resized_img)



'''
(h, w) = img.shape[:2]   # 取 NumPy 数组前 2 个维度的尺寸（行数、列数），返回一个包含前两维大小的元组
center = (w // 2, h // 2)  # 旋转中心
M = cv.getRotationMatrix2D(center, 45, 1.0)  # 旋转 45 度    旋转图像
# M = np.float32([[1, 0, 100], [0, 1, 50]])  # 向右平移 100 像素，向下平移 50 像素
# rotated_img = cv.warpAffine(img, M, (w, h))
rotated_img = cv.warpAffine(img, M, (w, h))   # 平移图像
cv.imshow("jpg",rotated_img)
'''




flipped_img = cv.flip(img, 1)  # 0上下翻转，1水平翻转，2上下水平同时翻转
cv.imshow("jpg",flipped_img)







# cv.imshow("jpg",img)
cv.waitKey(0)
cv.destroyAllWindows()
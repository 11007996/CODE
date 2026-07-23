import cv2 as cv
import time
import numpy as np

img = cv.imread("opencvBase.jpg")   # 读取图像文件，返回一个 NumPy 数组。


# cv.imwrite("output_image.jpg", img)    # 保存图片
# print(img.shape)   # 图像大小及3个通过BGR
# print(img[100:])
# img[0:100:5] = [255,0,0]    # 设置像素值
# img[20:493,0:525:5] = [255,0,0]    # 设置某个区域的像素值

# b, g, r = cv.split(img)  # 分离通道
# merged_image = cv.merge([r, r, b])    # 合并通道
# merged_image = cv2.merge([b, g, r])    # 合并通道
# cv.imshow("img",merged_image)     # 显示图像

# 缩放
# resized_image = cv2.resize(image, (new_width, new_height))

# 旋转
# (h, w) = img.shape[:2]   # 取 NumPy 数组前 2 个维度的尺寸（行数、列数），返回一个包含前两维大小的元组
# (center_x, center_y) = (w // 2, h // 2)  # 旋转中心
# rotation_matrix = cv.getRotationMatrix2D((center_x, center_y), 20, 1.0)
# rotated_image = cv.warpAffine(img, rotation_matrix, (w, h))

# 平移
# translation_matrix = np.float32([[1, 0, tx], [0, 1, ty]])  # tx, ty 为平移距离
# translated_image = cv2.warpAffine(image, translation_matrix, (width, height))

# 翻转
# flipped_image = cv2.flip(image, flip_code)  # flip_code: 0 (垂直翻转), 1 (水平翻转), -1 (双向翻转)

# result = cv2.add(image1, image2)  # 图片加法

# result = cv2.subtract(image1, image2)  # 图片减法

# result = cv2.addWeighted(image1, alpha, image2, beta, gamma)   # 图像混合

# ret, thresholded_image = cv.threshold(img, thresh, maxval, cv.THRESH_BINARY)   # 简单阈值处理

# thresholded_image = cv2.adaptiveThreshold(image, maxval, cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY, block_size, C)   # 自适应阈值处理

# ret, thresholded_image = cv2.threshold(image, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)   # Otsu's 二值化


# blurred_image = cv2.blur(image, (kernel_size, kernel_size))  # 均值滤波

# blurred_image = cv2.GaussianBlur(image, (kernel_size, kernel_size), sigmaX)  # 高斯滤波

# blurred_image = cv2.medianBlur(image, kernel_size)  # 中值滤波

# blurred_image = cv2.bilateralFilter(image, d, sigmaColor, sigmaSpace)   # 双边滤波


# gray_img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)    #  从 RGB 转换为灰度图

# hsv_img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)    # 从 BGR 转换为 HSV

# yuv_img = cv2.cvtColor(img, cv2.COLOR_BGR2YUV)    # 从 RGB 转换为 YUV


# cropped_img = img[0:200, 0:20]     # 图像裁剪


# blurred_img = cv.GaussianBlur(img, (11, 11), 0)   # 高斯模糊

# blurred_img = cv.blur(img, (5, 5))    # 均值模糊


# blurred_img = cv.medianBlur(img, 5)   # 中值模糊
# cv.imshow("img",blurred_img)


# edges = cv.Canny(img, 100, 200)    # 图像边缘检测   通过对图像进行梯度计算来找出边缘，返回一个二值图像，边缘处为白色，其他区域为黑色
# cv.imshow("jpg",edges)


# sobel_x = cv.Sobel(img, cv.CV_64F, 1, 0, ksize=5)
# sobel_y = cv.Sobel(img, cv.CV_64F, 0, 1, ksize=5)    # Sobel 算子
# print(sobel_x)
# print(sobel_y)
# laplacian = cv.Laplacian(img, cv.CV_64F)    # Laplacian 算子
# print(laplacian)


# 应用Sobel算子进行边缘检测
# sobelx = cv.Sobel(img, cv.CV_64F, 1, 0, ksize=1)
# sobely = cv.Sobel(img, cv.CV_64F, 0, 1, ksize=1)
# # 计算梯度幅值
# gradient_magnitude = np.sqrt(np.square(sobelx) + np.square(sobely))
# # 转换为二值图像
# _, binary_image = cv.threshold(gradient_magnitude, 50, 255, cv.THRESH_BINARY)
# cv.imshow("img",binary_image)



# 腐蚀（Erosion）：将图像中的白色区域收缩
# kernel = cv.getStructuringElement(cv.MORPH_RECT, (5, 5))
# eroded_img = cv.erode(img, kernel, iterations=1)
# cv.imshow("img",eroded_img)


# 膨胀（Dilation）：将图像中的白色区域扩展
# kernel = cv.getStructuringElement(cv.MORPH_RECT, (5, 5))
# dilated_img = cv.dilate(img, kernel, iterations=1)
# cv.imshow("img",dilated_img)



# kernel = cv.getStructuringElement(cv.MORPH_RECT, (5, 5))
# # 开运算（先腐蚀再膨胀）：用于去除小物体
# opening_img = cv.morphologyEx(img, cv.MORPH_OPEN, kernel)
# # 闭运算（先膨胀再腐蚀）：用于填补图像中的小孔洞
# closing_img = cv.morphologyEx(img, cv.MORPH_CLOSE, kernel)
# cv.imshow("img",opening_img)
# cv.imshow("img",closing_img)




# gray_img = cv.cvtColor(img, cv.COLOR_BGR2GRAY)   # 灰度图
# _, threshold_img = cv.threshold(gray_img, 127, 255, cv.THRESH_BINARY)
# contours, _ = cv.findContours(threshold_img, cv.RETR_EXTERNAL, cv.CHAIN_APPROX_SIMPLE)     # 检测轮廓
# cv.drawContours(img, contours, -1, (0, 255, 0), 3)    # 绘制轮廓



# cap = cv.VideoCapture('video.mp4')
# while cap.isOpened():
#     ret, frame = cap.read()     # 读取视频数据
#     if not ret:
#         break
#     # 处理每一帧
#     cv.imshow('Video', frame)
#     if cv.waitKey(1) & 0xFF == ord('q'):
#         break
# cap.release()
# cv.destroyAllWindows()












# cv.imshow("img",img)     # 显示图像
cv.waitKey(0)    #  等待用户按任意键
cv.destroyAllWindows()    # 关闭显示窗口
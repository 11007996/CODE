import cv2
import numpy as np



# 读取两幅图像
img1 = cv2.imread('add1.jpg')
img2 = cv2.imread('add2.jpg')


# 图像加法
# result = cv2.add(img1, img2)    # 图像效果类似于PS的线性减淡 如果像素值相加后超过 255，OpenCV 会自动将其截断为 255

# 图像减法   如果像素值相减后小于 0，OpenCV 会自动将其截断为 0
# result = cv2.subtract(img1, img2)

# 图像乘法  如果像素值相乘后超过 255，OpenCV 会自动将其截断为 255。
# result = cv2.multiply(img1, img2)

# 图像除法  如果除数为 0，OpenCV 会自动将结果设置为 0。
# result = cv2.divide(img1, img2)


# 图像位运算 (AND, OR, NOT, XOR)
# 函数	功能	应用场景
# cv2.bitwise_and()	按位与操作	掩码操作、图像分割
# cv2.bitwise_or()	按位或操作	图像叠加
# cv2.bitwise_not()	按位取反操作	图像反色
# cv2.bitwise_xor()	按位异或操作	图像差异检测


# 位与运算
# result = cv2.bitwise_and(img1, img2)

# 位或运算
# result = cv2.bitwise_or(img1, img2)

# 位非运算
# result = cv2.bitwise_not(img1)   # 反色    同cv2.imshow("jpg",255 - img1[:,:])

# 位异或运算
# result = cv2.bitwise_xor(img1, img2)


# 图像混合   将两幅图像按照一定的权重进行线性组合，生成一幅新的图像。
# alpha = 0.7  # 第一幅图像的权重
# beta = 0.3   # 第二幅图像的权重
# gamma = 170    # 可选的标量值   gamma值
# result = cv2.addWeighted(img1, alpha, img2, beta, gamma)    # result = img1 * alpha + img2 * beta + gamma








# 显示结果
cv2.imshow('Result', result)
cv2.waitKey(0)
cv2.destroyAllWindows()
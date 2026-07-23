'''
模板匹配是一种在图像中寻找与给定模板图像最相似区域的技术。
简单来说，模板匹配就是在一幅大图像中寻找与模板图像（即我们想要识别的物体）最匹配的部分，这种方法适用于物体在图像中的大小、方向和形状基本不变的情况
模板匹配的基本原理是通过滑动模板图像在目标图像上移动，计算每个位置的相似度，并找到相似度最高的位置。OpenCV 提供了多种相似度计算方法
模板匹配的实现步骤
加载图像: 读取搜索图像和模板图像。
模板匹配: 使用 cv2.matchTemplate() 在搜索图像中查找模板图像。
获取匹配结果: 使用 cv2.minMaxLoc() 获取最佳匹配位置。
绘制匹配结果: 在搜索图像中绘制匹配区域。
显示结果: 显示匹配结果。
'''


'''
匹配方法
OpenCV 提供了多种模板匹配方法，可以通过 cv2.matchTemplate() 的第三个参数指定：
方法	说明
cv2.TM_SQDIFF	平方差匹配，值越小匹配度越高。
cv2.TM_SQDIFF_NORMED	归一化平方差匹配，值越小匹配度越高。
cv2.TM_CCORR	相关匹配，值越大匹配度越高。
cv2.TM_CCORR_NORMED	归一化相关匹配，值越大匹配度越高。
cv2.TM_CCOEFF	相关系数匹配，值越大匹配度越高。
cv2.TM_CCOEFF_NORMED	归一化相关系数匹配，值越大匹配度越高。
'''





import cv2
import numpy as np

# 加载目标图像和模板图像
img = cv2.imread('opencvBase.jpg', 0)
template = cv2.imread('base.jpg', 0)

# 获取模板图像的尺寸
w, h = template.shape[::-1]

# 进行模板匹配
# res = cv2.matchTemplate(img, template, cv2.TM_SQDIFF)
# res = cv2.matchTemplate(img, template, cv2.TM_SQDIFF_NORMED)
# res = cv2.matchTemplate(img, template, cv2.TM_CCORR)
# res = cv2.matchTemplate(img, template, cv2.TM_CCORR_NORMED)
# res = cv2.matchTemplate(img, template, cv2.TM_CCOEFF)
res = cv2.matchTemplate(img, template, cv2.TM_CCOEFF_NORMED)

# 设置匹配阈值
threshold = 0.99999

# 找到匹配位置
loc = np.where(res >= threshold)

# 在目标图像中标记匹配位置
for pt in zip(*loc[::-1]):
    cv2.rectangle(img, pt, (pt[0] + w, pt[1] + h), (0, 255, 0), 2)

# 显示结果图像
cv2.imshow('Matched Image', img)
cv2.waitKey(0)
cv2.destroyAllWindows()
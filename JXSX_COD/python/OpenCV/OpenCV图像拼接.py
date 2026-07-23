'''
图像拼接是计算机视觉中的一个重要应用，它可以将多张有重叠区域的图像拼接成一张更大的图像。
应用场景
全景图生成: 将多幅图像拼接成一幅全景图。
地图拼接: 将多幅地图图像拼接成一幅更大的地图。
医学图像处理: 将多幅医学图像拼接成一幅完整的图像。
图像拼接的基本流程
图像拼接的基本流程可以分为以下几个步骤：
图像读取：读取需要拼接的图像。
特征点检测：在每张图像中检测出关键点（特征点）。
特征点匹配：在不同图像之间匹配这些特征点。
计算变换矩阵：根据匹配的特征点计算图像之间的变换矩阵。
图像融合：将图像按照变换矩阵进行拼接，并进行融合处理以消除拼接痕迹。
'''




import cv2
import numpy as np

# 1. 加载图像
image1 = cv2.imread("RC1.jpg")
image2 = cv2.imread("RC2.jpg")
# 2. 转换为灰度图
gray1 = cv2.cvtColor(image1, cv2.COLOR_BGR2GRAY)
gray2 = cv2.cvtColor(image2, cv2.COLOR_BGR2GRAY)
# 3. 特征点检测
sift = cv2.SIFT_create()
keypoints1, descriptors1 = sift.detectAndCompute(gray1, None)
keypoints2, descriptors2 = sift.detectAndCompute(gray2, None)
# 4. 特征点匹配
matcher = cv2.BFMatcher()
matches = matcher.knnMatch(descriptors1, descriptors2, k=2)
# 5. 筛选匹配点
good_matches = []
for m, n in matches:
    if m.distance < 0.75 * n.distance:
        good_matches.append(m)
# 6. 计算单应性矩阵
if len(good_matches) > 10:
    print(good_matches)
    src_pts = np.float32([keypoints1[m.queryIdx].pt for m in good_matches]).reshape(-1, 1, 2)
    dst_pts = np.float32([keypoints2[m.trainIdx].pt for m in good_matches]).reshape(-1, 1, 2)
    H, _ = cv2.findHomography(src_pts, dst_pts, cv2.RANSAC, 5.0)
else:
    print(good_matches)
    print("Not enough matches found.")
    exit()
# 7. 图像变换
height1, width1 = image1.shape[:2]
height2, width2 = image2.shape[:2]
warped_image = cv2.warpPerspective(image1, H, (width1 + width2, height1))

# 8. 图像拼接
warped_image[0:height2, 0:width2] = image2

# 9. 显示结果
cv2.imshow("Stitched Image", warped_image)
cv2.waitKey(0)
cv2.destroyAllWindows()
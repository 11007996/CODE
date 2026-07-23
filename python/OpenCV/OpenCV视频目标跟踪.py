'''
MeanShift（均值漂移）算法是一种基于密度的非参数化聚类算法，最初用于图像分割，后来被引入到目标跟踪领域。其核心思想是通过迭代计算目标区域的质心，并将窗口中心移动到质心位置，从而实现目标的跟踪。
MeanShift 算法的基本步骤如下：
初始化窗口：在视频的第一帧中，手动或自动选择一个目标区域，作为初始窗口。
计算质心：在当前窗口中，计算目标区域的质心（即像素点的均值）。
移动窗口：将窗口中心移动到质心位置。
迭代：重复步骤 2 和 3，直到窗口中心不再变化或达到最大迭代次数。
优点：
简单易实现，计算效率高。
对目标的形状和大小变化不敏感。
缺点：
对目标的快速运动或遮挡处理能力较差。
窗口大小固定，无法自适应目标大小的变化。
'''




import cv2
import numpy as np


'''
# 读取视频
cap = cv2.VideoCapture('video.mp4')
# 读取第一帧
ret, frame = cap.read()
# 设置初始窗口 (x, y, width, height)
x, y, w, h = 50, 200, 100, 50
track_window = (x, y, w, h)
# 设置 ROI (Region of Interest)
roi = frame[y:y+h, x:x+w]
# 转换为 HSV 颜色空间
hsv_roi = cv2.cvtColor(roi, cv2.COLOR_BGR2HSV)
# 创建掩膜并计算直方图
mask = cv2.inRange(hsv_roi, np.array((0., 60., 32.)), np.array((180., 255., 255.)))
roi_hist = cv2.calcHist([hsv_roi], [0], mask, [180], [0, 180])
cv2.normalize(roi_hist, roi_hist, 0, 255, cv2.NORM_MINMAX)
# 设置终止条件
term_crit = (cv2.TERM_CRITERIA_EPS | cv2.TERM_CRITERIA_COUNT, 10, 1)
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 转换为 HSV 颜色空间
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    # 计算反向投影
    dst = cv2.calcBackProject([hsv], [0], roi_hist, [0, 180], 1)
    # 应用 MeanShift 算法
    ret, track_window = cv2.meanShift(dst, track_window, term_crit)
    # 绘制跟踪结果
    x, y, w, h = track_window
    img2 = cv2.rectangle(frame, (x, y), (x+w, y+h), 255, 2)
    cv2.imshow('MeanShift Tracking', img2)
    if cv2.waitKey(30) & 0xFF == 27:
        break
cap.release()
cv2.destroyAllWindows()
'''







'''
CamShift（Continuously Adaptive MeanShift）算法是 MeanShift 的改进版本，它通过自适应调整窗口大小来更好地跟踪目标。CamShift 算法在 MeanShift 的基础上增加了窗口大小和方向的调整，使其能够适应目标在视频中的尺寸和旋转变化。
CamShift 算法的基本步骤如下：
初始化窗口：与 MeanShift 相同，在视频的第一帧中选择初始窗口。
计算质心：在当前窗口中，计算目标区域的质心。
移动窗口：将窗口中心移动到质心位置。
调整窗口大小和方向：根据目标的尺寸和方向调整窗口。
迭代：重复步骤 2 到 4，直到窗口中心不再变化或达到最大迭代次数。
'''

'''
# 读取视频
cap = cv2.VideoCapture('video.mp4')
# 读取第一帧
ret, frame = cap.read()
# 设置初始窗口 (x, y, width, height)
x, y, w, h = 100, 200, 50, 50
track_window = (x, y, w, h)
# 设置 ROI (Region of Interest)
roi = frame[y:y+h, x:x+w]
# 转换为 HSV 颜色空间
hsv_roi = cv2.cvtColor(roi, cv2.COLOR_BGR2HSV)
# 创建掩膜并计算直方图
mask = cv2.inRange(hsv_roi, np.array((0., 60., 32.)), np.array((180., 255., 255.)))
roi_hist = cv2.calcHist([hsv_roi], [0], mask, [180], [0, 180])
cv2.normalize(roi_hist, roi_hist, 0, 255, cv2.NORM_MINMAX)
# 设置终止条件
term_crit = (cv2.TERM_CRITERIA_EPS | cv2.TERM_CRITERIA_COUNT, 10, 1)
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 转换为 HSV 颜色空间
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    # 计算反向投影
    dst = cv2.calcBackProject([hsv], [0], roi_hist, [0, 180], 1)
    # 应用 CamShift 算法
    ret, track_window = cv2.CamShift(dst, track_window, term_crit)
    # 绘制跟踪结果
    pts = cv2.boxPoints(ret)
    pts = np.int32(pts)
    img2 = cv2.polylines(frame, [pts], True, 255, 2)
    cv2.imshow('CamShift Tracking', img2)
    if cv2.waitKey(30) & 0xFF == 27:
        break
cap.release()
cv2.destroyAllWindows()
'''


# OpenCV 提供了预训练的 Haar 特征分类器，可以直接用于人脸检测。这些分类器以 XML 文件的形式存储，包含了训练好的模型参数。
# OpenCV 中的 cv2.CascadeClassifier() 类用于加载和使用这些分类器
# 人脸检测的实现步骤
# 加载 Haar 特征分类器模型: 使用 cv2.CascadeClassifier() 加载预训练的人脸检测模型。
# 读取图像: 使用 cv2.imread() 读取待检测的图像。
# 转换为灰度图: 将图像转换为灰度图，因为 Haar 特征分类器在灰度图上运行更快。
# 检测人脸: 使用 detectMultiScale() 方法检测图像中的人脸。
# 绘制检测结果: 在图像中绘制检测到的人脸矩形框。
# 显示结果: 显示检测结果。



import cv2

# 加载 Haar 特征分类器
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')
# 读取图像
image = cv2.imread('R-C.jpg')
# 转换为灰度图像
gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# 进行人脸检测
# scaleFactor: 表示每次图像尺寸减小的比例，用于构建图像金字塔。默认值为 1.1。
# minNeighbors: 表示每个候选矩形框应该保留的邻居数量。默认值为 5。
# minSize: 表示检测目标的最小尺寸。默认值为 (30, 30)。
faces = face_cascade.detectMultiScale(gray_image, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))
# 绘制检测结果
for (x, y, w, h) in faces:
    cv2.rectangle(image, (x, y), (x+w, y+h), (255, 0, 0), 2)
# 显示结果
cv2.imshow('Detected Faces', image)
cv2.waitKey(0)
cv2.destroyAllWindows()

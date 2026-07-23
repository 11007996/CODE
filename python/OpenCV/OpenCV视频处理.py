'''
视频处理的应用
视频分析: 通过视频处理技术，可以分析视频中的运动、目标、事件等。
视频增强: 对视频进行去噪、增强、稳定化等处理，提升视频质量。
视频编辑: 对视频进行剪辑、拼接、添加特效等操作。
实时监控: 通过摄像头实时监控场景，并进行目标检测、行为分析等。
'''


import cv2




'''
# 创建 VideoCapture 对象，读取视频文件
cap = cv2.VideoCapture('video.mp4')
# 检查视频是否成功打开
if not cap.isOpened():
    print("Error: Could not open video.")
    exit()
# 读取视频帧
while True:
    ret, frame = cap.read()
    # 如果读取到最后一帧，退出循环
    if not ret:
        break;
    # 显示当前帧
    cv2.imshow('Video', frame)
    
    # 按下 'q' 键退出
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
# 释放资源
cap.release()
cv2.destroyAllWindows()
'''





'''
# 创建 VideoCapture 对象，读取摄像头视频
cap = cv2.VideoCapture(0)
# 检查摄像头是否成功打开
if not cap.isOpened():
    print("Error: Could not open camera.")
    exit()
# 读取视频帧
while True:
    ret, frame = cap.read()
    # 如果读取到最后一帧，退出循环
    if not ret:
        break
    # 显示当前帧
    cv2.imshow('Camera', frame)
    # 按下 'q' 键退出
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
# 释放资源
cap.release()
cv2.destroyAllWindows()
'''







'''
cap = cv2.VideoCapture('video.mp4')
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 将帧转换为灰度图像
    gray_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)     # 用于将图像从一种颜色空间转换为另一种颜色空间
    # 显示灰度帧
    cv2.imshow('Gray Video', gray_frame)
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
cap.release()
cv2.destroyAllWindows()
'''




'''
参数说明:
filename: 输出视频文件名。
fourcc: 视频编码器（如 cv2.VideoWriter_fourcc(*'XVID')）。
fps: 帧率。
frameSize: 帧大小（宽度, 高度）。
'''
'''
cap = cv2.VideoCapture('video.mp4')
# 获取视频的帧率和尺寸
fps = int(cap.get(cv2.CAP_PROP_FPS))
width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
# 创建 VideoWriter 对象，保存处理后的视频
fourcc = cv2.VideoWriter_fourcc(*'XVID')
out = cv2.VideoWriter('output.avi', fourcc, fps, (width, height))
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 将帧转换为灰度图像
    gray_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    # 将灰度帧写入输出视频
    out.write(cv2.cvtColor(gray_frame, cv2.COLOR_GRAY2BGR))
    # 显示灰度帧
    cv2.imshow('Gray Video', gray_frame)
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
cap.release()
out.release()
cv2.destroyAllWindows()
'''



'''
# 视频中的物体检测
# 加载 Haar 特征分类器
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')    # 使用 Haar 特征分类器进行人脸检测
cap = cv2.VideoCapture('face.mp4')
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 将帧转换为灰度图像
    gray_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    # 检测人脸
    faces = face_cascade.detectMultiScale(gray_frame, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))
    # 在帧上绘制矩形框标记人脸
    for (x, y, w, h) in faces:
        cv2.rectangle(frame, (x, y), (x+w, y+h), (255, 0, 0), 2)
    # 显示带有人脸标记的帧
    cv2.imshow('Face Detection', frame)
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
cap.release()
cv2.destroyAllWindows()
'''



'''
# 通过计算帧之间的差异来检测运动物体
cap = cv2.VideoCapture('video.mp4')
# 读取第一帧
ret, prev_frame = cap.read()
prev_gray = cv2.cvtColor(prev_frame, cv2.COLOR_BGR2GRAY)
while True:
    ret, frame = cap.read()
    if not ret:
        break
    # 将当前帧转换为灰度图像
    gray_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    # 计算当前帧与前一帧的差异
    frame_diff = cv2.absdiff(prev_gray, gray_frame)
    # 对差异图像进行二值化处理
    _, thresh = cv2.threshold(frame_diff, 30, 255, cv2.THRESH_BINARY)
    # 显示运动检测结果
    cv2.imshow('Motion Detection', thresh)
    # 更新前一帧
    prev_gray = gray_frame
    if cv2.waitKey(25) & 0xFF == ord('q'):
        break
cap.release()
cv2.destroyAllWindows()
'''



'''
常用函数
功能	函数/方法	说明
读取视频	cv2.VideoCapture()	读取视频文件或摄像头。
逐帧读取视频	cap.read()	逐帧读取视频。
获取视频属性	cap.get(propId)	获取视频的属性（如宽度、高度、帧率等）。
保存视频	cv2.VideoWriter()	创建视频写入对象并保存视频。
视频帧处理	图像处理函数（如 cv2.cvtColor()）	对视频帧进行图像处理。
目标跟踪	cv2.TrackerKCF_create()	使用目标跟踪算法跟踪视频中的物体。
运动检测	cv2.createBackgroundSubtractorMOG2()	使用背景减除算法检测视频中的运动物体。
'''






'''
# 用于从视频文件或摄像头中捕获视频帧
# cv2.VideoCapture(source)
# 打开默认摄像头
cap = cv2.VideoCapture(0)
while True:
    ret, frame = cap.read()
    if not ret:
        break
    cv2.imshow('Frame', frame)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break
cap.release()
cv2.destroyAllWindows()
'''





'''
用于在图像上绘制文本
cv2.putText(image, text, org, fontFace, fontScale, color, thickness)
参数说明:
image: 输入图像。
text: 要绘制的文本。
org: 文本左上角坐标。
fontFace: 字体类型（如 cv2.FONT_HERSHEY_SIMPLEX）。
fontScale: 字体缩放比例。
color: 文本颜色。
thickness: 文本线宽。
'''
'''
img = cv2.imread('opencvBase.jpg')
cv2.putText(img, 'Hello, OpenCV!', (100, 50), cv2.FONT_HERSHEY_SIMPLEX, 1, (255, 0, 0), 2)
cv2.imshow('Text', img)
cv2.waitKey(0)
cv2.destroyAllWindows()
'''

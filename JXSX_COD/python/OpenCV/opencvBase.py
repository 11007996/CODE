# 导入 OpenCV 库，并使用别名 cv 代替 cv
import cv2 as cv


# print(cv.__version__)    # opencv版本号








# 1. 读取图像
# 替换为实际的图像路径，这里是当前目录下的 "bird.jpg"
image_path = "opencvBase.jpg"
image = cv.imread(image_path)
# 检查图像是否成功读取
if image is None:
    print("错误：无法加载图像，请检查路径是否正确。")
    exit()
# 2. 显示图像
# 创建一个名为 "Display Image" 的窗口，并在其中显示图像
cv.imshow("Display Image", image)

# 3. 等待用户按键
# 参数 0 表示无限等待，直到用户按下任意键
key = cv.waitKey(0)

# 4. 根据用户按键执行操作
if key == ord('s'):  # 如果按下 's' 键
    # 保存图像
    output_path = "saved_image.jpg"
    cv.imwrite(output_path, image)
    print(f"图像已保存为 {output_path}")
elif key == ord('q'):  # 如果按下 'q' 键   返回字符 'q' 的 ASCII 码值
    print("程序已退出。")
else:  # 如果按下其他键
    print("图像未保存。")
# 5. 关闭所有窗口
cv.destroyAllWindows()





'''
以下是 OpenCV 中最常用的一些模块：
cv2.core: 核心模块，包含了图像处理的基础功能（如图像数组的表示和操作）。
cv2.imgproc: 图像处理模块，提供图像的各种操作，如滤波、图像变换、形态学操作等。
cv2.highgui: 图形用户界面模块，提供显示图像和视频的功能。
cv2.video: 提供视频处理的功能，如视频捕捉、视频流的处理等。
cv2.features2d: 特征检测与匹配模块，包含了角点、边缘、关键点检测等。
cv2.ml: 机器学习模块，提供了多种机器学习算法，可以进行图像分类、回归、聚类等。
cv2.calib3d : 相机校准和 3D 重建模块。
cv2.objdetect : 目标检测模块。
cv2.dnn : 深度学习模块。
'''





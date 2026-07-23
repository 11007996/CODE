import numpy as np
import matplotlib.pyplot as plt

x = np.array([1, 2, 3, 4])
y = np.array([1, 4, 9, 16])
plt.title("RUNOOB grid() Test")
plt.xlabel("x - label")
plt.ylabel("y - label")
plt.plot(x, y)
# plt.grid()  # 渲染背景网格线
# plt.grid(axis='x') # 设置y就在轴方向显示网格线
# plt.grid(axis='y') # 设置y就在轴方向显示网格线
plt.grid(color = 'r', linestyle = '-.', linewidth = 0.5)  # 设置网格线样式
plt.show()
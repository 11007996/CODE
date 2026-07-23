import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

# 示例数据
# data = {
#     'Height': [150, 160, 170, 180, 190],
#     'Weight': [45, 55, 65, 75, 85],
#     'Age': [20, 25, 30, 35, 40]
# }
# df = pd.DataFrame(data)
# 计算皮尔逊相关系数
# correlation = df.corr(method='pearson')    # Pearson 即皮尔逊相关系数，用于衡量了两个变量之间的线性关系强度和方向，它的取值范围在 -1 到 1 之间，其中 -1 表示完全负相关，1 表示完全正相关，0 表示无线性相关。
# print(correlation)   # 可以看到，Height 与 Weight 和 Age 都有很强的正相关性。
# spearman_correlation = df.corr(method='spearman')   # 斯皮尔曼相关系数用于衡量两个变量的单调关系（无论是线性还是非线性），它是基于变量的排名计算的。斯皮尔曼相关系数的取值范围与皮尔逊相关系数相同：-1 到 1。
# print(spearman_correlation)   # 也可以看到，Height 与 Weight 和 Age 都有很强的正相关性。
# kendall_correlation = df.corr(method='kendall')  # 肯德尔秩相关系数也用于衡量变量之间的单调关系，它是通过计算两个变量排名之间的一致性来得出的。 # 计算复杂，比较耗资源，适合小数据集
# print(kendall_correlation)
# plt.figure(figsize=(8, 6))
# sns.heatmap(df.corr(), annot=True, cmap='coolwarm', fmt='.2f', vmin=-1, vmax=1)   # 为了更直观地呈现相关性矩阵，可以使用热图（Heatmap）来可视化各个变量之间的相关性
# plt.title('Correlation Heatmap') 
# plt.show()


'''
# 创建一个示例数据框
data = {'A': [1, 2, 3, 4, 5], 'B': [5, 4, 3, 2, 1]}
df = pd.DataFrame(data)
# 计算 Pearson 相关系数
correlation_matrix = df.corr()
# 使用热图可视化 Pearson 相关系数
sns.heatmap(correlation_matrix, annot=True, cmap='coolwarm', fmt=".2f")    # 热力图
plt.show()
'''
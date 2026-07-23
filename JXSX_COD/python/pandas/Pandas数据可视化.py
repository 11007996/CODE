'''
图表类型	描述	方法
折线图	展示数据随时间或其他连续变量的变化趋势	df.plot(kind='line')
柱状图	比较不同类别的数据	df.plot(kind='bar')
水平柱状图	比较不同类别的数据，但柱子水平排列	df.plot(kind='barh')
直方图	显示数据的分布	df.plot(kind='hist')
散点图	展示两个数值型变量之间的关系	df.plot(kind='scatter', x='col1', y='col2')
箱线图	显示数据分布，包括中位数、四分位数等	df.plot(kind='box')
密度图	展示数据的密度分布	df.plot(kind='kde')
饼图	显示不同部分在整体中的占比	df.plot(kind='pie')
区域图	展示数据的累计数值	df.plot(kind='area')
'''


'''
1. 基本的 plot() 方法
参数	说明
kind	图表类型，支持 'line', 'bar', 'barh', 'hist', 'box', 'kde', 'density', 'area', 'pie' 等类型
x	设置 x 轴的数据列
y	设置 y 轴的数据列
title	图表的标题
xlabel	x 轴的标签
ylabel	y 轴的标签
color	设置图表的颜色
figsize	设置图表的大小（宽, 高）
legend	是否显示图例
'''




import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
'''
# 示例数据
data = {'Year': [2015, 2016, 2017, 2018, 2019, 2020],
        'Sales': [100, 150, 200, 250, 300, 350]}
df = pd.DataFrame(data)
# 绘制折线图
df.plot(kind='line', x='Year', y='Sales', title='Sales Over Years', xlabel='Year', ylabel='Sales', figsize=(10, 6))
plt.show()
'''

'''
# 示例数据
data = {'Category': ['A', 'B', 'C', 'D'],
        'Value': [10, 15, 7, 12]}
df = pd.DataFrame(data)
# 绘制柱状图
df.plot(kind='bar', x='Category', y='Value', title='Category Values', xlabel='Category', ylabel='Value', figsize=(8, 5),color="red")
plt.show()
'''

'''
# 示例数据
data = {'Height': [150, 160, 170, 180, 190],
        'Weight': [50, 60, 70, 80, 90]}
df = pd.DataFrame(data)
# 绘制散点图
df.plot(kind='scatter', x='Height', y='Weight', title='Height vs Weight', xlabel='Height (cm)', ylabel='Weight (kg)', figsize=(8, 5))
plt.show()
'''

'''
# 示例数据
data = {'Scores': [55, 70, 55, 55, 60, 55, 80, 95, 100, 65]}
df = pd.DataFrame(data)
# 绘制直方图
df.plot(kind='hist',y='Scores', bins=5, title='Scores Distribution', xlabel='Scores', figsize=(8, 5))
plt.show()
'''

'''
# 示例数据
data = {'Scores': [55, 70, 85, 90, 60, 75, 80, 95, 100, 65,20.5]}
df = pd.DataFrame(data)
# 绘制箱线图
df.plot(kind='box', title='Scores Boxplot', ylabel='Scores', figsize=(8, 5))   # 箱线图用于展示数据的分布情况，包括中位数、四分位数以及异常值。
plt.show()
'''


'''
# 示例数据
data = {'Category': ['A', 'B', 'C', 'D'],
        'Value': [10, 15, 7, 12]}
df = pd.DataFrame(data)
# 绘制饼图
df.plot(kind='pie', y='Value', labels=df['Category'], autopct='%1.1f%%', title='Category Proportions', figsize=(8, 5))
plt.show()
'''

'''
# 示例数据
data = {'A': [1, 2, 3], 'B': [4, 5, 6], 'C': [7, 8, 9]}
df = pd.DataFrame(data)
# 绘制相关性热力图
sns.heatmap(df.corr(), annot=True, cmap='coolwarm')
plt.show()
'''


'''
# 示例数据
data = {'A': [1, 2, 3], 'B': [4, 5, 6], 'C': [7, 8, 9]}
df = pd.DataFrame(data)
sns.pairplot(df)  # 数据集中所有数值特征之间的散点图矩阵
plt.show()
'''


'''
# 示例数据
data = {'Year': [2015, 2016, 2017, 2018, 2019],
        'Sales': [100, 150, 200, 250, 300]}
df = pd.DataFrame(data)
# 绘制折线图
plt.plot(df['Year'], df['Sales'], color='blue', marker='o')
# 自定义
plt.title('Sales Over Years')
plt.xlabel('Year')
plt.ylabel('Sales')
plt.grid(True)
# 显示
plt.show()
'''



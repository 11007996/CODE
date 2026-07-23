import pandas as pd

'''
merge() 方法允许根据某些列对两个 DataFrame 进行合并，类似 SQL 中的 JOIN 操作。支持内连接、外连接、左连接和右连接。
参数	说明
left	左侧 DataFrame
right	右侧 DataFrame
how	合并方式，支持 'inner', 'outer', 'left', 'right'
on	连接的列名（如果两侧列名不同，可使用 left_on 和 right_on）
left_on	左侧 DataFrame 的连接列
right_on	右侧 DataFrame 的连接列
suffixes	添加后缀，以区分重复的列名
'''
'''
# 示例数据
left = pd.DataFrame({'ID': [1, 2, 3], 'Name': ['Alice', 'Bob', 'Charlie']})
right = pd.DataFrame({'ID': [1, 2, 4], 'Age': [24, 27, 22]})
# 使用 merge 进行内连接
result = pd.merge(left, right, on='ID', how='inner')
print(result)
'''


'''
concat() 用于将多个 DataFrame 沿指定轴（行或列）进行连接，常用于行合并（垂直连接）或列合并（水平连接）
参数	说明
objs	需要合并的 DataFrame 列表
axis	合并的轴，0 表示按行合并，1 表示按列合并
ignore_index	是否忽略索引，重新生成索引（默认为 False）
keys	为合并的对象提供层次化索引
'''
'''
# 示例数据
df1 = pd.DataFrame({'A': [1, 2, 3]})
df2 = pd.DataFrame({'A': [4, 5, 6]})
# 行合并
result = pd.concat([df1, df2], axis=0, ignore_index=True)
print(result)
'''




'''
join() 方法是 Pandas 中的简化连接操作，通常用于基于索引将多个 DataFrame 连接。
参数	说明
other	需要连接的另一个 DataFrame
how	合并方式，支持 'left', 'right', 'outer', 'inner'
on	使用的连接列，默认基于索引
'''
'''
# 示例数据
left = pd.DataFrame({'A': [1, 2, 3]}, index=[1, 2, 3])
right = pd.DataFrame({'B': [4, 5, 6]}, index=[1, 2, 4])
# 使用 join 进行连接
# result = left.join(right, how='inner')
result = left.join(right, how='inner')
print(result)
'''




'''
Pandas提供了pivot_table()方法来创建透视表，和crosstab()方法来计算交叉表。透视表和交叉表都非常适合数据的汇总和重新排列。
pivot_table() — 创建透视表
参数	说明
data	输入的数据
values	要汇总的列
index	用作行索引的列
columns	用作列索引的列
aggfunc	聚合函数，默认为mean，可以是sum, count等
fill_value	填充缺失值
'''
'''
# 示例数据
data = {'Date': ['2024-01-01', '2024-01-02', '2024-01-03', '2024-01-04'],
        'Category': ['A', 'B', 'A', 'B'],
        'Sales': [100, 150, 200, 250]}
df = pd.DataFrame(data)
# 创建透视表
pivot_table = pd.pivot_table(df, values='Sales', index='Date', columns='Category', aggfunc='sum', fill_value=0)
print(pivot_table)
'''


'''
crosstab() — 创建交叉表
参数	说明
index	行标签
columns	列标签
values	用于计算的数据（可选）
aggfunc	聚合函数，默认 count
'''
'''
# 示例数据
data = {'Category': ['A', 'B', 'A', 'B', 'A', 'B'],
        'Region': ['North', 'South', 'North', 'South', 'West', 'East']}
df = pd.DataFrame(data)
# 创建交叉表
cross_table = pd.crosstab(df['Category'], df['Region'])
print(cross_table)
'''




'''
apply() — 应用函数到 DataFrame 或 Series 上
apply() 方法允许在 DataFrame 或 Series 上应用自定义函数，支持对行或列进行操作。
参数	说明
func	需要应用的函数
axis	默认为 0，表示按列应用；1 表示按行应用
raw	是否传递原始数据（默认为 False）
result_type	定义输出的类型，如 expand, reduce, broadcast
'''
'''
# 示例数据
df = pd.DataFrame({'A': [1, 2, 3, 4], 'B': [10, 20, 30, 40]})
# 定义自定义函数
def custom_func(x):
    return x * 2
# 在列上应用函数
df['A'] = df['A'].apply(custom_func)
print(df)
'''



'''
applymap() — 在整个 DataFrame 上应用函数
applymap() 只能应用于 DataFrame，作用于 DataFrame 中的每个元素。
'''
'''
# 示例数据
df = pd.DataFrame({'A': [1, 2, 3], 'B': [4, 5, 6]})
# 在 DataFrame 上应用自定义函数
df = df.applymap(lambda x: x ** 2)
print(df)
'''





'''
map() — 应用函数到 Series 上
map() 可以对 Series 中的每个元素应用一个函数或一个映射关系。
'''
'''
# 示例数据
df = pd.DataFrame({'A': ['cat', 'dog', 'rabbit']})
# 使用字典进行映射
df['A'] = df['A'].map({'cat': 'kitten', 'dog': 'puppy'})
print(df)
'''






'''
groupby() — 数据分组
参数	说明
by	按照哪个列或索引分组
axis	分组的轴，默认为 0，即按行进行分组
level	按照索引的级别进行分组（适用于 MultiIndex）
'''
'''
# 示例数据
df = pd.DataFrame({
    'Category': ['A', 'B', 'A', 'B', 'A', 'B'],
    'Value': [10, 20, 30, 40, 50, 60]
})
# 按照 Category 列进行分组并计算每组的总和
grouped = df.groupby('Category')['Value'].sum()
print(grouped)
'''





# 聚合操作（agg()）
# agg() 用于执行复杂的聚合操作，可以传入多个函数来同时计算多个聚合值。
'''
# 示例数据
df = pd.DataFrame({
    'Category': ['A', 'B', 'A', 'B', 'A', 'B'],
    'Value': [10, 20, 30, 40, 50, 60]
})
# 使用 agg() 来进行多个聚合操作
grouped = df.groupby('Category')['Value'].agg([sum, min, max])
print(grouped)
'''



'''
date_range() — 生成时间序列
参数	说明
start	起始日期
end	结束日期
periods	生成的时间点数
freq	频率（如 D 表示天，H 表示小时等）
'''

# 生成时间序列
# date_range = pd.date_range(start='2024-01-01', periods=5, freq='D')
# print(date_range)

# date = pd.to_datetime('2024-01-01')
# new_date = date + pd.Timedelta(days=10)     # 使用 pd.Timedelta() 可以进行时间的加减操作
# print(new_date)


'''
使用 rolling() 和 expanding() 方法进行滚动和扩展窗口操作，常用于时间序列中的移动平均等计算。
方法	说明
rolling()	计算滚动窗口操作，常用于移动平均等
expanding()	计算扩展窗口操作，累计值
'''
'''
df = pd.DataFrame({'Value': [10, 20, 30, 40, 50]})
# df['Rolling_Mean'] = df['Value'].rolling(window=3).mean()   # 计算前3天滚动平均
df['Rolling_Mean'] = df['Value'].expanding().mean()   # 累计之前天数平均
print(df)
'''


# 多重索引
'''
# 创建元组
index_tuples = [('A', 1), ('A', 2), ('B', 1), ('B', 2)]    # 使用元组来创建多重索引，每个元组对应一个索引层级。
# 创建多重索引
multi_index = pd.MultiIndex.from_tuples(index_tuples, names=['Letter', 'Number'])
# 创建 DataFrame
df = pd.DataFrame({'Value': [10, 20, 30, 40]}, index=multi_index)
print(df)
'''
'''
# 创建多个列表
index_values = [['A', 'B'], [1, 2]]
# 创建多重索引
multi_index = pd.MultiIndex.from_product(index_values, names=['Letter', 'Number'])   # 使用多个列表的笛卡尔积来创建多重索引，适合用于数据维度较多的情况。
# 创建 DataFrame
df = pd.DataFrame({'Value': [10, 20, 30, 40]}, index=multi_index)
print(df)
'''

# 示例数据
data = {
    'Letter': ['A', 'A', 'B', 'B'],
    'Number': [1, 2, 1, 2],
    'Value': [10, 20, 30, 40]
}
df = pd.DataFrame(data)
# 设置多重索引
df.set_index(['Letter', 'Number'], inplace=True)   # set_index() 方法可以将 DataFrame 的列转换为多重索引，适用于从已有的数据创建多重索引。
print(df)
# print(df.loc['A', 2])    # 通过多重索引获取数据
# print(df.xs(1, level='Number'))   # xs() 方法可以在多重索引中选择指定级别的切片。
# print(df.loc['A'])     # 选择 Letter 为 'A' 的所有数据
# df_sorted = df.sort_index(level=['Letter', 'Number'], ascending=[True, False])  # 按照多重索引排序
# print(df_sorted)
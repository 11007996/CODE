'''
pandas.DataFrame(data=None, index=None, columns=None, dtype=None, copy=False)
参数说明：
data：DataFrame 的数据部分，可以是字典、二维数组、Series、DataFrame 或其他可转换为 DataFrame 的对象。如果不提供此参数，则创建一个空的 DataFrame。
index：DataFrame 的行索引，用于标识每行数据。可以是列表、数组、索引对象等。如果不提供此参数，则创建一个默认的整数索引。
columns：DataFrame 的列索引，用于标识每列数据。可以是列表、数组、索引对象等。如果不提供此参数，则创建一个默认的整数索引。
dtype：指定 DataFrame 的数据类型。可以是 NumPy 的数据类型，例如 np.int64、np.float64 等。如果不提供此参数，则根据数据自动推断数据类型。
copy：是否复制数据。默认为 False，表示不复制数据。如果设置为 True，则复制输入的数据。
'''


import pandas as pd
import numpy as np

'''
# data = [['Google', 10], ['Runoob', 12], ['Wiki', 13]]   # 使用列表创建DataFrame,以行为单元
# data = {'Site':['Google', 'Runoob', 'Wiki'], 'Age':[10, 12, 13]}   # 使用字典创建DataFrame 以列为单元
data = np.array([['Google', 10],['Runoob', 12],['Wiki', 13]])    # 使用 ndarrays 创建DataFrame
index=['G','R','W']
df = pd.DataFrame(data,index=index, columns=['Site', 'Age'])
# 使用astype方法设置每列的数据类型
df['Site'] = df['Site'].astype(str)
df['Age'] = df['Age'].astype(float)
print(df)
'''

'''
data = [{'a': 1, 'b': 2},{'a': 5, 'b': 10, 'c': 20}]   # 自动对齐数据NaN
df = pd.DataFrame(data)
print(df)
'''

'''
data = {
  "calories": [420, 380, 390],
  "duration": [50, 40, 45]
}
# 数据载入到 DataFrame 对象
df = pd.DataFrame(data)
# print(df)
# print(df.loc[0])  # 返回第一行
# print(df.loc[1])  # 返回第二行
# print(df.loc[[1,2]])  # 同时返回多行
# print(df.shape) # 返回行数和列数
# print(df.sort_values("duration"))  # 按指定列排序
'''





# 创建 DataFrame
data = {
    'Name': ['Alice', 'Bob', 'Charlie', 'David'],
    'Age': [25, 30, 35, 40],
    'City': ['New York', 'Los Angeles', 'Chicago', 'Houston']
}
df = pd.DataFrame(data)
# print(df.head(2))  # 查看前两行数据
# print(df.info())  # 查看 DataFrame 的基本信息
# print(df.describe())  # 获取描述统计信息
# df_sorted = df.sort_values(by='Age', ascending=False)  # 按年龄排序   False表示从大到小降序排列
# print(df_sorted)
# print(df[['Name', 'Age']])  # 选择指定列
# print(df.iloc[1:3])  # 按索引选择行 选择第二到第三行（按位置）
# print(df.loc[1:2])  # 按标签选择行 选择第二到第三行（按标签）
# print(df.loc[0, 'Name'])  # 通过行标签访问
# print(df.groupby('City')['Age'].mean())  # 计算分组统计(按城市分组，计算平均年龄)
# df['Age'] = df['Age'].fillna(30) # 处理缺失值（填充缺失值）
# df.to_csv('output.csv', index=False) # 导出为 CSV 文件
# print(df['Age'].sum())   # 求和
# print(df[['Age']].sum())   # 求和
# print(df['Name'][0])     # 获取单个元素
# print(df['Name'])  # 返回整列，Series类型
# print(df[['Name']])  # 返回整列列值
# print(df['Age']>30)  # 数据过滤，数据清洗
# print(df[df['Age']>30]['Name'].reset_index().loc[0,'Name'])     # 返回年龄大于30的第一个人
# print(df[df['Age']>30]['Name'].reset_index()['Name'])     # 返回年龄大于30的人
# df['NewColumn'] = [100, 200, 300]    # 添加新列



# 使用concat添加新行
# new_row = pd.DataFrame([[4, 7]], columns=['A', 'B'])  # 创建一个只包含新行的DataFrame
# df = pd.concat([df, new_row], ignore_index=True)  # 将新行添加到原始DataFrame

# df1 = pd.DataFrame(['a','b','c','d'],columns=['hello'])
# pd.merge(df,df1, on='hello')   # 横向合并列
# print(df)

# df_dropped = df.drop('Column1', axis=1)    # 删除某列
# df_dropped = df.drop(0)  # 删除行索引为 0 的行



# 索引和切片
# print(df[1:3])               # 切片行
# print(df.loc[:, 'Name'])     # 提取单列
# print(df.loc[1:2, ['Name', 'Age']])  # 标签索引提取指定行列
# print(df.iloc[:, 1:])        # 位置索引提取指定列





'''
DataFrame 的常用操作和方法如下表所示：
方法名称	功能描述
head(n)	返回 DataFrame 的前 n 行数据（默认前 5 行）
tail(n)	返回 DataFrame 的后 n 行数据（默认后 5 行）
info()	显示 DataFrame 的简要信息，包括列名、数据类型、非空值数量等
describe()	返回 DataFrame 数值列的统计信息，如均值、标准差、最小值等
shape	返回 DataFrame 的行数和列数（行数, 列数）
columns	返回 DataFrame 的所有列名
index	返回 DataFrame 的行索引
dtypes	返回每一列的数值数据类型
sort_values(by)	按照指定列排序
sort_index()	按行索引排序
dropna()	删除含有缺失值（NaN）的行或列
fillna(value)	用指定的值填充缺失值
isnull()	判断缺失值，返回一个布尔值 DataFrame
notnull()	判断非缺失值，返回一个布尔值 DataFrame
loc[]	按标签索引选择数据
iloc[]	按位置索引选择数据
at[]	访问 DataFrame 中单个元素（比 loc[] 更高效）
iat[]	访问 DataFrame 中单个元素（比 iloc[] 更高效）
apply(func)	对 DataFrame 或 Series 应用一个函数
applymap(func)	对 DataFrame 的每个元素应用函数（仅对 DataFrame）
groupby(by)	分组操作，用于按某一列分组进行汇总统计
pivot_table()	创建透视表
merge()	合并多个 DataFrame（类似 SQL 的 JOIN 操作）
concat()	按行或按列连接多个 DataFrame
to_csv()	将 DataFrame 导出为 CSV 文件
to_excel()	将 DataFrame 导出为 Excel 文件
to_json()	将 DataFrame 导出为 JSON 格式
to_sql()	将 DataFrame 导出为 SQL 数据库
query()	使用 SQL 风格的语法查询 DataFrame
duplicated()	返回布尔值 DataFrame，指示每行是否是重复的
drop_duplicates()	删除重复的行
set_index()	设置 DataFrame 的索引
reset_index()	重置 DataFrame 的索引
transpose()	转置 DataFrame（行列交换）
'''
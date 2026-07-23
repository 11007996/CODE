import pandas as pd
import requests

# 从 CSV 文件中读取数据
# df = pd.read_csv('data.csv')

# 从 Excel 文件中读取数据
# df = pd.read_excel('data.xlsx')

# 从 SQL 数据库中读取数据
# import sqlite3
# conn = sqlite3.connect('D:\\ruanjian\\TOOL\\sqlite\\mydatabase.db')
# df = pd.read_sql('SELECT name FROM g_student_t', conn)
# print(df)

# 从 JSON 字符串中读取数据
# json_string = '{"name": "John", "age": 30, "city": "New York"}'
# df = pd.read_json(json_string)

# 从 HTML 页面中读取数据
# url = 'https://www.runoob.com'
# dfs = pd.read_html(url)
# df = dfs[0] # 选择第一个数据框


# data = {"name":["aaa","bbb","ccc"],"age":[11,22,33]}
# df = pd.DataFrame(data)
# print(df)
# print(df["name"][0])
# ds = df.replace(df["name"][0], "ddd")  # 替换数据
# print(ds)
# print(df.duplicated())   # 检查是否有重复的数据
# ds = df.drop_duplicates()   # 删除重复的数据
# print(ds)




'''
数据选择和切片
函数	说明
df[column_name]	选择指定的列；
df.loc[row_index, column_name]	通过标签选择数据；
df.iloc[row_index, column_index]	通过位置选择数据；
df.ix[row_index, column_name]	通过标签或位置选择数据；
df.filter(items=[column_name1, column_name2])	选择指定的列；
df.filter(regex='regex')	选择列名匹配正则表达式的列；
df.sample(n)	随机选择 n 行数据。
'''


# data = {"name":["aaa","bbb","ccc"],"age":[11,22,33]}
# df = pd.DataFrame(data)
# print(df.iloc[1,1])   # 返回单个元素
# print(df.loc[[0,1]])   # 切片返回某几行
# print(df.filter(items=["name", "age"]))  # 选择指定的列
# print(df.filter(regex='na\we'))  # 选择指定的列
# print(df.sample(1))   # 随机选择 n 行数据



'''
数据排序
函数	说明
df.sort_values(column_name)	按照指定列的值排序；
df.sort_values([column_name1, column_name2], ascending=[True, False])	按照多个列的值排序；
df.sort_index()	按照索引排序。
'''


# data = {"name":["bbb","aaa","ccc"],"age":[44,22,33]}
# df = pd.DataFrame(data)
# print(df.sort_values("age"))  # 按照指定列的值排序
# print(df.sort_values(["name", "age"], ascending=False))   # 按照多个列的值排序
# print(df.sort_index())   # 	按照索引排序


'''
数据分组和聚合
函数	说明
df.groupby(column_name)	按照指定列进行分组；
df.aggregate(function_name)	对分组后的数据进行聚合操作；
df.pivot_table(values, index, columns, aggfunc)	生成透视表。
'''


# data = {"name":["bbb","aaa","ccc","bbb"],"age":[44,22,33,11]}
# df = pd.DataFrame(data)
# print(df.groupby('name').count())   # 按照指定列进行分组
# print(df.aggregate('max'))    # 对分组后的数据进行聚合操作
# 生成透视表
# print(df.pivot_table(data, columns='name', aggfunc='count'))    # 生成透视表



'''
数据合并
函数	说明
pd.concat([df1, df2])	将多个数据框按照行或列进行合并；
pd.merge(df1, df2, on=column_name)	按照指定列将两个数据框进行合并。
'''

# data1 = {"name":["bbb","aaa","ccc","bbb"],"age":[44,22,33,11]}
# data2 = {"name":["ddd","ee"],"age":[23,24]}
# data3 = {"name":["ddd","aaa"],"age":[23,24]}
# df1 = pd.DataFrame(data1)
# df2 = pd.DataFrame(data2)
# df3 = pd.DataFrame(data3)
# df = pd.concat([df1, df2])   # 并集
# df = pd.merge(df1, df3, on='name')      # 多表连接
# print(df)



'''
数据选择和过滤
函数	说明
df.loc[row_indexer, column_indexer]	按标签选择行和列。
df.iloc[row_indexer, column_indexer]	按位置选择行和列。
df[df['column_name'] > value]	选择列中满足条件的行。
df.query('column_name > value')	使用字符串表达式选择列中满足条件的行。
'''

'''
数据统计和描述
函数	说明
df.describe()	计算基本统计信息，如均值、标准差、最小值、最大值等。
df.mean()	计算每列的平均值。
df.median()	计算每列的中位数。
df.mode()	计算每列的众数。
df.count()	计算每列非缺失值的数量。
'''

'''
data = {"name":["bbb","aaa","ccc"],"age":[44,22,33]}
df = pd.DataFrame(data)
print(df[df['age'] > 30])   # 	选择列中满足条件的行。
print(df.query('age > 30'))  # 使用字符串表达式选择列中满足条件的行。
print(df.mode())
'''
'''
Pandas清洗空值
DataFrame.dropna(axis=0, how='any', thresh=None, subset=None, inplace=False)
参数说明：
axis：默认为 0，表示逢空值剔除整行，如果设置参数 axis＝1 表示逢空值去掉整列。
how：默认为 'any' 如果一行（或一列）里任何一个数据有出现 NA 就去掉整行，如果设置 how='all' 一行（或列）都是 NA 才去掉这整行。
thresh：设置需要多少非空值的数据才可以保留下来的。
subset：设置想要检查的列。如果是多个列，可以使用列名的 list 作为参数。
inplace：如果设置 True，将计算得到的值直接覆盖之前的值并返回 None，修改的是源数据。
'''


import pandas as pd
from sklearn.preprocessing import StandardScaler

# df = pd.read_csv('property-data.csv')
# Pandas 默认把 n/a 和 NA 当作空数据
# print (df['NUM_BEDROOMS'])
# print (df['NUM_BEDROOMS'].isnull())

# missing_values = ["n/a", "na", "--"]   # 指定空值
# df = pd.read_csv('property-data.csv', na_values = missing_values)
# print (df['NUM_BEDROOMS'])
# print (df['NUM_BEDROOMS'].isnull())   # 显示数据是否为空
# new_df = df.dropna()   # 删除包含空值的行
# df.dropna(inplace = True)   # 删除包含空值的行并覆盖修改原数据
# df.dropna(subset=['ST_NUM','NUM_BEDROOMS'], inplace = True)   # 移除指定列有空值的行
# df.fillna(12345, inplace = True)   # 用12345替换空值
# df['PID'].fillna(12345, inplace = True)  # 指定替换某一个列空值数据
# print(df.to_string())
# x = df["ST_NUM"].mean()   # 以平均值来替换空值
# x = df["ST_NUM"].median()   # 以中位数来替换空值
# x = df["ST_NUM"].mode()   # 以众数来替换空值
# df["ST_NUM"].fillna(x, inplace = True)
# print(df.to_string())


# 清洗格式错误数据
# 第三个日期格式错误
# data = {
#   "Date": ['2020/12/01', '2020/12/02' , '20201226'],
#   "duration": [50, 40, 45]
# }
# df = pd.DataFrame(data, index = ["day1", "day2", "day3"])
# df['Date'] = pd.to_datetime(df['Date'], format='mixed')    # 统一时间格式
# print(df.to_string())

# 清洗错误数据
# person = {
#   "name": ['Google', 'Runoob' , 'Taobao'],
#   "age": [50, 40, 12345]    # 12345 年龄数据是错误的
# }
# df = pd.DataFrame(person)
# df.loc[2, 'age'] = 30 # 修改数据
# print(df.to_string())
# person = {
#   "name": ['Google', 'Runoob' , 'Taobao'],
#   "age": [50, 200, 12345]    
# }
# df = pd.DataFrame(person)
# for x in df.index:
#   if df.loc[x, "age"] > 120:
#     df.loc[x, "age"] = 120    # 将 age 大于 120 的设置为 120:
# print(df.to_string())
# person = {
#   "name": ['Google', 'Runoob' , 'Taobao'],
#   "age": [50, 40, 12345]    # 12345 年龄数据是错误的
# }
# df = pd.DataFrame(person)
# for x in df.index:
#   if df.loc[x, "age"] > 120:
#     df.drop(x, inplace = True)   # 将 age 大于 120 的删除
# print(df.to_string())

# person = {
#   "name": ['Google', 'Runoob', 'Runoob', 'Taobao'],
#   "age": [50, 40, 40, 23]  
# }
# df = pd.DataFrame(person)
# print(df.duplicated())     # 检查整行是否为重复值
# df.drop_duplicates(inplace = True)   # 删除重复值
# print(df)



# data = {'City': ['New York', 'Los Angeles', 'Chicago', 'Houston']}
# df = pd.DataFrame(data)
# # 对 "City" 列进行 One-Hot 编码
# df_encoded = pd.get_dummies(df, columns=['City'])
# print(df_encoded)

'''
# 示例数据 标准化
data = {'Age': [25, 30, 35, 40, 45],
        'Salary': [50000, 60000, 70000, 80000, 90000]}
df = pd.DataFrame(data)
# 标准化数据
scaler = StandardScaler()
df_scaled = scaler.fit_transform(df)

print(df_scaled)

'''






'''
常用方法及说明
数据清洗与预处理的常见方法：
操作	方法/步骤	说明	常用函数/方法
缺失值处理	填充缺失值	使用指定的值（如均值、中位数、众数等）填充缺失值。	df.fillna(value)
删除缺失值	删除包含缺失值的行或列。	df.dropna()
重复数据处理	删除重复数据	删除 DataFrame 中的重复行。	df.drop_duplicates()
异常值处理	异常值检测（基于统计方法）	通过 Z-score 或 IQR 方法识别并处理异常值。	自定义函数（如基于 Z-score 或 IQR）
替换异常值	使用合适的值（如均值或中位数）替换异常值。	自定义函数（如替换异常值）
数据格式转换	转换数据类型	将数据类型从一个类型转换为另一个类型，如将字符串转换为日期。	df.astype()
日期时间格式转换	转换字符串或数字为日期时间类型。	pd.to_datetime()
标准化与归一化	标准化	将数据转换为均值为0，标准差为1的分布。	StandardScaler()
归一化	将数据缩放到指定的范围（如 [0, 1]）。	MinMaxScaler()
类别数据编码	标签编码	将类别变量转换为整数形式。	LabelEncoder()
独热编码（One-Hot Encoding）	将每个类别转换为一个新的二进制特征。	pd.get_dummies()
文本数据处理	去除停用词	从文本中去除无关紧要的词，如 "the" 、 "is" 等。	自定义函数（基于 nltk 或 spaCy）
词干化与词形还原	提取词干或恢复单词的基本形式。	nltk.stem.PorterStemmer()
分词	将文本分割成单词或子词。	nltk.word_tokenize()
数据抽样	随机抽样	从数据中随机抽取一定比例的样本。	df.sample()
上采样与下采样	通过过采样（复制少数类样本）或欠采样（减少多数类样本）来平衡数据集中的类别分布。	SMOTE()（上采样）； RandomUnderSampler()（下采样）
特征工程	特征选择	选择对目标变量有影响的特征，去除冗余或无关特征。	SelectKBest()
特征提取	从原始数据中创建新的特征，提升模型的预测能力。	PolynomialFeatures()
特征缩放	对数值特征进行缩放，使其具有相同的量级。	MinMaxScaler() 、 StandardScaler()
类别特征映射	特征映射	将类别变量映射为对应的数字编码。	自定义映射函数
数据合并与连接	合并数据	将多个 DataFrame 按照某些列合并在一起，支持内连接、外连接、左连接、右连接等。	pd.merge()
连接数据	将多个 DataFrame 进行行或列拼接。	pd.concat()
数据重塑	数据透视表	将数据根据某些维度进行分组并计算聚合结果。	pd.pivot_table()
数据变形	改变数据的形状，如从长格式转为宽格式或从宽格式转为长格式。	df.melt() 、 df.pivot()
数据类型转换与处理	字符串处理	对字符串数据进行处理，如去除空格、转换大小写等。	str.replace() 、 str.upper() 等
分组计算	按照某个特征分组后进行聚合计算。	df.groupby()
缺失值预测填充	使用模型预测填充缺失值	使用机器学习模型（如回归模型）预测缺失值，并填充缺失数据。	自定义模型（如 sklearn.linear_model.LinearRegression）
时间序列处理	时间序列缺失值填充	使用时间序列的方法（如前向填充、后向填充）填充缺失值。	df.fillna(method='ffill')
滚动窗口计算	使用滑动窗口进行时间序列数据的统计计算（如均值、标准差等）。	df.rolling(window=5).mean()
数据转换与映射	数据映射与替换	将数据中的某些值替换为其他值。	df.replace()
'''
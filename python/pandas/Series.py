'''   # Series语法
pandas.Series(data=None, index=None, dtype=None, name=None, copy=False, fastpath=False)
参数说明：
data：Series 的数据部分，可以是列表、数组、字典、标量值等。如果不提供此参数，则创建一个空的 Series。
index：Series 的索引部分，用于对数据进行标记。可以是列表、数组、索引对象等。如果不提供此参数，则创建一个默认的整数索引。
dtype：指定 Series 的数据类型。可以是 NumPy 的数据类型，例如 np.int64、np.float64 等。如果不提供此参数，则根据数据自动推断数据类型。
name：Series 的名称，用于标识 Series 对象。如果提供了此参数，则创建的 Series 对象将具有指定的名称。
copy：是否复制数据。默认为 False，表示不复制数据。如果设置为 True，则复制输入的数据。
fastpath：是否启用快速路径。默认为 False。启用快速路径可能会在某些情况下提高性能。
'''



import pandas as pd

'''
# 创建一个Series对象，指定名称为'A'，值分别为1, 2, 3, 4
# 默认索引为0, 1, 2, 3
series = pd.Series([1, 2, 3, 4], name='A')
# 显示Series对象
print(series)
# 如果你想要显式地设置索引，可以这样做：
custom_index = [1, 2, 3, 4]  # 自定义索引
series_with_index = pd.Series([1, 2, 3, 4], index=["a","b","c","d"], name='A')
# 显示带有自定义索引的Series对象
print(series_with_index["a"])
'''
'''

# 创建一个简单的 Series
ser = {0:"a",1:"b",2:"c",4:"d",5:"e"}
series_a = pd.Series(ser,name="S")
print(series_a)
print("索引：", series_a.index)
print("数据：", series_a.values)
print("数据类型：", series_a.dtype)
print("前两行数据：", series_a.head(2))
print("元素加倍后：", series_a.map(lambda x: x * 2))
print("累计求和：", series_a.cumsum())
print("缺失值判断：", series_a.isnull())
print("排序后的 Series：", series_a.sort_values())
'''


'''
# 创建一个简单的 DataFrame
data = {'Name': ['Google', 'Runoob', 'Taobao'], 'Age': [25, 30, 35]}
df = pd.DataFrame(data)
print(df)
'''


'''
# 创建两个Series对象,两个Series合并为一个DataFrame
series_apples = pd.Series([1, 3, 7, 4])
series_bananas = pd.Series([2, 6, 3, 5])
# 将两个Series对象相加，得到DataFrame，并指定列名
df = pd.DataFrame({ 'Apples': series_apples, 'Bananas': series_bananas })
# 显示DataFrame
print(df)
'''



# 创建一个Series对象，指定名称为'A'，值分别为1, 2, 3, 4
# 默认索引为0, 1, 2, 3
# Series对象包括名称(列名)，值，索引
# series = pd.Series([1, 2, 3, 4], name='A')
# print(series)   # 显示Series对象
# print(series[0])  #以索引显示数据
# 如果你想要显式地设置索引，可以这样做：
# custom_index = [1, 2, 3, 4]  # 自定义索引
# series_with_index = pd.Series([1, 2, 3, 4], index=custom_index, name='A')
# 显示带有自定义索引的Series对象
# print(series_with_index)


'''
# 只取字典的一部分数据
sites = {1: "Google", 2: "Runoob", 3: "Wiki"}
myvar = pd.Series(sites, index = [1, 2], name="RUNOOB-Series-TEST" )
print(myvar)
'''

def add(i):
	return i+1
a = [1,2,3,1,4,5,2,1]
myvar = pd.Series(a)
# print(myvar.map(add))   # 每个元素都加1
# print(myvar[1:4])   # 获取索引为1到3的值
# print(myvar[:3])   # 获取前3个元素
# print(myvar.head(3))   # 获取前3个元素
# myvar[8] = 6   # 赋值增加元素
# del myvar[0]  # 删除索引对应的元素
# myvar_dropped = myvar.drop([0,1])  # 返回一个删除了索引标签 'b' 的新 Series
# print(myvar)
# print(myvar_dropped)
# print(myvar * 2)   # 所有元素乘2
# filtered_series = myvar[myvar > 2]  # 选择大于2的元素,数据过滤
# print(filtered_series)
# print(myvar.sum())  # 输出 Series 的总和
# print(myvar.mean())  # 输出 Series 的平均值
# print(myvar.max())  # 输出 Series 的最大值
# print(myvar.min())  # 输出 Series 的最小值
# print(myvar.std())  # 输出 Series 的标准差
# 获取最大值和最小值的索引
# print(myvar.idxmax())
# print(myvar.idxmin())
# print(myvar.size)   # 获取元素个数
# myvar = myvar.astype('float64')  # 将 Series 中的所有元素转换为 float64 类型
# print(myvar.dtype)














'''
方法名称	功能描述
index	获取 Series 的索引
values	获取 Series 的数据部分（返回 NumPy 数组）
head(n)	返回 Series 的前 n 行（默认为 5）
tail(n)	返回 Series 的后 n 行（默认为 5）
dtype	返回 Series 中数据的类型
shape	返回 Series 的形状（行数）
describe()	返回 Series 的统计描述（如均值、标准差、最小值等）
isnull()	返回一个布尔 Series，表示每个元素是否为 NaN
notnull()	返回一个布尔 Series，表示每个元素是否不是 NaN
unique()	返回 Series 中的唯一值（去重）
value_counts()	返回 Series 中每个唯一值的出现次数
map(func)	将指定函数应用于 Series 中的每个元素
apply(func)	将指定函数应用于 Series 中的每个元素，常用于自定义操作
astype(dtype)	将 Series 转换为指定的类型
sort_values()	对 Series 中的元素进行排序（按值排序）
sort_index()	对 Series 的索引进行排序
dropna()	删除 Series 中的缺失值（NaN）
fillna(value)	填充 Series 中的缺失值（NaN）
replace(to_replace, value)	替换 Series 中指定的值
cumsum()	返回 Series 的累计求和
cumprod()	返回 Series 的累计乘积
shift(periods)	将 Series 中的元素按指定的步数进行位移
rank()	返回 Series 中元素的排名
corr(other)	计算 Series 与另一个 Series 的相关性（皮尔逊相关系数）
cov(other)	计算 Series 与另一个 Series 的协方差
to_list()	将 Series 转换为 Python 列表
to_frame()	将 Series 转换为 DataFrame
iloc[]	通过位置索引来选择数据
loc[]	通过标签索引来选择数据
'''
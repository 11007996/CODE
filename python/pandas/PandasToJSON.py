'''
语法格式：
df = pd.read_json(
    path_or_buffer,      # JSON 文件路径、JSON 字符串或 URL
    orient=None,         # JSON 数据的结构方式，默认是 'columns'
    dtype=None,          # 强制指定列的数据类型
    convert_axes=True,   # 是否转换行列索引
    convert_dates=True,  # 是否将日期解析为日期类型
    keep_default_na=True # 是否保留默认的缺失值标记
)

参数说明：
参数	说明	默认值
path_or_buffer	JSON 文件的路径、JSON 字符串或 URL	必需参数
orient	定义 JSON 数据的格式方式。常见值有 split、records、index、columns、values。	None（根据文件自动推断）
dtype	强制指定列的数据类型	None
convert_axes	是否将轴转换为合适的数据类型	True
convert_dates	是否将日期解析为日期类型	True
keep_default_na	是否保留默认的缺失值标记（如 NaN）	True

常见的 orient 参数选项:
orient 值	JSON 格式示例	描述
split	{"index":["a","b"],"columns":["A","B"],"data":[[1,2],[3,4]]}	使用键 index、columns 和 data 结构
records	[{"A":1,"B":2},{"A":3,"B":4}]	每个记录是一个字典，表示一行数据
index	{"a":{"A":1,"B":2},"b":{"A":3,"B":4}}	使用索引为键，值为字典的方式
columns	{"A":{"a":1,"b":3},"B":{"a":2,"b":4}}	使用列名为键，值为字典的方式
values	[[1,2],[3,4]]	只返回数据，不包括索引和列名
'''


import pandas as pd
import json

# df = pd.read_json('sites.json')   # 读取JSON文件
# print(df.to_string())
# 字典格式的 JSON           

# s = {
#     "col1":{"row1":1,"row2":2,"row3":3},
#     "col2":{"row1":"x","row2":"y","row3":"z"}
# }
# # 读取 JSON 转为 DataFrame                                                                                          
# df = pd.DataFrame(s)
# print(df)

# 远程JSON资源
# URL = 'https://static.jyshare.com/download/sites.json'
# df = pd.read_json(URL)
# print(df)


# JSON 字符串
# 三个单引号不在行首表示字符串段落
# json_data = '''
# [
#   {"Name": "Alice", "Age": 25, "City": "New York"},
#   {"Name": "Bob", "Age": 30, "City": "Los Angeles"},
#   {"Name": "Charlie", "Age": 35, "City": "Chicago"}
# ]'''

# # 从 JSON 字符串读取数据
# df = pd.read_json(json_data)
# df = pd.read_json(json_data, orient='records')
# print(df)



# 读取嵌套的JSON
# df = pd.read_json('nested_list.json')
# print(df)
# 使用 Python JSON 模块载入数据
# with open('nested_list.json','r') as f:
#     data = json.loads(f.read())
# # 展平数据
# df_nested_list = pd.json_normalize(data, record_path =['students'])   # 使用了参数 record_path 并设置为 ['students'] 用于展开内嵌的 JSON 数据 students
# print(df_nested_list)
# 展平数据
# df_nested_list = pd.json_normalize(
#     data,
#     record_path =['students'],
#     meta=['school_name', 'class']   #  meta 参数来显示非表格化数据
# )
# print(df_nested_list)




# 使用 Python JSON 模块载入数据
# with open('nested_mix.json','r') as f:
#     data = json.loads(f.read())
   
# df = pd.json_normalize(
#     data,
#     record_path =['students'],
#     meta=[
#         'class',
#         ['info', 'president'],   # 逗号相当于点句号，读取info.president
#         ['info', 'contacts', 'tel']   # 逗号相当于点句号，读取info.contacts.tel
#     ]
# )
# print(df)





'''
语法格式：
df.to_json(
    path_or_buffer=None,    # 输出的文件路径或文件对象，如果是 None 则返回 JSON 字符串
    orient=None,            # JSON 格式方式，支持 'split', 'records', 'index', 'columns', 'values'
    date_format=None,       # 日期格式，支持 'epoch', 'iso'
    default_handler=None,   # 自定义非标准类型的处理函数
    lines=False,            # 是否将每行数据作为一行（适用于 'records' 或 'split'）
    encoding='utf-8'        # 编码格式
)
参数说明：
参数	说明	默认值
path_or_buffer	输出的文件路径或文件对象，若为 None，则返回 JSON 字符串	None
orient	指定 JSON 格式结构，支持 split、records、index、columns、values	None（默认是 columns）
date_format	日期格式，支持 'epoch' 或 'iso' 格式	None
default_handler	自定义处理非标准类型（如 datetime 等）的处理函数	None
lines	是否将每行数据作为一行输出（适用于 records 或 split）	False
encoding	输出文件的编码格式	'utf-8'
'''


# 创建 DataFrame
# df = pd.DataFrame({
#     'Name': ['Alice', 'Bob', 'Charlie'],
#     'Age': [25, 30, 35],
#     'City': ['New York', 'Los Angeles', 'Chicago']
# })
# 将 DataFrame 转换为 JSON 字符串
# json_str = df.to_json()
# print(json_str)
# df.to_json('data.json', orient='records', lines=True)





# 创建 DataFrame，包含日期数据
# df = pd.DataFrame({
#     'Name': ['Alice', 'Bob', 'Charlie'],
#     'Date': pd.to_datetime(['2021-01-01', '2022-02-01', '2023-03-01']),
#     'Age': [25, 30, 35]
# })
# 将 DataFrame 转换为 JSON，并指定日期格式为 'iso'
# json_str = df.to_json(date_format='iso')
# json_str = df.to_json(date_format='epoch')
# print(json_str)
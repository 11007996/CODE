'''
语法格式如下：
pandas.read_excel(io, sheet_name=0, *, header=0, names=None, index_col=None, usecols=None, dtype=None, engine=None, converters=None, true_values=None, false_values=None, skiprows=None, nrows=None, na_values=None, keep_default_na=True, na_filter=True, verbose=False, parse_dates=False, date_parser=<no_default>, date_format=None, thousands=None, decimal='.', comment=None, skipfooter=0, storage_options=None, dtype_backend=<no_default>, engine_kwargs=None)
参数说明：
io：这是必需的参数，指定了要读取的 Excel 文件的路径或文件对象。
sheet_name=0：指定要读取的工作表名称或索引。默认为0，即第一个工作表。
header=0：指定用作列名的行。默认为0，即第一行。
names=None：用于指定列名的列表。如果提供，将覆盖文件中的列名。
index_col=None：指定用作行索引的列。可以是列的名称或数字。
usecols=None：指定要读取的列。可以是列名的列表或列索引的列表。
dtype=None：指定列的数据类型。可以是字典格式，键为列名，值为数据类型。
engine=None：指定解析引擎。默认为None，pandas 会自动选择。
converters=None：用于转换数据的函数字典。
true_values=None：指定应该被视为布尔值True的值。
false_values=None：指定应该被视为布尔值False的值。
skiprows=None：指定要跳过的行数或要跳过的行的列表。
nrows=None：指定要读取的行数。
na_values=None：指定应该被视为缺失值的值。
keep_default_na=True：指定是否要将默认的缺失值（例如NaN）解析为NA。
na_filter=True：指定是否要将数据转换为NA。
verbose=False：指定是否要输出详细的进度信息。
parse_dates=False：指定是否要解析日期。
date_parser=<no_default>：用于解析日期的函数。
date_format=None：指定日期的格式。
thousands=None：指定千位分隔符。
decimal='.'：指定小数点字符。
comment=None：指定注释字符。
skipfooter=0：指定要跳过的文件末尾的行数。
storage_options=None：用于云存储的参数字典。
dtype_backend=<no_default>：指定数据类型后端。
engine_kwargs=None：传递给引擎的额外参数字典。
'''




import pandas as pd
from datetime import date, datetime
import io
import zipfile 


# 读取 data.xlsx 文件
# df = pd.read_excel('runoob_pandas_data.xlsx')
# df = pd.read_excel('runoob_pandas_data.xlsx', sheet_name='Sheet1',names=['A', 'B', 'C'],skiprows=1)
# 打印读取的 DataFrame
# print(df)




'''
语法格式如下：
DataFrame.to_excel(excel_writer, *, sheet_name='Sheet1', na_rep='', float_format=None, columns=None, header=True, index=True, index_label=None, startrow=0, startcol=0, engine=None, merge_cells=True, inf_rep='inf', freeze_panes=None, storage_options=None, engine_kwargs=None)
参数说明：
excel_writer：这是必需的参数，指定了要写入的 Excel 文件路径或文件对象。
sheet_name='Sheet1'：指定写入的工作表名称，默认为 'Sheet1'。
na_rep=''：指定在 Excel 文件中表示缺失值（NaN）的字符串，默认为空字符串。
float_format=None：指定浮点数的格式。如果为 None，则使用 Excel 的默认格式。
columns=None：指定要写入的列。如果为 None，则写入所有列。
header=True：指定是否写入列名作为第一行。如果为 False，则不写入列名。
index=True：指定是否写入索引作为第一列。如果为 False，则不写入索引。
index_label=None：指定索引列的标签。如果为 None，则不写入索引标签。
startrow=0：指定开始写入的行号，默认从第0行开始。
startcol=0：指定开始写入的列号，默认从第0列开始。
engine=None：指定写入 Excel 文件时使用的引擎，默认为 None，pandas 会自动选择。
merge_cells=True：指定是否合并单元格。如果为 True，则合并具有相同值的单元格。
inf_rep='inf'：指定在 Excel 文件中表示无穷大值的字符串，默认为 'inf'。
freeze_panes=None：指定冻结窗格的位置。如果为 None，则不冻结窗格。
storage_options=None：用于云存储的参数字典。
engine_kwargs=None：传递给引擎的额外参数字典。
'''



'''
# 创建一个简单的 DataFrame
df = pd.DataFrame({
'Name': ['Alice', 'Bob', 'Charlie'],
'Age': [25, 30, 35],
'City': ['New York', 'Los Angeles', 'Chicago']
})
# 将 DataFrame 写入 Excel 文件，写入 'Sheet1' 表单
# df.to_excel('output.xlsx', sheet_name='Sheet1', index=False,freeze_panes=(1,0))   # freeze_panes冻结窗口
# 写入多个表单，使用 ExcelWriter
with pd.ExcelWriter('output.xlsx') as writer:
	df.to_excel(writer, sheet_name='Sheet1', index=False)
	df.to_excel(writer, sheet_name='Sheet2', index=False)
'''




'''
语法格式如下：
excel_file = pd.ExcelFile('data.xlsx')
常用方法：
方法	功能描述
sheet_names	返回文件中所有表单的名称列表
parse(sheet_name)	解析指定表单并返回一个 DataFrame
close()	关闭文件，以释放资源
'''




'''
# 使用 ExcelFile 加载 Excel 文件
excel_file = pd.ExcelFile('output.xlsx')
print(excel_file.sheet_names)   # 查看所有表单的名称
df = excel_file.parse('Sheet1')   # 读取指定的表单
print(df)
excel_file.close()   # 关闭文件
'''





'''
语法格式如下：
pandas.ExcelWriter(path, engine=None, date_format=None, datetime_format=None, mode='w', storage_options=None, if_sheet_exists=None, engine_kwargs=None)
参数说明：
path：这是必需的参数，指定了要写入的 Excel 文件的路径、URL 或文件对象。可以是本地文件路径、远程存储路径（如 S3）、URL 链接或已打开的文件对象。
engine：这是一个可选参数，用于指定写入 Excel 文件的引擎。如果为 None，则 pandas 会自动选择一个可用的引擎（默认优先选择 openpyxl，如果不可用则选择其他可用引擎）。常见的引擎包括 'openpyxl'（用于 .xlsx 文件）、'xlsxwriter'（提供高级格式化和图表功能）、'odf'（用于 OpenDocument 格式如 .ods）等。
date_format：这是一个可选参数，指定写入 Excel 文件中日期的格式字符串，例如 "YYYY-MM-DD"。
datetime_format：这是一个可选参数，指定写入 Excel 文件中日期时间对象的格式字符串，例如 "YYYY-MM-DD HH:MM:SS"。
mode：这是一个可选参数，默认为 'w'，表示写入模式。如果设置为 'a'，则表示追加模式，向现有文件中添加数据（仅支持部分引擎，如 openpyxl）。
storage_options：这是一个可选参数，用于指定与存储后端连接的额外选项，例如认证信息、访问权限等，适用于写入远程存储（如 S3、GCS）。
if_sheet_exists：这是一个可选参数，默认为 'error'，指定如果工作表已经存在时的行为。选项包括 'error'（抛出错误）、'new'（创建一个新工作表）、'replace'（替换现有工作表的内容）、'overlay'（在现有工作表上覆盖写入）。
engine_kwargs：这是一个可选参数，用于传递给引擎的其他关键字参数。这些参数会传递给相应引擎的函数，例如 xlsxwriter.Workbook(file, **engine_kwargs) 或 openpyxl.Workbook(**engine_kwargs) 等。
'''





df = pd.DataFrame(
    [
        [date(2018, 1, 31), date(1999, 9, 24)],
        [datetime(1998, 5, 26, 23, 33, 4), datetime(2014, 2, 28, 13, 5, 13)],
    ],
    index=["Date", "Datetime"],
    columns=["X", "Y"],
)
# with pd.ExcelWriter(
#     "path_to_file.xlsx",
#     date_format="YYYY-MM-DD",
#     datetime_format="YYYY-MM-DD HH:MM:SS"
# ) as writer:
#     df.to_excel(writer)   # 写入EXCEL表
# with pd.ExcelWriter("path_to_file.xlsx", mode="a", engine="openpyxl") as writer:
#     df.to_excel(writer, sheet_name="Sheet4")   # 追加数据表
# with pd.ExcelWriter(
#     "path_to_file.xlsx",
#     mode="a",
#     engine="openpyxl",
#     if_sheet_exists="replace",
# ) as writer:
#     df.to_excel(writer, sheet_name="Sheet1")    # 替换数据表的数据

# with pd.ExcelWriter("path_to_file.xlsx",
#     mode="a",
#     engine="openpyxl",
#     if_sheet_exists="overlay",
# ) as writer:
#     df.to_excel(writer, sheet_name="Sheet1")
#     df.to_excel(writer, sheet_name="Sheet1", startcol=3)    # 向同一个工作表写入多个 DataFrame


# df = pd.DataFrame([["ABC", "XYZ"]], columns=["Foo", "Bar"])
# buffer = io.BytesIO()
# with pd.ExcelWriter(buffer) as writer:
#     df.to_excel(writer)   # 写入内存

'''
df = pd.DataFrame([["ABC", "XYZ"]], columns=["Foo", "Bar"])
with zipfile.ZipFile("path_to_file.zip", "w") as zf:
    with zf.open("filename.xlsx", "w") as buffer:   # 写入压缩包
        with pd.ExcelWriter(buffer) as writer:
            df.to_excel(writer)
'''

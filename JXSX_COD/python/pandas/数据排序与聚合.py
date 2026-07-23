import pandas as pd


# 示例数据
# data = {'Name': ['Alice', 'Bob', 'Charlie', 'David'],
#         'Age': [25, 30, 35, 40],
#         'Salary': [50000, 60000, 70000, 80000]}
# df = pd.DataFrame(data)
# 按照 "Age" 列的值进行降序排序
# df_sorted = df.sort_values(by='Age', ascending=False)
# print(df_sorted)
# print(df["Age"].sum())   # 统计函数




data = {'Department': ['HR', 'Finance', 'HR', 'IT', 'IT'],
        'Employee': ['Alice', 'Bob', 'Charlie', 'David', 'Eve'],
        'Salary': [50000, 60000, 55000, 70000, 75000]}
df = pd.DataFrame(data)
# grouped = df.groupby('Department')['Salary'].mean()  # 按照部门分组，并计算每个部门的平均薪资
# print(grouped)
# grouped_multiple = df.groupby('Department').agg({'Salary': ['mean', 'sum']})   # 按照部门分组，并计算每个部门的平均薪资和总共薪资
# print(grouped_multiple)
# grouped_sorted = df.groupby('Department').apply(lambda x: x.sort_values(by='Salary', ascending=False))  # 按照部门分组后，按薪资降序排序
# print(grouped_sorted)
# pivot_table = df.pivot_table(values='Salary', index='Department', aggfunc='mean')   # 透视表，使用pivot_table计算每个部门的薪资平均值
# print(pivot_table)
# pivot_table.to_excel('Salary.xlsx', sheet_name='Sheet1', index=True,freeze_panes=(1,0))


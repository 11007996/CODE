import requests

'''
url = "https://api.github.com/search/repositories?q=lanueage:python&sorts=stars"
r = requests.get(url)  # GET请求
print(r.status_code)   # 响应代码
response_dict = r.json()   # 返回JSON
print(response_dict.keys())   # JSON键
print(response_dict["incomplete_results"])   # JSON键值
'''

'''
url = "https://api.github.com/rate_limit"
r = requests.get(url)  # GET请求
print(r.status_code)   # 响应代码
response_dict = r.json()   # 返回JSON
print(response_dict["resources"]["code_search"]["limit"])  # JSON键值
print(response_dict["rate"]["resource"])  # JSON键值
'''


# url = "https://hacker-news.firebaseio.com/v0/item/9884165.json"
# r = requests.get(url)
# response_dict = r.json()
# print(response_dict["kids"][1])
# print(r.text)    # 返回HTML页面
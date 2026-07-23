import numpy as np
from scipy.sparse.csgraph import connected_components
from scipy.sparse import csr_matrix




'''
# 邻接矩阵（Adjacency Matrix）是表示顶点之间相邻关系的矩阵。
# 邻接矩阵逻辑结构分为两部分：V 和 E 集合，其中，V 是顶点，E 是边，边有时会有权重，表示节点之间的连接强度
arr = np.array([
  [0, 1, 2],
  [1, 0, 0],
  [2, 0, 0]
])
newarr = csr_matrix(arr)
print(connected_components(newarr))
'''










# Dijkstra(迪杰斯特拉)最短路径算法，用于计算一个节点到其他所有节点的最短路径。
# Scipy 使用 dijkstra() 方法来计算一个元素到其他元素的最短路径。
# dijkstra() 方法可以设置以下几个参数：
# return_predecessors: 布尔值，设置 True，遍历所有路径，如果不想遍历所有路径可以设置为 False。
# indices: 元素的索引，返回该元素的所有路径。
# limit: 路径的最大权重。
'''
import numpy as np
from scipy.sparse.csgraph import dijkstra
from scipy.sparse import csr_matrix
arr = np.array([
  [0, 1, 2],
  [1, 0, 0],
  [2, 0, 0]
])
newarr = csr_matrix(arr)
print(dijkstra(newarr, return_predecessors=True, indices=0))
'''








# 弗洛伊德算法算法是解决任意两点间的最短路径的一种算法。
# Scipy 使用 floyd_warshall() 方法来查找所有元素对之间的最短路径。
'''
import numpy as np
from scipy.sparse.csgraph import floyd_warshall
from scipy.sparse import csr_matrix
arr = np.array([
  [0, 1, 2],
  [1, 0, 0],
  [2, 0, 0]
])
newarr = csr_matrix(arr)
print(floyd_warshall(newarr, return_predecessors=True))
'''





# 贝尔曼-福特算法是解决任意两点间的最短路径的一种算法。
# Scipy 使用 bellman_ford() 方法来查找所有元素对之间的最短路径，通常可以在任何图中使用，包括有向图、带负权边的图。
'''
import numpy as np
from scipy.sparse.csgraph import bellman_ford
from scipy.sparse import csr_matrix
arr = np.array([
  [0, -1, 2],
  [1, 0, 0],
  [2, 0, 0]
])
newarr = csr_matrix(arr)
print(bellman_ford(newarr, return_predecessors=True, indices=0))
'''





# 遍历方法
# depth_first_order() 方法从一个节点返回深度优先遍历的顺序。
'''
import numpy as np
from scipy.sparse.csgraph import depth_first_order
from scipy.sparse import csr_matrix
arr = np.array([
  [0, 1, 0, 1],
  [1, 1, 1, 1],
  [2, 1, 1, 0],
  [0, 1, 0, 1]
])
newarr = csr_matrix(arr)
print(depth_first_order(newarr, 1))
'''




# breadth_first_order() 方法从一个节点返回广度优先遍历的顺序。
'''
import numpy as np
from scipy.sparse.csgraph import breadth_first_order
from scipy.sparse import csr_matrix
arr = np.array([
  [0, 1, 0, 1],
  [1, 1, 1, 1],
  [2, 1, 1, 0],
  [0, 1, 0, 1]
])
newarr = csr_matrix(arr)
print(breadth_first_order(newarr, 1))
'''





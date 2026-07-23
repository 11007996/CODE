# 一维插值
# 一维数据的插值运算可以通过方法 interp1d() 完成。
# 该方法接收两个参数 x 点和 y 点。
# 返回值是可调用函数，该函数可以用新的 x 调用并返回相应的 y，y = f(x)。
'''
from scipy.interpolate import interp1d
import numpy as np
xs = np.arange(10)
ys = 2*xs + 1
interp_func = interp1d(xs, ys)
newarr = interp_func(np.arange(2.1, 3, 0.1))
print(newarr)
'''




'''
单变量插值
在一维插值中，点是针对单个曲线拟合的，而在样条插值中，点是针对使用多项式分段定义的函数拟合的。
单变量插值使用 UnivariateSpline() 函数，该函数接受 xs 和 ys 并生成一个可调用函数，该函数可以用新的 xs 调用。
分段函数，就是对于自变量 x 的不同的取值范围，有着不同的解析式的函数。
'''
'''
from scipy.interpolate import UnivariateSpline
import numpy as np
xs = np.arange(10)
ys = xs**2 + np.sin(xs) + 1
interp_func = UnivariateSpline(xs, ys)
newarr = interp_func(np.arange(2.1, 3, 0.1))
print(newarr)
'''



'''
径向基函数插值
径向基函数是对应于固定参考点定义的函数。
曲面插值里我们一般使用径向基函数插值。
Rbf() 函数接受 xs 和 ys 作为参数，并生成一个可调用函数，该函数可以用新的 xs 调用。
'''
'''
from scipy.interpolate import Rbf
import numpy as np
xs = np.arange(10)
ys = xs**2 + np.sin(xs) + 1
interp_func = Rbf(xs, ys)
newarr = interp_func(np.arange(2.1, 3, 0.1))
print(newarr)
'''

'''
import importModule   #导入整个模块
importModule.printH()
importModule.printW()
'''
'''
import importModule as im   #给导入的模块取个别名
im.printH()
im.printW()
'''


'''
from importModule import printH,printW   #导入模块的某个方法
printH()
printW()
'''

'''
from importModule import printH as H,printW as W  #给方法取个别名
H()
W()
'''

'''
from importModule import *   #导入模块中的所有方法
printH()
printW()
'''

'''
# from importModule import Dog,Dog_a   #导入模块中的类
from importModule import *   #导入模块中的所有类和方法
myDog = Dog("div",12)
myDog.show()
myDog_a = Dog_a("div",12)  #子类
myDog_a.show()
printH()   #方法
'''

'''
import importModule   #导入整个模块
myDog = importModule.Dog("div",12)
myDog.show()
myDog_a = importModule.Dog_a("div",12)  #子类
myDog_a.show()
importModule.printH()
'''


import sys   #使用sys模块中的path功能,动态地添加搜索路径
# sys.path.append('D:\\zzzzzzzz\\CodeBlock\\python\\python编程_从入门到实践\\Module')    #绝对路径
sys.path.append('.\\Module')   #相对路径
import importModule
myDog = importModule.Dog("div",12)
myDog.show()
myDog_a = importModule.Dog_a("div",12)  #子类
myDog_a.show()
importModule.printH(12)

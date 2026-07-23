# 测试方法
'''
import unittest   #导入测试模块
from importModule import printH    #导入需测试的方法
class TestDemoFun(unittest.TestCase):   #必须继承测试类
	def test_firstFun_0(self):    #测试方法，必须以test开头
		testfunResult = printH("hello")    #执行导入的方法
		self.assertEqual(testfunResult,"hello")   #断言方法，判断导入的结果是否与预期的结果一致

	def test_firstFun_1(self):
		testfunResult = printH("world")  #测试同一个方法但参数不同
		self.assertEqual(testfunResult,"hello")
unittest.main()   #运行单元测试,自动运行所以test开头的方法
'''

# 断言方法
'''
self.assertEqual(a,b)        #核实a==b
self.assertNotEqual(a,b)     #核实a!=b
self.assertTrue(x)            #核实ax为Ture
self.assertFalse(x)           #核实ax为False
self.assertIn(item,list)     #核实item在list中
self.assertNotIn(item,list)  #核实item不在list中
'''

# 测试通过打印.，测试错误打印E，断言失败打印F



# 测试类
import unittest
from importModule import Dog_a
class TestDog(unittest.TestCase):
	def setUp(self):   #setUp方法为TestCase自带的方法，在此方法中创建的对象可以在整个测试类中调用，类似于测试类的构造函数，在运行test方法前会先运行此方法
		self.myDog = Dog_a("div",12)  #实例化子类

	def test_DogClass_1(self):
		self.myDog.show()   #调用setUp方法创建的myDog类
		self.assertEqual(None,None)

	def test_DogClass_2(self):
		myHost = self.myDog.getBool()
		self.assertTrue(myHost)
unittest.main()

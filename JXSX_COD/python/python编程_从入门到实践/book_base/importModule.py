def printH(a):
  print("Hello")
  return a

def printW():
  print("World!")



class Dog():   #定义类
  def __init__(self,name,age):   #构造函数，self参数为必须
    self.name=name
    self.age=age
    self.type = "dog"   #构造函数默认值，调用构造函数时可不传参

  def show(self):    #方法，访问自身属性时必须要有self参数
    print(self.name + "-" + str(self.age))

class Dog_a(Dog):   #继承Dog类
  def __init__(self,name,age):  #子类的构造函数
    super().__init__(name,age)   #必须先初始化父类
    self.host = "roundom"  #子类的属性
  def printf(self):   #子类的方法
    print(self.host)
  def show(self):    #重写父类方法
    print("hello world")
  def getBool(self):
    return True

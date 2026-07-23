const float * pf;  //指向的对象不可修改，但指向的地址可修改
float * const pf;  //指向的地址不可修改，但指向的对象值可修改
const float * const pf;   //指向的对象和指向的地址都不可修改
volatile int locl;   //表示易变数据，告知计算机随时可改变变量的值，数据不放入寄存器调整缓存中，涉及到编译器的优化
int * restrict restar = (int *)maloc(10 * sizeof(int))   //restrict只能用于指针，表明该指针是访问数据对象的唯一且初始的方式，其它方式不可修改数据对象，用于编译器优化
double stick(double ar[static 20])  //函数调用中的实际参数应该是一个数组首元素的指针，且该数组至少有20个元素
_Atomic int hogs;atomic_store(&hogs,12)    //原子类型，当一个线程对一个原子类型的对象执行原子操作时，其他线程不能访问该对象
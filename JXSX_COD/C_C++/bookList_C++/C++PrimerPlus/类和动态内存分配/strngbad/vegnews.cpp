#include <iostream>
#include "strngbad.h"
using std::cout;
void callme1(StringBad &);
void callme2(StringBad);
int main()
{
	using std::endl;
	{
		cout << "Starting an inner block.\n";
		StringBad headline1("Celery Stalks at Midnight");
		StringBad headline2("Lettrce");
		StringBad sports("Spinach");

		cout << "headline1:" << headline1 << endl;
		cout << "headline2:" << headline2 << endl;
		cout << "sports:" << sports << endl;
		callme1(headline1);
		cout << "headline1:" << headline1 << endl;

		callme2(headline2);
		cout << "headline2:" << headline2 << endl;

		StringBad sailor = sports;
		cout << "sailor:" << sailor << endl;
		StringBad knot;
		knot = headline1;
		cout << "knot:" << knot << endl;
	}
	cout << "end of main()\n";
	std::cin.get();
	return 0;
}


void callme1(StringBad & rsb)
{
	cout << "String passed by reference:\n";
	cout << " \"" << rsb << "\"\n";
}
void callme2(StringBad sb)
{
	cout << "String passedn by value:\n";
	cout << " \"" << sb << "\"\n";
}


/*特殊成员函数
1.默认构造函数
2.默认析构函数
3.复制构造函数
4.赋值运算符
5.地址运算符

每当程序生成对象副本时，编译器都将使用复制构造函数，具体讲，当函数按值传递对象或函数返回对象时，都将使用生抽构造函数
编译器先生成一个临时对象，再将临时对象赋值给新创建的对象
由于按值传递对象将调用复制构造函数，因此应该按引用传递对象。这样可以节省调用构造函数的时间及存储新对象的空间


如果方法或函数要返回局部对象，则应返回对象，而不是指向对象的引用；如果方法或函数返回一个没有公有复制构造函数的类的对象，它必须返回一个指向这种对象的引用
有些方法和函数可以返回对象，也可以返回指向对象的引用，在这种情况下，应首先引用，因为其效率更高*/
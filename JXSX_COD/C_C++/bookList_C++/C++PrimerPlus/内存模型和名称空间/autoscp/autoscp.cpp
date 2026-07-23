#include <iostream>
void oil(int x);
int main()
{
	using namespace std;
	int texas = 31;
	int year = 2011;
	cout << "In main(),year = " << texas << ", &texas = ";
	cout << &texas << endl;
	cout << "In main(),year = " << year << ",&year = ";
	cout << &year << endl;
	oil(texas);
	cout << "In main(),texas = " << texas << ", &texas = ";
	cout << &texas << endl;
	cout << "In main(),year = " << year << ",&tear = ";
	cout << &year << endl;
	return 0;
}
void oil(int x)
{
	using namespace std;
	int texas =5;
	cout << "In oil(),texas = " << texas << ", &texas = ";
	cout << &texas << endl;
	cout << "In oil(),x = " << ", &x = ";
	cout << &x <<endl;
	{
		int texas = 113;
		cout << "In block, texas = " << endl;
		cout << ", &texas = " << &texas << endl;   //局部作用域，代码块内暂时隐藏函数定义的texas变量
		cout << "In block ,x = " << x << ",&x = ";
		cout << &x << endl;
	}
	cout << "Post-bloc texas = " << endl;
	cout << ",&texas = " << &texas << endl;
}
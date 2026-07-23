#include <iostream>
#include <stdio.h>
// using namespace std;
using std::cout;   //只使用命名空间的cout对象
using std::endl;
void show(char);
void show(char str,int n);
int main(void)
{
	cout << "Come up and C++ me some time.";   //cout C++输出工具
	cout << endl;
	cout << "You won't regret it!" << endl;
	std::cout << "namespace std" << std::endl;   //命名空间全称，加命名空间前缀可以不写using编译指令
	printf("%s\n", "C++ start");   //C输出函数
	show('h',5);
	// cin.get();
	return 0;
}


void show(char str)
{
	double a=3.2,b=5.2,c=7.2;
	a = b = c = 4.3;
	cout << a << b << c << endl;
	char p[9] = " zhizhen";
	cout << str << p << " END!" << endl;
}

void show(char str,int n)   //函数重载
{
	for(int i=0;i<n;i++)
		cout << str << endl;
}
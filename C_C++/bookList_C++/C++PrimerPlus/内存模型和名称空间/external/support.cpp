#include <iostream>
#include "e.h"
extern double warming;   //外部变量
// void update(double dt);
// void local();
// using std::cout;
using namespace std;
void update(double dt)
{
	extern double warming;   //外部变量，不分配内存
	warming += dt;
	cout << "Updating global warming to " << warming;
	cout << " degrees.\n";
}

void local()
{
	double warming = 0.8;   //块存储期，无链接
	cout << "Local warming = " << warming << " degrees.\n";
	cout << "But global warming = " << ::warming;   //::表示warming使用全局变量版本，有别于块存储期的warming局部变量
	cout << " degrees.\n";
}
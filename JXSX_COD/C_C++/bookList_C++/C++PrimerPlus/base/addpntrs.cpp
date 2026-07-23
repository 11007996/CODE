#include <iostream>
int main()
{
	using namespace std;
	int a[5] = {1,3,5,7,9};
	int *p = &a[0];
	int *t = a;
	cout << "p:" << p << endl;
	cout << "t:" << t << endl;

	cout << "-------------------------------------\n";

	cout << "*p:" << *p << endl;
	cout << "p[0]:" << p[0] << endl;
	cout << "*(p +1):"<< *(p+1) << endl;
	cout << "p[1]:" << p[1] << endl;
	cout << "*(a+1):" << *(a+1) << endl;

	cout << "*&a:" << **(&a) << endl;
	cout << "*&a:" << &a << endl;   //整个数组的地址
	cout << "*&a:" << *(&a) << endl;   //数组第一个元素的地址

	return 0;

}
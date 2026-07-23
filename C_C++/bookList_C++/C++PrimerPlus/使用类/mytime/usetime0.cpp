#include <iostream>
#include "mytime0.h"
int main()
{
	using std::cout;
	using std::endl;
	Time planning;
	Time coding(2,40);
	Time fixing(5,55);
	Time total;

	cout << "planning time = ";
	planning.Show();
	cout << endl;

	cout << "coding time =";
	coding.Show();
	cout << endl;

	cout << "fixing time =";
	fixing.Show();
	cout << endl;

	// total = coding.Sum(fixing);
	total = coding.operator+(fixing);    //运算符作为函数名
	// total = coding+fixing;   //运算符重载
	cout << "coding.Sum(fixing) =";
	total.Show();
	cout << endl;

	std::cin.get();
	return 0;
}
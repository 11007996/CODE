#include <iostream>
#include "mytime3.h"
int main()
{
	using std::cout;
	using std::endl;
	Time aida(3,35);   //构造函数
	Time tosca(2,48);
	Time temp;

	cout << "Aida and Tosca:\n";
	cout << aida<<";" << tosca << endl;
	temp = aida + tosca;
	temp = aida * 1.17;
	//<<运算符重载
	cout << "Aida * 1.17:" << temp << endl;
	cout << "10.0 * Tosca: " << 10.0 * tosca << endl;

	std::cin.get();
	return 0;
}
#include <iostream>
#include <string>
#include "brass.h"
const int CLINETS =4;
int main()
{
	using std::cin;
	using std::cout;
	using std::endl;

	Brass * p_clients[CLINETS];   //创建一个基类指针数组，使它既可以指向基类，又可以指向派生类，派生类可能隐式转换为基类，多态性
	std::string temp;
	long tempnum;
	double tembal;
	char kind;

	for(int i =0;i<CLINETS;i++)
	{
		cout << "Enter client's name: ";
		getline(cin,temp);
		cout << "Enter client's account number: ";
		cin >> tempnum;
		cout << "Enter 1 for Brass Account or "
		     << "2 for BrassPlus Account: ";
		while(cin >> kind && (kind != '1' && kind != '2'))
			cout << "Enter eighter 1 or 2:";
		if(kind == '1')
			p_clients[i] = new Brass(temp,tempnum,tembal);  //指针指向一个基类对象
		else
		{
			double tmax,trate;
			cout << "Enter the interest rate "
				 << "as a decimal fraction: ";
			cin >> trate;
			p_clients[i] = new BrassPlus(temp,tempnum,tembal,tmax,trate);   //指针指向一个派生类对象
		}
		while(cin.get() != '\n')
			continue;
	}
	cout << endl;
	for(int i = 0;i< CLINETS;i++)
	{
		p_clients[i]->ViewAcct();   //通过指针调用类的虚方法
		cout << endl;
	}
	for(int i =0;i<CLINETS;i++)
	{
		delete p_clients[i];   //释放类对象动态存储空间，调用类相应的虚析构函数，因此有虚方法时注意创建虚析构函数
	}
	cout << "Done.\n";
	return 0;
}	



/*
因为函数重载，编译器无法通过函数名就确定执行哪个代码块，必须查看函数参数以及函数名才能确定使用哪个函数，，这个过程在编译阶段就可以完成，，称为静态联编
而虚方法不能在编译时确定使用哪个代码块，必须在程序运行时选择正确的虚方法的代码，这称为动态联编
将派生类引用或指针转换为基类引用或指针被称为向上强制转换，这使公有继承不需要进行显式类型转换；相反的过程，将基类指针或引用转换为派生类指针或引用称为向下强制转换，如果不使用显式类型转换，则向下强制转移是不允许的，原因是is-a关系通常是不可逆的
按值传递导致只将派生类的基类部分传递给公有基类函数
隐式向上强制转换使基类指针或引用可以指向基类对象或派生类对象，因此需要动态联编

动态联编会在每个对象中增加一个虚函数地址表，辅助程序在运行时确定使用哪个函数代码块，因此比静态联编执行效率较低

通常应给基类提供一个虚析构函数，即使它并不需要析构函数

派生类中应重新定义基类中的所有虚函数，否则将隐藏基类的虚函数

如果重新定义继承的方法，应确保与原来的原型完全相同，但如果返回类型是基类引用或指针，则可以修改为指向派生类的引用或指针，这种特性被称为返回类型协烃，因为允许返回类型随类型匠变化而变化
如果基类声明被重载，则应在派生类中重新定义所有的基类版本，如果派生类不需要修改基类的代码逻辑，也就应该在新定义中显式调用基类版本
*/
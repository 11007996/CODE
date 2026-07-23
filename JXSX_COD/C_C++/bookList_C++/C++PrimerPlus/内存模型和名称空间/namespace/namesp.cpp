#include <iostream>
#include "namesp.h"
namespace pers   //和头文件中同一个名称空间，名称空间是开放的，可以随时添加
{
	using std::cout;   //using声明，相当于声明了一个变量
	using std::cin;
	void getPerson(Person & rp)   //函数原型在名称空间的另一处
	{
		cout << "Enter first name: ";
		cin >> rp.fname;
		cout << "Enter last name: ";
		cin >> rp.lname;
	}

	void showPerson(const Person & rp)
	{
		std::cout << rp.lname << ", " << rp.fname;    //加名称空间使用cout
	}
}

namespace debts
{
	void getDebt(Debt & rd)
	{
		getPerson(rd.name);   //已在名称空间中用using编译指令添加了pers名称空间
		std::cout << "Enter debt: ";
		std::cin >> rd.amount;
	}
	void showDebt(const Debt & rd)
	{
		showPerson(rd.name);
		std::cout << ": $" << rd.amount << std::endl;
	}
	double sumDebts(const Debt ar[],int n)
	{
		double total =0;
		for(int i =0;i<n;i++)
		{
			total += ar[i].amount;
		}
		return total;
	}
}
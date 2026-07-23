#include<iostream>
namespace pers
{
	struct Person
	{
		std::string fname;
		std::string lname;
	};
	void getPerson(Person &);  //函数原型在名称空间中另一处
	void showPerson(const Person &);
}
namespace debts
{
	using namespace pers;  //using编译指令，使debts空间中可以使用pers空间中的Person结构
	struct Debt
	{
		Person name;
		double amount;
	};
	void getDebt(Debt &);
	void showDebt(const Debt &);
	double sumDebts(const Debt ar[],int n);
}
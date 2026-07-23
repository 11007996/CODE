#include <iostream>
#include "brass.h"
int main()
{
	using std::cout;
	using std::endl;

	//构造函数实例化基类对象
	Brass Piggy("Porcelot Pigg",381299,4000.00);
	//构造函数实例派生类对象
	BrassPlus Hoggy("Horatio Hogg",382288,3000.00);
	Piggy.ViewAcct();   //调用基类虚函数
	cout << endl;
	Hoggy.ViewAcct();    //调用派生类虚函数
	cout << endl;
	cout << "Depositing $1000 into the Hoggy Account:\n";
	Hoggy.Deposit(1000.00);   //调用基类公有函数
	cout << "New balance:$" << Hoggy.Balance() << endl;   //调用基类公有函数
	cout << "Withdraw $4200 from the Pigg Account:\n";
	Piggy.Withdraw(4200.00);   //调用基类虚函数
	cout << "Pigg account balance: $" << Piggy.Balance() << endl;
	cout << "Withdraw $4200 from the Hogg Account:\n";
	Hoggy.Withdraw(4200.00);
	Hoggy.ViewAcct();

	system("pause");
	return 0;
}
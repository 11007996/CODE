#include <iostream>
#include <string>
#include "acctabc.h"
const int CLINETS = 4;
int main()
{
	using std::cin;
	using std::cout;
	using std::endl;

	// 基类指针数组
	AcctABC * p_clients[CLINETS];
	std::string temp;
	long tempnum;
	double tempbal;
	char kind;

	for(int i =0;i<CLINETS;i++)
	{
		cout << "Enter client's name: ";
		getline(cin,temp);
		cout << "Enter client's account number: ";
		cin >> tempnum;
		cout << "Enter opening balance: $";
		cin >> tempbal;
		cout << "Enter 1 for Brass Account or "
			 << "2 for BrassPlus Account:";
			 while(cin >> kind && (kind != '1' && kind != '2'))
			 	cout << "Enter either 1 or 2: ";
			 if(kind == '1')
			 	//派生类赋值给基类数组，协变
			 	p_clients[i] = new Brass(temp,tempnum,tempbal);
			 else
			 {
			 	double tmax,trate;
			 	cout << "Enter the overdraft limit: $";
			 	cin >> tmax;
			 	cout << "Enter the interest rate "
			 		 << "as a decimal fraction: ";
			 		 cin >> trate;
			 		 //派生类赋值给基类数组，协变
			 		 p_clients[i] = new BrassPlus(temp,tempnum,tempbal,tmax,trate);
			 }
			 while(cin.get() != '\n')
			 	continue;
	}
	cout << endl;
	for(int i =0;i<CLINETS;i++)
	{
		//指针调用虚函数，动态联编
		p_clients[i]->ViewAcct();
		cout << endl;
	}
	for(int i = 0;i<CLINETS;i++)
	{
		//循环删除new创建的对象
		delete p_clients[i];
	}
	cout << "Done.\n";
	return 0;
}
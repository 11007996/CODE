#include <iostream>
#include "namesp.h"
void other(void);
void another(void);
int main(void)
{
	using debts::Debt;  //在代码块内声明debts名称空间结构中的Debt结构，只在代码块中有效

	using debts::showDebt;
	Debt golf = {{"Benny","Goatsniff"},120.0};    //初始化结构
	showDebt(golf);
	other();
	another();
	std::cin.get();
	return 0;
}
void other(void)
{
	using std::cout;
	using std::endl;
	using namespace debts;    //在代码块中将整个名称空间添加
	Person dg = {"Doodles","Glister"};
	showPerson(dg);
	cout << endl;
	Debt zippy[3];   //结构数组
	int i;
	for(i=0;i<3;i++)
		showDebt(zippy[i]);
	cout << "Total debt: $" << sumDebts(zippy,3) << endl;
	return;
}
void another(void)
{
	using pers::Person;   //只添加了Person结构体
	Person collector = {"Milo","Rigthshift"};
	pers::showPerson(collector);    //使用pers名称空间中的showPerson函数
	std::cout << std::endl;
}
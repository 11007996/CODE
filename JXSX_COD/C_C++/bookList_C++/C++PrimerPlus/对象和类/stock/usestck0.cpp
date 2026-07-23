#include <iostream>
#include "stock00.h"
int main()
{
	{
		using std::cout;
		cout << "Using constructors to create new objects\n";
		Stock stock1("NanoSmart", 12,20.0);   //调用自定义构造函数
		stock1.show();
		Stock stock2 = Stock("Boffo Objects",2,2.0);
		stock2.show();
	
		cout << "Assigning stock1 to stock2:\n";
		stock2 = stock1;
		cout << "Listing stock1 and stock2:\n";
		stock1.show();
		stock2.show();

		cout << "Using a constructors to reset an object\n";
		stock1 = Stock("Nifty Foods",10,50.0);    //并非初始化，先创建一个临时对象，然后将临时对象的值赋予stock1，再调用析构函数删除临时对象
		cout << "Revised stock1:\n";
		stock1.show();
		cout << "Done\n";
	}    //退出代码块调用 stock1 stock2的析构函数

std::cin.get();
return 0;
}
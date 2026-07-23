#include<iostream>
#include "stonewt.h"
using std::cout;
using std::endl;
void display(const Stonewt & st,int n);

int main()
{
	Stonewt incognito = 275;
	Stonewt wolfe(285.7);
	Stonewt taft(21,8);

	cout << "The celebrity weighted ";
	incognito.show_stn();
	cout << "The detective weighted ";
	wolfe.show_stn();
	cout << "The President weighted ";
	taft.show_lbs();
	incognito = 276.8;
	taft = 325.6;
	cout << "After dinner, the celebrity weighted ";
	incognito.show_stn();
	cout << "After dinner, the President weighted ";
	taft.show_lbs();
	display(taft,2);
	cout << "The wrestler weighted even more.\n";
	display(422,2);   //将422隐式转换为Stonewt类,调用Stonewt(double lbs)构造函数
	cout << "No stone left unearned\n";

	cout << "operation int():" << int (taft) << endl;
	cout << "operation double():" << double (taft) << endl; //转换函数
	cout << "get_int():" << taft.get_int(taft) << endl;  //普通函数

	system("pause");
	return 0;
}

void display(const Stonewt & st,int n)
{
	for(int i =0;i<n;i++)
	{
		cout << "Wow! ";
		st.show_stn();
	}
}
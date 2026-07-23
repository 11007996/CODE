#include<iostream>
//自定义头文件用引号，内置关文件用尖括号
#include "coordin.h"   //自建头文件，在函数定义文件和调用函数文件中都要引用
using namespace std;
int main()
{
	rect rplace;
	polar pplace;

	cout << "Enter the x and y values: ";
	while(cin >> rplace.x >> rplace.y)
	{
		pplace = rect_to_polar(rplace);
		show_polar(pplace);
		cout << "Next two numbers (q to quti): ";
	}

	cout << "Bye!\n";
	return 0;
}
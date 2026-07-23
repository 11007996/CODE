#include <iostream>
int main()
{
	using namespace std;
	int auks,bats,coots;

	auks = 19.99 + 11.99;

	bats = (int)19.99 + (int)11.99;   //强制转移为整型
	coots = int(19.99) + int(11.99);
	cout << "auks = " << auks <<".bats = " << bats;
	cout << ".coots = " << coots << endl;

	char ch = 'z';
	cout << "The code for " << ch << " is ";
	cout << int(ch) << endl;
	cout << "Yes,the code is ";
	cout << static_cast<int>(ch) << endl;  //预定义强制转换数值类型
	return 0;
}
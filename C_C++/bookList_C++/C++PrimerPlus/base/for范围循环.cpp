#include <iostream>
int main()
{
	using namespace std;
	double prices[5] = {4.99,10.99,6.87,3.14,5.56};
	for(double x : prices)   //for范围循环
	{
		cout << x << endl;
	}

	for(double &x : prices)   //&表示x是一个引用变量
	{
		x = x * 10;    //修改数组的值
	}
	return 0;
}
#include <iostream>
#include "arraytp.h"
int main(void)
{
	using std::cout;
	using std::endl;
	ArrayTP<int,10> sums;   //初始化模板类
	ArrayTP<double,10> aves;
	ArrayTP<ArrayTP<int,5>,10> twodee;   //初始化递归模板类

	int i,j;

	for(i =0;i<10;i++)
	{
		sums[i] = 0;
		for(j = 0;j<5;j++)子田
		{
			twodee[i][j] = (i + 1) * (j + 1);
			sums[i] += twodee[i][j];
		}
		aves[i] = (double)sums[i]/10;
	}

	for(i = 0;i<10;i++)
	{
		for(j=0;j<5;j++)
		{
			cout.width(2);  //设置输出字符宽度
			cout << twodee[i][j] << ' ';
		}
		cout << ":sum = ";
		cout.width(3);
		cout << sums[i] << ",average = " << aves[i] << endl;
	}
	cout << "Done\n";
	std::cin.get();
	return 0;
}
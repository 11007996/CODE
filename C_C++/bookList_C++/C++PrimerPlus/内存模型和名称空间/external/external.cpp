#include <iostream>
#include "e.h"

double warming = 0.3;   //文件存储期，外部链接
// void update(double dt);
// void local();

int main()
{
	using namespace std;
	cout << "Clobal warming is " << warming << " degrees.\n";
	update(0.1);  //外部函数
	cout << "Global warming is " << warming << " degrees.\n";
	local();
	cout << "Global warming is " << warming << " degrees.\n";
	cin.get();
	return 0;
}
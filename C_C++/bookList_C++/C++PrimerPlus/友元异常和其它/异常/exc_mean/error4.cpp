#include <iostream>
#include <cmath>
#include "exc_mean.h"
double hmean(double a,double b);
double gmean(double a,double b);
int main()
{
	using std::cout;
	using std::cin;
	using std::endl;
	double x,y,z;
	cout << "Enter two numbers: ";
	while(cin >> x >> y)
	{
		try
		{
			z = hmean(x,y);
			cout << "Harmonic mean of " << x << " and " << y << " is " << z << endl;
			cout << "Geometric mean of " << x << " and " << y << " is " << gmean(x,y) << endl;
			cout << "Enter next set o fnumbers <q to quie>:";
		}
		catch(bad_hmean & bg)   //两个catch语句，分别匹配不同的异常类型
		{
			bg.mesg();
			cout << "try again.\n";
			continue;
		}
		catch(bad_gmean & bg)
		{
			cout << bg.mesg();
			cout << "values used: " << bg.v1 << ", " << bg.v2 << endl;
			cout << "Sorry,you don't get to play any more.\n";
			break;
		}
	}
	cout << "Bye!\n";
	system("pause");
	return 0;
}

double hmean(double a,double b)
{
	if(a==-b)
		throw bad_hmean(a,b);    //引发异常，返回一个对象，构造函数
	return 2.0 * a * b / (a + b);
}

double gmean(double a,double b)
{
	if(a < 0 || b < 0)
		throw bad_gmean(a,b);
	return std::sqrt(a*b);
}
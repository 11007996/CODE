
//abort函数终止程序运行
/*#include <iostream>
#include <cstdlib>
double hmean(double a,double b);
int main()
{
	double x,y,z;
	std::cout << "Enter two numbers: ";
	while(std::cin >> x >> y)
	{
		z = hmean(x,y);
		std::cout << "Harmonic mean of " << x << " and " << y << " is " << z << std::endl;
		std::cout << "Enter next set of numbers <q to quit>: ";
	}
	std::cout << "Bye!\n";
	system("pause");
	return 0;
}


double hmean(double a,double b)
{
	if(a == -b)
	{
		std::cout << "untenable arguments to hmead()\n";
		std::abort();    //终止程序，不返回到main函数
	}
	return 2.0 * a * b / (a + b);
}*/



/*#include <iostream>
#include <cfloat>
bool hmean(double a,double b,double * ans);
int main()
{
	double x,y,z;
	std::cout << "Enter two numbers: ";
	while(std::cin >> x >> y)
	{
		if(hmean(x,y,&z))
			std::cout << "Harmonic mean of " << x << " and " << y << " is " << z << std::endl;
		else
			std::cout << "One value should not be the negative "
					  << "of the other - try again.\n";
		std::cout << "Enter next set of numbers <q to quit>: ";
	}
	std::cout << "Bye!\n";
	return 0;
}

bool hmean(double a,double b,double * ans)
{
	if(a == -b)
	{
		*ans = DBL_MAX;
		return false;
	}
	else
	{
		*ans = 2.0 * a * b / (a + b);
		return true;
	}
}*/


#include <iostream>
double hmean(double a,double b);
int main()
{
	double x,y,z;
	std::cout << "Enter two numbers: ";
	while(std::cin >> x >> y)
	{
		try  //可能发生异常的代码块
		{
			z = hmean(x,y);
		}
		catch(const char * s)    //捕捉异常，s就是throw设置的异常类型
		{
			std::cout << s << std::endl;
			std::cout << "Enter a new pair of numbers: ";
			continue;
		}
		std::cout << "Harmonic mean of " << x << " and " << y << " is " << z << std::endl;
		std::cout << "Enter next set of numbers <1 to quir>: ";
	}
	std::cout << "Bye!\n";
	return 0;
}

double hmean(double a,double b)
{
	if(a==-b)
		throw "bad hmean() arguments: a = -b not allowed";   //引发异常，异常类型，可以是字符串，也可以是其它数据类型
	return 2.0 * a * b / (a+b);
}
#include<iostream>

//内联函数,将函数代码直接插入调用函数处,与C语言的宏不同,宏是以文本替换的形式运行,因此内联函数比宏效率更高,在C++中都就将宏写成内联函数
inline double square(double x) {return x*x;} 

int main()
{
	using namespace std;
	double a,b;
	double c= 13.0;
	a = square(5.0);
	b = square(4.5+7.5);
	cout << "a= " << a << ",b= " << b << "\n";
	cout << "c= " << c;
	cout << ", c square = " << square(c++) << "\n";
	cout << "Now c= "<< c << "\n";
	return 0;
}
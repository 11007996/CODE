#include<iostream>
// template <class T>   //c++老版本模板版原型
template <typename T>   //模板原型,注意模板类型可能不支持函数某些操作
void Swap(T &a,T &b);   //函数原型

template <typename T>  //每个函数都要新建一个模板
void sum(const T &a,const T &b);

template <typename T,typename 	Q>  //每个函数都要新建一个模板
void sum(const T &a,const Q &b);   //模板函数重载

using namespace std;
int main()
{
	
	int i = 10;
	int j = 20;
	cout << "i,j = " << i << ", " << j << endl;
	cout << "Using compiler-generated int swapper:\n";
	Swap<int>(i,j);   //<int>可以不写,编译器会自动从参数列表中判断参数
	cout << "Now i,j = " << i << ", "<< j << ",\n";

	double x = 24.5;
	double y = 81.7;
	cout << "x,y = " << i << ", " << j << ",\n";
	cout << "Using compiler-generated double swapper:\n";
	Swap<double>(x,y);
	cout << "Now x,y = " << x << ", "<< y << ",\n";

	sum<int>(2,3);   //显式实例化
	sum(2.2,3.3);   //隐式实例化
	sum<int,double>(2,2.3);
	return 0;
}

template <typename T>  //模板声明
void Swap(T &a,T &b)   //函数声明
{
	T temp;
	temp = a;
	a = b;
	b = temp;
}

template <typename T>
void sum(const T &a,const T &b)
{
	T tem;
	tem = a+b;
	cout << "tem: = " << tem << endl;
}



template <typename T>  //每个函数都要新建一个模板
void sum(const T &a,const T &b,const T &n)    //模板函数重载
{
	T tem;
	tem = a+b+n;
	cout << "tem: = " << tem << endl;
}


template <typename T,typename Q>  //每个函数都要新建一个模板
void sum(const T &a,const Q &b)    //模板函数重载
{
	Q tem;
	tem = a+b;
	cout << "tem: = " << tem << endl;
}
#ifndef ARRAYTP_H_
#define ARRAYTP_H_

#include <iostream>
#include <cstdlib>

//模板类
template<class T,int n>
class ArrayTP
{
private:
	T ar[n];
public:
	ArrayTP(){};
	explicit ArrayTP(const T & v);
	virtual T & operator[](int i);   //虚函数重载运算符
	virtual T operator[](int i) const;	
};

// 模板函数 构造函数
template <class T,int n>
ArrayTP<T,n>::ArrayTP(const T & v)
{
	for(int i = 0;i< n;i++)
		ar[i] = v;
}

// 模板函数 运算符重载
template <class T,int n>
T & ArrayTP<T,n>::operator[](int i)
{
	if(i < 0 || i >= n)
	{
		std::cerr << "Error in array limits: " << i
				  << " is out of range\n";
		std::exit(EXIT_FAILURE);
	}
	return ar[i];
}

// 模板函数 运算符重载
template <class T,int n>
T ArrayTP<T,n>::operator[](int i) const
{
	if(i < 0 || i>= n)
	{
		std::cerr << "Error in array limits: " << i
				  << " is out of range\n";
		std::exit(EXIT_FAILURE);
	}
	return ar[i];
}
#endif
// 私有继承，基类的公有成员和保护成员都将成为派生类的私有成员，这意味着基类方法将不会成为派生对象公有接口的一部分，但可以在派生类的成员函数中使用它们
// 包含将对象作为一个命名的成员对象添加到类中，而私有继承将对象作为一个未被命名的继承对象添加到类中，我们将使用术语子对象来表示通过继承或包含添加的对象，私有继承与包含相同，获得实现，但不获得接口
// 使用包含时将使用对象名来调用方法，而使用私有继承时将使用类名和作用域解析运算符来调用方法
// 访问基类对象或基类对象的友元函数，可能将派生类强制转换为基类，再利用基类进行访问
// 在私有继承中，在不进行显式类型转换的情况下，不能将指向派生类的引用或指针赋给基类引用或指针
// 通常，应使用包含来建立has-a关系。如果新类需要访问原有类的保护成员或需要重新定义虚函数，则应使用私有继承


#ifndef STUDENTC_H_
#define STUDENTC_H_
#include <iostream>
#include <valarray>
#include <string>
//私有继承
class Student : private std::string,private std::valarray<double>   //valarray模板类
{
private:
	typedef std::valarray<double> ArrayDb;   //继承后私有成员
	std::ostream & arr_out(std::ostream & os) const;
public:
	Student() : std::string("Null Student"),ArrayDb(){}  //成员初始化列表
	explicit Student(const std::string & s):std::string(s),ArrayDb(){}    //显式转换构造函数，继承的子对象
	explicit Student(int n) : std::string("Null"),ArrayDb(n){}
	Student(const std::string & s,int n) : std::string(s),ArrayDb(n){};
	Student(const std::string & s,const ArrayDb & a) : std::string(s),ArrayDb(a){}
	Student(const char * str,const double * pd,int n) : std::string(str),ArrayDb(pd,n){}
	~Student(){};
	double Average() const;
	double & operator[](int i);
	double operator[](int i) const;
	const std::string & Name() const;

	friend std::istream & operator>>(std::istream & is,Student & stu);
	friend std::istream & getline(std::istream & is,Student & stu);
	friend std::ostream & operator<<(std::ostream & os,const Student & stu);
};
#endif
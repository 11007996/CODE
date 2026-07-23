#include <iostream>
#include <string>
#include <valarray>
class Student
{
private:
	typedef std::valarray<double> ArrayDb;   //valarray模板类，C++标准库，比array数据类型功能更多
	std::string name;
	ArrayDb scores;
	std::ostream & arr_out(std::ostream & os) const;   //私有函数
public:
	Student() : name("Null Student"),scores() {} //成员初始化列表，构造函数,内联函数
	explicit Student(int n) : name("Nully"),scores(n){}   //显式转换构造函数
	Student(const std::string & s,const ArrayDb & a) : name(s),scores(a){}   //内联函数
	Student(const std::string & s,int n):name(s),scores(n){}
	Student(const char * str,const double * pd,int n):name(str),scores(pd,n){}    //内联函数
	// ~Student(){};
	double Average() const;
	const std::string & Name() const;
	double & operator[](int i);
	double operator[](int i) const;

	//友元函数，不属于类成员函数，不能继承
	friend std::istream & operator>>(std::istream & is,Student & stu);
	friend std::istream & getline(std::istream & is,Student & stu);
	friend std::ostream & operator<<(std::ostream & os,const Student & stu);
};
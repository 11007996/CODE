#include "studenti.h"
using std::ostream;
using std::endl;
using std::istream;
using std::string;
double Student::Average()const
{
	if(ArrayDb::size()>0)   //私有继承，使用类名和命名空间解析符访问基类的公有函数
		return ArrayDb::sum()/ArrayDb::size();
	else
		return 0;
}
const string & Student::Name() const
{
	return (const string &) *this;   //利用派生类的公有函数访问基类对象，必须进行强制类型转换
}
double & Student::operator[](int i)
{
	return ArrayDb::operator[](i);   //利用派生类的公有函数访问基类方法
}
double Student::operator[](int i)const
{
	return ArrayDb::operator[](i);
}

ostream & Student::arr_out(ostream & os) const
{
	int i;
	int lim = ArrayDb::size();
	if(lim >0)
	{
		for(i=0;i<lim;i++)
		{
			os << ArrayDb::operator[](i) << " ";
			if(i % 5 == 4)
				os << endl;
		}
		if(i % 5 != 5)
			os << endl;
	}
	else
		os << " empty arrray ";
	return os;
}

istream & operator>>(istream & is,Student & stu)
{
	is >> (string &)stu;   //访问基类对象本身必须进行强制类型转换
	return is;
}

istream & getline(istream & is,Student & stu)
{
	getline(is,(string &)stu);    //必须进行强制类型转换为string对象，否则陷入递归循环
	return is;
}
ostream & operator<<(ostream & os,const Student & stu)
{
	os << "Scores for " << (const string &)stu << ":\n";
	stu.arr_out(os);
	return os;
}
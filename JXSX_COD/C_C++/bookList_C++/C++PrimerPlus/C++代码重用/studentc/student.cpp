#include "studentc.h"
using std::ostream;
using std::endl;
using std::istream;
using std::string;
double Student::Average()const
{
	if(scores.size() > 0)
		return scores.sum()/scores.size();   //valarray类函数，计算平均值
	else
		return 0;
}

const string & Student::Name() const    //私有字段通过公有函数访问
{
	return name;
}
double & Student::operator[](int i)   //[]运算符重载
{
	return scores[i];
}
double Student::operator[](int i)const
{
	return scores[i];
}
//私有函数
ostream & Student::arr_out(ostream & os)const
{
	int i;
	int lim = scores.size();  //获取valarray长度
	if(lim> 0)
	{
		for(i = 0;i<lim;i++)
		{
			os << scores[i] << " ";
			if(i % 5 == 4)
				os << endl;
		}
		if(i % 5 != 0)
			os<<endl;
	}
	else
		os << " empty array ";
	return os;
}

istream & operator>>(istream & is,Student & stu)
{
	is >> stu.name;
	return is;
}
//友元函数
istream & getline(istream & is,Student & stu)
{
	getline(is,stu.name);  //递归
	return is;
}
ostream & operator<<(ostream & os,const Student & stu)
{
	os << "Scores for " << stu.name << ":\n";
	stu.arr_out(os);
	return os;
}
#include <iostream>
#ifndef STRNGBAD_H_
#define STRNGBAD_H_
class StringBad
{
private:
	char * str;  //类成员只存储字符串的地址，字符串的内容保存在堆中，析构函数只能释放指针，但不能释放指针指向的对象，因此需要在析构函数中释放对象空间
	int len;
	static int num_strings;
public:
	StringBad(const char * s);
	StringBad();
	~StringBad();
	friend std::ostream & operator<<(std::ostream & os,const StringBad & st);
	
};
#endif
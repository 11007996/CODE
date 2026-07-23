#ifndef N11   //避免头文件中的内容被重复调用
#define N11   //建议定义一下与头文件类似的宏名称
#define N 10    //宏定义

typedef int _BOOL_;    //类型定义

int add(int,int);    //函数原型


int add(int a,int b)
{
	return a+b;
}
#endif
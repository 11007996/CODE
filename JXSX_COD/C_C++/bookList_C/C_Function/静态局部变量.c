#include<stdio.h>
int test(void);
int main(void)
{
	test();
	return 0;

}
int test(void)
{
	static int i;    //静态局部变量是块作用域，函数返回时变量i不会丢失其值
	for(;i<10;i++)
	{
		printf("i=%d\n",i);
	}
	test();
	return 0;
}


// 全局变量拥有静态存储期限，且具有文件作用域
#include <stdio.h>
#include <math.h>
#include <stdlib.h>
/*int main(void)
{
	int i = 030;  //以0开头按8进制数字处理
	printf("%d\n",i);
	return 0;
}*/

/*int main(void)
{
	char a = 'a';    //以最后一个字符为准，由编译器定义
	printf("%c\n",a);
	printf("%c\n",a+1);
	printf("%c\n",98);
	return 0;
}*/

/*int main(void)
{
	int a=2,c=3;
	printf("%d\n",a+++c);   //先算加法，再自增a
	printf("%d\n",a);
	return 0;
}*/


/*int add(int a,int b)
{
	return a+b;
}

int (*p)(int,int) = add;   //函数指针

int addd(const int (*p)(int,int),int a)
{
	// int i = 4;
	return p(2,3)+a;  //(*p)(2,3)+a的简写形式
}

int main(void)
{
	// int i=p(2,3);
	int i = addd(p,6);
	printf("%d\n",i);
	return 0;
}*/

/*// 优先级
单目运算符>双目运算符>算术运算符>移位运算符>关系运算符>逻辑>赋值运算符>条件运算符(三目运算符)
任何一个逻辑运算符的优先级低于任何一个关系运算符
移位运算符的优先级比算术运算会要低，但是比关系运算符要高*/



/*int main(void)
{
	int a[5] = {0};
	int *p = a;
	printf("%d\n",*p+1);
	printf("%d\n",(*p)+1);
	printf("%d\n",*(p+1));
}*/


/*int main(void)
{
	int i,a[10];
	for(i=1;i<=10;i++)
		a[i]=0;
	puts("结束");
	return 0;
}*/
/*main()
{
	double s;
	s=sqrt(2);
	printf("%g\n",s);     //函数没有声明返回类型，默认以sqrt的值为main函数返回值
}*/


/*int main()
{
	int i;
	char c;
	for(i=0;i<5;i++)
	{
		scanf("%d",&c);
		printf("%d ",i);
	}
	printf("\n");
}*/


/*int main(void)
{
	union dx
	{
		int i;
		char c;
	}dxd;
	dxd.i = 12345;   //十六进制为3039
	if(dxd.c==0x39)    //十六进制
		printf("true\n");   //小端
	else
	printf("false\n");      //大端
	
	printf("int:%d\n",sizeof(int));
	printf("char:%d\n",sizeof(char));
	return 0;
}*/

int main(void)
{
	system("notepad.exe");
	return 0;
}



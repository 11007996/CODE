#include<stdio.h>
// #include<string.h>
#include<limits.h>   //整数大小限制
#include<float.h>    //浮点型大小限制
#define DENSITY 62.4
#define PAGES 959

int main()
{
	/*float weight,volume;
	int size,letters;
	char name[40];

	printf("Hi What's your first name?\n");
	scanf("%s",name);    //如果用scanf读取基本变量类型的值，在变量名前加一个&;如果把字符串读入字符数组中，不要使用&
	printf("%s,What's you weight in pounds?\n",name);
	scanf("%f",&weight);
	size = sizeof name;      //运算对象是类型时括号必不可少，对于运算某个特定量，括号可有可无
	letters = strlen(name);
	volume = weight / DENSITY;
	printf("Well,%s,your volume is %2.2f cubic feet.\n",name,volume);
	printf("Also,your first name has %d letters,\n",letters);
	printf("and we have %d bytes to store it.\n",size);
	*/
	/*char name[1];
	scanf("%s",name);   //scanf读取字符串的第一个字符，如输入”qwe rty“,则只打印”qwe“
	printf("%s\n",name);*/

	/*float a = 78902.4;
	printf("%3.2f",a);*/
	// printf("%4d\n",45);

	/*printf("*%d*\n",PAGES);
	printf("*%2d*\n",PAGES);
	printf("*%10d*\n",PAGES);
	printf("*%-10d*\n",PAGES);*/

	/*const double  RENT = 3852.99;   //只读变量
	printf("*%f*\n",RENT);
	printf("*%e*\n",RENT);
	printf("*%4.2f*\n",RENT);
	printf("*%3.1f*\n",RENT);
	printf("*%10.3f*\n",RENT);
	printf("*%10.3E*\n",RENT);
	printf("*%+4.2f*\n",RENT);
	printf("*%+4.2f*\n",RENT);
	printf("*%010.2f*\n",RENT);*/
	int a,b;
	scanf("%d,%d\n",&a,&b);
	printf("%*d\n",a,b);
	return 0;
}

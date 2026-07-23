#include<stdio.h>
// extern int aa;
int main(void)
{
	/*float weight,value;
	printf("Are you withd\n");
	printf("请输入体重：\n");

	scanf("%f",weight);
	printf("you weight is %f",weight);*/

	/*int a = 12;
	int* p = &a;
	printf("%p\n",p );
	*p = 13;
	printf("%d\n",a);
	int c = printf("%d\n",*p);     //printf返回值为返回的字符个数
	*/

	/*float a = 7/2.0;
	printf("%.2f\n",a );*/

	// getchar();

	
	/*printf("%d\n",sizeof(int));
	printf("%d\n",sizeof(short));
	printf("%d\n",sizeof(char));
	printf("%d\n",sizeof(long));
	printf("%d\n",sizeof(long long));
	printf("%d\n",sizeof(float));
	printf("%d\n",sizeof(double));
	printf("%d\n",sizeof(unsigned int));
	printf("%d\n",sizeof(signed int));*/

	/*int a = -1234567890;
	printf("%d\n",a );*/

	/*int x = 100;
	printf("dec = %d octal = %o hex = %X\n",x,x,x );  //八进制、十六进制
	printf("dec = %d octal = %#o hex = %#X\n",x,x,x );
	printf("dec = %ld",x );    //%ld表示十进制长整型
	printf("dec = %hd",x );    //%ld表示十进制短整型
	printf("dec = %lu",x );    //%ld表示无符号长整型
	printf("dec = %lo",x );    //%ld表示八进制长整型
	printf("dec = %lx",x );    //%ld表示十六进制长整型*/

	/*char a = 80;
	char b ='W';
	printf("%c\n%c\n",a ,b);    //打印字符数据
	printf("%d\n",a);*/

	/*double a =100.;
	double b = .2;
	double c = .8E-3;
	float toobig = 3.4E38*100.0f;
	float toosmall = 0.1234E-10 /10;
	printf("%e\n %Le\n %.5f\n",a,b,c );   //指数计数法
	printf("%e\n",toobig );   //数值上溢，打印无穷大
	printf("%e\n",toosmall );   //数值下溢，损失有效位的最后一位*/

	/*printf("%d\n",sizeof(int) );
	printf("%zd\n",sizeof(short) );   //%zd匹配sizeof数据类型*/
	/*float g = 8.0f;
	printf("%d\n",g );*/


	float salary;
	printf("\aEnter your desired monthly salary:");   //\a表示警报声
	printf(" $______\b\b\b\b\b\b");
	scanf("%f",&salary);
	printf("\n\t$%.2f a month is $%.2f a year.",salary,salary * 12.0);
	printf("\rGee!\n");


	return 0;
}
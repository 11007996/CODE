#include<stdio.h>
#include<string.h>
#include<math.h>
#include<stdbool.h>   //布尔值头文件，bool作为_Bool的别名，有利于与C++兼容
#include<ctype.h>   //字符测试函数
#include<iso646.h>   //包含and/or/not逻辑运算符

// void display(char cr,int lines,int width);

long get_long(void);
bool bad_limits(long being,long end,long low, long hight);
double sum_squares(long a,long b);

int main()
{
	/*typedef double real;   //定义double类型的别名
	real = 0.2;*/

	/*const double ANSWER=3.14159;
	double response;
	printf("What is the value of pi?\n");
	scanf("%lf",&response);
	while(fabs(response-ANSWER)>0.1)       //fabs得到差值的绝对值
	{
		printf("Try again!\n");
		scanf("%lf",&response);
	}
	printf("Close enough!\n");*/

	/*char ch;
	while((ch = getchar())!= '\n')
	{
		if(isalpha(ch))    //是否是一个字母
		{
			putchar(ch+1);
		}
		else
		{
			putchar(ch);
		}
	}
	putchar(ch);*/

	/*int guess =1;
	printf("Pick an iteger from 1 to 100.I will try to guess ");
	printf("it.\nRespond with a y if my guess is right and with");
	printf("\nan n if it is wrong.\n");
	printf("Uh...is your number %d?\n",guess);
	while(getchar() != 'y')
	{
		printf("Well, then,is it %d?\n",++guess);
		while(getchar()!='\n')
			continue;
	}
	printf("I knew I could do it!\n");*/



	/*int ch;
	int rows,cols;
	printf("Enter a character and two integers:\n");
	while((ch = getchar() != '\n'))
	{
		scanf("%d %d",&rows,&cols);
		display(ch,rows,cols);
		printf("Enter another character and two integers;\n");
		printf("Enter a newline to quit.\n");
	}
	printf("Bye.\n");*/


	const long MIN = -100000000L;
	const long MAX = +100000000L;
	long start,stop;
	double answer;
	printf("This program computes the sum of the squares of "
		   "integers in a range.\nThe lower bound should not "
		   "be less than -10000000 and \nThe upper bound "
		   "should not by more than +10000000.\nEnter the "
		   "limites (enter 0 for both limits to quit):\n"
		   "lower limit: ");
	start = get_long();
	printf("upper limit: ");
	stop = get_long();
	while(start != 0 || stop!= 0)
	{
		if(bad_limits(start,stop,MIN,MAX))
		{
			printf("Please try again.\n");
		}
		else
		{
			answer = sum_squares(start,stop);
			printf("The sum of the squares of the integers ");
			printf("from %ld to %ld is %g\n",start,stop,answer);
		}
		printf("Enter the limis (enter 0 for both limits to quit):\n");
		printf("lower limit: ");
		start = get_long();
		printf("upper limit: ");
		stop = get_long();
	}
	printf("Done.\n");
	return 0;
}

long get_long(void)
{
	long input;
	char ch;
	while(scanf("%ld",&input)!=1)
	{
		while((ch = getchar()) != '\n')
			putchar(ch);
		printf(" is not an integer.\nPlease enter an ");
		printf("inter value,such as 25,-178,or 3: ");
	}
	return input;
}

double sum_squares(long a,long b)
{
	double total =0;
	long i;
	for(i=a;i<=b;i++)
		total +=(double)i*(double)i;
	return total;
}

bool bad_limits(long begin,long end,long low,long hight)
{
	bool not_good = false;
	if(begin > end)
	{
		printf("%ld isn't smaller then %ld.\n",begin,end);
		not_good = true;
	}
	if(begin < low || end < low)
	{
		printf("Values must by %ld or greater.\n",low);
		not_good = true;
	}
	if(begin >hight || end > hight)
	{
		printf("Values must by %ld or less.\n",hight);
		not_good = true;
	}
	return not_good;
}
/*void display(char cr,int lines,int width)
{
	int row,col;
	for(row = 1;row<= lines;row++)
	{
		for(col = 1;col<= width;col++)
			putchar(cr);
		putchar('\n');
	}
}*/


/*isalnum()  //是否是字母或数字
isalpha()  //是否是字母
isblank()  //是否是标准的空白字符（空格、水平制表符或换行符）或任何其他本地化指定为空白的字符
iscntrl()  //是否是控制字符，如ctrl+b
isdigit()  //是否是数字
isgraph()  //是否是除空格之外的任意可打印字符
islower()  //是否是小写字母
isprint()  //是否是可打印字符
ispunct()  //是否是标点符号（除空格或字母数字字符以外的任何可打印字符）
isspace()  //是否是空白字符（空格、换行符、换页符、垂直制表符。水平制表符或其他本地化定义的字符）
isupper()  //是否是大写字母
isxdigit() //是否是十进制数字符
tolower()  //转为小写字母
toupper()  //转为大写字母*/
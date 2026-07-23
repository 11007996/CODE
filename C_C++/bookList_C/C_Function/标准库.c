/*<assert.h>
仅包含assert宏，它允许我们在程序中插入自我检查，一旦任何检查失败，程序会被终止

<ctype.h>
提供用于字符分类及大小写转换的函数

<errno.h>
提供了errno，可以在调用特定库函数后进行检测，来判断调用过程中是否有错误发生

<float.h>
提供了用于描述浮点类型特性的宏，包括值的范围及精度

<limits.h>
提供了用于描述整数不顾一切(包括字符类型)特性的宏，包括它们的最大值和最小值

<locale.h>
提供了一些函数来帮助程序适应针对某个国家或地区的特定行为方式，包括显示数的方式、货币的格式、字符集以及日期和时间的表示形式

<math.h>
提供了常见的数学函数，包括三角函数、双曲函数、指数函数、对数函数、幂函数、邻近取整函数、绝对值运算函数及取余函数

<setjmp.h>   非本地跳转

<signal.h>  信号处理
用于处理异常情况的函数，包括中断和运行时错误，signal函数可以设置一个函数，使系统会在给定信号发生后自动调用该函数；raise函数用来产生信号

<stdio.h>
提供了大量的输入输出函数，包含对顺序访问和随机访问文件的操作

<stdarg.h>
提供了一些工具用于编写参数个数可变的函数，就像printf和scanf一样

<stddef.h>
提供了经常使用的类型和宏的定义

<string.h>
提供了用于进行字符串操作(包括复制、拼接、比较及搜索)的函数以及对任意内存块进行操作的函数

<time.h>
提供了相应的函数来获取时间，操纵时间，以及格式化时间的显示

<complex.h>
定义了complex和I宏，用于复数运算

<fenv.j>
提供了对浮点标志和控制模式的访问，程序可以测试标志和判断浮点数运算过程中是否发生溢出，或者设置控制模式来指定如何进行取整

<inttypes.h>
用于<stdint.h>中声明的整数类型的输入输出的格式化字符串的宏，还提供了处理最大宽度整数的函数

<iso646.h>
定义了可代表特定运算符(包含字符&、|、！、和^的运算符)的宏

<stdbool.h>
定义了bool、true和false宏，同时还定义了一个可以用于测试这些宏是否已被定义的宏

<stdint.h>
声明了指定宽度的整数类型并定义了相关的宏，同时定义了用于构建具体类型的整数常量的带参数的宏

<tgmath.h>  泛型数学

<wchar.h>
提供了宽字符输入输出和宽字符串操作的函数

<wctype.h>
是<ctpe.h>的宽字符版本，提供了对宽字符进行分类和修改的函数


isalnum(c)   //判断是否是字母或数字
isalpha(c)   //判断是否是字母
isblank(c)   //判断是否是标准空白字符
iscntrl(c)   //判断是否是控制字符
isdigit(c)   //判断是否是十进制数字
isgraph(c)   //c是否是可显示字符(除空格外)
islower(c)   //c是否是小写字母
isprint(c)   //c是否是可打印字符(包含空格)
ispunct(c)   //是否是标点符号
isspace(c)   //是否是空白字符
isupper(c)   //是否是大写字母
isxdigit(c)  //是否是十六进制数字

tolower(c)   //转为小定
toupper(c)   //转为大写*/



/*#include <stdio.h>
// #define NDEBUG    //定义NDEBUG后，assert.h不会编译，assert宏失效
// #ifdef NDEBUG
#include <assert.h>
// #endif


int main(void)
{
	int i=10;

	assert(i<0);     //断言，为假时终止程序

	printf("%d\n",i);
	assert(i<20);
	return 0;
}*/


#include <stdio.h>
#include <stdlib.h>
#include <errno.h>   //包含errno宏
#include <math.h>
#include <string.h>
#include <signal.h>   //信号处理
#include <setjmp.h>   //跳转
#include <unistd.h>   //定义sleep函数
#include <stdarg.h>   //定义可函数可带任意多个参数
/*int main(void)
{
	errno=0;  //必须先将errno设置为0
	int y = sqrt(-1);   //函数错误会将errno改为其它错误码
	// 错误发生时，errno通常为EDOM或ERANGE
	// EDOM(定义域错误)：传递给函数的一个参数超出了函数的定义域
	// ERANGE(取值范围错误)：函数的返回值太大，无法用返回类型表示
	if(errno!=0)  //判断程序是否出错
	{
		printf("程序错误,erno:%d\n",errno);
		perror("perror message");    //返回错误类型，表示errno值的意义，在<stdio.h>中定义
		puts(strerror(errno));   //strerror函数返回参数代表的错误信息，在<string.h>中定义
		exit(EXIT_FAILURE);
	}
	printf("%d\n",y);
	return 0;
}*/

/*void close_sighandler(int signum)
{
	printf("signum=%d\n",signum);
	exit(EXIT_FAILURE);
}
int main(void)
{
	signal(SIGINT,close_sighandler);   //异步执行，第一个参数表示接收系统发出的某种信号，第二个参数表示处理信号的自定义函数，，接收到指定的信号后执行自定义函数
	// signal(SIGINT,SIG_IGN);   //SIG_IGN是预定义信号处理函数，表示忽略该信号
	// signal(SIGINT,SIG_DFL);   //SIG_DFL是预定义信号处理函数，表示使用编译器默认是处理方式处理信号，一般会终止程序
 	if(signal(SIGINT,close_sighandler)==SIG_ERR)   //信号处理函数执行失败时会返回SIG_ERR，并设置errno为非零值
 	{
 		perror("signal(SIGINT,close_sighandler) failed");
 	}

	// raise(SIGTERM);   //主动向正在执行的程序发送终止信号，程序直接退出，成功返回0，失败返回非零值
	// SIGABRT   异常终止(可能由于调用abort导致)
	// SIGFPE    在算术运算中发生错误(可能是除以0或溢出)
	// SIGILL    无效指令
	// SIGINT    中断(ctrl+c发送此信号)
	// SIGSEGV   无效存储访问
	// SIGTERM   终止请求


	while(1)
	{
		printf("main fun...\n");
		sleep(1);
	}

	return 0;
}*/


/*void myfunc(int sig)
{
	if(sig==SIGINT)
		printf("系统出现SIGINT信号\n");
	if(sig==SIGTERM)
		printf("系统出现SIGTERM信号\n");
	if(sig==SIGFPE)
		printf("系统出现SIGFPE信号\n");
	if(sig==SIGSEGV)
		printf("系统出现SIGSEGV信号\n");
}

int main(void)
{
	signal(SIGINT,myfunc);
	signal(SIGTERM,myfunc);
	signal(SIGFPE,myfunc);

	raise(SIGTERM);
	while(1);
	return 0;
}*/

/*void msgfun(int msg)
{
	if(msg==SIGFPE)
		printf("除0错误\n");
	if(msg==SIGINT)
		printf("主动退出\n");
	exit(EXIT_FAILURE);
}

int main(void)
{
	signal(SIGFPE,msgfun);
	signal(SIGINT,msgfun);

	int i=1;
	int c = 1;
	const int a = 100;
	scanf("%d",&i);
	sleep(1);
	c = a%i;
	printf("c:%d",c);
	return 0;

}*/





/*
jmp_buf env;   //jmp_buf宏，在<setjmp.h>定义
void f1(void);
void f2(void);
int main(void)
{
	if(setjmp(env) == 0)  //第一次调用setjmp返回0，再次调用会返回一个非零值；第一次调用会把当前位置保存在env,使用longjmp调用时跳转到此处
		printf("setjmp returned 0\n");
	else
	{
		printf("Program terminates:longjmp called\n");
		return 0;
	}
	f1();
	printf("Program terminates:longjmp normally\n");
	return 0;
}

void f1(void)
{
	printf("f1 begins\n");
	f2();
	printf("f1 returns\n");
}
void f2(void)
{
	printf("f2 begins\n");
	longjmp(env,1);  //程序运行跳转到指定位置，第一个参数表示跳转的位置，第二个参数是跳转后再次调用setjmp是返回值(如果是0,setjmp宏会返回1)
	printf("f2 returns\n");
}*/


/*
<iso646.h>定义宏
and     &&
and_eq  &=
bitand  &
bitor   |
compl   ~
not     !
not_eq  !=
or      ||
or_eq   |=
xor     ^
xor_eq  ^=*/


/*int max_int(int n,...)
{
	va_list ap;
	int i,current,largest;
	va_start(ap,n);   //va_start指出参数列表中可变长度部分开始的位置，此处是n之后可以有多个参数，省略号必须在参数列表的末尾
	largest=va_arg(ap,int);   //va_arg获取可变长度部分的参数赋值给largest，参数int表示可变长度参数为int类型
	for(i=i;i<n;i++)
	{
		current = va_arg(ap,int);   //不断循环获取下一个参数值
		if(current >largest)
			largest=current;
	}
	va_end(ap);    //关闭遍历参数，没有关闭再次运行又从头循环遍历
	return largest;
}*/



/*//类型转换
double atof(const char *nptr);
int atoi(const char *nptr);
long int atol(const char *nptr);
long long int atoll(const char *nptr);
//增加一个转换停止的位置，如果转换返回数值超过类型的表示范围，会设置errno变量为ERANGE
double strtod(const char * restrict nptr,char ** restrict endptr);
float strtof(const char * restrict nptr,char ** restrict endptr);
long double strtold(const char * restict nptr,char ** restrict endptr);
long int atrtol(const char * restrict nptr,char ** restrict endptr,int base);
long long int strtoll(const char * restrict nptr,char ** restrict endptr,int base);
unsigned long int strtoul(const char * restrict nptr,char ** restrict endptr,int base);
unsigned long long int strtoull(const char * restrict nptr,char ** restrict endptr,int base);*/



/*伪随机数生成函数
int rand(void);
void srand(unsigned int seed);*/
/*#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void show(void)
{
	printf("atexit o");
}
int main(void)
{
	clock_t start_clock=clock();   //记录当前时间，clock_t类型在<time.h>中定义
	int seed = time(NULL);
	printf("seed:%d\n",seed);
	// atexit(show);
	// exit(EXIT_FAILURE);
	srand(seed);
	for(int i=0;i<5;i++)
	printf("%d\n",rand() % 100);
	printf("Process time used: %g sec.\n",(clock()-start_clock)/(double)CLOCKS_PER_SEC);   //计算间隔多少秒，CLOCKS_PER_SEC在<time.h>中定义，编译器中不规定其数据类型，所有先将其转换为double
	return 0;
}*/


/*void abort(void);     //强制非正常终止程序(不清理输出缓冲区，不关闭打开的流，不删除临时文件)
int atexit(void (*func)(void));      //配合exit函数，退出程序之前执行自定义func函数
void exit(int status);     //退出程序，等价于return 0，宏EXIT_FAILURE和EXIT_SUCCESS
void _Exit(int status);    //等价于exit函数，但不会执行atexit注册的函数
void *getenv(const char *name);   //char *p = getenv("PATH")  获取操作环境提供的一组描述用户特性的字符串
int system(const char *string);    //在程序内调用另一个程序，就像用CMD控制台调用*/   
/*int main(void)
{
	system("notepad.exe");
	getchar();
	return 0;
}*/



/*time_t cur_time = time(NULL)
等价于
time(&cur_time)


difftime(time(NULL)-cur_time);    获取两个时间点的间隔*/



/*#include <stdio.h>
#include <time.h>

int main(void)
{
	time_t curTime = time(NULL);    //日历时间
	struct tm *t = localtime(&curTime);    //日历时间转换为分解时间
	// mktime(t);
	printf("year:%d\n",t->tm_year +1900);
	printf("month:%d\n",t->tm_mon +1);
	printf("day:%d\n",t->tm_mday);
	printf("hour:%d\n",t->tm_hour);
	printf("min:%d\n",t->tm_min);
	printf("sec:%d\n",t->tm_sec);
	printf("wday:%d\n",t->tm_wday);
	printf("yday:%d\n",t->tm_yday+1);
	return 0;
}*/

// char *asctime(const struct tm *timeptr);   //分解时间转换为字符串，函数返回一个指向以空字符串结尾的字符串的指针
// char *ctime(const time_t *timer);   //返回一个指向描述本地时间的字符串的指针，ctime(&cur_time)等价于asctime(localtime(&cur_time))
// struct tm *gmtime(const time_t *timer);  //与localtime类型，返回时为UTC(协调世界时间)表示
// struct tm *localtime(const time_t *timer);   //返回以当时间表示
// size_t strftime(char * restrict s,size_t maxsize,const char * restrivt format,const struct tm * restrict timeptr);

/*#include <stdio.h>
#include<stdlib.h>
int main(void)
{
	char *p = "abcdefghijk";
	char d ='d';
	char *i = memchr(p,d,8);    //返回指向字符的指针，没找到就返回空指针
	printf("%s\n",i);
	return 0;
}*/


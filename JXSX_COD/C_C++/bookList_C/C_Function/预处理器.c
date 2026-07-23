/*三种类型：
    1.宏定义：#deinfe
    2.文件包含：#include
    3.条件编译：#if #ifdef  #ufbdef #else #endif
    4.其它：#error #line #pragma*/


// 程序调用函数会有一定的开销，如存储上下文信息、自制参数的值等

/*#include <stdio.h>
#define PRINT_INT(n)  printf(#n "=%d\n", n);    //#运算符  #n表示将传入的参数字符串化
int main(void)
{
	PRINT_INT(2+1);    //返回2+1=3
	return 0;
}*/


// 宏定义的作用范围通常到出现这个宏的文件末尾

// #undef DEFINE_NAME  //取消宏定义

/*#define BCHO(s) (get(s),puts(s))
相当于
#define BCHO(s) {get(s);puts(s)}*/


/*预定义宏
__LINE_  被编译的文件中的行号
__FILE__  被编译的文件名
__DATE__  编译的日期(Mmm dd yyyy)
__TIME__  编译的时间(hh:mm:ss)
__STDC__  如果编译器符合C标准，那么值为1*/

/*#include <stdio.h>
int main (void)
{
    printf("Copyright (c) Powered by www.develhome.com\n");
    printf("Compiled on %s at %s\n", __DATE__,__TIME__);
    return 0;
}*/


// 参数个数可变的宏
/*#define TEST(condition,...) ((condition)?\
    printf("Passed test:%s\n",#condition):\
    printf(__VA_ARGS__))   //__VA_ARGS__可变参数宏，只能出现在宏的参数列表中，代表所有与省略号相对应的参数*/

/*#include <stdio.h>
#define FUNCTION_CALLED() printf("%s called\n",__func__ );
#define FUNCTION_RETURNS() printf("%s returns\n",__func__ );     //__func__返回当前调用的函数
int main(void)
{
	fun();
}
void fun(void)
{
	FUNCTION_CALLED();
	FUNCTION_RETURNS();
}*/



/*#include <stdio.h>
#define DEBUG 1
int main(void)
{
	#if DEBUG    //值为0时编译器删除这段代码，非零时执行
	puts("DEBUG");
	#endif

	puts("OK");
	return 0;
}*/



/*#include <stdio.h>
#define DEBUG 1
// #undef DEBUG
int main(void)
{
	// #if defined(DEBUG)    //defined预处理器运算符，表示宏是否被已定义,括号可以省略
	#ifdef DEBUG    //判断宏是否被已定义，与defined运算符等价
	#ifndef DEBUG   //判断宏是否没被定义，与#ifdef相反
	puts("DEBUG");
	#endif

	puts("OK");
	return 0;
}*/


/*#include <stdio.h>
#define DEBUG 51
// #undef DEBUG
int main(void)
{
	//if条件编译
	#if (DEBUG>5)
	puts("大于5");
	#elif (DEBUG>2)
	puts("大于2");
	#else
	puts("实数");
	#endif

	puts("OK");
	return 0;
}*/



#include <stdio.h>
int main(void)
{
	// #error error消息    //编译中返回一条错误消息

	// #line 20    //修改程序行号，也可以理解为修改了__LINE__宏
	// printf("%d\n",__LINE__);

	/*_Pragma("data(heap_size => 1000, stack_size => 2000)");
	等价于
	#pragma date(heap_size => 1000, stack_size => 2000)    //#pragma预处理器的作用是设定编译器的状态或者是指示编译器完成一些特定的动作
	// #pragma message("asdfg");   //指示编译器编译时返回一条消息
	*/

	
	return 0;
}
#include <stdio.h>
#include "11.h"
// #include "D:\zzzzzzzz\CodeBlock\C_C++\bookList_C\CC\test\11.h"
int main(void)
{
	//int i=10;  //定义变量
	//extern i;  //声明变量，表示此变量在其它c文件已定义，这里只是引用，不分配内存空间
	printf("N:%d\n",N);
	int c = add(2,3);
	printf("c:%d\n",c);
	getchar();
	return 0;
}

//预处理头文件加入C源文件 -> 编译生成目标文件.obj -> 把库函数及全局变量结合目标文件生成可执行程序
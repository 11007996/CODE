// malloc函数：分配内存块，但是不对内存块进行初始化，常用，返回类型为void*，表示通用指针
// calloc函数：分配内存块，并且对内存块进行清零
// realloc函数：调整先前分配的内存块大小
// 在分配内存时，如果找不到我们需要的足够大的内存块时，函数会返回一个空指针，在应用分配的内存时需要判断是否返回空指针
// 分配内存要包含<stdlib.h>头文件
/*char *p=(char *)malloc(1000+1);  //单位字节,也可以不强制转换为char*类型指针，C语言会根据p指针类型隐式转换
if(p=NULL)
{
	return 0;
}*/


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
int main(void)
{
	char *result;
	char s1[5] = "abcd";
	char s2[3] = "ef";
	// result = malloc(strlen(s1)+strlen(s2)+1); //以字节为单位
	result = calloc(7,sizeof(char));    //分配指定指定数量的数据类型的空间
	if(result==NULL)
	{
		puts("分配内存失败");
	}
	result = realloc(result,8);  //重新调整已分配的内存块大小，已分配的内存必须是通过malloc或calloc或realloc获得的内存块
	// 在realloc减少内存块时，应该在原先的内存块上直接进行缩减，而不需要移动存储在内存块中的数据。同理，扩大内存收款人时也不应该对其进行移动。如果无法扩大内存块，realloc函数会在另外分配新的内存块，然后把旧块中的内容复制到新块中
	// 一旦realloc函数返回，请一定要对指向内存块的所有指针进行更新，因为realloc函数可能会使内存块移动到了其他地方
	strcpy(result,s1);
	strcat(result,s2);
	printf("result:%s\n",result);
	printf("result:%p\n",result);
	free(result);   //释放内存必须是malloc等函数分配的内存块，此时result对内存没有任何控制权，称为悬空指针，不能再访问悬空指针
	// malloc函数和其他内存分配函数所获得的内存块都来自一个称为堆的存储池
	// 对程序而言，不可再访问的内存块被称为垃圾，留有垃圾的程序存在内存泄漏现象，C不提供垃圾收集器，每个C程序都是负责回收各自的垃圾

	return 0;
}
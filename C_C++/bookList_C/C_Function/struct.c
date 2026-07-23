// 结构体在内存中的存储是顺序的，但成员之间可能有空㓊，有浪费的空间地址，这是由编译器决定的
/*struct   //匿名定义结构体
{
	char name[20];
	char sex;
	int age;
}man1,man2;

// 结构体用于链表时只能用此种方式定义结构体
struct man   //结构体标记
{
	char name[20];
	char sex;
	int age;
}man1,man2;

typedef struct man    //结构体标记别名
{
	char name[20];
	char sex;
	int age;
}man1,man2;
*/

/*#include <stdio.h>
//联合   编译器只为联合中最大的成员分配足够的内存空间，联合的成员在这个空间内彼此覆盖，即联合只存在一个内存地址，成员只能同时存在一个
union
{
	int i;
	double d;
}u={0},u2={.d=1.234};
int main(void)
{
	u.i = 4;
	printf("i:%d\n",u.i);
	u.d = 2.1234;
	printf("d:%f\n",u.d);
	printf("i:%d\n",u.i);
	u.i = 345678;
	printf("i:%d\n",u.i);
	printf("d:%f\n",u.d);
	printf("d:%f\n",u2.d);
	return 0;
}*/


/*//结构体与联系嵌套，结构体的第四个成员只能是book,mug,shirt中的一个，可以节省结构体的内存空间
struct catalog_item
{
	int stock_number;
	float price;
	int item_type;
	union
	{
		struct
		{
			char title[TITLE_LEN+1];
			char author[AUTHOR_LEN+1];
			int num_pages;
		}book;
		struct
		{
			char design[DESIGN_LEN+1];
		}mug;
		struct
		{
			char design[DESIGN_LEN+1];
			int colors;
			int sizes;
		}shirt;
	}item;
};*/



/*typedef union
{
	int i;
	double d;
}Number;
Number number_array[10]   //创建包含不同数据类型的数组，打破了数组成员必须是同一类型的规则*/   //意味着可能实现类泛型的功能


/*typedef struct
{
	int kind;    //为联合增加一个代表数据类型的字段，在修改联合字段i或d时同时修改字段kind,便于知道联合当前存储的是哪个值
	union
	{
		int i;
		double d;
	}u
}Number;*/



/*//枚举类型
enum
{
	ONE=1,
	TWO=2,
	THREE=3,
	FOUR=4
}Number;


typedef enum num
{
	ONE,   //0
	TWO,   //1
	THREE, //2
	FOUR   //3
}Number;

// 枚举和联合结合使用
typedef struct
{
	enum {int_i,double_d} kind;  
	union
	{
		int i;
		double d;
	}u
}Number;*/



/*struct st
{
	int a : 1;  //整个结构体占四个字节
	int b : 1;
	int c : 2;
}

如果相邻位域字段的类型相同，且其位宽之和小于类型的sizeof大小，则后面的字段将紧邻前一个字段存储，直到不能容纳为止。
如果相邻的位域字段的类型不同，则各编译器的具体实现有差异，VC6采取不压缩方式，Dev-C++，GCC采取压缩方式；*/
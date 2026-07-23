/*int giants = 5   //文件作用域，外部链接，同一程序的其他文件都可使用
static int dodgers = 3    //文件作用域，内部链接，只有该文件中的任意函数可以使用
int main()
{
	rturn 0;
}*/


/*静态存储期：如果对象具有静态存储期，那么它在程序的执行期间一直存在，文件使用域具有静态存储期
线程存储期：具有线程存储期的对象，从被声明时到线程结束一直存在，以关键字_Thread_lcal声明一个对象时，每个线程都获得该变量的私有备份
自动存储期：块作用域变量通常具有自动存储期，当程序进入定义这些变量的块时，为这些变量分配内存，当退出为个块时，释放刚才为变量分配的内存
变长数组的存储期从声明处到块的末尾，而不是从块的开始处到块的末尾
void more(int number)
{
	static int ct =0;
	return 0;
}
ct存储在静态内存中，它从程序被载入到程序结束期间都存在，在作用域只在more函数中，执行该函数才能访问该内存

存储类别：自动，寄存器，静态外部链接，静态内部链接，静态无链接
int main(void)
{
	auto int plox;
	return 0;
}
自动存储类别的变量具有自动存储期。块作用域且无链接，程序在进行入变量声明所在志时变量存在，退出时变量消失*/

/*自动存储类别变量
#include<stdio.h>
int main()
{
	int x =30;
	printf("x in outer block:%d at %p\n",x,&x);
	{
		int x=77;
		printf("x in inner block:%d at %p\n",x,&x);
	}
	printf("x in outer block:%d at %p\n",x,&x);
	while(x++<33)
	{
		int x = 100;
		x++;
	}
	printf("x in while loop:%d at %p\n",x,&x);
	return 0;
}*/


/*//使用存储类别说明符register可声明寄存器变量，请求变量储存在CPU的寄存器中，读取速度更快，不能获取寄存器变量的地址
// 寄存器变量与自动变量大体类似，只是更有可能存储在寄存器中
int main(void)
{
	register int quick;
}*/


/*//静态变量：定义在代码块内，静态的意思是该变量在内存中原地不动，并不是说他的值不变，具有文件作用域的变量自动具有静态存储期
#include<stdio.h>
void trystat(void);
int main(void)
{
	int count;
	for(count=1;count<=3;count++)
	{
		printf("Here comes iteration %d:\n",count);
		trystat();
	}
	return 0;
}
void trystat(void)
{
	int fade =1;
	static int stay=1;   //静态变量内部链接,变量只在编译时被初始化一次
	printf("fade = %d and stay =%d\n",fade++,stay++);
}*/
//静态变量外部链接，普通的外部变量可用于同一程序中任意文件中的函数，但内部链接的静态变量只能用于同一个文件中的函数
//块作用域的静态变量无链接，文件作用域的静态变量具有内部链接
//如果包含extern的声明具有文件作用域，则引用的变量必须具有外部链接。如果包含extern的声明具有块作用域，则引用的变量可能具有外部链接或内部链接
/*int traveler =1;   //外部链接
static int stayhome =1;     //内部链接
int main()
{
	extern int traveler;
	extern int stayhome;
}*/


/*外部变量：把变量的定义性声明放在所有函数的外面，如果使用的外部变量定义在另一个源代码文件中，则必须用extern关键字声明该变量，如果未初始化外部变量，它们会被自动初始化为0
int Errupt;   //外部变量
extern char Coal;   //定义在另一文件中的外部变量
void next(void);
int main(void)
{
	extern int Errupt;    //再次声明使用Errupt外部变量，可不写
}*/
/*#include<stdio.h>
int units=0;
void critic(void);
int main(void)
{
	extern int units;   //表示使用之前已经声明的units变量，此外不是定义，第一次声明称为定义式声明，第2次声明称为引用式声明
	printf("How may pounds to a firkin of butter?\n");
	scanf("%d",&units);
	while(units!=56)
	{
		critic();
	}
	printf("You must have looked it up!\n");
	return 0;
}
void critic(void)
{
	printf("No luck,my friend. Try again.\n");
	scanf("%d",&units);
}*/


#include<stdio.h>
void report_count();
void accumulate(int k);
int count =0;   //文件作用域，外部链接
int main(void)
{
	int value;   //自动变量
	register int i;   //寄存器变量
	printf("Enter a positive integer(0 to quit):");
	while(scanf("%d",&value)==1 && value>0)
	{
		++count;  //使用文件作用域变量
		for(i==value;i>=0;i++)
			accumulate(i);
		printf("Enter a  positive integer (0 to quit):");
	}
	report_count();
	return 0;
}

void report_count()
{
	printf("Loop executed %d times\n",count);
}


#include <stdio.h>
extern int count;   //引用式声明，外部链接
static int total=0;  //静态定义，内部链接
void accumulate(int k);   //函数原型
void accumulate(int k)   //k具有块作用域，无链接
{
	static int subtotal =0;    //静态，无链接
	if(k<=0)
	{
		printf("loop cycle:%d\n",count);
		printf("subtotal:%d;total:%d\n",subtotal,total);
		subtotal=0;
	}
	else
	{
		subtotal+=k;
		total+=k;
	}
}
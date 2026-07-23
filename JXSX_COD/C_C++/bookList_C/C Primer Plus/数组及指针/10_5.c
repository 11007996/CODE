#include<stdio.h>
#include<string.h>
// #define MONTHS 12
#define SIZE 4
int main()
{
	/*int days[MONTHS] = {31,28,[4] = 31,30,31,[1] = 29};    //数组初始化，可指定某项初始化，如果指定的初始化器后面有更多的值，那么后面这些值将被用于初始化指定元素后面的元素，如果再次初始化指定指定的元素，那么最后的初始化将会补取代之前的初始化
	int i;
	for(i=0;i<MONTHS;i++)
	{
		printf("%2d %d\n",i+1,days[i]);
	}*/

	/*short dates[SIZE];
	short * pti;
	short index;
	double bills[SIZE];
	double * ptf;

	pti = dates;   //数组名存储的就是数组第一个元素的地址
	ptf = bills;
	

	printf("%23s %15s\n","short","double");
	for(index=0;index<SIZE;index++)
	{
		printf("pointers + %d: %10p %10p\n",index,pti+index,ptf+index);//指针加1表示下一个元素的地址，而不是指定一下个字节
		//指针加1，指针的值递增它所指向类型的大小
		//dates + 2 == &date[2]
		//*(dates + 2) == dates[2]
	}*/

	/*int a[SIZE] = {11,22,33,44};
	int* p = a;

	printf("%d\n",*(p+1));
	printf("%d\n",*(&a[1]));*/
	return 0;
}
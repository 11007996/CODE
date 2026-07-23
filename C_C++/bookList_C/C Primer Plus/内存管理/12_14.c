/*double * pt;
pt = (double*)malloc(30*sizeof(double));  //为30个double类型的值请求内存空间，相当于声明了一个数组
free(pt);  //释放malloc分配的内存空间*/


#include <stdio.h>
#include<stdlib.h>

int main(void)
{
	double * ptd;
	int max;
	int number;
	int i =0;
	puts("What is the maximum number of type doule entries?");
	if(scanf("%d",&max)!=1)
	{
		puts("Number bit correctly entered -- by.");
		exit(EXIT_FAILURE);
	}
	ptd = (double *)malloc(max*sizeof(double));   //声明double类型的数组内存
	if(ptd ==NULL)
	{
		puts("Memory allocation failed.Goodbye.");
		exit(EXIT_FAILURE);
	}
	puts("Enter the values (q to quit):");
	while(i<max && scanf("%lf",&ptd[i])==1)
		++i;
	printf("Here are you %d entries:\n",number =i);
	for(i=0;i<number;i++)
	{
		printf("%7.2f",ptd[i]);
		if(i%7==6)
			putchar('\n');
	}
	if(i%7!=0)
		putchar('\n');
	puts("Done.");
	free(ptd);   //释放数组内存
	return 0;
}



long * newmem;
newmem = (long *)calloc(100,sizeof(long));  //第一个参数表示所需的存储单元数量，第2个参数表存储单元的大小，该函数所所有对象初始化为0，free()释放内存
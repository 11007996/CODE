#include<stdio.h>
#define N 10
/*int* max_min(const int a[],int n,int* max,int* min);

int main(void)
{
	int *p = NULL;
	int i,big,small;
	int b[10] = {2,4,1,5,3,6,43,7,34,92};
	//int *q = (int []){3,46,1,6,8,54,3,4,56,67};     //复合字面量（匿名数组）
	p = max_min(b,N,&big,&small);     //若big包含大量数据，即使不需要修改big，通过传递指针也可以降低时间和空间复杂度
	printf("Largest:%d\n",big);
	printf("Small:%d\n",small);
	printf("return:%p\n",p);
	return 0;
}

int* max_min(const int a[],int n,int* max,int* min)   //函数返回指针
{
	int i =1;
	*max = *min = a[0];
	for(;i<n;i++)
	{
		if(a[i] > *max)
			*max = a[i];
		else if(a[i]<*min)
			*min = a[i];
	}
	return max;
}*/

int main(void)
{
	int a[][5] = {{1,2,3,4,5},{6,7,8,9,10},{11,12,13,14,15}};
	int* p = &a[0][0];
	printf("a[1][0]:%d\n",a[1][0]);
	printf("*a[1]:%d\n",*a[1]);
	printf("a+1:%p\n",a+1);   printf("*(a+1):%p\n",*(a+1));    //a+1是指向本身的指针;a+1是指向数组第1行的指针(数组指针或行指针);*(a+1)是指向数组第1行第1列的指针
	printf("**(a+1):%d\n",**(a+1));   //*(a+1)=a[1]=&a[1][0]
	printf("p[5]:%d\n",p[5]);
	printf("*(p+5):%d\n",*(p+5));
	printf("*p+5:%d\n",*p+5);
	return 0;
}



//数组指针又称行指针，是一个指针指向数组的某一行，int (*p)[n]
//指针数组：是n个指针组成一个数组，int* p[n]

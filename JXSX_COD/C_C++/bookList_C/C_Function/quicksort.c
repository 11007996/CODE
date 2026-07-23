#include<stdio.h>
#define N 10

/*void quicksort(int a[],int low ,int high);
int split(int a[],int low,int high);
int main(void)
{
	int a[N],i;
	printf("Enter %d number to be sorted:",N);
	for(i =0;i<N;i++)
	{
		scanf("%d",&a[i]);
	}
	quicksort(a,0,N-1);
	printf("In sorted order:");
	for(i=0;i<N;i++)
	{
		printf("%d",a[i]);
	}
	printf("\n");
	return 0;
}

void quicksort(int a[],int low,int high)
{
	int middle;
	if(low>=high) return;
	middle = split(a,low,high);
	quicksort(a,low,middle -1);
	quicksort(a,middle+1,high);
}

//快速排序
int split(int a[],int low,int high)
{
	int part_element = a[low];
	for(;;)
	{
		while(low<high && part_element<=a[high])
		{
			high--;
		}
		if(low>=high) break;
		a[low++] = a[high];   //[]优先级为1，--优先级为2

		while(low<high && a[low]<=part_element)
			low++;
		if(low>=high) break;
		a[high--] = a[low];
	}
	a[high] = part_element;
	return high;
}*/


int main(void)
{
	int a[N],*p;
	printf("Enter %d numbers: ",N);
	for(p =a;p<a+N;p++)
	{
		scanf("%d",p);
	}

	printf("In reverse order:");
	for(p=a+N-1;p>=a;p--)
		printf(" %d",*p);
	printf("\n");

	return 0;
}
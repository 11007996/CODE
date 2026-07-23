#include<stdio.h>
int main(int argc,char *argv[])    //等同于int main(int argc,**argv) 指针的指针，argc表示参数的个数，默认情况下argv[0]指向函数名，argv[1]指向第1个参数
{
	int count;
	printf("The command line has %d arguments:\n",argc-1);
	for(count=0;count<argc;count++)
		printf("%d:%s\n",count,argv[count]);
	printf("\n");
	return 0;
}
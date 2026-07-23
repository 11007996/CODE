#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#define NUM_PLANETS 4

//命令行程序
int main(int argc,char *argv[])
{
	char *planents[] = {"Mercury","Venus","Earth","Mars"};  //指针数组
	int i,j;
	for(i=1;i<argc;i++)
	{
		for(j=0;j<NUM_PLANETS;j++)
		{
			if(strcmp(argv[i],planents[j])==0)   //字符串匹配
			{
				printf("%s is planet %d\n",argv[i],j+1);
				break;
			}
		}
		if(j==NUM_PLANETS)
		printf("%s is not a planet\n",argv[i]);
	}
	return 0;
}
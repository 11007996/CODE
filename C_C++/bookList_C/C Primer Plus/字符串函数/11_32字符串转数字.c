#include<stdio.h>
#include<stdlib.h>
#define LIM 30
char * s_gets(char * st,int n);
/*int main(int argc,char * argv[])
{
	int i,times;
	if(argc<2 || (times=atoi(argv[1]))<1)   //atoi函数将字符串转换为整数值，如“45”转换为45，命令行参数都是以字符串形式储存，不是数字形式则返回0
		printf("Usage: %s positive-number\n",argv[0]);
	else
		for(i=0;i<times;i++)
			puts("Hello,good looking!");
	return 0;
}*/

/*atof()把字符串转换为double类型的值
atol()把字符串转换为long类型的值*/

/*//可识别和报告字符串中的首字母是否是数字
strtol()把字符串转换为long类型的值
strtoul()把字符串转换为unsigned long类型的值
strtod()把字符串转换为double类型的值*/


int main()
{
	char number[LIM];
	char * end;
	long value;

	puts("Enter a number (empty line to quit):");
	while(s_gets(number,LIM) && number[0] != '\0')  //输入的数字都是以字符串形式储存
	{
		value=strtol(number,&end,10); //&end表示结束读取数据的地址
		printf("base 10 input,base 10 output:%d,stopped at %s (%d)\n",value,end,*end);
		value = strtol(number,&end,16);   //十六进制输入
		printf("base 16 input,base 10 output:%ld,stopped at %s (%d)\n",value,end,*end);   //以十进制显示
		puts("Next number:");
	}
	puts("Bye!\n");
	return 0;
}

char * s_gets(char * st,int n)
{
	char * ret_val;
	int i =0;
	ret_val = fgets(st,n,stdin);
	if(ret_val)
	{
		while(st[i] != '\n' && st[i] != '\0')
			i++;
		if(st[i]=='\n')
			st[i]='\0';
		else
			while(getchar()!= '\n')
				continue;
	}
	return ret_val;
}
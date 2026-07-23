#include<stdio.h>
#include<string.h>
#include<ctype.h>
#define LIMIT 81
void ToUpper(char *);
int PunctCount(const char *);

int main(void)
{
	char line[LIMIT];
	char * find;

	puts("Please enter a line:");
	fgets(line,LIMIT,stdin);
	find = strchr(line,'\n');  //保存换行符的指针
	if(find)
	{
		*find = '\0';   //把换行符替换为\0结束符
	}
	ToUpper(line);
	puts(line);
	printf("That line has %d punctuation characters.\n",PunctCount(line));
	return 0;
}

void ToUpper(char * str)
{
	while(*str)
	{
		*str = toupper(*str);   //转换为大写
		str++;
	}
}

int PunctCount(const char * str)
{
	int ct=0;
	while(*str)
	{
		if(ispunct(*str))   //是否为标点符号
			ct++;
		str++;
	}
	return ct;
}
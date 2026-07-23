#include<stdio.h>
#include<stdlib.h>
#include<unistd.h>
#define CNTL_Z '\032'
#define SLEN 81
/*int main(void)
{
	char file[SLEN];
	char ch;
	FILE *fp;
	long count,last;

	puts("Enter the name of the file to be processed:");
	scanf("%80s",file);
	if((fp= fopen(file,"rb"))== NULL)   //以末尾添加的方式打开文件
	{
		printf("reverse can't open %s\n",file);
		exit(EXIT_FAILURE);
	}

	fseek(fp,0L,SEEK_END);   //定位到文件末尾
	last = ftell(fp);  //返回文件是当前位置
	for(count = 1L;count<=last;count++)
	{
		fseek(fp,-count,SEEK_END);    //从末尾开始回退文件位置
		ch = getc(fp);    //从后往前读取文件内容
		if(ch != CNTL_Z && ch != '\r')
		{
			putchar(ch);  //从后往前显示文件内容
		}

	}
	putchar('\n');
	fclose(fp);
	return 0;
}*/

int main(void)
{
	// char buf[1024];
	// sscanf("123456abcdefBCDEF", "%[^A-Z]", buf);
	// sscanf("123456#abcdef", "%[^#]", buf);
	// sscanf("hello, world", "%*s%s", buf);
	// sscanf("hello world", "%s", buf);
	// printf("hello world");
	// sscanf("ABCabcAB=", "%*[A-Z]%*[a-z]%[^a-z=]", buf);
	// sscanf("201*1b_-cdZA&", "%[0-9|_|--|a-z|A-Z|&|*]", buf);
	// sscanf("liwei0526vip@linuxblogs.cn", "%*[^@]@%[^.]", buf);
	// printf("%s\n", buf);
	// system("color f5");    //修改控制台颜色，第一个字符表示背景色，第二个字符表示字符颜色，如果只有一个字符则表示字符颜色，而背景色为默认颜色
	//system("cls");   //清屏
	/*char a;
	scanf("%c",&a);
	printf("%c\n",a+1);*/
	/*int a;
	scanf("%d",&a);
	if (a%10==7) printf("yes");
	else  printf("no");*/

	/*int a,b,c,d;
	scanf("%d %d %d",&a,&b,&c);
	if(a<b)
	{
		d=a;
		a=b;
		b=d;
	}
	if(a<c)
	{
		d=a;
		a=c;
		c=d;
	}
	if(b<c)
	{
		d=b;
		b=c;
		c=d;
	}
	printf("%d %d %d",a,b,c);
	_sleep(3000);
	system("cls");*/

	return 0;
}
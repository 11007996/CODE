#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#define LEN 40
int main(int argc,char * argv[])
{
	FILE *in,*out;
	int ch;
	char name[LEN];
	int count =0;

	if(argc<2)
	{
		fprintf(stderr, "Usage: %s filename\n",argv[0] );
		exit(EXIT_FAILURE);
	}

	if((in = fopen(argv[1],"r")) == NULL)  //以读模式打开文件，返回一个文件指针
	{
		fprintf(stderr,"I couldn't open the file \"%s\"\n",argv[1]);
		exit(EXIT_FAILURE);
	}
	strncpy(name,argv[1],LEN-5);   //把argv[1]复制到name，最多复制LEN-5个字节
	name[LEN-5] = '\0';   //字符串末尾加结束符
	strcat(name,".red");
	if((out = fopen(name,"w")) == NULL)   //以写模式打开文件
	{
		fprintf(stderr,"Can't create output file.\n");   //第1个参数是文件指针，用于把第2个参数发送到屏幕
		exit(3);
	}
	while((ch = getc(in))!= EOF)   //逐字节读取文件
	{
		if(count++ % 3 == 0)
			putc(ch,out);   //每3个字节写一个字节到out文件指针
	}
	if(fclose(in)!=0 || fclose(out) != 0)   //关闭两个文件指针
		fprintf(stderr,"Error in closing files\n");
	return 0;
}
#include<stdio.h>
#include<stdlib.h>
int main(int argc,char * argv[])
{
	int ch;
	FILE * fp;  //文件原型，FILE定义在stdio.h，文件指针，它指向一个包含文件信息的数据对象
	unsigned long count =0;
	if(argc !=2)
	{
		printf("Usage: %s filename\n",argv[0]);
		exit(EXIT_FAILURE);   //退出程序，EXIT_FAILURE表示结束程序失败，0或EXIT_SUCCESS表示成功结束程序，都包含在stdlib.h头文件中
	}
	if((fp = fopen(argv[1],"r")) == NULL)  //打开文件，包含在stdio.h
	{
		printf("Can't open %s\n",argv[1]);
		exit(EXIT_FAILURE);
	}
	while((ch = getc(fp))!= EOF)   //从标准输入中获取一个字符，文件末尾返回EOF
	{
		putc(ch,stdout);     //把字符ch放入stdout屏幕中
		count++;
	}
	fclose(fp);   //关闭文件
	printf("\nFile %s has %lu characters\n",argv[1],count);
	return 0;
}


fp = fopen(argv[1],"r")
返回一个文件指针
第一个参数表示待打开文件名称，包含该文件名和的字符串地址
第十个参数表示待打开文件的模式
"r" 以读模式打开文件
"w" 以写模式打开文件，把现有文件的长度截为0，如果文件不存在，则创建一个新文件
"a" 以写模式打开文件，在现在文件末尾添加内容，如果文件不存在，则创建一个新文件
"r+" 以更新模式打开文件（即可以读写文件）
"w+" 以更新模式打开文件（即读和写），如果文件存在，则将其长度截为0；如果文件不存在，则创建一个新文件
"a+" 以更新模式打开文件，在现在文件的末尾添加内容，如果文件不存在，则创建一个新文件，可以读整个文件，但是只能从末尾添加内容
"rb"/"wb"/"ab"/"ab+"/"a+b"/"wb+"/"w+b"/"ab+"/"a+b"   与上一个模式类似，但是以二进制模式而不是文本模式打开文件
"wx"/"wbx"/"w+x"/"wb+x"/"w+bx"  类似非x模式，但是如果文件已存在或以独占模式打开文件，则打开文件失败


putc(ch,fpout);   ////把字符ch放入FILE指针fpout指定的文件中
第一个参数是待写入的字符，第二个参数是文件指针


if(fclose(fp) != 0)   //如果成功关闭，函数返回0，否则返回EOF
printf("Enter in closing file %s\n",argv[1]);


stdin   文件指针，键盘标准输入
stdout  文件指针，显示器标准输出
stderr  文件指针，显示器标准输入
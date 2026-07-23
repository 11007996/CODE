#include<stdio.h>
#include<stdlib.h>
//int main(void)
//{
	/*printf("abcdefg" 
		"-aiuhege");*/
	// char a[5] = {'a','b','\0','c'};
	/*char a[5] = "abcde";
	printf("%.2s\n",a);*/

	/*char a[5] = "abcd";
	a[3] = 'e';
	printf("%s\n",a);*/

	/*int read_line(char str[],int n)     //输入整行字符串
	{
		int ch,i=0;
		while((ch=getchar())!= '\n')
			if(i<n)
				str[i++]=ch;
		str[i] = '\0';
		return i;
	}*/
	//return 0;
//}



// 命令行参数
int main(int argc,char *argv[])    //argc是命令行参数的数量(包括程序名本身)，argv是指向命令行参数的指针数组，argv[0]指向程序名
{
	// int c =atoi(*(argv+1)) + atoi(*(argv+2));
	int c =atoi(argv[1]) + atoi(argv[2]);
	printf("%d\n",c);
	return 0;
}
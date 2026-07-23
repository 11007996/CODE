#include<stdio.h>
// #define MSG "I am a symbolic string constant."
// #define MAXLENGTH 81
#define STLEN 14
#define DEF "I am a #defined string."
int main(void)
{
	/*char words[MAXLENGTH] ="I am a string in an array";
	const char * pt1 = "Something is pointing at me.";
	puts("Here are some strings:");    //puts只显示字符串，末尾自动换行
	puts(MSG);
	puts(words);
	puts(pt1);
	words[8] = 'p';
	puts(words);*/


	// printf("%s,%p,%c\n","We","are",*"space farers");

	/*char words[STLEN];
	puts("Enter a string ,please");
	gets(words);   //读取一整行输入，gets函数不会检测输入的字符串是否超过words数组长度，造成缓冲区溢出，甚至可能修改其它内存数据，造成安全隐患
	printf("Your string twice:\n");
	printf("%s\n",words);
	puts(words);
	puts("Done.");*/

	/*char words[STLEN];
	puts("Enter a string,please");
	fgets(words,STLEN,stdin);    //stdin作为标识符定义在stdio.h中，表示通过键盘读入字符
	printf("Your string twice (puts(),then fputs()):\n");
	puts(words);
	fputs(words,stdout);
	puts("Enter another string,please");
	fgets(words,STLEN,stdin);
	printf("Your string twice (puts(),then fputs()):\n");
	puts(words);
	fputs(words,stdout);  //stdout作为标识符定义在stdio.h中，表示字符要写入到屏幕上
	puts("Done.");*/



	/*char str1[180] = "An array was initialized to me.";
	const char * str2 = "A pointer was initialized to me.";

	puts("I'm an argument to puts().");
	puts(DEF);
	puts(str1);
	puts(str2);
	puts(&str1[5]);
	puts(str2 + 4);*/

	char side_a[]= "Side A";
	char dont[]= {'W','O','W','!'};
	char side_b[] = "Side B";
	puts(dont);    //以结束的空字符为结束标识，没有则继续读取内存中的数据
	return 0;
}


/*strlen("grgr")  //用于统计字符串的长度
strcat("sgrgr","sgdg")   //两个字符串拼接
strncat(but,addon,available)    //拼接第1和第2个字符器，且会检测第1个字符器空间是否能容纳下第2个字符串
strcmp(str1,str2)   //检测两个字符串是否一致，返回值是0或非0
strncmp(str1.str2,5)  //检测两个字符串在指定位置前是否一致，5表示只比较前5个字符
strcpy(sr1,str2)    //将第2个字符串复制到第1个字符串，把str2拷贝到str1的地址上，返回值为第一个字符串的地址
strcpy(sr1+2,str2)  //只拷贝到第3个字符之后、
strncpy(str1,str2,5)  //将第2个字符串复制到第1个字符串，最多拷贝5个字符，避免第1个字符串容纳不下第2个字符串
sprintf(str1,"%s,%-19s:$%6.2f\n",last,first,prize)   //将多个字符串储存到str1的地址，将多个字符串拼接
strchr("afefeed",'d')  //返回第1个字符串是否包含第2个字符串*/
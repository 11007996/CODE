#include<stdio.h>
#include<stdlib.h>
#include<limits.h>
#include<math.h>
#include<ctype.h>
#include<string.h>
#include<time.h>
// void showmessage(void);   //当函数定义在实际调用它之前，需要加函数原型声明，定义在实际调用它之后不需要加函数原型声明

/*void showmessage(void)
{
	puts("后声明");
	return;
}

int add(const int a,int b)    //const表示传入的a值不可修改
{
	return a+b;
}*/

/*#define NUMBER 5
#define FAILED -1
int search(const int v[],int key,int n)   //顺序查找
{
	int i =0;
	while(1)   //非零为true
	{
		if(i==n)
			return FAILED;
		if(v[i] == key)
			return i;  //找到数后直接退出
		i++;
	}
}
int search2(int v[],int key,int n)    //哨兵查找
{
	int i =0;
	v[n] = key;
	while(1)
	{
		if(v[i] == key)
			break;
		i++;
	}
	return (i<n)?i:FAILED;
}*/

// int main(void)
// {
	/*int a =3.25;
	int b=38.89;
	printf("a:%d b:%d\n",a,b);*/

	/*int a;
	char string[5];
	scanf("%d",&a);
	printf("%d\n",&a+4);
	printf("%p\n",&a);
	puts("打印字符串并换行");
	itoa(a,string,10);   //a整型转为字符串
	puts(string);*/

	/*double a,b;
	int c;
	scanf("%lf %lf",&a,&b);
	c = (int)(a+b);
	printf("a+b=%09.2lf\n",a+b);  //9位数以0补齐，两位小数位
	printf("a+b=%9d\n", c);   //9位数以0补齐
	printf("a+b=%-9d\n", c);   //左对齐*/

	/*int a,b,c;
	scanf("%d %d",&a,&b);
	c = (a>b)?a:b;   //true为a,false为b
	printf("%d\n",c);*/

	/*putchar('A');   //输出单个字符
	putchar('\n');*/

	/*x && y 相当于 !(!x || !y)
	x || y 相当于 !(!x && !y)*/

	// showmessage();

	/*int i,ky,idx;
	int vx[NUMBER];
	for(i=0;i<NUMBER;i++)
	{
		printf("vx[%d]:",i);
		scanf("%d",&vx[i]);   //为数组赋值
	}
	printf("要查找的值:");
	scanf("%d",&ky);
	idx = search(vx,ky,NUMBER);
	if(idx == FAILED)
		puts("\nsearch:查找失败");
	else
		printf("search:%d是数组的第%d号元素.\n",ky,idx +1);

	idx = search2(vx,ky,NUMBER);
	if(idx == FAILED)
		puts("\nsearch2:查找失败");
	else
		printf("search2:%d是数组的第%d号元素.\n",ky,idx +1);
*/


	/*int i = 9;
	{
		int i = 8;
		printf("%d\n",i);
	}
	printf("%d\n",i);*/
	// return 0;
// }


/*int i;
int main(void)
{
	auto int b;
	int c;
	int d;
	printf("%d\n",i);
	printf("%d\n",b);
	printf("%d\n",c);
	printf("%d\n",d);
	return 0;
}*/

/*int main(void)
{
	printf("%d\n",CHAR_MIN);
}*/

/*int main(void)
{
	int a=0,b=13;
	printf("%d\n",a^b);  //异或，一1一0为1
	printf("%d\n",a|b);  //按位与，一1一1为1
	printf("%d\n",a&b);  //按位或，其中一1为1
	printf("%d\n",~b+1); //1为0，0为1
	return 0;
}*/

/*int main(void)
{
	int a = 52;
	printf("%d\n",a>>3);  //向右移两位，数字减小，当a为无符数值则结果为a/(2*2*2)，注意不要对负数进行位移运算
	printf("%d\n",a<<3);  //向左移两位，数字增大，当a为无符数值则结果为a*(2*2*2)，注意不要对负数进行位移运算
	return 0;
}*/


/*int main(void)
{
	// int a = 10;
	// int b = 20;
	// printf("%d\n",(a+b)>>1);
	// printf("%d\n",(a+b)/2);

	// printf("%d\n",22>>1);  //偶数右移一位等价于除2
	// printf("%d\n",-21<<1);  //左移一位等价于乘2

	// int a = 10;
	// int b = 20;
	// a = a^b;  //a b互换
	// b = a^b;
	// a = a^b;
	// printf("%d\n",a);
	// printf("%d\n",b);
	// return 0;
}*/


/*int main(void)
{
	printf("%f\n",sqrt(9));     //开方，返回double类型
	return 0;
}*/


/*#define sqr(x) ((x)*(x))   //函数宏，在编译时就展开，可以类泛型跨数据类型运算
#define alert() (puts("无参数函数宏"))
#define alert1() {puts("复全语句函数宏");puts("复全语句函数宏");}
int main(void)
{
	printf("%d\n",sqr(3));
	printf("%f\n",sqr(1.1));
	alert();
	alert1();
	return 0;
}*/


//冒泡排序
/*int main(void)
{
	int a[6] = {6,5,4,7,92,9};
	for(int i=0;i<7;i++)
	{
		for(int j = i+1;j<6;j++)
		{
			if(a[j]>a[i])
			{
				int temp = a[i];
				a[i] = a[j];
				a[j]= temp;
			}
		}
	}
	for(int c = 0;c<6;c++)
	{
		printf("%d\n",a[c]);
	}
	return 0;
}*/


/*enum animal {dog,cat,monkey,invalid};    //枚举类型，0，1，2，3
int main(void)
{
	printf("%f\n",dog);
	printf("%d\n",cat);
	printf("%d\n",monkey);
	printf("%d\n",invalid);
	printf("%d\n",dog + cat + monkey + invalid);
	return 0;
}*/


/*void add(int n)   //递归函数
{
	if(n < 100)
	{
		n += 10;
		printf("n=%d\n",n);
		add(n);
	}
	return;
}

int main(void)
{
	int i = 0;
	scanf("%d",&i);
	add(i);
	return 0;
}*/


/*int main(void)
{
	int ch;
	while((ch = getchar())!= EOF)    //getchar从标准流中读取下一个字符，读取错误返回EOF
	{
		putchar(ch);
	}
	return 0;
}
*/



/*int main(void)
{
	printf("%d\n",'a'-'f');   //字符转换为编码后进行算数运算
	printf("%d\n",'\62');   //八进制数表示
	printf("%c\n",'\164');   //八进制数表示字符
	printf("%c\n",'\x64');   //十六进制数表示字符
	return 0;
}
*/

/*int main(void)
{
	char *chr = "hello world";
	printf("%s\n",chr);
	printf("%p\n",chr);

	char str[] = "hello!!!";
	printf("%s\n",str);
	printf("%c\n",str[0]);
	printf("%p\n",str[0]);
	printf("%p\n",*str);
	return 0;
}*/


/*int main(void)
{
	char chr[] = "abcd\0e";
	//scanf("%s",chr);
	printf("%s\n",chr);
	printf("%2s\n",chr);
	printf("%3.2s\n",chr);   //输出最小宽度3，精度2，字符串
	return 0;
}*/


/*int main(void)
{
	int i;
	char cs[][6] = {"abc","def","ghj","kl"};
	for(i=0;i<4;i++)
	{
		printf("cs[%d]%s\n",i,cs[i]);
		printf("cs[%d]%c\n",i,cs[i][0]);
	}

	return 0;
}*/

/*int main(void)
{
	// char cs[][6] = {"aaa","bbb","ccc"};
	char cs[6] = "abc\0de";
	int len = 0;
	while(cs[len])
	{
		len++;
	}
	printf("长度：%d\n",len);    //求字符串长度

	int a = 0;
	while(cs[a])
	{
		// putchar(cs[a]);    //显示字符串
		putchar(cs[a++]);    //显示字符串
		a++;
	}
	putchar('\n');
	return 0;
}*/


/*int main(void)
{
	char cs[] = "ab14cb2ef3";
	int i = 0;
	int cnt = 0;
	while(cs[i])
	{
		if(cs[i]> '0' && cs[i]<'9')   //统计字符串中数字的个数
			cnt++;
		i++;
	}
	printf("%d\n",cnt);
	return 0;
}*/



/*
void str_toupper(char s[])
{
	int i=0;
	while(s[i])
	{
		s[i] = toupper(s[i]);   //转为大写
		i++;
	}
}

void str_tolower(char s[])
{
	int i =0;
	while(s[i])
	{
		s[i] = tolower(s[i]);    //转为小写
		i++;
	}
}

int main(void)
{
	char str[128];
	printf("请输入字符串：");
	scanf("%s",str);
	str_toupper(str);
	printf("大写字母：%s\n",str);
	str_tolower(str);
	printf("小写字母：%s\n",str);
	return 0;
}
*/

/*void put_strary(const char s[][6],int n)
{
	int i;
	for(i=0;i<n;i++)
	{
		printf("s[%d] -- \"%s\"\n",i,s[i]);
	}
}

int main(void)
{
	char cs[][6] = {"Turbc","CA","DOHC","AAAAAA"};  //"AAAAAA"超过6个字符长度，此时它不算字符串，只能算是字符集合，因为未必没有'\0'
	put_strary(cs,4);
	return 0;
}*/


/*#define STR_LENGTH 128
void put_string_rep(const char s[])
{
	int i =0;
	while(s[i])
	{
		putchar(s[i++]);
	}
	printf(" { ");
	i = 0;
	while(s[i])
	{
		putchar('"');
		putchar(s[i++]);
		printf("'");
	}
	printf("'\\0' }\n");
}
int main(void)
{
	int i;
	char s[STR_LENGTH];
	char ss[5][STR_LENGTH];
	printf("字符串s:");
	scanf("%s",s);
	printf("请输入5个字符串。\n");
	for(i=0;i<5;i++)
	{
		printf("ss[%d]:",i);
		scanf("%s",ss[i]);
	}
	printf("字符串s:");
	put_string_rep(s);
	printf("字符串数组ss\n");
	for(i=0;i<5;i++)
	{
		printf("ss[%d]:",i);
		put_string_rep(ss[i]);
	}
	return 0;
}*/



/*int main(void)
{
	int sato = 178;
	int sanaka = 175;
	int masaki= 179;

	int *isako,*hiroko;
	isako=&sato;
	hiroko = &masaki;   //对象的地址
	printf("*isako:%d\n",*isako);
	printf("*hiroko:%d\n",*hiroko);    //地址的对象
	isako = &sanaka;
	*hiroko = 180;
	putchar('\n');
	printf("sato:%d\n",sato);
	printf("sanaka:%d\n",sanaka);
	printf("masaki:%d\n",masaki);
	printf("*isako:%d\n",*isako);
	printf("*hiroko:%d\n",*hiroko);
	return 0;
}*/


/*void sum_diff(int n1,int n2,int *sum,int *diff)
{
	*sum = n1+n2;
	*diff = (n1>n2)?n1-n2:n2-n1;
}
int main(void)
{
	int na,nb;
	int wa=0,sa=0;
	scanf("%d",&na);
	scanf("%d",&nb);
	sum_diff(na,nb,&wa,&sa);    //指针传值
	printf("sum:%d  diff:%d\n",wa,sa);
	return 0;
}*/

/*int main(void)
{
	int a[3] = {10,11,12};
	int *p = a;

	printf("%d\n",p[1]);     //p[1]、*(p+1)、*(a+1)三个值相等，都是a[1]的别名
	printf("%d\n",1[p]);
	printf("%d\n",*(p+1));
	printf("%d\n",*(a+1));
	printf("%d\n",a[1]);
	printf("%d\n",1[a]);

	printf("%d\n",*p);
	printf("%d\n",*a);


	printf("%p\n",&a[1]);   //都指向同一个地址
	printf("%p\n",&p[1]);
	printf("%p\n",a+1);
	printf("%p\n",p+1);


	printf("%d\n",(p+2)-(p));  //指针可以做减法，表示两个指针的距离

	return 0;
}*/


/*int main(void)
{
	char * p = "abcde";
	printf("%c\n",p[1]);
	p = "123";
	printf("%s\n",p);
	return 0;

}*/





/*char* str_copy(char *d,const char *s)
{
	char *t = d;
	while(*d++ = *s++);  //先赋值，再递增，再判断执行，赋给d的值为\0则退出循环
	return t;
}
// int str_copy(char *d,const char *s)
// {
// 	while(*d++ = *s++);  //先赋值，再递增，再判断执行，赋给d的值为\0则退出循环
// 	return 0;
// }
int main(void)
{
	char str[128] = "ABC";    //注意数组的长度，防止溢出内存修改到其它地址
	char tmp[128];
	char *t;
	printf("str = \"%s\"\n",str);
	printf("复制的是：",tmp);
	scanf("%s",tmp);
	t=str_copy(str,tmp);   //函数返回指针
	puts("复制了");
	printf("str = \"%s\"\n",str);
	printf("*t = \"%s\"\n",t);
	return 0;
}*/


/*int main(void)
{
	char a[] = "abcd";
	char *p = "sdfgdre";
	int len =strlen(a);    //数组字符串长度
	int len2 =strlen(p);   //指针字符串长度
	printf("%d\n",len);
	printf("%d\n",len2);

	//char *strcpy(char *s1,const char *s2);   //将s2的值复制到s1
	//char *strncpy(char *s1,const char *s2,size_t n);  //将s2的值复制到s1,最多复制n个字符

	//char *strcat(char *s1,const char *s2)   //连接两个字符串
	//char *strncat(char *s1,const char *s2,size_t n)   //连接两个字符串，连接后最大长度为n

	//int strcmp(const char *s1,const char *s2)  //比较两个字符串大小，s1大于s2返回正整数，s1小于s2返回负整数，相等返回0
	//int strncmp(const char *s1,const char *s2,size_t n)  //比较两个字符串前n个字符的大小

	//int atoi(const char *nptr)   //将字符串转换为Int整型
	// int atol(const char *nptr)   //将字符串转换为Long整型
	// double atof(const char *nptr)  //将字符串转换为double浮点型
	return 0;
}*/

/*#define NAME_LEN 64
struct student     //结构体
{
	char name[NAME_LEN];
	int height;
	float weight;
	long schols;
};

// typedef为结构体定义一个同义词,struct student可简写为Student
// typedef struct student //定义同义词时，结构中student可以省略
// {
// 	char name[NAME_LEN];
// 	int height;
// 	float weight;
// 	long schols;
// }Student;

void showname(struct student *stu)    //结构体参数
{
	printf("姓名T = %s\n",(*stu).name);  //.运算符获取结构成员
	printf("身高T = %d\n",stu->height);  //箭头运算符获取结构成员
}

int main(void)
{
	//初始化一
	struct student sanaka;
	strcpy(sanaka.name,"Sanaka");
	sanaka.height = 175;
	sanaka.weight = 52.5;
	sanaka.schols = 73000;

	//初始化二
	struct student takao = {"Takao",173,86.21,1000};

	printf("姓名 = %s\n",sanaka.name);
	printf("身高 = %d\n",sanaka.height);
	printf("体重 = %lf\n",sanaka.weight);
	printf("奖学金 = %ld\n",sanaka.schols);
	printf("姓名T = %s\n",takao.name);
	showname(&takao);   //结构体作为函数参数
	return 0;
}
*/


/*//读取文件内容
int main(void)
{
	char content[128] = {0};
	FILE *fp;
	fp = fopen("D:\\zzzzzzzz\\CodeBlock\\C_C++\\bookList_C\\11.txt","r");
	if(fp == NULL)
	{
		puts("打开失败");
	}
	else
	{
		puts("打开成功");
		fscanf(fp,"%s",content);      //读取文件内容
		printf("内容：%s\n",content);
		fclose(fp);
	}
	return 0;
}*/


/*int main(void)
{
	char content[128] = "FGFGFGFG";
	char con[50];
	FILE *fp,*fp2;
	fp = fopen("D:\\zzzzzzzz\\CodeBlock\\C_C++\\bookList_C\\11.txt","a");
	if(fp == NULL)    //打开文件失败返回NULL
	{
		puts("打开失败");
	}
	else
	{
		puts("打开成功");
		fprintf(fp,"%s",content);    //读取文件内容
		// printf("内容：%s\n",content);
		fclose(fp);

		fp2 = fopen("D:\\zzzzzzzz\\CodeBlock\\C_C++\\bookList_C\\11.txt","r");
		fscanf(fp2,"%s",con);      //写入文件内容
		// fscanf(stdin,"%d",&i) 相当于 scanf("%d",&i)
		// fprintf(stdout,"%d",i) 相当于 printf("%d",i)
		printf("内容：%s\n",con);
		fclose(fp2);
	}
	return 0;
}*/

/*//获取当前时间
int main(void)
{
	time_t current = time(NULL);
	struct tm *timer = localtime(&current);
	char *wday_name[] = {"日","一","二","三","四","五","六"};
	printf("当前日期和时间为%d年%d月%d日(%s)%d时%d分%d秒。\n",
		timer->tm_year+1900,
		timer->tm_mon + 1,
		timer->tm_mday,
		wday_name[timer->tm_wday],
		timer->tm_hour,
		timer->tm_min,
		timer->tm_sec
		);
	return 0;
}*/


/*int main(void)
{
	int ch;
	FILE *fp;
	char fname[FILENAME_MAX]="D:\\zzzzzzzz\\CodeBlock\\C_C++\\bookList_C\\11.txt";
	printf("文件名:");
	// scanf("%s",fname);
	if((fp = fopen(fname,"r")) == NULL)
		printf("文件打开失败。\n");
	else
	{
		while((ch = fgetc(fp)) != EOF)   //按字符读取文件
			putchar(ch);
		putchar('\n');
		fclose(fp);
	}
	return 0;
}*/


/*int main(void)
{
	int ch;
	FILE *sfp;
	FILE *dfp;
	char sname[FILENAME_MAX];
	char dname[FILENAME_MAX];

	printf("打开原文件:");   scanf("%s",sname);
	printf("打开目标文件:");   scanf("%s",dname);

	if((sfp = fopen(sname,"r")) = NULL)
		printf("原文件打开失败\n");
	else
	{
		if((dfp = fopen(dname,"w"))==NULL)
			printf("目标文件打开失败\n");
		else
		{
			while((ch = fgetc(sfp))!=EOF)    //读取流字符以字符编码返回
				fputc(ch,dfp);    //以字符编码形式写入流
			fclose(dfp);
		}
		fclose(sfp);
	}
	return 0;
}*/



/*size_t fwrite(const void *ptr,size_t size,size_t nmemb,FILE *stream);    //向stream流中从ptr数组中写入最多nmemb个字符，返回写入的字符长度
size_t fread(const void *ptr,size_t size,size_t nmemb,FILE *stream);    //从stream流中读取最多nmemb长度的字符存入ptr变量，返回读取的元素个数
第1个参数是要指向读写数据的首地址的指针，第2个参数是数据类型的长度，第3个是要读写的长度，第4个参数是指向读写对象的流的指针
fwrite(&pi,sizeof(double),1,fp);*/

/*int main(void)
{
	char chr[3] = {0};
	FILE *fp;
	if((fp = fopen("D:\\zzzzzzzz\\CodeBlock\\C_C++\\bookList_C\\11.txt","r")) ==NULL)
		puts("读取失败");
	else
	{
		fread(&chr,sizeof(char),3,fp);   //读取文件3个字符
		printf("读取：%s",chr);
	}
	return 0;
}*/


/*int main(void)
{
	char c = 'b';
	if(isprint(c))   //判断是否是可打印字符
	{
		int a =printf("%06c\n",c);   //输出错误返回负值
		printf("%d\n",a);
	}
	else
	{
		puts("非打印字符");
	}
}*/

/*int main(void)
{
	int ch;
	FILE *fp;
	char fname(FILENAME_MAX);
	printf("目标文件名");
	scanf("%s\n",fname);
	if((fp = fopen(fbane,"w")) == NULL)   //打开文件时文件数据放入标准输入流
	{
		printf("无法打开目标文件");
	}
	else
	{
		while((ch = fgetc(stdin)) != EOF)   //stdin标准输入流
		{
			fputc(ch,fp);
		}
		fclose(fp);
	}
	return 0;
}*/
























//在函数外声明的变量为文件作用域，可以在文件的任何位置调
// extern 关键字在函数内使用，表示调文件作用域中的变量，可以省略
// 在函数中不使用存储说明符static定义出的对象，被赋予自动存储期，不进行初始化会被赋予不确定的值
// 在函数中使用存储说明符static定义出的对象，或者在函数外声明定义出来的对象都被赋予静态存储期，不进行初始化会被赋予0
// 静态变量会在main函数之前被初始化，即使静态变量定义在main函数之后
// 字符串字面量具有静态存储期，因此它存在从程序开到结束的整个生命周期

// 什么也不指向的特殊指针是空指针，表示空指针的对象式宏NULL是空指针常量
// 对于使用register关键字声明的的寄存器对象不能加上取址运算符&
// 数组名原则上会被解释为指向该数组起始元素的指针
// 指针p+i和&a[i]是等价的
// 指向元素e后第i个元素的*(p+i),可以写成p[i],指向元素e前第i个元素的*(p-i),可以写成p[-i],所有p[2]也是数组a[2]的别名
// 数组名a是指向起始元素a[0]的指针，所有a+1是指向第2个元素a[1]的指针，即*(a+1)=a[1]
// 有些编译器不允许修改指针字符串字面量，因此不要必定字符串字面量，也不要对超过字符串字面量的内容空间进行写入操作
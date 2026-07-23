#include <stdio.h>

/*struct bitwei    //位域,位域没有地址
{
	unsigned short int day:5;
	unsigned short month:4;
	unsigned short year:7;
}ymd;


struct nian
{
	unsigned short day;
	unsigned short month;
	unsigned short year;
}nian;


int main(void)
{
	printf("ymd:%d",sizeof(ymd));
	printf("ymd:%d",sizeof(nian));
	return 0;
}*/

/*struct file_date
{
	unsigned short int day:5;
	unsigned short int month:4;
	unsigned short int year:7;
};

union int_date   //同一块内存两种方式进行解析
{
	unsigned short i;
	struct file_date fd;
};

void print_date(unsigned short n)
{
	union int_date u;
	u.i = n;
	printf("%d/%d/%d\n",u.fd.month,u.fd.day,u.fd.year+1980);
}

int main(void)
{
	print_date(810);   //本电脑CPU是按小端存储，数据从右向左存储
	return 0;
}*/


//volatile char *p;    //volatile表示p指向的内存是易变内存，即频繁更改内存中的数据，这就告知编译器每次读取数据时必须从内存中读取而不能从缓存中读取
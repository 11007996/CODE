// void dribble(char *bits);    //函数重载只与不带const参数匹配，因为程序设置了两个重载
// void dribble(const char *bites);   //函数重载只与带const参数匹配
// void dabble(char *bits);   //函数重载只与不带const参数匹配，因为带const参数不能赋值给不带const的参数
// void drivale(const char * bits);   //函数重载带与不带const参数都匹配，因为程序只有一个函数原型

#include<iostream>
unsigned long left(unsigned long num,unsigned ct);   //left两个重载
char * left(const char * str,int n =1);

int main()
{
	using namespace std;
	const char trip[] = "Hawaii!!";
	unsigned long n = 12345678;
	int i;
	char * temp;
	for(i =1;i<10;i++)
	{
		cout << left(n,i) << endl;
		temp = left(trip,i);
		cout << temp << endl;
		cout << temp << endl;
		delete [] temp;
	}
	return 0;
}

unsigned long left(unsigned long num,unsigned ct)
{
	unsigned digits =1;
	unsigned long n = num;
	if(ct ==0 || num ==0)
		return 0;
	while(n /= 10)
		digits++;
	if(digits > ct)
	{
		ct = digits - ct;
		while(ct--)
			num /= 10;
		return num;
	}
	else
	{
		return num;
	}
}

char * left(const char * str,int n)
{
	if(n<0)
		n=0;
	char * p = new char[n+1];
	int i;
	for(i=0;i<n && str[i];i++)
		p[i] = str[i];
	while(i<=n)
		p[i++] = '\0';
	return p;
}
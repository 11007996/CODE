/*#include<stdio.h>
extern unsigned in rando(void);
int main(void)
{
	int count;
	for(count=0;count<5;count++)
	{
		printf("%d\n",rando());
	}
	return 0;
}


#include<stdio.h>
static unsigned long int next=1;
unsigned int rando(void)
{
	next=next*1103515245 + 12345;
	return (unsigned int)(next/65536)%32758;
}*/
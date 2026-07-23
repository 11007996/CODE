#include <iostream>
int main()
{
	using namespace std;
	int nights = 1001;
	int *pt = new int;
	*pt = 1001;

	cout << "nithgs value = ";
	cout << nights << ":location =" << pt << endl;
	cout << "int ";
	cout << "value = " << *pt << ":location = " << pt << endl;
	double *pd = new double;   //等价于C风格 double *pd = malloc(siezof(doubel))   内存存在于堆中
	*pd = 1000001.0;

	cout << "doubel ";
	cout << "value = " << *pd << ":location = " << pd << endl;
	cout << "location of pointer pd:" << &pd << endl;
	cout << "size of *pt = " << sizeof(*pt) << endl;
	cout << "size of pd = " << sizeof pd;
	cout << ":size of *pd = " << sizeof(*pd) << endl;
	delete pt;   //释放内存,一个内存有两个指针则只能释放一次
	delete pd;
	return 0;
}


//被分配的内存不释放,再也无法使用了,将发生内存泄漏
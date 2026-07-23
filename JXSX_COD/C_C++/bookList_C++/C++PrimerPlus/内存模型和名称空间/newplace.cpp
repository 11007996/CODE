// 程序连续使用多次某个变量的值，编译器可能会将此变量缓存到寄存器(这属于编译器的优化)，但同时变量可能被其它语句修改值，此时缓存中值成了脏值，使用volatile告诉编译器此变量多变，避免编译器对变量进行优化


/*struct data
{
	char name[50];
	mutable int accesses;
};
//const关键字使结构veep不可修改，但mutable关键字使结构中的accesses成员可以修改，增加了程序的灵活性
const struct data veep = { "abcd",1};*/



#include<iostream>
#include<new>  //定位new运算符头文件

const int BUF = 512;
const int N = 5;
char buffer[BUF];
int main()
{
	using namespace std;
	double *pd1,*pd2;
	int i;   //自动变量
	cout << "Calling new and placement new:\n";
	pd1 = new double[N];  //动态存储
	pd2 = new (buffer)double[N];   //定位new运算符，将double数组存放在buffer的内存地址中，也就是将double数组存放在指定的内存地址上，此时double数组和buffer内存地址是一样的，强制类型转换
	for(i=0;i<N;i++)
		pd2[i] = pd1[i] = 1000 + 20.0 * i;
	cout << "Memory addresses:\n" << " heap: " << pd1
	     << " static: " << (void *) buffer << endl;
	cout << "Memory contents:\n";
	for(i =0;i< N;i++)
	{
		cout << pd1[i] << " at " << &pd1[i] << "; ";
		cout << pd2[i] << " at " << &pd2[i] << endl;
	}
	cout << "\nCalling new and placement new a second time:\n";
	double *pd3, *pd4;
	pd3 = new double[N];
	pd4 = new (buffer)double[N];
	for(int i=0;i<N;i++)
	{
		pd4[i] = pd3[i] = 1000 + 40.0 *i;
	}
	cout << "Memory contents:\n";
	for(i =0;i<N;i++)
	{
		cout << pd3[i] << " at " << &pd3[i] << "; ";
		cout << pd4[i] << " at " << &pd4[i] << endl;
	}
	cout << "\nCalling new and placement new a third time:\n";
	delete [] pd1;
	pd1 = new double[N];
	pd2 = new (buffer + N * sizeof(double)) double[N];
	for(i=0;i<N;i++)
		pd2[i] = pd1[i] = 1000+60.0*i;
	cout << "Memory contents:\n";
	for(i=0;i<N;i++)
	{
		cout << pd1[i] << " at " << &pd1[i] << "; ";
		cout << pd2[i] << " at " << &pd2[i] << endl;
	}
	delete [] pd1;
	delete [] pd3;
	return 0;
}
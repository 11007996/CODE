#include<iostream>
void swapr(int & a,int & b);
void swapp(int *p,int * q);
int main()
{
	using namespace std;
	int wallet1 = 300;
	int wallet2 = 350;
	cout << "wallet1= " << wallet1;
	cout << "wallet2= " << wallet2 << endl;

	cout << "Using references to swap contents:\n";
	swapr(wallet1,wallet2);    //通过引用传参
	cout << "wallet1= " << wallet1;
	cout << "wallet2= " << wallet2 << endl;

	cout << "Using pointers to swap contents again:\n";
	swapp(&wallet1,&wallet2);   //通过指针传参
	cout << "wallet1= " << wallet1;
	cout << "wallet2= " << wallet2 << endl;

	return 0;
}

void swapr(int & a,int & b)
{
	int temp;
	temp =a;
	a =b;
	b=temp;
}

void swapp(int * p,int * q)
{
	int temp;
	temp =*p;
	*p =*q;
	*q=temp;
}


--对于使用传递的值而不作修改的函数
如果数据对象很小,如内置数据类型或小型结构,则按值传递
如果数据对象是数组,则使用指针,因为这是唯一的选择,并将指针声明为const指针
如果数据对象是较大的结构,则使用const指针或const引用,以提高程序的效率,这样可以节省复制结构所需的时间和空间
如果数据对象是类对象,则使用const引用,类设计的语言常常要求使用引用,传递类对象参数的标准方式是按引用传递

--对于修改调用函数中的数据的函数
如果数据对象是内置的数据类型,则使用指针
如果数据对象是为数据,则只能使用指针
如果数据对象是结构,则使用引用或指针
如果数据对象是类对象,则使用引用
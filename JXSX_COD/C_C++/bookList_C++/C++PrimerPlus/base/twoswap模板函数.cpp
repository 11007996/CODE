#include <iostream>

template <typename T> void Swap(T &a,T &b);   //普通模板,默认为隐式实例

// 编译器在选择原型时,非模板版本优先于显式具体化和模板版本,而显式具体化优先于使用模板生成的版本
// 函数定义 > 显式具体化 > 普通模板的显式实例化 > 普通模板
// 不能在同一文件中使用同一种类型的显式实例和显式具体化
template void Swap<int>(int &a,int &b);   //普通模板的显式实例化
// template <> void Swap<int>(int &,int &);  //显式具体化
struct job
{
	char name[20];
	double salary;
	int floor;
};


template <> void Swap<job>(job &j1,job &j2);   //显式具体化,具体化是模板的具体化,因此在具体化之前必须要定义好模板
// template <> void Swap(job &j1,job &j2);   //可省略写<job>,与上一句是等价的

void Show(job &j);

int main()
{
	using namespace std;
	cout.setf(ios_base::fixed,ios_base::floatfield);
	int i = 10,j = 20;
	cout << "i,j = " << i << ", " << j << ".\n";
	Swap<int>(i,j);  //具体化
	cout << "Now i,j = " << i << ", " << j << ".\n";

	job sue = {"Sussan Yaffee",73000.60,7};
	job sidney = {"Sidney Taffee",78060.72,9};
	cout << "Before job swaping:\n";
	Show(sue);
	Show(sidney);
	 Swap<job>(sue,sidney);  //显式具体化
	 // Swap(sue,sidney);   //简写具体化实例化
	cout << "After job swappingL\n";
	Show(sue);
	Show(sidney);
	return 0;
}

template <typename T> void Swap(T &a,T &b)   //普通模板只能访问内置的数据类型
{
	T temp;
	temp = a;
	a = b;
	b = temp;
}


/*template <> void Swap<int>(int &a,int &b)   //具体化既能访问内置的数据类型,又能访问自定义的数据类型,如结构和联合
{
	int temp;
	temp = a;
	a = b;
	b = temp;
}*/


// 如果函数有多个原型,则编译器在选择原型时,非模板版本优先于显式具体化和模板版本,而显式具体化优先于使用模板生成的版本
// template <> void Swap(job &j1,job &j2)  //模板隐式具体化,
template <> void Swap<job>(job &j1,job &j2)   //模板显式具体化,可以访问结构的成员,具体化才可以访问自定义的数据类型,模板函数重载
{
	double t1;
	int t2;
	t1=j1.salary;
	j1.salary = j2.salary;
	j2.salary = t1;
	t2 = j1.floor;
	j1.floor = j2.floor;
	j2.floor = t2;
}

void Show(job &j)
{
	using namespace std;
	cout << j.name << ": $" << j.salary
	     << " on floor " << j.floor << endl;
}




/*template<class T1,class T2>
void ft(T1 x,T2 y)
{
	// 因为编译器不能预知x+y是什么数据类型，所有需要一个机制提前设置x+y的数据类型
	decltype(x+y) xpy = x+y;      //decltype关键字表示声明一个xpy参数，它的数据类型与x+y一致
}

//后置返回类型，表示函数返回double
auto h(int x,float y) -> double
{
	// function doby
}

// 因为编译器不能预知返回x+y是什么数据类型，所以将返回类型后置，auto在此处起一个占位符作用
template<class T1,class T2>
auto gt(T1 x,T2 y) -> decltype(x+y)
{
	return x+y;
}*/
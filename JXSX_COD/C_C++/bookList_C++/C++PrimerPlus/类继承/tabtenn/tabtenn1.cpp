#include"tabtenn1.h"
#include <iostream>

//参数列表语法
TabletennisPlayer::TabletennisPlayer(const string & fn,const string & ln,bool ht) : firstname(fn),lastname(ln),hasTable(ht){}

void TabletennisPlayer::Name() const
{
	std::cout << lastname << ", " << firstname;
}

// 派生类构造函数，继承基类
// 参数列表将参数传给基类的构造函数，先调用基类的构造函数实例一个基类对象，再调用派生类原构造函数创建派生类对象
// 创建派生类对象时，程序首先创建基类对象。从概念上说，这意味着基类对象就当在程序进行派生构造函数之前被创建，C++通过使用成员初始化列表语法来完成这种工作。
// 派生类需要自己的构造函数
// 派生类不能直接访问基类的私有成员，而必须通过基类的方法进行访问
RatedPlayer::RatedPlayer(unsigned int r,const string & fn,const string & ln,bool ht) :TabletennisPlayer(fn,ln,ht)
{
	rating = r;
}

// 首先调用基类的复制构造函数(没有定义则调用编译器自动生成的复制构造函数)，用于作为参数调用基类的构造函数
RatedPlayer::RatedPlayer(unsigned int r,const TabletennisPlayer & tp):TabletennisPlayer(tp),rating(r){}

/*派生类构造函数：
1.首先创建基类对象
2.派生类构造函数应通过成员初始化列表将基类信息传递给基类构造函数
3.派生类构造函数应初始化派生类新增的数据成员*/


/*创建派生类对象时，程序首先调用基类的构造函数，然后再调用派生类构造函数。
基类构造函数负责初始化继承的数据成员，派生类构造函数主要用于初始化新增的成员
派生类的构造函数问题调用一个基类的构造函数
可能使用初始化器列表语法指明要使用的基类构造函数，否则将使用默认的基类构造函数
派生类对象过期时，程序将首先调用派生类析构函数，然后再调用基类析构函数*/


/*基类指针可以在不进行显式类型转换的情况下指向派生类的对象；基类引用可以在不进行显式类型转换的情况下引用派生类对象
具体的可能隐式转为广泛的，基类范围更大，如直线继承自线，直线就可以隐式转换为线，但转换后线不具有直接我有的属性
基类指针或引用只能用于调用基类方法*/
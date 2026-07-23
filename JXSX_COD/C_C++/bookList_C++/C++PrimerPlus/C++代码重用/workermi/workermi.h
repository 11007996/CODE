// 如果类从不同的类那里继承了两个或更多的同名成员，则使用该成员名时，如果没有和类名进行限定，将导致二义性。但如果是虚基类，则这样做不一定会导致二义性，越靠近低层基类的函数优先级越高，如果某个名称优先于其他所有名称，则使用它时，即使不使用限定符，也不会导致二义性
// 如B和C都继承于A,D又继承于B和C，如果它们都有同名称的函数，则优先使用A的函数定义



#ifndef WORKERMI_H
#define WORKERMI_H
#include <string>

class Worker
{
private:
	std::string fullname;
	long id;
protected:
	virtual void Data() const;
	virtual void Get();
public:
	Worker() : fullname("no one"),id(0L){}
	Worker(const std::string & s,long n):fullname(s),id(n){}
	virtual ~Worker() = 0;    //虚析构函数
	virtual void Set() =0;   //纯虚构函数 ，抽象类
	virtual void Show() const =0;	
};

// 虚基类，解决多重继承多个基类对象的问题
class Waiter : virtual public Worker
{
private:
	int panache;
protected:
	void Data() const;
	void Get();
public:
	Waiter() : Worker(),panache(0){}
	Waiter(const std::string & s,long n,int p =0):Worker(s,n),panache(p){}
	Waiter(const Worker & wk,int p =0):Worker(wk),panache(p){}
	void Set();
	void Show() const;
};

// 虚基类
class Singer : virtual public Worker
{
protected:
	enum {other,alto,contralto,soprano,baxx,baritone,tenor};
	enum {Vtypes = 7};
	void Data() const;
	void Get();
private:
	static char *pv[Vtypes];
	int voice;
public:
	Singer():Worker(),voice(other){}
	Singer(const std::string & s,long n,int v = other):Worker(s,n),voice(v){}
	Singer(const Worker & wk,int v = other):Worker(wk),voice(v){}
	void Set();
	void Show() const;
};

//公有多重继承
class SingingWaiter : public Singer,public Waiter
{
protected:
	void Data() const;    //继承虚基类，可以重写函数
	void Get();
public:
	SingingWaiter(){}
	SingingWaiter(const std::string & s,long n,int p =0,int v = other):Worker(s,n),Waiter(s,n,p),Singer(s,n,v){}  //必须调用Worker构造函数，没有则调用默认Worker构造函数，解决多重继承多个基类的问题
	SingingWaiter(const Worker & wk,int p =0,int v = other):Worker(wk),Waiter(wk,p),Singer(wk,v){}    //必须调用Worker构造函数，没有则调用默认Worker构造函数，解决多重继承多个基类的问题
	SingingWaiter(const Waiter & wk,int v = other):Worker(wk),Waiter(wk),Singer(wk,v){}   //必须调用Worker构造函数，没有则调用默认Worker构造函数，解决多重继承多个基类的问题
	SingingWaiter(const Singer & wt,int p =0):Worker(wt),Waiter(wt,p),Singer(wt){}   //必须调用Worker构造函数，没有则调用默认Worker构造函数，解决多重继承多个基类的问题，两个基类共享一个Worker基类

	void Set();
	void Show() const;	
};
#endif
/*private和protected之间有区别只有在基类派生的类中才会表现出来，派生类的成员可以直接访问基类的保护成员，但不能直接访问基类的私有成员，因此对于外部世界来说，保护成员的行为与私有成员相似，但对于派生类来说，保护成员的行为与公有成员相似。

最好对类数据成员采用私有访问控制，不要使用保护访问控制，同时通过基类方法使派生类能够访问基类数据


当类声明中包含纯虚函数时，则不能创建该类的对象，包含纯虚函数的类只用作基类，也就是抽象类*/


#ifndef ACCTABC_H_
#define ACCTABC_H_
#include <iostream>
#include <string>
class AcctABC
{
private:
	std::string fullName;
	long acctNum;
	double balance;
//受保护的成员结构和函数
// 只能由派生类访问
protected:
	//类中包含成员结构
	struct Formatting
	{
		std::ios_base::fmtflags flag;
		std::streamsize pr;
	};
	const std::string & FullName() const {return fullName;}
	long AcctNum() const{return acctNum;}
	Formatting SetFormat() const;
	void Restore(Formatting & f) const;
public:
	AcctABC(const std::string & s = "Nullbody", long an = -1,double bal = 0.0);
	void Deposit(double amt);
	//纯虚函数，类为抽象类，不可实例化可能继承
	virtual void Withdraw(double amt) =0;
	double Balance() const {return balance;}
	virtual void ViewAcct() const =0;
	virtual ~AcctABC() {}	//虚析构函数
};

//类继承
class Brass:public AcctABC
{
public:
	Brass(const std::string & s = "Nullbody",long an = -1,double bal = 0.0) : AcctABC(s,an,bal){}
	virtual void Withdraw(double amt);
	virtual void ViewAcct() const;
	virtual ~Brass() {};
};

class BrassPlus:public AcctABC
{
private:
	double maxLoan;
	double rate;
	double owesBank;
public:
	BrassPlus(const std::string & s = "Nullbody",long an = -1,double bal = 0.0,double ml = 500,double r = 0.10);
	BrassPlus(const Brass & ba,double ml = 500,double r = 0.1);
	virtual void ViewAcct() const;
	virtual void Withdraw(double amt);
	void ResetMax(double m){maxLoan = m;}
	void ResetRate(double r){rate = r;}
	void ResetOwes(){owesBank = 0;}
};
#endif
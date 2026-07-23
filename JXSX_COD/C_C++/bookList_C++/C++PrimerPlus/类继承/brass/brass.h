#ifndef BRASS_H_
#define BRASS_H_
#include <string>
class Brass
{
private:
	std::string fullName;
	long acctNum;
	double balance;
public:
	Brass(const std::string & s = "Nullbody",long an =-1,double bal = 0.0);
	void Deposit(double amt);
	virtual void Withdraw(double amt);
	double Balance() const;
	virtual void ViewAcct() const;
	virtual ~Brass() {}	   //虚析构函数，有虚方法就声明一个虚析构函数，这是一种惯例
};

class BrassPlus : public Brass
{
private:
	double maxLoan;
	double rate;
	double owesBank;
public:
	BrassPlus(const std::string & s = "Nullbody",long an = -1,double bal = 0.0,double ml = 500,double r = 0.11125);
	BrassPlus(const Brass & ba,double ml = 500,double r = 0.11125);
	virtual void Withdraw(double amt);   //虚方法原型
	virtual void ViewAcct() const;
	void ResetMax(double m) {maxLoan = m;}
	void ResetRate(double r) {rate = r;}
	void ResetOwes() {owesBank =0;}	
};
#endif
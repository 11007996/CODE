#include <iostream>
#include "brass.h"
using std::cout;
using std::endl;
using std::string;

typedef std::ios_base::fmtflags format;
typedef std::streamsize precis;
format setFormat();   //全局函数原型
void restore(format f,precis p);

Brass::Brass(const string & s, long an ,double bal)
{
	fullName = s;
	acctNum = an;
	balance = bal;
}

void Brass::Deposit(double amt)
{
	if(amt < 0)
		cout << "Negative deposit not allowed; "
			 << "deposit is cancelled.\n";
	else
		balance += amt;
}

void Brass::Withdraw(double amt)
{
	format initialState = setFormat();
	precis prec = cout.precision(2);
	if(amt < 0)
		cout << "withdrawal amout must be positive; "
			 << "Withdraw canceled.\n";
	else if(amt <= balance)
		balance -= amt;
	else
		cout << "withdrawal amount of $" << amt
			 << " exceeds your balance.\n"
			 << "withdrawal cancelled.\n";
	restore(initialState,prec);
}

double Brass::Balance() const
{
	return balance;
}

void Brass::ViewAcct() const
{
	format initialState = setFormat();
	precis prec = cout.precision(2);
	cout << "Client: " << fullName << endl;
	cout << "Account Number: " << acctNum << endl;
	cout << "Balance: $" << balance << endl;
	restore(initialState,prec);
}

//派生类构造函数，初始化参数列表调用基类构造函数
BrassPlus::BrassPlus(const string & s,long an,double bal,double ml,double r) :Brass(s,an,bal)
{
	maxLoan = ml;
	owesBank = 0.0;
	rate = r;
}

BrassPlus::BrassPlus(const Brass & ba, double ml,double r):Brass(ba)
{
	maxLoan = ml;
	owesBank = 0.0;
	rate = r;
}

//派生类虚方法重写
//调用基类中的公有方法显示基类的私有字段，再通过自身显示派生类的新增字段
void BrassPlus::ViewAcct() const
{
	format initialState = setFormat();
	precis prec = cout.precision(2);

	Brass::ViewAcct();  //运用作用域符调用基类的公有方法
	cout << "Maximum loan: $" << maxLoan << endl;
	cout << "Owed to bank: $" << owesBank << endl;
	cout.precision(3);
	cout << "Loan Rate: " << 100 * rate << "%\n";
	restore(initialState,prec);   //派生类内联函数
}
//派生类虚方法重写
void BrassPlus::Withdraw(double amt)
{
	format initialState = setFormat();
	precis prec = cout.precision(2);
	double bal = Balance();   //调用基类的公有函数
	if(amt < bal)
		Brass::Withdraw(amt);  //调用基类的虚函数
	else if(amt <= bal + maxLoan - owesBank)
	{
		double advance = amt = bal;
		owesBank += advance*(1.0 + rate);
		cout << "Finance charge: $" << advance * rate << endl;
		Deposit(advance);   //调用基类的公有函数
		Brass::Withdraw(amt);
	}
	else
		cout << "Credit limit exceeded.Transaction cancelled.\n";
	restore(initialState,prec);
}


format setFormat()
{
	return cout.setf(std::ios_base::fixed,std::ios_base::floatfield);
}

void restore(format f,precis p)
{
	cout.setf(f,std::ios_base::floatfield);
	cout.precision(p);
}
#ifndef STOCK00_H_
#define STOCK00_H_
#include <string>
class Stock
{
private:
	std::string company;   //私有字段，隐藏灱数据
	long shares;
	double total_val;
	double share_val;
	void set_tot(){total_val = shares * share_val;}     //私有函数，只能在类内部调用
public:
	// void acquire(const std::string & co,long n, double pr);
	void buy(long num,double price);
	void sell(long num,double price);
	void update(double price);
	void show();
	Stock();    //默认构造函数
	Stock(const std::string & co,long n=0,double pr = 0.0);   //自定义构造函数
	~Stock();   //析构函数	
};
#endif
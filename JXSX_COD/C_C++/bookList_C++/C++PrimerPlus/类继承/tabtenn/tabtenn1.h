#ifndef TABTENN1_H_
#define TABTENN1_H_
#include <string>
using std::string;

//基类
class TabletennisPlayer
{
private:
	string firstname;
	string lastname;
	bool hasTable;
public:
	TabletennisPlayer(const string & fn = "none",const string & ln = "none",bool ht = false);
	void Name() const;
	bool HasTable() const {return hasTable;};
	void ResetTable(bool v) {hasTable = v;};
};

//派生类，public表示公有派生
// 基类的公有成员将成为派生类的公有成员，基类的私有部分也将成为派生类的一部分，但只能通过基类的公有和保护方法访问
class RatedPlayer : public TabletennisPlayer
{
private:
	unsigned int rating;
public:
	RatedPlayer(unsigned int r =0,const string & fn = "none",const string & ln = "none",bool ht = false);
	RatedPlayer(unsigned int r,const TabletennisPlayer & tp);
	unsigned int Rating() const {return rating;}
	void ResetRating(unsigned int r){rating =r;}
};

#endif
#include <iostream>
#include "tabtenn0.h"

//参数列表
TableTennisPlayer::TableTennisPlayer(const string & fn,const string & ln,bool ht):firstname(fn),lastname(ln),hasTable(ht){}
void TableTennisPlayer::Name() const
{
	std::cout << lastname << ", " << firstname;
}
//效果与参数列表语法相同
/*TableTennisPlayer::TableTennisPlayer(const string & fn,const string & ln,bool ht)
{
	firstname = fn;
	lastname = ln;
	hasTable = ht;
}*/
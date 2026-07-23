#include "tabtenn0.h"
#include <iostream>
int main()
{
	using std::cout;
	//实例化类
	TableTennisPlayer player1("Chuck","Blizzard",true);
	TableTennisPlayer player2("Tara","Boomdea",false);
	player1.Name();
	if(player1.HasTable())
		cout << ": has a table.\n";
	else
		cout << ": hasn't a table.\n";
	player2.Name();
	if(player2.HasTable())
		cout << ": has a table";
	else
		cout << ": hasn't a table.\n";

	system("pause");
	return 0;
}
#include <iostream>
#include "tabtenn1.h"
int main(void)
{
	using std::cout;
	using std::endl;
	TabletennisPlayer player1("Tara","Boomdea",false);
	RatedPlayer rplayer1(1140,"Mallory","Duck",true);
	rplayer1.Name();
	if(rplayer1.HasTable())
		cout << ": has a table.\n";
	else
		cout << ": hasn't a table.\n";
	cout << "Name: ";
	rplayer1.Name();
	cout << ":Rating: " << rplayer1.Rating() << endl;
	RatedPlayer rplyer2(1212,player1);
	cout << "Name:";
	rplyer2.Name();
	cout << ";Rating: " << rplyer2.Rating() << endl;
	system("pause");
	return 0;
}
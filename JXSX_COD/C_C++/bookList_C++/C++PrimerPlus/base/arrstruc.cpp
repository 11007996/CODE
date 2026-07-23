#include <iostream>
struct inflatable
{
	char name[20];
	float volume;
	double price;
};
// struct torgle_register
// {
// 	int a : 1;
// 	int b : 1;
// 	int c : 1; 
	
// };


/*union un
{
	char c;
	int a;
	long long b;
	double e;
};*/

enum en
{
	one=2,
	two,
	third,
	four,
	five,
	six,
	seven,
	eith,
	night,
	ten,
};

int main()
{
	using namespace std;
	/*inflatable guests[2]={{"Bambi",0.5,21.99},{"Godzilla",2000,565.99}};

	cout << "The guests " << guests[0].name << " and " << guests[1].name
	     << "\nhave a combined volume of "
	     << guests[0].volume + guests[1].volume << " cubic feet.\n";*/

	// cout << sizeof(torgle_register) << endl;
	// cout << sizeof(un) << endl;
	cout << five << endl;
	return 0;
}
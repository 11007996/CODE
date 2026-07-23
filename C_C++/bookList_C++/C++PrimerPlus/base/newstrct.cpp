#include <iostream>
struct inflatable
{
	char name[20];
	float volume;
	double price;
};

int main()
{
	using namespace std;
	inflatable * ps = new inflatable;  //动态结构体
	cout << "Enter name of inflatable item\n";
	cin.get(ps->name,20);
	cout << "Enter volume in cubic feet: ";
	cin >> (*ps).volume;
	cout << "Enter price:$";
	cin >> ps->price;
	cout << "Volume: " << ps->volume << " cubic feet\n";
	cout << "Price:$" << ps->price << endl;
	delete ps;
	return 0;
}
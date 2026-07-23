#include <iostream>

struct un
{
	int donuts =6;
	double cups = 4.5;
}unn;
int main()
{
	using namespace std;
	cout << "donuts value = " << unn.donuts;
	cout << " and donuts address = " << &unn.donuts << endl;
	cout << "cups value = " << unn.cups;
	cout << " and cups address = " << &unn.cups << endl;
	return 0;
}
#include <iostream>
#include <cmath>
#include <ctime>
// #include <math.h>   //C头文件
int main()
{
	using namespace std;

	// float area;
	// cout << "Enter the floor area, in square feet,of your home:\n";
	// cin >> area;
	// float side;
	// side = sqrt(area);  //求平方根
	// cout << "That's the qeuivalent of a square " << side
	//      <<" feet to the side." << endl;
	// cout << "How fascinating!"  << endl;


	// int a = 8;
	// a = pow(a,2);  //平方
	// cout << "8^2=" << a<<endl;

	srand(time(NULL));
	int i= rand() % 100;
	cout << i << endl;

	return 0;
}
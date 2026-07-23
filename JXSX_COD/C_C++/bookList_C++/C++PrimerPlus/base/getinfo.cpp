#include <iostream>
#include <cmath>
int main()
{
	using std::cout;
	using std::cin;
	using std::endl;
	int carrots;

	cout << "How many carrots do you have" << endl;
	cin >> carrots;   //输入流
	cout << "Here are two more. ";
	carrots += 2;
	cout << "Now you have " << carrots << " carrots" << endl;

	float c = 9.0;
	c = sqrt(c);    //平方根
	cout << c << endl;
	return 0;
}
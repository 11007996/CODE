#include <iostream>
int simon(int);
int main()
{
	int i=0;
	using std::cout;
	using std::endl;
	i= simon(3);
	cout << "Pick an integer: ";
	i=simon(i);
	cout << "Done!" << endl;
	return 0;
}
int simon(int n)
{
	using namespace std;
	cout << "Simon says touch you r toes " << n << " times." << endl;
	return ++n;
}
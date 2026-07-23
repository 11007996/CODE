#include <iostream>
#include <cstring>
using namespace std;
char *getname();
int main()
{
	char * name;
	name = getname();
	cout << name << " at " << &name << "\n";
	delete [] name;

	name = getname();
	cout << name << " at " <<(int *)name << "\n";
	delete [] name;
	return 0;
}

char * getname()
{
	char temp[80];
	cout << "Enter last name: ";
	cin >> temp;
	char * pn = new char[strlen(temp) +1];
	strcpy(pn,temp);
	return pn;
}
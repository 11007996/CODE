#include <iostream>
const int MAX = 5;
int main()
{
	using namespace std;
	int golf[MAX];
	cout << "Please enter your golf scores.\n";
	cout << "You must enter " << MAX << " rounds.\n";
	int i;
	for (i=0;i<MAX;i++)
	{
		cout << "round #" << i+1 << ": ";
		while(!(cin >> golf[i]))
		{
			cin.clear();   //把当前的缓存清空
			while(cin.get() != '\n')
				continue;
			cout << "Please enter a number: ";
		}
	}
	double total =0.0;
	for(i =0;i<MAX;i++)
		total += golf[i];
	cout << total / MAX << " = average score "
	     << MAX << " rounds\n";
	     return 0;
}
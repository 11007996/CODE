#include <iostream>
#include <string>
#include <cctype>
#include "stacktp.h"
using std::cout;
using std::cin;
int main()
{
	Stack<std::string> st;   //实例化时必须要指明泛型类型，这称为具体化
	char ch;
	std::string po;
	cout <<"Please enter A to add a purchase order.\n"
		 << "P to process a PO, or Q to quit.\n";
	while(cin >> ch && std::toupper(ch) != 'Q')
	{
		while(cin.get() != '\n')
			continue;
		if(!std::isalpha(ch))
		{
			cout << '\a';
			continue;
		}
		switch(ch)
		{
			case 'A':
			case 'a': cout << "Enter a PO number to add: ";
			cin >> po;
			if(st.isfull())
				cout << "stack already full\n";
			else
			{
				st.pop(po);
				cout << "PO #" << po << " popped\n";
				break;
			}
		}
		cout << "Please enter A to add a purchase order,\n"
			 << "p to process a PO, or Q to quit.\n";
	}
	cout << "Bye.\n";
	cin.get();
	return 0;
}
#include <iostream>
const int ArSize = 80;
/*int main()
{
	using namespace std;
	char line[ArSize];
	int spaces =0;
	cout << "Enter a line of text:\n";
	cin.get(line,ArSize);
	cout << "Complete line:\n" << line << endl;
	cout << "Line through first period:\n";
	for(int i =0;line[i] !='\0';i++)
	{
		cout << line[i];
		if(line[i] == '.')
			break;
		if(line[i] != ' ')
			continue;
		spaces++;
	}
	cout << "\n" << spaces << " spaces\n";
	cout << "Done.\n";
	return 0;
}*/

int main()
{
	using namespace std;
	int a=0;
	cin >> a;
	if(!isdigit(a))
	{
		cout << "非数字" << endl;
		exit(EXIT_FAILURE);
	}

	if(a > 10)
		goto aa;
	else
		cout << "小于10";
	return 0;
	aa:
	cout << "不小于10" << endl;
}
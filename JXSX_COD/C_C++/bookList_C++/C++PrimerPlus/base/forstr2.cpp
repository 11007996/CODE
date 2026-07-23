#include <iostream>
#include <cstring>
#include <stdlib.h>
int main()
{
	using namespace std;
	/*cout << "Enter a word: ";
	string word;
	cin >> word;

	char temp;
	int i,j;
	for(j=0,i=word.size()-1;j<i;--i,++j)  //前后对调字符
	{
		temp = word[i];
		word[i] = word[j];
		word[j] = temp;
	}
	cout << word << "\nDone\n";*/
	/*int a = 1;
	int b=2;
	int c = a|b;
	cout << c << endl;*/

	char *p = (char *)"abcd";
	char *pp = (char *)"abcd";
	cout << "p:" << (int *)p << endl;
	cout << "pp:" << (int *)pp << endl;
	char a[5] = "aaaa";
	char *i = new char[5];
	i = a;
	cout << i << endl;
	delete i;
	return 0;
}
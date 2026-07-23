#include <iostream>
int main()
{
	using namespace std;

	// static char a[20];
	// static char b[20];

	/*cin.get(a,20);   //可以读取整行，最多读取20个字符，其中包括\0终止符
	cin.get();    //序列中保留换行符，调用get()表示重启一行为输入做准备
	cin.get(b,20);*/

	// cin.getline(a,20).getline(b,20);   
	// cin.getline(a,20);

	/*cout << a << endl;
	cout << b << endl;*/

	// cout << b << endl;



	const int ArSize = 20;
	char name[ArSize];
	char dessert[ArSize];

	cout << "Enter your name: \n";
	cin.get(name,ArSize).get();    //read string,newline
	cout << "Enter your favorite dessert:\n";
	cin.get(dessert,ArSize).get();
	cout << "I have some dilicious " << dessert;
	cout << " for you, " << name << ".\n";
	return 0;
}
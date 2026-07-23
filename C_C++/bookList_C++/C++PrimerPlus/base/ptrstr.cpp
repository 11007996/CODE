#include <iostream>
#include <cstring>
int main()
{
	using namespace std;
	char animal[20] = "bear";
	const char * bird = "wren";
	char * ps;
	cout << animal << " and";
	cout << *bird << "\n";
	cout << "Enter a kind of animal: ";
	cin >> animal;
	ps = animal;   //不是复制字符串，而只是复制地址
	cout << ps << "!\n";
	cout << "Before using strcpy():\n";
	cout << animal << " an " << (int *)animal << endl;
	cout << ps << " at " << (int *)ps << endl;

	ps = new char[strlen(animal) +1];
	strcpy(ps,animal);    //复制字符串副本
	cout << "After using strcpy():\n";
	cout << animal << " at " << (int *)animal << endl;
	cout << ps << " at " << (int *)ps << endl;    //ps指向整个字符串，*p指个字符串的首个字母
    //一般来说,如果给cout 提供一个指针,它将打印地址,但如果指针的类型为char *,则cout将显示指向的字符串,
    // 如果显示的是字符串的地址,则必须将这种指针强制转换为另一种指针类型,如int *
	return 0;
}
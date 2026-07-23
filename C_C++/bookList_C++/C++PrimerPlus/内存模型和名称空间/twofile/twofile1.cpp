#include <iostream>
int tom =3;
int dick = 30;
static int harry = 300;  //静态变量，内部链接，只能在本文件中共享数据
void remote_access();
int main()
{
	using namespace std;
	cout << "main() reports the following addresses:\n";
	cout << &tom << " = &tom, " << &dick << " = &dick, ";
	cout << &harry << " = &harry\n";
	remote_access();
	return 0;
}
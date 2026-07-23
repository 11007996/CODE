#include <iostream>
#include <climits>
int main()
{
	using namespace std;
	/*int n_int = INT_MAX;
	short n_short = SHRT_MAX;
	long n_long = LONG_MAX;
	long long n_llong = LLONG_MAX;

	cout <<"int is " << sizeof(int) << " bytes." << endl;
	cout << "short is " << sizeof n_short << " bytes." << endl;
	cout << "long is " << sizeof n_long << " bytes." << endl;
	cout << "long long is " << sizeof n_llong << " bytes." << endl;
	cout << endl;

	cout << "Maximum values:" << endl;
	cout << "int: " << n_int << endl;
	cout << "short: "<< n_short << endl;
	cout << "long: " << n_long << endl;
	cout << "long long:" << n_llong << endl << endl;

	cout << "Minimum int value = " << INT_MAX << endl;
	cout << "Bits per byte = " << CHAR_BIT << endl;*/

	int a[4]{6,7,8,9};  //数组初始化，等价于int a[4]={6,7,8,9}
	cout << a[2] << endl;
	return 0;
}


/*climits头文件常数
CHAR_BIT    char的位数
CHAR_MAX    char的最大值
CHAR_MIN    char的最小值
SCHAR_MAX   signed char的最大值
SCHAR_MIN   signed char的最小值
UCHAR_MAX   unsigned char的最大值
SHRT_MAX    short的最大值
SHRT_MIN    short的最小值
USHRT_MAX   unsigned short的最大值
INT_MAX     int的最大值
INT_MIN     int的最小值
UNIT_MAX    unsigned int的最大值
LONG_MAX    long的最大值
LONG_MIN    long的最小值
ULONG_MAX   unsigned long的最大值
LLONG_MIN   long long的最小值
LLONG_MAX   long long的最大值
ULLONG_MAX  unsigned long long的最大值*/

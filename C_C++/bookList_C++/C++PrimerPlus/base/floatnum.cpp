#include <iostream>
int main()
{
	using namespace std;
	/*cout.setf(ios_base::fixed,ios_base::floatfield);  //强制以小数形式表示浮点数
	float tub = 10.0/3.0;   //float至少6位有效精度
	double mint = 10.0/3.0;  //doube至少13位有效精度
	const float million = 1.0e6;
	cout << "tub = " << tub;
	cout << ", a million tubs = " << million *tub;
	cout << ",\nand ten million tubs = ";
	cout << 10 * million * tub << endl;

	cout << "mint = " << mint << " and a million mints = ";
	cout << million * mint << endl;*/


	/*float a = 2.34e+8f;
	float b = a + 1.0f;   //float只有6位无效精度，加1对数据存储没有影响
	cout << "a= " << a << endl;
	cout << "b - a = " << b - a << endl;    //因精度问题，b-a=0
*/

	cout << 3/2.0f << endl;
	return 0;
}
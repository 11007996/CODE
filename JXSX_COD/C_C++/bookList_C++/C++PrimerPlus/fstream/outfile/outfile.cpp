#include <iostream>
#include <fstream>
int main()
{
	using namespace std;
	char automobile[50];
	int year;
	double a_price;
	double d_price;

	ofstream outFile;
	outFile.open("carinfo.txt");  //以覆盖形式打开文本文件，不存在则创建文本文件

	cout << "Enter the make and model of automobile: ";
	cin.getline(automobile,50);
	cout << "Enter the model year: ";
	cin >> year;
	cout << "Enter the original asking price： ";
	cin >> a_price;
	d_price = 0.913 * a_price;

	cout << fixed;
	cout.precision(2);  //设置小数点精度为2
	cout.setf(ios_base::showpoint);   //以小数点形式显示
	cout << "Year: " << year << endl;
	cout << "Was asking $" << a_price << endl;
	cout << "Now asking $" << d_price << endl;

	outFile << fixed;
	outFile.precision(2);
	outFile.setf(ios_base::showpoint);
	outFile << "Make and model: " << automobile << endl;   //输出到文本文件
	outFile << "Year: " << year << endl;
	outFile << "Was asking $" << a_price << endl;
	outFile << "Now asking $" << d_price << endl;

	outFile.close();   //关闭文本文件
	return 0;
}
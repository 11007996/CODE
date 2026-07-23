#include <iostream>
#include <fstream>
#include <cstdlib>
const int SIZE = 120;
int main()
{
	using namespace std;
	char filename[SIZE];

	ifstream inFile;
	cout << "Enter name of data file: ";
	cin.getline(filename,SIZE);   //输入文件路径
	inFile.open(filename);
	if(!inFile.is_open())
	{
		cout << "Coule not open the file " << filename << endl;
		cout << "Program terminating.\n";
		exit(EXIT_FAILURE);
	}
	if(!inFile.bad())  //如果文件损坏或硬件故障
	{
		cout << "file is bad";
	}
	double value;
	double sum =0.0;
	int count =0;
	inFile >> value;   //将读取的文件内容存入value变量中
	while(inFile.good())   //方法没有任何错误时返回true，应在文件读取后立即检查
	{
		++count;
		sum += value;
		inFile >> value;
	}
	if(inFile.eof())   //判断最后一次读取数据时候是否遇到EOF，若是返回true
		cout << "End of file reached.\n";
	else if(inFile.fail())   //判断最后一次读取数据的时候是否遇到了类型不配的情况，若是返回true,若遇到了EOF也返回true
		cout << "Input terminated by data mismatch.\n";
	else
		cout << "Input terminated for unknown reason.\n";

	if(count ==0)
		cout << "No data processed.\n";
	else
	{
		cout << "Items read: " << count << endl;
		cout << "Sum: " << sum << endl;
		cout << "Average: " << sum / count << endl;
	}
	inFile.close();
	return 0;
}
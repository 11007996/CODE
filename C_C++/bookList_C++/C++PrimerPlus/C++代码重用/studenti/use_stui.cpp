#include <iostream>
#include "studenti.h"
using std::cin;
using std::cout;
using std::endl;

void set(Student & sa,int n);
const int pupils = 3;
const int quizzes = 5;
int main()
{
	// 初始化类对象，调用构造函数
	Student ada[pupils] = {Student(quizzes),Student(quizzes),Student(quizzes)};
	int i;
	for(i = 0;i<pupils;i++)
		set(ada[i],quizzes);   //调用非const版本重载[]
	cout << "\nStudent List:\n";
	for(i=0;i<pupils;i++)
		cout << ada[i].Name() << endl;    //强制转换为string类型
	cout << "\nResults:";
	for(i=0;i<pupils;i++)
	{
		cout << endl << ada[i];    //调用const版本重载[]
		cout << "average: " << ada[i].Average() << endl;
	}
	cout << "Done.\n";
	cin.get();
	return 0;
}

void set(Student & sa,int n)
{
	cout << "Please enter the student's name: ";
	getline(cin,sa);
	cout << "Please enter " << n << " quiz scores:\n";
	for(int i=0;i<n;i++)
		cin >> sa[i];    //重载[]运算符
	while(cin.get() != '\n')
		continue;
}
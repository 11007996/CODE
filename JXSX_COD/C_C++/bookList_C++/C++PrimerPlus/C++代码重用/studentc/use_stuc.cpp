#include <iostream>
#include "studentc.h"
using std::cin;
using std::cout;
using std::endl;
void set(Student & sa,int n);    //全局函数原型
const int pupils = 3;
const int quizzes = 2;

int main()
{
	Student ada[pupils] = {Student(quizzes),Student(quizzes),Student(quizzes)};   //初始化类数组
	int i;
	for(i = 0;i<pupils;++i)
		set(ada[i],quizzes);
	cout << "\nStudent List:\n";
	for(i =0;i<pupils;++i)
		cout << ada[i].Name() << endl;
	cout << "\nResults:";
	for(i = 0;i<pupils;++i)
	{
		cout << endl << ada[i];
		cout << "average: " << ada[i].Average() << endl;
	}
	cout << "Done.\n";

	cin.get();
	return 0;
}
//全局函数
void set(Student & sa,int n)
{
	cout << "Please enter the student's name: ";
	getline(cin,sa);
	cout << "Please enter " << n << " quiz scores:\n";
	for(int i=0;i<n;i++)
		cin >> sa[i];
	while(cin.get() != '\n')
		continue;
}
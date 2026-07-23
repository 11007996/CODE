#include "workermi.h"
#include <iostream>
using std::cout;
using std::cin;
using std::endl;
Worker::~Worker(){}

void Worker::Data()const
{
	cout << "Name: " << fullname << endl;
	cout << "Employee ID: " << id << endl;
}

void Worker::Get()
{
	getline(cin,fullname);
	cout << "Enter worker's ID: ";
	cin >> id;
	while(cin.get() != '\n')
		continue;
}

void Waiter::Set()
{
	cout << "Enter waiter's name: ";
	Worker::Get();
	Get();
}

/*void Waiter::Set()
{
	cout << "Enter waiter's name: ";
	Worker::GerIO;
	Get();
}*/

void Waiter::Show() const
{
	cout << "Category: waiter\n";
	Worker::Data();
	Data();
}

void Waiter::Get()
{
	cout << "Enter waiter's panache rating: ";
	cin >> panache;
	while(cin.get() != '\n')
		continue;
}

char * Singer::pv[Singer::Vtypes] = {"other","alto","contralto","soprano","bass","baritone","tenor"};

void Singer::Set()
{
	cout << "Enter singer's name: ";
	Worker::Get();
	Get();
}


void Singer::Show() const
{
	cout << "Category: singer\n";
	Worker::Data();
	Data();
}

void Singer::Data()const
{
	cout << "Vocal range: " << pv[voice] << endl;
}

void Singer::Get()
{
	cout << "Enter number for singer's vocal range:\n";
	int i;
	for(i =0;i<Vtypes;i++)
	{
		cout << i << ": " << " ";
		if(i % 4 == 3)
			cout << endl;
	}

	if(i % 4 != 0)
		cout << '\n';
	cin >> voice;
	while(cin.get() != '\n')
		continue;
}

//多重继承，为避免继承函数出现二义性，必须重写函数
void SingingWaiter::Data() const
{
	Singer::Data();    //在重写函数中调用基类的函数
	Waiter::Data();
}
void SingingWaiter::Get()
{
	Waiter::Get();
	Singer::Get();
}

void SingingWaiter::Set()
{
	cout << "Enter singing waiter's name: ";
	Worker::Get();
	Get();
}

void SingingWaiter::Show() const
{
	cout << "Category: singing waiter\n";
	Worker::Data();
	Data();
}
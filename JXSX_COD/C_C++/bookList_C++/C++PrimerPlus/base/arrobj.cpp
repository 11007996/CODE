#include<iostream>
#include<array>
#include<String>

const int Seasons =4;
const std::array<std::string,Seasons> Snames ={"Spring","Summer","Fall","Winter"};

void fill(std::array<double,Seasons> *pa);
void show(std::array<double,Seasons> da);

int main()
{
	std::array<double,Seasons> expenses;   //array容器在std空间中,double表示数据类型,seasons表示容器大小,array也是数组,但大小固定,比数组安全
	fill(&expenses);
	show(expenses);
	return 0;
}

void fill(std::array<double,Seasons> *pa)
{
	using namespace std;
	for(int i=0;i<Seasons;i++)
	{
		cout << "Enter " << Snames[i] << " expenses: ";
		// cin >> pa[i];
		cin >> (*pa)[i];
	}
}

void show(std::array<double,Seasons> da)
{
	using namespace std;
	double total =0.0;
	cout << "\nEXXENSES\n";
	for(int i=0;i<Seasons;i++)
	{
		cout << Snames[i] << ": $" << da[i] << endl;
		total += da[i];
	}
	cout << "Total Expenses: $" << total << endl;
}

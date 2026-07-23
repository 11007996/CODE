/*
// 模板类的非模板码元函数
#include <iostream>
using std::cout;
using std::endl;
template <typename T>
class HasFriend
{
private:
	T item;
	static int ct;
public:
	HasFriend(const T & i) : item(i) {ct++;}
	~HasFriend() {ct--;}
	friend void counts();
	// 友元函数，以模板作为参数，函数是模板具体化后类对象的友元函数
	friend void reports(HasFriend<T> &);
};

template <typename T>
int HasFriend<T>::ct = 0;

void counts()
{
	cout << "int count: " << HasFriend<int>::ct << ": ";
	cout << "double count: " << HasFriend<double>::ct << endl;
}
// 友元函数，以模板作为参数，函数是模板具体化后类对象的友元函数
// 模板类的非模板友元函数
void reports(HasFriend<int> & hf)
{
	cout << "HasFriend<int>:" << hf.item << endl;
}

void reports(HasFriend<double> & hf)
{
	cout << "HasFriend<double>:" << hf.item << endl;
}

int main()
{
	cout << "No objects declared: ";
	counts();
	HasFriend<int> hfi1(10);
	cout << "After hfi1 declared: ";
	counts();
	HasFriend<int> hfi2(20);
	cout << "After hfi2 declared: ";
	counts();
	HasFriend<double> hfdb(10.5);
	cout << "After hfdb declared: ";
	counts();
	reports(hfi1);   //码元函数，不是模板类的成员
	reports(hfi2);
	reports(hfdb);
	system("pause");
	return 0;
}*/



// 模板类的约束模板友元函数
#include <iostream>
using std::cout;
using std::endl;

//模板函数原型
template <typename T> void counts();
template <typename T> void report(T &);

template <typename TT>
class HasFriendT
{
private:
	TT item;
	static int ct;
public:
	HasFriendT(const TT & i) : item(i) {ct++;}
	~HasFriendT(){ct--;}
	friend void counts<TT>();
	//友元函数本身就是模板函数
	friend void report<>(HasFriendT<TT> &);
};

template <typename T>
int HasFriendT<T>::ct = 0;

template <typename T>
void counts()
{
	cout << "template size: " << sizeof(HasFriendT<T>) << "; ";
	cout << "template counts(): " << HasFriendT<T>::ct << endl;
}

template <typename T>
void report(T & hf)
{
	cout << hf.item << endl;
}

int main()
{
	counts<int>();
	HasFriendT<int> hfi1(10);
	HasFriendT<int> hfi2(20);
	HasFriendT<double> hfdb(10.5);
	report(hfi1);
	report(hfi2);
	report(hfdb);
	cout << "counts<double>() output:\n";
	counts<int>();
	cout << "counts<double>() output:\n";
	counts<double>();

	system("pause");
	return 0;
}
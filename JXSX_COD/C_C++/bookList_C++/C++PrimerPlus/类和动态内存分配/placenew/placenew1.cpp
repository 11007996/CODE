/*#include <iostream>
#include <string>
#include <new>
using namespace std;
const int BUF = 512;
class JustTesting
{
private:
	string words;
	int number;
public:
	JustTesting(const string & s = "Just Testing",int n =0)   //默认构造函数
	{
		words = s;
		number =n;
		cout << words << " constructed\n";
	}
	~JustTesting() {cout << words << " destroyed\n";}
	void show() const
	{
		cout << words << ", " << number << endl;
	}
};

int main()
{
	char * buffer = new char[BUF];
	JustTesting *pc1,*pc2;

	pc1 = new (buffer) JustTesting;  //new定位符
	pc2 = new JustTesting("heap1",20);   //构造函数返回指针

	cout << "Memory contents:\n";
	cout << pc1 << ": ";
	pc1->show();    //指针调用类成员函数
	cout << pc2 << ": ";
	pc2->show();

	JustTesting *pc3,*pc4;
	pc3 = new (buffer) JustTesting("Bad Idea",6);  //同样定位到buffer指向的内存，会覆盖掉pc1对象
	pc4 = new JustTesting("Heap2",10);

	cout << "Memory constents:\n";
	cout << pc3 << ": ";
	pc3->show();
	cout << "pc1:";
	pc1->show();
	cout << pc4 << ": ";
	pc4->show();

	//注意正确的删除顺序，对于使用定位new运算符妴的对象，应以与创建顺序相反的顺序进行删除，原因在于，晚创建的对象可能依赖于早创建的对象
	delete pc2;   //删除指针，将引发指针指向对象的析构函数
	delete pc4;
	delete [] buffer;   //只删除了buffer指针，但没有释放指针所指向的内存，pc1,pc3对象没有被释放
	cout << "Done\n";
	return 0;
}*/



#include <iostream>
#include <string>
#include <new>
using namespace std;
const int BUF = 512;
class JustTesting
{
private:
	string words;
	int number;
public:
	JustTesting(const string & s = "Just Testing",int n=0)
	{
		words = s;
		number = n;
		cout << words << " constructed\n";
	}
	~JustTesting()
	{
		cout << words << ", " << " destroyed\n";
	}
	void show()
	{
		cout << words << ", " << number << endl;
	}
	
};

int main()
{
	char * buffer = new char[BUF];
	JustTesting *pc1,*pc2;
	pc1 = new (buffer) JustTesting;
	pc2 = new JustTesting("Heap1",20);

	cout << "Memory block addresses:\n" << "buffer: "
		 << (void *) buffer << " heap: " << pc2 << endl;
	cout << "Memory contents:\n";
	cout << pc1 << ": ";
	pc1->show();
	cout << pc2 << ": ";
	pc2->show();

	JustTesting *pc3,*pc4;
	pc3 = new (buffer + sizeof(JustTesting)) JustTesting("Better Idea",6);   //new定位运算符，指针向后移位，避免覆盖pc1对象
	pc4 = new JustTesting("Heap2",10);

	cout << "Memory contents:\n";
	cout << pc3 << ": ";
	pc3->show();
	cout << pc4 << ": ";
	pc4->show();

	delete pc2;
	delete pc4;

	pc3->~JustTesting();   //显式调用析构函数，delete与new定位运算符不匹配
	pc1->~JustTesting();
	delete [] buffer;
	cout << "Done\n";
	return 0;
}
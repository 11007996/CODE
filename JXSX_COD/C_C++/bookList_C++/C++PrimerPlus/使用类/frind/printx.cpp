#include <iostream>
using namespace std;
class Shape
{
private:
	int width;
	int height;
public:
	void setWidth(int w){width =w;}
	void setheight(int h){height =h;}
	friend void printwidth(Shape & s1);
	friend void printheight(Shape & s1);
};

//函数不属于Shape类，但可以在类外访问Shape私有变量
void printwidth(Shape & s1)
{
	cout << "s1 weith:" << s1.width << endl;
}
void printheight(Shape & s2)
{
	cout << "s2 heignt:" << s2.height << endl;
}



int main()
{
	Shape s1;
	s1.setWidth(10);
	s1.setheight(20);
	printheight(s1);
	printwidth(s1);
	system("pause");
	return 0;
}
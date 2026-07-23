#include <iostream>
#include <vector>   //模板类vector
#include <array>    //模板类array
int main()
{
	using namespace std;
	double a1[4] = {1.2,2.4,3.6,4.8};   //数组，静态，固定长度，存在栈中

	vector<double> a2(4);  //vector, 动态数组，动态长度，存在堆中,所占大小初始为12字节，自动增加大小
	a2[0] = 1.0/3.0;
	a2[1] = 1.0/5.0;
	a2[2] = 1.0/7.0;
	a2[3] = 1.0/9.0;

	array<double,4> a3 = {3.14,2.72,1.62,1.41};   //array，固定长度，存在栈中,所占大小为sizeof(typenae)*n
	array<double,4> a4;
	a4 = a3;  //可以整个array复制

	cout << "a1[2]: " << a1[2] << " at " << &a1[2] << endl;
	cout << "a2[2]: " << a2[2] << " at " << &a2[2] << endl;
	cout << "a3[2]: " << a3[2] << " at " << &a3[2] << endl;
	cout << "a4[2]: " << a4[2] << " at " << &a4[2] << endl;

	a1[-2] = 20.2;   //等价于*(a1-2) ,说明数组操作不安全
	// a1.at(2) = 20.0;    //等价于a1[2] = 20.2，但不允许非法索引
	cout << "a1[-2]: " << a1[-2] << " at " << &a1[-2] << endl;
	cout << "a3[2]: " << a3[2] << " at " << &a3[2] << endl;
	cout << "a4[2]: " << a4[2] << " at " << &a4[2] << endl;
	cout << "a4.begin():" << a4.begin() << "\na4.end():" << a4.end() << endl;
	return 0;
}



// 使用new申请的指针必须用delete释放内存，程序结束只是释放指针所在的内存，不释放指针所指向的数据的内存
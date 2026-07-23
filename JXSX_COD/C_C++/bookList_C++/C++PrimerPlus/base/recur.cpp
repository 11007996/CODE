#include<iostream>
void countdown(int n);
int main()
{
	countdown(4);
	return 0;
}
void countdown(int n)
{
	using namespace std;
	cout << "Counting down ... " << n << endl;  //每次递归都创建一个n变量,即同时存在多个n变量,它们的内存地址不同,因此不适合递归层次较多的情况
	if(n>0)
		countdown(n-1);
	cout << n << ":Kaboom!\n";   //递归后以相反的顺序执行
}
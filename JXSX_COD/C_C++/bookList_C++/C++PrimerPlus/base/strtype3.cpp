#include <iostream>
#include <cstring>
int main()
{
	using namespace std;
	/*char charr1[20];
	char charr2[20] ="jaguar";
	string str1;
	string str2 = "panther";

	str1 = str2;
	strcpy(charr1,charr2);   //复制
	str1 +=" paste";
	strcat(charr1," juice");   //合并
	int len1 = str1.size();  //长度
	int len2 = strlen(charr1);   //长度
	cout << "The string " << str1 << " contains "
	     << len1 << " characters.\n";
	cout << "The string " << charr1 << " contains "
	     << len2 << " characters.\n";*/

	cout << R"+*(abcdef)+*" << endl;   //R"+*(...)+*"表示输出原始字符,不需要转义序列
	return 0;
}


// 当数据从值类型转换为引用类型的过程被称为“装箱”，而从引用类型转换为值类型的过程则被成为“拆箱”
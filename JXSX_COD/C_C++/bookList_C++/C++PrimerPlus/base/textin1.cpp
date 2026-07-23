#include <iostream>
/*int main()
{
	using namespace std;
	char ch;
	int count = 0;
	cout << "Enter characters;enter # to quit:\n";
	cin >> ch;
	while(ch != '#')
	{
		cout << ch;
		++count;
		cin >> ch;  //逐个字符读取,忽略空格、换行符等
	}
	cout << endl << count << " characters read\n";
	return 0;
}*/

/*int main()
{
	using namespace std;
	char ch;
	int count =0;
	cout << "Enter characters;enter # to quit:\n";
	cin.get(ch);   //将键盘输入的字符逐个存入ch中，输入存在缓存
	while(ch != '#')
	{
		cout << ch;
		++count;
		cin.get(ch);  //空格、换行符待也会被显示
	}
	cout << endl << count << " characters read\n";
	return 0;
}*/

/*int main()
{
	using namespace std;
	char ch;
	int count =0;
	cin.get(ch);
	// while(cin.fail() == false)   //false表示没检测到EOF,更通用
	// while(cin.eof() == false)  //false表示没检测到EOF
	while(cin)  //检测输入是否成功
	{
		cout << ch;
		++count;
		cin.get(ch);
	}
	cout << endl << count << " characters read\n";
	return 0;
}*/


int main()
{
	using namespace std;
	int ch;
	int count =0;
	while((ch = cin.get()) != EOF)  //检测文件结尾EOF
	{
		cout.put(ch);
		++count;
	}
	cout << endl << count << " characters read\n";
	return 0;
}
#include <iostream>
#include <cctype>
int main()
{
	using namespace std;
	cout << "Enter text for analysis,and type @ to terminate input.\n";
	char ch;
	int whitespace =0;
	int digits =0;
	int chars =0;
	int punct =0;
	int others =0;
	cin.get(ch);
	while(ch != '@')
	{
		if(isalpha(ch))   //是否是字母
			chars++;
		else if(isspace(ch))   //是否是空格
			whitespace++;
		else if(isdigit(ch))  //是否是数字
			digits++;
		else if(ispunct(ch))   //是否是标点符号
			punct++;
		else
			others++;
		cin.get(ch);
	}
	cout << chars << " letters, "
	     << whitespace << " whitespace,"
	     << digits << " digits,"
	     << punct << " punct,"
	     << others << " others.\n";
	return 0;
}


/*isalnum()    是否是字母或数字
isalpha()    是否是字母
iscntrl()    是否是控制字符
isdigit()    是否是数字
isgraph()    是否是除空格之外的打印字符
islower()    是否是小写字母
isprint()    是否是打印字符，包括空格
ispunct()    是否是标点符号
isspace()    是否是标准空白字符，如空格、进纸、换行符、回车。水平制表符或垂直制表符
isupper()    是否是大写字母
isxdigit()   是否是十六进制数字
tolower()    转换为小写
toupper()    转换为大写*/
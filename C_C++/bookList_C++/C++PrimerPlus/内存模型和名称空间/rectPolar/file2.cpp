#include <iostream>
#include <cmath>
//自定义头文件用引号，内置关文件用尖括号
#include "coordin.h"  //自建头文件，在函数定义文件和调用函数文件中都要引用

polar rect_to_polar(rect xypos)
{
	using namespace std;
	polar answer;

	answer.distance = sqrt(xypos.x * xypos.x+xypos.y * xypos.y);
	answer.angle = atan2(xypos.y,xypos.x);
	return answer;
}
void show_polar(polar dapos)
{
	using namespace std;
	const double Rad_to_deg = 57.29577951;

	cout << "distance = " << dapos.distance;
	cout << ", angle = " << dapos.angle * Rad_to_deg;
	cout << "degrees\n";
}


// C++编译命令: g++ file1.cpp file2.cpp -o file.exe
// C编译命令: gcc file1.cpp file2.cpp -o file.exe
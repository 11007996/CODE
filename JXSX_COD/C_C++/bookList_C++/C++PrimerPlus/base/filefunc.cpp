#include<iostream>
#include<fstream>
#include<cstdlib>
using namespace std;

void file_it(ostream & os,double fo,const double fe[],int n);
const int LIMIT =5;
int main()
{
	ofstream fout;
	const char * fn = "ep-data.txt";
	fout.open(fn);
	if(!fout.is_open())
	{
		cout << "Can't open " << fn << ". Bye.\n";
		exit(EXIT_FAILURE);
	}
	double objective;
	cout << "Enter the focal length of your thelescope objective in mm: ";
    cin >> objective;
    double eps[LIMIT];
    cout << "Enter the focal lengths, in mm, of " << LIMIT
	     << " eyepieces:\n";
    for(int i=0;i< LIMIT;i++)
    {
    	cout << "Eyepieece #" << i + 1 << ": ";
    	cin >> eps[i];
    }
    file_it(fout,objective,eps,LIMIT);   //引用ofstream类,ofstream派生于ostream类
    file_it(cout,objective,eps,LIMIT);   //引用ostream类
    cout << "Done\n";
    return 0;
}

void file_it(ostream & os,double of,const double fe[] ,int n)    //基类引用可接受派生类参数
{
	ios_base::fmtflags initial;  //存储这种信息所需的数据类型名称
	initial = os.setf(ios_base::fixed);  //将对象置于使用定点表示法的模式
	os.precision(0);
	os << "Focal length of objective: " << of << " mm:\n";
	os.setf(ios::showpoint);  //将对象置于显示小数点的模式
	os.precision(1);
	os.width(12);   //设置下一次输出操作使用的字段宽度,这种设置只在显示下一个值时有效,然后将恢复到默认设置,默认的字段宽度为零
	os << "f.l. eyepieces";
	os.width(15);
	os << "magnification" << endl;
	for(int i=0;i<n;i++)
	{
		os.width(12);
		os << fe[i];
		os.width(15);
		os << int(of/fe[i] + 0.5) << endl;
	}
	os.setf(initial);    //返回调用它之前有效的所有格式化设置,即恢复之前存储的格式信息
}
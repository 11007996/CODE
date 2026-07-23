#include <iostream>
using std::cout;
using std::endl;
// 成员模板，模板可用作结构、类或模板类的成员
template <typename T>
class beta
{
private:
	template <typename V>
	class hold    //嵌套类
	{
	private:
		V val;
	public:
		hold(V v = 0) : val(v){}
		void show() const {cout << val << endl;}
		V Value() const{return val;}
	};
	hold<T> q;   //普通模板
	hold<int> n;   //显示具体化
public:
	beta(T t,int i):q(t),n(i){}   //部分具体化
	//模板函数
	template<typename U>
	U blab(U u,T t){return (n.Value() + q.Value()) * u / t;}
	void Show() const{q.show();n.show();}
};

int main()
{
	beta<double> guy(3.5,3);
	cout << "T was set to double\n";
	guy.Show();
	cout << guy.blab(10,2.3) << endl;   //函数模板
	cout << "U was set to int\n";
	cout << guy.blab(10.0,2.3) << endl;   //函数模板
	cout << "U was set to double\n";
	cout << "Done\n";
	system("pause");
	return 0;
}
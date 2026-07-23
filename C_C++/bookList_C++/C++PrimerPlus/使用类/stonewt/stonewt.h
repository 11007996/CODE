#ifndef STONEWT_H_
#define STONEWT_H_
class Stonewt
{
private:
	enum {Lbs_per_stn = 14};
	int stone;
	double pds_left;
	double pounds;
public:
	//explicit Stonewt(double lbs);   //explicit表示禁止隐式转换类，即不允许Stonewt st=23.33，只能进行显示转换
	Stonewt(double lbs);   //允许 Stonewt st(23.11) 语句隐式创建类
	Stonewt(int stn,double lbs);
	Stonewt();
	~Stonewt();
	void show_lbs() const;
	void show_stn() const;

	int get_int(Stonewt st) const;   //普通函数实现转换函数功能

	operator int() const;    //转换函数，将自定义类转换为内置类型
	operator double() const;   //转换函数，不能有返回值，也不能有参数
};
#endif
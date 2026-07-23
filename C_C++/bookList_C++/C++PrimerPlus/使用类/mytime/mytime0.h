#ifndef MYTIME0_H_
#define MYTIME0_H_

class Time
{
private:
	int hours;
	int minutes;
public:
	Time();
	Time(int h,int m =0);
	void AddMin(int m);
	void AddHr(int h);
	void Reset(int h=0,int m=0);
	// Time Sum(const Time & t) const;
	Time operator+(const Time & t) const;   //末尾const表示调用该函数的对象不能被修改，尤其指this指针
	void Show() const;
	// ~Time();
	
};
#endif
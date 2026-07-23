#ifndef TV_H_
#ifndef TV_H_
#define TV_H_

class Tv;    //前向声明

class Remote
{
public:
	enum State{Off,On};
	enum{MinVal,MaxVal = 20};
	enum{Antenna,Cable};
	enum{TV,DVD};
private:
	int mode;
public:
	Remote(int m = Tv::TV):mode(m){};
	bool volup(Tv & t);
	bool voldown(Tv & t);
	void onoff(Tv & t);
	void chanup(Tv & t);
	void chandown(Tv & t);
	void set_chan(Tv & t,int c);
	void set_mode(Tv & t);
	void set_input(Tv & t);   //函数体需要知道Tv类的成员信息，所有函数用内联函数后置
};



class Tv
{
public:
	friend void Remote::set_chan(Tv & t,int c);   //友元函数，要求Remote类必须定义在Tv类之前，所有需要前声明，避免了友元类的整个类都是友元
	enum State{Off,On};
	enum {MinVal,MaxVal = 20};
	enum {Antenna,Cable};
	enum {TV,DVD};

	Tv(int s =Off,int mc = 125):state(s),volume(5),maxchannel(mc),channel(2),mode(Cable),input(TV){}
	void onoff() {state = (state == On) ? Off : On;}
	bool ison() const{return state == On;}
	bool volup();
	bool voldown();
	void chanup();
	void chandown();
	void set_mode(){mode = (mode==Antenna) ? Cable : Antenna;}
	void set_input(){input = (input == TV) ? DVD : TV;}
	void settings() const;
private:
	int state;
	int volume;
	int maxchannel;
	int channel;
	int mode;
	int input;
};

inline bool Remote::volup(Tv & t){return t.volup();}
inline bool Remote::voldown(Tv & t){return t.voldown();}
inline bool Remote::onoff(Tv & t){t.onoff();}
inline bool Remote::chanup(Tv & t){t.chanup();}
inline bool Remote::chandown(Tv & t){t.chandown();}
inline bool Remote::set_mode(Tv & t){t.set_mode();}
inline bool Remote::set_input(Tv & t){t.set_input();}
inline bool Remote::set_chan(Tv & t){t.channel=c;}
#endif
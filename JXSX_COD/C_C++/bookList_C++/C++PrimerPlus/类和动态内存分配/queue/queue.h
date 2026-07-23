#ifndef QUEUE_H_
#define QUEUE_H_
class Customer
{
private:
	long arrive;
	int processtime;
public:
	Customer() { arrive = processtime = 0;}   //默认构造函数
	void set(long when);
	long when() const { return arrive;}
	int ptime() const { return processtime;}
};

typedef Customer Item;

class Queue
{
private:
	struct Node {Item item;struct Node * next;};  //队列结构
	enum {Q_SIZE =10};   //队列长度
	Node * front;  //指向第一个节点
	Node * rear;   //指向最后一个节点
	int items;
	const int qsize;
	Queue(const Queue & q) : qsize(0){}   //私有构造函数，只能在类内使用；初始化参数列表语句，const变量只能在初始化时赋值，新建对象在进入构造函数体就已经存在
	Queue & operator=(const Queue & q) {return *this;}   //运算符重载，定义在私有部分相当于内联函数
public:
	Queue(int qs = Q_SIZE);
	~Queue();
	bool isempty() const;  //队列是否为空
	bool isfull() const;   //队列是否已满
	int queuecount() const;   //队列长度
	bool enqueue(const Item &item);    //添加节点
	bool dequeue(Item &item);    //减少节点
};
#endif
#include <cstdlib>
#include "queue.h"
Queue::Queue(int qs) : qsize(qs)
{
	front = rear = NULL;   //初始化为空指针
	items =0;
}

Queue::~Queue()
{
	Node * temp;
	while(front != NULL)
	{
		temp = front;   //运算符重载，返回指针指赂的对象
		front = front->next;
		delete temp;   //循环删除队列中的对象
	}
}

bool Queue::isempty() const
{
	return items == 0;
}
bool Queue::isfull() const
{
	return items == qsize;
}
int Queue::queuecount() const
{
	return items;
}

//向队列中添加节点
bool Queue::enqueue(const Item & item)
{
	if(isfull())   //如果节点已满，则返回false
		return false;
	Node * add = new Node;
	add->item = item;
	add->next = NULL;  //队尾节点指针为空
	items++;
	if(front == NULL)  //队列的第一个节点
		front = add;
	else
		rear->next = add;   //节点加在队尾
	rear = add;   //指示最后一个节点
	return true;
}

//在队首弹出一个节点
bool Queue::dequeue(Item & item)
{
	if(front == NULL)   //队列为空返回false
		return false;
	item = front->item;  //通过引用弹出队首节点
	items--;
	Node * temp = front;
	front = front->next;   //将队首设为下一节点
	delete temp;   //删除队首节点
	if(items ==0)
		rear = NULL;  //如果队列为空，设置队尾节点为空
	return true;
}
void Customer::set(long when)
{
	processtime = std::rand() % 3 +1;
	arrive = when;
}
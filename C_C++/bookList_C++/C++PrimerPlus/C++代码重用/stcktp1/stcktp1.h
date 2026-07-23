#ifndef STCKTP1_H_
#define STCKTP1_H_

template <typename Type>
class Stack
{
private:
	enum {SIZE = 10};
	int stacksize;
	Type * items;    //泛型类设置为指针，把常规类型和指针类型兼容
	int top;
public:
	explicit Stack(int ss = SIZE);
	Stack(const Stack & st);
	~Stack(){delete [] items;}    //删除数组的指针，而不是指针指向的字符串
	bool isempty(){return top == 0;}
	bool isfull(){return top == stacksize;}
	bool push(const Type & item);
	bool pop(Type & item);
	Stack & operator=(const Stack & st);
};

template <class Type>
Stack<Type>::Stack(int ss) : stacksize(ss),top(0)
{
	items = new Type[stacksize];   //新分配内存空间
}

template <class Type>
Stack<Type>::Stack(const Stack & st)
{
	stacksize = st.stacksize;
	top = st.top;
	items = new Type[stacksize];
	for(int i =0;i<top;i++)
		items[i] = st.items[i];
}

template <class Type>
bool Stack<Type>::push(const Type & item)
{
	if(top < stacksize)
	{
		items[top++] = item;   //添加指针数组内容
		return true;
	}
	else
		return false;
}

template <class Type>
bool Stack<Type>::pop(Type & item)
{
	if(top > 0)
	{
		item = items[--top];   //弹出指针数组内容
		return true;
	}
	else
		return false;
}

template<class Type>
Stack<Type> & Stack<Type>::operator=(const Stack<Type> & st)
{
	if(this == &st)
		return *this;
	delete [] items;
	stacksize = st.top;
	items = new Type[stacksize];
	for(int i = 0;i<top;i++)
		items[i] = st.items[i];
	return *this;
}
#endif
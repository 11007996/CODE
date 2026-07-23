#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

struct node *add_link(struct node **body,int n);

struct node
{
	int i;
	struct node *next;
};



/*int main(void)
{
	struct node *first = NULL;


	struct node *new_node = NULL;
	new_node = malloc(sizeof(struct node));  //新建链表结点
	first = malloc(sizeof(struct node));  //链表头结点

	if(new_node == NULL)	
	{
		printf("内存分配失败");
		return 1;
	}
	new_node->i = 10;
	new_node->next = NULL;
	first->i = 0;  //头结点数据域可能表示链表的长度
	first->i += 1;
	first->next = new_node;    //新建结束加入到头结点中，新结点位于链表的末尾
	printf("%d\n",first->next->i);

	for(;first != NULL;first = first->next)
		printf("%d\n",first->i);

	free(new_node);
	free(first);
	return 0;
}*/


/*struct node* add_to_list(struct node *list,int n)
{
	struct node *new_node;
	new_node = malloc(sizeof(struct node));
	if(new_node==NULL)
	{
		puts("new_node内存分配失败");
		exit(EXIT_FAILURE);
	}
	new_node->i = n;
	new_node->next = list;
	return new_node;
}

struct node *read_numbers(void)
{
	struct node *first = NULL;
	int n;
	printf("输入链表数据");
	scanf("%d",&n);
	if(n==0)
		return first;
	first = add_to_list(first,n);
}

int main(void)
{
	struct node *first = NULL;
	first=read_numbers();
	for(;first != NULL;first = first->next)
		printf("%d\n",first->i);
		return 0;	
}*/


int getNum()
{
	int a = 0;
	srand((unsigned)time(NULL));
	a = rand() % 100+1;         
	return a;
}

struct node *add_first_node()
{
	struct node* first_node = NULL;
	struct node* body_node = NULL;
	first_node = malloc(sizeof(struct node));
	if(first_node == NULL)
	{
		puts("first_node内存分配失败");
		exit(EXIT_FAILURE);
	}
	body_node = malloc(sizeof(struct node));
	if(body_node == NULL)
	{
		puts("first_node内存分配失败");
		exit(EXIT_FAILURE);
	}
	first_node->i=0;
	int a = 0;
	puts("请输入增加链表长度");
	scanf("%d",&a);
	body_node=add_link(&body_node,a);
	first_node->i += a;
	first_node->next = body_node;
	
	return first_node;
}

struct node *add_link(struct node **body,int n)
{
	struct node* new_node = NULL;
	new_node = malloc(sizeof(struct node));
	if(new_node ==NULL)
	{
		puts("new_node内存分配失败");
		exit(EXIT_FAILURE);
	}
	for(int b=0;b<n;b++)
	{
		if(b==0)
		{
			new_node->i=getNum();
			new_node->next = NULL;
			*body = new_node;
			continue;
		}
		new_node->i=getNum();
		new_node->next = *body;
		*body = new_node;
	}
	// free(new_node);
	return *body;
}

int main(void)
{
	struct node *nodes = NULL;
	nodes=add_first_node();
	// printf("%d\n",nodes->next->next->next->i);
	for(;nodes != NULL;nodes=nodes->next)
	{
		printf("%d\n",nodes->i);
	}
	return 0;
}




/*
#define NAME_LEN 25

struct part
{
	int number;
	char name[NAME_LEN];
	int on_hand;
	struct part* next;
};
struct part *inventory = NULL;
struct part *fubd_part(int number);
void insert(void);
void search(void);
void update(void);
void print(void);

int read_line(char str[],int n)
{
	int ch,i=0;
	while(isspace(ch != EOF))
	{
		if(i<n)
			str[i++] = ch;
		ch = getchar();
	}
	str[i] = '\0';
	return i;
}

int main(void)
{
	char code;
	for(;;)
	{
		printf("Enter operation code: ");
		scanf(" %c",&code);
		while(getchar() != '\n')
			;
		switch(code)
		{
			case 'i':insert(); break;
			case 's':search(); break;
			case 'u':update(); break;
			case 'p':print(); break;
			case 'q':return 0;
			default:printf("Illegal code\n");
		}
		printf("\n");
	}
}

struct part* find_part(int number)
{
	struct part *p;
	for(p=inventory;p!=NULL&&number > p->number;p=p->next)
		;
	if(p!=NULL && number == p->number)
		return p;
	return NULL;
}


void insert(void)
{
	struct part *cur,*prev,*new_node;
	new_node = malloc(sizeof(struct part));
	if(new_node==NULL)
	{
		printf("Database is full\n");
		return;
	}
	printf("Enter part number: ");
	scanf("%d",&new_node->number);

	for(cur = inventory,prev = NULL;cur!=NULL && new_node->number > cur->number;prev = cur,cur = cur->next)
		;
	if(cur != NULL && new_node->number == cur ->number)
	{
		printf("Part already exists.\n");
		free(new_node);
		return;
	}
	printf("Enter part name: ");
	read_line(new_node->name,NAME_LEN);
	printf("Enter quantity on hand: ");
	scanf("%d",&new_node->on_hand);
	new_node->next = cur;
	if(prev == NULL)
		inventory = new_node;
	else
		prev->next = new_node;
}

void search(void)
{
	int number;
	struct part *p;
	printf("Enter part number: ");
	scanf("%d",&number);
	if(p!=NULL)
	{
		printf("Part name: %s\n",p->name);
		printf("Quantity on hand:%d\n",p->on_hand);
	}
	else
	{
		printf("Part not found.\n");
	}
}


void update(void)
{
	int number,change;
	struct part *p;
	printf("Enter part number: ");
	scanf("%d",&number);
	p = find_part(number);
	if(p != NULL)
	{
		printf("Enter change in quantity on hand: ");
		scanf("%d",&change);
		p->on_hand += change;
	}
	else
		printf("Part not found.\n");
}


void print(void)
{
	struct part *p;
	printf("Part Number Part Name Quantity on Hand\n");
	for (p = inventory;p!=NULL;p = p->next)
		printf("%7d   %-25s%1ld\n",p->number,p->name,p->on_hand);
}*/
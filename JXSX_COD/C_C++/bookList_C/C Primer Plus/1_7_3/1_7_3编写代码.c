#include<stdio.h>  //标准输入输出头文件， 提供键盘输入输出的支持
//1.7.3
/*int main(void)
{
    int dogs;
    printf("How many dogs do you have?\n");
    scanf("%d",&dogs);
    printf("So you have %d dog(s)!\n",dogs);
    getchar();
    return 0;
}*/

//2.1
/*int aa(int);   //函数原型
int main(void)
{
    int num = aa(2);
    //scanf("%d",&num);
    printf("I am a simple ");
    printf("computer.\n");
    printf("My favorite number is %d because it is first.\n",num);
    return 0;
}
int aa(int intA)
{
    int num;
    scanf("%d",&num);
    num = num + intA;
    return num;
}*/

//2.2
/*int main(void)
{
    int feet,fathoms;

    fathoms = 2;
    feet = 6* fathoms;
    printf("There are %d feet in %d fathoms!\n",feet,fathoms);
    printf("Yes,I said %d feet!\n",6*fathoms);
}*/

//2.3
/*void butler(void);    //函数原型，告知编译器在程序中要使用该函数
int main(void)
{
    printf("I will summon the butler function.\n");
    butler();   //调用函数
    printf("Yes.Bring me some tea and writeable DVDs.\n");
}
void butler(void)  //定义函数
{
    printf("You rang,sir?\n");
}*/
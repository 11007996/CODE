#include<stdio.h>
#include<string.h>
int main(void)
{
	/*int i = 0,j = 0;
	scanf("%d,%d",&i,&j);   //格式串中含有逗号，则只输入i的值，如果i输入时失误包含逗号，则将i做为两个数分别赋给i和j
	printf("i:%d\nj:%d\n",i,j);*/

	/*int i = -10;
	unsigned u = 10;
	printf("%d\n",i<u);    //避免有符号和无符号型混合混合使用，其中涉及隐式数据转换*/

	/*int i = -12.345;
	printf("%d\n",i);*/

	/*int a[5] = {0,0,1,2,3};
	int a[5] = {[2]=1,[3]=2,[4]=3};*/


	/*int n =0;
	scanf("%d",&n);
	int a[n];    //可变长数组，不能初始化，没有静态存储期限
	for( int i = 0;i<n;i++)
		scanf("%d",&a[i]);
	for( int j = n-1;j>=0;j--)
		printf("%d\n",a[j]);*/


	/*int a[3] = {1,2,3};
	int b[3] = {4,5,6};
	memcpy(a,b,sizeof(a));   //内存复制，把数组b复制到数组a
	for( int i = 0;i<3;i++)
		printf("%d\n",a[i]);*/


	return 0;   //在main函数中等同于exit(0)，退出程序
}	

/*int sum_array(int n,int a[n])     //可变长度数组作为函数参数
{
	//...
	return 0;
}*/



/*int sum_array(int a[static 3],int n)     //static表示a可变数组最少有3个元素
{
	//....
	return 0;
}
//int total = sum_array((int []){1,2,3,4,5},5);     //复合字面量做参数调用函数
*/



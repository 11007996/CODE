#include <stdio.h>

int main(int argc,char *argv[])
{
	FILE *tempptr;
	tempptr = tmpfile();    //创建一个临时文件，该临时文件一起存在，除非关闭它或程序终止，函数调用会返回文件指针，如果创建文件失败，函数会返回空指针

	char *filename;
	filename = tmpnam(NULL);    //创建一个临时文件,

	char filename[length];
	tmpname(filename);    //创建一个临时文件,文件名保存在数组中


	fflush(fp);   //清洗缓冲区，将缓冲区的数据写入磁盘，调用成功返回零，发生错误则返回EOF

	// setvbuf允许改变缓冲流的方法，并且允许控制缓冲区的大小和位置。
	// 函数的第三个实际参数指明了期望的缓冲类型，参数可为3个
	//   _IOFBF(满缓冲)   当缓冲区为空时，从流读入数据，当缓冲区满时，向流写入数据
	//   _IOLBF(行缓冲)   每次从流计入一行数据或者向流写入一行数据
	//   _IONBF(无缓冲)   直接从流计入数据或者直接向流写入数据，而没有缓冲区
	// 第二个参数是期望缓冲区的地址
	// 最后一个参数是缓冲区内字节的数量，缓冲区的大小
	setvbuf(FILE * restrict stream,char * restrict buf,int mode,size_t size); 
	char buffer[10];
	setvbuf(stream,buffer,_IOFBF,10);
	// setvbuf函数的调用必须在打开stream之后在对其执行任何其他操作之前

	setbuf(stream,buf)
	//相当于
	// setvbuf(stream,bur,_IOFBF,BUFSIZ)
	// 使用setvbuf函数或setbuf函数时，一定要确保在释放缓冲区之间已经关闭了流，特别是如果缓冲荀局部于函数的，并且具有自动存储期限，一定要确保在函数返回之前关闭流

	int remove(const char *filename);   //删除文件，remove("1.txt")
	int rename(const char *old,const char *new);   //重命名文件,rename("old.txt","nex.txt")
	// 如果打开了改名的文件，那么一定要确保在调用rename函数之前关闭此文件，对打开铁文件执行改名操作会失败

	int fprintf(FILE * restrict stream,const char *restrivr format,...);
	fprintf(fp,"total:%d\n",total);
	// fprintf与printf唯一的不同就是printf始终向stdout写入内容，而fprintf则向它自己的每第一个实际参数指定的流中写入内容

	fscanf(fp,"%d",&n);   //从第一个参数所指定的流中读入内容

	void clearerr(FILE *stream);   //清除错误指示器和文件末尾指示器
	int feof(FILE *stream);  //如果与stream相关的流设置了文件末尾指示器，那么feof(stream)函数调用就会返回非零数，如果设置了错误指示器，那么 ferror(stream)函数的调用也会返回非零数
	int ferror(FILE *stream);

	fputc(ch,fp);   //fputc和putc是putchar函数向任意流写字符的更通用的版本
	putc(ch,fp);

	ch = fgetc(fp);    //fgetc和getc函数从任意流中写入一个字符
	ch = getc(fp);

	int fputs(const char * restrivt s,FILE *restrict stream);   //从任意流中写入一个字符串
	int puts(const char * s);    //向标准输出流stdout写入字符串

	char *fgets(char * restrict s,int n,FILE * restrict stream);  //从任意流中写入一行字符串
	char *gets(char *s);    //向标准输出流stdint读取一行字符串

	//用来 在音频中读和写大的数据块
	size_t fread(void * restrict ptr,size_t size,size_t nmemb,FILE * restrict stream);
	// n = fread(a,siezof(a[0]),sizeof(a)/sizeof(a[0]),fp);     //返回值说明了实际读的元素的数量(不是字节)
	size_t fwrite(void * restrict ptr,size_t size,size_t nmemb,FILE * restrict stream);
	// fwrite调用中第一个参数就是数组的地址，第十个参数是每个数组元素的大小，第三个参数是要写的元素数量，第四个参数是文件指针，此指针说明了要写的数据位置
	// fwrite(a,siezof(a[0]),sizeof(a)/sizeof(a[0]),fp);

	int fgetpos(FILE * restrict stream,fpos_t * restrict pos);
	int fseek(FILE * stream,long int offset,int whence);
	// fseek第二个参数是指移动多少个字符
	// 第三个参数SEEK_SET(文件开始位置)/SEEK_CUR(当前位置)/SEEK_END(文件末尾)
	// fseek(fp,-10L,SEEK_CUR);
	int fsetpos(FILE * stream,const fpos_t *pos);
	long int ftell(FILE * stream);   //以长整数返回当前文件位置，调用错误返回-1
	void rewind(FILE * stream);	  //把文件位置设置在起始处，等价于fseek(fp,oL,SEEK_SET)

	int sprintf(char * restrict s,const char * restrict format,...);    //此函数与printf不同是sprintf函数把输出写入(第一个实参指向的)字符数组而不是流中
	int snprintf(char * restrict s,size_t n,... const char * restrict format,...);

	int sscanf(const char * restrict s,const char * restrict format,...)   //与fscanf不同是sscanf函数是从(第一个参数指向的)字符串而不是流中读取数据
}



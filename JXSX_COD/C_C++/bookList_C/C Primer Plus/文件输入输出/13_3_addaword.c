#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#define MAX 41
int main(void)
{
	FILE *fp;
	char words[MAX];
	if((fp=fopen("wordy","a+")) == NULL)   //以读写模式打开文件，在文件末尾添加内容
	{
		fprintf(stdout,"Can't open \"wordy\" file.\n");
		exit(EXIT_FAILURE);
	}

	puts("Enter words to add to the file;press then #");
	puts("Key at the beginning of a line to terminate.");
	while((fscanf(stdin,"%40s",words)==1)&&(words[0] != '#'))   //以#号作为第1个字符跳出循环
		fprintf(fp,"%s\n",words);
	puts("File contents:");
	rewind(fp);   //fp作为文件指针，使程序回到文件开始处
	while(fscanf(fp,"%s",words) ==1)
		puts(words);
	puts("Done!");
	if(fclose(fp)!=0)
		fprintf(stderr,"Error closing file\n");
	return 0;
}
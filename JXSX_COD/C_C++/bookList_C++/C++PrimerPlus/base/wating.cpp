#include <iostream>
#include <ctime>
int main()
{
	using namespace std;
	cout << "Enter the delay time,in seconds: ";
	float secs;
	cin >> secs;
	clock_t deley = secs * CLOCKS_PER_SEC;
	cout << "starting\a\n";
	clock_t start = clock();
	while(clock() - start < deley);
	cout << "done \a\n";

	#if 0    //预处理器注释代码
	cout << "Hello worrld"
	#endif
	return 0;
}
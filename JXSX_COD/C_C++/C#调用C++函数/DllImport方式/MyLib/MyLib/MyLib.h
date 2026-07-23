// MyLib.h

#ifdef MYLIB_EXPORTS
#define MYLIB_API __declspec(dllexport)
#else
#define MYLIB_API __declspec(dllimport)
#endif

extern "C" MYLIB_API int addition(int a, int b);  // export the function

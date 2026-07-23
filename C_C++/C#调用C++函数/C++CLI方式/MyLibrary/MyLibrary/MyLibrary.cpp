// 这是主 DLL 文件。

#include "stdafx.h"

#include "MyLibrary.h"

#include "pch.h"
#include "LibraryWrapper.h"
#include "Library.h"

using namespace MyLibrary;

LibraryWrapper::LibraryWrapper()
{
	lib = new Library();  // create a new Library object
}

LibraryWrapper::~LibraryWrapper() {
	delete lib;  // release the memory
}

int LibraryWrapper::Addition(int a, int b)
{
	return lib->addition(a, b);  // call the addition method in C++ library
}
// MyLibrary.h

#pragma once

namespace MyLibrary {

	public ref class LibraryWrapper
	{
	private:
		Library* lib;  // the C++ object we want to wrap
	public:
		LibraryWrapper();  // constructor
		~LibraryWrapper();  // destructor
		int Addition(int a, int b);  // method used to add two numbers
	};
}
using System;
using System.Collections;
using System.Collections.Generic;
namespace pde
{
    class Class1
    {
        static void Main()
        {
            IDictionary<string, string> openWith =  new  Dictionary< string ,  string >();
             //向字典中添加元素
             openWith.Add( "txt" ,  "notepad.exe" );
             openWith.Add( "bmp" ,  "paint.exe" );
             openWith.Add( "dib" ,  "paint.exe" );
             openWith.Add( "rtf" ,  "wordpad.exe" );
 
             // The Add method throws an exception if the new key is 
             // already in the dictionary.
             try
             {
                 openWith.Add( "txt" ,  "winword.exe" );
             }
             catch  (ArgumentException)
             {
                 Console.WriteLine( "An element with Key = \"txt\" already exists." );
             }
 
             //按键获取值
             Console.WriteLine( "For key = \"rtf\", value = {0}." , 
                 openWith[ "rtf" ]);
 
             //修改某个键值
             openWith[ "rtf" ] =  "winword.exe" ;
             Console.WriteLine( "For key = \"rtf\", value = {0}." , 
                 openWith[ "rtf" ]);
 
             // If a key does not exist, setting the indexer for that key
             // adds a new key/value pair.
             //新加一个键值对
             openWith[ "doc" ] =  "winword.exe" ;
 
             // The indexer throws an exception if the requested key is
             // not in the dictionary.
             try
             {
                 Console.WriteLine( "For key = \"tif\", value = {0}." , 
                     openWith[ "tif" ]);
             }
             catch  (KeyNotFoundException)
             {
                 Console.WriteLine( "Key = \"tif\" is not found." );
             }
 
             // When a program often has to try keys that turn out not to
             // be in the dictionary, TryGetValue can be a more efficient 
             // way to retrieve values.
             string  value =  "" ;
             if  (openWith.TryGetValue( "tif" ,  out  value))
             {
                 Console.WriteLine( "For key = \"tif\", value = {0}." , value);
             }
             else
             {
                 Console.WriteLine( "Key = \"tif\" is not found." );
             }
 
             // ContainsKey can be used to test keys before inserting 
             // them.
             if  (!openWith.ContainsKey( "ht" ))
             {
                 openWith.Add( "ht" ,  "hypertrm.exe" );
                 Console.WriteLine( "Value added for key = \"ht\": {0}" , 
                     openWith[ "ht" ]);
             }
 
             // When you use foreach to enumerate dictionary elements,
             // the elements are retrieved as KeyValuePair objects.
             //遍历所有元素
             Console.WriteLine();
             foreach ( KeyValuePair< string ,  string > kvp  in  openWith )
             {
                 Console.WriteLine( "Key = {0}, Value = {1}" , 
                     kvp.Key, kvp.Value);
             }
 
             // To get the values alone, use the Values property.
             //字典值的集合
             ICollection< string > icoll = openWith.Values;
 
             // The elements of the ValueCollection are strongly typed
             // with the type that was specified for dictionary values.
             Console.WriteLine();
             foreach (  string  s  in  icoll )
             {
                 Console.WriteLine( "Value = {0}" , s);
             }
 
             // To get the keys alone, use the Keys property.
             //字典键的集合
             icoll = openWith.Keys;
             // The elements of the ValueCollection are strongly typed
             // with the type that was specified for dictionary values.
             Console.WriteLine();
             foreach (  string  s  in  icoll )
             {
                 Console.WriteLine( "Key = {0}" , s);
             }
 
             // Use the Remove method to remove a key/value pair.
             //删除某个值
             Console.WriteLine( "\nRemove(\"doc\")" );
             openWith.Remove( "doc" );
 
             if  (!openWith.ContainsKey( "doc" ))
             {
                 Console.WriteLine( "Key \"doc\" is not found." );
             }
             openWith.Clear();  //清除所有元素
 
             Console.ReadLine();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace EFCodeSqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteHelper testDb = new SQLiteHelper(@"Data Source=D:\ruanjian\TOOL\sqlite\mydatabase.db;Version=3;");
            //SQLiteHelper.DataBaceList.Add("mydat", testDb);

            //SQLiteConnection con = testDb.GetSQLiteConnection();

           


            string cmdText = "select * from g_student_t where name=@name";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("@name", "ww");
            DataTable dt = testDb.ExecuteDataTable(cmdText, data);
            if(dt.Rows.Count>0)
            {
                Console.WriteLine(dt.Rows[0][0].ToString());
            }
            else
            { 
                Console.WriteLine("no data");
            }

            Console.ReadKey();
        }
    }
}

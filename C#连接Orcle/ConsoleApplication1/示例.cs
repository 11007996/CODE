using System;
 2 using System.Data;
 3 using Oracle.ManagedDataAccess.Client;
 4 
 5 namespace ODP.NET
 6 {
 7     class Program
 8     {
 9         static void Main(string[] args)
10         {
11             OracleConnection conn = null;
12             try
13             {
14                 conn = OpenConn();
15                 var cmd = conn.CreateCommand();
16                 cmd.CommandText = "select * from s_awb_master where rownum=1";
17                 cmd.CommandType = CommandType.Text;
18                 var reader = cmd.ExecuteReader();
19                 while (reader.Read())
20                 {
21                     Console.WriteLine(string.Format("AwbPre:{0},AwbNo:{1}", reader["AwbPre"], reader["AwbNo"]));
22                 }
23             }
24             catch (Exception ex)
25             {
26                 Console.WriteLine(ex.Message);
27             }
28             finally
29             {
30                 CloseConn(conn);
31             }
32             Console.Read();
33         }
34 
35 
36         static OracleConnection OpenConn()
37         {
38             OracleConnection conn = new OracleConnection();
39             conn.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=***.***.***.***)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=***)));Persist Security Info=True;User ID=***;Password=***;";
40             conn.Open();
41             return conn;
42         }
43 
44         static void CloseConn(OracleConnection conn)
45         {
46             if (conn == null) { return; }
47             try
48             {
49                 if (conn.State != ConnectionState.Closed)
50                 {
51                     conn.Close();
52                 }
53             }
54             catch (Exception e)
55             {
56                 Console.WriteLine(e.Message);
57             }
58             finally
59             {
60                 conn.Dispose();
61             }
62         }
63     }
64 }
/*方式一
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        bool status = false;
        //连接共享文件夹（路径，账号，密码）
        status = connectState(@"\\10.200.8.73\share", "administrator", "11111111");
        if (status)
        {
                //共享文件夹的目录
            DirectoryInfo theFolder = new DirectoryInfo(@"\\10.200.8.73\share");
                //相对共享文件夹的路径
            string fielpath=@"\123\456\";
                //获取保存文件的路径
            string filename = theFolder.ToString() +fielpath ;
                //执行方法（原文件路径，要保存到的路径，要保存到的文件名）
            Transport(@"D:\1.jpg", filename, "1.jpg");
        }
        else
        {
                //ListBox1.Items.Add("未能连接！");
        }
        Console.ReadKey();
    }

    public static bool connectState(string path)
    {
        return connectState(path, "", "");
    }
        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
    public static bool connectState(string path, string userName, string passWord)
    {
        bool Flag = false;
        Process proc = new Process();
        try
        {
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
            proc.StandardInput.WriteLine(dosLine);
            proc.StandardInput.WriteLine("exit");
            while (!proc.HasExited)
            {                   
                proc.WaitForExit(1000);
            }
            string errormsg = proc.StandardError.ReadToEnd();
            proc.StandardError.Close();
            if (string.IsNullOrEmpty(errormsg))
            {
                Flag = true;
            }
            else
            {
                throw new Exception(errormsg);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            proc.Close();
            proc.Dispose();
        }
        return Flag;
    }

        /// <summary>
        /// 向远程文件夹保存本地内容，或者从远程文件夹下载文件到本地
        /// </summary>
        /// <param name="src">要保存的文件的路径，如果保存文件到共享文件夹，这个路径就是本地文件路径如：@"D:\1.avi"</param>
        /// <param name="dst">保存文件的路径，不含名称及扩展名</param>
        /// <param name="fileName">保存文件的名称以及扩展名</param>
    public static void Transport(string src, string dst,string fileName)
    {
            //原文件
        FileStream inFileStream = new FileStream(src, FileMode.Open);
        if (!Directory.Exists(dst))
        {
            Directory.CreateDirectory(dst);
        }
        dst = dst  + fileName;
            //保存到某个路径
        FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);

        byte[] buf = new byte[inFileStream.Length];

        int byteCount;

            //原文件读取出来然后写入到新的路径
        while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
        {
            outFileStream.Write(buf, 0, byteCount);
        }

        inFileStream.Flush();
        inFileStream.Close();
        outFileStream.Flush();
        outFileStream.Close();
    }
}
*/








//角色模拟
/*
public class utility
{
        // logon types
    const int LOGON32_LOGON_INTERACTIVE = 2;
    const int LOGON32_LOGON_NETWORK = 3;
    const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        // logon providers
    const int LOGON32_PROVIDER_DEFAULT = 0;
    const int LOGON32_PROVIDER_WINNT50 = 3;
    const int LOGON32_PROVIDER_WINNT40 = 2;
    const int LOGON32_PROVIDER_WINNT35 = 1;

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    public static extern int LogonUser(String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    public static extern bool RevertToSelf();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]

    public static extern bool CloseHandle(IntPtr handle);

    private WindowsImpersonationContext impersonationContext;

    public bool impersonateValidUser(String userName, String domain, String password)
    {
        WindowsIdentity tempWindowsIdentity;
        IntPtr token = IntPtr.Zero;
        IntPtr tokenDuplicate = IntPtr.Zero;

        if (RevertToSelf())
        {
                // 这里使用LOGON32_LOGON_NEW_CREDENTIALS来访问远程资源。
                // 如果要（通过模拟用户获得权限）实现服务器程序，访问本地授权数据库可
                // 以用LOGON32_LOGON_INTERACTIVE

            if (LogonUser(userName, domain, password, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
            {
                if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                {
                    tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                    impersonationContext = tempWindowsIdentity.Impersonate();
                    if (impersonationContext != null)
                    {
                        System.AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                        IPrincipal pr = System.Threading.Thread.CurrentPrincipal;
                        IIdentity id = pr.Identity;
                        CloseHandle(token);
                        CloseHandle(tokenDuplicate);
                        return true;
                    }
                }
            }
        }

        if (token != IntPtr.Zero)
        CloseHandle(token);

        if (tokenDuplicate != IntPtr.Zero)
        CloseHandle(tokenDuplicate);

        return false;
    }

    public void undoImpersonation()
    {
        impersonationContext.Undo();
    }

    public void TestFunc()
    {
        bool isImpersonated = false;

        try
        {
            if (impersonateValidUser("UserName", "Domain", "Password"))
            {
                isImpersonated = true;
                    //do what you want now, as the special user
                    // ...
                File.Copy(@"\\192.168.1.48\generals\now.htm", "c:\\now.htm", true);
            }
        }
        finally
        {
            if (isImpersonated)
            undoImpersonation();
        }
    }
}
utility myutility = new utility();
myutility.impersonateValidUser("mesdll", "172.18.20.188", "Lux.1qaz@WSX#EDC");    
//原文件      
FileInfo finfo = new FileInfo(sourcepath);
string fileName = finfo.Name;
string foldname = DateTime.Now.ToString("yyyyMMddHHmm") + fileName.Substring(fileName.LastIndexOf("\\") + 1, (fileName.LastIndexOf(".") - fileName.LastIndexOf("\\") - 1));
string servicePath = @"\\172.18.20.188\Chroma_back\" + foldname + "\\";

string servicePath1 = servicePath + "备份\\";
if (!Directory.Exists(servicePath))
{
    Directory.CreateDirectory(servicePath);
}
if (!Directory.Exists(servicePath1))
{
    Directory.CreateDirectory(servicePath1);
}
if (target1 != null && target1 != "")
{
    target = target1;
}
else
{
    target = target + "\\" + finfo.Name;
}
FileInfo finfoa = new FileInfo(target);
string path = servicePath + finfo.Name;
string path1 = servicePath1 + finfoa.Name; 
//copy(原文件路径，远程文件路径，)
File.Copy(finfo.FullName, path);              
if (finfoa.Exists)
{
    finfoa.CopyTo(path1);
}
else
{
    File.Copy(finfo.FullName, path1);
}
*/

















//角色模拟方式二
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
 
namespace ConsoleApp41
{
    class Program
    {
        static void Main(string[] args)
        {
            //模拟身份
            using (SharedTool tool = new SharedTool("administrator", "123456", "10.10.10.1"))
            {

                // File.WriteAllText("1.txt", "12345,67890");   //写入共享文件夹

                //读取文件夹
                string selectPath = @"\\10.10.10.1\c$";
 
                var dicInfo = new DirectoryInfo(selectPath);//选择的目录信息  
 
                DirectoryInfo[] dic = dicInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo temp in dic)
                {
                    Console.WriteLine(temp.FullName);
                }
 
                Console.WriteLine("---------------------------");
                FileInfo[] textFiles = dicInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);//获取所有目录包含子目录下的文件  
                foreach (FileInfo temp in textFiles)
                {
                    Console.WriteLine(temp.Name);
                }
            }
 
            Console.ReadKey();
        }
    }
 
    public class SharedTool : IDisposable
    {
        // obtains user token       
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
 
        // closes open handes returned by LogonUser       
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static bool CloseHandle(IntPtr handle);
 
        [DllImport("Advapi32.DLL")]
        static extern bool ImpersonateLoggedOnUser(IntPtr hToken);
 
        [DllImport("Advapi32.DLL")]
        static extern bool RevertToSelf();
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_NEWCREDENTIALS = 9;//域控中的需要用:Interactive = 2       
        private bool disposed;
 
        public SharedTool(string username, string password, string ip)
        {
            // initialize tokens       
            IntPtr pExistingTokenHandle = new IntPtr(0);
            IntPtr pDuplicateTokenHandle = new IntPtr(0);
 
            try
            {
                // get handle to token       
                bool bImpersonated = LogonUser(username, ip, password,
                    LOGON32_LOGON_NEWCREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);
 
                if (bImpersonated)
                {
                    if (!ImpersonateLoggedOnUser(pExistingTokenHandle))
                    {
                        int nErrorCode = Marshal.GetLastWin32Error();
                        throw new Exception("ImpersonateLoggedOnUser error;Code=" + nErrorCode);
                    }
                }
                else
                {
                    int nErrorCode = Marshal.GetLastWin32Error();
                    throw new Exception("LogonUser error;Code=" + nErrorCode);
                }
            }
            finally
            {
                // close handle(s)       
                if (pExistingTokenHandle != IntPtr.Zero)
                    CloseHandle(pExistingTokenHandle);
                if (pDuplicateTokenHandle != IntPtr.Zero)
                    CloseHandle(pDuplicateTokenHandle);
            }
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                RevertToSelf();
                disposed = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
        }
    }
 
}
*/
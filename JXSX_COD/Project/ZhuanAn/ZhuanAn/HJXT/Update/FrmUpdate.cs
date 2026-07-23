using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Update.Utils;

namespace Update
{
    public partial class FrmUpdate : Form
    {
        private DataTable sysFiles;
        private int allFileSize = 0;

        public FrmUpdate()
        {
            InitializeComponent();
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            //获取所有最新版本文件信息。
            string sql = "SELECT FileId,FileName,FileSize,FileTime,FileVersion,UpdateMode,Remark FROM S_SystemFile_T WHERE UseFlag='Y' AND UpdateStatus='Y' ";
            sysFiles = DBUtil.GetDataTable(sql);
            CompareFileInfo();
            if (sysFiles.Rows.Count == 0)
            {
                //没有更新，直接启动呼叫系统。
                StartCallSys();
            }
            else
            {
                //更新系统文件
                StartUpdateFiles();
            }
        }

        //比对系统文件与当前项目文件，判断哪些文件要更新
        public void CompareFileInfo()
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = sysFiles.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = sysFiles.Rows[i];
                string filePath = dirPath + row["FileName"].ToString();
                FileInfo fi = new FileInfo(filePath);
                string fileVersion = "";
                if (fi.Exists)
                {
                    System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
                    fileVersion = info.FileVersion;
                    fileVersion = fileVersion == null ? "" : fileVersion;
                }
                //清除不需要更新的文件(比对，文件名称相同，文件创建时间大于上传时间，版本号相同)
                if (fi.Exists && fi.CreationTime >= Convert.ToDateTime(row["FileTime"].ToString()) && row["FileVersion"].ToString() == fileVersion)
                {
                    sysFiles.Rows.Remove(row);
                }
                else
                {
                    if (fi.Exists) fi.Delete();
                    allFileSize += Int32.Parse(row["FileSize"].ToString());
                }
            }
        }

        //开始更新任务
        private void StartUpdateFiles()
        {
            BackgroundWorker bgwA = new BackgroundWorker();
            bgwA.WorkerReportsProgress = true;
            bgwA.DoWork += bgwA_DoWork;
            bgwA.ProgressChanged += bgwA_ProgressChanged;
            bgwA.RunWorkerCompleted += bgwA_Completed;
            bgwA.RunWorkerAsync();
        }

        //任务内容
        private void bgwA_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgworker = sender as BackgroundWorker;
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            int currLoadSize = 0;

            foreach (DataRow row in sysFiles.Rows)
            {
                //文件信息
                string fileName = row["FileName"].ToString();
                int fileSize = Int32.Parse(row["FileSize"].ToString());

                //下载
                string sql = string.Format("SELECT FileContent FROM S_SystemFile_T WHERE FileId={0} ", row["FileId"].ToString());
                DataTable dt = DBUtil.GetDataTable(sql);
                //写出
                byte[] bt = (byte[])dt.Rows[0]["FileContent"];
                FileStream fs = new FileStream(dirPath + row["FileName"].ToString(), FileMode.Create, FileAccess.Write);
                fs.Write(bt, 0, bt.Length);
                fs.Close();
                //文件创建时间
                FileInfo fi = new FileInfo(dirPath + row["FileName"].ToString());
                fi.CreationTime = TimeUtil.Now;

                currLoadSize += Int32.Parse(row["FileSize"].ToString());
                //回传进度
                int scale = (int)Math.Floor(((decimal)currLoadSize / (decimal)allFileSize) * 100);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("fileName", fileName);
                data.Add("fileSize", fileSize);
                data.Add("remark", "说明: " + row["Remark"].ToString());
                bgworker.ReportProgress(scale, data);
                System.Threading.Thread.Sleep(200);
            }
            Thread.Sleep(3000);
        }

        //更新UI控件显示内容
        private void bgwA_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Dictionary<string, object> data = e.UserState as Dictionary<string, object>;
            this.proBar.Value = e.ProgressPercentage;

            lsbUpdateDetail.Items.Add("下载: " + data["fileName"] + "  大小:" + data["fileSize"]);
            labFileName.Text = "文件:" + data["fileName"];
            if (!string.IsNullOrWhiteSpace(data["remark"].ToString()))
                lsbUpdateDetail.Items.Add(data["remark"].ToString());
            if (lsbUpdateDetail.Items.Count > 7)
            {
                lsbUpdateDetail.TopIndex = lsbUpdateDetail.Items.Count - 7;
            }
            this.labScale.Text = e.ProgressPercentage.ToString() + "%";
        }

        //更新完成重启
        private void bgwA_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            this.labFileName.Text = "更新完成,重启中...";
            StartCallSys();
        }


        //更新完成后启动主系统
        private void StartCallSys()
        {
            string username = string.IsNullOrWhiteSpace(GlobalData.UserName) ? "\"\"" : GlobalData.UserName;
            //启动CallSys.exe。退出
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "CallSys.exe", string.Format("{0} {1} {2}", "更新完成", username, GlobalData.LastFormName));
            System.Environment.Exit(0);
        }

    }
}

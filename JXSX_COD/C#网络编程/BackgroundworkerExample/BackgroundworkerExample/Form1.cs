using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace BackgroundworkerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //WorkerReportsProgress表示ReportProgress方法有效，可以触发ProgressChanged事件
            backgroundWorker1.WorkerReportsProgress = true;
            //WorkerSupportsCancellation表示CancelAsync方法有效，可以挂起异步操作
            backgroundWorker1.WorkerSupportsCancellation = true;
            //buttonStop.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                richTextBox1.Text = "开始产生1000以内的随机数……\n\n";
                //buttonStart.Enabled = false;
                //buttonStop.Enabled = true;
                //RunWorkerAsync触发DoWork事件
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Random r = new Random();
            int numCount = 0;
            //CancellationPending表示是否取消挂起后台操作
            while (worker.CancellationPending == false)
            {
                int num = r.Next(1000);
                if (num % 5 == 0)
                {
                    numCount++;
                    //ReportProgress触发ProgressChanged事件
                    worker.ReportProgress(0, num);
                    Thread.Sleep(1000);
                }
            }
            e.Result = numCount;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int num = (int)e.UserState;
            richTextBox1.Text += num + " ";
        }
        //后台操作挂起后执行此操作
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                richTextBox1.Text += "\n\n操作停止，共产生" + e.Result + "个能被5整除的随机数";
            }
            else
            {
                richTextBox1.Text += "\n操作过程中产生错误：" + e.Error;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            //挂起异步操作
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            //buttonStop.Enabled = false;
            //buttonStart.Enabled = true;
        }
    }
}

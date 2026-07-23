using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Call;
using System.Drawing.Drawing2D;
using Common.Util;

namespace Call
{
    public partial class FrmBarScan : Form
    {
        private int _X;
        private int _Y;

        private FrmMaster _Owner;
        public FrmBarScan()
        {
            InitializeComponent();
            SetWindowRegion();
        }

        private void FrmBarScan_Load(object sender, EventArgs e)
        {
            tbUserNo.Focus();
            _Owner = (FrmMaster)this.Owner;
        }

        private void tbUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13 && !string.IsNullOrWhiteSpace(tbUserNo.Text.Trim()))
                {
                    string msg = "";
                    bool isOk = _Owner.ScanCard_Handle(tbUserNo.Text.Trim(), ref msg);
                    tbUserNo.Text = "";
                    if (isOk)
                    {
                        this.Close();
                        this.Dispose();
                    }
                    else
                    {
                        labMessage.Text = msg;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBarScan), null, ex);
                return;
            }
        }

        #region 窗体样式
        public void SetWindowRegion()
        {
            GraphicsPath FormPath;

            FormPath = new System.Drawing.Drawing2D.GraphicsPath();

            Rectangle rect = new Rectangle(-1, -1, this.Width + 1, this.Height);

            FormPath = GetRoundedRectPath(rect, 24);

            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;

            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));

            GraphicsPath path = new GraphicsPath();

            //   左上角   
            path.AddArc(arcRect, 185, 90);

            //   右上角   
            arcRect.X = rect.Right - diameter;

            path.AddArc(arcRect, 275, 90);

            //   右下角   
            arcRect.Y = rect.Bottom - diameter;

            path.AddArc(arcRect, 356, 90);

            //   左下角   
            arcRect.X = rect.Left;

            arcRect.Width += 2;

            arcRect.Height += 2;

            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();

            return path;
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _X + _Y != 0)
            {
                this.Left += e.X - _X;
                this.Top += e.Y - _Y;
            }
            else
            {
                _X = e.X;
                _Y = e.Y;
            }
        }
        #endregion


        public void SetFormData(string stateTip, string machineTip, string handlerTip)
        {
            labState.Text = stateTip;
            labMachine.Text = machineTip;
            labUser.Text = handlerTip;
        }

        private void FrmBarScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            tbUserNo.Text = "";
            labMessage.Text = "";
            labMachine.Text = "";
            labState.Text = "";
        }

        private void FrmBarScan_Activated(object sender, EventArgs e)
        {
            if (!VirtualKeyboardHelper.IsKeyboardAttached())
            {
                VirtualKeyboardHelper.ShowVirtualKeyboard();
            }
        }

    }
}

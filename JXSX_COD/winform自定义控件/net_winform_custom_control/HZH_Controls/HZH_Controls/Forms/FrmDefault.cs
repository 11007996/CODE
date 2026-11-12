using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HZH_Controls.Forms
{
    public partial class FrmDefault : FrmBase
    {
        private Color _borderColor;
        [Description("边框及标题颜色"), Category("自定义")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                lblTitle.BackColor = btnMin.BackColor = btnMax.BackColor = btnClose.BackColor = ucSplitLine_H1.BackColor = ucSplitLine_V1.BackColor = ucSplitLine_V2.BackColor;

            }
        }
        private bool _isShowMinBtn = true;
        [Description("是否显示最小化按钮"), Category("自定义")]
        public bool IsShowMinBtn
        {
            get { return _isShowMinBtn; }
            set
            {
                _isShowMinBtn = value;
                btnMin.Visible = value;
                btnMin.BringToFront();
                ReloadBtn();
            }
        }
        private bool _isShowMaxBtn = true;
        [Description("是否显示最大化按钮"), Category("自定义")]
        public bool IsShowMaxBtn
        {
            get { return _isShowMaxBtn; }
            set
            {
                _isShowMaxBtn = value;
                btnMax.Visible = value;
                btnMax.BringToFront();
                ReloadBtn();
            }
        }

        private bool _isShowCloseBtn = true;
        [Description("是否显示关闭按钮"), Category("自定义")]
        public bool IsShowCloseBtn
        {
            get { return _isShowCloseBtn; }
            set
            {
                _isShowCloseBtn = value;
                btnClose.Visible = value;
                btnClose.BringToFront();
                ReloadBtn();
            }
        }
        [Bindable(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsFullSize
        {
            get { return base.IsFullSize; }
            set { base.IsFullSize = value; }
        }
        public FrmDefault()
        {
            InitializeComponent();
            base.IsFullSize = false;
            ReloadBtn();
            InitFormMove(this.lblTitle);
        }

        private void FrmDefault_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                btnMax.BackgroundImage = Properties.Resources.恢复;
            }
            else
            {
                btnMax.BackgroundImage = Properties.Resources.最大化;
            }
            ReloadBtn();
        }

        private void ReloadBtn()
        {
            int r = 0;
            if (_isShowCloseBtn)
            {
                r += 31 + 10;
                btnClose.Location = new Point(this.ClientRectangle.Right - r, btnClose.Location.Y);
                btnClose.BringToFront();
            }
            if (_isShowMaxBtn)
            {
                r += 31 + 10;
                btnMax.Location = new Point(this.ClientRectangle.Right - r, btnMax.Location.Y);
                btnMax.BringToFront();
            }
            if (_isShowMinBtn)
            {
                r += 31 + 10;
                btnMin.Location = new Point(this.ClientRectangle.Right - r, btnMin.Location.Y);
                btnMin.BringToFront();
            }
        }

        private void FrmDefault_Shown(object sender, EventArgs e)
        {
            ReloadBtn();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDefault_SizeChanged(object sender, EventArgs e)
        {
            ReloadBtn();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        bool blnMax = false;
        Rectangle nomoRect;
        private void btnMax_Click(object sender, EventArgs e)
        {
            var rect = Screen.GetWorkingArea(this);
            if (this.Size == rect.Size)
            {
                btnMax.BackgroundImage = Properties.Resources.最大化;
                this.Size = nomoRect.Size;
                this.Location = nomoRect.Location;
            }
            else
            {
                btnMax.BackgroundImage = Properties.Resources.恢复;
                nomoRect = this.DesktopBounds;
                this.Size = rect.Size;
                this.Location = rect.Location;
            }
        }

        private void FrmDefault_TextChanged(object sender, EventArgs e)
        {
            lblTitle.Text = Text;
        }
    }
}

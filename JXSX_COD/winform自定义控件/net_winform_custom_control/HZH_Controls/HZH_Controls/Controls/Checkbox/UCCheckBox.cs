// ***********************************************************************
// Assembly         : HZH_Controls
// Created          : 08-08-2019
//
// ***********************************************************************
// <copyright file="UCCheckBox.cs">
//     Copyright by Huang Zhenghui(黄正辉) All, QQ group:568015492 QQ:623128629 Email:623128629@qq.com
// </copyright>
//
// Blog: https://www.cnblogs.com/bfyx
// GitHub：https://github.com/kwwwvagaa/NetWinformControl
// gitee：https://gitee.com/kwwwvagaa/net_winform_custom_control.git
//
// If you use this code, please keep this note.
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HZH_Controls.Controls
{
    /// <summary>
    /// Class UCCheckBox.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [DefaultEvent("CheckedChangeEvent")]
    public partial class UCCheckBox : UserControl
    {
        /// <summary>
        /// 选中改变事件
        /// </summary>
        [Description("选中改变事件"), Category("自定义")]
        public event EventHandler CheckedChangeEvent;
        /// <summary>
        /// 字体
        /// </summary>
        /// <value>The font.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("字体"), Category("自定义")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                label1.Font = value;
            }
        }
        private Color _boxColor = Color.FromArgb(64, 158, 255);
        [Description("颜色"), Category("自定义")]
        public Color BoxColor
        {
            get { return _boxColor; }
            set
            {
                _boxColor = value;
                panel1.Refresh();
            }
        }
        /// <summary>
        /// The fore color
        /// </summary>
        private Color _ForeColor = Color.FromArgb(62, 62, 62);
        /// <summary>
        /// 字体颜色
        /// </summary>
        /// <value>The color of the fore.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("字体颜色"), Category("自定义")]
        public new Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                base.ForeColor = value;
                label1.ForeColor = value;
                _ForeColor = value;
            }
        }
        /// <summary>
        /// The text
        /// </summary>
        private string _Text = "复选框";
        /// <summary>
        /// 文本
        /// </summary>
        /// <value>The text value.</value>
        [Description("文本"), Category("自定义")]
        public string TextValue
        {
            get { return _Text; }
            set
            {
                label1.Text = value;
                _Text = value;
            }
        }
        /// <summary>
        /// The checked
        /// </summary>
        private bool _checked = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        [Description("是否选中"), Category("自定义")]
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    Refresh();
                    if (CheckedChangeEvent != null)
                    {
                        CheckedChangeEvent(this, null);
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示控件是否可以对用户交互作出响应。
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                Refresh();

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UCCheckBox" /> class.
        /// </summary>
        public UCCheckBox()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Handles the MouseDown event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void CheckBox_MouseDown(object sender, MouseEventArgs e)
        {
            Checked = !Checked;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
            //e.Graphics.Clear(panel1.BackColor);

            Rectangle rect = new Rectangle(new Point(2, (this.panel1.Height - 14) / 2), new Size(14, 14));
            e.Graphics.SetGDIHigh();
            if (this.Enabled)
            {
                if (_checked)
                {
                    e.Graphics.DrawRectangle(new Pen(_boxColor, 2), rect);
                    e.Graphics.FillRectangle(new SolidBrush(_boxColor), new Rectangle(rect.Left + 3, rect.Top + 3, 8, 8));
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(_boxColor, 2), rect);
                }
            }
            else
            {
                if (_checked)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189), 2), rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(189, 189, 189)), new Rectangle(rect.Left + 3, rect.Top + 3, 8, 8));
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189), 2), rect);

                }
            }
        }

        private void UCCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
    }
}

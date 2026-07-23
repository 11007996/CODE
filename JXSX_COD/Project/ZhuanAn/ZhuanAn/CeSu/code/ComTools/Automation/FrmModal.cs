using ComTools.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ComTools.Automation
{
    public partial class FrmModal : Form
    {
        public ElementInfo SelectedElement;
        private static ILog log = LogManager.GetLogger(typeof(FrmModal));

        //最后确认的控件元素
        private List<AutomationElement> Elements = new List<AutomationElement>();

        private TransparentPanel FocusControl;
        private string TargetWindowClassName;
        private IntPtr TargetWindowHandle;//目标窗口的句柄
        private string TargetWindowName;//窗口名称

        //目标窗口的类名
        private Rectangle TargetWindowRect;//目标窗口的坐标区域

        //当前鼠标所在目标窗口的控件

        #region 构造函数

        public FrmModal()
        {
            InitializeComponent();
            //选择窗口
            FrmSelectWindow frm = new FrmSelectWindow();
            frm.ShowDialog();
            if (frm.SelectedWindow != null)
            {
                TargetWindowHandle = frm.SelectedWindow.HWnd;
                WindowEnumerator.GetWindowInfo(TargetWindowHandle, out TargetWindowClassName, out TargetWindowName);
            }
            else
            {
                this.Close();
            }
        }

        public FrmModal(string windowTitle)
        {
            if (string.IsNullOrEmpty(windowTitle))
                this.Close();
            InitializeComponent();
            TargetWindowHandle = WindowEnumerator.GetWindowHandleByName(windowTitle);
            WindowEnumerator.GetWindowInfo(TargetWindowHandle, out TargetWindowClassName, out TargetWindowName);
        }

        #endregion 构造函数

        /// <summary>
        /// 设置焦点框位置
        /// </summary>
        /// <param name="rect"></param>
        public void SetFocusControlRect(Rectangle rect)
        {
            if (rect != null)
            {
                FocusControl.SetBounds(rect.X - TargetWindowRect.X, rect.Y - TargetWindowRect.Y, rect.Width, rect.Height);
            }
        }

        /// <summary>
        /// 设置焦点框位置
        /// </summary>
        /// <param name="rect"></param>
        public void SetFocusControlRect(System.Windows.Rect sysRect)
        {
            if (sysRect != null)
            {
                SetFocusControlRect(AutomationUtil.ConvertWindowRectToRectangle(sysRect));
            }
        }

        /// <summary>
        /// 判断鼠标是否在指定的元素区域内
        /// </summary>
        /// <param name="element"></param>
        private bool CheckMouseInElement(AutomationElement element)
        {
            Rectangle rect = AutomationUtil.ConvertWindowRectToRectangle(element.Current.BoundingRectangle);
            return rect.Contains(Cursor.Position);
        }

        /// <summary>
        /// 焦点控件的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusControl_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            //获取包含鼠标的所有控件
            List<ElementInfo> list = new List<ElementInfo>();
            foreach (AutomationElement ele in Elements)
            {
                if (CheckMouseInElement(ele))
                {
                    ElementInfo eleInfo = new ElementInfo()
                    {
                        WindowName = TargetWindowName,
                        ControlType = ele.Current.ControlType.ProgrammaticName,
                        AutomationID = ele.Current.AutomationId,
                        Rect = AutomationUtil.ConvertWindowRectToRectangle(ele.Current.BoundingRectangle),
                        ControlValue = AutomationUtil.GetControlValue(ele),
                    };
                    list.Add(eleInfo);
                }
            }
            //打开控件选择
            FrmModelSelected frm = new FrmModelSelected(list);
            frm.Owner = this;
            frm.ShowDialog();

            this.SelectedElement = frm.SelectedElement;
            this.Close();
        }

        /// <summary>
        /// 关闭模态窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowEnumerator.RemoveTopMost(TargetWindowHandle);
            timer1.Enabled = false;
        }

        /// <summary>
        /// 按下ESC关闭当前窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmModal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void FrmModal_Load(object sender, EventArgs e)
        {
            //显示要选择的窗口
            WindowEnumerator.ShowWindowsByHWnd(TargetWindowHandle);
            //设置选择的窗口为最顶层
            WindowEnumerator.SetAsTopMost(TargetWindowHandle);
            //显示模态窗口
            this.Show();
            //设置模态窗口大小
            AutomationElement windowElement = AutomationUtil.GetWindowAutomationElement(TargetWindowName, TargetWindowClassName);
            if (windowElement == null)
            {
                MessageBox.Show("未找到目标窗口");
                this.Close();
                return;
            }
            TargetWindowRect = AutomationUtil.ConvertWindowRectToRectangle(windowElement.Current.BoundingRectangle);
            this.SetBounds(TargetWindowRect.X, TargetWindowRect.Y, TargetWindowRect.Width, TargetWindowRect.Height);
            //初始化焦点框
            FocusControl = new TransparentPanel();
            FocusControl.SetBounds(0, 0, TargetWindowRect.Width, TargetWindowRect.Height);
            FocusControl.Click += FocusControl_Click;
            this.Controls.Add(FocusControl);

            //获取所有的控件
            Elements = AutomationUtil.GetAllElements(TargetWindowName, TargetWindowClassName);
        }

        /// <summary>
        /// 根据鼠标位置设置焦点控件的位置
        /// </summary>
        private void SetFocusControlByMouse()
        {
            foreach (AutomationElement ele in Elements)
            {
                if (CheckMouseInElement(ele))
                {
                    SetFocusControlRect(ele.Current.BoundingRectangle);
                }
            }
        }

        /// <summary>
        /// 定时器任务
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            SetFocusControlByMouse();
            timer1.Start();
        }
    }

    #region 自定义控件（透明红框）

    /// <summary>
    /// 自定义控件:焦点控件
    /// </summary>
    public partial class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 绘制边框（这里以红色为例）
            using (Pen borderPen = new Pen(Color.Red, 3))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }
        }
    }

    #endregion 自定义控件（透明红框）
}
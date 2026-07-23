using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Util
{
    public class FormUtil
    {
        /// <summary>
        /// 清除子控件（表单输入类控件）Text值
        /// </summary>
        /// <param name="parentControl"></param>
        public static void ClearControls(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if ((c is TextBox) || (c is ComboBox) || (c is NumericUpDown))
                    c.Text = "";
                if (c.GetType() == typeof(ListBox))
                {
                    ListBox a = (ListBox)c;
                    a.DataSource = null;
                    a.Items.Clear();
                }
            }
        }

        /// <summary>
        /// 清除子控件（表单输入类控件）Text值的左右空白字符
        /// </summary>
        /// <param name="parentControl"></param>
        public static void ClearControlsSpace(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if ((c is TextBox) || (c is ComboBox))
                    c.Text = c.Text.Trim();
            }
        }

    }
}

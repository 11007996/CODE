using CallSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallSys
{
    public partial class FrmInputDialog : Form
    {
        public delegate void TextEventHandle(string text);
        public TextEventHandle TextHandler;

        public static DialogResult Show(out string outText)
        {
            string text = string.Empty;
            FrmInputDialog frmInput = new FrmInputDialog();
            frmInput.TextHandler = (str) => { text = str; };
            DialogResult result = frmInput.ShowDialog();
            outText = text;
            return result;
        }

        public FrmInputDialog()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string inputVal = tbInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(inputVal))
            {
                MessageBox.Show("输入值不能为空");
                return;
            }
            if (null != TextHandler)
            {
                TextHandler.Invoke(inputVal);
                DialogResult = DialogResult.Yes;
            }

        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }



    }
}

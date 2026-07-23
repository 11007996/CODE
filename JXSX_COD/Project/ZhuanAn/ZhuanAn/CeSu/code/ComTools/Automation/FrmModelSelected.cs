using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComTools.Automation
{
    public partial class FrmModelSelected : Form
    {
        public ElementInfo SelectedElement;

        public FrmModelSelected(List<ElementInfo> list)
        {
            InitializeComponent();
            dgvSelected.AutoGenerateColumns = false;
            dgvSelected.DataSource = list;
        }

        private void dgvSelected_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ElementInfo element = dgvSelected.Rows[e.RowIndex].DataBoundItem as ElementInfo;
                FrmModal ower = this.Owner as FrmModal;
                if (ower != null)
                {
                    ower.SetFocusControlRect(element.Rect);
                }
            }
        }

        private void dgvSelected_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ElementInfo element = dgvSelected.Rows[e.RowIndex].DataBoundItem as ElementInfo;
                FrmModal ower = this.Owner as FrmModal;
                if (ower != null)
                {
                    ower.SetFocusControlRect(element.Rect);
                    SelectedElement = element;
                    this.Close();
                }
            }
        }

        private void FrmModelSelected_Load(object sender, EventArgs e)
        {
        }
    }
}
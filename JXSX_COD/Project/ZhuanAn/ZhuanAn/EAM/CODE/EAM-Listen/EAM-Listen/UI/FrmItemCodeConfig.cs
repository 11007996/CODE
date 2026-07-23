using EAM.Listen.Common.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmItemCodeConfig : Form
    {
        private Dictionary<string, string> ItemNameDict;
        private List<ItemCode> ItemCodes = new List<ItemCode>();
        private List<string> CaseDataList = new List<string>();//示例数据

        public FrmItemCodeConfig(List<ItemCode> itemCodes, Dictionary<string, string> itemNameDict)
        {
            ItemCodes = itemCodes;
            ItemNameDict = itemNameDict;
            InitializeComponent();
        }

        private void FrmItemCodeConfig_Load(object sender, EventArgs e)
        {
            dgv_ItemCode.AutoGenerateColumns = false;
            // 将字典转换为 List<KeyValuePair<string, string>>
            var items = ItemNameDict.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value)).ToList();
            dgc_ItemName.ValueMember = "Value";
            dgc_ItemName.DisplayMember = "Key";
            dgc_ItemName.DataSource = items;

            // 数据绑定
            BindingSource binding = new BindingSource();
            binding.DataSource = ItemCodes;
            dgv_ItemCode.DataSource = binding;

            CreateCaseData();
        }

        /// <summary>
        /// 生成示例数据
        /// </summary>
        private void CreateCaseData()
        {
            tlp_CaseData.SuspendLayout();
            tlp_CaseData.Controls.Clear();
            CaseDataList.Clear();
            if (ItemCodes.Count > 0)
            {
                tlp_CaseData.RowCount = 2;
                tlp_CaseData.ColumnCount = ItemCodes.Count;
                string itemName;
                string itemCode;
                byte[] data;
                for (int i = 0; i < ItemCodes.Count; i++)
                {
                    var item = ItemCodes[i];
                    itemName = ItemNameDict.AsEnumerable().Where(it => it.Value == item.ItemName).FirstOrDefault().Key;
                    if (!string.IsNullOrEmpty(item.FixedCode))
                    {
                        itemCode = item.FixedCode.Replace("-", " ");
                    }
                    else if (item.ByteLen > 0)
                    {
                        data = new byte[item.ByteLen];
                        data[item.ByteLen - 1] = 1;
                        itemCode = BitConverter.ToString(data).Replace("-", " ");
                    }
                    else
                    {
                        itemCode = null;
                    }
                    CaseDataList.Add(itemCode);

                    // 标题(第一行)
                    Label labItemName = new Label();
                    labItemName.Anchor = System.Windows.Forms.AnchorStyles.None;
                    labItemName.AutoSize = true;
                    labItemName.Location = new System.Drawing.Point(20, 10);
                    labItemName.Size = new System.Drawing.Size(37, 15);
                    labItemName.Text = itemName;
                    tlp_CaseData.Controls.Add(labItemName, i, 0);
                    // 数值(第二行)
                    Label labItemCode = new Label();
                    labItemCode.Anchor = System.Windows.Forms.AnchorStyles.None;
                    labItemCode.AutoSize = true;
                    labItemCode.ForeColor = System.Drawing.Color.Red;
                    labItemCode.Location = new System.Drawing.Point(15, 43);
                    labItemCode.Size = new System.Drawing.Size(47, 15);
                    labItemCode.Text = itemCode;
                    tlp_CaseData.Controls.Add(labItemCode, i, 1);
                }
                tlp_CaseData.ResumeLayout();
            }
        }

        //复制
        private void btnCopy_Click(object sender, EventArgs e)
        {
            string text = string.Join(" ", CaseDataList);
            Clipboard.SetText(text);
        }
    }
}
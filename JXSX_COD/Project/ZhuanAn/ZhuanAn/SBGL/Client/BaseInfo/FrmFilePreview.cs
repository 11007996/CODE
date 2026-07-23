using ApiManager.AssetSystem;
using ApiManager.AssetSystem.Base;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class FrmFilePreview : Form
    {
        List<PreviewImageModel> PreviewImgs = new List<PreviewImageModel>();

        public FrmFilePreview()
        {
            InitializeComponent();
        }

        public FrmFilePreview(int sourceFileId)
        {
            InitializeComponent();
            GetPreviewList(sourceFileId);
        }


        private void GetPreviewList(int sourceFileId)
        {
            string sql = $"SELECT FileId,PageNo FROM S_PreviewFileInfo WHERE SourceFileId ='{sourceFileId}' AND PreviewType='{(int)FilePreviewTypeEnum.BLUR}'  Order BY PageNo ASC;";
            DataTable blurDT = DBUtil.GetDataTable(sql);
             sql = $"SELECT FileId,PageNo FROM S_PreviewFileInfo WHERE SourceFileId ='{sourceFileId}' AND PreviewType='{(int)FilePreviewTypeEnum.HIGH}'  Order BY PageNo ASC;";
            DataTable highDT = DBUtil.GetDataTable(sql);
            foreach (DataRow row in blurDT.Rows)
            {
                PreviewImageModel model=   new PreviewImageModel() { 
                    BlurPreviewFileId = Convert.ToInt32(row["FileId"])
                };
                DataRow highRow = highDT.Select("PageNo='" + row["PageNo"].ToString() + "'").FirstOrDefault();
                if (highRow != null)
                {
                    model.HighPreviewFileId = Convert.ToInt32(highRow["FileId"]);
                }
                PreviewImgs.Add(model);
            }
        }

        #region 窗口生命周期
        private void FrmFilePreview_Load(object sender, EventArgs e)
        {
            tabPanelImage.Controls.Clear();
            pbCurrImg.MouseWheel += PictureBox_MouseWheel;
        }

        private void FrmFilePreview_Shown(object sender, EventArgs e)
        {
            //初始左侧缩略图
            List<PictureBox> pictureBoxes = new List<PictureBox>();
            foreach (PreviewImageModel model in PreviewImgs)
            {
                // 
                // pictureBox1
                // 
                PictureBox pictureBox = new PictureBox();
                pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
                pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pictureBox.Location = new System.Drawing.Point(22, 4);
                pictureBox.Size = new System.Drawing.Size(150, 120);
                pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                pictureBox.TabStop = true;
                pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
                pictureBox.Tag = model.BlurPreviewFileId;
                Image img = GetPreviewImage(model.BlurPreviewFileId);
                pictureBox.Image = img;
                model.BlurImage = img;
                pictureBoxes.Add(pictureBox);
            }
            tabPanelImage.Controls.AddRange(pictureBoxes.ToArray());

            if (pictureBoxes.Count > 0)
            {
                pictureBox_Click(pictureBoxes[0], null);
            }
        }
        #endregion

        #region 单击缩略图
        /// <summary>
        /// 左侧缩略图单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int fileId = Convert.ToInt32(pb.Tag);
            foreach (PreviewImageModel pim in PreviewImgs)
            {
                if (pim.BlurPreviewFileId == fileId)
                {
                    if (pim.HighImage == null)
                        pim.HighImage = GetPreviewImage(pim.HighPreviewFileId);
                    pbCurrImg.Size = panelRight.Size;
                    pbCurrImg.Location = new Point(0, 0);
                    pbCurrImg.Image = pim.HighImage;
                }
            }
        }
        #endregion

        #region 图片交互
        int newX;
        int newY;
        int mouseOldX;
        int mouseOldY;
        //鼠标滚动缩放
        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {

            if (pbCurrImg.ClientRectangle.Contains(e.Location))
            {
                ((HandledMouseEventArgs)e).Handled = true;
                pbCurrImg.Focus();
                float zoomChange = e.Delta > 0 ? 2f : 0.5f;
                float zoomFactor = 1.0f;
                zoomFactor *= zoomChange;
                // 计算缩放后图片的大小 
                int newWidth = (int)(pbCurrImg.Width * zoomFactor);
                int newHeight = (int)(pbCurrImg.Height * zoomFactor);
                if (e.Delta > 0)
                {
                    newX = (int)(pbCurrImg.Left - e.X);
                    newY = (int)(pbCurrImg.Top - e.Y);
                }
                else
                {
                    newX = (int)(pbCurrImg.Left + e.X / 2);
                    newY = (int)(pbCurrImg.Top + e.Y / 2);
                }
                pbCurrImg.Size = new Size(newWidth, newHeight);
                pbCurrImg.Location = new Point(newX, newY);
            }
        }



        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCurrImg_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOldX = e.X;
                mouseOldY = e.Y;
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCurrImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point endPoint = e.Location;
                int offsetX = endPoint.X - mouseOldX;
                int offsetY = endPoint.Y - mouseOldY;
                newX += offsetX;
                newY += offsetY;
                pbCurrImg.Location = new Point(newX, newY);
            }
        }
        #endregion

        private Image GetPreviewImage(int fileId)
        {
            Stream stream = AssetSystemApi.DownloadPreview(fileId);
            if (stream != null && stream.Length > 0)
            {
                return Image.FromStream(stream);
            }
            return null;
        }

        //private Image GetHighImage(int fileId)
        //{
        //    Stream stream = AssetSystemApi.DownloadHighPreview(fileId);
        //    if (stream != null && stream.Length > 0)
        //    {
        //        return Image.FromStream(stream);
        //    }
        //    return null;
        //}


        internal class PreviewImageModel
        {
            public int SourceFileId;
            public int BlurPreviewFileId { get; set; }
            public int HighPreviewFileId { get; set; }
            public Image BlurImage { get; set; }
            public Image HighImage { get; set; }
        }


    }
}

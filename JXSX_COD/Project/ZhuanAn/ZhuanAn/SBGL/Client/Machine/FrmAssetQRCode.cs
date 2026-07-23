using Common.Util;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmAssetQRCode : Form
    {
        public FrmAssetQRCode()
        {
            InitializeComponent();
            dgvAsset.AutoGenerateColumns = false;
        }

        private void FrmAssetQRCode_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            cmbUrl.SelectedIndex = 1;
            RefreshDataGrid();
        }

        #region 搜索
        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        #endregion

        #region 批量生成
        //批量生成
        private void btnBatchSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //文件夹选择
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "请选择文件保存位置";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string url = cmbUrl.Text;
                    string dir = folderBrowser.SelectedPath + "\\";
                    foreach (DataGridViewRow row in dgvAsset.Rows)
                    {
                        DataGridViewCheckBoxCell checkBox = (DataGridViewCheckBoxCell)row.Cells["dgcCheck"];
                        if (checkBox.Value != null && (bool)checkBox.Value)
                        {
                            string assetNo = row.Cells["dgcAssetNo"].Value.ToString();
                            string assetName = row.Cells["dgcAssetName"].Value.ToString();
                            Image qrImg = CreateQRCode(url + assetNo);
                            Image thImg = ThumbnailImage(qrImg, 1200, 1200);
                            Image wdImg = InsertWords(thImg, assetNo, assetName);
                            string path = dir + assetNo + ".png";
                            if (File.Exists(path)) File.Delete(path);
                            wdImg.Save(path);
                            wdImg.Dispose();
                        }
                    }
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "保存成功";
                }
                catch (Exception ex)
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "保存失败：" + ex.Message;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region 下载
        private void btnSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //文件夹选择
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "请选择文件保存位置";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string dir = folderBrowser.SelectedPath + "\\";
                try
                {
                    string path = dir + txbAssetNo.Text + ".png";
                    if (File.Exists(path)) File.Delete(path);
                    pbQRCode.Image.Save(path);
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "保存成功";
                }
                catch (Exception ex)
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "保存失败：" + ex.Message;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion


        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        //打印
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // 获取要打印的图片
            Image imageToPrint = pbQRCode.Image;

            // 计算图片在纸张上的位置和大小
            Rectangle imgRect = new Rectangle(e.MarginBounds.Location, e.MarginBounds.Size);
            Rectangle srcRect = new Rectangle(Point.Empty, imageToPrint.Size);
            float aspect = (float)srcRect.Width / (float)srcRect.Height;
            if (aspect > (float)imgRect.Width / (float)imgRect.Height)
            {
                float newHeight = imgRect.Width / aspect;
                imgRect.Y += (int)((imgRect.Height - newHeight) / 2);
                imgRect.Height = (int)newHeight;
            }
            else
            {
                float newWidth = imgRect.Height * aspect;
                imgRect.X += (int)((imgRect.Width - newWidth) / 2);
                imgRect.Width = (int)newWidth;
            }

            // 在打印纸上绘制图片
            e.Graphics.DrawImage(imageToPrint, imgRect, srcRect, GraphicsUnit.Pixel);

            // 在底部添加文本字符串
            string textToPrint = txbAssetNo.Text;
            float fontSize = FrmAssetQRCode.CalculateFontSize(textToPrint, imgRect.Width);
            Font fontToUse = new Font("Arial", fontSize, FontStyle.Bold);
            Brush brushToUse = Brushes.Black;
            float textWidth = e.Graphics.MeasureString(textToPrint, fontToUse).Width;
            float x = e.MarginBounds.Left + ((imgRect.Width - textWidth) / 2);
            float y = imgRect.Y + imgRect.Height;
            e.Graphics.DrawString(textToPrint, fontToUse, brushToUse, x, y);
        }

        /// <summary>
        /// 计算合适的字体大小
        /// </summary>
        /// <param name="text"></param>
        /// <param name="charCount"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static float CalculateFontSize(string text, float width)
        {
            using (var bmp = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var avgCharWidth = g.MeasureString(text, new Font("Arial", 12)).Width / text.Length;
                    var charWidth = width / text.Length;
                    return Math.Max(6f, charWidth);
                }
            }
        }
        #endregion

        #region 图像处理
        //加载图片
        private void LoadImage()
        {
            string assetNo = txbAssetNo.Text;
            if (string.IsNullOrWhiteSpace(assetNo)) return;
            string url = cmbUrl.Text.Trim();
            string assetName = txbAssetName.Text;
            Image qr = CreateQRCode(url + assetNo);
            Image thQR = ThumbnailImage(qr, 1200, 1200);
            Image wordQR = InsertWords(thQR, assetNo, assetName);
            pbQRCode.Image = wordQR;
        }

        //创建二维码图片
        private Image CreateQRCode(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return null;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode(content);

            using (var ms = new System.IO.MemoryStream())
            {
                var render = new GraphicsRenderer(new FixedModuleSize(10, QuietZoneModules.Two));    //5倍大小
                render.WriteToStream(qrCode.Matrix, System.Drawing.Imaging.ImageFormat.Png, ms);
                byte[] bytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(bytes, 0, (int)ms.Length);
                return Image.FromStream(ms);
            }
        }

        //缩放图像
        public Image ThumbnailImage(Image image, int width = 200, int height = 200)
        {
            // 缩放图片到指定大小
            Image thumbnail = image.GetThumbnailImage(width, height, null, IntPtr.Zero);

            // 创建一个新的图像对象
            Image newImage = new Bitmap(width, height + 280);

            // 创建一个图像绘制对象
            Graphics graphics = Graphics.FromImage(newImage);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            // 绘制原始图像
            graphics.DrawImage(image, new Rectangle(0, 100, thumbnail.Width, thumbnail.Height));

            // 释放对象
            image.Dispose();
            thumbnail.Dispose();
            graphics.Dispose();

            // 返回新的图像对象
            return newImage;

        }

        //添加文字
        public Image InsertWords(Image image, string assetNo, string assetName)
        {

            // 定义文本字符串、文本字体和画刷
            Font font1 = new Font("微软雅黑", 20f, FontStyle.Bold, GraphicsUnit.Millimeter);
            Font font2 = new Font("宋体", 20f, FontStyle.Regular, GraphicsUnit.Millimeter);
            Font font3 = new Font("宋体", 20f, FontStyle.Regular, GraphicsUnit.Millimeter);
            Brush brush = Brushes.Black;

            // 添加文本并获取新的图像对象
            AddTextToImage(image, "设备点检二维码", font1, brush, new PointF(325, 0));
            AddTextToImage(image, "名称:" + assetName, font2, brush, new PointF(10, image.Height - 170));
            AddTextToImage(image, "编号:" + assetNo, font3, brush, new PointF(10, image.Height - 80));
            return image;
        }

        public static void AddTextToImage(Image image, string text, Font font, Brush brush, PointF point)
        {
            // 创建一个图像绘制对象
            Graphics graphics = Graphics.FromImage(image);
            // 设置绘图对象属性
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // 绘制文本字符串
            graphics.DrawString(text, font, brush, point);
            // 释放对象
            graphics.Dispose();
        }
        #endregion

        #region 其他
        //刷新数据表格
        private void RefreshDataGrid()
        {
            string keywords = txbKeyWords.Text.Trim();
            string sql = "SELECT AssetNo,AssetName FROM A_AssetInfo_T WHERE 1=1";
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += string.Format(" AND AssetNo like '%{0}%' OR AssetName like '%{0}%'", keywords);
            }
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvAsset.DataSource = dt;
        }

        //数据表格选中行事件
        private void dgvAsset_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAsset.SelectedRows.Count > 0 && dgvAsset.SelectedRows[0].Cells["dgcAssetNo"].Value != null)
            {
                string assetNo = dgvAsset.SelectedRows[0].Cells["dgcAssetNo"].Value.ToString();
                string assetName = dgvAsset.SelectedRows[0].Cells["dgcAssetName"].Value.ToString();
                if (!string.IsNullOrWhiteSpace(assetNo))
                {
                    txbAssetNo.Text = assetNo;
                    txbAssetName.Text = assetName;
                    LoadImage();
                }
            }
        }

        //路径URL下拉选事件
        private void cmbUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            foreach (DataGridViewRow row in dgvAsset.Rows)
            {
                row.Cells["dgcCheck"].Value = chb.Checked;
            }
        }




        /// <summary>
        /// 二维码下面加上文字
        /// </summary>
        /// <param name="qrImg">QR图片</param>
        /// <param name="content">文字内容</param>
        /// <param name="n"></param>
        /// <returns></returns>
        //public Bitmap InsertWords(Bitmap qrImg, string content = "")
        //{
        //    Bitmap backgroudImg = new Bitmap(qrImg.Width, qrImg.Height + 25);

        //    backgroudImg.MakeTransparent();
        //    Graphics g2 = Graphics.FromImage(backgroudImg);
        //    //设置图像质量
        //    g2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //    g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //    g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    g2.Clear(Color.Transparent);
        //    //画二维码到新的面板上
        //    g2.DrawImage(qrImg, 0, 0);

        //    if (!string.IsNullOrEmpty(content))
        //    {
        //        FontFamily fontFamily = new FontFamily("楷体");
        //        Font font1 = new Font(fontFamily, 13f, FontStyle.Regular, GraphicsUnit.Pixel);

        //        //文字长度 
        //        int strWidth = (int)g2.MeasureString(content, font1).Width;
        //        //总长度减去文字长度的一半  （居中显示）
        //        int wordStartX = (qrImg.Width - strWidth) / 2;
        //        int wordStartY = qrImg.Height + 10;
        //        g2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        //        g2.DrawString(content, font1, Brushes.Black, wordStartX, wordStartY);
        //    }

        //    g2.Dispose();
        //    return backgroudImg;
        //}
        #endregion





    }
}

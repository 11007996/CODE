using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //填充渐变矩形
            //Graphics g = e.Graphics;    // 创建当前窗体的Graphics对象
            // Rectangle rect = new Rectangle(50, 30, 100, 100);   // 创建一个矩形(x,y,width,height)
            // // 创建线性渐变画刷(画刷界限, 起始颜色, 结束颜色, 渐变角度)
            //LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.LightGreen, Color.LightBlue, LinearGradientMode.ForwardDiagonal);
            // g.FillRectangle(lBrush, rect);  


            ////画弧
            // Graphics graphics = e.Graphics;
            // Pen pen = new Pen(Color.Blue);
            // Rectangle rect = new Rectangle(80,80,200,200);
            // graphics.DrawArc(pen, rect, 0, 360);


            ////画直线
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            //Pen pen = new Pen(Color.Blue);  // 创建蓝色画笔对象
            //Point pointStart = new Point(30, 30);   // 创建起始点
            //Point pointEnd = new Point(150, 150);   // 创建结束点
            //graphics.DrawLine(pen, pointStart, pointEnd);   // 画线


            ////画椭圆
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // Pen pen = new Pen(Color.Blue, 100);  // 创建蓝色 粗细为100的画笔对象
            // Rectangle rect = new Rectangle(100, 100, 200, 100);   // 创建椭圆所在的矩形范围
            // graphics.DrawEllipse(pen, rect);    // 在指定的范围内画椭圆


            ////文本
            //Font font = new Font("微软雅黑", 40); // 创建Font字体对象
            // Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // graphics.DrawString("孤影'Blog 欢迎您！", font, new SolidBrush(Color.Black), 20, 60);


            ////填充路径
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // graphics.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);  // 以白色画刷填充当前窗体
            // // 创建线组
            // GraphicsPath path = new GraphicsPath(new Point[] {
            //     new Point(40,140),
            //     new Point(275,200),
            //     new Point(105,225),
            //    new Point(190,300),
            //     new Point(50,350),
            //     new Point(20,180)
            // }, new byte[] { 
            //     (byte)PathPointType.Start,
            //     (byte)PathPointType.Bezier,
            //     (byte)PathPointType.Bezier,
            //     (byte)PathPointType.Bezier,
            //     (byte)PathPointType.Line,
            //     (byte)PathPointType.Line
            // });
            // // 路径笔刷
            // PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
            // // 设置路径中的点对应的颜色数组
            // pathGradientBrush.SurroundColors = new Color[] { Color.Green, Color.Yellow, Color.Red, Color.Blue, Color.Orange, Color.White };
            // graphics.FillPath(pathGradientBrush, path); // 填充路径


            ////SolidBrush实体画刷
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // SolidBrush solidBrushG = new SolidBrush(Color.Green);   // 绿色画刷
            // SolidBrush solidBrushR = new SolidBrush(Color.Red); // 红色画刷
            // SolidBrush solidBrushB = new SolidBrush(Color.Blue);    // 蓝色画刷
 
            // graphics.FillEllipse(solidBrushG, 20, 40, 60, 70);  // 用绿色画刷填充一个椭圆
 
            // Rectangle rect = new Rectangle(0, 0, 200, 100);     // 矩形
            // graphics.FillPie(solidBrushB, 0, 0, 200, 40, 0.0f, 30.0f);  // 填充饼图
 
            // // 组成多边形的点
            // PointF point1 = new PointF(50.0f, 250.0f);
            // PointF point2 = new PointF(100.0f, 25.0f);
            // PointF point3 = new PointF(150.0f, 40.0f);
            // PointF point4 = new PointF(200.0f, 50.0f);
            // PointF point5 = new PointF(250.0f, 100.0f);
            // PointF[] curvePoints = { point1, point2, point3, point4, point5 };
            // graphics.FillPolygon(solidBrushR, curvePoints);  // 填充多边形


            ////HatchBrush填充画刷
            //Graphics graphics = e.Graphics; //创建当前窗体的Graphics对象
            // //创建用于画三种不同样式图形的阴影画笔
            // HatchBrush hatchBrushR = new HatchBrush(HatchStyle.DiagonalCross, Color.Chocolate, Color.Red);
            // HatchBrush hatchBrushG = new HatchBrush(HatchStyle.DashedHorizontal, Color.Green, Color.Black);
            // HatchBrush hatchBrushB = new HatchBrush(HatchStyle.Weave, Color.BlueViolet, Color.Blue);
            // graphics.FillEllipse(hatchBrushR, 20, 80, 60, 20);  // 填充椭圆
            // // 填充饼图
            // Rectangle rect = new Rectangle(0, 0, 200, 100); 
            // graphics.FillPie(hatchBrushB, 0, 0, 200, 40, 0.0f, 30.0f);
            // // 填充自定义图形
            // PointF point1 = new PointF(50.0f, 250.0f);
            // PointF point2 = new PointF(100.0f, 25.0f);
            // PointF point3 = new PointF(150.0f, 40.0f);
            // PointF point4 = new PointF(250.0f, 50.0f);
            // PointF point5 = new PointF(300.0f, 100.0f);
            // PointF[] curvePoints = { point1, point2, point3, point4, point5 };
            // graphics.FillPolygon(hatchBrushG, curvePoints);


            ////纹理画刷
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // Bitmap bitmap = new Bitmap("‪C.bmp"); // 根据文件创建原始大小的Bitmap对象
            // bitmap = new Bitmap(bitmap, this.ClientRectangle.Size);  // 缩放到窗体大小
            // TextureBrush textureBrush = new TextureBrush(bitmap);
            // graphics.FillEllipse(textureBrush, this.ClientRectangle);



            ////线性渐变
            //Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
            // LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.Blue, LinearGradientMode.Vertical);
            // graphics.FillRectangle(linearGradientBrush, this.ClientRectangle);



            //路径渐变
            Graphics graphics = e.Graphics; // 创建当前窗体的Graphics对象
             Point centerPoint = new Point(150, 100);
             int R = 60;
             GraphicsPath path = new GraphicsPath();
             path.AddEllipse(centerPoint.X - R, centerPoint.Y - R, R * 2, R * 2);
             PathGradientBrush brush = new PathGradientBrush(path);
             brush.CenterPoint = centerPoint;    // 指定路径中心点
             brush.CenterColor = Color.Red;  // 指定路径中心的颜色
             brush.SurroundColors = new Color[] { Color.Plum };
             graphics.FillEllipse(brush, centerPoint.X - R, centerPoint.Y - R, R * 2, R * 2);
             centerPoint = new Point(350, 100);
             R = 20;
             path = new GraphicsPath();
             path.AddEllipse(centerPoint.X - R, centerPoint.Y - R, R * 2, R * 2);
             path.AddEllipse(centerPoint.X - R * 2, centerPoint.Y - R * 2, R * 4, R * 4);
             path.AddEllipse(centerPoint.X - R * 3, centerPoint.Y - R * 3, R * 6, R * 6);
             brush = new PathGradientBrush(path);
             brush.CenterPoint = centerPoint;
             brush.CenterColor = Color.Red;
             brush.SurroundColors = new Color[] { Color.Black, Color.Blue, Color.Green };
             graphics.FillPath(brush, path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControl
{
    //自定义组件，创建组件类，继承自Control
    [DefaultEvent("LValueChanged")]
    public class LTrackBar:Control
    {
        public LTrackBar()
        {
            //双缓存，避免控制重绘时闪烁
            SetStyle(ControlStyles.AllPaintingInWmPaint,true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer,true);
            CreateControl();
        }



        private Color _BarColor = Color.FromArgb(128, 255, 128);

        //创建属性
        [Category("DemoUI"),Description("背景颜色")]
        [ParenthesizePropertyName(true)]   //属性窗口中带括号，表示特殊属性，只是视觉效果，并不影响逻辑
        public Color L_BarColor
        {
            get { return _BarColor; }
            set{
                _BarColor = value;
                Invalidate();   //重绘
            }
        }

        
        private Color _SliderColor = Color.FromArgb(0, 192, 0);
        [Category("DemoUI"), Description("滑块颜色")]
        public Color L_SliderColor
        {
            get { return _SliderColor; }
            set
            {
                _SliderColor = value;
                Invalidate();
            }
        }
        
        private bool _IsRound = true;
        [Category("DemoUI"), Description("圆角")]
        public bool L_IsRound
        {
            set { 
                _IsRound = value;
                Invalidate();
            }
            get { return _IsRound; }
        }

        
        private int _Minimum = 0;
        [Category("DemoUI"), Description("最小值")]
        public int L_Mininum
        {
            get { return _Minimum; }
            set
            {
                _Minimum = value;
                if (_Minimum >= _Maximum) _Minimum = _Minimum - 1;
                if (_Minimum < 0) _Minimum = 0;
                Invalidate();
            }
        }

        
        private int _Maximum = 100;
        [Category("DemoUI"), Description("最大值")]
        public int L_Maxmun
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                if (_Maximum <= _Minimum) _Minimum = _Minimum + 1;
                Invalidate();
            }
        }


        
        private int _Value = 0;
        [Category("DemoUI"), Description("当前值")]
        public int L_Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (_Value < _Minimum) _Value = _Minimum;
                if (_Value > _Maximum) _Value = _Maximum;
                if (LValueChanged != null)
                {
                    LValueChanged.Invoke(this, new LEventArgs(_Value));
                }
                Invalidate();
            }
        }

        

        Size Size;

        
        private int _BarSize = 15;
        [Category("DemoUI"), Description("外观大小")]
        public int L_BarSize
        {
            get { return _BarSize; }
            set
            {
                _BarSize = value;
                if (_BarSize < 1) _BarSize = 1;
                if(_Orientation==Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
                {
                    Size = new Size(Width, _BarSize);
                }
                else
                {
                    Size = new Size(_BarSize, Height);
                }
            }
        }

        private Orientation _Orientation = Orientation.Horizontal_LR;
        public Orientation L_Orientation
        {
            get { return _Orientation; }
            set
            {
                Orientation old = _Orientation;
                _Orientation = value;
                if ((old == Orientation.Horizontal_LR || old == Orientation.Horizontal_RL) && (_Orientation == Orientation.Vertical_BT || _Orientation == Orientation.Vertical_TB))
                {
                    Size = new Size(Size.Height, Size.Width);
                }
                if ((_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL) && (old == Orientation.Vertical_BT || old == Orientation.Vertical_TB))
                {
                    Size = new Size(Size.Height, Size.Width);
                }
                Invalidate();
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int iHeight = _BarSize;
            if(_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                base.SetBoundsCore(x, y, width, iHeight, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, iHeight, height, specified);
            }
        }

        MouseStatus mouseStatus;
        PointF mousePoint;

        public delegate void LValueChangeEventHandler(object sender, LEventArgs e);
        //public EventHandler<LEventArgs> LValueChanged;
        public event LValueChangeEventHandler LValueChanged;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseStatus = MouseStatus.Down;
            mousePoint = e.Location;
            pPointToValue();
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(mouseStatus==MouseStatus.Down)
            {
                mousePoint = e.Location;
                pPointToValue();
                Invalidate();   //重绘控件
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseStatus = MouseStatus.Up;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseStatus = MouseStatus.Enter;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseStatus = MouseStatus.Leave;
        }
        
        private void pPointToValue()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if(_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
            }
            if(_Orientation == Orientation.Horizontal_LR)
            {
                float fRatio = Convert.ToSingle(mousePoint.X - fCapHalfWidth) / (Width -fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum-_Minimum) + _Minimum);
            }
            else if(_Orientation == Orientation.Horizontal_RL)
            {
                float fRatio = Convert.ToSingle(Width - mousePoint.X - fCapHalfWidth) / (Width - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum-_Minimum) +_Minimum);
            }
            else if(_Orientation == Orientation.Vertical_TB)
            {
                float fRatio = Convert.ToSingle(mousePoint.Y - fCapHalfWidth) / (Height - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum - _Minimum) + _Minimum);
            }
            else
            {
                float fRatio = Convert.ToSingle(Height - mousePoint.X-fCapHalfWidth) / (Height-fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum-_Minimum) + _Minimum);
            }
            if(_Value < _Minimum) _Value = _Minimum;
            if(_Value > _Maximum) _Value = _Maximum;
            //LValueChanged?.Invoke(this, new LEventArgs(_Value));
        }

        //Invalidate之后执行此方法
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            pValueToPoint();
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        
            Pen penBarBack = new Pen(_BarColor, _BarSize);
            Pen penBarFore = new Pen(_SliderColor, _BarSize);
        
            float fCapHalfWidth = 0;
            float fCapWidth =  0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
                penBarBack.StartCap = LineCap.Round;
                penBarBack.EndCap = LineCap.Round;
        
                penBarFore.StartCap = LineCap.Round;
                penBarFore.EndCap = LineCap.Round;
            }
        
            float fPointValue = 0;
            if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarBack, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth, Height / 2f);
                fPointValue = mousePoint.X;
                if (fPointValue < fCapHalfWidth) fPointValue = fCapHalfWidth;
                if (fPointValue > Width - fCapHalfWidth) fPointValue = Width - fCapHalfWidth;
            }
            else
            {
                e.Graphics.DrawLine(penBarBack, Width / 2f, fCapHalfWidth, Width / 2f, Height - fCapHalfWidth);
        
                fPointValue = mousePoint.Y;
                if (fPointValue < fCapHalfWidth) fPointValue = fCapHalfWidth;
                if (fPointValue > Height - fCapHalfWidth) fPointValue = Height - fCapHalfWidth;
            }
        
        
            if (_Orientation == Orientation.Horizontal_LR)
            {
                e.Graphics.DrawLine(penBarFore, fCapHalfWidth, Height / 2f, fPointValue, Height / 2f);
            }
            else if (_Orientation == Orientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarFore,  fPointValue, Height / 2f, Width - fCapHalfWidth, Height / 2f);
            }
            else if (_Orientation == Orientation.Vertical_TB)
            {
                e.Graphics.DrawLine(penBarFore, Width / 2f, fCapHalfWidth, Width / 2f, fPointValue);
            }
            else
            {
                e.Graphics.DrawLine(penBarFore, Width / 2f,  fPointValue, Width / 2f, Height - fCapHalfWidth);
            }
        }


        private void pValueToPoint()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
            }

            float fRatio = Convert.ToSingle(_Value - _Minimum) / (_Maximum - _Minimum);
            if (_Orientation == Orientation.Horizontal_LR)
            {
                float fPointValue = fRatio * (Width - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(fPointValue, fCapHalfWidth);
            }
            else if (_Orientation == Orientation.Horizontal_RL)
            {
                float fPointValue = Width - fCapHalfWidth - fRatio * (Width - fCapWidth);
                mousePoint = new PointF(fPointValue, fCapHalfWidth);
            }
            else if (_Orientation == Orientation.Vertical_TB)
            {
                float fPointValue = fRatio * (Height - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(fCapHalfWidth, fPointValue);
            }
            else
            {
                float fPointValue = Height - fCapHalfWidth - fRatio * (Height - fCapWidth);
                mousePoint = new PointF(fCapHalfWidth, fPointValue);
            }
        
        }
    
    }
    
    public class LEventArgs : EventArgs
    {
        public object Value { set; get; }
        public LEventArgs(object value)
        {
            Value = value;
        }
    }

    public enum Orientation
    {
        Horizontal_LR,
        Horizontal_RL,
        Vertical_BT,
        Vertical_TB
    }

    public enum MouseStatus
    {
        Enter,
        Leave,
        Down,
        Up
    }


    class Size
    {
        public int Height { set; get; }
        public int Width { set; get; }
        public Size(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }


}

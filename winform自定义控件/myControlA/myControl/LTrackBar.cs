using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControl
{
    public class LTrackBar : Control
    {
        #region 属性
        private Color _BarColor = Color.FromArgb(128, 255, 128);
        //背景条颜色
        public Color L_BarColor
        {
            get { return _BarColor; }
            set { 
                _BarColor = value;
                Invalidate();
            }
        }

        private Color _SliderColor = Color.FromArgb(0,192,0);
        //滑块颜色
        public Color L_SliderColor
        {
            get { return _SliderColor; }
            set
            {
                _SliderColor = value;
                Invalidate();
            }
        }

        //是否显示圆角
        private bool _IsRound = true;
        public bool L_IsRound
        {
            get { return _IsRound; }
            set
            {
                _IsRound = value;
                Invalidate();
            }
        }

        //滑块最小值
        private int _Mininum = 0;
        public int L_Mininum
        {
            get { return _Mininum; }
            set { 
                _Mininum = value;
                Invalidate();
            }
        }

        //滑块最大值
        private int _Maximun = 100;
        public int L__Maximun
        {
            get { return _Maximun; }
            set
            {
                _Maximun = value;
                if(_Maximun<=_Mininum)
                {
                    _Maximun = _Mininum + 1;
                }
            }
        }


        private int _Value = 0;
        public int L_Value
        {
            get { return _Value; }
            set
            {
                _Value=value;
                if(_Value<_Mininum) _Value=_Mininum;
                if(_Value>_Maximun) _Value=_Maximun;
                Invalidate();
                LValueChanged.Invoke(this,new LEventArgs(_Value));
            }
        }
        #endregion

        LTrackBar()
        {
            LValueChanged += showdata;
        }
        /*
        public delegate void LValueChangedDel(object obj, LEventArgs a);
        public event LValueChangedDel LValueChanged;
        */


        public EventHandler<LEventArgs> LValueChanged;
        private void showdata(object obj, LEventArgs e)
        {
            _Value = e._Value;
        }
        

    }
        
        class LEventArgs:EventArgs
        {
            public int _Value { set; get; }
            public LEventArgs(int Value)
            {
                _Value = Value;
            }
        }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DryIocEvent.Views
{
    /// <summary>
    /// TCwindow.xaml 的交互逻辑
    /// </summary>
    public partial class TCwindow : Window,IDialogWindow  //使用窗口替换用户控件弹窗，必须实现此接口
    {
        public TCwindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}

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
using WpfNoDataContent.Models;

namespace WpfNoDataContent.Views
{
    /// <summary>
    /// StartPage.xaml 的交互逻辑
    /// </summary>
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
        }

        //手动实现消息总线
        /*
        public StartPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();


            //获取消息总线对象,通过构造注入的方式获取
            //通过消息对象进行订阅或发布
            //总线  事件对象
            eventAggregator
                .GetEvent<EventMessage>()
                .Subscribe(Receive);
        }
        private void Receive()
        {

        }
        */
    }
}

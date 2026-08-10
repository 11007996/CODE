using DryIocEvent.Models;
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
    /// MainWin.xaml 的交互逻辑
    /// </summary>
    public partial class MainWin : Window
    {
        IEventAggregator eventAggregator;
        public MainWin(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            //订阅ViewModel发布的事件消息
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<CommonEvent>().Subscribe(show);

            //手动绑定事件
            this.eventButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.ButtonCliked));
        }
        private void ButtonCliked(object obj,RoutedEventArgs args)
        {
            MessageBox.Show("触发事件");
        }

        private void eventButton1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("触发事件1");
        }

        private void show(EventParamer ep)
        {
            MessageBox.Show("事件参数：" + ep.message);
            //在适当时机取消订阅，全局事件总线，否则容易造成内存泄漏
            eventAggregator.GetEvent<CommonEvent>().Unsubscribe(show);
        }
    }
}

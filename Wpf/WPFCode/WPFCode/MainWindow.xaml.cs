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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCode
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        users user = new users();
        Int16 aa = 23;
        public MainWindow()
        {
            InitializeComponent();
            
            this.txtt.DataContext = user;
        }

        private void ModifyText(object sender, RoutedEventArgs e)
        {
            //user.Name = "hello";
            //this.txtt.Text = "hello";

            this.txtt.Text = this.FindResource("imgPath") as string;   //资源字典
        }

        private void readText(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(user.Name);
        }

        private void winClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

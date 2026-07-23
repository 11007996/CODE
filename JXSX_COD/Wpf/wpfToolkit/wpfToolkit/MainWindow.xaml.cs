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
using wpfToolkit.VM;

namespace wpfToolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bindViewModel d = new bindViewModel();
            this.btnText.DataContext = d;
            this.txtBox.DataContext = d;
            this.tB.DataContext = d;
            this.showText2.DataContext = d;
        }

        private void showText_Click(object sender, RoutedEventArgs e)
        {
            bindViewModel d = this.txtBox.DataContext as bindViewModel;
            MessageBox.Show(d.Name);
        }
    }
}

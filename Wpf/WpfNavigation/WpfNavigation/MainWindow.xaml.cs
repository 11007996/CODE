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
using WpfNavigation.Models;

namespace WpfNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pageClass pageClass = new pageClass();
            //this.DataContext = pageClass;
            this.pageControl.DataContext = pageClass;
            this.radioA.DataContext = pageClass;
            this.radioB.DataContext = pageClass;
            this.radioC.DataContext = pageClass;
        }

    }
}

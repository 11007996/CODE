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

namespace DryIocNavigation.ViewModels
{
    /// <summary>
    /// MainWin.xaml 的交互逻辑
    /// </summary>
    public partial class MainWin : Window
    {
        public MainWin(IRegionManager regionManager)
        {
            InitializeComponent();

            //区域加载，也可以放到viewmodel中再加载
            regionManager.RegisterViewWithRegion("pageNavigate1", "NpageA");
            regionManager.RegisterViewWithRegion("pageNavigate1", "NpageB");
            /*
            this.Loaded += (se, ev) =>
            {
                var region = regionManager.Regions["pageNavigate1"];
                var view = region.Views.FirstOrDefault(v => v.GetType().Name == "NpageB");
                region.Activate(view);
            };
            */
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DryIocNavigation.ViewModels
{
    internal class MainWinViewModel:BindableBase
    {
        public DelegateCommand<object> BtnCommand { set; get; }
        public IRegionManager RegionManager { get; set; }
        public MainWinViewModel(IRegionManager regionManager) 
        {
            RegionManager= regionManager;
            BtnCommand = new DelegateCommand<object>(DbBtnCommand);
        }
        private void DbBtnCommand(object obj)
        {
            const string region = "pageNavigate";
            if (!RegionManager.Regions.ContainsRegionWithName(region)) return;   //防止页面重复加载
            var r = RegionManager.Regions[region];
            r.RemoveAll();

            NavigationParameters ns = new NavigationParameters();
            ns.Add("page1Text", "Navigation_Text");
            RegionManager.RequestNavigate(region, obj.ToString(), ns);
        }
    }
}

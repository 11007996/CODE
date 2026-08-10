using Prism.Navigation.Regions;
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
        public DelegateCommand regionPage { set; get; }
        
        public IRegionManager RegionManager { get; set; }
        public MainWinViewModel(IRegionManager regionManager) 
        {
            RegionManager= regionManager;
            BtnCommand = new DelegateCommand<object>(DbBtnCommand);
            regionPage = new DelegateCommand(regionPageShow);


            //预加载页面
            //RegionManager.RegisterViewWithRegion("pageNavigate1", "NpageA");
            //RegionManager.RegisterViewWithRegion("pageNavigate1", "NpageB");
        }
        private void regionPageShow()
        {
            //激活区域页面
            var region = RegionManager.Regions["pageNavigate1"];
            var view = region.Views.FirstOrDefault(v => v.GetType().Name == "NpageB");
            region.Activate(view);
        }
        private void DbBtnCommand(object obj)
        {
            const string region = "pageNavigate";
            if (!RegionManager.Regions.ContainsRegionWithName(region)) return;   //防止页面重复加载
            var r = RegionManager.Regions[region];
            r.RemoveAll();

            NavigationParameters ns = new NavigationParameters();
            ns.Add("page1Text", "Navigation_Text1");
            ns.Add("page2Text", "Navigation_Text2");
            RegionManager.RequestNavigate(region, obj.ToString(), ns);
        }
    }
}

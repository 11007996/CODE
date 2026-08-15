using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfLibrary;

namespace DryIocNavigation.ViewModels
{
    internal class MainWinViewModel:BindableBase
    {
        public DelegateCommand<object> BtnCommand { set; get; }
        public DelegateCommand regionPage { set; get; }
        public DelegateCommand goBack { set; get; }
        public DelegateCommand goAhead { set; get; }
        public DelegateCommand LoadModel {  set; get; }

        public CompositeCommand composite { set; get; }
        public DelegateCommand compositeA {  set; get; }
        public DelegateCommand compositeB {  set; get; }

        public IRegionManager RegionManager { get; set; }
        private IRegionNavigationJournal Journal;   //导航日志，上一页下一页
        


        public MainWinViewModel(IRegionManager regionManager, IRegionNavigationService rns) 
        {
            //引用并使用类库
            /*
            Class1 saf = new Class1();
            saf.Idd = 2;
            */

            RegionManager = regionManager;
            BtnCommand = new DelegateCommand<object>(DbBtnCommand);
            regionPage = new DelegateCommand(regionPageShow);
            goBack = new DelegateCommand(goBackFun);
            goAhead = new DelegateCommand(goAheadFun);
            LoadModel = new DelegateCommand(showMode);

            //复合命令，触发多个命令
            compositeA = new DelegateCommand(compositeAFun);
            compositeB = new DelegateCommand(compositeBFun);
            composite = new CompositeCommand();
            composite.RegisterCommand(compositeA);
            composite.RegisterCommand(compositeB);

            //预加载页面
            //RegionManager.RegisterViewWithRegion("pageNavigate1", "NpageA");
            //RegionManager.RegisterViewWithRegion("pageNavigate1", "NpageB");
        }

        private void showMode()
        {
            RegionManager.RequestNavigate("pageNavigate", "dllUser");
        }


        private void compositeAFun()
        {
            MessageBox.Show("compositeAFun");
        }
        private void compositeBFun()
        {
            MessageBox.Show("compositeBFun");
        }

        //向后 上一页
        private void goBackFun()
        {
            journal.GoBack();
        }
        //向前 下一页
        private void goAheadFun()
        {
            journal.GoForward();
        }
        private void regionPageShow()
        {
            //激活区域页面
            var region = RegionManager.Regions["pageNavigate1"];
            var view = region.Views.FirstOrDefault(v => v.GetType().Name == "NpageB");
            region.Activate(view);
        }

        private IRegionNavigationJournal journal;
        private void DbBtnCommand(object obj)
        {
            const string region = "pageNavigate";
            if (!RegionManager.Regions.ContainsRegionWithName(region)) return;   //防止页面重复加载

            NavigationParameters ns = new NavigationParameters();
            ns.Add("page1Text", "Navigation_Text1");
            ns.Add("page2Text", "Navigation_Text2");
            //跳转页面
            //RegionManager.RequestNavigate(region, obj.ToString(), ns);

            RegionManager.RequestNavigate(region, obj.ToString(), callback, ns);
        }

        //跳转日志回调函数
        private void callback(NavigationResult result)
        {
            journal = result.Context.NavigationService.Journal;  //导航日志
        }
    }
}

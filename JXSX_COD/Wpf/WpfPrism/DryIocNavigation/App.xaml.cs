using DryIocNavigation.ViewModels;
using DryIocNavigation.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DryIocNavigation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //设置启动页面
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWin>();
        }

        //注册导航页
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NpageA>();
            containerRegistry.RegisterForNavigation<NpageB>();
        }

        //自动关联 View与 ViewModel 的工具，这个方法就是统一配置 ViewModel 自动匹配规则的入口。
        //PrismApplication默认实现，此处不写也行
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //手动绑定View和ViewModel   参数1：你的UserControl；参数2：对应ViewModel
            //ViewModelLocationProvider.Register<DryIocNavigation.Views.NpageA, ViewModels.NpageAViewModel>();
        }
    }

}

using System.Configuration;
using System.Data;
using System.Windows;
using PrismNavigation.Services;
using PrismNavigation.ViewModels;
using PrismNavigation.Views;

namespace PrismNavigation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<fWin>();   //fWin必须在Views文件夹中
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<Services.ICustomerStore, Services.DbCustomerStore>();
            containerRegistry.RegisterForNavigation<Npage1>();    //以用户控件导入主界面，且必须在Views文件夹中
            containerRegistry.RegisterForNavigation<Npage2>();
        }
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }
    }

}

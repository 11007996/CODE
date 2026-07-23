using System.Data;
using System.Windows;
using System.Xaml;
using WpfNoDataContent.PrismInnitialize;
using WpfNoDataContent.Services;
using WpfNoDataContent.ViewModels;
using WpfNoDataContent.Views;

namespace WpfNoDataContent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App() 
        {
            //AppStartA作为启动入口
            new AppStartA().Run();
        }


        
        protected override Window? CreateShell()
        {
            return null;
            //APP作为启动入口
            //return Container.Resolve<StartPage>();

            ////return new StartPage();  //不推荐

            //声明一个带消息总线的主窗口   不推荐
            //var ea = Container.Resolve<IEventAggregator>();
            //return new StartPage(ea);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register other needed services here
            //containerRegistry.Register<Services.ICustomerStore, Services.DbCustomerStore>();
        }
        
    }

}

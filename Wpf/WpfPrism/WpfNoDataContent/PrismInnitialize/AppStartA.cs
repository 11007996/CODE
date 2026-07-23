using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfNoDataContent.Views;

namespace WpfNoDataContent.PrismInnitialize
{
    internal class AppStartA :PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<StartPage>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}

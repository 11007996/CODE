using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DryIocNavigation.ViewModels
{
    class NpageCViewModel : BindableBase,INavigationAware
    {
        private string _pageCtext="C字符";
        public string PageCtext
        {
            get { return _pageCtext; }
            set { SetProperty(ref _pageCtext, value); }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //PageCtext=navigationContext.Parameters.GetValue<string>("key");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocNavigation.ViewModels
{
    internal class NpageBViewModel :BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private string _pagecText = "";
        public string PagecText
        {
            get { return _pagecText; }
            set { SetProperty(ref _pagecText, value); }
        }


        public bool KeepAlive => true;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PagecText=navigationContext.Parameters.GetValue<string>("key");
        }
    }
}

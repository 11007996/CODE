using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocNavigation.ViewModels
{
    class NpageAViewModel:BindableBase,INavigationAware
    {
        private string _page1Text="page1";
        public string Page1Text
        {
            get { return _page1Text; }
            set
            {
                SetProperty(ref _page1Text, value);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 决定是否重用当前视图
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // 导航离开时的逻辑
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取导航参数并更新数据
            Page1Text = navigationContext.Parameters.GetValue<string>("page1Text");
        }
    }
}

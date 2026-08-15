using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DryIocNavigation.ViewModels
{
    class NpageAViewModel:BindableBase,INavigationAware,IRegionMemberLifetime,IConfirmNavigationRequest
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

        private string _page2Text = "page2";
        public string Page2Text
        {
            get { return _page2Text; }
            set
            {
                SetProperty(ref _page2Text, value);
            }
        }

        //切换时是否销毁页面，true表示页面缓存、不销毁
        public bool KeepAlive => true;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // 决定是否重用当前视图,false表示新建一个页面
            //是否重用和是否缓存是两回事
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // 导航离开时的逻辑
            //若有事件订阅，在离开时要取消订阅，防止内存泄漏

            //跳转到其它页面时将此参数带过去
            navigationContext.Parameters.Add("key","value");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取导航参数并更新数据，导航到此页面时触发
            Page1Text = navigationContext.Parameters.GetValue<string>("page1Text");
            Page1Text = navigationContext.Parameters.GetValue<string>("page2Text");
        }

        //跳转离开此页面时触发，在OnNavigatedFrom之前触发，用于确认是否可能跳转
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            MessageBoxResult re = MessageBox.Show("打开？", "问题", MessageBoxButton.YesNo);
            if (re == MessageBoxResult.Yes) 
            { 
                continuationCallback?.Invoke(true);
            }
            else
            {
                continuationCallback?.Invoke(false);
            }
        }
    }
}

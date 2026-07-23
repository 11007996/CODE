using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigation.Models
{
    class pageClass: ObservableObject
    {
        public object UseView { set; get; }

        /*//全局通知
        private object useView;
        public object UseView
        {
            get { return useView; }
            set
            {
                if (useView == value) return;
                useView = value;
                //OnPropertyChanged();   //全局通知
            }
        }
        */
        public pageClass()
        {
            radioCommand = new RelayCommand<object>(pageTran);
        }
        public RelayCommand<object> radioCommand { set; get; }

        public void pageTran(object obj)
        {
            UseView = obj;
            this.OnPropertyChanged(nameof(UseView));  //命令通知
        }

    }
}

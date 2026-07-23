using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfNoDataContent.ViewModels
{
    class StartPageViewModel:BindableBase
    {
        private string _txtMessage = "张三";
        public string TxtMessage
        {
            get { return _txtMessage; }
            set
            {
                SetProperty(ref _txtMessage, value);
                //_txtMessage = value;
                //this.RaisePropertyChanged(nameof(TxtMessage));
            }
        }

        public ICommand BtnCommand { set; get; }
        public ICommand BtnSCommand { set; get; }
        public StartPageViewModel()
        {
            BtnCommand = new DelegateCommand(ChangeText);
            BtnSCommand = new DelegateCommand(ShowText);
        }

        private void ChangeText()
        {
            TxtMessage = "李四";
        }
        private void ShowText()
        {
            MessageBox.Show(TxtMessage);
        }
    }
}

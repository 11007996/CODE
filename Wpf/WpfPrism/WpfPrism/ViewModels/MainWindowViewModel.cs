using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfPrism.ViewModels
{
    class MainWindowViewModel:BindableBase
    {
        private string _txtMessage = "张三";
        public string TxtMessage
        {
            get { return _txtMessage; }
            set {SetProperty(ref _txtMessage, value);
                //_txtMessage = value;
                //this.RaisePropertyChanged(nameof(TxtMessage));
            }
        }

        public DelegateCommand BtnCommand { set; get; }
        public DelegateCommand BtnSCommand { set; get; }
        public MainWindowViewModel()
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

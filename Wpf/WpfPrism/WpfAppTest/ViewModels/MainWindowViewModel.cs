using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest.ViewModels
{
    internal class MainWindowViewModel:BindableBase
    {
        private string _testMsg = "StartWin文本";
        public string TextMsg
        {
            get { return _testMsg; }
            set { SetProperty(ref _testMsg, value); }
        }
    }
}

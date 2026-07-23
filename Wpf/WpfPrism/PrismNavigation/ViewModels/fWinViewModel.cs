using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismNavigation.ViewModels
{
    internal class fWinViewModel:BindableBase
    {
        private string _testText = "fWin文本";
        public string TestText
        {
            get { return _testText; }
            set { SetProperty(ref _testText, value); }
        }

        private IRegionManager _regionManager;
        public IRegionManager RegionManager
        {
            get { return _regionManager; }
            set { SetProperty(ref _regionManager, value); }
        }

        public DelegateCommand<object> BtnCommand { get; set; }
        public fWinViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            BtnCommand = new DelegateCommand<object>(DoBtnCommand);
        }

        private void DoBtnCommand(object obj)
        {
            _regionManager.RequestNavigate("mainContainer", obj.ToString());
        }
    }
}

using DryIocEvent.Models;
using DryIocEvent.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DryIocEvent.ViewModels
{
    class MainWinViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        IDialogService _dialogService;
        private string _textBox="";
        public string TextBox
        {
            get { return _textBox; }
            set { SetProperty(ref _textBox, value); }
        }
        public DelegateCommand<string> ButtonMouse { get; set; }
        public DelegateCommand ButtonEvent {  get; set; }
        //注入事件和弹窗
        public MainWinViewModel(IEventAggregator ea, IDialogService dialogService)
        {
            _eventAggregator = ea;
            _dialogService = dialogService;
            ButtonMouse = new DelegateCommand<string>(ButtonMouse_On);

            ButtonEvent = new DelegateCommand(showWin);
        }

        Random random = new Random();
        public void ButtonMouse_On(string message)
        {
            TextBox = message+"触发" + random.Next(1,10);
            
        }
        //命令绑定触发此方法
        private void showWin()
        {
            //在ViewMdoel中发布事件消息,在窗口xaml中订阅此消息
            _eventAggregator.GetEvent<CommonEvent>().Publish(new EventParamer { message="事件消息"});
            //触发弹窗
            _dialogService.ShowDialog("TCWin",null,dialogResut);
            //_dialogService.Show("TCWin");
        }
        //弹窗关闭后的回调数据
        private void dialogResut(IDialogResult dialogResult)
        {
            if ((bool)dialogResult.Parameters["result"])
            {
                MessageBox.Show("弹窗回调函数");
            }
        }
    }
}

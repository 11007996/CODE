using DryIocEvent.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DryIocEvent.ViewModels
{
    internal class TCWinViewModel :BindableBase, IDialogAware //弹窗需实现此接口
    {
        //设置弹窗栏标题
        public string Title => "用户控件弹窗";
        public DialogCloseListener RequestClose{ get;set;}
        
        public bool CanCloseDialog()
        {
            return true;
        }
        //关闭弹窗时触发
        public void OnDialogClosed()
        {
            IDialogResult dialogResult= new DialogResult();
            dialogResult.Parameters.Add("result", true);
            RequestClose.Invoke(dialogResult);
        }
        //打开弹窗时触发
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}

using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
　　　　　　 //打开链接
            ExchangeService _service = new ExchangeService(ExchangeVersion.Exchange2013);
            //不使用当前安全上下文          
            _service.UseDefaultCredentials = false;
            //指定安全上下文
            _service.Credentials =
                new NetworkCredential(
                   "USERNAME",
                   "PASSWORD",
                    "DOMAIN");
            //根据邮箱地址自动发现 
            _service.AutodiscoverUrl("test@domain.com");
            //或者 直接指定，速度稍快，但万一哪天邮箱管理员把它改变了呢
            //_service.Url = new Uri("https:/xxxxx/ews/exchange.asmx");

            //声明一个分页器之类的玩意
            var itemView = new ItemView(10);
            //var folders = _service.FindFolders(WellKnownFolderName.Inbox, folderVw);
            //我们只需要获取未读邮件
            SearchFilter.IsEqualTo unreadFilter =new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);
            try
            {
                //指定收件箱，并绑定到Service 
                var folder = Folder.Bind(_service, WellKnownFolderName.Inbox, BasePropertySet.IdOnly);
                //调用api 获取未读邮件清单
                var items = folder.FindItems(unreadFilter, itemView);
                //var items = _service.FindItems(WellKnownFolderName.Inbox, unreadFilter, itemView);
                //需要指定要解析的属性，更多属性参考该类定义  ！！
                PropertySet propSet = new PropertySet(
                    EmailMessageSchema.TextBody,
                    EmailMessageSchema.IsRead,
                    EmailMessageSchema.Sender,
                    EmailMessageSchema.From, 
                    EmailMessageSchema.Subject);
                //遍历未读邮件
                foreach (EmailMessage item in items)
                {
                    //这里必须指定上面定义的需要解析的架构，重新绑定才可以解析，不然某些属性会报错，比如  You must load or assign this property before you can read its value.
                    EmailMessage message =  (EmailMessage)Item.Bind(_service, item.Id, propSet);
                    
                    Console.WriteLine($"{message.Subject},Body:" + message.TextBody);
                    item.IsRead = true; //设置为已读
                    item.Update(ConflictResolutionMode.AlwaysOverwrite);//调用API更新
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
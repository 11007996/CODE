namespace EAM.ServiceCore.Signalr
{
    /// <summary>
    /// hub消息类型常量
    /// </summary>
    public class HubsConstant
    {
        public const string ReceiveNotice = "receiveNotice"; // 接收后台手动推送消息
        public const string ReceiveChat = "receiveChat"; // 接收聊天数据
        public const string MoreNotice = "moreNotice";// 接收系统通知/公告
        public const string OnlineNum = "onlineNum";//在线人数
        public const string OnlineInfo = "onlineInfo";//当前用户信息
        public const string LockUser = "lockUser";
        public const string ForceUser = "forceUser";//强退用户
        public const string LogOut = "logOut";//用户登出
        public const string ConnId = "connId";//socket连接ID
        public const string OnlineUser = "onlineUser";
    }
}
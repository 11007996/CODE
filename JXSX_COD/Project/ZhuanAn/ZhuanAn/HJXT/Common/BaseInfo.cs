using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Common
{

    public class BaseInfo
    {
        #region config配置文件
        //-----------------基本---------------------
        /// <summary>
        /// 工厂
        /// </summary>
        public static string Factory;

        /// <summary>
        /// 厂区
        /// </summary>
        public static string Area;
        /// <summary>
        /// 线别名称
        /// </summary>
        public static string Line;
        /// <summary>
        /// 机台
        /// </summary>
        public static string Machine;
        //-----------------看板---------------------
        /// <summary>
        /// 部门(看板显示的人员所属部门)
        /// </summary>
        public static string Dept;
        /// <summary>
        /// 看板统计图表显示的机台数(1~30,默认20)
        /// </summary>
        public static int MachineNum = 20;
        /// <summary>
        /// 看板统计图表显示前几周数据（0当前周，1前一周，2是前两周;默认2）
        /// </summary>
        public static int PreWeekNum = 5;
        //-----------------呼叫---------------------
        /// <summary>
        /// 呼叫限制
        /// </summary>
        public static bool CallLimit = false;
        //-----------------广播---------------------
        //设置音量[0,100]
        public static int Volume = 100;

        //设置语速,[-10,10]
        public static int Rate = 0;
        //-----------------通信---------------------
        /// <summary>
        /// 是否开启TCP服务监听
        /// </summary>
        public static bool OpenFlag = false;
        /// <summary>
        /// 当前监听状态
        /// </summary>
        public static bool ListenState = false;
        /// <summary>
        /// 开启服务的IP
        /// </summary>
        public static IPAddress IP;
        /// <summary>
        /// 服务端口号
        /// </summary>
        public static int Port = 10409;
        /// <summary>
        /// 接收超时(分钟)
        /// </summary>
        public static int ReceiveTimeout = 10;
        /// <summary>
        /// 监听限制
        /// </summary>
        public static int ListenLimit = 100;


        //-----------------系统---------------------
        /// <summary>
        /// 图片缓存路径
        /// </summary>
        public static string PicCachePath;
        /// <summary>
        /// 是否开通自动更新，开通则每天早上8:30开始检查更新。
        /// </summary>
        public static bool AutoUpdate = true;
        #endregion

        /// <summary>
        /// 应用启动时间
        /// </summary>
        public static DateTime AppStartTime = TimeUtil.Now;
        /// <summary>
        /// 呼叫标记,用来判断是否可以关闭呼叫面板(true:有未处理完的故障，false:所有故障处理完成 ，默认false)
        /// </summary>
        public static bool CallFlag = false;
        /// <summary>
        /// 看板故障数
        /// </summary>
        public static int KanBanErrorCount = 0;

        #region 记时器
        /// <summary>
        /// 时钟显示开关（默认关闭）
        /// </summary>
        public static bool ClockFlag = false;
        /// <summary>
        /// 计时开始时间
        /// </summary>
        public static DateTime ClockStartTime = TimeUtil.Now;
        #endregion

        #region 登入人
        /// <summary>
        /// 当前登入人工号(为空，未登入)
        /// </summary>
        public static string LoginHandlerNo;
        /// <summary>
        /// 当前登入人名称
        /// </summary>
        public static string LoginHandlerName;
        /// <summary>
        /// 当前登入人权限（A:管理员，B:产线人员）
        /// </summary>
        public static string LoginHandlerRight;
        /// <summary>
        /// 当前登入人部门
        /// </summary>
        public static string LoginHandlerDept;
        #endregion


    }
}


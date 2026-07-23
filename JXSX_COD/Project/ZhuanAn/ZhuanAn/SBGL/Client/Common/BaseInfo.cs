using Common.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
        /// <summary>
        /// 呼叫限制
        /// </summary>
        public static bool CallLimit = false;

        //-----------------看板---------------------
        /// <summary>
        /// 看板显示: 分为
        ///        呼叫看板
        ///        设备看板
        ///        兼容切换
        /// </summary>
        public static string KanBanShow = "呼叫看板";
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
        /// <summary>
        /// 是否开启指定人员的故障显示
        /// </summary>
        public static bool CallHandlerShowFlag = false;

        //-----------------广播---------------------
        /// <summary>
        /// 广播语速[-10,10]
        /// </summary>
        public static int SpeechRate = 0;
        /// <summary>
        /// 广播间隔(分钟)(最小值1分钟)
        /// </summary>
        public static int SpeechSpanMinute = 1;
        /// <summary>
        /// 是否开启呼叫人员广播
        /// </summary>
        public static bool CallHandlerSpeechFlag = false;
     
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

        /// <summary>
        /// 当前打开的窗体类型
        /// </summary>
        public static Type CurrFrmType;

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
        public static string LoginUserNo;
        /// <summary>
        /// 当前登入人名称
        /// </summary>
        public static string LoginUserName;
        /// <summary>
        /// 当前登入人权限（A:管理员，B:产线人员）
        /// </summary>
        public static string LoginUserRight;
        /// <summary>
        /// 当前登入人部门
        /// </summary>
        public static string LoginDept;
        /// <summary>
        /// 设备保养系统WEB端的登入Token，有效时间24小时。
        ///     用于调用文件上传、下载、下载预览的接口使用
        /// </summary>
        public static string AssetSystemToken;
        #endregion


    }
}


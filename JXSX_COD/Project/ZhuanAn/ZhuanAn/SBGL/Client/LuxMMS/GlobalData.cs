using Machine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Call;

namespace LuxMMS
{
    class GlobalData
    {
        #region 窗体对象（看板、求助、信息维护）
        //当前打开的窗体类型（看板、求助、信息维护）
        //public static Type CurrFrmType;
        //呼叫看板
        public static FrmKanBan FrmKanBan;
        //设备看板
        public static FrmMachineWatch FrmMachineWatch;
        //信息维护窗体
        public static FrmTabFrame FrmBaseInfo;
        //呼叫窗体
        public static FrmMaster FrmMaster;
        #endregion
    }
}


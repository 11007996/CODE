using CallSys.Base;
using CallSys.Utils;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace CallSys
{

    class GlobalData
    {
        #region 窗体对象（看板、求助、信息维护）
        //当前打开的窗体类型（看板、求助、信息维护）
        public static Type CurrFrmType;
        //呼叫看板
        public static FrmKanBan FrmKanBan;
        //信息维护窗体
        public static FrmBaseInfo FrmBaseInfo;
        //呼叫窗体
        public static FrmMaster FrmMaster;
        #endregion
    }
}


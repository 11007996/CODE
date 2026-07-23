using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call.Base
{
    public class ErrorInfo
    {
        //----------------------呼叫-------------------------
        /// <summary>
        /// ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 厂区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 线名
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }

        private string _machine;
        /// <summary>
        /// 机台(带*机台编号,但不带线名)
        /// </summary>
        public string Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
                MachineType = _machine.Split('*')[0];
            }
        }

        /// <summary>
        /// 机台型号
        /// </summary>
        public string MachineType { get; set; }

        /// <summary>
        /// 目标人员
        /// </summary>
        public string TargetHandler { get; set; }
        /// <summary>
        /// 目标人员
        /// </summary>
        public string TargetHandlerName { get; set; }

        /// <summary>
        /// 开始呼叫时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 呼叫原因
        /// </summary>
        public string CallReason { get; set; }

        /// <summary>
        /// 状态(A待处理 B处理中 C已完成 D解除呼叫)
        /// </summary>
        public ErrorStatus Status { get; set; }

        /// <summary>
        /// 最大处理时间（<=0 时无限制）
        /// </summary>
        public int MaxHandleTimes { get; set; }

        /// <summary>
        /// 最大支援时间（<=0 时无限制）
        /// </summary>
        public int MaxHelpTimes { get; set; }

        /// <summary>
        /// 开始处理时间
        /// </summary>
        public DateTime? ComeTime { get; set; }




        //---------------------处理中------------------------------
        //处理人刷卡信息
        public ScanInfo HandlerScanInfo { get; set; }
        //支援人刷卡信息
        public ScanInfo HelperScanInfo { get; set; }
        //完成人刷卡信息
        public ScanInfo SolverScanInfo { get; set; }


        ///// <summary>
        ///// 处理人
        ///// </summary>
        public string HandlerNo { get; set; }

        ///// <summary>
        ///// 支援人
        ///// </summary>
        public string HelperNo { get; set; }

        ///// <summary>
        ///// 解决人
        ///// </summary>
        public string SolverNo { get; set; }

        ///// <summary>
        ///// 解决人名称
        ///// </summary>
        public string SolverName { get; set; }

        //-----------------------处理完成------------------------------
        ///// <summary>
        ///// 品质确认人
        ///// </summary>
        public string QCName { get; set; }

        /// <summary>
        /// 调机品数
        /// </summary>
        public int? ProdCount { get; set; }

        /// <summary>
        /// 良品数
        /// </summary>
        public int? PassCount { get; set; }

        /// <summary>
        /// 不良品数
        /// </summary>
        public int? NGCount { get; set; }

        /// <summary>
        /// 故障原因
        /// </summary>
        public string ErrorReason { get; set; }

        /// <summary>
        /// 故障类型
        /// </summary>
        public string FaultType { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        public string FaultContent { get; set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        public string SolutionContent { get; set; }

        /// <summary>
        /// 处理完成时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    //刷卡信息
    public class ScanInfo
    {
        /// <summary>
        /// 处理人编号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 当前处理人刷卡时间
        /// </summary>
        public DateTime ScanTime { get; set; }
    }
}

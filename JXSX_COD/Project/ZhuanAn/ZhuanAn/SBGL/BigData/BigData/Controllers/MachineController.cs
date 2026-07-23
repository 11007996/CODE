using BigData.BLL;
using BigData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigData.Controllers
{

    public class MachineController : Controller
    {
        private MachineService service = new MachineService();

        /// <summary>
        /// 统计设备的状态
        /// </summary>
        /// <returns></returns>
        public JsonResult MachinesState()
        {
            IList<MachineState> list = service.GetMachineStateStat();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 统计设备分布
        /// </summary>
        /// <returns></returns>
        public JsonResult MachinesDistribute()
        {
            IList<MachineDistribute> list = service.GetMachineDistributeStat();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取实时上报记录，每个设备最后上报的6条记录
        /// </summary>
        /// <returns></returns>
        public JsonResult MachineCurrLastReports()
        {
            IList<Machine> list = service.CurrLastReport(6);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取呼叫次数统计：（月、周）
        /// </summary>
        /// <returns></returns>
        public JsonResult CallStat()
        {
            CallStatVo csVo = service.CallStat();
            return Json(csVo, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当日、昨日的呼叫次数
        /// </summary>
        /// <returns></returns>
        public JsonResult CallWeekAndMonthCountStat()
        {
            List<int> countList = service.CallWeekAndMonthCountStat();
            int currWeekCount = countList[0];
            int preOneWeekCount = countList[1];
            int preTwoWeekCount = countList[2];
            int preOneMonthCount = countList[3];
            int preTwoMonthCount = countList[4];
            decimal preWeekRate = Math.Floor((decimal)(preOneWeekCount - preTwoWeekCount) / preTwoWeekCount * 100);
            decimal preMonthRate = Math.Floor((decimal)(preOneMonthCount - preTwoMonthCount) / preTwoMonthCount * 100);
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "CurrWeekCount", currWeekCount },
                { "PreOneWeekCount", preOneWeekCount },
                { "PreTwoWeekCount", preTwoWeekCount },
                { "PreMonthRate", preMonthRate },
                { "PreWeekRate", preWeekRate }
            };
            return Json(dic, JsonRequestBehavior.AllowGet);
        }


        //实时故障，平均oee,设备实时状态，设备oee
        public JsonResult MachineStatOneDay()
        {
            var r = service.MachineStatReportOneDay();
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 24小时呼叫次数
        /// </summary>
        /// <returns></returns>
        public JsonResult MachineCallOneDay()
        {
            var r = service.MachineCallStatByOneDay();
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 能耗统计
        /// </summary>
        /// <returns></returns>
        public JsonResult MachineEnergyStat()
        {
            var r = service.MachineEnergyStat();
            return Json(r, JsonRequestBehavior.AllowGet);
        }


    }
}
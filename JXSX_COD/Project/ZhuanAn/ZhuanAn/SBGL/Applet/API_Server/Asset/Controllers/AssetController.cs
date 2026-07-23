using Asset.BLL;
using Asset.Filter;
using Asset.Model;
using Asset.Model.Enum;
using Asset.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Asset.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AssetController : BaseController
    {
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(AssetController));
        private AssetService service = new AssetService();
        private UserService userService = new UserService();


        /// <summary>
        /// 搜索资产
        /// </summary>
        /// 
        [MyAuthorize]
        public JsonResult AssetList(string keywords)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                IList<AssetInfoDO> list = service.GetAssetList(keywords);

                r.MsgCode = "0";
                r.Data = list;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.Data = "获取资产列表信息失败,异常:" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取资产详情
        /// </summary>
        /// <param name="assetNo"></param>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult AssetInfo(string assetNo)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                AssetInfoDO asset = service.GetAssetInfo(assetNo);
                if (asset != null)
                {
                    r.MsgCode = "0";
                    r.Data = asset;
                }
                else
                {
                    r.MsgCode = "1";
                    r.MsgInfo = "未找到此资产编号";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.Data = "获取资产信息失败,异常:" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取保养项目名称集合
        /// </summary>
        /// <param name="assetNo"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="timeMark"></param>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult MaintenanceItems(string assetNo, int year, int month, string timeMark, int timeMarkValue)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                //timeMarkValue转timeMarkStamp
                int timeMarkStamp = 0;
                switch (timeMark)
                {
                    case "D":
                        DateTime day = Convert.ToDateTime(year + "-" + month + "-" + timeMarkValue);
                        timeMarkStamp = day.DayOfYear;
                        break;
                    case "W":
                        DateTime firstDay = Convert.ToDateTime(year + "-" + month + "-1");
                        GregorianCalendar gregorianCalendar = new GregorianCalendar();
                        int weekStamp = gregorianCalendar.GetWeekOfYear(firstDay, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周
                        timeMarkStamp = weekStamp + (timeMarkValue - 1);
                        break;
                    case "M":
                    case "Q":
                    case "Y":
                        timeMarkStamp = timeMarkValue;
                        break;
                }

                //获取保养项目
                List<MaintenanceItemDO> items = service.GetMaintenanceItems(assetNo, year, timeMark, timeMarkStamp);
                if (items == null || items.Count == 0)
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "未找到保养项目";
                    return Json(r, JsonRequestBehavior.AllowGet);
                }

                //判断结果
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("items", items);
                dic.Add("timeMarkStamp", timeMarkStamp);
                r.MsgCode = "0";
                r.Data = dic;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.Data = "获取保养项目名称集合,异常:" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新保养记录
        /// </summary>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult UpdateMaintenance(MaintenanceReportDetialDO param)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                r.MsgCode = "0";
                //参数验证
                if (param == null || param.AssetNo == null || param.Year <= 0 || param.TimeMark == null || param.TimeMarkStamp <= 0)
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "参数不正确";
                    return Json(r, JsonRequestBehavior.AllowGet);
                }

                //修改的时间判断是否合规则
                DateTime now = DateTime.Now;
                GregorianCalendar gregorianCalendar = new GregorianCalendar();
                int weekStamp = gregorianCalendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//当天所属周

                bool TimeCheckFlag = true;
                if (param.Year != now.Year) TimeCheckFlag = false;//年表，判断是否当年
                else if (param.TimeMark == "M" && param.TimeMarkStamp != now.Month) TimeCheckFlag = false;//月表，判断是否当年当月
                else if (param.TimeMark == "D" && param.TimeMarkStamp != now.DayOfYear) TimeCheckFlag = false;//日期是否当天
                else if (param.TimeMark == "W" && param.TimeMarkStamp != weekStamp) TimeCheckFlag = false;//周期是否合规
                else if (param.TimeMark == "M" && now.Day < 25) TimeCheckFlag = false;//日期是否大于等于当月25号

                //权限验证
                UserInfoDO loginUser = GetCurrUser();

                if (param.TimeMark == "W" || param.TimeMark == "M" || !TimeCheckFlag)
                {
                    SysUserDO sysUser = userService.GetUserInfo(loginUser.WorkCode);
                    if (sysUser == null)
                    {
                        r.MsgCode = "-1";
                        r.MsgInfo = "该用户无权限";
                        return Json(r, JsonRequestBehavior.AllowGet);
                    }
                    if ((param.TimeMark == "W" || param.TimeMark == "M") && (sysUser.Dept.IndexOf("生技") < 0 && sysUser.UserRight != "A"))
                    {
                        r.MsgCode = "-1";
                        r.MsgInfo = "非生技用户，无修改权限";
                        return Json(r, JsonRequestBehavior.AllowGet);
                    }
                    if (!TimeCheckFlag && sysUser.UserRight != "A")
                    {//时间限制权限未通过
                        r.MsgCode = "-1";
                        r.MsgInfo = "超编辑期限，不可修改";
                        return Json(r, JsonRequestBehavior.AllowGet);
                    }
                }

                //保养项目参数处理，将[保养人签名]设置为当前用户
                if (param.ItemValueDic != null)
                {

                    for (int i = param.ItemValueDic.Count - 1; i >= 0; i--)
                    {
                        if (param.ItemValueDic[i].ItemName == "保养人签名")
                        {
                            param.ItemValueDic.RemoveAt(i);
                            break;
                        }
                    }
                    MaintenanceItemDO item = new MaintenanceItemDO();
                    item.ItemName = "保养人签名";
                    item.ItemValue = loginUser.UserName;
                    param.ItemValueDic.Add(item);
                }

                //保存
                if (!service.UpdateMaintenanceD(param, loginUser.WorkCode))
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "保存失败";
                    return Json(r, JsonRequestBehavior.AllowGet);
                }

                if (!string.IsNullOrWhiteSpace(r.MsgInfo))
                {
                    r.MsgCode = "-1";
                }
                else
                {
                    r.MsgInfo = "保存成功";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.Data = "更新保养记录失败,异常:" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取资产文件信息
        /// </summary>
        /// <param name="assetNo"></param>
        /// <param name="fileClass"></param>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult AssetFileInfo(string assetNo, FileClassEnum fileClass)
        {
            ResultMsg r = new ResultMsg();
            try
            {
                //获取保养项目
                FileInfoDO fi = service.GetAssetFileInfo(assetNo, fileClass);
                if (fi == null || fi.FileId == 0)
                {
                    r.MsgCode = "-1";
                    r.MsgInfo = "未找到文件信息";
                    return Json(r, JsonRequestBehavior.AllowGet);
                }

                //判断结果
                r.MsgCode = "0";
                r.Data = fi;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.Data = "获取资产文件信息失败,异常:" + ex.Message;

            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }

}
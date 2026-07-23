using Asset.DAL;
using Asset.Model;
using Asset.Model.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asset.BLL
{
    public class AssetService
    {
        private AssetDAL dao = new AssetDAL();

        /// <summary>
        /// 搜索资产
        /// </summary>
        /// <returns></returns>
        public IList<AssetInfoDO> GetAssetList(string keywords)
        {
            IList<AssetInfoDO> list = dao.GetAssetList(keywords);
            return list;
        }

        /// <summary>
        ///获取资产详情
        /// </summary>
        /// <returns></returns>
        public AssetInfoDO GetAssetInfo(string assetNo)
        {
            AssetInfoDO info = dao.GetAssetInfo(assetNo);
            info.FileInfo = dao.GetAssetFileInfos(assetNo);
            return info;
        }



        /// <summary>
        /// 获取保养项目记录
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="timeMark"></param>
        /// <param name="timeMarkValue"></param>
        /// <returns></returns>
        public List<MaintenanceItemDO> GetMaintenanceItems(string assetNo, int year, string timeMark, int timeMarkStamp)
        {
            List<MaintenanceItemDO> mItems = new List<MaintenanceItemDO>();
            //查询原有的记录
            int? Id = dao.GetMaintenanceRecordId(assetNo, year, timeMark, timeMarkStamp);
            DataTable itemValDT = null;
            if (Id != null)
            {
                itemValDT = dao.GetMaintenanceDetailItemData((int)Id);
            }

            //保养项目
            List<string> items = dao.GetMaintenanceItems(assetNo, year, timeMark);
            if (items != null)
            {
                foreach (string itemName in items)
                {
                    MaintenanceItemDO item = new MaintenanceItemDO();
                    item.ItemName = itemName;
                    //获取保养项目的值
                    if (itemValDT != null)
                    {
                        DataRow[] row = itemValDT.Select("ItemName='" + itemName + "'");
                        if (row != null && row.Length > 0)
                        {
                            item.ItemValue = Convert.ToString(row[0]["ItemValue"]);
                        }
                    }
                    //无值处理
                    if (string.IsNullOrWhiteSpace(item.ItemValue))
                    {
                        if (item.ItemName == "保养人签名")
                            item.ItemValue = "";
                        else
                            item.ItemValue = "X";
                    }
                    mItems.Add(item);
                }
            }
            return mItems;
        }


        public bool UpdateMaintenanceD(MaintenanceReportDetialDO param, string workCode)
        {
            bool r = false;
            //检查保养记录并添加
            int? id = dao.GetMaintenanceRecordId(param.AssetNo, param.Year, param.TimeMark, param.TimeMarkStamp);
            if (id == null)
            {
                id = dao.InsertMaintenanceD(param, workCode);
            }
            else
            {
                dao.UpdateMaintenanceD((int)id, workCode);
            }

            //添加保养记录详情 
            if (id != null)
            {
                r = dao.SaveMaintenanceDV((int)id, param.ItemValueDic);
            }
            return true;
        }


        public FileInfoDO GetAssetFileInfo(string assetNo, FileClassEnum fileClass)
        {
            if (string.IsNullOrWhiteSpace(assetNo))
                return null;
            if (fileClass == FileClassEnum.None)
                return null;
            return dao.GetAssetFileInfo(assetNo, fileClass);
        }
    }

}

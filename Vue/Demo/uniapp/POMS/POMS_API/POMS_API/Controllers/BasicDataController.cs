using Microsoft.AspNetCore.Mvc;
using static POMS_API.Models.BaseClass;
using POMS_API.Models;

namespace POMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasicDataController : Controller
    {
        public BasicDataController()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
            DBhelper.mesdbhelper.connString = configuration.GetConnectionString("DefaultConnection");
        }


        /// <summary>
        /// 新增线别
        /// </summary>
        /// <param name="lineData"></param>
        /// <returns></returns>
        [HttpPost("AddLine", Name = "AddLine")]
        public APIReturn AddLine(BasicData.Line lineData)
        {
            APIReturn apiReturn = new APIReturn();
            string res = string.Empty;
            if(lineData != null)
            {
                res=lineData.AddLine();
                if(res.StartsWith("OK"))
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "新增OK";
                    apiReturn.data.Add(lineData);

                }
                else
                {
                    apiReturn.status=301;
                    apiReturn.msg = res;
                }
                return apiReturn;
            }
            else
            {
                apiReturn.status = 301;
                apiReturn.msg = "新增数据为空";
                return apiReturn;
            }
        }

        /// <summary>
        /// 获取线别详细信息
        /// </summary>
        /// <param name="lineid"></param>
        /// <returns></returns>
        [HttpGet("GetLineDetailsById", Name = "GetLineDetailsById")]
        public APIReturn GetLineDetailsById(string lineid)
        {
            APIReturn apiReturn = new APIReturn();
            BasicData.Line lineData = new BasicData.Line();
            lineData.lineId =Convert.ToInt32(lineid);
            lineData=lineData.GetLineDetailsById();
            if (!string.IsNullOrEmpty(lineData.lineName))
            {
                apiReturn.status = 0;
                apiReturn.msg = "OK";
                apiReturn.data.Add(lineData);
                return apiReturn;
            }
            else
            {
                apiReturn.status = 201;
                apiReturn.msg = "线别未维护";
                return apiReturn;
            }
        }

        //获取班别用于线别设置
        [HttpGet("GetShiftOverview", Name = "GetShiftOverview")]
        public APIReturn GetShiftOverview()
        {
            APIReturn apiReturn = new APIReturn();
            BasicData.Shift shift = new BasicData.Shift();
            List<BasicData.Shift> shifts=shift.GetShiftOverview();
            if(shifts.Count>0)
            {
                apiReturn.status = 0;
                apiReturn.msg = "OK";
                apiReturn.data.Add(shifts);
                return apiReturn;
            }
            else
            {
                apiReturn.status = 201;
                apiReturn.msg = "班别未维护";
                return apiReturn;
            }

        }
    }
}

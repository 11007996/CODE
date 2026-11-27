using Microsoft.AspNetCore.Mvc;
using POMS_API.Helper.Services;
using POMS_API.Models;
using System.Data;
using System.Text;
using static POMS_API.Models.BaseClass;
using static POMS_API.Models.BasicData;

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


        #region 新增线别
        /// <summary>
        /// 新增线别
        /// </summary>
        /// <param name="lineData">线体实体类</param>
        /// <returns></returns>
        [HttpPost("AddLine", Name = "AddLine")]
        public APIReturn AddLine(BasicData.Line lineData)
        {
            APIReturn apiReturn = new APIReturn();
            string res = string.Empty;
            if (lineData != null)
            {
                res = lineData.AddLine();
                if (res.StartsWith("OK"))
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "新增OK";
                    apiReturn.data.Add(lineData);

                }
                else
                {
                    apiReturn.status = 301;
                    apiReturn.msg = res;
                }
                return apiReturn;
            }
            else
            {
                apiReturn.status = 301;
                apiReturn.msg = "新增线别数据为空";
                return apiReturn;
            }
        }
        #endregion

        # region 获取线别详细信息
        /// <summary>
        /// 获取单个线别详细信息
        /// </summary>
        /// <param name="lineid"></param>
        /// <returns></returns>
        [HttpGet("GetLineDetailsById", Name = "GetLineDetailsById")]
        public APIReturn GetLineDetailsById(string lineid)
        {
            APIReturn apiReturn = new APIReturn();
            BasicData.Line lineData = new BasicData.Line();
            lineData.lineId = Convert.ToInt32(lineid);
            lineData = lineData.GetLineDetailsById();
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
        #endregion

        # region 获取班别用于线别设置
        /// <summary>
        /// 获取所有班别概况
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShiftOverview", Name = "GetShiftOverview")]
        public APIReturn GetShiftOverview()
        {
            APIReturn apiReturn = new APIReturn();
            BasicData.Shift shift = new BasicData.Shift();
            List<BasicData.Shift> shifts = shift.GetShiftOverview();
            if (shifts.Count > 0)
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
        #endregion

        #region 修改线别
        /// <summary>
        /// 修改线别资料
        /// </summary>
        /// <param name="lineData"></param>
        /// <returns></returns>
        [HttpPut("ModifyLine", Name = "ModifyLine")]
        public APIReturn ModifyLine(BasicData.Line lineData)
        {
            APIReturn apiReturn = new APIReturn();
            string res = string.Empty;
            if (lineData != null)
            {
                res = lineData.ModifyLine();
                if (res.StartsWith("OK"))
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "修改OK";
                    apiReturn.data.Add(lineData);

                }
                else
                {
                    apiReturn.status = 301;
                    apiReturn.msg = res;
                }
                return apiReturn;
            }
            else
            {
                apiReturn.status = 301;
                apiReturn.msg = "修改线别数据为空";
                return apiReturn;
            }
        }
        #endregion

        #region 删除线别
        /// <summary>
        /// 删除线别资料
        /// </summary>
        /// <param name="lineid">线别ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteLine", Name = "DeleteLine")]
        public APIReturn DeleteLine(int lineid)
        {
            APIReturn apiReturn = new APIReturn();
            string res = string.Empty;
            BasicData.Line lineData = new BasicData.Line();
            lineData.lineId = lineid;
            res = lineData.DeleteLine();
            if (res.StartsWith("OK"))
            {
                apiReturn.status = 0;
                apiReturn.msg = "删除OK";
                apiReturn.data.Add(lineData);

            }
            else
            {
                apiReturn.status = 301;
                apiReturn.msg = res;
            }
            return apiReturn;
        }
        #endregion

        #region 查询所有线别
        /// <summary>
        /// 查询所有线别
        /// </summary>
        /// <param name="lineName">线别名称</param>
        /// <param name="lineEnabled">线别是否有效</param>
        /// <returns></returns>
        [HttpGet("GetLineAll", Name = "GetLineAll")]
        public APIReturn GetLineAll(string lineName = "", string lineEnabled = "")
        {
            APIReturn apiReturn = new APIReturn();
            TokenService tokenService = new TokenService();
            string jwt = HttpContext.Request.Headers["Authorization"];
            TokenService.ReturnInfo returninfo = tokenService.CheckToken(jwt);
            if (returninfo.status != 0)
            {
                apiReturn.status = returninfo.status;
                apiReturn.msg = returninfo.msg;
                return apiReturn;
            }

            string res = string.Empty;
            List<BasicData.Line> lines = new List<BasicData.Line>();
            try
            {
                BasicData.Line line = new BasicData.Line();
                if (!string.IsNullOrEmpty(lineName))
                {
                    line.lineName = lineName;
                }
                if (!string.IsNullOrEmpty(lineEnabled))
                {
                    line.enabled = lineEnabled;
                }
                lines = line.GetLineAll();
                if (lines.Count > 0)
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "OK";
                    apiReturn.data.Add(lines);
                }
                else
                {
                    apiReturn.status = 201;
                    apiReturn.msg = "线别未维护";
                }
                return apiReturn;
            }
            catch (Exception ex)
            {
                apiReturn.status = 2;
                apiReturn.msg = "线别获取异常，联系IT运维人员";
                return apiReturn;
            }
        }
        #endregion

        #region 查询所有部门
        /// <summary>
        /// 查询所有部门
        /// </summary>
        /// <param name="enabled">是否有效</param>
        /// <returns></returns>
        [HttpGet("GetDepartAll", Name = "GetDepartAll")]
        public APIReturn GetDepartAll(string enabled)
        {
            APIReturn apiReturn = new APIReturn();
            TokenService tokenService = new TokenService();
            string jwt = HttpContext.Request.Headers["Authorization"];
            TokenService.ReturnInfo returninfo = tokenService.CheckToken(jwt);
            if (returninfo.status != 0)
            {
                apiReturn.status = returninfo.status;
                apiReturn.msg = returninfo.msg;
                return apiReturn;
            }

            List<BasicData.Departmant> departmants = new List<BasicData.Departmant>();
            Departmant depart = new Departmant();
            depart.enabled = enabled;
            departmants = depart.GetDepartAll();
            if (departmants.Count > 0)
            {
                apiReturn.status = 0;
                apiReturn.msg = "OK";
                apiReturn.data.Add(departmants);
            }
            else
            {
                apiReturn.status = 202;
                apiReturn.msg = "部门未维护";
            }
            return apiReturn;
        }
        #endregion

        #region 修改部门资料
        /// <summary>
        /// 修改部门资料
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        [HttpPut("ModifyDepartById", Name = "ModifyDepartById")]
        public APIReturn ModifyDepartById(BasicData.Departmant depart)
        {
            APIReturn apiReturn = new APIReturn();
            if (depart.departId == 0)
            {
                apiReturn.status = 302;
                apiReturn.msg = "部门ID为空";
                return apiReturn;
            }
            string res = depart.ModifyDepartById();
            if (res.StartsWith("OK"))
            {
                apiReturn.status = 0;
                apiReturn.msg = res;
            }
            else
            {
                apiReturn.status = 302;
                apiReturn.msg = res;
            }
            return apiReturn;
        }
        #endregion 

        #region 新增部门
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        [HttpPost("AddDepart", Name = "AddDepart")]
        public APIReturn AddDepart(BasicData.Departmant depart)
        {
            APIReturn apiReturn = new APIReturn();
            try
            {
                string res = depart.AddDepart();
                if (res.StartsWith("OK"))
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "增加OK";
                    apiReturn.data.Add(res.Split(":")[1]);
                }
                else
                {
                    apiReturn.status = 302;
                    apiReturn.msg = res;
                }
                return apiReturn;
            }
            catch (Exception ex)
            {
                apiReturn.status = 3;
                apiReturn.msg = ex.Message;
                return apiReturn;
            }

        }
        #endregion

        #region 删除部门
        /// <summary>
        /// 删除部门资料
        /// </summary>
        /// <param name="departid"></param>
        /// <returns></returns>
        [HttpDelete("DateleDepart",Name = "DateleDepart")]
        public APIReturn DateleDepart(int departid)
        {
            APIReturn apiReturn = new APIReturn();
            BasicData.Departmant departmant = new BasicData.Departmant();
            departmant.departId = departid;
            try
            {
                string res = departmant.DateleDepart();
                if (res.StartsWith("OK"))
                {
                    apiReturn.status = 0;
                    apiReturn.msg = "删除完成";
                }
                else
                {
                    apiReturn.status = 302;
                    apiReturn.msg = res;
                }
                return apiReturn;
            }
            catch (Exception ex)
            {
                apiReturn.status = 3;
                apiReturn.msg = ex.Message;
                return apiReturn;
            }
        }
        #endregion
    }
}

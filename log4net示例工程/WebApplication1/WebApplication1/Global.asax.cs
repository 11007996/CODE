using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.DOMConfigurator.Configure();
            ActionHelper.LogHelper log = ActionHelper.LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("系统启动");
            log.Debug("运行错误");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ActionHelper.LogHelper log = ActionHelper.LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error("未处理异常",HttpContext.Current.Server.GetLastError());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            ActionHelper.LogHelper log = ActionHelper.LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("系统停止");
        }
    }
}
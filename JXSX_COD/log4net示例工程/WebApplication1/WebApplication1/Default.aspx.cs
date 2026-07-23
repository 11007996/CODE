using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        private ActionHelper.LogHelper log;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageLoad();
        }

        private void pageLoad()
        {
            
            log = ActionHelper.LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName + ":" + MethodBase.GetCurrentMethod().Name);
            log.Info("起始页面载入222");
            log.Error("起始页面载入");
        }
    }
}
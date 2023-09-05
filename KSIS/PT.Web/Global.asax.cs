using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PT.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //세션값 초기화
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["UserId"] = "";
            Session["UserNm"] = "";
            Session["Email"] = "";
            Session["DeptCd"] = "";
            Session["DeptNm"] = "";
            Session["TitleCd"] = "";
            Session["TitleNm"] = "";
            Session["DutyCd"] = "";
            Session["DutyNm"] = "";
            Session["EmpNo"] = "";
            Session["Server"] = "";
            Session["SiteCd"] = "";
            Session["BizPartCd"] = "";
            Session["AdminConfirm"] = "";
        }

    }

    
}
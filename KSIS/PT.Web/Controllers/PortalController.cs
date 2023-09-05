using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class PortalController : BaseController
    {
        PortalFactory _portalFactory = new PortalFactory();
        UserFactory _userFactory = new UserFactory();

        #region Home : 홈
        [HttpGet]
        public ActionResult Home()
        {
            //홈
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Home", Session["browser"].ToString(), Session["device"].ToString());
            Portal dbSuju = _portalFactory.GetSujuPlanResultHome(Util.MakeDateTime("YearMon"));

            return View(dbSuju);
        }
        #endregion
         
        

        #region test : 테스트
        [HttpGet]
        public ActionResult test()
        {            
            Portal dbSuju = _portalFactory.GetSujuPlanResultHome(Util.MakeDateTime("YearMon"));
            return View(dbSuju);
        }
        #endregion
        
    }
}

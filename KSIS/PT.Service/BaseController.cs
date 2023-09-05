using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PT.Service
{
    [BaseAuthAttribute]
    public class BaseController : Controller
    {
        protected string _UserId { get; private set; }
        protected string _UserNm { get; private set; }
        protected string _Email { get; private set; }
        protected static string _DeptCd { get; private set; }
        protected string _DeptNm { get; private set; }
        protected string _TitleCd { get; private set; }
        protected string _TitleNm { get; private set; }
        protected string _DutyCd { get; private set; }
        protected string _DutyNm { get; private set; }
        protected string _EmpNo { get; private set; }
        protected string _Server { get; private set; }
        protected string _SiteCd { get; private set; }
        protected string _BizPartCd { get; private set; }
        protected string _AdminConfirm { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Session["UserId"] != null)
            {               
                _UserId = Session["UserId"].ToString();
                _UserNm = Session["UserNm"].ToString();
                _Email = Session["Email"].ToString();
                _DeptCd = Session["DeptCd"].ToString();
                _DeptNm = Session["DeptNm"].ToString();
                _TitleCd = Session["TitleCd"].ToString();
                _TitleNm = Session["TitleNm"].ToString();
                _DutyCd = Session["DutyCd"].ToString();
                _DutyNm = Session["DutyNm"].ToString();
                _EmpNo = Session["EmpNo"].ToString();
                _Server = Session["Server"].ToString();
                _SiteCd = Session["SiteCd"].ToString();
                _BizPartCd = Session["BizPartCd"].ToString();
                _AdminConfirm = Session["AdminConfirm"].ToString();
            }
        }
    }
}

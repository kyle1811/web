using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class SingleSignOnController : Controller
    {
        UserFactory _userFactory = new UserFactory();

        #region AutoLogin : 자동로그인(Get)
        [HttpGet]
        public ActionResult AutoLogin(string pUserId, string pPwd, string pUrl)
        {
            User pUser = new User();

            // 사용자ID 소문자로 변환
            pUser.UserId = HttpUtility.UrlDecode(pUserId).ToLower();
            pUser.Pwd = pPwd;
                       
            // 로그인 사용자정보 확인 
            User dbUser = _userFactory.CheckUserInfo(pUser);

            if (dbUser.ReturnVal == "NotRegister")
            {
                return RouteUtil.MessageAndMove("등록되지 않은 이메일 입니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            if (dbUser.ReturnVal == "NotUse")
            {
                return RouteUtil.MessageAndMove("사용이 정지된 이메일 입니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            if (dbUser.ReturnVal == "NotPwd")
            {
                return RouteUtil.MessageAndMove("패스워드가 일치하지 않습니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            try
            {
                // IE8 에서 Url을 두번 호출하는 증상으로 인한 처리
                /*if (Session["UserId"] == null)
                {
                    string pPage = "SingleSignOnLogin";
                    
                }*/

                // 사용자정보 조회
                dbUser = _userFactory.GetLoginUser(pUser.UserId);

                // Email로 인증쿠키 생성, 인증쿠키는 브라우저를 닫으면 유지되지 않도록 설정
                FormsAuthentication.SetAuthCookie(dbUser.UserId, false);

                // 세션에 사용자 정보 저장
                Session["UserId"] = dbUser.UserId;
                Session["UserNm"] = dbUser.UserNm;
                Session["Email"] = dbUser.Email;
                Session["DeptCd"] = dbUser.DeptCd;
                Session["DeptNm"] = dbUser.DeptNm;
                Session["TitleCd"] = dbUser.TitleCd;
                Session["TitleNm"] = dbUser.TitleNm;
                Session["DutyCd"] = dbUser.DutyCd;
                Session["DutyNm"] = dbUser.DutyNm;
                Session["EmpNo"] = dbUser.EmpNo;
                Session["Server"] = dbUser.Server;
                Session["SiteCd"] = dbUser.SiteCd;
                Session["BizPartCd"] = dbUser.BizPartCd;
                Session["AdminConfirm"] = dbUser.AdminConfirm;
                Session["browser"] = HttpContext.Request.Browser.Browser;

                var UserAgent = Request.UserAgent.ToString().ToLower();

                if (UserAgent != null)
                {
                    if ((UserAgent.Contains("mobile") || UserAgent.Contains("iphone") || UserAgent.Contains("ipod") ||
                        UserAgent.Contains("windows ce") || UserAgent.Contains("windows phone") || UserAgent.Contains("minimo phone") || UserAgent.Contains("opera mobi")
                        || UserAgent.Contains("opera mini") || UserAgent.Contains("blackberry") || UserAgent.Contains("nokia") || UserAgent.Contains("sm-g")) && (!UserAgent.Contains("ipad")))
                    {
                        Session["device"] = "Mobile";
                    }
                    else if (UserAgent.Contains("android") || UserAgent.Contains("ipad") || UserAgent.Contains("sm-p"))
                    {
                        Session["device"] = "Tablet";
                    }
                    else
                    {
                        Session["device"] = "PC";
                    }

                }

                // 자동 로그인정보 저장
                _userFactory.CreateLoginLogPlus(pUser.UserId, Request.ServerVariables["REMOTE_ADDR"].ToString(), "AutoLogin", Session["browser"].ToString(), Session["device"].ToString());

                if (pUrl == "")
                {
                    return Redirect("/Portal/Home");
                }
                else if(pUrl == "OrderResult")
                {
                    return Redirect("/Manage/OrderResult");
                }
                else
                {
                    return Redirect(HttpUtility.UrlDecode(pUrl));
                }
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Account/Login?pUserId=" + pUser.UserId);
            }
        }
        #endregion
    }
}

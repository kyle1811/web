using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;


namespace PT.Web.Controllers
{
    public class AccountController : Controller
    {
        AttFileFactory _attFileFactory = new AttFileFactory();
        UserFactory _userFactory = new UserFactory();

        #region Login : 로그인(Get)
        [HttpGet]
        public ActionResult Login(string pUserId)
        {
            // 폼인증과 세션을 제거한다.
            FormsAuthentication.SignOut();
            Session.Clear();

            ViewBag.userId = pUserId;
            return View();
        }
        #endregion

        #region Login : 로그인(Post)
        [HttpPost]
        public ActionResult Login(User pUser)
        {
            // 사용자ID 소문자로 변환
            pUser.UserId = pUser.UserId.ToLower();

            pUser.Pwd = Util.MakeSHA256HashCode(pUser.Pwd);

            pUser.Device = pUser.Device;

            // 로그인 사용자정보 확인 
            User dbUser = _userFactory.CheckUserInfo(pUser);

            if (dbUser.ReturnVal == "NotRegister")
            {
                return RouteUtil.MessageAndMove("등록되지 않은 계정 입니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            if (dbUser.ReturnVal == "NotUse")
            {
                return RouteUtil.MessageAndMove("사용이 정지된 계정 입니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            if (dbUser.ReturnVal == "NotPwd")
            {
                return RouteUtil.MessageAndMove("패스워드가 일치하지 않습니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            if (dbUser.ReturnVal == "NotOpen")
            {
                return RouteUtil.MessageAndMove("시스템 오픈 전으로 접속이 불가합니다.", "/Account/Login?pUserId=" + pUser.UserId);
            }

            try
            {
                // IE8 에서 Url을 두번 호출하는 증상으로 인한 처리
                /*if (Session["UserId"] == null)
                {
                    // 로그인정보 저장
                    _userFactory.CreateLoginLog(pUser.UserId, Request.ServerVariables["REMOTE_ADDR"].ToString(), "로그인");
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

                if (pUser.Device.ToString() == "ipad")
                {
                    UserAgent = "ipad";
                }

                if (UserAgent != null)
                {
                    if((UserAgent.Contains("mobile") || UserAgent.Contains("iphone") || UserAgent.Contains("ipod") ||
                        UserAgent.Contains("windows ce") || UserAgent.Contains("windows phone") || UserAgent.Contains("minimo phone") || UserAgent.Contains("opera mobi")
                        || UserAgent.Contains("opera mini") || UserAgent.Contains("blackberry") || UserAgent.Contains("nokia") || UserAgent.Contains("sm-g")) && (!UserAgent.Contains("ipad")))             
                    {
                        Session["device"] = "Mobile";                        
                    }
                    else if(UserAgent.Contains("android") || UserAgent.Contains("ipad") || UserAgent.Contains("sm-p"))
                    {
                        Session["device"] = "Tablet";                        
                    }
                    else
                    {
                        Session["device"] = "PC";                        
                    }
                    

                }
                
                // 로그인 정보 저장
                _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", Session["browser"].ToString(), Session["device"].ToString());

                return Redirect("/Portal/Home");
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Account/Login?pUserId=" + pUser.UserId);
            }
        }
        #endregion

        #region Logout : 로그아웃
        [HttpGet]
        public ActionResult Logout()
        {
            // 로그아웃 정보 저장
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Logout", Session["browser"].ToString(), Session["device"].ToString());

            // 폼인증과 세션을 제거한다.
            FormsAuthentication.SignOut();
            Session.Clear();

            return Redirect("/Account/Login");
        }
        #endregion

        #region TESTIMAGE
        [HttpGet]
        public ActionResult TESTIMAGE()
        {
           
            User dbUser = _userFactory.KCCIMGTEST("21300610");

            string imageBase64Data = Convert.ToBase64String(dbUser.userimgpic);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            ViewBag.ImageData = imageDataURL;

            return View();
        }

        #endregion

        #region testload
        [HttpGet]
        public ActionResult testload()
        {
            return View();
        }

        #endregion

    }
}

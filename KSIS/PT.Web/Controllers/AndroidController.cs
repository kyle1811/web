using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class AndroidController : Controller
    {
        UserFactory _userFactory = new UserFactory();
        ControlFactory _controlFactory = new ControlFactory();

        [HttpGet]
        public void AndroidTest(string pUserId)
        {

            _userFactory.CreateMailReceiveLog(pUserId, Request.ServerVariables["REMOTE_ADDR"].ToString(), "MailOpen");
            //return View();
        }

        #region GetAppVer : Voice_K 버전 조회(Get)
        [HttpGet]
        public JsonResult GetAppVersion(string pAppName)
        {
            Control AppVersion = _controlFactory.GetAppVersion(pAppName);

            var data = new
            {
                AppVersion = AppVersion.ReturnVal
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetUserInfo : 사우정보 조회(Get)
        [HttpGet]
        public JsonResult GetUserInfo(string pUserNm)
        {
            List<User> dbUser = _userFactory.GetUserInfo(pUserNm);

            var data = dbUser.Select(m => new
            {
                UserNm = m.UserNm,
                TitleNm = m.TitleNm,
                DeptNm = m.DeptNm,
                UserId = m.UserId,
                EmpNo = m.EmpNo,
                CompTel = m.CompTel,
                Mobile = m.Mobile,
                UserPhotoUrl = m.UserPhotoUrl,
                DiligenceStateNm = m.DiligenceStateNm

            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Login : 로그인(Post)
        [HttpPost]
        public JsonResult Login(string pUserStr, string pPwdrStr)
        {
            string statusID = "0";
            string statusMSG = "로그인 성공";

            // 사용자ID 소문자로 변환
            User pUser = new User();

            pUser.UserId = pUserStr.ToLower();
            pUser.Pwd = Util.MakeSHA256HashCode(pPwdrStr);

            // 로그인 사용자정보 확인 
            User dbUser = _userFactory.CheckUserInfo(pUser);

            
            if (dbUser.ReturnVal == "NotRegister")
            {
                statusID = "1";
                statusMSG = "등록되지 않은 계정 입니다.";                
            }

            if (dbUser.ReturnVal == "NotUse")
            {
                statusID = "1";
                statusMSG = "사용이 정지된 계정 입니다.";
            }

            if (dbUser.ReturnVal == "NotPwd")
            {
                statusID = "1";
                statusMSG = "패스워드가 일치하지 않습니다.";
            }

            if (dbUser.ReturnVal == "NotOpen")
            {
                statusID = "1";
                statusMSG = "시스템 오픈 전으로 접속이 불가합니다.";
            }
            
            var data = new
            {
                statusID = statusID,
                statusMSG = statusMSG
            };

            try
            {
                // 로그인 정보 저장
                _userFactory.CreateAndroidLoginLog(pUser.UserId, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Voice_K");

            }
            catch (Exception e)
            {

            }

            return Json(data, JsonRequestBehavior.AllowGet);
            
            
           
        }
        #endregion

        /*
        #region AndroidLogin : 테스트 뷰
        [HttpGet]
        public ActionResult AndroidLogin()
        {         
            return View();
        }
        #endregion

        #region AndroidLogin : 로그인(Post)
        [HttpPost]
        public JsonResult AndroidLogin(string pUserId)
        {

            User dbUser = _userFactory.GetUser(pUserId, "2");

            var data = new
            {
                TitleNm = dbUser.TitleNm,
                DeptNm = dbUser.DeptNm,
                EpUserId = dbUser.EpUserId,

            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        */
    }
}

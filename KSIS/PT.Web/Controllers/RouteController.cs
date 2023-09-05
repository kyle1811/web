using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PT.Web.Controllers
{
    public class RouteController : Controller
    {
        #region Message : 메세지 출력
        [HttpGet]
        public ActionResult Message(string pMessage)
        {
            ViewBag.Message = pMessage;

            return View();
        }
        #endregion

        #region MessageAndClose : 메세지 출력, 현재 페이지 닫기
        [HttpGet]
        public ActionResult MessageAndClose(string pMessage)
        {
            ViewBag.Message = pMessage;

            return View();
        }
        #endregion

        #region MessageAndMove : 메세지 출력, 페이지 이동
        [HttpGet]
        public ActionResult MessageAndMove(string pMessage, string pReturnPage)
        {
            ViewBag.Message = pMessage;
            ViewBag.ReturnPage = pReturnPage;

            return View();
        }
        #endregion

        #region MessageAndCloseAndReloadParent : 메세지 출력, 현재 페이지 닫기, 부모창 리로드
        [HttpGet]
        public ActionResult MessageAndCloseAndReloadParent(string pMessage)
        {
            ViewBag.Message = pMessage;

            return View();
        }
        #endregion

        #region PageNotFound : 페이지를 찾을 수 없습니다.
        public ActionResult PageNotFound()
        {
            return View();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Html.Utils;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class ManageController : BaseController
    {
        SiteFactory _siteFactory = new SiteFactory();

        OrderFactory _orderFactory = new OrderFactory();

        AttFileFactory _attFileFactory = new AttFileFactory();

        WeatherFactory _weatherFactory = new WeatherFactory();

        UserFactory _userFactory = new UserFactory();

        WebUtil.WebUtil _webUtil = new WebUtil.WebUtil();

        #region PLAnalysis : 손익분석
        [HttpGet]
        public ActionResult PLAnalysis()
        {
            //손익분석
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME060000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion


        #region PLAnalysisSC : 손익분석 외주
        [HttpGet]
        public ActionResult PLAnalysisSC()
        {
            //손익분석 외주
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME060100", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion
        
        #region PLAnalysisPR : 손익분석 자재
        [HttpGet]
        public ActionResult PLAnalysisPR()
        {
            //손익분석 자재
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME060200", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region PLAnalysisTerm : 손익분석 기간
        [HttpGet]
        public ActionResult PLAnalysisTerm()
        {
            //손익분석
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME060300", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region PLAnalysisMember : 손익분석 협력업체
        [HttpGet]
        public ActionResult PLAnalysisMember()
        {
            //손익분석
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME060400", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion
        
        #region OrderResult : 수주실적(전체)
        [HttpGet]
        public ActionResult OrderResult()
        {
            //수주실적(전체)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME010000", Session["browser"].ToString(), Session["device"].ToString());
            return View();
        }
        #endregion

        #region OrderReslutDept : 부서별 수주실적
        [HttpGet]
        public ActionResult OrderResultDept()
        {
            //수주실적(부서)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME020000", Session["browser"].ToString(), Session["device"].ToString());
            return View();
        }
        #endregion

        #region OrderState : 신규수주현황
        [HttpGet]
        public ActionResult OrderState(string pOrderStateTypeCd)
        {
            //신규수주현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME030000", Session["browser"].ToString(), Session["device"].ToString());
            
            ViewBag.OrderStateTypeCd = pOrderStateTypeCd;

            return View();
        }
        #endregion

        #region OrderStateEdit : 신규수주실적 작성(GET)
        [HttpGet]
        public ActionResult OrderStateEdit(string pOrderStateTypeCd, string pSaveMode, string pRegNo)
        {
            Order dbOrderState = new Order();

            // 신규작성
            if (pSaveMode == "C")
            {
                dbOrderState.RegNo = "";
                dbOrderState.OrderStateTypeCd = pOrderStateTypeCd;
                dbOrderState.RegUserNm = _UserNm;
                dbOrderState.RegDate = Util.MakeDateTime("YearMonDay");
                dbOrderState.SaveMode = pSaveMode;
            }
            else
            {
                // 회의자료 조회
                dbOrderState = _orderFactory.GetOrderState(pRegNo);
                dbOrderState.SaveMode = pSaveMode;

                if (dbOrderState.RegUserId != _UserId)
                {
                    return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Manage/OrderStateDetail?pOrderStateTypeCd=" + pOrderStateTypeCd + "&pRegNo=" + pRegNo);
                }
            }

            return View(dbOrderState);
        }
        #endregion

        #region OrderStateEdit : 신규수주실적 작성(POST)
        [HttpPost]
        public ActionResult OrderStateEdit(Order pOrderState, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            string regNo = "";

            try
            {
                if (pOrderState.AttId == null)
                {
                    // 첨부파일 신규
                    pOrderState.AttId = _attFileFactory.SaveFiles(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                }
                else
                {
                    // 첨부파일 수정
                    _attFileFactory.SaveFiles(pOrderState.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);
                }

                // 회의자료 저장
                regNo = _orderFactory.SaveOrderState(pOrderState, _UserId);
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Manage/OrderStateEdit?pOrderStateTypeCd=" + pOrderState.OrderStateTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pOrderState.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Manage/OrderState?pOrderStateTypeCd=" + pOrderState.OrderStateTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Manage/OrderStateEdit?pOrderStateTypeCd=" + pOrderState.OrderStateTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region OrderStateDetail : 신규수주실적 상세
        [HttpGet]
        public ActionResult OrderStateDetail(string pOrderStateTypeCd, string pRegNo)
        {
            //신규수주현황(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME030100", Session["browser"].ToString(), Session["device"].ToString());

            // 회의자료 조회
            Order dbOrderState = _orderFactory.GetOrderState(pRegNo);

            return View(dbOrderState);
        }
        #endregion     

        #region SalesPromotions : 분양현황
        [HttpGet]
        public ActionResult SalesPromotions()
        {
            //분양현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME040000", Session["browser"].ToString(), Session["device"].ToString());

            string returnVal = _webUtil.CheckAuthInfo("ME040000", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }

           return View();
        }
        #endregion

        #region CreditGrade : 신용등급현황
        [HttpGet]
        public ActionResult CreditGrade()
        {
            //신용등급현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "ME050000", Session["browser"].ToString(), Session["device"].ToString());

            Site dbSite = _siteFactory.CreditGrade(Util.MakeDateTime("YearMon"));

            Session["TypeNm1"] = dbSite.TypeNm1;
            Session["Grade1"] = dbSite.Grade1;
            Session["CDate1"] = dbSite.CDate1;
            Session["CType1"] = dbSite.CType1;
            Session["Grade2"] = dbSite.Grade2;
            Session["CDate2"] = dbSite.CDate2;
            Session["CType2"] = dbSite.CType2;
            Session["Grade3"] = dbSite.Grade3;
            Session["CDate3"] = dbSite.CDate3;
            Session["CType3"] = dbSite.CType3;

            Session["TypeNm2"] = dbSite.TypeNm2;
            Session["Grade4"] = dbSite.Grade4;
            Session["CDate4"] = dbSite.CDate4;
            Session["CType4"] = dbSite.CType4;
            Session["Grade5"] = dbSite.Grade5;
            Session["CDate5"] = dbSite.CDate5;
            Session["CType5"] = dbSite.CType5;
            Session["Grade6"] = dbSite.Grade6;
            Session["CDate6"] = dbSite.CDate6;
            Session["CType6"] = dbSite.CType6;
            Session["CType6"] = dbSite.CType6;

            Session["StandardDate"] = dbSite.StandardDate;

            return View(dbSite);
        }
        #endregion

        #region SiteWeatherState : 현장정보
        [HttpGet]
        public ActionResult SiteWeatherState()
        {
            Site dbSite = _siteFactory.HolidaySafetyWorkPlan(Util.MakeDateTime("YearMon"));
            Session["WORKINGDAY"] = dbSite.WORKINGDAY;
            return View();
        }
        #endregion

        #region ListChartAccSUJU : 수주실적현황 조회

        [HttpPost]
        public JsonResult ListChartAccSUJU(string pYEARMON)
        {
            // 분양률 조회
            List<Order> dbOrder = _orderFactory.ListChartAccSUJU(pYEARMON);

            var data = dbOrder.Select(m => new
            {
                YearPlan = m.YearPlan,
                BarColor = m.BarColor,
                Gubun = m.Gubun,
                YearPlanAmt = m.YearPlanAmt                
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FinancialInformation : 재무정보 조회
        [HttpGet]
        public ActionResult FinancialInformation()
        {                  
            return View();
        }
        #endregion
    }
}

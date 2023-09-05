using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Microsoft.Web.Html.Utils;
using System.IO;
using System.Text;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class ControlController : BaseController
    {
        UserFactory _userFactory = new UserFactory();
        ControlFactory _controlFactory = new ControlFactory();

        #region Budget : 가용예산
        public ActionResult Budget()
        {
            //가용예산
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "CL010000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region BizCost : 업무추진비 신청현황
        public ActionResult BizCost()
        {
            //업무추진비신청현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "CL030000", Session["browser"].ToString(), Session["device"].ToString());
            return View();
        }
        #endregion

        #region DailyDeposit : 일일입금현황
        public ActionResult DailyDeposit()
        {
            //일일입금현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "CL020000", Session["browser"].ToString(), Session["device"].ToString());
            return View();
        }
        #endregion

        #region DailyDepositRealTime : 일일입금현황 실시간
        public ActionResult DailyDepositRealTime()
        {
            return View();
        }
        #endregion

        #region Cooperator : 협력업체현황 
        public ActionResult Cooperator()
        {
            //협력업체현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN060000", Session["browser"].ToString(), Session["device"].ToString());
            ViewBag.Cooperator = "PT00111";

            return View();
        }
        #endregion

        #region SearchPopUp : 협력업체현황(상세) 팝업
        public ActionResult SearchPopUp()
        {
            return View();
        }
        #endregion

        #region ListSearchVendor : 협력업체현황 그리드

        [HttpPost]
        public ContentResult ListSearchVendor(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pVendornm)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 5] { 
                    { "VendorNm","VendorCd", "VendorKindNm", "RepNm", "BizNo"},
                    { "30%", "10%", "30%","15%","15%" },
                    { "거래처명","코드", "거래처유형", "대표자명", "사업자번호"},
                    { "Show", "Hide", "Show", "Show","Show" },
                    { "Center", "Center", "Center","Center","Center" },
                    { "None", "None", "None", "None", "None" }
            };

            // 회의자료 목록 그리드 조회
            List<Control> dbPopUp = _controlFactory.ListSearchVendor(pVendornm);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pVendornm;

            return Content(data, "text/html");
        }
        #endregion

        #region CooperatorDetail : 협력업체 상세정보 조회
        [HttpGet]
        public ActionResult CooperatorDetail(string pVendorCd)
        {
            //협력업체현황(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN060100", Session["browser"].ToString(), Session["device"].ToString());

            Control dbCooperatorDetail = _controlFactory.GetCooperatorDetail(pVendorCd);

            if (dbCooperatorDetail.VendorNm.ToString() != null)
            {
                return View(dbCooperatorDetail);
            }
            else
            {
                return RouteUtil.MessageAndMove("상세조회 내역이 없습니다.", "/Control/Cooperator");
            }
        }
        #endregion

        #region GetCooperatorClassification : 협력업체 분류 그리드(조회)

        /*[HttpPost]
        public ContentResult GetCooperatorClassification(string pVendorCd)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] { 
                    { "Box", "RegNo", "OrderStateypeCd", "Subject", "RegUserNm", "RegDate" },
                    { "2%", "0%", "10%", "50%", "0%", "25%" },
                    { "", "등록번호", "게시판타입코드", "제목", "작성자", "작성일자" },
                    { "Show", "Hide", "Hide", "Show", "Hide", "Show" },
                    { "Center", "Center", "Center", "Left", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None" }
            };

            // 회의자료 목록 그리드 조회
           // List<PopUp> dbClassification = _controlFactory.GetCooperatorClassification(pVendorCd);

            // 그리드 생성
            //string data = HtmlUtil.MakeGrid(pArrHeader, dbClassification, "N", 0, 0);

            return Content(data, "text/html");
        }*/
        #endregion

        #region ListBizCostGrid : 업무추진비 신청현황 조회

        [HttpPost]
        public ContentResult ListBizCostGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pDeptCd, string pEmpNo)
        {
            
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] { 
                    { "CostSeq","PreYn", "Gubun", "UseDate", "RegUser", "ApproveStatus"},
                    { "30%",     "10%",   "10%",    "15%",     "15%",       "20%" },
                    { "요청번호","사전/사후", "구분", "요청일자", "신청자","신청상태"},
                    { "Show", "Show", "Show", "Show","Show","Show" },
                    { "Center", "Center", "Center","Center","Center","Center" },
                    { "None", "None", "None", "None", "None", "None" }
            };

            // 회의자료 목록 그리드 조회
            List<Control> dbControl = _controlFactory.ListBizCostGrid(pDeptCd, pEmpNo, Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbControl, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region BizCostDetail : 업무추진비 상세정보 조회
        [HttpGet]
        public ActionResult BizCostDetail(string pCostSeq)
        {
            //업무추진비신청현황(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "CL030100", Session["browser"].ToString(), Session["device"].ToString());

            Control dbBizCostDetail = _controlFactory.GetBizCostDetail(pCostSeq);

            if (dbBizCostDetail.CostSeq.ToString() != null)
            {
                dbBizCostDetail.UpdateMode = "";
 
                return View(dbBizCostDetail);
            }
            else
            {    
                return RouteUtil.MessageAndMove("상세조회 내역이 없습니다.", "/Control/BizCost");
            }
        }
        #endregion

        #region BizCostDetail : 업무추진비 승인/반려(POST)
        [HttpPost]
        public ActionResult BizCostDetail(Control pControl)
        {
            string Msg = "";

            try
            {
                    // 승인/반려
                    Msg = _controlFactory.UpdateBizCost(pControl, _EmpNo);
                    return RouteUtil.MessageAndMove(Msg, "/Control/BizCost");
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove("에러가 발생했습니다.", "/Control/BizCost");
            }
        }
        #endregion

        #region KSISUsagestatistics : KSIS 사용 통계
        [HttpGet]
        public ActionResult KSISUsagestatistics()
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "CL040000", Session["browser"].ToString(), Session["device"].ToString());

            string[,] pArrList3 = new string[2, 9] {
                { "ALL", "Manage", "Site", "Person", "Control", "Resource", "Information", "Browser", "Device"},
                { "전체", "경영정보", "현장정보", "인사정보", "관리정보", "자료실", "사우마당", "브라우저", "접속기기"}
            };

            string[,] pArrList1 = new string[2, 6] {
                { "EKP00217","EKP00307","EKP00306","EKP00305","EKP00304","EKP00303"},
                { "임원","부장","차장","과장","대리","사원"}
            };

            string[,] pArrList2 = new string[2, 3] {
                { "YYYY", "MM","DD"},
                { "기간", "월","일"}
            };

            ViewBag.ddlSearchTypeCd1 = Util.ListForDDL(pArrList1, false, "ALL");

            ViewBag.ddlSearchTypeCd2 = Util.ListForDDL(pArrList2, false, "YYYY");

            ViewBag.ddlSearchTypeCd3 = Util.ListForDDL(pArrList3, false, "ALL");

            return View();
        }
        #endregion

        #region ChkTotalStatisticsUser : KSIS 사용통계(사용자 총합구하기)

        [HttpPost]
        public JsonResult ChkTotalStatisticsUser(string pGubun1, string pGubun2, string pFrom, string pTo, string pEPUserId)
        {

            List<Control> dbControl = _controlFactory.ListUserUsageStatisticsForKSIS(pGubun1,pGubun2, pFrom, pTo, pEPUserId);

            var data = dbControl.Select(m => new
            {
                DUTYNM = m.DUTYNM,
                RegCnt = m.RegCnt
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /*#region ChkTotalStatisticsMenu : KSIS 사용통계(메뉴 총합구하기)

        [HttpPost]
        public JsonResult ChkTotalStatisticsMenu(string pGubun1, string pGubun2, string pFrom, string pTo, string pEPUserId)
        {

            List<Control> dbControl = _controlFactory.ListMenuUsageStatisticsForKSIS(pGubun1, pGubun2, pFrom, pTo, pEPUserId);

            var data = dbControl.Select(m => new
            {
                PAGE = m.PAGE,
                PageCnt = m.PageCnt
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion*/

        #region ListUsageCircleChartForKSIS : 전체 사용 비중 차트 DATA 조회

        [HttpPost]
        public JsonResult ListUsageCircleChartForKSIS(string pScreenCd, string pGUBUN, string pSELECT, string pTerm)
        {
            // 분양률 조회
            List<Control> dbControl = _controlFactory.ListUsageCircleChartForKSIS(pScreenCd, pGUBUN, pSELECT, pTerm);

            var data = dbControl.Select(m => new
            {
                CircleScreenNm = m.CircleScreenNm,
                CirclePer = m.CirclePer,
                CircleColor = m.CircleColor,
                CircleValue = m.CircleValue              

            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListUsageBarChartForKSIS : 분기별 사용비중 막대 그래프 생성

        [HttpPost]
        public JsonResult ListUsageBarChartForKSIS(string pScreenCd, string pGUBUN, string pSELECT, string pTerm)
        {
            // 분양률 조회
            List<Control> dbControl = _controlFactory.ListUsageBarChartForKSIS(pScreenCd, pGUBUN, pSELECT, pTerm);

            var data = dbControl.Select(m => new
            {

                BarScreenNm = m.BarScreenNm,
                BarPer = m.BarPer,
                BarMM = m.BarMM,
                BarColor = m.BarColor

            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListUsageStatisticsTableForKSIS : KSIS 분기별 사용 통계 집계표
        [HttpPost]
        public ContentResult ListUsageStatisticsTableForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pScreenCd, string pGUBUN, string pSelect, string pTerm)
        {
            string[,] pArrHeader = new string[6,9];

            if(pSelect == "Browser")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 7] {
                    { "Browser","TbDutyNm", "FirstQuarter", "SecondQuarter", "ThirdQuarter", "FourthQuarter", "Total"},
                    { "40%", "10%", "10%", "10%", "10%", "10%", "10%"},
                    { "브라우저명", "직급", "1분기", "2분기", "3분기", "4분기", "총 계"},
                    { "Show", "Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Right", "Right", "Right", "Right", "Right"},
                    { "None", "None", "None", "None", "None", "None", "None" }
                 };
            }
            else if (pSelect == "Device")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 7] {
                    { "Device","TbDutyNm", "FirstQuarter", "SecondQuarter", "ThirdQuarter", "FourthQuarter", "Total"},
                    { "40%", "10%", "10%", "10%", "10%", "10%", "10%"},
                    { "기기명", "직급", "1분기", "2분기", "3분기", "4분기", "총 계"},
                    { "Show", "Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Right", "Right", "Right", "Right", "Right"},
                    { "None", "None", "None", "None", "None", "None", "None" }
                 };
            }
            else if (pTerm == "ALL" && pScreenCd != "ALL")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                 pArrHeader = new string[6, 9] {
                    { "TbScreenCd","TbScreenNm","TbDutyCd", "TbDutyNm", "FirstQuarter", "SecondQuarter", "ThirdQuarter", "FourthQuarter", "Total"},
                    { "0%", "30%", "0%", "20%", "10%", "10%", "10%", "10%", "10%"},
                    { "화면코드", "화면명", "직급코드", "직급", "1분기", "2분기", "3분기", "4분기", "총 계"},
                    { "Hide", "Show", "Hide", "Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Right", "Right", "Right", "Right", "Right"},
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None" }
                 };

            }
            else if (pTerm == "ALL" && pScreenCd == "ALL")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 9] {
                    { "TbScreenCd","TbScreenNm","ChildSc", "ChildSn", "FirstQuarter", "SecondQuarter", "ThirdQuarter", "FourthQuarter", "Total"},
                    { "0%", "20%", "0%", "20%", "10%", "10%", "10%","10%","20%"},
                    { "구분코드(대)", "구분(대)", "구분코드(중)", "구분(중)", "1분기", "2분기", "3분기", "4분기", "총 계"},
                    { "Hide", "Show", "Hide", "Show", "Show", "Show", "Show","Show","Show"},
                    { "Center", "Center", "Center", "Center", "Right", "Right", "Right","Right","Right"},
                    { "None", "None", "None", "None", "None", "None", "None","None","None" }
                };
            }
            else
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                 pArrHeader = new string[6, 8] {
                    { "TbScreenCd","TbScreenNm","TbDutyCd", "TbDutyNm", "FirstM", "SecondM", "ThirdM", "Quater"},
                    { "0%", "30%", "0%", "20%", "10%", "10%", "10%", "10%"},
                    { "화면코드", "화면명", "직급코드", "직급", "1월", "2월", "3월", "1분기 계"},
                    { "Hide", "Show", "Hide", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Right", "Right", "Right", "Right"},
                    { "None", "None", "None", "None", "None", "None", "None", "None" }
            };

                if (pTerm == "2")
                {
                    pArrHeader[2, 4] = "4월";
                    pArrHeader[2, 5] = "5월";
                    pArrHeader[2, 6] = "6월"; ;
                    pArrHeader[2, 7] = "2분기 계";

                }

                if (pTerm == "3")
                {
                    pArrHeader[2, 4] = "7월";
                    pArrHeader[2, 5] = "8월";
                    pArrHeader[2, 6] = "9월";
                    pArrHeader[2, 7] = "3분기 계";

                }

                if (pTerm == "4")
                {
                    pArrHeader[2, 4] = "10월";
                    pArrHeader[2, 5] = "11월";
                    pArrHeader[2, 6] = "12월";
                    pArrHeader[2, 7] = "4분기 계";

                }
            }

            //조회 
            List<Control> dbOrder = _controlFactory.ListUsageStatisticsTableForKSIS(pScreenCd, pGUBUN, pSelect, pTerm);
            
            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡";

            return Content(data, "text/html");
        }
        #endregion

        #region ListUsageAllMenuTableForKSIS : KSIS 사용 통계 집계표(전체메뉴)
        [HttpPost]
        public ContentResult ListUsageAllMenuTableForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pScreenCd, string pGUBUN, string pSelect, string pTerm)
        {
            string[,] pArrHeader = new string[6, 9];
            
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 11] {
                    { "TbScreenCd","TbScreenNm","ChildSc", "ChildSn", "CChildSc", "CChildSn" ,"FirstQuarter", "SecondQuarter", "ThirdQuarter", "FourthQuarter", "Total"},
                    { "0%", "10%", "0%", "10%", "0%", "30%", "10%", "10%", "10%","10%","10%"},
                    { "구분코드(대)", "구분(대)", "구분코드(중)", "구분(중)","구분코드(소)","구분(소)", "1분기", "2분기", "3분기", "4분기", "총 계"},
                    { "Hide", "Show", "Hide", "Show", "Hide", "Show", "Show", "Show", "Show","Show","Show"},
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Right", "Right", "Right","Right","Right"},
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None","None","None" }
            };


            //조회 
            List<Control> dbOrder = _controlFactory.ListUsageStatisticsTableForKSIS(pScreenCd, pGUBUN, pSelect, pTerm);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡";

            return Content(data, "text/html");
        }
        #endregion

        #region ListUsageBarChartAllForKSIS : 전체 사용비중 막대 그래프 생성

        [HttpPost]
        public JsonResult ListUsageBarChartAllForKSIS(string pScreenCd, string pGUBUN, string pSELECT, string pTerm)
        {
            // 분양률 조회
            List<Control> dbControl = _controlFactory.ListUsageBarChartForKSIS(pScreenCd, pGUBUN, pSELECT, pTerm);

            var data = dbControl.Select(m => new
            {

                ChildScrNm = m.ChildScrNm,
                TotalPercent = m.TotalPercent,
                TotalComma = m.TotalComma,
                AllBarColor = m.AllBarColor

            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListBudgetDetailGrid : 업무추진비 내역 상세조회 테이블

        [HttpPost]
        public ContentResult ListBudgetDetailGrid(string pCostSeq)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "ApproveStatus","TargetYn", "VendorNm", "Item", "Persons", "Amt", "Purpose"},
                    { "15%", "10%", "20%", "5%", "10%", "15%", "25%"},
                    { "상태", "대상구분", "접대처", "품목", "인원", "금액","사용목적"},
                    { "Show", "Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None", "None"}
            };

            // 가용예산 조회 
            List<Control> dbOrder = _controlFactory.ListBudgetDetailGrid((pCostSeq));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, "N", 0, 0);

            return Content(data, "text/html");
        }
        #endregion

    }
}
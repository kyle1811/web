using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Web.Html.Utils;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class PersonController : BaseController
    {
        UserFactory _userFactory = new UserFactory();
        DeptFactory _deptFactory = new DeptFactory();
        CDPFactory  _CDPFactory  = new CDPFactory();
        DiligenceFactory _diligenceFactory = new DiligenceFactory();
        WebUtil.WebUtil _webUtil = new WebUtil.WebUtil();

        #region OrganizeChart : 조직도
        [HttpGet]
        public ActionResult OrganizeChart()
        //public ActionResult OrganizeChart(string pDeptCd)
        {
            //조직도
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN030000", Session["browser"].ToString(), Session["device"].ToString());

            /*if (pDeptCd != null)
            {
                // 부서목록 트리뷰 조회
                List<Dept> dbDept = _deptFactory.ListDeptTreeView(pDeptCd);

                // 트리뷰 생성
                ViewBag.TreeData = HtmlUtil.MakeTreeView(dbDept);

                // 사용자 부서 반환
                ViewBag.DeptCd = pDeptCd;
            }
            else
            {*/
                // 부서목록 트리뷰 조회
                List<Dept> dbDept = _deptFactory.ListDeptTreeView(Session["DeptCd"].ToString());

                // 트리뷰 생성
                ViewBag.TreeData = HtmlUtil.MakeTreeView(dbDept);

                // 사용자 부서 반환
                ViewBag.DeptCd = Session["DeptCd"].ToString();
            //}
            // 검색 목록 생성
            string[,] pArrList = new string[2,8] { 
                { "UserNm", "DeptNm","EmpNo","DutyNm","Email","CompTel","Mobile","JobNm" },
                { "이름", "부서명","사번","직위","메일주소","전화번호","핸드폰","담당업무" }
            };
            ViewBag.ddlSearchTypeCd = Util.ListForDDL(pArrList, false, "UserNm");


            return View();
        }
        #endregion

        #region OrganizeChartDetail : 사용자정보
        [HttpGet]
        public ActionResult OrganizeChartDetail(string pUserId, string pSEQ)
        {
            //조직도(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN030100", Session["browser"].ToString(), Session["device"].ToString());

            // 사용자정보 조회

            User dbUser = _userFactory.GetUser(pUserId, pSEQ);

            if (pSEQ == "1")
            {
                dbUser = _userFactory.UserImgSrc(pSEQ, pUserId);

                string imageBase64Data = Convert.ToBase64String(dbUser.userimgpic);

                if (imageBase64Data != "")
                {
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                    ViewBag.ImageData = imageDataURL;
                }
                else 
                {
                    string imageDataURL = dbUser.COIMG;
                    ViewBag.ImageData = imageDataURL;
                }

                dbUser = _userFactory.GetUser(pUserId, pSEQ);
            }
            else
            {
                dbUser = _userFactory.GetUser(pUserId, pSEQ);

                ViewBag.ImageData = dbUser.UserPhotoUrl;
                ViewBag.DeptCd = dbUser.DeptCd;
            }

            return View(dbUser);
        }
        #endregion

        #region VacationState : 연차사용현황
        [HttpGet]
        public ActionResult VacationState()
        {
            //연차사용현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN040000", Session["browser"].ToString(), Session["device"].ToString());

         return View();
        }
        #endregion

        #region GetOffWorkTime : 본사퇴근현황
        [HttpGet]
        public ActionResult GetOffWorkTime()
        {
            //본사퇴근현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN050000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region CDP : 개인경력사항
        [HttpGet]
        public ActionResult CDP()
        {
            //경력개발(개인경력사항)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN010000", Session["browser"].ToString(), Session["device"].ToString());

            string returnVal = _webUtil.CheckAuthInfo("PN010000", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }
            // 사용자정보 조회
            CDP PSNL = _CDPFactory.GetPSNL(_EmpNo, _UserNm);

            return View(PSNL);
        }

        #endregion

        #region CDPSearch : 조건별 경력사항 조회
        [HttpGet]
        public ActionResult CDPSearch()
        { 
            //경력개발(조건별경력조회)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN020000", Session["browser"].ToString(), Session["device"].ToString());

            string returnVal = _webUtil.CheckAuthInfo("PN020000", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }
            return View();
        }

        #endregion

        #region CDPSearch : 조건별 경력사항 그리드 조회
        [HttpPost]
        public ContentResult CDPSearch(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pEmpNo, string pEmpNm, string pVendorNm, string pBizPartCd, string pConstTypeCdS, string pConsultContractAmt, string pEmpDuty, string pEmpClass, string pSiteNm)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 5] { 
                    { "EMP","EMP_NM", "EMP_CLASS","STATE", "FULL_DEPT_NM"},
                    { "5%", "5%", "5%", "5%", "10%" },
                    { "사번", "성명", "직급", "상태", "부서명" },
                    { "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center" , "Center", "LEFT" },
                    { "None", "None", "None", "None", "None"  }
            };

            // 회의자료 목록 그리드 조회
            List<CDP> dbPopUp = _CDPFactory.ListCDPSearchGrid(pEmpNo, pEmpNm, pVendorNm, pBizPartCd, pConstTypeCdS, pConsultContractAmt, pEmpDuty, pEmpClass, pSiteNm);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pEmpNo + "‡" + pEmpNm;

            return Content(data, "text/html");
        }
        #endregion

        #region ListAppointCareerGrid : 발령기준 경력사항 그리드 조회

        [HttpPost]
        public ContentResult ListAppointCareerGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 11] { 
                    { "APPOINT_NO","APPOINT_DATE","APPOINT_ID","FULL_DEPT_NM_APPOINT","EMP_DUTY","EMP_RANK","WORK_MONTH","ConsultContractAmt","BizPartNm","ConstTypeNm","VendorNm"},
                    { "5%",        "5%",          "5%",        "9%",                  "4%",          "3%",    "6.5%",     "10%",                "3%",       "4%",         "6%" },
                    { "발령번호",  "발령일",      "발령구분",  "발령부서",            "직무",        "직위",    "근무개월", "공사금액",          "부문",     "공사종류",   "발주처" },
                    { "Hide",      "Show",        "Show",      "Show",                "Show" ,       "Show",    "Show",     "Show",              "Show" ,    "Show",       "Show" },
                    { "Center",    "Center",      "Center",    "LEFT",                "Center",      "Center",  "Center",   "Center",            "Center",   "Center",     "Center" },
                    { "None",      "None",        "None",      "None",                "None",        "None",    "None",     "None",              "None",     "None",       "None" }
            };

            // 데이타 조회
            List<CDP> dbPopUp = _CDPFactory.ListAppointCareerGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListCareerGrid : 공사경력사항 그리드 조회

        [HttpPost]
        public ContentResult ListCareerGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] { 
                    //{ "ICOMPANY","ISUBPOST", "DURATION", "ICARMON", "IKIND", "IMONEYDIV", "IORDER", "IRANK"},
                    { "ICOMPANY","ISUBPOST", "DURATION", "ICARMON", "IKIND", "IMONEYDIV", "IORDER"},
                    { "4%",      "9%",       "6%",     "3.5%",    "5%",    "7.5%",        "6.5%"},
                    { "근무회사","근무부서", "근무기간", "근무개월", "공사종류", "공사금액구분", "발주처"},
                    { "Show", "Show", "Show", "Show", "Show" , "Show", "Show"},
                    { "Center", "left", "Center", "Center", "Center","Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 데이타 조회
            List<CDP> dbPopUp = _CDPFactory.ListCareerGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListLicenseGrid : 자격면허 그리드 조회

        [HttpPost]
        public ContentResult ListLicenseGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 3] { 
                    { "LICENSE_ID","LICENSE_LV", "LICENSE_DATE"},
                    { "6%", "5%", "5%" },
                    { "자격증명","등급", "취득일" },
                    { "Show", "Show", "Show" },
                    { "LEFT", "Center", "Center" },
                    { "None", "None", "None"  }
            };

            // 데이타 조회
            List<CDP> dbPopUp = _CDPFactory.ListLicenseGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListGradeGrid : 등급 그리드 조회

        [HttpPost]
        public ContentResult ListGradeGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 2] { 
                    { "ENGGRADE", "PROMDATE"},
                    { "5%", "5%" },
                    { "등급", "취득일" },
                    { "Show", "Show" },
                    { "Center", "Center" },
                    { "None", "None"  }
            };

            // 회의자료 목록 그리드 조회
            List<CDP> dbPopUp = _CDPFactory.ListGradeGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListEduBgrGrid : 학력사항 그리드 조회

        [HttpPost]
        public ContentResult ListEduBgrGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 3] { 
                    { "SCHOOL_NM","MAJOR", "GRADUATION_DATE"},
                    { "5%", "6%", "5%" },
                    { "학교명","전공", "졸업일" },
                    { "Show", "Show", "Show" },
                    { "LEFT", "Center", "Center" },
                    { "None", "None", "None"  }
            };

            // 회의자료 목록 그리드 조회
            List<CDP> dbPopUp = _CDPFactory.ListEduBgrGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListCareerGrid : 입사전 경력사항 그리드 조회

        [HttpPost]
        public ContentResult ListBefCareerGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 12] { 
                    { "ICOMPANY","IHEADDIV", "IMAINPOST", "ISUBPOST", "IPOSTGRP", "IPOSTROW", "IPOSTTYPE", "IPOSTDUTY", "DURATION",	"ICARMON", "ICONSTDIV",	"IKIND"},
                    { "7%",    "3%",     "6%",        "8.7%",       "4%",       "4%",       "4%",        "4%",        "8%",     "3%",  "6%",        "3%" },
                    { "회사명",  "본/현구분", "소속부서", "근무부서", "직군",     "직열",     "직종",      "직무",      "근무기간", "근무개월", "공사구분", "공사종류" },
                    { "Show", "Show", "Show", "Show", "Show" , "Show", "Show", "Show", "Show" , "Show", "Show", "Show" },
                    { "LEFT", "Center", "LEFT", "LEFT", "Center","Center", "Center", "Center" , "Center","Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 데이타 조회
            List<CDP> dbPopUp = _CDPFactory.ListBefCareerGrid(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region ListCareerGrid2 : 입사전 경력사항 그리드 조회2

        [HttpPost]
        public ContentResult ListBefCareerGrid2(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pEmpNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 13] { 
                    { "IMONEYDIV", "ILOCA", "IORDER", "IRANK", "IPOSTGRADE", "ISTRUC", "ISQUARE", "IFLOOR", "IHOUSE", "ILENGTH", "IBRIDGE", "ITUNNEL", "IOTHER"},
                    { "10%",       "3.5%",  "8%",     "4%",    "4%",         "5%",     "5%",      "4%",     "4%",     "4%",      "3%",      "3%",      "3%"},
                    { "금액구분",  "지역",  "발주처", "직위",  "직책",       "구조",   "연면적",  "층수",   "세대수", "총연장", "교량", "터널", "기타" },
                    { "Show", "Show", "Show", "Show" , "Show", "Show", "Show" , "Show", "Show", "Show" , "Show", "Show", "Show" },
                    { "LEFT", "Center", "Center", "Center","Center", "Center", "Center", "Center", "Center", "Center","Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 데이타 조회
            List<CDP> dbPopUp = _CDPFactory.ListBefCareerGrid2(pEmpNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pEmpNo;

            return Content(data, "text/html");
        }
        #endregion

        #region GroupCompany : 그룹사 사우정보
        public ActionResult GroupCompany ()
        {

            ViewBag.Cooperator = "PT00111";

            return View();
        }
        #endregion

        #region ListGroupCompany : 그룹사 사우정보 그리드 조회

        [HttpPost]
        public ContentResult ListGroupCompany(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCooperator, string pUsernm)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] { 
                    { "UserNm","TitleNm", "DeptNm", "EmpNo", "Email", "CompTel", "Mobile"},
                    { "8%", "7%", "20%","10%","25%","15%","15%" },
                    { "이름","직급", "부서", "사번", "E-Mail","회사전화","핸드폰"},
                    { "Show", "Show", "Show", "Show","Show", "Show","Show" },
                    { "Center", "Center", "Center","Center","Center","Center","Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 회의자료 목록 그리드 조회
            List<User> dbPopUp = _userFactory.ListGroupCompany(pUsernm);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pCooperator + "‡" + pUsernm;

            return Content(data, "text/html");
        }
        #endregion

        #region CDPSearchDetail : 개인경력사항
        [HttpGet]
        public ActionResult CDPSearchDetail(string pEmpNo)
        {
            //경력개발(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN020100", Session["browser"].ToString(), Session["device"].ToString());

            // 사용자정보 조회
            CDP PSNL = _CDPFactory.GetPSNL(pEmpNo, "");

            return View(PSNL);
        }

        #endregion

        #region DiligenceState  : 월별연차현황
        [HttpGet]
        public ActionResult DiligenceState()
        {
            //월별연차현황

            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "PN070000", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.ToYearMonDay = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        #endregion

        #region GetDiligenceCalendarAndState : 연차사용현황 및 달력 조회
        [HttpPost]
        public ActionResult GetDiligenceCalendarAndState(string pYearMon)
        {
            List<Diligence> dbCalendar = _diligenceFactory.listDiligenceCalendar(pYearMon);
            List<Diligence> dbState = _diligenceFactory.listDiligenceState(pYearMon, _EmpNo);

            var calendar = dbCalendar.Select(x => new
            {
                Seq = x.Seq,
                YearMonDay = x.YearMonDay,
                Mon = x.Mon,
                Day = x.Tdays,
                YearWeek = x.YearWeek,
                Weekly = x.Weekly,
                HoliDay = x.HoliDay
            });

            var state = dbState.Select(x => new
            {
                Seq = x.Seq,
                DateCnt = x.DateCnt,
                DniDate = x.DniDate,
                DniNm = x.DniNm,
                EmpNo = x.EmpNo,
                EmpNm = x.EmpNm,
                JobPositionCd = x.JobPositionCd,
                JobPositionNm = x.JobPositionNm,
                DeptCd = x.DeptCd
            });

            var datas = new
            {
                calendar = calendar,
                state = state
            };

            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

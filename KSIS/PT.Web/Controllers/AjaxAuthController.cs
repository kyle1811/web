using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.Web.Html.Utils;
using System.IO;
using System.Text;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{

    public class AjaxAuthController : BaseController
    {
        UserFactory _userFactory = new UserFactory();
        SiteFactory _siteFactory = new SiteFactory();
        MeetingFactory _meetingFactory = new MeetingFactory();
        FileConvertFactory _fileConvertFactory = new FileConvertFactory();
        OrderFactory _orderFactory = new OrderFactory();
        ControlFactory _controlFactory = new ControlFactory();
        CommunityFactory _communityFactory = new CommunityFactory();
        AuctionFactory _AuctionFactory = new AuctionFactory();
        WeatherFactory _weatherFactory = new WeatherFactory();
        AsManageFactory _AsManageFactory = new AsManageFactory();

        #region ListSitePhotoUrl : 현장사진 목록 조회

        [HttpPost]
        public JsonResult ListSitePhotoUrl(string pBizPartCd)
        {
            // 현장사진 목록 조회
            List<Site> dbSite = _siteFactory.ListSitePhotoUrl(Util.MakeDateTime("YearMon"), pBizPartCd);

            var data = dbSite.Select(m => new
            {
                RowNum = m.RowNum,
                RowGroup = m.RowGroup,
                BizPartCd = m.BizPartCd,
                SiteCd = m.SiteCd,
                SiteNm = m.SiteNm,
                SitePhotoUrl = m.SitePhotoUrl
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSiteStateRate : 현장현황 목록 조회

        [HttpPost]
        public JsonResult ListSiteStateRate()
        {
            // 현장현황(실행율,공정율,원가율) 목록 조회
            List<Site> dbSite = _siteFactory.ListSiteStateRate(Util.MakeDateTime("YearMon"));

            var data = dbSite.Select(m => new
            {
                SiteRate = m.SiteRate,
                GraphColor = m.GraphColor
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSiteStateRateALL : 현장현황 목록 조회

        [HttpPost]
        public JsonResult ListSiteStateRateALL(String pBizPartCd, String pHome)
        {
            // 현장현황(실행율,공정율,원가율) 목록 조회
            List<Site> dbSite = _siteFactory.ListSiteStateRateALL(Util.MakeDateTime("YearMon"), pBizPartCd, pHome);



            var data = dbSite.Select(m => new
            {
                SiteRate = m.SiteRate,
                GraphColor = m.GraphColor,
                Amout = m.Amout,
                value = m.value,
                FinishDate = m.FinishDate
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListHighRankSiteGrid : 현장(실행율) 상위3개 현장 조회

        [HttpPost]
        public ContentResult ListHighRankSiteGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "ChkBox", "SiteCd", "BizPartNm", "ConstFormNm", "SiteNm", "BudgetRate", "ConstDay" },
                    { "0%", "0%", "15%", "15%", "33%", "10%", "27%" },
                    { "구분", "현장코드", "부문", "공사유형", "현장명", "실행율", "공사기간" },
                    { "Hide", "Hide", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 현장(실행율) 상위3개 현장 조회
            List<Site> dbSite = _siteFactory.ListHighRankSiteGrid(Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListSiteAccidentGrid : 현장(재해현황) 조회

        [HttpPost]
        public ContentResult ListSiteAccidentGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 8] {
                    { "ChkBox", "SiteCd", "BizPartNm", "DisasterCnt", "SiteNm", "AccidentTypeNm", "AccidentBizNm", "AccidentDate" },
                    { "0%", "0%", "15%", "10%", "35%", "10%", "10%", "20%" },
                    { "구분", "현장코드", "부문", "재해건수", "현장명", "재해유형", "재해정도", "재해일자" },
                    { "Hide", "Hide", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 현장(재해현황) 조회
            List<Site> dbSite = _siteFactory.ListSiteAccidentGrid(Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListHolidaySafetyWorkPlan : 휴일작업안전계획 조회

        [HttpPost]
        public ContentResult ListHolidaySafetyWorkPlan(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 8] {
                    { "ChkBox", "BizPartCd", "BizPartNm", "TargetSiteCnt", "FirstDueDateCnt", "SecondDueDateCnt", "OutofDueDateCnt", "OutofCnt" },
                    { "0%", "0%", "5%", "15%", "20%", "20%", "20%", "20%" },
                    { "구분", "부문코드", "부문", "대상현장수", "1차기한(15:00이내)", "2차기한(17:00이내)", "기한초과(17:00)이후", "미작성" },
                    { "Hide", "Hide", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 현장(재해현황) 조회
            List<Site> dbSite = _siteFactory.ListHolidaySafetyWorkPlan(Util.MakeDateTime("YearMon"));

            // 현장별 종목 조회
            Site dbSiteGubun = _siteFactory.getYearMainSitelist(Util.MakeDateTime("YearMon"));

            Session["lbl1_1"] = dbSiteGubun.lbl1_1;
            Session["lbl1_2"] = dbSiteGubun.lbl1_2;
            Session["lbl1_3"] = dbSiteGubun.lbl1_3;
            Session["lbl1_4"] = dbSiteGubun.lbl1_4;
            Session["lbl1_5"] = dbSiteGubun.lbl1_5;
            Session["lbl1_6"] = dbSiteGubun.lbl1_6;
            Session["lbl2_1"] = dbSiteGubun.lbl2_1;
            Session["lbl2_2"] = dbSiteGubun.lbl2_2;
            Session["lbl2_3"] = dbSiteGubun.lbl2_3;
            Session["lbl2_4"] = dbSiteGubun.lbl2_4;
            Session["lbl2_5"] = dbSiteGubun.lbl2_5;
            Session["lbl3_1"] = dbSiteGubun.lbl3_1;
            Session["lbl3_2"] = dbSiteGubun.lbl3_2;
            Session["lbl3_3"] = dbSiteGubun.lbl3_3;
            Session["lbl4_1"] = dbSiteGubun.lbl4_1;
            Session["lbl4_2"] = dbSiteGubun.lbl4_2;
            Session["lbl4_3"] = dbSiteGubun.lbl4_3;
            Session["lbl4_4"] = dbSiteGubun.lbl4_4;
            Session["lbl5_1"] = dbSiteGubun.lbl5_1;
            Session["lbl5_2"] = dbSiteGubun.lbl5_2;


            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region GetWeatherInfo : 기상청 기상정보 조회

        [HttpPost]
        public JsonResult GetWeatherInfo(string pLatticeX, string pLatticeY)
        {
            string baseDate = DateTime.Now.ToString("yyyyMMdd"); // 기준일자
            string baseHour = DateTime.Now.ToString("HH"); // 현재시간
            string basePrevHour = DateTime.Now.AddHours(-1).ToString("HH"); // 현재시간 - 1
            string baseMinute = DateTime.Now.ToString("mm"); // 현재분
            string baseTime = "";   // 기준시간

            // 기준시간 구하기
            if (int.Parse(baseMinute) <= 30)
            {
                baseTime = basePrevHour + "00";
            }
            else
            {
                baseTime = baseHour + "00";
            }

            // 웹요청(GET방식) 호출
            var js = JObject.Parse(HtmlUtil.GetHttpGetWebRequest("http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastGrib?ServiceKey=D3GfW0hJurkv0UXhVebbZETHv0FgnQB4Fg9j2pnAGwTxhg4uefbr97MeHe6zuaZOo9oOwn2K2AFB3MYKqG0%2Fug%3D%3D&base_date=" + baseDate + "&base_time=" + baseTime + "&nx=" + pLatticeX + "&ny=" + pLatticeY + "&pageNo=1&numOfRows=10&_type=json"));

            // json 데이터 조회
            string sky = js["response"]["body"]["items"]["item"][4]["obsrValue"].ToString(); // 하늘상태 : 맑음(1), 구름조금(2), 구름많음(3), 흐림(4)
            string pty = js["response"]["body"]["items"]["item"][1]["obsrValue"].ToString(); // 강수형태 : 없음(0), 비(1), 비/눈(2), 눈(3)
            string lgt = js["response"]["body"]["items"]["item"][0]["obsrValue"].ToString(); // 낙뢰 : 없음(0), 있음(1)
            string t1h = js["response"]["body"]["items"]["item"][5]["obsrValue"].ToString() + "℃"; // 기온 : .C
            string reh = js["response"]["body"]["items"]["item"][2]["obsrValue"].ToString() + "%"; // 습도 : %
            string rn1 = js["response"]["body"]["items"]["item"][3]["obsrValue"].ToString() + "mm"; // 1시간강수량 : mm

            // json 데이터 생성
            var data = new
            {
                sky = sky,
                pty = pty,
                lgt = lgt,
                t1h = t1h,
                reh = reh,
                rn1 = rn1,
                time = Util.MakeDateTime("YearMonDay") + " " + baseTime.Substring(0, 2) + ":30"
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSiteStateRateDetail : 현장별 공사진행현황 조회

        [HttpPost]
        public JsonResult ListSiteStateRateDetail(string pSiteCd, String pHome)
        {
            // 현장별(실행율,공정율,원가율) 목록 조회
            List<Site> dbSite = _siteFactory.ListSiteStateRateDetail(Util.MakeDateTime("YearMon"), pSiteCd, pHome);


            var data = dbSite.Select(m => new
            {
                SiteRate = m.SiteRate,
                GraphColor = m.GraphColor,
                Amout = m.Amout,
                value = m.value,
                FinishDate = m.FinishDate
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSitePhotoDetailUrl : 현장상세 사진 목록 조회

        [HttpPost]
        public JsonResult ListSitePhotoDetailUrl(string pSiteCd, string pNumber)
        {
            // 현장사진 목록 조회
            List<Site> dbSite = _siteFactory.ListSitePhotoDetailUrl(pSiteCd, pNumber);

            var data = dbSite.Select(m => new
            {
                SitePhotoUrl = m.SitePhotoUrl,
                SiteBizpartCd = m.SiteBizpartCd,
                Number = m.Number,
                RegDate = m.RegDate
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListConstructionSiteWorkersInfo : 현장인원 현황 그리드 조회

        [HttpPost]
        public ContentResult ListConstructionSiteWorkersInfo(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSiteCd)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 5] { 
                    //{ "ICOMPANY","ISUBPOST", "DURATION", "ICARMON", "IKIND", "IMONEYDIV", "IORDER", "IRANK"},
                    {"EPUserId", "UserNm","TitleNm", "DeptNm", "UserMobileNo"},
                    {"0%", "20%", "20%", "35%", "25%"},
                    {"계정", "성명","직급", "부서", "연락처"},
                    {"Hide", "Show", "Show", "Show", "Show"},
                    {"Center", "Center", "Center","Center", "Center" },
                    {"None", "None", "None", "None", "None" }
            };

            // 데이타 조회
            List<Site> dbPopUp = _siteFactory.ListConstructionSiteWorkersInfo(pSiteCd);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pSiteCd;

            return Content(data, "text/html");
        }
        #endregion


        #region ListMeetingGrid : 회의자료 목록 그리드 조회

        [HttpPost]
        public ContentResult ListMeetingGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pMeetTypeCd, string pSubject, string pUserId)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "AttId", "RegNo", "MeetTypeCd", "Subject", "RegUserNm", "RegDate", "AttFileYN" },
                    { "0%", "0%", "0%", "65%", "10%", "15%", "10" },
                    { "첨부파일ID", "등록번호", "게시판타입코드", "제목", "작성자", "작성일자", "첨부파일" },
                    { "Hide", "Hide", "Hide", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Left", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 회의자료 목록 그리드 조회
            List<Meeting> dbMeeting = _meetingFactory.ListMeetingGrid(pMeetTypeCd, pSubject, pUserId);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbMeeting, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pMeetTypeCd + "‡" + pSubject;

            return Content(data, "text/html");
        }
        #endregion

        #region ListUserGrid : 사용자목록 그리드 조회

        [HttpPost]
        public ContentResult ListUserGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pTreeYn, string pDeptCd, string pSearchTypeCd, string pSearchWord)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 9] {
                    { "ChkBox", "SEQ", "UserId", "UserNm", "DutyNm", "CorpName", "DeptNm", "CompTel", "Mobile" },
                    { "0%", "0%", "0%", "10%", "13%", "10", "27%", "20%", "20%" },
                    { "구분", "순번", "사용자ID", "이름", "직급", "회사명", "부서", "전화번호", "휴대폰"},
                    { "Hide", "Hide", "Hide", "Show", "Show","Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center","Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None","None", "None", "None"}
            };

            // 사용자목록 그리드 조회
            List<User> dbUser = _userFactory.ListUserGrid(pTreeYn, pDeptCd, pSearchTypeCd, pSearchWord);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbUser, pPagerUseYn, pCurPage, pListCnt);

            // 검색조건 반환
            data = data + "‡" + pTreeYn + "‡" + pDeptCd + "‡" + pSearchTypeCd + "‡" + pSearchWord;

            return Content(data, "text/html");
        }
        #endregion

        #region GetSnyNapViewer : 파일변환 Url 조회

        [HttpPost]
        public ContentResult GetSnyNapViewer(string pAttId, string pConfirm)
        {
            // 파일변환 Url값 변수
            string data = "";
            string sysnapUrl = "";

            if (_Server == "REAL")
            {
                sysnapUrl = "https://viewer.kccworld.net:8443";
            }
            else
            {
                sysnapUrl = "https://viewerdev.ekpdev.kccworld.net:8443";
            }

            string[] arr = pAttId.Split('/');
            FileConvert dbFileConvert = new FileConvert();

            try
            {
                // 파일변환 변환번호 및 경로 조회
                dbFileConvert = _fileConvertFactory.GetFileConvertInfo(arr[0], arr[1], _UserId);

                if (pConfirm == "Y")
                {
                    return Content(dbFileConvert.FileUrl, "text/html");
                }

                // 파일 확장자 구하기
                string fileExtension = Path.GetExtension(dbFileConvert.FileUrl);
                string convertType = "";

                // 파일 확장자별 변환모드 설정
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".bmp" || fileExtension == ".tif")
                {
                    convertType = "0";
                }
                else if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".ppt" || fileExtension == ".pptx" || fileExtension == ".txt" || fileExtension == ".pdf")
                {
                    convertType = "1";
                }
                else
                {
                    return Content("NotConvertContent", "text/html");
                }

                // 매개변수
                string[] arrParam = new string[4];
                arrParam[0] = dbFileConvert.RegNo;
                arrParam[1] = convertType;
                arrParam[2] = Uri.EscapeDataString(dbFileConvert.FileUrl);
                //arrParam[2] = Uri.EscapeDataString(dbFileConvert.FileUrl).Replace("%20", "%2520");
                arrParam[3] = "UTF-8";

                // 웹요청(POST방식) 호출
                var js = JObject.Parse(HtmlUtil.PostHttpGetWebRequest(sysnapUrl + "/SynapDocViewServer/jobJson", "fid=" + arrParam[0] + "&convertType=" + arrParam[1] + "&fileType=URL&filePath=" + arrParam[2] + "&urlEncoding=" + arrParam[3]));

                // 호출 데이터 생성
                data = sysnapUrl + "/SynapDocViewServer/view/" + js["key"].ToString();

                return Content(data, "text/html");
            }
            catch
            {
                return Content("Error", "text/html");
            }
        }
        #endregion

        #region ListOrderResultAll : 수주실적(전체) 조회

        [HttpPost]
        public JsonResult ListOrderResultAll(string pDate)
        {
            if (pDate == "")
            {
                pDate = Util.MakeDateTime("YearMon");
            }
            // 수주실적(전체) 조회
            List<Order> dbOrder = _orderFactory.ListOrderResultAll(pDate);

            var data = dbOrder.Select(m => new
            {
                Rate = m.MonthRate,
                PlanFixAmt = m.PlanFixAmt,
                RsltFixAmt = m.RsltFixAmt,
                RsltFixAmtComma = m.RsltFixAmtComma,
                FiscalMonth = m.FiscalMonth
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListOrderResultAllGrid : 수주실적(전체) 조회 테이블

        [HttpPost]
        public ContentResult ListOrderResultAllGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pDate)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 8] {
                    { "BizPartCd", "CdNm", "DeptCd", "DeptNm","YearConstAmt", "RsltAmt", "AccRsltAmt", "YearRate" },
                    { "0%", "0%", "0%", "20%", "20%", "20%", "20%", "20%" },
                    { "부문코드", "부문", "부서코드", "부서", "연간계획", "당월실적", "누계실적", "연간달성율(%)" },
                    { "Hide", "Hide", "Hide", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Right", "Right", "Right", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None" }
         };

            if (pDate == "")
            {
                pDate = Util.MakeDateTime("YearMon");
            }

            // 수주실적(전체) 조회
            List<Order> dbOrder = _orderFactory.ListOrderResultAllGrid(pDate);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion       


        #region ListOrderResultAllGridHome : 수주실적(전체) 홈 조회 테이블

        [HttpPost]
        public ContentResult ListOrderResultAllGridHome(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 13] {
                    { "SEQ", "BizPartCd", "CdNm", "DeptCd", "DeptNm","YearConstAmt", "ConstAmt", "RsltAmt","Rate","AccConstAmt","AccRsltAmt","AccRate", "YearRate" },
                    { "0%", "0%", "0%", "0%", "15%", "12.5%", "12.5%", "12.5%", "11%", "12.5%", "12.5%", "11%", "0%" },
                    { "순번", "부문코드", "부문", "부서코드", "부서", "연간계획","당월계획", "당월실적","당월(%)", "누계계획", "누계실적", "누계(%)", "연간(%)" },
                    { "Hide", "Hide", "Hide", "Hide", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Hide" },
                    { "Center", "Center", "Center", "Center", "Center", "Right", "Right", "Right", "Center", "Right", "Right", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 수주실적(전체) 조회
            List<Order> dbOrder = _orderFactory.ListOrderResultAllGridHome(Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListOrderResultAllGrid : 수주실적(부서) 조회 테이블

        [HttpPost]
        public ContentResult ListOrderResultDeptGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "ChargeDeptCd", "DeptNm", "ProjectId", "ProjectNm", "ReceiveOrderFixDay", "RsltAmt", "ShareRate" },
                    { "10%", "10%", "15%", "35%", "10%", "10%", "10%" },
                    { "담당부서코드", "부서", "프로젝트코드", "공사명", "수주년월", "수주금액", "당사지분율" },
                    { "Hide", "Hide", "Hide", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Right", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 수주실적(전체) 조회
            List<Order> dbOrder = _orderFactory.ListOrderResultDeptGrid(Util.MakeDateTime("YearMon"), pCnt);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListWeatherMainGrid : 현장 현재 날씨 조회(부문별)

        [HttpPost]
        public ContentResult ListWeatherMainGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pBizPartCd, string pGubun)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 9] {
                    { "SiteCd", "SiteNm", "WeatherTime", "WeatherCode", "Temp", "PM100", "WindSpeed", "TimeRainfall", "DayRainfallAcct" },
                    { "0%", "35%", "10%", "10%", "11%", "11%", "11%" ,"11%", "11%" },
                    { "현장코드", "현장명", "시간", "날씨", "기온(℃)", "미세먼지", "풍속(m/s)","강우량 (mm/시간)","강우량 (mm/일일)" },
                    { "Hide", "Show", "Hide", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 현장 현재 날씨 조회(부문별)
            List<Order> dbOrder = _siteFactory.ListWeatherMainGrid(Util.MakeDateTime("YearMon"), pBizPartCd, pGubun);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion




        #region ListSiteWeatherThreeDetail : 현장날씨 목록 조회(3일 3시간)
        [HttpPost]
        public ContentResult ListSiteWeatherThreeDetail(string pSiteCd)
        {
            // 현장날씨 목록 조회(3일 3시간)
            List<Weather> dbWeather = _weatherFactory.ListSiteWeatherThreeDetail(pSiteCd);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:3.8%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col style=""width:3.7%"" />");
            sb.AppendLine(@"<col />");
            sb.AppendLine(@"</colgroup>");
            sb.AppendLine(@"<tbody>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">날짜</th>");
            sb.AppendLine(@"<th colspan=""8"">오늘 " + DateTime.Today.ToString("MM") + " / " + DateTime.Today.ToString("dd") + " " + DateTime.Today.ToString("(dddd)") + "</th>");
            sb.AppendLine(@"<th colspan=""8"">내일 " + DateTime.Today.AddDays(1).ToString("MM") + " / " + DateTime.Today.AddDays(1).ToString("dd") + " " + DateTime.Today.AddDays(1).ToString("(dddd)") + "</th>");
            sb.AppendLine(@"<th colspan=""8"">모레 " + DateTime.Today.AddDays(2).ToString("MM") + " / " + DateTime.Today.AddDays(2).ToString("dd") + " " + DateTime.Today.AddDays(2).ToString("(dddd)") + "</th>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">시간</th>");
            sb.AppendLine(@"<td class=""td_center"">00</td>");
            sb.AppendLine(@"<td class=""td_center"">03</td>");
            sb.AppendLine(@"<td class=""td_center"">06</td>");
            sb.AppendLine(@"<td class=""td_center"">09</td>");
            sb.AppendLine(@"<td class=""td_center"">12</td>");
            sb.AppendLine(@"<td class=""td_center"">15</td>");
            sb.AppendLine(@"<td class=""td_center"">18</td>");
            sb.AppendLine(@"<td class=""td_center"">21</td>");
            sb.AppendLine(@"<td class=""td_center"">00</td>");
            sb.AppendLine(@"<td class=""td_center"">03</td>");
            sb.AppendLine(@"<td class=""td_center"">06</td>");
            sb.AppendLine(@"<td class=""td_center"">09</td>");
            sb.AppendLine(@"<td class=""td_center"">12</td>");
            sb.AppendLine(@"<td class=""td_center"">15</td>");
            sb.AppendLine(@"<td class=""td_center"">18</td>");
            sb.AppendLine(@"<td class=""td_center"">21</td>");
            sb.AppendLine(@"<td class=""td_center"">00</td>");
            sb.AppendLine(@"<td class=""td_center"">03</td>");
            sb.AppendLine(@"<td class=""td_center"">06</td>");
            sb.AppendLine(@"<td class=""td_center"">09</td>");
            sb.AppendLine(@"<td class=""td_center"">12</td>");
            sb.AppendLine(@"<td class=""td_center"">15</td>");
            sb.AppendLine(@"<td class=""td_center"">18</td>");
            sb.AppendLine(@"<td class=""td_center"">21</td>");
            sb.AppendLine(@"</tr>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">날씨</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center""><img src='/Contents/images/icon/" + dbWeather[i].WeatherCode + ".gif' style='width:95%;height:95%;' /></td>");
            }
            sb.AppendLine(@"</tr>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">기온</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"" style = ""font-size:13px;"">" + dbWeather[i].Temp + "</td>");
            }
            sb.AppendLine(@"</tr>");


            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">최저기온(℃)</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"" style = ""font-size:13px;"">" + dbWeather[i].MinTemp + "</td>");
            }
            sb.AppendLine(@"</tr>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">최고기온(℃)</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"" style = ""font-size:13px;"">" + dbWeather[i].MaxTemp + "</td>");
            }
            sb.AppendLine(@"</tr>");


            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">강수량(mm)</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"">" + dbWeather[i].Rainfall + "</td>");
            }
            sb.AppendLine(@"</tr>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">적설량(cm)</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"" style = ""font-size:13px;"">" + dbWeather[i].Snow + "</td>");
            }
            sb.AppendLine(@"</tr>");


            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">강수확률(%)</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"">" + dbWeather[i].RainfallPro + "</td>");
            }
            sb.AppendLine(@"</tr>");

            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan=""3"">미세먼지</th>");

            for (int i = 0; i < dbWeather.Count(); i++)
            {
                sb.AppendLine(@"<td class=""td_center"">" + dbWeather[i].PM100 + "</td>");
            }
            sb.AppendLine(@"</tr>");


            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region ListWeatherDetailWeekGrid : 현장 현재 날씨 조회(부문별)

        [HttpPost]
        public ContentResult ListWeatherDetailWeekGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSiteCd)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 5] {
                    { "WeekDate", "WeatherCode", "MinTemp", "MaxTemp", "RainfallPro"},
                    { "20%", "20%", "20%", "20%", "20%"},
                    { "날짜", "날씨", "최저기온", "최고기온", "강수확률" },
                    { "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None" }
            };

            // 현장 현재 날씨 조회(부문별)
            List<Order> dbOrder = _siteFactory.ListWeatherDetailWeekGrid(pSiteCd);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListOrderStateGrid : 신규수주현황 목록 그리드(조회)

        [HttpPost]
        public ContentResult ListOrderStateGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pOrderStateTypeCd, string pSubject)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "Box", "RegNo", "OrderStateypeCd", "Subject", "GridFixAmt", "RegUserNm", "RegDate" },
                    { "2%", "0%", "10%", "48%", "25%", "0%", "15%" },
                    { "", "등록번호", "게시판타입코드", "공사명", "공사금액", "작성자", "수주년월" },
                    { "Show", "Hide", "Hide", "Show", "Show", "Hide", "Show" },
                    { "Center", "Center", "Center", "Left", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            List<Order> dbOrderState = _orderFactory.ListOrderStateGrid(pOrderStateTypeCd, pSubject, "");

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrderState, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pOrderStateTypeCd + "‡" + pSubject;

            return Content(data, "text/html");
        }
        #endregion

        #region ListGetRegNo : 게시물 번호 가지고 오기

        [HttpPost]
        public JsonResult ListGetRegNo(string pOrderStateTypeCd, string pSubject, string pProjectId)
        {
            List<Order> dbOrderState = _orderFactory.ListGetRegNo(pOrderStateTypeCd, pSubject, pProjectId);

            var data = dbOrderState.Select(m => new
            {
                RegNo = m.RegNo,
                Subject = m.Subject,
                CNT  = m.CNT,
                ProjectFullNm  = m.ProjectFullNm,
                VendorNm = m.VendorNm,
                FixAmt  = m.FixAmt
           
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListControlBudgetGrid : 가용예산 조회 테이블

        [HttpPost]
        public ContentResult ListControlBudgetGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pDeptCd)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] {
                    { "chk","Acctcd", "AcctNm", "MonAmt", "MonRsltAmt", "RemainMonAmt"},
                    { "10%", "15%", "30%", "15%", "15%", "15%"},
                    { "체크", "계정코드", "계정명", "당월예산", "당월실적", "가용예산"},
                    { "Hide", "Hide", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Right", "Right", "Right"},
                    { "None", "None", "None", "None", "None", "None"}
            };

            // 가용예산 조회 
            List<Control> dbOrder = _controlFactory.ListControlBudgetGrid((pDeptCd));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListControlDailyDepositGrid : 일일입금현황 조회 테이블

        [HttpPost]
        public ContentResult ListControlDailyDepositGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pDatePick1, string pDatePick2)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 9] {
                    { "TRNX_DATE","JOURNAL_NUM", "AMOUNT", "IN_BRANCH", "BANK_ACCOUNT_NUM", "BANK_BRANCH_NAME","TRX_DESC","AMOUNT_UNAPPLIED","DEPT_DESC"},
                    { "10%", "20%", "13%", "10%", "12%", "0%","15%","10%","10%"},
                    { "일자", "전표번호", "입금액", "송금은행", "입금계좌번호", "거래지점","통장표시내용","미반제","반제부서"},
                    { "Show", "Show", "Show", "Show", "Show", "hide","Show","Show","Show"},
                    { "Center", "Center", "Right", "Center", "Center", "Center","Center","Right","Center"},
                    { "None", "None", "None", "None", "None", "None","None","None","None"}
            };

            // 가용예산 조회 
            List<Control> dbOrder = _controlFactory.ListControlDailyDepositGrid(pDatePick1, pDatePick2);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ##경매##

        #region GetOnProgress : 진행중인경매 그리드 조회

        [HttpPost]
        public ContentResult GetOnProgress(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pAuctionTypeCd, string pSubject)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 8] {
                    { "AuctionNo", "ItemNm", "CurPrice", "RegDate", "CloseDate", "BidCnt", "ImageFile1",   "CommentCnt" },
                    { "17%", "36%", "15%", "15%", "15%", "7%", "0%", "0%" },
                    { "경매번호", "물품명", "현재가", "시작일시", "마감일시", "입찰수", "이미지",  "댓글"},
                    { "Show", "Show", "Hide", "Show", "Show", "Show", "Hide", "Hide" },
                    { "Left", "Left", "Right", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 진행중인경매 목록 그리드 조회
            List<Auction> dbGetOnProgress = _AuctionFactory.GetOnProgress(pSubject);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbGetOnProgress, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pAuctionTypeCd + "‡" + pSubject;

            return Content(data, "text/html");
        }
        #endregion

        #region GetComment : 댓글 조회

        [HttpPost]
        public ContentResult GetComment(string pAuctionTypeCd, string pAuctionNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 4] {
                    { "CommentSeq", "CommentRegUserID", "CommentText", "CommentRegDate" },
                    { "0%", "15%", "65%", "20%" },
                    { "댓글번호", "작성자", "댓글", "작성일자" },
                    { "Hide", "Show", "Show", "Show" },
                    { "Center", "Center", "Left", "Center", },
                    { "None", "None", "None", "None" }
            };

            // 댓글 목록 그리드 조회
            List<Auction> GetComment = _AuctionFactory.GetComment(pAuctionNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, GetComment, "N", 0, 0);

            data = data + "‡" + pAuctionTypeCd + "‡" + pAuctionNo;

            return Content(data, "text/html");
        }
        #endregion

        #region GetCurPrice : 현재최고가
        [HttpPost]
        public ContentResult GetCurPrice(string pAuctionNo)
        {

            // 현재최고가 조회
            Auction dbGetCurPrice = _AuctionFactory.GetCurPrice(pAuctionNo);

            string data = dbGetCurPrice.CurPrice + "‡" + dbGetCurPrice.BidCnt;

            return Content(data, "text/html");
        }
        #endregion

        #region GetAuctionBiddingInfo : 참여한경매 조회

        [HttpPost]
        public ContentResult GetAuctionBiddingInfo(string pPagerUseYn, string pUserId, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            /*
            string[,] pArrHeader = new string[6, 7] {
                    { "AuctionNo", "ItemNm", "MyBidPrice", "CurPrice", "WinningBidPrice", "SellRegUserNm", "CloseDate" },
                    { "0%", "30%", "15%", "15%", "15%", "10%", "15%" },
                    { "경매번호", "물품명", "나의경매가", "투찰시간", "낙찰가", "판매자", "입찰마감" },
                    { "Hide", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Left", "Left", "Right", "Center", "Right", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };
            */
            string[,] pArrHeader = new string[6, 7] {
                    { "AuctionNo", "ItemNm", "MyBidPrice", "CurPrice", "SellRegUserNm", "CloseDate", "WinningBidPrice" },
                    { "18%", "28%", "15%", "15%",  "10%", "15%", "10%" },
                    { "경매번호", "물품명", "나의경매가", "투찰시간", "판매자", "입찰마감", "낙찰여부" },
                    { "Show", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Left", "Left", "Right", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
             };

            // 참여한 경매 그리드 조회
            List<Auction> dbGetAuctionBiddingInfo = _AuctionFactory.GetAuctionBiddingInfo(pUserId);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbGetAuctionBiddingInfo, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pUserId;

            return Content(data, "text/html");
        }
        #endregion

        #region GetAuctionSellingInfo : 경매관리자 조회

        [HttpPost]
        public ContentResult GetAuctionSellingInfo(string pPagerUseYn, string pUserId, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "AuctionNo", "ItemNm", "WinningBidPrice", "BidCnt", "BuyerNm", "CloseDate", "AuctionStatus" },
                    { "16%", "27%", "15%", "9%", "9%", "15%", "10" },
                    { "경매번호", "물품명", "낙찰가", "입찰수", "구매자", "마감일", "상태" },
                    { "Show", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Left", "Left", "Right", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None" }
            };

            // 참여한 경매 그리드 조회
            List<Auction> dbGetAuctionSellingInfo = _AuctionFactory.GetAuctionSellingInfo(pUserId);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbGetAuctionSellingInfo, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pUserId;

            return Content(data, "text/html");
        }
        #endregion

        #region ListBidDetailPopUP : 입찰내역(경매기록) 조회

        [HttpPost]
        public ContentResult ListBidDetailPopUP(string pAuctionNo, string pUserId, string pListType, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 3] {
                    { "BidderID", "BidDate",  "BidPrice"},
                    { "20%",      "50%",      "30%" },
                    { "입찰자ID", "입찰일시", "입찰가" },
                    { "Show",     "Show", "Show" },
                    { "Center", "Center", "Right", },
                    { "None", "None", "None" }
            };

            // 댓글 목록 그리드 조회
            List<Auction> dbListBidDetailPopUP = _AuctionFactory.ListBidDetailPopUP(pAuctionNo, pUserId, pListType);
            
            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbListBidDetailPopUP, "Y", pCurPage, pListCnt);

            data = data + "‡" + pAuctionNo;

            return Content(data, "text/html");
        }
        #endregion

        #endregion

        #region ListSalesMoveInRate : 분양율 차트 조회

        [HttpPost]
        public JsonResult ListSalesRate()
        {
            // 분양률 조회
            List<Site> dbSite = _siteFactory.ListSalesRate(Util.MakeDateTime("YearMon"));

            var data = dbSite.Select(m => new
            {
                Mon = m.Mon,
                UNSALERATE = m.UNSALERATE,
                Sales = m.Sales,
                SalesColor = m.SalesColor,
                slice = m.slice,
                ViewSale = m.ViewSale

            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion        

        #region ListMoveInRate : 입주율 차트 조회

        [HttpPost]
        public JsonResult ListMoveInRate()
        {
            // 분양률 조회
            List<Site> dbSite = _siteFactory.ListMoveInRate(Util.MakeDateTime("YearMon"));

            var data = dbSite.Select(m => new
            {
                Mon = m.Mon,
                UNMOVERATE = m.UNMOVERATE,
                Movein = m.Movein,
                MoveinColor = m.MoveinColor,
                slice = m.slice,
                ViewMove = m.ViewMove
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSalesRateGrid : 분양현황 테이블 - NEW
        [HttpPost]
        public ContentResult ListSalesRateGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
           

            List<Site> dbSite = _siteFactory.ListSalesRateGrid(Util.MakeDateTime("YearMon"));

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:50%"" />");
            sb.AppendLine(@"<col style=""width:50%"" />");
            sb.AppendLine(@"<col />");
            sb.AppendLine(@"</colgroup>");
            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>총 세대수</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].TOTCNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>분양</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[1].UNSALECNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>미분양</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].UNSALECNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region ListMoveinRateGrid : 입주현황 테이블 - NEW

        [HttpPost]
        public ContentResult ListMoveinRateGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {            
            List<Site> dbSite = _siteFactory.ListMoveinRateGrid(Util.MakeDateTime("YearMon"));

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:50%"" />");
            sb.AppendLine(@"<col style=""width:50%"" />");
            sb.AppendLine(@"<col />");
            sb.AppendLine(@"</colgroup>");
            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>준공 세대수</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].TOTSUPCNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>입주</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[1].UNMOVEINCNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>미입주</th>");
            sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].UNMOVEINCNT + "</td>");
            sb.AppendLine(@"</tr>");
            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        //#region ListSalesRateGrid : 분양현황 테이블
        //[HttpPost]
        //public ContentResult ListSalesRateGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        //{
        //    // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
        //    string[,] pArrHeader = new string[6, 4] {
        //            { "Sales", "TOTCNT", "UNSALECNT", "UNSALERATE"},
        //            { "15%", "30%", "30%", "25%"},
        //            { "구분", "총 세대수", "현황(누계)", "(%)"},
        //            { "Show", "Show", "Show", "Show"},
        //            { "Center", "Center", "Center", "Center"},
        //            { "None", "None", "None", "None"}
        //    };

        //    List<Site> dbSite = _siteFactory.ListSalesRateGrid(Util.MakeDateTime("YearMon"));

        //    // 그리드 생성
        //    string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

        //    return Content(data, "text/html");
        //}
        //#endregion

        //#region ListMoveinRateGrid : 입주현황 테이블

        //[HttpPost]
        //public ContentResult ListMoveinRateGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        //{
        //    // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
        //    string[,] pArrHeader = new string[6, 4] {
        //            { "Movein", "TOTSUPCNT", "UNMOVEINCNT", "UNMOVERATE"},
        //            { "15%", "30%", "30%", "25%"},
        //            { "구분", "총 세대수", "현황(누계)", "(%)"},
        //            { "Show", "Show", "Show", "Show"},
        //            { "Center", "Center", "Center", "Center"},
        //            { "None", "None", "None", "None"}
        //    };

        //    // 현장(재해현황) 조회
        //    List<Site> dbSite = _siteFactory.ListMoveinRateGrid(Util.MakeDateTime("YearMon"));

        //    // 그리드 생성
        //    string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

        //    return Content(data, "text/html");
        //}
        //#endregion

        #region ListSalesMoveInState : 월별 분양 입주현황 조회

        [HttpPost]
        public JsonResult ListSalesMoveInState(string pGubun)
        {
            // 분양률 조회
            List<Site> dbSite = _siteFactory.ListSalesMoveInState(Util.MakeDateTime("YearMon"), pGubun);

            var data = dbSite.Select(m => new
            {
                MonSaleCnt = m.MonSaleCnt,
                MonMoveinCnt = m.MonMoveinCnt
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSalesMoveinGrid : 분양입주현황 상세조회 테이블

        [HttpPost]
        public ContentResult ListSalesMoveinGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 12] {
                    { "Chk","ProjectId","ProjectNm", "TotCnt", "YCnt", "SCnt", "UsCnt", "UsRate", "YMCnt", "MCnt", "UMCnt", "UMRate"},
                    { "0%", "0%", "20%", "8%", "8%", "8%", "8%", "8%", "8%", "8%", "8%", "8%"},
                    { "구분","현장ID", "현장명", "공급세대수", "분양(연간)", "분양(누계)", "미분양", "(%)", "입주(연간)", "입주(누계)","미입주","(%)"},
                    { "Hide","Hide", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center","Center", "Center", "Center", "Center","Center","Center","Center","Center","Center","Center","Center"},
                    { "None","None", "None", "None", "None","None","None","None","None","None","None","None"}
            };

            // 현장(재해현황) 조회
            List<Site> dbSite = _siteFactory.ListSalesMoveinGrid(Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListMonthPersonalVacationCnt : 연차사용현황 차트 조회

        [HttpPost]
        public JsonResult ListMonthPersonalVacationCnt(string pEmpNo, decimal pFlag)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListMonthPersonalVacationCnt(Util.MakeDateTime("YearMon"), pEmpNo, pFlag);

            var data = dbUser.Select(m => new
            {
                VacationMon = m.VacationMon,
                VacationCnt = m.VacationCnt,
                DNICD = m.DNICD,
                VacationColor = m.VacationColor
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListMonthDeptVacationCnt : 부서원 연차사용현황 차트 조회

        [HttpPost]
        public JsonResult ListMonthDeptVacationCnt(string pDeptCd, string pEmpNo)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListMonthDeptVacationCnt(Util.MakeDateTime("YearMon"), pDeptCd, pEmpNo);

            var data = dbUser.Select(m => new
            {
                VacationUser = m.VacationUser,
                VacationDeptCnt = m.VacationDeptCnt,
                VacationDeptColor = m.VacationDeptColor,
                VacationRsltColor = m.VacationRsltColor,
                VacationDeptTotCnt = m.VacationDeptTotCnt,
                DeptTotCnt = m.DeptTotCnt,
                RsltCnt = m.RsltCnt,
                PeoPleCnt = m.PeoPleCnt
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListMonthDeptVacationPercent : 부서 연차사용율 차트 조회

        [HttpPost]
        public JsonResult ListMonthDeptVacationPercent(string pDeptCd, string pEmpNo, string pBizpartCd, string pGubun)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListMonthDeptVacationPercent(Util.MakeDateTime("YearMon"), pDeptCd, pEmpNo, pBizpartCd, pGubun);

            var data = dbUser.Select(m => new
            {
                VacationUserP = m.VacationUserP,
                VacationDeptCntP = m.VacationDeptCntP,
                VacationDeptPColor = m.VacationDeptPColor,
                VacationInfoP = m.VacationInfoP
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListPersonGetOffWorkTime : 개인 퇴근시간 현황 차트 조회

        [HttpPost]
        public JsonResult ListPersonGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListPersonGetOffWorkTime(pDeptCd, pEmpNo);

            var data = dbUser.Select(m => new
            {
                ChartCntP = m.ChartCntP,
                Mon = m.Mon,
                Color = m.Color,
                AVRTIMEP = m.AVRTIMEP,
                ChartCntGubun1 = m.ChartCntGubun1
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListPLAnalysis : 손익현황 테스트

        [HttpPost]
        public JsonResult ListPLAnalysis(string pDeptCd, string pEmpNo)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListPLAnalysis(pDeptCd, pEmpNo);

            var data = dbUser.Select(m => new
            {

                NUM = m.NUM,
                TOTAMT = m.TOTAMT,
                AVRTIMEP = m.AVRTIMEP
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region ListDeptGetOffWorkTime : 부서별 퇴근시간 차트 조회

        [HttpPost]
        public JsonResult ListDeptGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListDeptGetOffWorkTime(pDeptCd, pEmpNo);

            var data = dbUser.Select(m => new
            {
                ChartCnt = m.ChartCnt,
                DEPT_NAME = m.DEPT_NAME,
                DEPT_COLOR = m.DEPT_COLOR,
                AVRTIME = m.AVRTIME,
                ChartCntGubun2 = m.ChartCntGubun2
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListDeptPersonGetOffWorkTime : 부서원 퇴근시간 현황 차트 조회

        [HttpPost]
        public JsonResult ListDeptPersonGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            // 분양률 조회
            List<User> dbUser = _userFactory.ListDeptPersonGetOffWorkTime(pDeptCd, pEmpNo);

            var data = dbUser.Select(m => new
            {
                EMP_NO = m.EMP_NO,
                USER_NAME = m.USER_NAME,
                DEPT_COLOR = m.DEPT_COLOR,
                AVRTIMEDP = m.AVRTIMEDP,
                ChartCntDP = m.ChartCntDP,
                ChartCntGubun3 = m.ChartCntGubun3
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListCreditGradeChart : 신용등급현황 차트 조회

        [HttpPost]
        public JsonResult ListCreditGradeChart(decimal Cnt)
        {
            // 분양률 조회
            List<Site> dbSite = _siteFactory.ListCreditGradeChart(Cnt);

            var data = dbSite.Select(m => new
            {
                ChartGradeNum1 = m.ChartGradeNum1,
                ChartCDate1 = m.ChartCDate1,
                ChartGrade1 = m.ChartGrade1,
                ChartGradeNum2 = m.ChartGradeNum2,
                ChartCDate2 = m.ChartCDate2,
                ChartGrade2 = m.ChartGrade2,
                ChartGradeNum3 = m.ChartGradeNum3,
                ChartCDate3 = m.ChartCDate3,
                ChartGrade3 = m.ChartGrade3,

                ChartGradeNum4 = m.ChartGradeNum4,
                ChartCDate4 = m.ChartCDate4,
                ChartGrade4 = m.ChartGrade4,
                ChartGradeNum5 = m.ChartGradeNum5,
                ChartCDate5 = m.ChartCDate5,
                ChartGrade5 = m.ChartGrade5,
                ChartGradeNum6 = m.ChartGradeNum6,
                ChartCDate6 = m.ChartCDate6,
                ChartGrade6 = m.ChartGrade6
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSubContractRankingChart : 도급순위현황 차트 조회

        [HttpPost]
        public JsonResult ListSubContractRankingChart()
        {
            // 분양률 조회
            List<Site> dbSite = _siteFactory.ListSubContractRankingChart(Util.MakeDateTime("YearMon"));

            var data = dbSite.Select(m => new
            {
                Ranking = m.Ranking,
                CorpName = m.CorpName,
                CreditGrade1 = m.CreditGrade1,
                CreditGrade2 = m.CreditGrade2,
                RankingType = m.RankingType,
                RankingSEQ = m.RankingSEQ,
                CorpColor = m.CorpColor,
                Fiscalyear = m.Fiscalyear
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListSubContractRanking : 도급순위현황 테이블

        [HttpPost]
        public ContentResult ListSubContractRanking(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 14] {
                    { "CorpName1", "Gubun1", "Gubun2", "Gubun3","BL","CorpName2","Gubun21","Gubun22","Gubun23","BL2","CorpName3","Gubun31","Gubun32","Gubun33"},
                    { "7%", "12%", "7%", "7%","1%","7%","12%","7%","7%","1%","7%","12%","7%","7%"},
                    { "도급순위", "회사명", "회사채", "기업어음","","도급순위","회사명","회사채","기업어음","","도급순위","회사명","회사채","기업어음"},
                    { "Show", "Show", "Show", "Show","Show", "Show", "Show", "Show","Show", "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center","Center", "Center", "Center", "Center","Center", "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None", "None"}
            };

            // 현장(재해현황) 조회
            List<Site> dbSite = _siteFactory.ListSubContractRanking(Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListControlDailyDepositRealTimeGrid : 일일입금현황(실시간) 조회 테이블

        [HttpPost]
        public ContentResult ListControlDailyDepositRealTimeGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pDatePick1, string pDatePick2, string pAcct, string pGubun)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 7] {
                    { "TRAN_TYPE", "TRAN_DATE", "OUT_AMOUNT", "IN_AMOUNT", "REMAIN_AMOUNT","DESCRIPTION","CUST_BANK"},
                    { "5%", "13%", "13%", "13%","13%","23%","20%"},
                    { "구분", "거래일자", "출금금액", "입금금액", "거래 후 잔액", "적요","취급은행"},
                    { "Show", "Show", "Show", "Show","Show","Show","Show"},
                    { "Center", "Center", "Right", "Right", "Right", "Center","Center"},
                    { "None", "None", "None", "None", "None", "None", "None"}
            };

            // 가용예산 조회 
            List<Control> dbOrder = _controlFactory.ListControlDailyDepositRealTimeGrid(pDatePick1, pDatePick2, pAcct, pGubun);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
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

            List<Control> dbControl = _controlFactory.ListBizCostGrid(pDeptCd, pEmpNo, Util.MakeDateTime("YearMon"));

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbControl, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListNewSujuState : 신규수주현황 조회

        [HttpPost]
        public ContentResult ListNewSujuState(string pPagerUseYn, decimal pCurPage, decimal pListCnt)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 3] {
                    {"RegNo", "Subject", "RegDate"},
                    { "0%","70%", "30%"},
                    { "등록번호","공사명", "수주년월"},
                    { "Hide", "Show", "Show"},
                    { "Left", "Left", "Center"},
                    { "None", "None", "None" }
            };

            List<Order> dbControl = _orderFactory.ListNewSujuState("");

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbControl, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region GetSiteCoordinates : 현장좌표값 가지고 오기

        [HttpPost]
        public JsonResult GetSiteCoordinates()
        {
            List<Order> dbOrder = _orderFactory.GetSiteCoordinates();

            var data = dbOrder.Select(m => new
            {
                ProjectId = m.ProjectId,
                ProjectNm = m.ProjectNm,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                BizPartCd = m.BizPartCd
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetSiteCoordinatesSearch : 현장좌표값 가지고 오기(검색)

        [HttpPost]
        public JsonResult GetSiteCoordinatesSearch(string pStieNm)
        {
            List<Order> dbOrder = _orderFactory.GetSiteCoordinatesSearch(pStieNm);

            var data = dbOrder.Select(m => new
            {
                ProjectId = m.ProjectId,
                ProjectNm = m.ProjectNm,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                BizPartCd = m.BizPartCd,
                Windy = m.Windy
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetSiteCoordinatesGubun : 현장좌표값 가지고 오기(검색)

        [HttpPost]
        public JsonResult GetSiteCoordinatesGubun(string pGubun)
        {
            List<Order> dbOrder = _orderFactory.GetSiteCoordinatesGubun(pGubun);

            var data = dbOrder.Select(m => new
            {
                ProjectId = m.ProjectId,
                ProjectNm = m.ProjectNm,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                BizPartCd = m.BizPartCd
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GridSiteState : 주간공정보고 테이블 조회

        [HttpPost]
        public ContentResult GridSiteState(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pBizPartCd, string pHome)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 9] {
                    { "SiteCd", "SiteNm", "FLAG", "ContractAmt","TotSaleTotAmt", "AccSiteRate", "YearlySalePlanAmt","RsltAmt", "SiteRate" },
                    { "0%", "30%", "10%", "10%", "10%", "10%", "10%", "10%", "10%" },
                    { "현장코드", "현장명", "구분", "누계계획", "누계실적","달성율(%)", "연간계획","연간실적", "달성율(%)" },
                    { "Hide", "Show", "Show", "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Right", "Right", "Center", "Right", "Right", "Center" },
                    { "None", "None", "None", "None", "None", "None", "None", "None", "None" }
            };

            // 수주실적(전체) 조회
            List<Site> dbSite = _siteFactory.GridSiteState(Util.MakeDateTime("YearMon"), pBizPartCd, pHome);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbSite, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListChkAuth : 회의자료 권한 조회

        [HttpPost]
        public JsonResult ListChkAuth(string pMeetTypeCd, string pEmpNo)
        {
            // 회의자료 권한 조회
            List<Meeting> dbMeeting = _meetingFactory.ListChkAuth(pMeetTypeCd, pEmpNo);

            var data = dbMeeting.Select(m => new
            {
                AuthType = m.AuthType
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListUserUsageStatisticsForKSIS : KSIS 사용자 통계 조회 테이블

        [HttpPost]
        public ContentResult ListUserUsageStatisticsForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pGubun1, string pGubun2, string pFrom, string pTo, string pEPUserId)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 5] {
                    { "USERID","USERNM", "DUTYNM", "RegCnt", "DUTYCD"},
                    { "0%", "25%", "25%", "25%", "0%"},
                    { "사용자ID", "성명", "직급", "접속횟수", "직급CD"},
                    { "hide", "Show", "Show", "Show", "hide"},
                    { "Center", "Center", "Center", "Right", "Center"},
                    { "None", "None", "None", "None", "None"}
            };

            //조회 
            List<Control> dbOrder = _controlFactory.ListUserUsageStatisticsForKSIS(pGubun1, pGubun2, pFrom, pTo, pEPUserId);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListUserStatisticsDetailForKSIS : KSIS 사용자별 통계 조회 테이블

        [HttpPost]
        public ContentResult ListUserStatisticsDetailForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pGubun1, string pGubun2, string pFrom, string pTo, string pEPUserId)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader;

            if (pGubun1 == "DD")
            {
                    pArrHeader = new string[6, 6] {
                    { "UserNmDetail", "DutyNmDetail", "PAGE", "Browser", "Device", "RegDateUser"},
                    { "0%", "0%", "30%", "20%", "20", "30"},
                    { "성명", "직급", "화면명", "브라우저", "접속기기", "접속일자" },
                    { "Hide", "Hide", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None"}
            };
            }
            else
            {
                    pArrHeader = new string[6, 4] {
                    { "UserNmDetail", "DutyNmDetail", "PAGE", "RegDateUser"},
                    { "0%", "0%", "50%", "50"},
                    { "성명", "직급", "화면명", "접속일자" },
                    { "Hide", "Hide", "Show", "Show"},
                    { "Center", "Center", "Center","Center"},
                    { "None", "None", "None", "None"}
            };

            }


            //조회 
            List<Control> dbOrder = _controlFactory.ListUserStatisticsDetailForKSIS(pGubun1, pGubun2, pFrom, pTo, pEPUserId);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡";

            return Content(data, "text/html");
        }
        #endregion

        #region ListMenuUsageStatisticsForKSIS : KSIS 메뉴 통계 조회 테이블

        [HttpPost]
        public ContentResult ListMenuUsageStatisticsForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pGubun1, string pGubun2, string pFrom, string pTo, string pSelect, string pTitleCd)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] {
                    { "ParentNm","ScreenCd","ScreenNm","TitleCd","TitleNm","PageCnt"},
                    { "25%", "0%", "25%", "0%", "25%", "25%"},
                    { "구분", "화면코드","화면명", "직급코드", "직급","클릭횟수"},
                    { "Show", "Hide", "Show", "Hide", "Show","Show"},
                    { "Center", "Center", "Center", "Center", "Center", "Right"},
                    { "None", "None", "None", "None", "None", "None"}
            };

            //조회 
            List<Control> dbOrder = _controlFactory.ListMenuUsageStatisticsForKSIS(pGubun1, pGubun2, pFrom, pTo, pSelect, pTitleCd);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListMenuStatisticsDetailForKSIS : KSIS 메뉴별 통계 조회 테이블

        [HttpPost]
        public ContentResult ListMenuStatisticsDetailForKSIS(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pGubun1, string pGubun2, string pFrom, string pTo, string pSelect, string pTitleCd)
        {

            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader;

            if (pGubun1 == "DD")
            {
                pArrHeader = new string[6, 5] {
                    { "DUTYNM", "USERNM", "PAGE", "Browser", "DateDetail"},
                    { "10%", "10%", "30%", "20%", "30%"},
                    { "직급", "성명", "세부화면", "브라우저", "일자" },
                    { "Show", "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None"}
            };
            }
            else
            {
                pArrHeader = new string[6, 4] {
                   { "DUTYNM", "USERNM", "PAGE", "DateDetail" },
                    { "15%", "15%", "30%", "40%" },
                    { "직급", "성명", "세부화면", "일자"},
                    { "Show" ,"Show","Show", "Show"},
                    { "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None"}
            };

            }

            //조회 
            List<Control> dbOrder = _controlFactory.ListMenuStatisticsDetailForKSIS(pGubun1, pGubun2, pFrom, pTo, pSelect, pTitleCd);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion




        ////////////////// 하자관리

        #region ListAsStepSummaryGrid : 단계별 진행 현황

        [HttpPost]
        public ContentResult ListAsStepSummaryGrid(string pSRVCNTR)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 37] {
                    { "RN",     "P",      "CDP",    "NMP",      "CNTP",     "A",        "CDA",    "NMA",    "CNTA",     "B",                  "CDB",    "NMB",    "CNTB",   "ZERO",   "CDZERO", "NMZERO", "CNTZERO", "TWO",       "CDTWO",  "NMTWO",  "CNTTWO", "THREE",  "CDTHREE", "NMTHREE", "CNTTHREE", "FOUR",   "CDFOUR", "NMFOUR", "CNTFOUR", "FIVE",      "CDFIVE", "NMFIVE", "CNTFIVE", "SUIT",   "CDSUIT", "NMSUIT", "CNTSUIT"},
                    { "0%",     "8%",     "0%",     "0%",       "0%",       "8%",       "0%",     "0%",     "0%",       "8%",                 "0%",     "0%",     "0%",     "8%",     "0%",     "0%",     "0%",      "8%",        "0%",     "0%",     "0%",     "8%",     "0%",      "0%",      "0%",       "8%",     "0%",     "0%",     "0%",      "8%",        "0%",     "0%",     "0%",      "8%",     "0%",     "0%",     "0%" },
                    { "RN",     "준비",   "CDP",    "NMP",      "CNTP",     "품질점검", "CDA",    "NMA",    "CNTA",     "입주자<br>사전점검", "CDB",    "NMB",    "CNTB",   "입주",   "CDZERO", "NMZERO", "CNTZERO", "2년차이하", "CDTWO",  "NMTWO",  "CNTTWO", "3년차",  "CDTHREE", "NMTHREE", "CNTTHREE", "4년차",  "CDFOUR", "NMFOUR", "CNTFOUR", "5년차이상", "CDFIVE", "NMFIVE", "CNTFIVE", "소송",   "CDSUIT", "NMSUIT", "CNTSUIT" },
                    { "hidden", "Show",   "hidden", "hidden",   "hidden",   "Show",     "hidden", "hidden", "hidden",   "Show",               "hidden", "hidden", "hidden", "Show",   "hidden", "hidden", "hidden",  "Show",      "hidden", "hidden", "hidden", "Show",   "hidden",  "hidden",  "hidden",   "Show",   "hidden", "hidden", "hidden",  "Show",      "hidden", "hidden", "hidden",  "Show",   "hidden", "hidden", "hidden"  },
                    { "Center", "Center", "Center", "Center",   "Center",   "Center",   "Center", "Center", "Center",   "Center",             "Center", "Center", "Center", "Center", "Center", "Center", "Center",  "Center",    "Center", "Center", "Center", "Center", "Center",  "Center",  "Center",   "Center", "Center", "Center", "Center",  "Center",    "Center", "Center", "Center",  "Center", "Center", "Center", "Center"},
                    { "None",   "None",   "None",   "None",     "None",     "None",     "None",   "None",   "None",     "None",               "None" ,  "None",   "None",   "None",   "None",   "None",   "None",    "None",      "None",   "None" ,  "None",   "None",   "None",    "None",    "None",     "None",   "None",   "None",   "None" ,   "None",      "None",   "None",   "None",    "None",   "None",   "None",   "None"}
         };

            // 단계별 하자현황 조회
            List<AsManage> dbAsManage = _AsManageFactory.ListAsStepSummary(pSRVCNTR);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbAsManage, "N", 0, 10);

            return Content(data, "text/html");
        }
        #endregion

        #region ListAsSummaryGrid : 단계별 처리현황

        [HttpPost]
        public ContentResult ListAsSummaryGrid(string pSRVCNTR, string pGubun)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 14] {
                    { "kpicd",  "kpinm",    "ordno",  "e01",        "a60",      "c30",                "e00",   "s01",   "s02",   "s03",   "s04",   "s05",   "s07",   "s10"},
                    { "0%",     "12%",      "0%",     "0%",         "0%",       "8%",                 "8%",    "8%",    "8%",    "8%",    "8%",    "8%",    "8%",    "8%"  },
                    { "kpicd",  "구분",     "ordno",  "1년차부터",  "품질점검", "입주자<BR>사전점검", "입주",  "1년차", "2년차", "3년차", "4년차", "5년차", "7년차", "10년차"},
                    { "Hidden", "Show",     "Hidden", "Hidden",     "Hidden",   "Show",               "Show",  "Show",  "Show",  "Show",  "Show",  "Show",  "Show",  "Show" },
                    { "Center", "Center",    "Right", "Right",      "Right",    "Right",              "Right", "Right", "Right", "Right", "Right", "Right", "Right", "Right"},
                    { "None",   "None",     "None",   "None",       "None",     "None",               "None",  "None",  "None",  "None",  "None" , "None",  "None",  "None"}
         };

            // 단계별 하자현황 조회
            List<AsManage> dbAsManage = _AsManageFactory.ListAsSummary(pSRVCNTR, pGubun);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbAsManage, "N", 0, 10);

            return Content(data, "text/html");
        }
        #endregion

        #region ListAsStepSummaryDetail : 현장별 단계별 하자현황
        [HttpPost]
        public ContentResult ListAsStepSummaryDetail(string pPJTCD)
        {
            List<AsManage> dbAsManage = _AsManageFactory.ListAsStepSummaryDetail(pPJTCD);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:11%"" />");
            sb.AppendLine(@"<col style=""width:10%"" />");
            //sb.AppendLine(@"<col style=""width:11%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"</colgroup>");

            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th class=""td_center"">세대수</th>");
            sb.AppendLine(@"<th class=""td_center"">구분</th>");
            //sb.AppendLine(@"<th class=""td_center"">D-60</th>");
            sb.AppendLine(@"<th class=""td_center"">입주자사전점검</th>");
            sb.AppendLine(@"<th class=""td_center"">입주</th>");
            sb.AppendLine(@"<th class=""td_center"">2년차이하</th>");
            sb.AppendLine(@"<th class=""td_center"">3년차</th>");
            sb.AppendLine(@"<th class=""td_center"">4년차</th>");
            sb.AppendLine(@"<th class=""td_center"">5년차이상</th>");
            sb.AppendLine(@"</tr>");

            for (int i = 0; i < dbAsManage.Count(); i++)
            {
                sb.AppendLine(@"<tr>");
                if (i == 0)
                {
                    sb.AppendLine(@"<td rowspan=""2"" class=""td_center"">" + dbAsManage[i].TOTHOUSENO + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DIV + "</td>");
                }
                else if (i == 1)
                {
                    sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DIV + "</td>");
                }
                else
                {
                    sb.AppendLine(@"<td colspan=""2"" class=""td_center"">" + dbAsManage[i].TOTHOUSENO + "</td>");
                }
                //sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].D60 + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].D30 + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].MOVEIN + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].MOVEIN2 + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].MOVEIN3 + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].MOVEIN4 + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].MOVEIN5 + "</td>");
                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");

        }
        #endregion ListAsStepSummaryDetail : 현장별 하자현황

        #region ListAsVendor : 현장별 업체 현황

        [HttpPost]
        public ContentResult ListAsVendor(string pPJTCD, string pPROGSTEP)
        {
            List<AsManage> dbAsManage = _AsManageFactory.ListAsVendor(pPJTCD, pPROGSTEP);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:8%"" />");
            sb.AppendLine(@"<col style=""width:11%"" />");
            sb.AppendLine(@"<col style=""width:18%"" />");           
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:9%"" />");
            sb.AppendLine(@"<col style=""width:0%;"" />");       // 20190214 suyeon26
            sb.AppendLine(@"<col style=""width:0%"" />");       // 20190214 suyeon26
            sb.AppendLine(@"</colgroup>");

            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th class=""td_center"">구분</th>");
            sb.AppendLine(@"<th class=""td_center"">공종</th>");
            sb.AppendLine(@"<th class=""td_center"">업체명</th>");
            sb.AppendLine(@"<th class=""td_center"">지적건</th>");
            sb.AppendLine(@"<th class=""td_center"">세대당<br>지적건</th>");
            sb.AppendLine(@"<th class=""td_center"">처리건</th>");
            sb.AppendLine(@"<th class=""td_center"">미처리건</th>");
            sb.AppendLine(@"<th class=""td_center"">재하자건</th>");
            sb.AppendLine(@"<th class=""td_center"">처리율<br>(%)</th>");
            sb.AppendLine(@"<th class=""td_center"">재하자율<br>(%)</th>");
            sb.AppendLine(@"<th class=""td_center"">업체코드</th>");
            sb.AppendLine(@"<th class=""td_center"">순위</th>");
            sb.AppendLine(@"</tr>");

            for (int i = 0; i < dbAsManage.Count(); i++)
            {
                sb.AppendLine(@"<tr>");

                if (i == dbAsManage.Count() - 1)
                {
                    sb.AppendLine(@"<td colspan=""3""class=""td_center""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].RCPTCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].HOUSEINDICCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].HNDCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].YETHNDCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].RERCPTCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].HNDRATE + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].RERCPTRATE + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].TRADEID + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].RNK + "</td>");
                }
                else
                {
                    // 대공종, 소계 추가 20201027 suyeon26
                    if (i == 0 || dbAsManage[i].DFLJOBTYPENM != dbAsManage[i-1].DFLJOBTYPENM)
                    {
                        sb.AppendLine(@"<td rowspan=" + dbAsManage[i].DFLJOBTYPECNT);
                        sb.AppendLine(@" class=""td_center"">" + dbAsManage[i].DFLJOBTYPENM + "</td>");
                    }

                    if (dbAsManage[i].DFJOBTYPENM == "소계")
                    {
                        sb.AppendLine(@"<td colspan=""2"" class=""td_center"" style=""background-color:#eaeaea;"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].RCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].HOUSEINDICCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].HNDCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].YETHNDCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].RERCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].HNDRATE + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].RERCPTRATE + "</td>");
                        sb.AppendLine(@"<td class=""td_center""style=""background-color:#eaeaea;"">" + dbAsManage[i].TRADEID + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].RNK + "</td>");
                    }
                    else
                    {
                        sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                        sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].TRADENM + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].RCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].HOUSEINDICCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].HNDCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].YETHNDCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].RERCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].HNDRATE + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].RERCPTRATE + "</td>");
                        sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].TRADEID + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].RNK + "</td>");
                    }
                    
                    
                }

                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");

        }
        #endregion

        #region ListAsReasonGrid : 현장별 하자 원인

        [HttpPost]
        public ContentResult ListAsReasonGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pPJTCD, string pPROGSTEP)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 4] {
                    { "DFCAUSEID",  "DFCAUSENM", "INDICCNT", "CAUSERATE" },
                    { "0%",         "33%",       "33%",      "33%" },
                    { "하자원인ID", "하자원인",  "지적건",   "비율(%)" },
                    { "Hidden",     "Show",      "Show",     "Show" },
                    { "Center",     "Center",    "Right",    "Right" },
                    { "None",       "None",      "None",     "None" }
         };

            // 단계별 하자현황 조회
            List<AsManage> dbAsManage = _AsManageFactory.ListAsReason(pPJTCD, pPROGSTEP);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbAsManage, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListAsItem : 현장별 하자 공종
        [HttpPost]
        public ContentResult ListAsItem(string pPJTCD, string pPROGSTEP)
        {
            List<AsManage> dbAsManage = _AsManageFactory.ListAsItem(pPJTCD, pPROGSTEP);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:13%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:35%"" />");
            sb.AppendLine(@"<col style=""width:16%"" />");
            sb.AppendLine(@"<col style=""width:16%"" />");
            sb.AppendLine(@"</colgroup>");

            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>"); ;
            sb.AppendLine(@"<th class=""td_center"">구분</th>");
            sb.AppendLine(@"<th colspan=""2""class=""td_center"">공종</th>");
            sb.AppendLine(@"<th class=""td_center"">지적건</th>");
            sb.AppendLine(@"<th class=""td_center"">비율(%)</th>");
            sb.AppendLine(@"</tr>");

            for (int i = 0; i < dbAsManage.Count(); i++)
            {
                sb.AppendLine(@"<tr>");

                if (i == dbAsManage.Count() - 1)
                {
                    sb.AppendLine(@"<td colspan=""3""class=""td_center""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].DFLJOBTYPENM + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].RCPTCNT + "</td>");
                    sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;font-weight:bold"">" + dbAsManage[i].STEPRATE + "</td>");
                }
                else
                {
                    // 대공종 추가 (셀병합) 20201005 SUYEON26
                    if (i == 0 || dbAsManage[i].DFLJOBTYPENM != dbAsManage[i - 1].DFLJOBTYPENM)
                    {
                        sb.AppendLine(@"<td rowspan=" + dbAsManage[i].DFLJOBTYPECNT);
                        sb.AppendLine(@" class=""td_center"">" + dbAsManage[i].DFLJOBTYPENM + "</td>");
                    }

                    if (dbAsManage[i].UPDFJOBTYPENM == "소계")
                    {
                        sb.AppendLine(@"<td colspan=""2"" class=""td_center""style=""background-color:#eaeaea;"">" + dbAsManage[i].UPDFJOBTYPENM + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].RCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right""style=""background-color:#eaeaea;"">" + dbAsManage[i].STEPRATE + "</td>");
                    }
                    else
                    {
                        sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].UPDFJOBTYPENM + "</td>");
                        sb.AppendLine(@"<td class=""td_center""style=""color:#0000FF;cursor:pointer"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                        //sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].RCPTCNT + "</td>");
                        sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[i].STEPRATE + "</td>");
                    }                   
                }

                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region ListAsDistribution : 하자발생분포도 차트 조회

        [HttpPost]
        public JsonResult ListAsDistribution(string pPJTCD, string pPROGSTEP)
        {
            // 분양률 조회
            List<AsManage> dbAsManage = _AsManageFactory.ListAsDistribution(pPJTCD, pPROGSTEP);

            var data = dbAsManage.Select(m => new
            {
                CONSTRADEID = m.CONSTRADEID,
                RCPTCNT = m.RCPTCNT,
                RCPTCNTCOMMA = m.RCPTCNTCOMMA, // 하자발생 천단위표시 20201028
                HNDRATE = m.HNDRATE,
                DFJOBTYPENM = m.DFJOBTYPENM,
                TRADESHORTNM = m.TRADESHORTNM,
                TRADENM = m.TRADENM,
                COLOR = m.COLOR,
                ORD = m.ORD
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ListAsActionRate : 하자보수 조치율 차트 조회

        [HttpPost]
        public JsonResult ListAsActionRate(string pPJTCD, string pPROGSTEP)
        {
            // 분양률 조회
            List<AsManage> dbAsManage = _AsManageFactory.ListAsActionRate(pPJTCD, pPROGSTEP);

            var data = dbAsManage.Select(m => new
            {
                TRADESHORTNM = m.TRADESHORTNM,
                HNDRATE = m.HNDRATE,
                ORD = m.ORD,
                COLOR = m.COLOR
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

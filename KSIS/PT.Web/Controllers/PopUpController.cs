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

    public class PopUpController : BaseController
    {
        PopUpFactory    _PopUpFactory    = new PopUpFactory();
        UserFactory     _userFactory     = new UserFactory();
        AttFileFactory  _attFileFactory  = new AttFileFactory();
        SiteFactory     _siteFactory     = new SiteFactory();
        CDPFactory      _CDPFactory      = new CDPFactory();
        ControlFactory  _controlFactory  = new ControlFactory();
        MeetingFactory  _meetingFactory  = new MeetingFactory();
        AsManageFactory _asManageFactory = new AsManageFactory();
        OrderFactory _OrderFactory = new OrderFactory();

        // GET: /PopUp/

        #region SearchPopUp 업체조회 팝업 활성화

        public ActionResult SearchPopUp()
        {
            return View();
        }

        #endregion

        #region CooperatorDetail : 협력업체 상세정보 조회
        [HttpGet]
        public ActionResult CooperatorDetail(string pVendorCd)
        {
            
            PopUp dbCooperatorDetail = _PopUpFactory.GetCooperatorDetail(pVendorCd);

            return View(dbCooperatorDetail);
            

        }
        #endregion

        #region ListVendorClassification : 협력업체 공종/자재 조회

        [HttpPost]
        public ContentResult ListVendorClassification(string pVendorCd)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 4] { 
                    { "CdNm", "RegStatusNm", "RegTargetTypeNm", "RequestNm"},
                    { "30%", "10%", "10%", "50%"},
                    { "등록공종 / 자재", "등록상태", "등록형태", "상세내용"},
                    { "Show", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None"}
            };
            string pPagerUseYn="N";
            decimal pCurPage=0, pListCnt=0;
            // 현장(재해현황) 조회
            List<PopUp> dbVendor = _PopUpFactory.ListVendorClassification(pVendorCd);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbVendor, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region ListUser 사우정보 검색(둘이상일경우) 팝업

        public ActionResult ListUser(string pSearchWord)
        { 
            return View(ListUserGrid("N",0,0,pSearchWord));
        }
        #endregion

        #region ListUser : 사우정보 검색(둘이상일경우) 팝업
        [HttpGet]
        public ActionResult ListUserGrid(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSearchWord)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] { 
                    { "UserId", "UserNm", "DutyNm", "DeptNm", "CompTel", "Mobile" },
                    { "10%", "10%", "15%", "20%", "20%", "20%" },
                    { "사용자ID", "이름", "직급", "부서", "전화번호", "휴대폰" },
                    { "Show", "Show", "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Center", "Center", "Center" },
                    { "None", "None", "None", "None", "None", "None" }
            };

            // 사용자목록 그리드 조회

            List<PopUp> dbUser = _PopUpFactory.ListUserGrid(pSearchWord);

            // 그리드 생성
            ViewBag.table = HtmlUtil.MakeGrid(pArrHeader, dbUser, pPagerUseYn, pCurPage, pListCnt);

            // 검색조건 반환
            //data = data + "‡" + pSearchWord;

            return View();
        }
        #endregion

        #region ListUserDetail : 사용자정보
        [HttpGet]
        public ActionResult ListUserDetail(string pUserId, string pSEQ)
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
        
        #region ListUserDetail : 입찰내역보기( 경매기록팝업 )
        [HttpGet]
        public ActionResult ListBidDetailPopUP(string pAuctionNo, string pListType)
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "입찰내역보기", Session["browser"].ToString(), Session["device"].ToString());
            ViewBag.pAuctionNo = pAuctionNo;
            ViewBag.pUserId    = Session["UserId"].ToString();
            ViewBag.pListType  = pListType;

            return View();
        }
        #endregion

        #region SaveMagazine : 사보등록(GET)
        [HttpGet]
        public ActionResult SaveMagazine(string pSaveMode, string pRegNo)
        {
            //사보등록
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN010100", Session["browser"].ToString(), Session["device"].ToString());

            Community dbCommunity = new Community();
            
            dbCommunity.RegNo = pRegNo;
            dbCommunity.RegUserNm = _UserNm;
            dbCommunity.RegDate = Util.MakeDateTime("YearMonDay");
            dbCommunity.SaveMode = pSaveMode;

            return View(dbCommunity);
        }
        #endregion

        #region SaveMagazine : 사보등록(POST)
        [HttpPost]
        public ActionResult SaveMagazine(Community pMagazine, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            string regNo = "";

            try
            {
                if (pMagazine.AttId == null)
                {
                    var YearMon = pMagazine.Date.Replace("-","");
                    // 첨부파일 신규
                    pMagazine.AttId = _attFileFactory.SaveMagazine(pAttIdFiles, ModuleCd.MG, RouteData, _UserId, YearMon);
                }
                else
                {
                    // 첨부파일 수정
                    _attFileFactory.SaveFiles(pMagazine.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.MG, _UserId);
                }

                // 사보자료 저장
                regNo = _PopUpFactory.SaveMagazine(pMagazine, _UserId);
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove("에러가 발생했습니다.", "/Information/Magazine");
            }

            if (pMagazine.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Information/Magazine");
            }
            else
            {
                return RouteUtil.MessageAndCloseAndReloadParent("저장 하였습니다.");
            }
        }
        #endregion

        #region SaveSitePhoto : 현장현황사진등록(GET)
        [HttpGet]
        public ActionResult SaveSitePhoto(string pSiteCd, string pSaveMode, string pRegNo)
        {
            //현장현황(사진등록)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020101", Session["browser"].ToString(), Session["device"].ToString());

            PopUp dbPopUp = new PopUp();

            dbPopUp.RegNo = "";
            dbPopUp.RegUserNm = _UserNm;
            dbPopUp.RegDate = Util.MakeDateTime("YearMonDay");
            dbPopUp.SaveMode = pSaveMode;
            dbPopUp.SiteCd = pSiteCd;

            return View(dbPopUp);
        }
        #endregion

        #region SaveSitePhoto : 사진등록(POST)
        [HttpPost]
        public ActionResult SaveSitePhoto(PopUp pPopUp, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            string regNo = "";

            try
            {
                if (pPopUp.AttId == null)
                {
                    // 첨부파일 신규 
                    pPopUp.AttId = _attFileFactory.SaveResizePhoto(pAttIdFiles, ModuleCd.FS, RouteData, _UserId);
                }
                else
                {
                    // 첨부파일 수정
                    _attFileFactory.SaveResizePhoto(pPopUp.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.FS, _UserId);
                }

                // 사보자료 저장
                regNo = _PopUpFactory.SaveSitePhoto(pPopUp, _UserId);
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove("에러가 발생했습니다.", "/Information/Magazine");
            }

            if (pPopUp.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Information/Magazine");
            }
            else
            {
                return RouteUtil.MessageAndCloseAndReloadParent("저장 하였습니다.");
            }
        }
        #endregion

        #region BlowUpPhoto : 사진확대
        [HttpGet]
        public ActionResult BlowUpPhoto(string pSiteCd, string pNumber)
        {
            //현장현황(사진확대)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020103", Session["browser"].ToString(), Session["device"].ToString());

            Site dbSite = _siteFactory.GetSitePhotoDetailUrl(pSiteCd, pNumber);

             return View(dbSite);
        }
        #endregion

        #region SitePhotoListAll : 등록사진 전체보기
        [HttpGet]
        public ActionResult SitePhotoListAll(string pSiteCd)
        {
            //현장현황(전체사진보기)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020102", Session["browser"].ToString(), Session["device"].ToString());

            List<Site> dbSite = _siteFactory.ListSitePhotoUrlAll(pSiteCd);

            ViewBag.SitePhoto = HtmlUtil.MakeBannerSiteBox(dbSite, "bn_box7");

            ViewBag.SiteCd = pSiteCd;

            return View();
        }
        #endregion

        #region ListSitePhotoAllCheck : 등록사진 전체보기 체크
        [HttpPost]
        public ActionResult ListSitePhotoAllCheck(string pSiteCd)
        {
            //현장현황(전체사진보기)
            
            List<Site> dbSite = _siteFactory.ListSitePhotoUrlAll(pSiteCd);

            var data = dbSite.Select(m => new
            {
                RowNum = m.RowNum
            });

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion
        

        #region SiteStateSummary : 공사개요 상세보기
        [HttpGet]
        public ActionResult SiteStateSummary(string pBizPartCd, string pSiteCd)
        {
            // 현장정보 조회
            Site dbSite = _siteFactory.GetSiteInfo(pSiteCd);

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View(dbSite);
        }
        #endregion

        #region CDPDetail : 개인경력사항(팝업)
        [HttpGet]
        public ActionResult CDPDetail(string pEmpNo)
        {
            // 사용자정보 조회
            CDP PSNL = _CDPFactory.GetPSNL(pEmpNo, "");

            return View(PSNL);
        }

        #endregion

        #region SearchSite : 부서조회(팝업)
        [HttpGet]
        public ActionResult SearchSite(string pSearchWord)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 2] {
                    { "ProjectCode", "ProjectName"},
                    { "20%", "80%"},
                    { "현장코드", "현장명"},
                    { "Show", "Show"},
                    { "Center", "Center"},
                    { "None", "None"}
            };

            string pSiteCd = pSearchWord;

            List<Site> dbSite = _siteFactory.ListCompletionSiteInfoDetail(pSiteCd);

            // 그리드 생성
            ViewBag.table = HtmlUtil.MakeGrid(pArrHeader, dbSite, "N", 0, 0);

            return View();
        }

        #endregion 

        #region UsagePopUp : 사용자 이력 정보
        [HttpGet]
        public ActionResult UsagePopUp(string pScreenCd, string pGUBUN, string pDutyCd, string pTerm)
        {
            ViewBag.ScreenCd = pScreenCd;
            ViewBag.GUBUN = pGUBUN;
            ViewBag.DutyCd = pDutyCd;
            ViewBag.Term = pTerm;

            return View();
        }

        #endregion

        #region ListUsagePopUp : 사용자 이력 정보 조회
        [HttpPost]
        public ContentResult ListUsagePopUp(string pScreenCd, string pGUBUN ,string pDutyCd, string pTerm)
        {
            List<PopUp> dbPopUpH = _PopUpFactory.ListUsagePopUp(pScreenCd, pGUBUN, pDutyCd, "N", pTerm);
            List<PopUp> dbPopUpL = _PopUpFactory.ListUsagePopUp(pScreenCd, pGUBUN, pDutyCd, "Y", pTerm);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:10%"" />");
            sb.AppendLine(@"<col style=""width:10%"" />");
            foreach (PopUp f in dbPopUpH)
            {
                sb.AppendLine(@"<col style=""width:10%"" />");
            }
            sb.AppendLine(@"<col />");
            sb.AppendLine(@"</colgroup>");
            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th>직 급</th>");
            sb.AppendLine(@"<th>성 명</th>");
            for (int i = 0; i < dbPopUpH.Count; i++) {
                sb.AppendLine(@"<th>" + dbPopUpH[i].TableHead + "</th>");
            }
            sb.AppendLine(@"</tr>");
            if(pScreenCd == "Auction")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN020000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN020100 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN030000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN030100 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN040000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "OrderResult")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME010000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME020000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME030000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME030100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "SalesPromotions")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME040000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "CreditGrade")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.ME050000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "SiteState")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE010000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE020000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE020100 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE020101 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE020102 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE020103 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE030000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE040000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "SiteWeather")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE050000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE050100 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE060000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE070000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "Map")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE080000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "CompletionSite")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE090000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "CDP")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN010000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN020000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN020100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "OrganizeChart")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN030000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN030100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "VacationState")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN040000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "GetOffWorkTime")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN050000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "Cooperator")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN060000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.PN060100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "Budget")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.CL010000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "DailyDeposit")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.CL020000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "BizCost")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.CL030000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.CL030100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "KSISUsageStatistics")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.CL040000 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "Meeting")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.RE010000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.RE010100 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.RE010200 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "Magazine")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN010000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.IN010100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            else if (pScreenCd == "AsManage")
            {
                foreach (PopUp f in dbPopUpL)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.DutyPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + f.UserPop + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE100000 + "</td>");
                    sb.AppendLine(@"<td class=""td_center"" style=""text-align:Right;"">" + f.SE100100 + "</td>");
                    sb.AppendLine(@"</tr>");
                }
            }
            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");

        }
        #endregion

        #region AuthRegist : 권한등록
        [HttpGet]
        public ActionResult AuthRegist(string pMenuCd, string pRegUserId )
        {
            string[,] pArrList = new string[2, 3] {
                { "UserNm", "DeptNm", "DutyNm"},
                { "이름", "부서명", "직급/직책" }
            };
            ViewBag.ddlSearchTypeCd = Util.ListForDDL(pArrList, false, "UserNm");
            ViewBag.MenuCd = pMenuCd;
            ViewBag.RegUserId = pRegUserId;

            return View();
        }

        #endregion

        #region ListCondtionalSearch : 권한등록 페이지 조회
        [HttpPost]
        public ActionResult ListCondtionalSearch(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSearchTypeCd, string pSearchWord)
        {
            string[,] pArrHeader = new string[6, 7];

            if (pSearchTypeCd == "UserNm")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 5] {
                    { "UserId","DutyNm","UserNm", "DeptNm", "GubunCd"},
                    { "25%", "25%", "25%", "25%", "0%"},
                    { "사번", "직급", "이름", "부서", "구분자"},
                    { "Show", "Show", "Show", "Show", "Hide"},
                    { "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None"}
                };

            }
            else if (pSearchTypeCd == "DeptNm")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 3] {
                    { "DeptCd","DeptNm", "GubunCd"},
                    { "50%", "50%", "0%"},
                    { "부서코드", "부서", "구분자"},
                    { "Show", "Show", "Hide"},
                    { "Center", "Center", "Center"},
                    { "None", "None", "None"}
                };
            }
            else
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 3] {
                    { "CD","CodeNm", "GubunCd"},
                    { "50%", "50%", "0%"},
                    { "직급/직책코드", "직급/직책명", "구분자"},
                    { "Show", "Show", "Hide"},
                    { "Center", "Center", "Center"},
                    { "None", "None", "None"}
                };
            }

            // 현장(재해현황) 조회
            List<PopUp> dbPopUp = _PopUpFactory.ListCondtionalSearch(pSearchTypeCd, pSearchWord);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbPopUp, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region InsertCondtional : 권한등록
        [HttpPost]
        public ActionResult InsertCondtional(string pMenuCd, string pAuthUserId, string pRegUserId)
        {
            
            // 현장(재해현황) 조회
            string Msg = _PopUpFactory.InsertCondtional(pMenuCd, pAuthUserId, pRegUserId);

            return Content(Msg, "text/html");
        }
        #endregion

        #region NoticePopUp : 공지사항팝업
        [HttpGet]
        public ActionResult NoticePopUp(string pMeetTypeCd, string pRegNo)
        {
            // 회의자료 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View(dbMeeting);
        }
        #endregion

        #region SearchAsProject : 프로젝트조회(팝업)
        [HttpGet]
        public ActionResult SearchAsProject(string pSearchWord)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 2] {
                    { "PJTCD", "PJTNM"},
                    { "20%", "80%"},
                    { "현장코드", "현장명"},
                    { "Show", "Show"},
                    { "Center", "Center"},
                    { "None", "None"}
            };

            string pPJTCD = pSearchWord;

            List<AsManage> dbAsManage = _asManageFactory.ListAsProject(pPJTCD);

            // 그리드 생성
            ViewBag.table = HtmlUtil.MakeGrid(pArrHeader, dbAsManage, "N", 0, 0);

            return View();
        }

        #endregion

        #region AsVendorDetail : 업체별 하자현황 팝업
        [HttpGet]
        public ActionResult AsVendorDetail(string pPJTCD, string pCONSTRADEID, string pTRADENM, string pDFJOBTYPENM, string pPROGSTEP)
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE100101", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.pPJTCD = pPJTCD;            
            ViewBag.pCONSTRADEID = pCONSTRADEID;
            ViewBag.pTRADENM = pTRADENM;
            ViewBag.pDFJOBTYPENM = pDFJOBTYPENM;
            ViewBag.pPROGSTEP = pPROGSTEP;

            return View();
        }
        #endregion
        
        #region ListAsVendorDetail : 업체별 하자현황 상세
        [HttpPost]
        public ContentResult ListAsVendorDetail(string pPJTCD, string pCONSTRADEID, string pDFJOBTYPENM, string pPROGSTEP)
        {
            List<AsManage> dbAsManage = _asManageFactory.ListAsVendorDetail(pPJTCD, pCONSTRADEID, pDFJOBTYPENM, pPROGSTEP);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:17%"" />");
            sb.AppendLine(@"<col style=""width:17%"" />");
            sb.AppendLine(@"<col style=""width:17%"" />");
            sb.AppendLine(@"<col style=""width:17%"" />");
            sb.AppendLine(@"<col style=""width:16%"" />");
            sb.AppendLine(@"<col style=""width:16%"" />");
            sb.AppendLine(@"</colgroup>");

            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th class=""td_center"">중공종</th>");
            sb.AppendLine(@"<th class=""td_center"">세부공종</th>");
            sb.AppendLine(@"<th class=""td_center"">위치</th>");
            sb.AppendLine(@"<th class=""td_center"">유형</th>");
            sb.AppendLine(@"<th class=""td_center"">원인</th>");
            sb.AppendLine(@"<th class=""td_center"">건수</th>");
            sb.AppendLine(@"</tr>");            

            for (int i = 0; i < dbAsManage.Count(); i++)
            {
                sb.AppendLine(@"<tr>");

                if (i == 0)
                {
                    sb.AppendLine(@"<td rowspan=" + dbAsManage.Count());
                    sb.AppendLine(@" class=""td_center"">" + dbAsManage[i].UPDFJOBTYPENM + "</td>");                                       
                };

                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFJOBTYPENM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].LOCNM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFNM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFCAUSENM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].CNT + "</td>");
                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");           

        }

        #endregion ListAsVendorDetail : 업체별 하자현황 상세

        #region AsItemDetail : 공종별 하자현황 팝업
        [HttpGet]
        public ActionResult AsItemDetail(string pPJTCD, string pUPDFNM, string pDFNM, string pPROGSTEP)
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE100102", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.pPJTCD = pPJTCD;
            ViewBag.pUPDFNM = pUPDFNM;
            ViewBag.pDFNM = pDFNM;
            ViewBag.pPROGSTEP = pPROGSTEP;

            return View();
        }
        #endregion AsItemDetail : 공종별 하자현황 팝업

        #region ListAsItemDetail : 공종별 하자현황 상세
        [HttpPost]
        public ContentResult ListAsItemDetail(string pPJTCD, string pUPDFNM, string pDFNM, string pPROGSTEP)
        {
            List<AsManage> dbAsManage = _asManageFactory.ListAsItemDetail(pPJTCD, pUPDFNM, pDFNM, pPROGSTEP);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"</colgroup>");

            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th class=""td_center"">업체명</th>");
            sb.AppendLine(@"<th class=""td_center"">위치</th>");
            sb.AppendLine(@"<th class=""td_center"">유형</th>");
            sb.AppendLine(@"<th class=""td_center"">원인</th>");
            sb.AppendLine(@"<th class=""td_center"">건수</th>");
            sb.AppendLine(@"</tr>");

            for (int i = 0; i < dbAsManage.Count(); i++)
            {
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].TRADENM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].LOCNM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFNM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].DFCAUSENM + "</td>");
                sb.AppendLine(@"<td class=""td_center"">" + dbAsManage[i].CNT + "</td>");
                sb.AppendLine(@"</tr>");
            }

            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");           

        }

        #endregion ListAsItemDetail : 공종별 하자현황 상세

        #region ProjectDetail : 수주정보
        [HttpGet]
        public ActionResult ProjectDetail(string pOrderStateTypeCd, string pSubject, string pProjectId)
        {
            // 수주정보 조회

            Order dbOrder = _OrderFactory.ListProject(pOrderStateTypeCd, pSubject, pProjectId);


            dbOrder = _OrderFactory.ListProject(pOrderStateTypeCd, pSubject, pProjectId);

            ViewBag.ProjectFullNm = dbOrder.ProjectFullNm;
            ViewBag.VendorNm = dbOrder.VendorNm;
            ViewBag.FixAmt = dbOrder.FixAmt;


            return View(dbOrder);
        }
        #endregion

        #region SujuStatePopUp : 수주실적 월별 현황 조회
        [HttpGet]
        public ActionResult SujuStatePopUp(string pYearMon)
        {

            ViewBag.YearMon = pYearMon;

            return View();

        }
        #endregion  

        #region ListSujuMonthResult : 수주실적 월별 현황 조회

        [HttpPost]
        public ContentResult ListSujuMonthResult(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pYearMon)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 4] {
                    { "ChargeDeptNm", "SujuProjectNm", "ReceiveFixDay", "SujuFixAmt" },
                    { "20%", "40%", "20%", "20%" },
                    { "부서", "공사명", "수주년월", "수주금액" },
                    { "Show", "Show", "Show", "Show" },
                    { "Center", "Center", "Center", "Right" },
                    { "None", "None", "None", "None" }
         };

            if (pYearMon == "")
            {
                pYearMon = Util.MakeDateTime("YearMon");
            }

            // 수주실적(전체) 조회
            List<Order> dbOrder = _OrderFactory.ListSujuMonthResult(pYearMon);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbOrder, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion   

        #region AuthRegistSuju : 권한등록 팝업
        [HttpGet]
        public ActionResult AuthRegistSuju(string pMeetTypeCd, string pRegNo, string pRegUserId)
        {

            ViewBag.MeetTypeCd = pMeetTypeCd;
            ViewBag.RegNo = pRegNo;
            ViewBag.RegUserId = pRegUserId;

            return View();
        }

        #endregion

        #region ListMeetingExecutives : 자료공개 대상 리스트 조회
        [HttpPost]
        public ActionResult ListMeetingExecutives(string pMeetTypeCd, string pRegNo, string pSelectGubun)
        {
            List<PopUp> dbPopUp = _PopUpFactory.ListMeetingExecutives(pMeetTypeCd, pRegNo, pSelectGubun);

            StringBuilder sb = new StringBuilder("");

            if(pSelectGubun == "List")
            {
                sb.AppendLine(@"<table>");
                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"</colgroup>");

                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th class=""td_center"">선택</th>");
                sb.AppendLine(@"<th class=""td_center"">사번</th>");
                sb.AppendLine(@"<th class=""td_center"">직급</th>");
                sb.AppendLine(@"<th class=""td_center"">이름</th>");
                sb.AppendLine(@"</tr>");

                for (int i = 0; i < dbPopUp.Count(); i++)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center""><input type=""checkbox"" name = ""checkList"" id='checkList_" + i + "'></td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].UserId + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].DutyNm + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].UserNm + "</td>");
                    sb.AppendLine(@"</tr>");
                }

                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");
            }
            else
            {
                sb.AppendLine(@"<table>");
                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"<col style=""width:25%"" />");
                sb.AppendLine(@"</colgroup>");

                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th class=""td_center"">선택</th>");
                sb.AppendLine(@"<th class=""td_center"">사번</th>");
                sb.AppendLine(@"<th class=""td_center"">직급</th>");
                sb.AppendLine(@"<th class=""td_center"">이름</th>");
                sb.AppendLine(@"</tr>");

                for (int i = 0; i < dbPopUp.Count(); i++)
                {
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<td class=""td_center""><input type=""checkbox"" name = ""checkAuth"" id='checkAuth_" + i + "'></td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].UserId + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].DutyNm + "</td>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbPopUp[i].UserNm + "</td>");
                    sb.AppendLine(@"</tr>");
                }

                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");
            }

            

            return Content(sb.ToString(), "text/html");
        }
        #endregion

    
        #region AuthRegistSuju : 임원공개대상 저장(POST)
        [HttpPost]
        public ActionResult AuthRegistSuju(string[] Arr, string pMeetTypeCd, string pRegNo, string pRegUserId, string pGubun)
        {
            string strErrorState = "";

            if (pGubun == "s")
            {
                strErrorState = "저장했습니다.";
            }
            else
            {
                strErrorState = "삭제했습니다.";
            }            

            List<PopUp> SujuExecutive = new List<PopUp>();

            for (int i = 0; i < Arr.Length; i++)
            {
                string[] arrSub = Arr[i].Split('/');

                SujuExecutive.Add(new PopUp()
                {
                    UserId = arrSub[0]
                });
            }


            try
            {
                _PopUpFactory.AuthRegistSuju(SujuExecutive, pMeetTypeCd, pRegNo, pRegUserId, pGubun);
            }
            catch (Exception e)
            {
                strErrorState = "Error";
            }

            return Json(strErrorState);

        }
        #endregion

    }

}

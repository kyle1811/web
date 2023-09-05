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
    public class SiteController : BaseController
    {
        SiteFactory _siteFactory = new SiteFactory();

        UserFactory _userFactory = new UserFactory();

        WeatherFactory _weatherFactory = new WeatherFactory();

        AsManageFactory _asManageFactory = new AsManageFactory();

        WebUtil.WebUtil _webUtil = new WebUtil.WebUtil();

        #region SiteState : 현장정보(전체)
        [HttpGet]
        public ActionResult SiteState()
        {
            // 현장현황(전체)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE010000", Session["browser"].ToString(), Session["device"].ToString());

            Site dbSite = _siteFactory.HolidaySafetyWorkPlan(Util.MakeDateTime("YearMon"));
            Session["WORKINGDAY"] = dbSite.WORKINGDAY;
            return View();
        }
        #endregion

        #region SiteStateListCivil : 현장목록(토목)
        [HttpGet]
        public ActionResult SiteStateListCivil(string pBizPartCd)
        {
            //현장현황(토목)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020000", Session["browser"].ToString(), Session["device"].ToString());

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

            // 해외현장사진 목록 조회
            /*List<Site> dbSite7 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E01");
            List<Site> dbSite8 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E02");
            List<Site> dbSite9 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E03");
            List<Site> dbSite10 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E04");*/

            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
                case "SA020105":
                    ViewBag.BizPartNm = "해외";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteStateListConstruct : 현장목록(건축)
        [HttpGet]
        public ActionResult SiteStateListConstruct(string pBizPartCd)
        {
            //현장현황(건축)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE030000", Session["browser"].ToString(), Session["device"].ToString());

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

            // 현장사진 목록 조회
            /*List<Site> dbSite1 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B01");
            List<Site> dbSite2 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B02");
            List<Site> dbSite3 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B03");
            List<Site> dbSite4 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B04");
            List<Site> dbSite5 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B05");

            // 현장사진 박스 생성
            if (dbSite1.Count() > 0)
            {
                ViewBag.SitePhoto1 = HtmlUtil.MakeBannerSiteBox(dbSite1, "bn_box2");
            }

            if (dbSite2.Count() > 0)
            {
                ViewBag.SitePhoto2 = HtmlUtil.MakeBannerSiteBox(dbSite2, "bn_box2");
            }

            if (dbSite3.Count() > 0)
            {
                ViewBag.SitePhoto3 = HtmlUtil.MakeBannerSiteBox(dbSite3, "bn_box2");
            }

            if (dbSite4.Count() > 0)
            {
                ViewBag.SitePhoto4 = HtmlUtil.MakeBannerSiteBox(dbSite4, "bn_box2");
            }

            if (dbSite5.Count() > 0)
            {
                ViewBag.SitePhoto5 = HtmlUtil.MakeBannerSiteBox(dbSite5, "bn_box2");
            }*/

            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
                case "SA020105":
                    ViewBag.BizPartNm = "해외";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteStateListOverseas : 현장목록(해외)
        [HttpGet]
        public ActionResult SiteStateListOverseas(string pBizPartCd)
        {
            // 현장사진 목록 조회
            List<Site> dbSite1 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E01");
            List<Site> dbSite2 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E02");
            List<Site> dbSite3 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E03");
            List<Site> dbSite4 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "E04");

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

            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
                case "SA020105":
                    ViewBag.BizPartNm = "해외";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteStateListPlant : 현장목록(플랜트)
        [HttpGet]
        public ActionResult SiteStateListPlant(string pBizPartCd)
        {
            //현장현황(플랜트)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE040000", Session["browser"].ToString(), Session["device"].ToString());

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

            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
                case "SA020105":
                    ViewBag.BizPartNm = "해외";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteStateDetail : 현장현황상세
        [HttpGet]
        public ActionResult SiteStateDetail(string pBizPartCd, string pSiteCd)
        {
            //현장현황(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020100", Session["browser"].ToString(), Session["device"].ToString());

            string chk = "";

            if (pBizPartCd == "KSIS")
            {
                chk = _siteFactory.listSiteStateYn(pBizPartCd, pSiteCd);
            }

            if (chk == "N")
            {
                return RouteUtil.Message("잘못누르셨습니다.");
            }

            // 현장정보 조회
            Site dbSite = _siteFactory.GetSiteInfo(pSiteCd);

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View(dbSite);
        }
        #endregion

        #region SiteWeatherDetail : 현장날씨현황상세
        [HttpGet]
        public ActionResult SiteWeatherDetail(string pBizPartCd, string pSiteCd)
        {
            // 현장정보 조회
            //Site dbSite = _siteFactory.GetSiteInfo(pSiteCd);

            //현장날씨정보(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE050100", Session["browser"].ToString(), Session["device"].ToString());

            //Weather dbWeather = _weatherFactory.GetSiteWeatherInfoDetail(pSiteCd);
            Weather dbWeather = _weatherFactory.GetSiteWeatherInfoDetail(pSiteCd);

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View(dbWeather);
        }
        #endregion

        #region SiteWeatherCivil : 현장날씨목록(토목)
        [HttpGet]
        public ActionResult SiteWeatherCivil(string pBizPartCd)
        {
            //현장날씨정보(토목)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE050000", Session["browser"].ToString(), Session["device"].ToString());

            // 현장사진 목록 조회
            List<Site> dbSite1 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A01");
            List<Site> dbSite2 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A02");
            List<Site> dbSite3 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A03");
            List<Site> dbSite4 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A04");
            List<Site> dbSite5 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A05");
            List<Site> dbSite6 = _siteFactory.ListSiteWeatherGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "A06");



            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteWeatherConstruct : 현장날씨목록(건축)
        [HttpGet]
        public ActionResult SiteWeatherConstruct(string pBizPartCd)
        {
            //현장날씨정보(건축)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE060000", Session["browser"].ToString(), Session["device"].ToString());

            // 현장사진 목록 조회
            List<Site> dbSite1 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B01");
            List<Site> dbSite2 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B02");
            List<Site> dbSite3 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B03");
            List<Site> dbSite4 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B04");
            List<Site> dbSite5 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "B05");

            // 현장사진 박스 생성
            if (dbSite1.Count() > 0)
            {
                ViewBag.SitePhoto1 = HtmlUtil.MakeBannerSiteBox(dbSite1, "bn_box2");
            }

            if (dbSite2.Count() > 0)
            {
                ViewBag.SitePhoto2 = HtmlUtil.MakeBannerSiteBox(dbSite2, "bn_box2");
            }

            if (dbSite3.Count() > 0)
            {
                ViewBag.SitePhoto3 = HtmlUtil.MakeBannerSiteBox(dbSite3, "bn_box2");
            }

            if (dbSite4.Count() > 0)
            {
                ViewBag.SitePhoto4 = HtmlUtil.MakeBannerSiteBox(dbSite4, "bn_box2");
            }

            if (dbSite5.Count() > 0)
            {
                ViewBag.SitePhoto5 = HtmlUtil.MakeBannerSiteBox(dbSite5, "bn_box2");
            }


            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region SiteWeatherPlant : 현장날씨목록(플랜트)
        [HttpGet]
        public ActionResult SiteWeatherPlant(string pBizPartCd)
        {
            //현장날씨정보(플랜트)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE070000", Session["browser"].ToString(), Session["device"].ToString());

            // 현장사진 목록 조회
            List<Site> dbSite1 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "D01");
            List<Site> dbSite2 = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, "D02");

            // 현장사진 박스 생성
            if (dbSite1.Count() > 0)
            {
                ViewBag.SitePhoto1 = HtmlUtil.MakeBannerSiteBox(dbSite1, "bn_box2");
            }

            if (dbSite2.Count() > 0)
            {
                ViewBag.SitePhoto2 = HtmlUtil.MakeBannerSiteBox(dbSite2, "bn_box2");
            }

            // 부문명 리턴
            switch (pBizPartCd)
            {
                case "SA020101":
                    ViewBag.BizPartNm = "토목";
                    break;
                case "SA020102":
                    ViewBag.BizPartNm = "건축";
                    break;
                case "SA020104":
                    ViewBag.BizPartNm = "플랜트";
                    break;
                case "SA020105":
                    ViewBag.BizPartNm = "해외";
                    break;
            }

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View();
        }
        #endregion

        #region Map : 지도로보기
        [HttpGet]
        public ActionResult Map()
        {
            //지도로보기
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE080000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region CompletionSite : 준공현황
        [HttpGet]
        public ActionResult CompletionSite(string pDeptCd)
        {
            //준공현황
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE090000", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.DeptCd = _SiteCd;
            ViewBag.BizPartCd = _BizPartCd;

            List<Manage> dbSite = _siteFactory.ListCompletionSiteTreeView(ViewBag.DeptCd);          

            // 트리뷰 생성
            ViewBag.TreeData = HtmlUtil.MakeTreeView(dbSite);

            return View();
        }
        #endregion

        #region ReCallCompletionSite : 준공현황
        [HttpPost]
        public ContentResult ReCallCompletionSite(string pSiteCd)
        {
            
            List<Manage> dbSite = _siteFactory.ListCompletionSiteTreeView(pSiteCd);

            string data = HtmlUtil.MakeTreeView(dbSite);

            return Content(data, "text/html");         
            
        }
        #endregion

        #region ListCompletionSiteInfoDetail : 준공현장 상세조회
        [HttpPost]
        public ContentResult ListCompletionSiteInfoDetail(string pSiteCd, string pSelect, string pBizpartCd)
        {
    
            List<Site> dbSite = _siteFactory.ListCompletionSiteInfoDetail(pSiteCd);

            StringBuilder sb = new StringBuilder("");

            if (pSelect == "area" && pBizpartCd == "SA020102")
            {
                
                sb.AppendLine(@"<table>");
                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:20%"" />");
                sb.AppendLine(@"<col style=""width:30%"" />");
                sb.AppendLine(@"<col style=""width:20%"" />");
                sb.AppendLine(@"<col style=""width:30%"" />");
                sb.AppendLine(@"<col />");
                sb.AppendLine(@"</colgroup>");
                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>지역/지구</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Area + "</td>");
                sb.AppendLine(@"<th>대지면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].LandArea + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>건축면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].ConstArea + "</td>");
                sb.AppendLine(@"<th>건폐율</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].BldgToLandRatio + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>연면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].GrossArea + "</td>");
                sb.AppendLine(@"<th>면세면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].TaxFreeArea + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>용적율적용면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].FlSpaceIndexDivertArea + "</td>");
                sb.AppendLine(@"<th>용적율</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].FlSpaceIndex + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>조경면적</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].GardeningArea + "</td>");
                sb.AppendLine(@"<th>조경율</th>");
                sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].GardeningRate + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");
            }
            else if (pSelect == "facility")
            {
                if(pBizpartCd == "SA020102")
                {
                    sb.AppendLine(@"<table>");
                    sb.AppendLine(@"<colgroup>");
                    sb.AppendLine(@"<col style=""width:20%"" />");
                    sb.AppendLine(@"<col style=""width:80%"" />");
                    sb.AppendLine(@"<col />");
                    sb.AppendLine(@"</colgroup>");
                    sb.AppendLine(@"<tbody>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>공사규모</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].BlockTy + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>주차대수</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Parkingcnt + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>기타사항</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Etc + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>구조</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Structure + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>외장</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Exterior + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>난방방식</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].HeatMeth + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"</tbody>");
                    sb.AppendLine(@"</table>");
                }
                else
                {
                    sb.AppendLine(@"<table>");
                    sb.AppendLine(@"<colgroup>");
                    sb.AppendLine(@"<col style=""width:20%"" />");
                    sb.AppendLine(@"<col style=""width:80%"" />");
                    sb.AppendLine(@"<col />");
                    sb.AppendLine(@"</colgroup>");
                    sb.AppendLine(@"<tbody>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>공사규모</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].BlockTy + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"<tr>");
                    sb.AppendLine(@"<th>기타사항</th>");
                    sb.AppendLine(@"<td class=""td_center"">" + dbSite[0].Etc + "</td>");
                    sb.AppendLine(@"</tr>");
                    sb.AppendLine(@"</tbody>");
                    sb.AppendLine(@"</table>");
                }                   

            }        
            else
            {
                foreach(Site f in dbSite) { 
                sb.AppendLine(@"<h1 style ='font-weight:bold; font-family:NanumGothic;font-size:20px;text-align:center;' >" + f.SiteName + "</h1>");
                }
            }

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region PrintMap : 지도로보기
        [HttpGet]
        public ActionResult PrintMap()
        {
            return View();
        }
        #endregion

        #region SiteStateTreeView : 현장정보트리형태(전체)
        [HttpGet]
        public ActionResult SiteStateTreeView()
        {
            // 현장현황(전체)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE010000", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.DeptCd = "ALL";

            List<Manage> dbSite = _siteFactory.ListCompletionSiteTreeView(ViewBag.DeptCd);

            // 트리뷰 생성
            ViewBag.TreeData = HtmlUtil.MakeTreeView(dbSite);

            return View();
        }
        #endregion       

        #region MakeSitePhotoGubun : 항목별 현장조회
        [HttpPost]
        public ContentResult MakeSitePhotoGubun(string pBizpartCd, string pProjetId)
        {

            List<Site> sitePhoto = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizpartCd, pProjetId);

            StringBuilder sb = new StringBuilder("");

            foreach (Site f in sitePhoto)
            {
                sb.Append(@"<a class='bn_box2'>"
                        + "<img class='lazy' src='/Contents/images/ex/loading.gif' data-src='" + f.SitePhotoUrl + "' />"
                        + "<span id='" + f.SiteCd + "'>" + f.SiteNm + "</a>"
                    );
            }

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region SiteStateTreeViewDetail : 현장정보트리형태(현장별 상세)
        [HttpGet]
        public ActionResult SiteStateTreeViewDetail(string pBizPartCd, string pSiteCd)
        {
            //현장현황(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE020100", Session["browser"].ToString(), Session["device"].ToString());

            string chk = "";

            if (pBizPartCd == "KSIS")
            {
                chk = _siteFactory.listSiteStateYn(pBizPartCd, pSiteCd);
            }

            if (chk == "N")
            {
                return RouteUtil.Message("잘못누르셨습니다.");
            }

            // 현장정보 조회
            Site dbSite = _siteFactory.GetSiteInfo(pSiteCd);

            // 부문코드 리턴
            ViewBag.BizPartCd = pBizPartCd;

            return View(dbSite);
        }
        #endregion


        /// 하자관리

        #region AsManage : 하자관리현황
        [HttpGet]
        public ActionResult AsManage()
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE100000", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.ddlSRVCNTR = _asManageFactory.ListAsServiceCenter();

            //string pSRVCNTR = "*";

            //AsManage dbAsManage = _asManageFactory.ListFisrtAsReceiptSummary(pSRVCNTR);

            //return View(dbAsManage);
            return View();
        }
        #endregion

        #region AsReceiptSummary : 하자관리현황 - 권역 조회 시 접수유형별 하자현황
        [HttpPost]
        public ContentResult AsReceiptSummary(string pSRVCNTR)
        {

            List<AsManage> dbAsManage = _asManageFactory.ListAsReceiptSummary(pSRVCNTR);
  
            StringBuilder sb = new StringBuilder("");
       
                sb.AppendLine(@"<table>");

                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:9%"" />");
                sb.AppendLine(@"<col style=""width:10%"" />");
                sb.AppendLine(@"<col />");
                sb.AppendLine(@"</colgroup>");

                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th colspan=""2"" class=""td_center"">방문</th>");
                sb.AppendLine(@"<th colspan=""2"" class=""td_center"">전화</th>");
                sb.AppendLine(@"<th colspan=""2"" class=""td_center"">공문</th>");
                sb.AppendLine(@"<th colspan=""2"" class=""td_center"">기타</th>");
                sb.AppendLine(@"<th colspan=""3"" class=""td_center"">계</th>");
                sb.AppendLine(@"</tr>");

                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th class=""td_center"">접수</th>");
                sb.AppendLine(@"<th class=""td_center"">완료</th>");
                sb.AppendLine(@"<th class=""td_center"">접수</th>");
                sb.AppendLine(@"<th class=""td_center"">완료</th>");
                sb.AppendLine(@"<th class=""td_center"">접수</th>");
                sb.AppendLine(@"<th class=""td_center"">완료</th>");
                sb.AppendLine(@"<th class=""td_center"">접수</th>");
                sb.AppendLine(@"<th class=""td_center"">완료</th>");
                sb.AppendLine(@"<th class=""td_center"">접수</th>");
                sb.AppendLine(@"<th class=""td_center"">완료</th>");
                sb.AppendLine(@"<th class=""td_center"">처리율(%)</th>");
                sb.AppendLine(@"</tr>");

                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].RCPTV + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].FINISHV + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].RCPTP + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].FINISHP + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].RCPTD + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].FINISHD + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].RCPTE + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].FINISHE + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].RCPTSUM + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].FINISHSUM + "</td>");
                sb.AppendLine(@"<td class=""td_right"">" + dbAsManage[0].HNDRATE + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");

            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region AsManageDetail : 현장별 하자현황
        [HttpGet]
        public ActionResult AsManageDetail(string pPJTCD, string pPJTNM, string pCNT)
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "SE100100", Session["browser"].ToString(), Session["device"].ToString());

            ViewBag.PJTCD = pPJTCD;
            ViewBag.PJTNM = pPJTNM;
            ViewBag.CNT = pCNT;

            ViewBag.ddlPROGSTEP = _asManageFactory.ListAsStep(pPJTCD);
            

            return View();
        }
        #endregion



    }

}

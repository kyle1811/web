using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Web.Routing;
using System.Collections.Specialized;
using Microsoft.Web.Html.Utils;

namespace PT.Web.Controllers
{
    public class InformationController : BaseController
    {
        AttFileFactory _attFileFactory = new AttFileFactory();
        MeetingFactory _meetingFactory = new MeetingFactory();
        AuctionFactory _AuctionFactory = new AuctionFactory();
        CommunityFactory _CommunityFactory = new CommunityFactory();
        UserFactory _userFactory = new UserFactory();
        UsedSaleFactory _usedSaleFactory = new UsedSaleFactory();
        WebUtil.WebUtil _webUtil = new WebUtil.WebUtil();

        #region Auction : 경매
        [HttpGet]
        public ActionResult Auction()
        {
            string returnVal = _webUtil.CheckAuthInfo("IN020000", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("진행중인 경매가 없습니다.");
            }
            // 경매타입 리턴
           // ViewBag.AuctionTypeCd = pItemNm;

            //경매(진행중인경매)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN020000", Session["browser"].ToString(), Session["device"].ToString());
            return View();
        }
        #endregion

        #region AuctionDetail : 진행중인경매상세
        [HttpGet]
        public ActionResult AuctionDetail(string pAuctionNo, string pAuctionTypeCd)
        {
            //경매(진행중인경매상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN020100", Session["browser"].ToString(), Session["device"].ToString());

            Auction dbAuction = _AuctionFactory.GetAuction(pAuctionNo);

            dbAuction.ServerTime = Convert.ToDateTime(dbAuction.ServerTime).ToString(@"yyyy\/MM\/dd HH:mm:ss");
            dbAuction.CloseDate  = Convert.ToDateTime(dbAuction.CloseDate).ToString(@"yyyy\/MM\/dd HH:mm:ss");
            dbAuction.AuctionTypeCd = pAuctionTypeCd;

            //regNo = _AuctionFactory.SaveComment(pComment, _UserId);
            
            return View(dbAuction);
        }
        #endregion

        #region AuctionDetail : POST 저장
        [HttpPost]
        public ActionResult AuctionDetail(Auction pAuction)
        {
            string comment = "";
            string price = "";

            try
            {
                if (pAuction.SaveMode == "P") // 입찰금액 저장
                {
                    price = _AuctionFactory.SavePrice(pAuction, _UserId);
                }
                else if (pAuction.SaveMode == "C") // 댓글 저장
                {
                    comment = _AuctionFactory.SaveComment(pAuction, _UserId); 
                }                
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Information/AuctionDetail?pAuctionNo=" + pAuction.AuctionNo + "&pAuctionTypeCd=" + pAuction.AuctionTypeCd);
            }

            if (pAuction.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Information/AuctionDetail?pAuctionNo=" + pAuction.AuctionNo + "&pAuctionTypeCd=" + pAuction.AuctionTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Information/AuctionDetail?pAuctionNo=" + pAuction.AuctionNo + "&pAuctionTypeCd=" + pAuction.AuctionTypeCd);
            }
        }
        #endregion

        #region AuctionComplete : 완료된경매
        [HttpGet]
        public ActionResult AuctionComplete()
        {
            //경매(참여한경매)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN050000", Session["browser"].ToString(), Session["device"].ToString());
                       
            return View();
        }
        #endregion

        #region AuctionBidding : 참여한경매
        [HttpGet]
        public ActionResult AuctionBidding()
        {
            //경매(참여한경매)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN030000", Session["browser"].ToString(), Session["device"].ToString());

            //Auction dbAuction = _AuctionFactory(

            return View();
        }
        #endregion

        #region AuctionSelling : 경매관리자
        [HttpGet]
        public ActionResult AuctionSelling()
        {
            //경매(경매관리자)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN040000", Session["browser"].ToString(), Session["device"].ToString());
            //Auction dbAuction = _AuctionFactory(
            string returnVal = _webUtil.CheckAuthInfo("IN040000", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }
            return View();            
        }
        #endregion

        #region AuctionRegistration : 물품등록
        [HttpGet]
        public ActionResult AuctionRegistration(string pAuctionNo)
        {
            //경매(물품등록추가)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN030100", Session["browser"].ToString(), Session["device"].ToString());

            Auction dbAuction = new Auction();

            // 신규작성
            if (pAuctionNo == "i")
            {
                dbAuction.RegNo     = "";
                dbAuction.RegUserNm = _UserNm;
                dbAuction.RegDate   = Util.MakeDateTime("YearMonDay");
                dbAuction.StartDateYear  = Util.MakeDateTime("Year");
                dbAuction.StartDateMonth = Util.MakeDateTime("Mon");
                dbAuction.StartDateDay   = Util.MakeDateTime("Day");
                //dbAuction.StartDateHour  = Util.MakeDateTime("Hour");
                dbAuction.StartDateHour   = DateTime.Now.ToString("HH");
                dbAuction.StartDateMinute = DateTime.Now.ToString("mm");
                dbAuction.CloseDateYear  = Util.MakeDateTime("Year");
                dbAuction.CloseDateMonth = Util.MakeDateTime("Mon");
                dbAuction.CloseDateDay   = Util.MakeDateTime("Day");
                dbAuction.CloseDateHour   = DateTime.Now.ToString("HH");
                dbAuction.CloseDateMinute = DateTime.Now.ToString("mm");
                dbAuction.SellQuantity = "1";
                dbAuction.AuctionNo = " ";
            }
            else
            {
                // 경매등록 자료 조회
                dbAuction = _AuctionFactory.GetRegAuction(pAuctionNo);

                if (dbAuction.StartDateMonth.Length < 2)
	            {
                    dbAuction.StartDateMonth = '0' + dbAuction.StartDateMonth;
	            }
                if (dbAuction.StartDateDay.Length < 2)
                {
                    dbAuction.StartDateDay = '0' + dbAuction.StartDateDay;
                }
                if (dbAuction.CloseDateMonth.Length < 2)
                {
                    dbAuction.CloseDateMonth = '0' + dbAuction.CloseDateMonth;
                }
                if (dbAuction.CloseDateDay.Length < 2)
                {
                    dbAuction.CloseDateDay = '0' + dbAuction.CloseDateDay;
                }

                if (dbAuction.AuctionRegUserId != _UserId)
                {
                    return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Information/AuctionSelling");
                }
            }

            return View(dbAuction);
        }
        #endregion

        #region AuctionRegistrationError: 물품등록 저장오류시 입력 데이타 복구
        [HttpGet]
        public ActionResult AuctionRegistrationError(Auction pAuction)
        {
            return View(pAuction);
        }
        #endregion

        #region AuctionRegistration : 물품등록 저장(POST)
        [HttpPost]
        public ActionResult AuctionRegistration(Auction pAuction, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            string registrationAuctionNo = "";

            try
            {
                if (pAuction.SaveMode == "R") // 물품등록
                {
                    
                    if (pAuction.AttId == null)
                    {
                        // 첨부파일 신규
                        pAuction.AttId = _attFileFactory.SaveResizePhoto(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                    }
                    else
                    {
                        // 첨부파일 수정
                        _attFileFactory.SaveResizePhoto(pAuction.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);
                    }
                    
                    registrationAuctionNo = _AuctionFactory.SaveAuction(pAuction, _UserId);
                }

                if (pAuction.SaveMode == "N")
                {
                    registrationAuctionNo = _AuctionFactory.SavePostNotice(pAuction, _UserId);
                }

                if (pAuction.SaveMode == "C")
                {
                    registrationAuctionNo = _AuctionFactory.SaveAuctionStatusCancel(pAuction, _UserId);
                }

                if (pAuction.SaveMode == "D")
                {
                    registrationAuctionNo = _AuctionFactory.deleteAuctionRegistration(pAuction, _UserId);
                }
                if (pAuction.SaveMode == "W")
                {
                    registrationAuctionNo = _AuctionFactory.closeAuction(pAuction, _UserId);
                }
            }
            catch (Exception e)
            {
                //return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Information/AuctionSelling");

                pAuction.errormsg = Util.MakeMessage(e);

                return AuctionRegistrationError(pAuction);
            }

            if (pAuction.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Information/AuctionSelling");
            }
            else if (pAuction.SaveMode == "N")
            {
                return RouteUtil.MessageAndMove("입찰공고 하였습니다.", "/Information/AuctionRegistration?pAuctionNo=" + pAuction.AuctionNo);
            }
            else if (pAuction.SaveMode == "C")
            {
                return RouteUtil.MessageAndMove("취소 하였습니다.", "/Information/AuctionRegistration?pAuctionNo=" + pAuction.AuctionNo);
            }
            else if (pAuction.SaveMode == "W")
            {
                return RouteUtil.MessageAndMove("낙찰 완료하였습니다.", "/Information/AuctionRegistration?pAuctionNo=" + pAuction.AuctionNo);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Information/AuctionRegistration?pAuctionNo=" + registrationAuctionNo);
            }
        }
        #endregion

        #region Magazine : 사보
        [HttpGet]
        public ActionResult Magazine()
        {
            //사보
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN010000", Session["browser"].ToString(), Session["device"].ToString());

            List<Community> dbMagzine = _CommunityFactory.ListMagazinePhotoUrl(Util.MakeDateTime("YearMon"));

            if (dbMagzine.Count() > 0)
            {
                ViewBag.MagazinePhoto = HtmlUtil.MakeBannerSiteBox(dbMagzine, "bn_MagazineBox");

            }

            return View();
        }
        #endregion

        #region ListChangeMagazine : 사보목록 조회

        [HttpPost]
        public JsonResult ListChangeMagazine(string pGubun)
        {
            // 사보목록 조회
            List<Community> dbCommunity = _CommunityFactory.ListChangeMagazine(pGubun);

            var data = dbCommunity.Select(m => new
            {
                RowNum = m.RowNum,
                RowGroup = m.RowGroup,
                MagazinePdfUrl = m.MagazinePdfUrl,
                MagazineNm = m.MagazineNm,
                MagazineJpgUrl = m.MagazineJpgUrl
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UsedSale : 물품나눔

        #region UsedSale : 진행중인나눔 페이지 조회
        [HttpGet]
        public ActionResult UsedSale()
        {           
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN050000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region UsedSaleDetail : 물품 상세조회 페이지 호출
        [HttpGet]
        public ActionResult UsedSaleDetail(string pUsedSaleNo, string pRegUser, string pStatus, string pUserId)
        {
            ViewBag.UsedSaleNo = pUsedSaleNo;
            ViewBag.RegUser = pRegUser;
            ViewBag.Status = pStatus;

            if (pStatus == "C")
            {
                _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN060100", Session["browser"].ToString(), Session["device"].ToString());
            }
            else
            {
                _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN050100", Session["browser"].ToString(), Session["device"].ToString());
            }            

            UsedSale dbUsedSale = _usedSaleFactory.ListUsedSaleDetail(pUsedSaleNo, pStatus, pUserId);

            return View(dbUsedSale);
        }
        #endregion

        #region ListUsedSaleDetailOther : 물품 상세조회 페이지 그리드(유동적)
        [HttpPost]
        public ContentResult ListUsedSaleDetailOther(string pUsedSaleNo, string pGubun, string pStatus)
        {

            List<UsedSale> dbUsedSale = _usedSaleFactory.ListUsedSaleDetailOther(pUsedSaleNo, pGubun, pStatus);

            StringBuilder sb = new StringBuilder("");
            StringBuilder ob = new StringBuilder("");
            StringBuilder cb = new StringBuilder("");

            if (pGubun == "detail") { 
                sb.AppendLine(@"<table>");
                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:15%"" />");
                sb.AppendLine(@"<col style=""width:35%"" />");
                sb.AppendLine(@"<col style=""width:15%"" />");
                sb.AppendLine(@"<col style=""width:35%"" />");
                sb.AppendLine(@"<col />");
                sb.AppendLine(@"</colgroup>");
                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>제목</th>");
                sb.AppendLine(@"<td colspan='3'>" + dbUsedSale[0].ItemNm + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>판매가격</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].Price + "</td>");
                sb.AppendLine(@"<th>사용기간</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].UsePeriod + "</td>");        
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>판매자</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].RegUserNm + '(' + dbUsedSale[0].DeptNm + ")" + "</td>");
                sb.AppendLine(@"<th>연락처</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].TelNo + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<th>거래방법</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].TradeWay + "</td>");
                sb.AppendLine(@"<th>배송방법</th>");
                sb.AppendLine(@"<td>" + dbUsedSale[0].ShippingMethod + "</td>");
                sb.AppendLine(@"</tr>");                           
                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");

                sb.AppendLine(@"<br>");
                sb.AppendLine(@"<table>");
                sb.AppendLine(@"<colgroup>");
                sb.AppendLine(@"<col style=""width:100%"" />");
                sb.AppendLine(@"<col />");
                sb.AppendLine(@"</colgroup>");
                sb.AppendLine(@"<tbody>");
                sb.AppendLine(@"<tr>");                
                sb.AppendLine(@"<td style='height:100px;'>" + dbUsedSale[0].MoreInfo + "</td>");
                sb.AppendLine(@"</tr>");
                sb.AppendLine(@"</tbody>");
                sb.AppendLine(@"</table>");
                sb.AppendLine(@"<br>");            

            foreach (UsedSale f in dbUsedSale)
                {
                    sb.AppendLine(@"<div style='width:100%;text-align:center;'>");
                    sb.AppendLine(@"<img style='width:500px;height:300px;' src ='" + f.ImgSrc + "' />");
                    sb.AppendLine(@"</div>");
                }      
                               
            }
            sb.AppendLine(@"<br>");

            if (dbUsedSale.Count > 0)
            {
                ob.AppendLine(@"<h1 style='padding-left:15px;background:url(/Contents/images/icon/icon_title1.png) no-repeat left center; font-size:15px;color:#121212;'>댓글 " + dbUsedSale[0].Comment + "</h1>");
            }
            else
            {
                ob.AppendLine(@"<h1 style='padding-left:15px;background:url(/Contents/images/icon/icon_title1.png) no-repeat left center; font-size:15px;color:#121212;'>댓글 0</h1>");
            }
                
            ob.AppendLine(@"<br>");            

            if (dbUsedSale.Count > 0)
            {
                ob.AppendLine(@"<div style='height:auto;overflow:auto;max-height:400px;'>");

                ob.AppendLine(@"<table>");
                ob.AppendLine(@"<colgroup>");
                ob.AppendLine(@"<col style=""width:15%"" />");
                ob.AppendLine(@"<col style=""width:65%"" />");
                ob.AppendLine(@"<col style=""width:20%"" />");
                ob.AppendLine(@"<col />");
                ob.AppendLine(@"</colgroup>");
                ob.AppendLine(@"<tbody>");
                ob.AppendLine(@"<tr>");
                ob.AppendLine(@"<th style='font-family:돋움;color:#000000;'>작성자</th><th style='font-family:돋움;color:#000000;'>댓글</th><th style='font-family:NanumGothic;color:#000000;'>작성일자</th>");
                ob.AppendLine(@"</tr>");

                foreach (UsedSale f in dbUsedSale)
                {                   
                    ob.AppendLine(@"<tr>");
                    ob.AppendLine(@"<th style='font-weight:normal;font-family:NanumGothic;background-color:#ffffff;color:#000000;'>" + f.CommentUserNm + "</th><th style='font-weight:normal;font-family:NanumGothic;background-color:#ffffff;color:#000000;'>" + f.CommentText + "</th><th style='font-weight:normal;family:NanumGothic;background-color:#ffffff;color:#000000;'>" + f.CommentRegDate + "</th>");
                    ob.AppendLine(@"</tr>");                  
                }
                ob.AppendLine(@"</tbody>");
                ob.AppendLine(@"</table>");
                ob.AppendLine(@"</div>");
            }
            ob.AppendLine(@"<br>");
            ob.AppendLine(@"<table>");
            ob.AppendLine(@"<colgroup>");
            ob.AppendLine(@"<col style=""width:15%"" />");
            ob.AppendLine(@"<col style=""width:65%"" />");
            ob.AppendLine(@"<col style=""width:10%"" />");
            ob.AppendLine(@"<col style=""width:10%"" />");
            ob.AppendLine(@"<col />");
            ob.AppendLine(@"</colgroup>");
            ob.AppendLine(@"<tbody>");
            ob.AppendLine(@"<tr>");
            ob.AppendLine(@"<th>댓글작성</th>");
            ob.AppendLine(@"<td><textarea id='CommentText' name='CommentText' rows='5' style = 'width: 638px;' placeholder='메시지를 입력하세요' ></textarea></td>");
            ob.AppendLine(@"<td colspan='2' class='td_center'><a id = 'btn_enroll' class='btn1'>등록</a></td>");
            ob.AppendLine(@"</tr>");
            ob.AppendLine(@"</tbody>");
            ob.AppendLine(@"</table>");
            ob.AppendLine(@"<br>");

            if (pGubun == "detail")
            {
                cb = sb;
            }
            else
            {
                cb = ob;
            }


            return Content(cb.ToString(), "text/html");
        }
        #endregion

        #region GetOnProgress : 진행중인나눔 그리드 조회

        [HttpPost]
        public ContentResult GetOnProgress(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSubject, string pOption)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 9] {
                    { "UsedSaleNo", "UsedSaleSeq", "ItemNm", "RegUserNm", "RegUserMail", "RegDate", "Views", "Comment", "RegUser"},
                    { "0%","10%", "40%", "15%", "0%", "15%", "10%", "10%", "0%"},
                    { "등록번호","번호", "물품명", "작성자", "이메일", "작성일", "조회수", "댓글수", "사번"},
                    { "Hide","Show", "Show", "Show", "Hide", "Show", "Show", "Show", "Hide"},
                    { "Center","Center", "Left", "Center", "Center", "Center", "Center", "Center", "Center"},
                    { "None","None", "None", "None", "None", "None", "None", "None", "None"}
            };

            // 진행중인경매 목록 그리드 조회
            List<UsedSale> dbGetOnProgress = _usedSaleFactory.GetOnProgress(pSubject, pOption);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbGetOnProgress, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pSubject;

            return Content(data, "text/html");
        }
        #endregion

        #region ListUsedSaleComment : 댓글 조회
        [HttpPost]
        public ContentResult ListUsedSaleComment(string pUsedSaleNo)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 4] {
                    { "CommentSeq", "CommentUserNm", "CommentText", "CommentRegDate" },
                    { "0%", "15%", "65%", "20%" },
                    { "댓글번호", "작성자", "댓글", "작성일자" },
                    { "Hide", "Show", "Show", "Show" },
                    { "Center", "Center", "Left", "Center", },
                    { "None", "None", "None", "None" }
            };

            // 댓글 목록 그리드 조회
            List<UsedSale> dbUsedSale = _usedSaleFactory.ListUsedSaleComment(pUsedSaleNo);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbUsedSale, "N", 0, 0);

            return Content(data, "text/html");
        }
        #endregion

        #region ListUsedSaleCommentOther : 댓글 조회
        [HttpPost]
        public ContentResult ListUsedSaleCommentOther(string pUsedSaleNo, string pUserId, string pStatus)
        {
            List<UsedSale> dbUsedSale = _usedSaleFactory.ListUsedSaleCommentOther(pUsedSaleNo, pUserId, pStatus);

            StringBuilder sb = new StringBuilder("");

            sb.AppendLine(@"<table>");
            sb.AppendLine(@"<colgroup>");
            sb.AppendLine(@"<col style=""width:0%"" />");
            sb.AppendLine(@"<col style=""width:0%"" />");
            sb.AppendLine(@"<col style=""width:15%"" />");
            sb.AppendLine(@"<col style=""width:60%"" />");
            sb.AppendLine(@"<col style=""width:20%"" />");
            sb.AppendLine(@"<col style=""width:5%"" />");
            sb.AppendLine(@"<col />");
            sb.AppendLine(@"</colgroup>");
            sb.AppendLine(@"<tbody>");
            sb.AppendLine(@"<tr>");
            sb.AppendLine(@"<th colspan = '3' >작성자</th><th>댓글</th><th colspan = '2' >작성일자</th>");
            sb.AppendLine(@"</tr>");

            foreach (UsedSale f in dbUsedSale)
            {
                sb.AppendLine(@"<tr>");
                sb.AppendLine(@"<td style='visibility:hidden;width:0px;height:0px;' class='td_center'>" + f.CommentSeq + "</td><td style='visibility:hidden;width:0px;height:0px;' class='td_center'>" + f.CommentUserId + "</td><td class='td_center'>" + f.CommentUserNm + "</td><td class='td_left'>" + f.CommentText + "</td><td colspan = '2' class='td_center'>" + f.CommentRegDate);
                    if(f.DeleteYn == "X")
                    {
                        sb.AppendLine(@"<a id='btn_CommentDel' class='btn4'>" + f.DeleteYn + "</a></td>");
                    }
                    else
                    {
                        sb.AppendLine(@"&nbsp;</td>");
                    }                    
                sb.AppendLine(@"</tr>");
            }
            sb.AppendLine(@"</tbody>");
            sb.AppendLine(@"</table>");
            

            return Content(sb.ToString(), "text/html");            
        }
        #endregion

        #region SaveUsedSaleComment : 댓글등록
        [HttpPost]
        public ActionResult SaveUsedSaleComment(string pUsedSaleNo, string pCommentText, string pUserId)
        {
            string Message = "";

            Message = _usedSaleFactory.SaveUsedSaleComment(pUsedSaleNo, pCommentText, pUserId);
          
            return Content(Message, "text/html");
        }
        #endregion 

        #region DeleteUsedSaleComment : 댓글삭제
        [HttpPost]
        public ActionResult DeleteUsedSaleComment(string pUsedSaleNo, string pCommentSeq, string pUserId)
        {
            string Message = "";
            
            Message = _usedSaleFactory.DeleteUsedSaleComment(pUsedSaleNo, pCommentSeq, pUserId);

            return Content(Message, "text/html");
        }
        #endregion 

        #region UsedSaleCompleted : 나눔완료 페이지 조회
        [HttpGet]
        public ActionResult UsedSaleCompleted()
        {            
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN060000", Session["browser"].ToString(), Session["device"].ToString());

            return View();
        }
        #endregion

        #region GetOnComplete : 나눔완료 그리드 조회

        [HttpPost]
        public ContentResult GetOnComplete(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pSubject, string pUserId, string pOption)
        {
            // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
            string[,] pArrHeader = new string[6, 6] {
                    { "UsedSaleNo", "UsedSaleSeq", "ItemNm", "RegUserNm", "RegDate", "RegUser"},
                    { "0%", "10%", "40%", "15%", "15%", "0%"},
                    { "등록번호", "번호", "물품명", "작성자", "작성일", "사번"},
                    { "Hide", "Show", "Show", "Show", "Show", "Hide"},
                    { "Center", "Center", "Left", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None"}
            };

            // 진행중인경매 목록 그리드 조회
            List<UsedSale> dbGetOnComplete = _usedSaleFactory.GetOnComplete(pSubject, pUserId, pOption);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbGetOnComplete, pPagerUseYn, pCurPage, pListCnt);

            data = data + "‡" + pSubject;

            return Content(data, "text/html");
        }
        #endregion

        #region SaveUsedSaleComplete : 판매완료처리
        [HttpPost]
        public ActionResult SaveUsedSaleComplete(string pUsedSaleNo, string pUserId, string pStatus)
        {
            string Message = "";

            Message = _usedSaleFactory.SaveUsedSaleComplete(pUsedSaleNo, pUserId, pStatus);

            return Content(Message, "text/html");
        }
        #endregion

        #region UsedSaleRegistration : 나눔물품 등록 조회
        [HttpGet]
        public ActionResult UsedSaleRegistration(string pSaveMode, string pUsedSaleNo, string pUserId)
        {
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "IN070000", Session["browser"].ToString(), Session["device"].ToString());

            //UsedSale dbUser = _AdminFactory.GetUser(pUserId, pSEQ);
            //dbUser.RegUserNm = _UserNm;
            //dbUser.RegDate = Util.MakeDateTime("YearMonDay");

            UsedSale dbUsedSale = new UsedSale();

            dbUsedSale.RegUserId = "";
            dbUsedSale.SaveMode = pSaveMode;

            if(pSaveMode == "U")
            {
                dbUsedSale = _usedSaleFactory.ListUsedSaleDetail(pUsedSaleNo, "U", pUserId);
            }

            return View(dbUsedSale);
        }
        #endregion

        #region UsedSaleRegistration : 나눔물품 등록 
        [HttpPost]
        public ActionResult UsedSaleRegistration(UsedSale pUsedSale, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {            
            try
            {
                if (pUsedSale.AttId == null)
                {
                    pUsedSale.AttId = _attFileFactory.SaveResizePhoto(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                }
                else
                {
                    // 첨부파일 수정
                    _attFileFactory.SaveResizePhoto(pUsedSale.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);

                }

                _usedSaleFactory.SaveUsedSale(pUsedSale);

            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Information/UsedSale");
            }
            if(pUsedSale.SaveMode == "U")
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Information/UsedSaleDetail?pUsedSaleNo=" + pUsedSale.UsedSaleNo + "&pRegUser=" + pUsedSale.RegUserId + "&pStatus=P");
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Information/UsedSale");
            }
            
           
        }
        #endregion

        #region DeleteUsedSaleItem : 나눔물품 삭제
        [HttpPost]
        public ActionResult DeleteUsedSaleItem(string pUsedSaleNo, string pAttId)
        {
            string Message = "";

            _attFileFactory.DeleteFileAll(pAttId);

            Message = _usedSaleFactory.DeleteUsedSaleItem(pUsedSaleNo, pAttId);            

            return Content(Message, "text/html");
        }
        #endregion 
        #endregion

    }
}

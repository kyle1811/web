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
    
    public class AdminController : BaseController
    {
        AdminFactory _AdminFactory = new AdminFactory();
        UserFactory _userFactory = new UserFactory();
        MeetingFactory _meetingFactory = new MeetingFactory();
        AttFileFactory _attFileFactory = new AttFileFactory();

        #region AuthControl : 권한 관리
        [HttpGet]
        public ActionResult AuthControl()
        {
            ViewBag.MenuCd = "Home";

            List<Admin> dbAdmin = _AdminFactory.ListAuthControlTreeView(ViewBag.MenuCd);

            // 트리뷰 생성
            ViewBag.TreeData = HtmlUtil.MakeTreeView(dbAdmin);

            return View();
        }
        #endregion

        #region ListAuthStatus : 권한 현황 그리드

        [HttpPost]
        public ContentResult ListAuthStatus(string pPagerUseYn, decimal pCurPage, decimal pListCnt, string pMenuCd, string pGubun)
        {

            string[,] pArrHeader = new string[6, 7];

            if (pGubun == "User")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 7] {
                    { "Menu","MenuNm","AuthCd", "DutyCd", "DutyNm", "AuthNm", "RegDate"},
                    { "0%", "20%", "20%", "0%", "20%","20%", "20%"},
                    { "화면코드", "화면명", "사번", "직급코드", "직급", "사용자", "등록일"},
                    { "Hide", "Show", "Show", "Hide", "Show", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None", "None", "None"}
                };

            }
            else if (pGubun == "Dept")
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 5] {
                    { "Menu","MenuNm","AuthCd", "AuthNm", "RegDate"},
                    { "20%", "20%", "20%", "20%", "20%"},
                    { "화면코드", "화면명", "권한자코드", "부서", "등록일"},
                    { "Hide", "Show", "Hide", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None"}
                };
            }
            else
            {
                // 그리드해더 (아이디, 넓이, 명칭, 숨기기, 정렬, 언더라인)
                pArrHeader = new string[6, 5] {
                    { "Menu","MenuNm","AuthCd", "AuthNm", "RegDate"},
                    { "20%", "20%", "20%", "20%", "20%"},
                    { "화면코드", "화면명", "권한자코드", "직급/직책", "등록일"},
                    { "Hide", "Show", "Hide", "Show", "Show"},
                    { "Center", "Center", "Center", "Center", "Center"},
                    { "None", "None", "None", "None", "None"}
                };
            }

            List<Admin> dbAdmin = _AdminFactory.ListAuthStatus(pMenuCd, pGubun);

            // 그리드 생성
            string data = HtmlUtil.MakeGrid(pArrHeader, dbAdmin, pPagerUseYn, pCurPage, pListCnt);

            return Content(data, "text/html");
        }
        #endregion

        #region NoticeControl : 공지사항관리
        [HttpGet]
        public ActionResult NoticeControl()
        {
            return View();
        }
        #endregion

        #region NoticeControlEdit : 공지사항작성(GET)
        [HttpGet]
        public ActionResult NoticeControlEdit(string pMeetTypeCd, string pSaveMode, string pRegNo)
        {
            Meeting dbMeeting = new Meeting();

            // 신규작성
            if (pSaveMode == "C")
            {
                dbMeeting.RegNo = "";
                dbMeeting.MeetTypeCd = pMeetTypeCd;
                dbMeeting.RegUserNm = _UserNm;
                dbMeeting.RegDate = Util.MakeDateTime("YearMonDay");
                dbMeeting.SaveMode = pSaveMode;
            }
            else
            {
                // 회의자료 조회
                dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);
                dbMeeting.SaveMode = pSaveMode;

                if (pMeetTypeCd != "PT001011")
                {
                    if (dbMeeting.RegUserId != _UserId)
                    {
                        return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Admin/NoticeControlDetail?pMeetTypeCd=" + pMeetTypeCd + "&pRegNo=" + pRegNo);
                    }
                }


            }

            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View(dbMeeting);
        }
        #endregion

        #region NoticeControlEdit : 공지사항작성(POST)
        [HttpPost]
        public ActionResult NoticeControlEdit(Meeting pMeeting, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            string regNo = "";
                      
            try
            {
                if (pMeeting.AttId == null)
                {
                    // 첨부파일 신규
                    pMeeting.AttId = _attFileFactory.SaveFiles(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                }
                else
                {
                    // 첨부파일 수정
                    _attFileFactory.SaveFiles(pMeeting.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);
                }

                // 회의자료 저장
                regNo = _meetingFactory.SaveMeeting(pMeeting, _UserId);
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Admin/NoticeControlEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pMeeting.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Admin/NoticeControl?pMeetTypeCd=" + pMeeting.MeetTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Admin/NoticeControlEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region NoticeControlDetail : 공지사항상세
        [HttpGet]
        public ActionResult NoticeControlDetail(string pMeetTypeCd, string pRegNo)
        {
            // 공지사항 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            return View(dbMeeting);
        }
        #endregion

    }
}

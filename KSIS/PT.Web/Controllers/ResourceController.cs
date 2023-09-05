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


namespace PT.Web.Controllers
{
    public class ResourceController : BaseController
    {
        AttFileFactory _attFileFactory = new AttFileFactory();
        MeetingFactory _meetingFactory = new MeetingFactory();
        WebUtil.WebUtil _webUtil = new WebUtil.WebUtil();
        UserFactory _userFactory = new UserFactory();

        #region Meeting : 회의자료
        [HttpGet]
        public ActionResult Meeting(string pMeetTypeCd)
        {
            //회의자료
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "RE010000", Session["browser"].ToString(), Session["device"].ToString());

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;          

            return View();
        }
        #endregion

        #region MeetingEdit : 회의자료작성(GET)
        [HttpGet]
        public ActionResult MeetingEdit(string pMeetTypeCd, string pSaveMode, string pRegNo)
        {
            //회의자료(작성)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "RE010200", Session["browser"].ToString(), Session["device"].ToString());

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
                        return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Resource/MeetingDetail?pMeetTypeCd=" + pMeetTypeCd + "&pRegNo=" + pRegNo);
                    }
                }


            }

            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View(dbMeeting);
        }
        #endregion

        #region MeetingEdit : 회의자료작성(POST)
        [HttpPost]
        public ActionResult MeetingEdit(Meeting pMeeting, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        { 
            string regNo = "";

            try
            {
                if (pMeeting.AttId == null)
                {
                    // 첨부파일 신규
                    if(pMeeting.MeetTypeCd == "RE100000" || pMeeting.MeetTypeCd == "RE170000")
                    {
                        pMeeting.AttId = _attFileFactory.SaveFilesMid(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                    }
                    else
                    {
                        pMeeting.AttId = _attFileFactory.SaveFiles(pAttIdFiles, ModuleCd.PT, RouteData, _UserId);
                    }                                         
                }
                else
                {
                    // 첨부파일 수정
                    if (pMeeting.MeetTypeCd == "RE100000" || pMeeting.MeetTypeCd == "RE170000")
                    {
                        _attFileFactory.SaveFilesMid(pMeeting.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);
                    }
                    else
                    {
                        _attFileFactory.SaveFiles(pMeeting.AttId, Request["pAttIdSavedFileSeqs"], pAttIdFiles, ModuleCd.PT, _UserId);
                    }                   
                        
                }

                // 회의자료 저장
                regNo = _meetingFactory.SaveMeeting(pMeeting, _UserId);
            }
            catch (Exception e)
            {
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Resource/MeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pMeeting.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Resource/Meeting?pMeetTypeCd=" + pMeeting.MeetTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Resource/MeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region MeetingDetail : 회의자료상세
        [HttpGet]
        public ActionResult MeetingDetail(string pMeetTypeCd, string pRegNo)
        {
            //회의자료(상세)
            _userFactory.CreateLoginLogPlus(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "RE010100", Session["browser"].ToString(), Session["device"].ToString());

            // 회의자료 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View(dbMeeting);
        }
        #endregion

        #region CompanyManageMeeting : 전사운영회의자료
        [HttpGet]
        public ActionResult CompanyManageMeeting(string pMeetTypeCd)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "전사운영회의");

            string returnVal = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View();
        }
        #endregion

        #region DepartmentLeaderMeeting : 부서장 회의자료
        [HttpGet]
        public ActionResult DepartmentLeaderMeeting(string pMeetTypeCd)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "부서장회의");

            string returnVal = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View();
        }
        #endregion

        #region DirectorsMeeting : 임원회의자료
        [HttpGet]
        public ActionResult DirectorsMeeting(string pMeetTypeCd)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "임원회의");

            string returnVal = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            if (returnVal == "N")
            {
                return RouteUtil.Message("해당 화면의 권한이 없습니다.");
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            // 회의타입 리턴
            ViewBag.MeetTypeCd = pMeetTypeCd;

            return View();
        }
        #endregion

        #region CompanyManageMeetingDetail : 전사운영회의자료 상세
        [HttpGet]
        public ActionResult CompanyManageMeetingDetail(string pMeetTypeCd, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "전사운영회의(상세)");

            // 회의자료 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region DepartmentLeaderMeetingDetail : 부서장회의자료 상세
        [HttpGet]
        public ActionResult DepartmentLeaderMeetingDetail(string pMeetTypeCd, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "부서장회의(상세)");

            // 회의자료 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region DirectorsMeetingDetail : 임원회의자료 상세
        [HttpGet]
        public ActionResult DirectorsMeetingDetail(string pMeetTypeCd, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "임원회의(상세)");
            // 회의자료 조회
            Meeting dbMeeting = _meetingFactory.GetMeeting(pRegNo, pMeetTypeCd);

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region CompanyManageMeetingEdit : 전사운영회의 자료작성(GET)
        [HttpGet]
        public ActionResult CompanyManageMeetingEdit(string pMeetTypeCd, string pSaveMode, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "전사운영회의(작성)");

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

                if (dbMeeting.RegUserId != _UserId)
                {
                    return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Resource/CompanyManageMeetingDetail?pMeetTypeCd=" + pMeetTypeCd + "&pRegNo=" + pRegNo);
                }
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region CompanyManageMeetingEdit : 전사운영회의 자료작성(POST)
        [HttpPost]
        public ActionResult CompanyManageMeetingEdit(Meeting pMeeting, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "전사운영회의(작성)");

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
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Resource/CompanyManageMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pMeeting.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Resource/CompanyManageMeeting?pMeetTypeCd=" + pMeeting.MeetTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Resource/CompanyManageMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region DepartmentLeaderMeetingEdit : 부서장회의 자료작성(GET)
        [HttpGet]
        public ActionResult DepartmentLeaderMeetingEdit(string pMeetTypeCd, string pSaveMode, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "부서장회의(작성)");

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
                    if (dbMeeting.RegUserId != _UserId)
                    {
                        return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Resource/DepartmentLeaderMeetingDetail?pMeetTypeCd=" + pMeetTypeCd + "&pRegNo=" + pRegNo);
                    }
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region DepartmentLeaderMeetingEdit : 회의자료작성(POST)
        [HttpPost]
        public ActionResult DepartmentLeaderMeetingEdit(Meeting pMeeting, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "부서장회의(작성)");

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
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Resource/DepartmentLeaderMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pMeeting.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Resource/DepartmentLeaderMeeting?pMeetTypeCd=" + pMeeting.MeetTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Resource/DepartmentLeaderMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region DirectorsMeetingEdit : 임원회의 자료작성(GET)
        [HttpGet]
        public ActionResult DirectorsMeetingEdit(string pMeetTypeCd, string pSaveMode, string pRegNo)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "임원회의(작성)");

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

                if (dbMeeting.RegUserId != _UserId)
                {
                    return RouteUtil.MessageAndMove("작성자만 수정 가능 합니다.", "/Resource/DirectorsMeetingDetail?pMeetTypeCd=" + pMeetTypeCd + "&pRegNo=" + pRegNo);
                }
            }

            string CMMeeting = _webUtil.CheckAuthInfo("CompanyManageMeeting", _EmpNo);
            string DLMeeting = _webUtil.CheckAuthInfo("DepartmentLeaderMeeting", _EmpNo);
            string DMeeting = _webUtil.CheckAuthInfo("DirectorsMeeting", _EmpNo);

            ViewBag.CMMeeting = CMMeeting;
            ViewBag.DLMeeting = DLMeeting;
            ViewBag.DMeeting = DMeeting;

            return View(dbMeeting);
        }
        #endregion

        #region DirectorsMeetingEdit : 임원회의 자료작성(POST)
        [HttpPost]
        public ActionResult DirectorsMeetingEdit(Meeting pMeeting, IEnumerable<HttpPostedFileBase> pAttIdFiles)
        {
            _userFactory.CreateLoginLog(Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "임원회의(작성)");

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
                return RouteUtil.MessageAndMove(Util.MakeMessage(e), "/Resource/DirectorsMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=C&pRegNo=");
            }

            if (pMeeting.SaveMode == "D")
            {
                return RouteUtil.MessageAndMove("삭제 하였습니다.", "/Resource/DirectorsMeeting?pMeetTypeCd=" + pMeeting.MeetTypeCd);
            }
            else
            {
                return RouteUtil.MessageAndMove("저장 하였습니다.", "/Resource/DirectorsMeetingEdit?pMeetTypeCd=" + pMeeting.MeetTypeCd + "&pSaveMode=U&pRegNo=" + regNo);
            }
        }
        #endregion

        #region ListAttFileLayUp : 첨부파일 레이업 조회
        [HttpPost]
        public ContentResult ListAttFileLayUp(string pTitle, string pId, string pAttId, string pWidth, string pGubun)
        {
            AttFileFactory _attFileFactory = new AttFileFactory();

            // 첨부파일 목록
            List<AttFile> attFiles = new List<AttFile>();

            if (pAttId == "" || pAttId == null)
            {
                attFiles = new List<AttFile>();
            }
            else
            {
                attFiles = _attFileFactory.ListAttFile(pAttId);
            }

            StringBuilder sb = new StringBuilder("");

            if(pGubun == "PC") { 
                sb.Append(@"<div class='file_float_left style='width:" + pWidth + "'>"
                            + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                            + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                            + "<div class='box_fileLayer'>"
                                + "<div>"
                                    + "<table class='tbl_file'>"
                                        + "<caption>첨부파일</caption>"
                                            + "<colgroup>"
                                                + "<col style='width:50%'>"
                                                + "<col style='width:auto'>"
                                            + "</colgroup>"
                                            + "<tbody id='tbody_" + pId.ToLower() + "'>"
                );
                foreach (AttFile f in attFiles)
                {
                    sb.Append(@"<tr class='attfile'>"
                                + "<td class='attfile'>"
                                    + "<a class='attfile' id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                                + "<td class='attfile'>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "' class='btn_delete_file' style='background:url(/Contents/images/button/btn_origin_file.png) no-repeat 5px center;float:right;margin-right:20px;'>원본보기</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                            + "</tr>"
                        );
                }
            }
            else
            {
                sb.Append(@"<div class='file_float_left style='width:" + pWidth + "'>"
                        + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                        + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                        + "<div class='box_fileLayer'>"
                            + "<div>"
                                + "<table class='tbl_file'>"
                                    + "<caption>첨부파일</caption>"
                                        + "<colgroup>"
                                            + "<col style='width:100%'>"
                                        + "</colgroup>"
                                        + "<tbody id='tbody_" + pId.ToLower() + "'>"
                );
                foreach (AttFile f in attFiles)
                {
                    sb.Append(@"<tr class='attfile'>"
                                + "<td class='attfile'>"
                                    + "<a class='attfile' id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                            + "</tr>"
                        );
                }
            }            

            sb.Append(@"</tbody></table></div></div></div>");


            return Content(sb.ToString(), "text/html");
        }
        #endregion

        #region CoronaMap : 코로나맵
        [HttpGet]
        public ActionResult CoronaMap()
        {
            return View();
        }
        #endregion

    }
}

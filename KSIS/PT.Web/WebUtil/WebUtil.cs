using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.WebUtil
{
    public enum Position { left, right };

    /// <summary>
    /// View 관련 유틸
    /// </summary>
    public class WebUtil
    {
        UserFactory _userFactory = new UserFactory();

        #region MakeFileControl : 파일 컨트롤 생성
        /// <summary>
        /// 파일 컨트롤 생성
        /// </summary>
        public static MvcHtmlString MakeFileControl(string pTitle, string pId, string pAttId = "", bool pEditable = true, Position pPos = Position.left, string pWidth = "49%")
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

            if (pEditable == true)
            {
                //수정할 수 있는 경우
                sb.Append(@"<div class='file_float_" + pPos + "' style='width:" + pWidth + "'>"
                            + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                            + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                            + "<a class='btn_add_file' id='btn_add_" + pId.ToLower() + "'>파일추가</a>"
                            + "<div class='box_file'>"
                                + "<h1>파일명</h1>"
                                + "<div>"
                                    + "<table class='tbl_file'>"
                                        + "<caption>첨부파일</caption>"
                                            + "<colgroup>"
                                                + "<col style=''>"
                                                + "<col style='width:10%'>"
                                            + "</colgroup>"
                                            + "<tbody id='tbody_" + pId.ToLower() + "'>"
                );
                foreach (AttFile f in attFiles)
                {
                    sb.Append(@"<tr>"
                                + "<td>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                                + "<td>"
                                    + "<a class='btn_delete_file'>삭제</a>"
                                + "</td>"
                            + "</tr>"
                        );
                }

                sb.Append(@"</tbody></table></div></div></div>");

                sb.Append(@"<script type=""text/javascript"">
	                        $(""#btn_add_" + pId.ToLower() + @""").click(function () {
		                        $(""#tbody_" + pId.ToLower() + @""").append('<tr><td><input type=""file"" name=""p" + pId + @"Files"" /></td><td><a class=""btn_delete_file"">삭제</a></td></tr>')
	                        });

	                        $(document).on(""click"", ""#tbody_" + pId.ToLower() + @" .btn_delete_file"", function () {
		                        $(this).parent().parent().remove();
	                        });
                        </script>"
                );
            }
            else
            {
                //수정할 수 없는 경우
                sb.Append(@"<div class='file_float_" + pPos + "' style='width:" + pWidth + "'>"
                            + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                            + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                            + "<div class='box_file'>"
                                + "<h1>파일명</h1>"
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
                    sb.Append(@"<tr>"
                                + "<td>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                            + "</tr>"
                        );
                }

                sb.Append(@"</tbody></table></div></div></div>");
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region MakeFileControlForMeet : 회의자료 전용 파일 컨트롤 생성
        /// <summary>
        /// 파일 컨트롤 생성
        /// </summary>
        public static MvcHtmlString MakeFileControlForMeet(string pTitle, string pId, string pAttId = "", bool pEditable = true, Position pPos = Position.left, string pWidth = "49%")
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

            if (pEditable == true)
            {
                //수정할 수 있는 경우
                sb.Append(@"<div class='file_float_" + pPos + "' style='width:" + pWidth + "'>"
                            + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                            + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                            + "<a class='btn_add_file' id='btn_add_" + pId.ToLower() + "'>파일추가</a>"
                            + "<div class='box_file'>"
                                + "<h1>파일명</h1>"
                                + "<div>"
                                    + "<table class='tbl_file'>"
                                        + "<caption>첨부파일</caption>"
                                            + "<colgroup>"
                                                + "<col style=''>"
                                                + "<col style='width:10%'>"
                                            + "</colgroup>"
                                            + "<tbody id='tbody_" + pId.ToLower() + "'>"
                );
                foreach (AttFile f in attFiles)
                {
                    sb.Append(@"<tr>"
                                + "<td>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                                + "<td>"
                                    + "<a class='btn_delete_file'>삭제</a>"
                                + "</td>"
                            + "</tr>"
                        );
                }

                sb.Append(@"</tbody></table></div></div></div>");

                sb.Append(@"<script type=""text/javascript"">
	                        $(""#btn_add_" + pId.ToLower() + @""").click(function () {
		                        $(""#tbody_" + pId.ToLower() + @""").append('<tr><td><input type=""file"" name=""p" + pId + @"Files"" /></td><td><a class=""btn_delete_file"">삭제</a></td></tr>')
	                        });

	                        $(document).on(""click"", ""#tbody_" + pId.ToLower() + @" .btn_delete_file"", function () {
		                        $(this).parent().parent().remove();
	                        });
                        </script>"
                );
            }
            else
            {
                //수정할 수 없는 경우
                sb.Append(@"<div class='file_float_" + pPos + "' style='width:" + pWidth + "'>"
                            + "<input type='hidden' id='" + pId + "' name='" + pId + "' value='" + pAttId + "'>"
                            + "<h1 class='h_file_title'>" + pTitle + "</h1>"
                            + "<div class='box_file'>"
                                + "<h1>파일명</h1>"
                                + "<div>"
                                    + "<table class='tbl_file'>"
                                        + "<caption>첨부파일</caption>"
                                            + "<colgroup>"
                                                + "<col style='width:70%'>"
                                                + "<col style='width:auto'>"
                                            + "</colgroup>"
                                            + "<tbody id='tbody_" + pId.ToLower() + "'>"
                );
                foreach (AttFile f in attFiles)
                {
                    sb.Append(@"<tr>"
                                + "<td>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "'>" + f.AttFileNm + "</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                                + "<td>"
                                    + "<a id='" + f.AttId + "/" + f.Seq + "' class='btn_delete_file' style='background:url(/Contents/images/button/btn_origin_file.png) no-repeat 5px center;float:right;margin-right:20px;'>원본보기</a>"
                                    + "<input type='hidden' name='p" + pId + "SavedFileSeqs' value='" + f.Seq + "'>"
                                + "</td>"
                            + "</tr>"
                        );
                }

                sb.Append(@"</tbody></table></div></div></div>");
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region CheckAuthInfo : 메뉴별 사용자 권한 확인
        /// <summary>
        /// 메뉴별 사용자 권한 확인
        /// </summary>
        public string CheckAuthInfo(string pMenu, string pEmpNo)
        {
            string returnVal = "N";


            // 사용자 권한 확인
            User dbUser = _userFactory.GetAuthInfo(pMenu, pEmpNo);

            if (dbUser != null)
            {
                returnVal = dbUser.AuthYn;
            }

            return returnVal;
        }
        #endregion

        #region MakeSitePhotoGubun : 현장현황 사진 그리드 생성(구분별)
        /// <summary>
        /// 현장현황 사진 그리드 생성(구분별)
        /// </summary>
        public static MvcHtmlString MakeSitePhotoGubun(string pBizPartCd, string pGubun)
        {
            SiteFactory _siteFactory = new SiteFactory();

            // 첨부파일 목록
            List<Site> sitePhoto = _siteFactory.ListSitePhotoUrlGubun(Util.MakeDateTime("YearMon"), pBizPartCd, pGubun);

            StringBuilder sb = new StringBuilder("");

                foreach (Site f in sitePhoto)
                {
                    sb.Append(@"<a class='bn_box2'>"
                            + "<img class='lazy' src='/Contents/images/ex/loading.gif' data-src='"+ f.SitePhotoUrl + "' />" 
                            + "<span id='"+ f.SiteCd + "'>" + f.SiteNm + "</a>"
                        );
                }     

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region MakeSitePhotoHome : 홈화면 그리드 생성
        /// <summary>
        /// 홈화면 그리드 생성
        /// </summary>
        public static MvcHtmlString MakeSitePhotoHome(string pBizPartCd)
        {
            SiteFactory _siteFactory = new SiteFactory();

            // 첨부파일 목록
            List<Site> sitePhoto = _siteFactory.ListSitePhotoUrl(Util.MakeDateTime("YearMon"), pBizPartCd);

            StringBuilder sb = new StringBuilder("");

            foreach (Site f in sitePhoto)
            {
                sb.Append(@"<li>"
                                + "<a class='bn_box1' style='cursor: pointer'>"
                                //+ "<img style='width:220px;height:180px; border:5px solid #d8d8d8;' class='lazy' src='" + f.SitePhotoUrl + "' />"
                                + "<img style='width:220px;height:180px; border:5px solid #d8d8d8;' class='lazy' src='/Contents/images/ex/loading.gif' data-src='" + f.SitePhotoUrl + "' />"
                                + "<p style='font-weight:bold;color:#0c4da2;font-family:NanumGothic;' class='" + f.BizPartCd + "' id='" + f.SiteCd + "'>" + f.SiteNm + "</p>"
                                + "</a>"
                        + "</li>"
                    );
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region MakeCompletionSitePhoto : 준공현장 사진 그리드 생성(PC)
        /// <summary>
        /// 준공현장 사진 그리드 생성
        /// </summary>
        public static MvcHtmlString MakeCompletionSitePhoto(string pDeptCd)
        {
            SiteFactory _siteFactory = new SiteFactory();

            // 첨부파일 목록
            List<Site> sitePhoto = _siteFactory.ListSitePhotoDetailUrl(pDeptCd, "0");

            StringBuilder sb = new StringBuilder("");

            foreach (Site f in sitePhoto)
            {
                sb.Append(@"<li id='thumb_" + f.Number + "' data-thumb='" + f.SitePhotoUrl + "' data-type='text'>"
                                + "<img id='img_" + f.Number + "' style='width:650px;height:600px;' src='" + f.SitePhotoUrl + "' data-BizpartCd = '"+f.SiteBizpartCd+"' />"
                                + "</li>"
                    // + "<img style='width:220px;height:180px; border:5px solid #d8d8d8;' class='lazy' src='" + f.SitePhotoUrl + "' />"
                    );
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region MakeSlideSitePhotoAll : 현장사진 조회(전체)
        /// <summary>
        /// 현장사진 조회(전체)
        /// </summary>
        public static MvcHtmlString MakeSlideSitePhotoAll(string pSiteCd)
        {
            SiteFactory _siteFactory = new SiteFactory();

            // 첨부파일 목록
            List<Site> sitePhoto = _siteFactory.ListSitePhotoUrlAll(pSiteCd);

            StringBuilder sb = new StringBuilder("");

            sb.Append(@"<div id='slider' class='flexslider'>");
            sb.Append(@"<ul class='slides'>");

            foreach (Site f in sitePhoto)
            {
                sb.Append(@"<li>"
                                + "<img style='width:1070px;height:600px;' src='" + f.SitePhotoUrl + "'/>"
                                + "</li>"                   
                    );
            }
            sb.Append(@"</ul>");
            sb.Append(@"</div>");

            sb.Append(@"<div id='carousel' class='flexslider'>");
            sb.Append(@"<ul class='slides'>");

            foreach (Site f in sitePhoto)
            {
                sb.Append(@"<li>"
                                + "<img style='width:220px;height:180px; border:5px solid #d8d8d8;' src='" + f.SitePhotoUrl + "'/>"
                                + "<p style='font-weight:bold;color:#4373d9;font-family:NanumGothic;text-align:center;'>" + f.RegDate + "</p>"
                                + "</li>"
                    );
            }
            sb.Append(@"</ul>");
            sb.Append(@"</div>");

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region MakeSitePhotoAll : 현장사진 조회(전체)
        /// <summary>
        /// 현장사진 조회(전체)
        /// </summary>
        public static MvcHtmlString MakeSitePhotoAll(string pSiteCd)
        {
            SiteFactory _siteFactory = new SiteFactory();

            // 첨부파일 목록
            List<Site> sitePhoto = _siteFactory.ListSitePhotoUrlAll(pSiteCd);

            StringBuilder sb = new StringBuilder("");

            int i = 0;
            int j = 0;
            var src = "";
            var rowGroup = "";

            var data = sitePhoto.Select(m => new {
                RowNum = m.RowNum
            });

            
            
            foreach (Site f in sitePhoto)
            {
                if(j==0)
                {
                    sb.Append(@"<div>");
                }

                sb.Append(@" <a id = 'a_" + f.RowNum + "' class='bn_box7'>"
                                + "<img class='lazy' id='imgId_" + f.RowNum+ "' src='/Contents/images/ex/loading.gif' data-src = '" + f.SitePhotoUrl + "' data-group='" + f.RowGroup + "'/>"
                                + "<span id='span_"+f.RowNum+"' data-group='" + f.RowGroup + "'>"+ f.SiteNm + "</span>"
                                + "</a>"

                );
                
                if ((Int32.Parse(f.RowNum)+1)%7 == 0)
                {                   
                    sb.Append(@"<div class = 'bn_box6' id = 'large_"+f.RowGroup+"'>"
                                + "<div class='leftArrow'><a id='prev' class='prev'></a></div>"
                                + "<img class='lazy' id='" + f.RowGroup + "' src='/Contents/images/ex/loading.gif' data-src = '" + f.SitePhotoUrl + "'/>"
                                + "<div class='rightArrow'><a id='next' class='next'></a></div>"
                                + "</div>");
                    sb.Append(@"</div>");
                }

                if(i== 6)
                {
                    i = 0;
                    j = 0;
                }
                else
                {
                    i++;
                    j++;
                }

                src = f.SitePhotoUrl;
                rowGroup = f.RowGroup;

            }

            if(data.Count()%7 != 0)
            {
                sb.Append(@"<div class = 'bn_box6' id = 'large_"+rowGroup+"'>"
                               + "<div class='leftArrow'><a id='prev' class='prev'></a></div>"
                               + "<img class='lazy' id='" + rowGroup + "' src='/Contents/images/ex/loading.gif' data-src = '" + src + "'/>"
                               + "<div class='rightArrow'><a id='next' class='next'></a></div>"
                               + "</div>");
                sb.Append(@"</div>");
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region ListMeetingGrid : 공지사항 리스트(홈)
        /// <summary>
        /// 준공현장 사진 그리드 생성
        /// </summary>
        public static MvcHtmlString ListMeetingGrid()
        {
            MeetingFactory _meetingFactory = new MeetingFactory();

            // 첨부파일 목록
            List<Meeting> meeting = _meetingFactory.ListMeetingGrid("NoticeControl", "", "");

            StringBuilder sb = new StringBuilder("");

            foreach (Meeting f in meeting)
            {
                sb.Append(@"<li>"
                                + "<a id='" + f.RegNo + "' style='color:#ffffff;' class='Notice' >" + f.Subject + "</a>"
                                + "</li>"                  
                    );
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion
        
    }
}
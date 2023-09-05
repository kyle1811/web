using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PT.BusinessLogic.Entities
{
    public class Meeting
    {

        #region 공통 : 그리드 및 함수값 관련
        /// <summary>
        /// 체크박스
        /// </summary>
        public string ChkBox { get; set; }

        /// <summary>
        /// 반환값
        /// </summary>
        public string ReturnVal { get; set; }

        /// <summary>
        /// 순번
        /// </summary>
        public string RowNum { get; set; }

        /// <summary>
        /// 순번그룹
        /// </summary>
        public string RowGroup { get; set; }

        /// <summary>
        /// 저장방법
        /// </summary>
        public string SaveMode { get; set; }
        #endregion

        #region Meeting : 회의자료
        /// <summary>
        /// 등록번호
        /// </summary>
        public string RegNo { get; set; }

        /// <summary>
        /// 게시판타입코드
        /// </summary>
        public string MeetTypeCd { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        [AllowHtml]
        public string Content { get; set; }

        /// <summary>
        /// 내용(Html)
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// 첨부ID
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 작성자ID
        /// </summary>
        public string RegUserId { get; set; }

        /// <summary>
        /// 작성자명
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 작성일시
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 전사운영회의권한
        /// </summary>
        public string CMMeeting { get; set; }

        /// <summary>
        /// 부서장회의권한
        /// </summary>
        public string DLMeeting { get; set; }

        /// <summary>
        /// 임원회의권한
        /// </summary>
        public string DMeeting { get; set; }

        /// <summary>
        /// 회의자료권한
        /// </summary>
        public string AuthType { get; set; }

        /// <summary>
        ///  첨부파일 유무
        /// </summary>
        public string AttFileYN { get; set; }
        #endregion
    }
}

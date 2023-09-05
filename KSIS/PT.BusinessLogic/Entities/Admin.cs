using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Admin
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
        /// 휴대폰 번호
        /// </summary>
        public string UserMobileNo { get; set; }

        /// <summary>
        /// 순번그룹
        /// </summary>
        public string RowGroup { get; set; }
        #endregion

        #region AuthControl : 화면 권한 관련

        #region AuthControl : 트리뷰
            /// <summary>
            /// 메뉴 CD
            /// </summary>
            public string DeptCd { get; set; }

            /// <summary>
            /// 메뉴명
            /// </summary>
            public string DeptNm { get; set; }

            /// <summary>
            /// 부모CD
            /// </summary>
            public string ParentCd { get; set; }

            /// <summary>
            /// 레벨
            /// </summary>
            public string DeptLevel { get; set; }

            /// <summary>
            /// 자식노드여부
            /// </summary>
            public string ChildYn { get; set; }

            /// <summary>
            /// 자식노드여부
            /// </summary>
            public string OpenYn { get; set; }

            /// <summary>
            /// 자식노드여부
            /// </summary>
            public string DeptSort { get; set; }
        #endregion

        #region AuthControl : 테이블
        /// <summary>
        /// 메뉴 CD
        /// </summary>
        public string Menu { get; set; }

        /// <summary>
        /// 메뉴명
        /// </summary>
        public string MenuNm { get; set; }

        /// <summary>
        /// 권한자 코드
        /// </summary>
        public string AuthCd { get; set; }

        /// <summary>
        /// 권한자
        /// </summary>
        public string AuthNm { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 직급CD
        /// </summary>
        public string DutyCd { get; set; }

        /// <summary>
        /// 직급
        /// </summary>
        public string DutyNm { get; set; }


        #endregion

        #endregion
    }
}

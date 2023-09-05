using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Dept
    {
        #region 공통 : 그리드 및 함수값 관련
        /// <summary>
        /// 반환값
        /// </summary>
        public string ReturnVal { get; set; }
        #endregion

        #region Dept : 부서정보
        /// <summary>
        /// 부서코드
        /// </summary>
        public string DeptCd { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string DeptNm { get; set; }

        /// <summary>
        /// 상위부서코드
        /// </summary>
        public string ParentCd { get; set; }

        /// <summary>
        /// 레벨
        /// </summary>
        public string DeptLevel { get; set; }

        /// <summary>
        /// 순번
        /// </summary>
        public string DeptSort { get; set; }

        /// <summary>
        /// 자식부서여부
        /// </summary>
        public string ChildYn { get; set; }

        /// <summary>
        /// 오픈여부
        /// </summary>
        public string OpenYn { get; set; }
        #endregion
    }
}

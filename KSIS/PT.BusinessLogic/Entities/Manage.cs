using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Manage
    {
        /// <summary>
        /// 부서코드
        /// </summary>
        public string DeptCd { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string DeptNm { get; set; }
        /// <summary>
        /// 현장코드
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
        /// 하위부서YN
        /// </summary>
        public string ChildYn { get; set; }

        /// <summary>
        /// 오픈 YN
        /// </summary>
        public string OpenYn { get; set; }

    }
}

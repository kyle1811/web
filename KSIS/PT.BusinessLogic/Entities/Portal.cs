using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Portal
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

        #region 수주실적 : 수주 계획 대 실적 조회(홈화면)
        /// <summary>
        /// 연간계획
        /// </summary>
        public string YearPlan { get; set; }

        /// <summary>
        /// 누계계획
        /// </summary>
        public string AccPlan { get; set; }

        /// <summary>
        /// 2누계실적
        /// </summary>
        public string AccRslt { get; set; }

        /// <summary>
        /// 연간 달성율
        /// </summary>
        public string YearRate { get; set; }

        #endregion
    }
}

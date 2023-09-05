using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class DiligenceFactory
    {
        #region listDiligenceCalendar : 월별연차현황 달력 조회
        /// <summary>
        /// 월별연차현황 달력 조회
        /// </summary>
        public List<Diligence> listDiligenceCalendar(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Diligence> returnData = DBAccess.ListProcData<Diligence>("PPT_Diligence_listDiligenceCalendar", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region listDiligenceState : 월별연차현황 조회
        /// <summary>
        /// 월별연차현황 조회
        /// </summary>
        public List<Diligence> listDiligenceState(string pYearMon, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<Diligence> returnData = DBAccess.ListProcData<Diligence>("PPT_Diligence_listDiligenceState", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion
    }
}

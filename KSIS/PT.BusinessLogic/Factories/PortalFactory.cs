using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class PortalFactory
    {
        #region GetSujuPlanResultHome : 수주계획 대 실적조회(홈화면)
        /// <summary>
        /// 수주계획 대 실적조회
        /// </summary>
        public Portal GetSujuPlanResultHome(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YEARMON", pYearMon)
            };

            List<Portal> returnData = DBAccess.ListProcData<Portal>("PPT_Manage_listSujuPlanResultHome", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Portal>();
        }
        #endregion

        #region ListUserGrid : 사용자목록 그리드 조회
        /// <summary>
        /// 사용자목록 그리드 조회
        /// </summary>
        public List<PopUp> ListUserGrid(string pSearchWord)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SearchWord", pSearchWord)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_User_listUserGridHome", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion
    }
}

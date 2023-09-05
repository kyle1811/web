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
    public class CommunityFactory
    {
        #region ListMagazinePhotoUrl :  사진 목록 조회
        /// <summary>
        /// 현장상세 사진 목록 조회
        /// </summary>
        public List<Community> ListMagazinePhotoUrl(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYear)
            };

            List<Community> returnData = DBAccess.ListProcData<Community>("PPT_Information_listMagazine", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListChangeMagazine : 사보 목록 조회
        /// <summary>
        /// 현장사진 목록 조회
        /// </summary>
        public List<Community> ListChangeMagazine(string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pGubun)
            };

            List<Community> returnData = DBAccess.ListProcData<Community>("PPT_Information_listMagazine", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

    }
}

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
    public class DeptFactory
    {
        #region ListDeptTreeView : 부서목록 트리뷰 조회
        /// <summary>
        /// 부서목록 트리뷰 조회
        /// </summary>
        public List<Dept> ListDeptTreeView(string pDeptCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd)
            };

            List<Dept> returnData = DBAccess.ListProcData<Dept>("PPT_Dept_listDeptTreeView", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion
    }
}

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
    public class AdminFactory
    {
        #region ListAuthControlTreeView : 화면권한 트리 뷰 조회
        /// <summary>
        /// 부서목록 트리뷰 조회
        /// </summary>
        public List<Admin> ListAuthControlTreeView(string pMenuCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@MenuCd", pMenuCd)
            };

            List<Admin> returnData = DBAccess.ListProcData<Admin>("PPT_Admin_listAuthControlTreeView", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListAuthStatus : 현장사진 목록 전체조회
        /// <summary>
        /// 현장사진 목록 전체조회
        /// </summary>
        public List<Admin> ListAuthStatus(string pMenuCd, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                 new SqlParameter("@Gubun", pGubun)
                ,new SqlParameter("@MenuCd", pMenuCd)               
            };

            List<Admin> returnData = DBAccess.ListProcData<Admin>("PPT_Admin_listAuthStatus", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region CreateLoginLog : 로그인정보 저장
        /// <summary>
        /// 로그인정보 저장
        /// </summary>
        public void CreateLoginLogPlus(string pUserId, string pIP, string pPage, string pBrowser)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@IP", pIP),
                new SqlParameter("@Page", pPage),
                new SqlParameter("@Browser", pBrowser)
            };

            DBAccess.ExcProc("PPT_User_createLoginLog", sqlParams, ConnStrNm.Main);
        }
        #endregion

        #region GetUser : 사용자정보 조회
        /// <summary>
        /// 사용자정보 조회
        /// </summary>
        public UsedSale GetUser(string pUserId, string pSeq)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@Seq", pSeq)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_User_getUser", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<UsedSale>();
        }
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Transactions;
using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class PopUpFactory
    {
        #region ListSearchVendor : 업체 조회 그리드
        /// <summary>
        /// 업체 조회 그리드
        /// </summary>
        public List<PopUp> ListSearchVendor(string pVendornm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Vendornm", pVendornm)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Cooperator_listVendor", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetCooperatorDetail : 협력업체 상세조회
        /// <summary>
        /// 협력업체 상세조회
        /// </summary>
        public PopUp GetCooperatorDetail(string pVendorCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@VendorCd", pVendorCd)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Cooperator_listVendorDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<PopUp>();
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

        #region SaveMagazine : 사보저장
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string SaveMagazine(Community pCommunity, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@SaveMode", pCommunity.SaveMode),
                new SqlParameter("@RegNo", pCommunity.RegNo),
                new SqlParameter("@AttId", pCommunity.AttId),
                new SqlParameter("@UserId", pUserId)
            };

            DBAccess.ExcProc("PPT_Information_saveMagazine", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region SaveSitePhoto : 사진저장
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string SaveSitePhoto(PopUp pPopUp, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@SaveMode", pPopUp.SaveMode),
                new SqlParameter("@SiteCd",pPopUp.SiteCd),
                new SqlParameter("@RegNo", pPopUp.RegNo),
                new SqlParameter("@AttId", pPopUp.AttId),
                new SqlParameter("@UserId", pUserId)
            };

            DBAccess.ExcProc("PPT_Site_saveSiteStatePhoto", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region ListVendorClassification : 공종/자재 조회
        /// <summary>
        /// 공종/자재 조회
        /// </summary>
        public List<PopUp> ListVendorClassification(string pVendorCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@VendorCd", pVendorCd)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Cooperator_listVendorClassification", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region listUsagePopUp : 동적 테이블 헤더 조회
        /// <summary>
        /// KSIS 집계 테이블
        /// </summary>
        public List<PopUp> ListUsagePopUp(string pScreenCd, string pGUBUN, string pDutyCd, string pSelect, string pTerm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ScreenCd", pScreenCd),
                new SqlParameter("@GUBUN", pGUBUN),
                new SqlParameter("@DutyCd", pDutyCd),
                new SqlParameter("@Select", pSelect),
                new SqlParameter("@Term", pTerm)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Control_listUsagePopUpForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListCondtionalSearch : 권한등록 페이지 조회
        /// <summary>
        /// 권한등록 페이지 조회
        /// </summary>
        public List<PopUp> ListCondtionalSearch(string pSearchTypeCd, string pSearchWord)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Gubun", pSearchTypeCd),
                new SqlParameter("@SearchTXT", pSearchWord)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Admin_listCondtionalSearch", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region InsertCondtional : 권한등록
        /// <summary>
        /// 권한등록
        /// </summary>
        public string InsertCondtional(string pMenuCd, string pAuthUserId, string pRegUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@MenuCd", pMenuCd),
                new SqlParameter("@AuthUserId",pAuthUserId),
                new SqlParameter("@RegUserId", pRegUserId)
            };

            DBAccess.ExcProc("PPT_Admin_insertCondtional", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region ListMeetingExecutives : 임원 리스트 조회
        /// <summary>
        ///  임원 리스트 조회
        /// </summary>
        public List<PopUp> ListMeetingExecutives(string pMeetTypeCd, string pRegNo, string pSelectGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
               new SqlParameter("@MeetTypeCd", pMeetTypeCd),
               new SqlParameter("@RegNo", pRegNo),
               new SqlParameter("@SelectGubun", pSelectGubun)
            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Meeting_listExecutives", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region 임원공개대상 저장
        /// <summary>
        /// 임원공개대상 저장
        /// </summary>
        public void AuthRegistSuju(List<PopUp> pSujuExecutive, string pMeetTypeCd, string pRegNo, string pRegUserId, string pGubun)
        {
            

            using (TransactionScope scope = new TransactionScope())
            {                
                // 선택내용 저장
                foreach (PopUp s in pSujuExecutive)
                {

                    SqlParameter[]  sqlParams = new SqlParameter[] {
                        new SqlParameter("@AuthUserId", s.UserId),
                        new SqlParameter("@MeetTypeCd", pMeetTypeCd),
                        new SqlParameter("@RegNo", pRegNo),
                        new SqlParameter("@RegUserId", pRegUserId),
                        new SqlParameter("@Gubun", pGubun)
                    };

                    DBAccess.ExcProc("PPT_Admin_insertAuthRegistSuju", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }

        }
        #endregion

        #region ListAuthMeetingExecutives : 자료공개대상 임원 리스트 조회
        /// <summary>
        ///자료공개대상 임원 리스트 조회
        /// </summary>
        public List<PopUp> ListAuthMeetingExecutives(string pMeetTypeCd, string pRegNo, string pSelectGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
               new SqlParameter("@MeetTypeCd", pMeetTypeCd),
               new SqlParameter("@RegNo", pRegNo),
               new SqlParameter("@SelectGubun", pSelectGubun)

            };

            List<PopUp> returnData = DBAccess.ListProcData<PopUp>("PPT_Meeting_listExecutives", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

    }
}

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
    public class UserFactory
    {
        #region CheckUserInfo : 로그인 사용자정보 확인
        /// <summary>
        /// 로그인 사용자정보 확인
        /// </summary>
        public User CheckUserInfo(User pUser)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUser.UserId),
                new SqlParameter("@Pwd", pUser.Pwd)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_checkUserInfo", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region GetLoginUser : 로그인 사용자정보 조회
        /// <summary>
        /// 사용자정보 조회
        /// </summary>
        public User GetLoginUser(string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_getLoginUser", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region GetUser : 사용자정보 조회
        /// <summary>
        /// 사용자정보 조회
        /// </summary>
        public User GetUser(string pUserId, string pSeq)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@Seq", pSeq)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_getUser", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region CreateLoginLog : 로그인정보 저장
        /// <summary>
        /// 로그인정보 저장
        /// </summary>
        public void CreateLoginLog(string pUserId, string pIP, string pPage)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@IP", pIP),
                new SqlParameter("@Page", pPage),
                new SqlParameter("@Browser", "")
            };

            DBAccess.ExcProc("PPT_User_createLoginLog", sqlParams, ConnStrNm.Main);
        }
        #endregion

        #region CreateLoginLog : 로그인정보 저장
        /// <summary>
        /// 로그인정보 저장
        /// </summary>
        public void CreateLoginLogPlus(string pUserId, string pIP, string pPage, string pBrowser, string pDevice)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@IP", pIP),
                new SqlParameter("@Page", pPage),
                new SqlParameter("@Browser", pBrowser),
                new SqlParameter("@Device", pDevice)
            };

            DBAccess.ExcProc("PPT_User_createLoginLog", sqlParams, ConnStrNm.Main);
        }
        #endregion

        #region CreateAndroidLoginLog : Voice_K 로그인정보 저장
        /// <summary>
        /// 로그인정보 저장
        /// </summary>
        public void CreateAndroidLoginLog(string pUserId, string pIP, string pPage)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@IP", pIP),
                new SqlParameter("@Page", pPage)
            };

            DBAccess.ExcProc("PCO_Mobile_createLoginLog", sqlParams, ConnStrNm.Main);
        }
        #endregion

        #region CreateMailReceiveLog : 메일 열람정보 저장
        /// <summary>
        /// 메일 열람정보 저장
        /// </summary>
        public void CreateMailReceiveLog(string pUserId, string pIP, string pType)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserId", pUserId),
                new SqlParameter("@IP", pIP),
                new SqlParameter("@Type", pType)
            };

            DBAccess.ExcProc("PCO_Mail_createMailLog", sqlParams, ConnStrNm.Main);
        }
        #endregion

        #region ListUserGrid : 사용자목록 그리드 조회
        /// <summary>
        /// 사용자목록 그리드 조회
        /// </summary>
        public List<User> ListUserGrid(string pTreeYn, string pDeptCd, string pSearchTypeCd, string pSearchWord)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@TreeYn", pTreeYn),
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@SearchTypeCd", pSearchTypeCd),
                new SqlParameter("@SearchWord", pSearchWord)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_listUserGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMonthPersonalVacationCnt : 연차사용현황 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListMonthPersonalVacationCnt(string pYearMon, string pEmpNo, decimal pFlag)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@FiscalYear", pYearMon),
                new SqlParameter("@EmpNo", pEmpNo),
                new SqlParameter("@Flag", pFlag)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthPersonalVacationCnt", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMonthDeptVacationCnt : 부서원 연차사용현황 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListMonthDeptVacationCnt(string pYearMon, string pDeptCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@FiscalYear", pYearMon),
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthDeptVacationCnt", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMonthDeptVacationPercent : 부서 연차사용율 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListMonthDeptVacationPercent(string pYearMon, string pDeptCd, string pEmpNo, string pBizpartCd, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@FiscalYear", pYearMon),
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo),
                new SqlParameter("@BizpartCd", pBizpartCd),
                new SqlParameter("@Gubun", pGubun)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthDeptVacationPercent", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion




        #region ListPLAnalysis : 손익현황 테스트
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListPLAnalysis(string pDeptCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_PLAnalysis_list", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion



        #region ListPersonGetOffWorkTime : 개인 평균 퇴근시간 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListPersonGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthUserAvrGetOffWorkTime", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListDeptGetOffWorkTime : 부서 평균 퇴근시간 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListDeptGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthDeptAvrGetOffWorkTime", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListDeptPersonGetOffWorkTime : 부서 평균 퇴근시간 조회
        /// <summary>
        /// 연차사용현황 조회
        /// </summary>
        public List<User> ListDeptPersonGetOffWorkTime(string pDeptCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_Person_listMonthDeptUserAvrGetOffWorkTime", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetAuthInfo : 사용자 권한 확인
        /// <summary>
        /// 사용자 권한 확인
        /// </summary>
        public User GetAuthInfo(string pMenu, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Menu", pMenu),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_getAuthInfo", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region ListGroupCompany : 그룹사 사우정보 조회 그리드
        /// <summary>
        /// 업체 조회 그리드
        /// </summary>
        public List<User> ListGroupCompany(string pUsernm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Usernm", pUsernm)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_GroupCompany_listUser", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region KCCIMGTEST : KCC 이미지 조회 테스트
        /// <summary>
        /// KCC 이미지 조회 테스트
        /// </summary>
        public User KCCIMGTEST(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_KCCUser_ImgTest", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region UserImgSrc : 사우 이미지 소스 지정
        /// <summary>
        /// KCC 이미지 조회 테스트
        /// </summary>
        public User UserImgSrc(string pSEQ, string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SEQ", pSEQ)
               ,new SqlParameter("@UserId", pUserId)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_ImgSrc", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<User>();
        }
        #endregion

        #region GetUserInfo : 이름으로 사용자정보 조회(안드로이드용)
        /// <summary>
        ///  이름으로 사용자정보 조회
        /// </summary>
        public List<User> GetUserInfo(string pUserNm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserNm", pUserNm)
            };

            List<User> returnData = DBAccess.ListProcData<User>("PPT_User_getUserInfo", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


    }
}

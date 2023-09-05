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
    public class MeetingFactory
    {
        #region ListMeetingGrid : 회의자료 목록 그리드 조회
        /// <summary>
        /// 회의자료 목록 그리드 조회
        /// </summary>
        public List<Meeting> ListMeetingGrid(string pMeetTypeCd, string pSubject, string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@MeetTypeCd", pMeetTypeCd),
                new SqlParameter("@Subject", pSubject),
                new SqlParameter("@UserId", pUserId),
            };

            List<Meeting> returnData = DBAccess.ListProcData<Meeting>("PPT_Meeting_listMeetingGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetMeeting : 회의자료 조회
        /// <summary>
        /// 회의자료 조회
        /// </summary>
        public Meeting GetMeeting(string pRegNo, string pMeetTypeCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@RegNo", pRegNo),
                new SqlParameter("@MeetTypeCd", pMeetTypeCd)
            };

            List<Meeting> returnData = DBAccess.ListProcData<Meeting>("PPT_Meeting_getMeeting", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Meeting>();
        }
        #endregion

        #region SaveMeeting : 회의자료 저장
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string SaveMeeting(Meeting pMeeting, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@SaveMode", pMeeting.SaveMode),
                new SqlParameter("@RegNo", pMeeting.RegNo),
                new SqlParameter("@MeetTypeCd", pMeeting.MeetTypeCd),
                new SqlParameter("@Subject", pMeeting.Subject),
                new SqlParameter("@Content", pMeeting.Content),
                new SqlParameter("@AttId", pMeeting.AttId),
                new SqlParameter("@UserId", pUserId),
            };

            DBAccess.ExcProc("PPT_Meeting_saveMeeting", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region ListChkAuth : 회의자료 권한 조회
        /// <summary>
        /// 회의자료 권한 조회
        /// </summary>
        public List<Meeting> ListChkAuth(string pMeetTypeCd, string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Menu", pMeetTypeCd),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<Meeting> returnData = DBAccess.ListProcData<Meeting>("PPT_User_getAuthInfo", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion
    }
}

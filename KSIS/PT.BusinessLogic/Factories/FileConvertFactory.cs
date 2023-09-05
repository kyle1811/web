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
    public class FileConvertFactory
    {
        #region GetFileConvertInfo : 파일변환 변환번호 및 경로 조회
        /// <summary>
        /// 파일변환 변환번호 및 경로 조회
        /// </summary>
        public FileConvert GetFileConvertInfo(string pAttId, string pSeq, string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AttId", pAttId),
                new SqlParameter("@Seq", pSeq),
                new SqlParameter("@UserId", pUserId)
            };

            List<FileConvert> returnData = DBAccess.ListProcData<FileConvert>("PPT_FileConvert_getFileConvertInfo", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<FileConvert>();
        }
        #endregion
    }
}

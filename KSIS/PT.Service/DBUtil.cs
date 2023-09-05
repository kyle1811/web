using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PT.Service
{
    /// <summary>
    /// DB관련 유틸
    /// </summary>
    public class DBUtil
    {
        #region ProcessNull : 매개변수값이 null인 경우 DBNull.Value 또는 문자열("")로 처리한다.
        /// <summary>
        /// 매개변수값이 null인 경우 DBNull.Value 또는 문자열("")로 처리한다.
        /// </summary>
        /// <param name="pSqlParams"></param>
        public static void ProcessNull(SqlParameter[] pSqlParams)
        {
            foreach (SqlParameter sp in pSqlParams)
            {

                // 매개변수의 값이 null인 경우
                if (sp.Value == null)
                {

                    // IsNullable이 true이면 DBNull.Value을 넣어 null 처리
                    if (sp.IsNullable == true)
                    {
                        sp.Value = DBNull.Value;
                    }
                    else
                    {
                        // IsNullable이 false이면 문자열("")로 처리
                        sp.Value = "";
                    }
                }
            }
        }
        #endregion

        #region MakeSqlParameter : SqlParameter 객체를 생성한다.
        /// <summary>
        /// IsNullable이 true인 SqlParameter 객체를 생성하기 위한 메서드
        /// </summary>
        /// <param name="pName">매개변수명 ex) @name </param>
        /// <param name="pValue">매개변수값</param>
        /// <param name="pIsNullable">매개변수값 null허용여부</param>
        /// <returns>SqlParameter 객체</returns>
        public static SqlParameter MakeSqlParameter(string pName, string pValue, bool pIsNullable)
        {
            SqlParameter sqlParameter = new SqlParameter(pName, pValue);
            sqlParameter.IsNullable = pIsNullable;

            return sqlParameter;
        }

        /// <summary>
        /// ParameterDirection이 Output인 SqlParameter 객체를 생성하기 위한 메서드
        /// </summary>
        /// <param name="pName">매개변수명 ex) @name </param>
        /// <param name="pValue">매개변수값</param>
        /// <param name="pParameterDirection">ParameterDirection</param>
        /// <param name="pSize">매개변수의 크기</param>
        /// <returns>SqlParameter 객체</returns>
        public static SqlParameter MakeSqlParameter(string pName, string pValue, ParameterDirection pParameterDirection, int pSize = 50)
        {

            SqlParameter sqlParameter = new SqlParameter(pName, SqlDbType.NVarChar, pSize);
            sqlParameter.Direction = pParameterDirection;
            sqlParameter.Value = pValue;

            return sqlParameter;
        }
        #endregion
    } 
}

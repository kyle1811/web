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
    /// DB 연결정보 이름 
    /// </summary>
    //  하자관리 추가 2018.11.26 송규현 추가 (As_
    public enum ConnStrNm { Main, As }; 

    /// <summary>
    /// 프로시저명과 파라미터를 받아 실행하고 결과를 리턴하는 클래스
    /// </summary>
    public class DBAccess
    {
        #region ListProcData<T> : 프로시저를 실행하고 데이터를 받아온다.
        /// <summary>
        /// 프로시저를 실행하고 데이터를 받아온다.
        /// </summary>
        /// <typeparam name="T">리턴받을 데이터 타입</typeparam>
        /// <param name="pSPNm">프로시저명</param>
        /// <param name="pSqlParams">프로시저 매개변수배열</param>
        /// <param name="pConnStrNm">연결할 DB이름</param>
        /// <returns>조회결과 List</returns>
        public static List<T> ListProcData<T>(string pSPNm, SqlParameter[] pSqlParams, ConnStrNm pConnStrNm = ConnStrNm.Main) where T : new()
        {
            // 매개변수의 값이 null인 경우 ""으로 변환
            DBUtil.ProcessNull(pSqlParams);

            // 반환할 데이터 선언
            List<T> returnData = new List<T>();

            // pConnStrNm에 따라 SqlConnection 생성
            // using을 사용(using 블럭을 닫으면 SqlConnection도 자동으로 닫힘)
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[pConnStrNm.ToString()].ConnectionString))
            {

                // SqlCommand에 연결, 프로시저명, 매개변수 설정
                SqlCommand cmd = new SqlCommand(pSPNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pSqlParams);

                // SqlDataReader 받음
                // SqlDataReader를 닫으면 SqlConnection도 닫히도록 설정 CommandBehavior.CloseConnection
                conn.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // 조회된 데이터의 컬럼을 조사해서 cols에 담는다.
                List<string> cols = new List<string>();

                for (int i = 0; i < sqlDataReader.VisibleFieldCount; i++)
                {
                    cols.Add(sqlDataReader.GetName(i));
                }

                // 반환할 타입의 PropertyInfo 배열을 구한다.
                System.Reflection.PropertyInfo[] pi = typeof(T).GetProperties();

                // 데이터를 읽어 returnData에 저장한다.
                while (sqlDataReader.Read())
                {

                    // 반환할 데이터 타입 선언
                    T t = new T();

                    // 프로시저에서 조회된 컬럼만(cols) 반환할 타입의 프로퍼티에 저장한다.
                    foreach (var p in pi.Where(p => cols.Contains(p.Name)))
                    {
                        if (p.Name == "userimgpic")
                        {
                            p.SetValue(t, (byte[])sqlDataReader[p.Name], null);
                        }
                        else
                        {
                            p.SetValue(t, sqlDataReader[p.Name].ToString(), null);
                        }
                    }

                    // 반환할 데이터에 추가한다.
                    returnData.Add(t);
                }
            }

            return returnData;
        }
        #endregion

        #region ExcProc : 프로시저를 실행한다.(반환값 없음)
        /// <summary>
        /// 프로시저를 실행한다.(반환값 없음)
        /// </summary>
        /// <param name="pSPNm">프로시저명</param>
        /// <param name="pSqlParams">프로시저 매개변수</param>
        /// <param name="pConnStrNm">DB 연결정보 이름(Default는 Main)</param>
        public static void ExcProc(string pSPNm, SqlParameter[] pSqlParams, ConnStrNm pConnStrNm = ConnStrNm.Main)
        {
            // 매개변수의 값이 null인 경우 ""으로 변환
            DBUtil.ProcessNull(pSqlParams);

            // pConnStrNm에 따라 SqlConnection 생성
            // using을 사용(using 블럭을 닫으면 SqlConnection도 자동으로 닫힘)
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[pConnStrNm.ToString()].ConnectionString))
            {

                // SqlCommand에 연결, 프로시저명, 매개변수 설정
                SqlCommand cmd = new SqlCommand(pSPNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pSqlParams);

                // 프로시저 실행
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        //  하자관리 추가 2018.11.26 송규현 추가 (As)

        #region ListProcDataAs<T> : 프로시저를 실행하고 데이터를 받아온다.
        /// <summary>
        /// 프로시저를 실행하고 데이터를 받아온다.
        /// </summary>
        /// <typeparam name="T">리턴받을 데이터 타입</typeparam>
        /// <param name="pSPNm">프로시저명</param>
        /// <param name="pSqlParams">프로시저 매개변수배열</param>
        /// <param name="pConnStrNm">연결할 DB이름</param>
        /// <returns>조회결과 List</returns>
        public static List<T> ListProcDataAs<T>(string pSPNm, SqlParameter[] pSqlParams, ConnStrNm pConnStrNm = ConnStrNm.As) where T : new()
        {
            // 매개변수의 값이 null인 경우 ""으로 변환
            DBUtil.ProcessNull(pSqlParams);

            // 반환할 데이터 선언
            List<T> returnData = new List<T>();

            // pConnStrNm에 따라 SqlConnection 생성
            // using을 사용(using 블럭을 닫으면 SqlConnection도 자동으로 닫힘)
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[pConnStrNm.ToString()].ConnectionString))
            {

                // SqlCommand에 연결, 프로시저명, 매개변수 설정
                SqlCommand cmd = new SqlCommand(pSPNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pSqlParams);

                // SqlDataReader 받음
                // SqlDataReader를 닫으면 SqlConnection도 닫히도록 설정 CommandBehavior.CloseConnection
                conn.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // 조회된 데이터의 컬럼을 조사해서 cols에 담는다.
                List<string> cols = new List<string>();

                for (int i = 0; i < sqlDataReader.VisibleFieldCount; i++)
                {
                    cols.Add(sqlDataReader.GetName(i));
                }

                // 반환할 타입의 PropertyInfo 배열을 구한다.
                System.Reflection.PropertyInfo[] pi = typeof(T).GetProperties();

                // 데이터를 읽어 returnData에 저장한다.
                while (sqlDataReader.Read())
                {

                    // 반환할 데이터 타입 선언
                    T t = new T();

                    // 프로시저에서 조회된 컬럼만(cols) 반환할 타입의 프로퍼티에 저장한다.
                    foreach (var p in pi.Where(p => cols.Contains(p.Name)))
                    {
                        if (p.Name == "userimgpic")
                        {
                            p.SetValue(t, (byte[])sqlDataReader[p.Name], null);
                        }
                        else
                        {
                            p.SetValue(t, sqlDataReader[p.Name].ToString(), null);
                        }
                    }

                    // 반환할 데이터에 추가한다.
                    returnData.Add(t);
                }
            }

            return returnData;
        }
        #endregion

        #region ExcProcAs : 프로시저를 실행한다.(반환값 없음)
        /// <summary>
        /// 프로시저를 실행한다.(반환값 없음)
        /// </summary>
        /// <param name="pSPNm">프로시저명</param>
        /// <param name="pSqlParams">프로시저 매개변수</param>
        /// <param name="pConnStrNm">DB 연결정보 이름(Default는 Main)</param>
        public static void ExcProcAs(string pSPNm, SqlParameter[] pSqlParams, ConnStrNm pConnStrNm = ConnStrNm.As)
        {
            // 매개변수의 값이 null인 경우 ""으로 변환
            DBUtil.ProcessNull(pSqlParams);

            // pConnStrNm에 따라 SqlConnection 생성
            // using을 사용(using 블럭을 닫으면 SqlConnection도 자동으로 닫힘)
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[pConnStrNm.ToString()].ConnectionString))
            {

                // SqlCommand에 연결, 프로시저명, 매개변수 설정
                SqlCommand cmd = new SqlCommand(pSPNm, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pSqlParams);

                // 프로시저 실행
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}

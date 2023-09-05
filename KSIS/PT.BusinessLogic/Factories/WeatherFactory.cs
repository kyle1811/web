using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Transactions;

using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class WeatherFactory
    {
        #region 날씨정보 생성(현재)
        /// <summary>
        /// 날씨정보 생성(현재)
        /// </summary>
        public void CreateWeatherNow(List<Weather> pWeather, string pDate, string pTime)
        {
            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@WeatherDate", pDate),
                        new SqlParameter("@WeatherTime", pTime)
                };

                DBAccess.ExcProc("PPT_Weather_deleteWeatherNow", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (Weather w in pWeather)
                {
                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@SiteCd", w.SiteCd),
                        new SqlParameter("@WeatherDate", w.WeatherDate),
                        new SqlParameter("@WeatherTime", w.WeatherTime),
                        new SqlParameter("@WeatherCode", w.WeatherCode),
                        new SqlParameter("@Temp", w.Temp),
                        new SqlParameter("@WindDirection", w.WindDirection),
                        new SqlParameter("@WindSpeed", w.WindSpeed),
                        new SqlParameter("@TimeRainfall", w.TimeRainfall),
                        new SqlParameter("@DayRainfallAcct", w.DayRainfallAcct),
                        new SqlParameter("@Humidity", w.Humidity),
                        new SqlParameter("@Discomfort", w.Discomfort),
                        new SqlParameter("@PM100Value", w.PM100Value),
                        new SqlParameter("@PM100State", w.PM100State),
                        DBUtil.MakeSqlParameter("@PM25Value", w.PM25Value == "-" ? null : w.PM25Value, true),
                        DBUtil.MakeSqlParameter("@PM25State", w.PM25State == "-" ? null : w.PM25State, true),
                        new SqlParameter("@Snow", w.Snow),
                        new SqlParameter("@SnowAcct", w.SnowAcct)
                    };

                    DBAccess.ExcProc("PPT_Weather_createWeatherNow", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region 날씨정보 생성(3일 3시간 예보)
        /// <summary>
        /// 날씨정보 생성(3일 3시간 예보)
        /// </summary>
        public void CreateWeatherThree(List<Weather> pWeather, string pDate)
        {
            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@WeatherDate", pDate)
                };

                DBAccess.ExcProc("PPT_Weather_deleteWeatherThree", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (Weather w in pWeather)
                {
                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@SiteCd", w.SiteCd),
                        new SqlParameter("@WeatherDate", w.WeatherDate),
                        new SqlParameter("@WeatherTime", w.WeatherTime),
                        new SqlParameter("@WeatherCode", w.WeatherCode),
                        new SqlParameter("@Temp", w.Temp),
                        new SqlParameter("@Rainfall", w.Rainfall),
                        new SqlParameter("@MinTemp", w.MinTemp),
                        new SqlParameter("@MaxTemp", w.MaxTemp),
                        new SqlParameter("@RainfallPro", w.RainfallPro),
                        new SqlParameter("@PM100Value", w.PM100Value),
                        new SqlParameter("@PM100State", w.PM100State),
                        new SqlParameter("@Snow", w.Snow)
                    };

                    DBAccess.ExcProc("PPT_Weather_createWeatherThree", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region 날씨정보 생성(일주일 예보)
        /// <summary>
        /// 날씨정보 생성(일주일 예보)
        /// </summary>
        public void CreateWeatherWeek(List<Weather> pWeather, string pDate)
        {
            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@WeatherDate", pDate)
                };

                DBAccess.ExcProc("PPT_Weather_deleteWeatherWeek", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (Weather w in pWeather)
                {
                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@SiteCd", w.SiteCd),
                        new SqlParameter("@WeatherDate", w.WeatherDate),
                        new SqlParameter("@WeatherCode", w.WeatherCode),
                        new SqlParameter("@MinTemp", w.MinTemp),
                        new SqlParameter("@MaxTemp", w.MaxTemp),
                        new SqlParameter("@RainfallPro", w.RainfallPro),
                        new SqlParameter("@PM100Value", w.PM100Value),
                        new SqlParameter("@PM100State", w.PM100State)
                    };

                    DBAccess.ExcProc("PPT_Weather_createWeatherWeek", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region GetSiteWeatherInfoDetail : 현장 날씨 상세정보 조회
        /// <summary>
        /// 현장 날씨 상세정보 조회
        /// </summary>
        public Weather GetSiteWeatherInfoDetail(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd)
            };

            List<Weather> returnData = DBAccess.ListProcData<Weather>("PPT_GetSite_WeatherInfoDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Weather>();
        }
        #endregion

        #region ListSiteWeatherThreeDetail : 현장날씨 목록 조회(3일 3시간)
        /// <summary>
        /// 현장날씨 목록 조회(3일 3시간)
        /// </summary>
        public List<Weather> ListSiteWeatherThreeDetail(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd)
            };

            List<Weather> returnData = DBAccess.ListProcData<Weather>("PPT_Site_listSiteWeatherThreeDetail", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

    }
}

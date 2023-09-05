using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Weather
    {
        #region Weather : 날씨정보
        /// <summary>
        /// 현장코드
        /// </summary>
        public string SiteCd { get; set; }

        /// 현장명
        /// </summary>
        public string SiteNm { get; set; }

        /// <summary>
        /// 날짜
        /// </summary>
        public string WeatherDate { get; set; }

        /// <summary>
        /// 시간
        /// </summary>
        public string WeatherTime { get; set; }

        /// <summary>
        /// 날씨코드
        /// </summary>
        public string WeatherCode { get; set; }


        /// <summary>
        /// 날씨코드
        /// </summary>
        public string WeatherCodeDesc { get; set; }

        /// <summary>
        /// 기온
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 풍향
        /// </summary>
        public string WindDirection { get; set; }

        /// <summary>
        /// 풍속
        /// </summary>
        public string WindSpeed { get; set; }

        /// <summary>
        /// 1시간 강수량
        /// </summary>
        public string TimeRainfall { get; set; }

        /// <summary>
        /// 일누적 강수량
        /// </summary>
        public string DayRainfallAcct { get; set; }

        /// <summary>
        /// 습도
        /// </summary>
        public string Humidity { get; set; }

        /// <summary>
        /// 불쾌지수
        /// </summary>
        public string Discomfort { get; set; }

        /// <summary>
        /// PM10수치
        /// </summary>
        public string PM100Value { get; set; }

        /// <summary>
        /// PM10상태
        /// </summary>
        public string PM100State { get; set; }

        /// <summary>
        /// PM10상태
        /// </summary>
        public string PM100 { get; set; }


        /// <summary>
        /// PM2.5수치
        /// </summary>
        public string PM25Value { get; set; }

        /// <summary>
        /// PM2.5상태
        /// </summary>
        public string PM25State { get; set; }

        /// <summary>
        /// 강수량
        /// </summary>
        public string Rainfall { get; set; }

        /// <summary>
        /// 최저기온
        /// </summary>
        public string MinTemp { get; set; }

        /// <summary>
        /// 최고기온
        /// </summary>
        public string MaxTemp { get; set; }

        /// <summary>
        /// 강수확률
        /// </summary>
        public string RainfallPro { get; set; }

        /// <summary>
        /// 날짜
        /// </summary>
        public string WeekDate { get; set; }

        /// <summary>
        /// 눈
        /// </summary>
        public string Snow { get; set; }

        /// <summary>
        /// 눈(누적)
        /// </summary>
        public string SnowAcct { get; set; }

        /// <summary>
        /// Windy 좌표
        /// </summary>
        public string IframeSrc { get; set; }

        #endregion
    }
}

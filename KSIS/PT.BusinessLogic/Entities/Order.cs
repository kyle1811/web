using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Order
    {
        #region 공통 : 그리드 및 함수값 관련
        /// <summary>
        /// 체크박스
        /// </summary>
        public string ChkBox { get; set; }

        /// <summary>
        /// 반환값
        /// </summary>
        public string ReturnVal { get; set; }

        /// <summary>
        /// 순번
        /// </summary>
        public string RowNum { get; set; }

        /// <summary>
        /// 순번그룹
        /// </summary>
        public string RowGroup { get; set; }

        /// <summary>
        /// 저장방법
        /// </summary>
        public string SaveMode { get; set; }
        #endregion

        #region Order : 수주실적
        /// <summary>
        /// 부문코드
        /// </summary>
        public string SEQ { get; set; }

        /// <summary>
        /// 부문코드
        /// </summary>
        public string BizPartCd { get; set; }

        /// <summary>
        /// 담당부서코드
        /// </summary>
        public string ChargeDeptCd { get; set; }

        /// <summary>
        /// 프로젝트코드
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 현장명
        /// </summary>
        public string ProjectNm { get; set; }

        /// <summary>
        /// 수주시기
        /// </summary>
        public string ReceiveOrderFixDay { get; set; }

        /// <summary>
        /// 지분율
        /// </summary>
        public string ShareRate { get; set; }


        /// <summary>
        /// 부분명
        /// </summary>
        public string CdNm { get; set; }

        /// <summary>
        /// 부서코드
        /// </summary>
        public string DeptCd { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string DeptNm { get; set; }

        /// <summary>
        /// 연간계획금액
        /// </summary>
        public string YearConstAmt { get; set; }

        /// <summary>
        /// 당월계획금액
        /// </summary>
        public string ConstAmt { get; set; }

        /// <summary>
        /// 당월실적금액
        /// </summary>
        public string RsltAmt { get; set; }

        /// <summary>
        /// 누계계획금액
        /// </summary>
        public string AccConstAmt { get; set; }

        /// <summary>
        /// 누계실적금액
        /// </summary>
        public string AccRsltAmt { get; set; }

        /// <summary>
        /// 누계달성율
        /// </summary>
        public string AccRate { get; set; }

        /// <summary>
        /// 계획금액
        /// </summary>
        public string PlanFixAmt { get; set; }

        /// <summary>
        /// 실적금액
        /// </summary>
        public string RsltFixAmt { get; set; }

        /// <summary>
        /// 실적금액콤마표시
        /// </summary>
        public string RsltFixAmtComma { get; set; }

        /// <summary>
        /// 달성율
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// 연간 달성율
        /// </summary>
        public string YearRate { get; set; }

        /// <summary>
        /// 월별 달성율, 수주현황
        /// </summary>
        public string MonthRate { get; set; }

        /// <summary>
        /// 월별 
        /// </summary>
        public string FiscalMonth { get; set; }
        #endregion

        #region OrderState : 신규수주현황
        /// <summary>
        /// 등록번호
        /// </summary>
        public string RegNo { get; set; }

        /// <summary>
        /// 게시판타입코드
        /// </summary>
        public string OrderStateTypeCd { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 내용(Html)
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// 첨부ID
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 작성자ID
        /// </summary>
        public string RegUserId { get; set; }

        /// <summary>
        /// 작성자명
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 작성일시
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 박스
        /// </summary>
        public string Box { get; set; }

        #endregion

        #region SiteCoordinates : 현장좌표
        /// <summary>
        /// 위도
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 경도
        /// </summary>
        public string Longitude { get; set; }
        #endregion

        #region SiteWeather : 현장날씨
        /// <summary>
        /// 
        /// </summary>
        public string SiteCd { get; set; }
        /// <summary>
        /// 경도
        /// </summary>
        public string SiteNm { get; set; }
        /// <summary>
        /// 경도
        /// </summary>
        public string Time { get; set; }
    /// <summary>
        /// 경도
        /// </summary>
        public string WeatherCode { get; set; }
    /// <summary>
        /// 경도
        /// </summary>
        public string Temp { get; set; }
    /// <summary>
        /// <summary>
        /// 경도
        /// </summary>
        public string PM100 { get; set; }
        /// <summary>
        ///     /// <summary>
        /// 경도
        /// </summary>
        public string TimeRainfall { get; set; }
        /// <summary>
        ///     /// <summary>
        /// 경도
        /// </summary>
        public string DayRainfallAcct { get; set; }
        /// <summary>
        /// 경도
        /// </summary>
        public string WindDirection { get; set; }
    /// <summary>
        /// 경도
        /// </summary>
        public string WindSpeed { get; set; }
    /// <summary>
        /// 경도
        /// </summary>
        public string Humidity { get; set; }
    /// <summary>
        /// 경도
        /// </summary>
        public string Discomfort { get; set; }

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
        /// 윈디제어
        /// </summary>
        public string Windy { get; set; }

        /// <summary>
        /// 수주실적
        /// </summary>
        public string YearPlan { get; set; }

        /// <summary>
        /// 색상
        /// </summary>
        public string BarColor { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string Gubun { get; set; }

        /// <summary>
        /// 수주실적Comma
        /// </summary>
        public string YearPlanAmt { get; set; }
        #endregion

        #region SUJU : 수주관련
        /// <summary>
        /// 숫자
        /// </summary>
        public string CNT { get; set; }

        /// <summary>
        /// 전체 프로젝트명
        /// </summary>
        public string ProjectFullNm { get; set; }

        /// <summary>
        /// 발주처
        /// </summary>
        public string VendorNm { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public string FixAmt { get; set; }

        /// <summary>
        /// 수주게시판 금액
        /// </summary>
        public string GridFixAmt { get; set; }

        #endregion

        #region SujuPopUp : 수주팝업관련
        /// <summary>
        /// 수주담당부서
        /// </summary>
        public string ChargeDeptNm { get; set; }

        /// <summary>
        /// 수주프로젝트명
        /// </summary>
        public string SujuProjectNm { get; set; }

        /// <summary>
        /// 수주확정일자
        /// </summary>
        public string ReceiveFixDay { get; set; }

        /// <summary>
        /// 확정수주금액
        /// </summary>
        public string SujuFixAmt { get; set; }



        #endregion

    }
}

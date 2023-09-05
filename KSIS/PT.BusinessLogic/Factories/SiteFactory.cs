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
    public class SiteFactory
    {
        #region ListSitePhotoUrl : 현장사진 목록 조회
        /// <summary>
        /// 현장사진 목록 조회
        /// </summary>
        public List<Site> ListSitePhotoUrl(string pYearMon, string pBizPartCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizPartCd", pBizPartCd)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoUrl", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSitePhotoUrl : 현장사진 목록 조회(구분)
        /// <summary>
        /// 현장사진 목록 조회
        /// </summary>
        public List<Site> ListSitePhotoUrlGubun(string pYearMon, string pBizPartCd, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@Gubun",pGubun)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoUrl_Gubun", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSitePhotoUrl : 현장사진 목록 전체조회
        /// <summary>
        /// 현장사진 목록 전체조회
        /// </summary>
        public List<Site> ListSitePhotoUrlAll(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ProjectId", pSiteCd)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoUrlAll", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region getYearMainSitelist : 공사별 구분명칭 가져오기
        /// <summary>
        /// 현장(재해현황) 조회
        /// </summary>
        public Site getYearMainSitelist(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YEARMON", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_getYearMainSitelist", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Site>();
        }
        #endregion

        #region ListSiteStateRate : 현장현황 목록 조회
        /// <summary>
        /// 현장현황(실행율,공정율,원가율) 목록 조회
        /// </summary>
        public List<Site> ListSiteStateRate(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listWeeklySiteStateRate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSiteStateRateALL : 현장현황 목록 조회
        /// <summary>
        /// 현장현황(실행율,공정율,원가율) 목록 조회
        /// </summary>
        public List<Site> ListSiteStateRateALL(string pYearMon, string pBizpartCd, string pHome)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizpartCd", pBizpartCd),
                new SqlParameter("@Home", pHome)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listWeeklySiteStateRate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListHighRankSiteGrid : 현장(실행율) 상위3개 현장 조회
        /// <summary>
        /// 현장(실행율) 상위3개 현장 조회
        /// </summary>
        public List<Site> ListHighRankSiteGrid(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listHighRankSiteGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSiteAccidentGrid : 현장(재해현황) 조회
        /// <summary>
        /// 현장(재해현황) 조회
        /// </summary>
        public List<Site> ListSiteAccidentGrid(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Year", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSiteAccidentGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListHolidaySafetyWorkPlan : 휴일안전작업계획
        /// <summary>
        /// 현장(재해현황) 조회
        /// </summary>
        public List<Site> ListHolidaySafetyWorkPlan(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Year", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_HolidaySafetyWorkPlan_listSum", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region HolidaySafetyWorkPlan : 휴일안전작업계획 일자조회
        /// <summary>
        /// 현장(재해현황) 조회
        /// </summary>
        public Site HolidaySafetyWorkPlan(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Year", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_HolidaySafetyWorkPlan_getWorkingDay", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Site>();
        }
        #endregion

        #region ListConstructionSiteWorkersInfo : 현장인원 현황 테이블 조회
        /// <summary>
        ///  현장인원 현황 테이블 조회
        /// </summary>
        public List<Site> ListConstructionSiteWorkersInfo(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ProjectId", pSiteCd)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listConstructionSiteWorkersInfo", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetSiteInfo : 현장정보 조회
        /// <summary>
        /// 현장정보 조회
        /// </summary>
        public Site GetSiteInfo(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_getSiteInfo", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Site>();
        }
        #endregion

        #region ListSiteStateRateDetail : 현장별 공사진행현황 조회
        /// <summary>
        /// 현장별(실행율,공정율,원가율) 목록 조회
        /// </summary>
        public List<Site> ListSiteStateRateDetail(string pYearMon, string pSiteCd, string pHome)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@ProjectId", pSiteCd),
                new SqlParameter("@Home", pHome)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listWeeklySiteStateRateDetail", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSitePhotoDetailUrl : 현장상세 사진 목록 조회
        /// <summary>
        /// 현장상세 사진 목록 조회
        /// </summary>
        public List<Site> ListSitePhotoDetailUrl(string pSiteCd, string pNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd),
                new SqlParameter("@Number", pNumber)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoDetailUrl", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetSitePhotoDetailUrl : 현장상세 사진 목록 조회
        /// <summary>
        /// 현장상세 사진 목록 조회
        /// </summary>
        public Site GetSitePhotoDetailUrl(string pSiteCd, string pNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd),
                new SqlParameter("@Number", pNumber)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoDetailUrl", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Site>();
        }
        #endregion

        #region ListSalesRate : 분양율 조회
        /// <summary>
        /// 분양율 조회
        /// </summary>
        public List<Site> ListSalesRate(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_Rate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMoveInRate : 입주율 조회
        /// <summary>
        /// 입주율 조회
        /// </summary>
        public List<Site> ListMoveInRate(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_Rate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSalesRateGrid : 분양율 테이블(NEW)
        /// <summary>
        /// 분양율 테이블
        /// </summary>
        public List<Site> ListSalesRateGrid(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_Rate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        //#region ListSalesRateGrid : 분양율 테이블
        ///// <summary>
        ///// 분양율 테이블
        ///// </summary>
        //public List<Site> ListSalesRateGrid(string pYear)
        //{
        //    SqlParameter[] sqlParams = new SqlParameter[] {
        //        new SqlParameter("@YearMon", pYear)
        //    };

        //    List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_Rate", sqlParams, ConnStrNm.Main);

        //    return returnData;
        //}
        //#endregion

        #region ListMoveinRateGrid : 입주율 테이블 조회
        /// <summary>
        ///  입주율 테이블 조회
        /// </summary>
        public List<Site> ListMoveinRateGrid(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_Rate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSalesMoveInState : 월별 분양입주 현황 조회
        /// <summary>
        ///  월별 분양입주 현황 조회
        /// </summary>
        public List<Site> ListSalesMoveInState(string pYear, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@FiscalYear", pYear),
                new SqlParameter("@Gubun", pGubun)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntolist_MonthState", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSalesMoveinGrid : 분양입주현황 상세 테이블 조회
        /// <summary>
        /// 분양입주현황 상세 테이블 조회
        /// </summary>
        public List<Site> ListSalesMoveinGrid(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@FiscalYear", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_SalesPromotion_listYearMonMoveIntoDetaillist", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region CreditGrade : 신용등급현황 조회
        /// <summary>
        /// 
        /// </summary>
        public Site CreditGrade(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_CreditGrade_listCredit", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Site>();
        }
        #endregion

        #region ListCreditGradeChart : 신용등급현황 조회(차트)
        /// <summary>
        /// 
        /// </summary>
        public List<Site> ListCreditGradeChart(decimal pListCnt)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CNT", pListCnt)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_CreditGrade_listCreditChart", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSubContractRankingChart : 도급순위현황(차트)
        /// <summary>
        /// 도급순위현황(차트) 조회
        /// </summary>
        public List<Site> ListSubContractRankingChart(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_CreditGrade_listSubContractRankingChart", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSubContractRanking : 도급순위현황(테이블)
        /// <summary>
        /// 분양율 테이블
        /// </summary>
        public List<Site> ListSubContractRanking(string pYear)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYear)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_CreditGrade_listSubContractRanking", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSiteWeatherGubun : 현장 현재 날씨 조회(부문별)
        /// <summary>
        /// 현장 현재 날씨 조회
        /// </summary>
        public List<Site> ListSiteWeatherGubun(string pYearMon, string pBizPartCd, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@Gubun",pGubun)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSiteWeather_Gubun", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListWeatherMainGrid : 현장 현재 날씨 조회(부문별)
        /// <summary>
        /// 현장 현재 날씨 조회(부문별)
        /// </summary>
        public List<Order> ListWeatherMainGrid(string pYearMon, string pBizPartCd, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@Gubun",pGubun)
                //new SqlParameter("@Cnt", pCnt)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Site_listSiteWeather_Gubun", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListWeatherDetailWeekGrid : 현장 현재 날씨 조회(부문별)
        /// <summary>
        /// 현장 현재 날씨 조회상세(현장별)
        /// </summary>
        public List<Order> ListWeatherDetailWeekGrid(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Site_listSiteWeather_DetailWeek", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region listSiteStateYn : 현장 유무 판단
        /// <summary>
        /// 현장 유무 판단
        /// </summary>
        public string listSiteStateYn(string pBizPartCd, string pSiteCd)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@ProjectId", pSiteCd)
            };

            DBAccess.ExcProc("PPT_Manage_listSiteStateYn", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region GridSiteState : 공사진행현황 테이블 조회
        /// <summary>
        /// 공사진행현황 테이블 조회
        /// </summary>
        public List<Site> GridSiteState(string pYearMon, string pBizPartCd, string pHome)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@Home", pHome)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listWeeklySiteStateRate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListCompletionSiteTreeView : 준공현황 트리뷰 조회
        /// <summary>
        /// 부서목록 트리뷰 조회
        /// </summary>
        public List<Manage> ListCompletionSiteTreeView(string pDeptCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd)
            };

            List<Manage> returnData = DBAccess.ListProcData<Manage>("PPT_Site_listCompletionSiteTreeView", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListCompletionSiteInfoDetail : 현장정보 상세조회
        /// <summary>
        /// 현장정보 상세조회
        /// </summary>
        public List<Site> ListCompletionSiteInfoDetail(string pSiteCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SiteCd", pSiteCd)
            };

            List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_getSiteInfoDetail", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        //#region ListSitePhotoUrl : 현장사진 목록 조회(구분)
        ///// <summary>
        ///// 현장사진 목록 조회
        ///// </summary>
        //public List<Site> ListSitePhotoUrlGubun(string pYearMon, string pBizPartCd, string pGubun)
        //{
        //    SqlParameter[] sqlParams = new SqlParameter[] {
        //        new SqlParameter("@YearMon", pYearMon),
        //        new SqlParameter("@BizPartCd", pBizPartCd),
        //        new SqlParameter("@Gubun",pGubun)
        //    };

        //    List<Site> returnData = DBAccess.ListProcData<Site>("PPT_Site_listSitePhotoUrl_Gubun", sqlParams, ConnStrNm.Main);

        //    return returnData;
        //}
        //#endregion


    }
}

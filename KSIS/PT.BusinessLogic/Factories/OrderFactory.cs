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
    public class OrderFactory
    {
        #region ListOrderResultAll : 수주실적(전체) 조회 그래프
        /// <summary>
        /// 수주실적(전체) 조회 그래프
        /// </summary>
        public List<Order> ListOrderResultAll(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Suju_listSujuRsltMonthRate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListOrderResultAllGrid : 수주실적(전체) 조회 테이블
        /// <summary>
        /// 수주실적(전체) 조회 테이블
        /// </summary>
        public List<Order> ListOrderResultAllGrid(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Suju_listSujuRsltMonthAllHome", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListOrderResultAllGridHome : 수주실적(전체) 조회 홈 테이블
        /// <summary>
        /// 수주실적(전체) 조회 테이블
        /// </summary>
        public List<Order> ListOrderResultAllGridHome(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Suju_listSujuRsltMonthAllHome", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListOrderResultDeptGrid : 수주실적(부서) 조회 테이블
        /// <summary>
        /// 수주실적(부서) 조회 테이블
        /// </summary>
        public List<Order> ListOrderResultDeptGrid(string pYearMon, string pCnt)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon),
                new SqlParameter("@Cnt", pCnt)
            };
            
            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Suju_listSujuRsltMonthDept", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListOrderStateGrid : 신규수주현황 목록 그리드(조회)
        /// <summary>
        /// 신규수주현황 목록 그리드(조회)
        /// </summary>
        public List<Order> ListOrderStateGrid(string pOrderStateTypeCd, string pSubject, string pProjectId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@OrderStateTypeCd", pOrderStateTypeCd),
                new SqlParameter("@Subject", pSubject),
                new SqlParameter("@ProjectId", pProjectId)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_OrderState_listOrderStateGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListGetRegNo : 게시물 번호 가지고 오기
        /// <summary>
        /// 신규수주현황 목록 그리드(조회)
        /// </summary>
        public List<Order> ListGetRegNo(string pOrderStateTypeCd, string pSubject, string pProjectId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@OrderStateTypeCd", pOrderStateTypeCd),
                new SqlParameter("@Subject", pSubject),
                new SqlParameter("@ProjectId", pProjectId)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_OrderState_listOrderStateGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListProject : 수주정보 조회
        /// <summary>
        /// 사용자정보 조회
        /// </summary>
        public Order ListProject(string pOrderStateTypeCd, string pSubject, string pProjectId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@OrderStateTypeCd", pOrderStateTypeCd),
                new SqlParameter("@Subject", pSubject),
                new SqlParameter("@ProjectId", pProjectId)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_OrderState_listOrderStateGrid", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Order>();
        }
        #endregion


        #region GetOrderState : 신규수주현황 조회
        /// <summary>
        /// 신규수주현황 조회
        /// </summary>
        public Order GetOrderState(string pRegNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@RegNo", pRegNo)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_OrderState_getOrderState", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Order>();
        }
        #endregion

        #region SaveOrderState : 신규수주현황 저장
        /// <summary>
        /// 신규수주현황 저장
        /// </summary>
        public string SaveOrderState(Order pOrderState, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),       
                new SqlParameter("@SaveMode", pOrderState.SaveMode),
                new SqlParameter("@RegNo", pOrderState.RegNo),
                new SqlParameter("@OrderStateTypeCd", pOrderState.OrderStateTypeCd),
                new SqlParameter("@Subject", pOrderState.Subject),
                new SqlParameter("@Content", pOrderState.Content),
                new SqlParameter("@AttId", pOrderState.AttId),
                new SqlParameter("@UserId", pUserId),
            };

            DBAccess.ExcProc("PPT_OrderState_saveOrderState", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region ListNewSujuState : 신규수주현황 목록 그리드(홈)
        /// <summary>
        /// 신규수주현황 목록 그리드(홈)
        /// </summary>
        public List<Order> ListNewSujuState(string pSubject)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_OrderState_listNewSujuStateHome", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetSiteCoordinates : 현장좌표값 가지고오기
        /// <summary>
        /// 현장좌표값 가지고오기
        /// </summary>
        public List<Order> GetSiteCoordinates()
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Manage_getSiteCoordinates", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region GetSiteCoordinatesSearch : 현장좌표값 가지고오기
        /// <summary>
        /// 현장좌표값 가지고오기
        /// </summary>
        public List<Order> GetSiteCoordinatesSearch(string pStieNm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@pStieNm", pStieNm)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Manage_getSiteCoordinatesSearch", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region GetSiteCoordinatesGubun : 부문별 현장좌표값 가지고오기
        /// <summary>
        /// 부문별 현장좌표값 가지고오기
        /// </summary>
        public List<Order> GetSiteCoordinatesGubun(string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@pGubun", pGubun)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Manage_getSiteCoordinatesGubun", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListChartAccSUJU : 수주실적현황 조회
        public List<Order> ListChartAccSUJU(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YEARMON", pYearMon)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Manage_listSujuPlanResultHome", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSujuMonthResult : 월별수주실적 팝업
        /// <summary>
        /// 수주실적(전체) 조회 테이블
        /// </summary>
        public List<Order> ListSujuMonthResult(string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@YearMon", pYearMon)
            };

            List<Order> returnData = DBAccess.ListProcData<Order>("PPT_Suju_listSujuRsltMonthRate", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


    }
}

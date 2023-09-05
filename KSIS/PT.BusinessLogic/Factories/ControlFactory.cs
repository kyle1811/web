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
    public class ControlFactory
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

        #region ListControlBudgetGrid : 가용예산 조회 테이블
        /// <summary>
        /// 가용예산 조회 테이블
        /// </summary>
        public List<Control> ListControlBudgetGrid(string pDeptCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@DeptCd", pDeptCd)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listBudget", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListControlDailyDepositGrid : 일일입금현황 조회 테이블
        /// <summary>
        /// 일일입금현황 조회 테이블
        /// </summary>
        public List<Control> ListControlDailyDepositGrid(string pDatePick1, string pDatePick2)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@sdate", pDatePick1),
                new SqlParameter("@edate", pDatePick2)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listDailyDeposit", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListControlDailyDepositRealTimeGrid : 일일입금현황(실시간) 조회 테이블
        /// <summary>
        /// 일일입금현황 조회 테이블
        /// </summary>
        public List<Control> ListControlDailyDepositRealTimeGrid(string pDatePick1, string pDatePick2, string pAcct, string pGubun)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@sdate", pDatePick1),
                new SqlParameter("@edate", pDatePick2),
                new SqlParameter("@Account", pAcct),
                new SqlParameter("@Gubun", pGubun)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listDailyDepositRealTime", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListSearchVendor : 업체 조회 그리드
        /// <summary>
        /// 업체 조회 그리드
        /// </summary>
        public List<Control> ListSearchVendor(string pVendornm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@Vendornm", pVendornm)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Cooperator_listVendor", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetCooperatorDetail : 협력업체 상세조회
        /// <summary>
        /// 협력업체 상세조회
        /// </summary>
        public Control GetCooperatorDetail(string pVendorCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@VendorCd", pVendorCd)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Cooperator_listVendorDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Control>();
        }
        #endregion

        #region GetCooperatorClassification : 협력업체 분류조회
        /// <summary>
        /// 협력업체 분류조회
        /// </summary>
        public Control GetCooperatorClassification(string pVendorCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@VendorCd", pVendorCd)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Cooperator_listVendorClassification", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Control>();
        }
        #endregion

        #region ListBizCostGrid : 업무추진비 사용 신청/승인 조회
        /// <summary>
        /// 업체 조회 그리드
        /// </summary>
        public List<Control> ListBizCostGrid(string pDeptCd, string pEmpNo, string pYearMon)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CostCenter", pDeptCd),
                new SqlParameter("@EmpNo", pEmpNo),
                new SqlParameter("@UseYearMon", pYearMon)

            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listBizCost", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetBizCostDetail : 업무추진비 상세조회
        /// <summary>
        /// 업무추진비 상세조회
        /// </summary>
        public Control GetBizCostDetail(string pCostSeq)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CostSeq", pCostSeq)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listBizCostDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Control>();
        }
        #endregion

        #region UpdateBizCost : 업무추진비 승인/반려
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string UpdateBizCost(Control pControl, string pEmpNo)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@UpdateMode", pControl.UpdateMode),
                new SqlParameter("@CostSeq", pControl.CostSeq),
                new SqlParameter("@EmpNo", pEmpNo)
            };

            DBAccess.ExcProc("PPT_Control_updateBizCost", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region ListUserUsageStatisticsForKSIS : KSIS 사용자 통계
        /// <summary>
        /// KSIS 사용자 통계
        /// </summary>
        public List<Control> ListUserUsageStatisticsForKSIS(string pGUBUN1, string pGUBUN2, string pFROM, string pTO, string pEPUserid)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@GUBUN1", pGUBUN1),
                new SqlParameter("@GUBUN2", pGUBUN2),
                new SqlParameter("@FROM", pFROM),
                new SqlParameter("@TO", pTO),
                new SqlParameter("@EPUserid", pEPUserid),
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Person_listUserUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUserStatisticsDetailForKSIS : KSIS 사용자별 통계
        /// <summary>
        /// KSIS 사용자별 통계
        /// </summary>
        public List<Control> ListUserStatisticsDetailForKSIS(string pGUBUN1, string pGUBUN2, string pFROM, string pTO, string pEPUserid)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@GUBUN1", pGUBUN1),
                new SqlParameter("@GUBUN2", pGUBUN2),
                new SqlParameter("@FROM", pFROM),
                new SqlParameter("@TO", pTO),
                new SqlParameter("@EPUserid", pEPUserid),
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Person_listUserUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMenuUsageStatisticsForKSIS : KSIS 메뉴 통계
        /// <summary>
        /// KSIS 사용자 통계
        /// </summary>
        public List<Control> ListMenuUsageStatisticsForKSIS(string pGUBUN1, string pGUBUN2, string pFROM, string pTO, string pSelect, string pTitleCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@GUBUN1", pGUBUN1),
                new SqlParameter("@GUBUN2", pGUBUN2),
                new SqlParameter("@FROM", pFROM),
                new SqlParameter("@TO", pTO),
                new SqlParameter("@SELECT", pSelect),
                new SqlParameter("@TitleCd", pTitleCd),
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Person_listMenuUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMenuStatisticsDetailForKSIS : KSIS 메뉴별 통계
        /// <summary>
        /// KSIS 메뉴별 통계
        /// </summary>
        public List<Control> ListMenuStatisticsDetailForKSIS(string pGUBUN1, string pGUBUN2, string pFROM, string pTO, string pSelect, string pTitleCd)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@GUBUN1", pGUBUN1),
                new SqlParameter("@GUBUN2", pGUBUN2),
                new SqlParameter("@FROM", pFROM),
                new SqlParameter("@TO", pTO),
                new SqlParameter("@SELECT", pSelect),
                new SqlParameter("@TitleCd", pTitleCd),
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Person_listMenuUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUsageCircleChartForKSIS : 전체 사용 비중 조회(원)
        /// <summary>
        /// 
        /// </summary>
        public List<Control> ListUsageCircleChartForKSIS(string pScreenCd, string pGUBUN, string pSELECT, string pTerm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ScreenCd", pScreenCd),
                new SqlParameter("@GUBUN", pGUBUN),
                new SqlParameter("@SELECT", pSELECT),
                new SqlParameter("@Term", pTerm)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listTableUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUsageBarChartForKSIS : 분기별 사용 조회(막대)
        /// <summary>
        /// 
        /// </summary>
        public List<Control> ListUsageBarChartForKSIS(string pScreenCd, string pGUBUN, string pSELECT, string pTerm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ScreenCd", pScreenCd),
                new SqlParameter("@GUBUN", pGUBUN),
                new SqlParameter("@SELECT", pSELECT),
                new SqlParameter("@Term", pTerm)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listTableUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListMenuStatisticsDetailForKSIS : KSIS 집계 테이블
        /// <summary>
        /// KSIS 집계 테이블
        /// </summary>
        public List<Control> ListUsageStatisticsTableForKSIS(string pScreenCd, string pGUBUN, string pSelect, string pTerm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ScreenCd", pScreenCd),
                new SqlParameter("@GUBUN", pGUBUN),
                new SqlParameter("@SELECT", pSelect),
                new SqlParameter("@Term", pTerm)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listTableUsageStatisticsForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region listUsagePopUp : KSIS 사용자 이력 정보 조회
        /// <summary>
        /// KSIS 집계 테이블
        /// </summary>
        public List<Control> ListUsagePopUp(string pScreenCd, string pGUBUN, string pDutyCd, string pSelect)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ScreenCd", pScreenCd),
                new SqlParameter("@GUBUN", pGUBUN),
                new SqlParameter("@DutyCd", pDutyCd),
                new SqlParameter("@Select", pSelect)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listUsagePopUpForKSIS", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetAppVersion : Voice_K 버전 조회
        /// <summary>
        ///   Voice_K 버전 조회
        /// </summary>
        public Control GetAppVersion(string pAppName)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AppName", pAppName)
            };

            //DBAccess.ExcProc("PCO_Mobile_checkAppVer", sqlParams, ConnStrNm.Main);

            //returnResult = sqlParams[0].Value.ToString(); //결과값

            List<Control> returnData = DBAccess.ListProcData<Control>("PCO_Mobile_checkAppVer", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Control>();
        }
        #endregion

        #region ListBudgetDetailGrid : 업무추진비 상세조회 테이블
        /// <summary>
        /// 업무추진비 상세조회 테이블
        /// </summary>
        public List<Control> ListBudgetDetailGrid(string pCostSeq)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@CostSeq", pCostSeq)
            };

            List<Control> returnData = DBAccess.ListProcData<Control>("PPT_Control_listBizCostDetail", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion
    }
}

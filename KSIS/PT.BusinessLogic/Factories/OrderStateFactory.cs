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
    public class OrderStateFactory
    {
        #region ListOrderStateGrid : 수주 목록 그리드 조회
        /// <summary>
        /// 회의자료 목록 그리드 조회
        /// </summary>
        public List<OrderState> ListOrderStateGrid(string pOrderStateTypeCd, string pSubject)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@OrderStateTypeCd", pOrderStateTypeCd),
                new SqlParameter("@Subject", pSubject)
            };

            List<OrderState> returnData = DBAccess.ListProcData<OrderState>("PPT_OrderState_listOrderStateGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetOrderState : 회의자료 조회
        /// <summary>
        /// 회의자료 조회
        /// </summary>
        public OrderState GetOrderState(string pRegNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@RegNo", pRegNo)
            };

            List<OrderState> returnData = DBAccess.ListProcData<OrderState>("PPT_OrderState_getOrderState", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<OrderState>();
        }
        #endregion

        #region SaveOrderState : 회의자료 저장
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string SaveOrderState(OrderState pOrderState, string pUserId)
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



    }
}
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
    public class UsedSaleFactory
    {
        #region GetOnProgress : 진행중인나눔
        /// <summary>
        /// 진행중인나눔
        /// </summary>
        /// <param name="pItemNm">조회조건 물품명</param>
        /// <returns></returns>
        public List<UsedSale> GetOnProgress(string pSubject, string pOption)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ItemNm", pSubject)
               ,new SqlParameter("@Option", pOption)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleOnProgress", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetOnComplete : 진행완료나눔
        /// <summary>
        /// 진행완료나눔
        /// </summary>
        /// <param name="pItemNm">조회조건 물품명</param>
        /// <returns></returns>
        public List<UsedSale> GetOnComplete(string pSubject, string pUserId, string pOption)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ItemNm", pSubject)
               ,new SqlParameter("@UserId", pUserId)
               ,new SqlParameter("@Option", pOption)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleOnComplete", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUsedSaleDetail : 물품 상세조회 그리드
        /// <summary>
        /// 물품 상세조회 그리드
        /// </summary>
        /// <param name="pUsedSaleNo">등록번호</param>
        /// <returns></returns>
        public UsedSale ListUsedSaleDetail(string pUsedSaleNo, string pStatus, string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                 new SqlParameter("@UsedSaleNo", pUsedSaleNo)
                ,new SqlParameter("@Status", pStatus)
                ,new SqlParameter("@UserId", pUserId)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<UsedSale>(); ;
        }
        #endregion

        #region ListUsedSaleDetailOther : 물품 상세조회 그리드 유동적
        /// <summary>
        /// 물품 상세조회 그리드
        /// </summary>
        /// <param name="pUsedSaleNo">등록번호</param>
        /// <returns></returns>
        public List<UsedSale> ListUsedSaleDetailOther(string pUsedSaleNo, string pGubun, string pStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                 new SqlParameter("@UsedSaleNo", pUsedSaleNo)
                ,new SqlParameter("@Gubun", pGubun)
                ,new SqlParameter("@Status", pStatus)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleDetail", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUsedSaleComment : 댓글 조회
        /// <summary>
        /// 댓글 조회
        /// </summary>
        /// <param name="pItemNm">조회조건 물품번호</param>
        /// <returns></returns>
        public List<UsedSale> ListUsedSaleComment(string pUsedSaleNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UsedSaleNo", pUsedSaleNo)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleComment", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListUsedSaleCommentOther : 댓글 조회(유동적)
        /// <summary>
        /// 댓글 조회
        /// </summary>
        /// <param name="pUsedSaleNo">조회조건 물품번호</param>
        /// <param name="pUserId">조회조건 로그인 사번</param>
        /// <returns></returns>
        public List<UsedSale> ListUsedSaleCommentOther(string pUsedSaleNo, string pUserId, string pStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UsedSaleNo", pUsedSaleNo)
               ,new SqlParameter("@UserId", pUserId)
               ,new SqlParameter("@Status", pStatus)
            };

            List<UsedSale> returnData = DBAccess.ListProcData<UsedSale>("PPT_Information_getUsedSaleComment", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region SaveUsedSaleComment : 댓글 저장
        /// <summary>
        /// 댓글 저장
        /// </summary>
        public string SaveUsedSaleComment(string pUsedSaleNo, string pCommentText, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@UsedSaleNo",   pUsedSaleNo),
                new SqlParameter("@CommentText", pCommentText),
                new SqlParameter("@UserId",   pUserId)
            };

            DBAccess.ExcProc("PPT_Information_InsertUsedSaleComment", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region DeleteUsedSaleComment : 댓글 삭제
        /// <summary>
        /// 댓글 저장
        /// </summary>
        public string DeleteUsedSaleComment(string pUsedSaleNo, string pCommentSeq, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@UsedSaleNo",   pUsedSaleNo),
                new SqlParameter("@CommentSeq", pCommentSeq),
                new SqlParameter("@UserId",   pUserId)
            };

            DBAccess.ExcProc("PPT_Information_DeleteUsedSaleComment", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region SaveUsedSaleComplete : 판매완료 처리
        /// <summary>
        /// 댓글 저장
        /// </summary>
        public string SaveUsedSaleComplete(string pUsedSaleNo, string pUserId, string pStatus)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@UsedSaleNo",   pUsedSaleNo),
                new SqlParameter("@UserId",   pUserId),
                new SqlParameter("@Status",   pStatus)
            };

            DBAccess.ExcProc("PPT_Information_UpdateUsedSaleComplete", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region SaveUsedSale : 물품나눔 등록
        /// <summary>
        /// 회의자료 저장
        /// </summary>
        public string SaveUsedSale(UsedSale pUsedSale)
        {
            string returnResult = "";

            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UsedSaleNo",pUsedSale.UsedSaleNo ),
                new SqlParameter("@SharingType",pUsedSale.SharingType ),
                new SqlParameter("@ItemNm",pUsedSale.ItemNm ),
                //new SqlParameter("@UsePeriod",Convert.ToInt32(pUsedSale.UsePeriod) ),
                new SqlParameter("@Price",pUsedSale.Price ),
                //new SqlParameter("@TradeWay",pUsedSale.TradeWay ),
                new SqlParameter("@MoreInfo",pUsedSale.MoreInfo ),
                new SqlParameter("@RegUserId",pUsedSale.RegUserId ),
                new SqlParameter("@AttId",pUsedSale.AttId ),
                new SqlParameter("@ShippingMethod",pUsedSale.ShippingMethod),
                new SqlParameter("@PeriodType", pUsedSale.PeriodType),
                new SqlParameter("@SaveMode", pUsedSale.SaveMode)
            };

            DBAccess.ExcProc("PPT_Information_insertUsedSaleItem", sqlParams, ConnStrNm.Main);

            returnResult = sqlParams[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region DeleteUsedSaleItem : 나눔물품 삭제
        /// <summary>
        /// 댓글 저장
        /// </summary>
        public string DeleteUsedSaleItem(string pUsedSaleNo, string pAttId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@UsedSaleNo",   pUsedSaleNo)
               ,new SqlParameter("@AttId",   pAttId)
            };

            DBAccess.ExcProc("PPT_Information_deleteUsedSaleItem", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

    }
}

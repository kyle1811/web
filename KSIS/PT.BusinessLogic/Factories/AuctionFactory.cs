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
    public class AuctionFactory
    {
        #region GetOnProgress : 진행중인경매
        /// <summary>
        /// 진행 중인 경매
        /// </summary>
        /// <param name="pItemNm">조회조건 물품명</param>
        /// <returns></returns>
        public List<Auction> GetOnProgress(string pSubject)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@ItemNm", pSubject)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getOnProgress", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetAuction : 진행중인경매 상세 조회
        /// <summary>
        /// 진행중인경매 조회
        /// </summary>
        public Auction GetAuction(string pAuctionNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AuctionNo", pAuctionNo)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Auction>();
        }
        #endregion

        #region GetComment : 댓글 조회
        /// <summary>
        /// 댓글 조회
        /// </summary>
        /// <param name="pItemNm">조회조건 물품명</param>
        /// <returns></returns>
        public List<Auction> GetComment(string pAuctionNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AuctionNo", pAuctionNo)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getComment", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region  ListBidDetailPopUP : 입찰내역(경매기록) 조회
        /// <summary>
        /// 댓글 조회
        /// </summary>
        /// <param name="pItemNm">조회조건 물품명</param>
        /// <returns></returns>
        public List<Auction> ListBidDetailPopUP(string pAuctionNo, string pUserId, string pListType)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AuctionNo", pAuctionNo),
                new SqlParameter("@BidderID",  pUserId),
                new SqlParameter("@ListType",  pListType)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_listBidDetailPopUP", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetCurPrice : 현재최고가 조회
        /// <summary>
        /// 현재최고가 조회
        /// </summary>
        /// <param name="pItemNm">조회조건 경매번호</param>
        /// <returns></returns>
        public Auction GetCurPrice(string pAuctionNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AuctionNo", pAuctionNo)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getCurPrice", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Auction>();
            
        }
        #endregion

        #region SaveComment : 댓글 저장
        /// <summary>
        /// 댓글 저장
        /// </summary>
        public string SaveComment(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@AuctionNo",   pAuction.AuctionNo),
                new SqlParameter("@CommentText", pAuction.Content),
                new SqlParameter("@RegUserID",   pUserId),
            };

            DBAccess.ExcProc("PPT_Auction_insertComment", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region SavePrice : 입찰금액 저장
        /// <summary>
        /// 입찰금액 저장
        /// </summary>
        public string SavePrice(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            pAuction.BidPrice = pAuction.BidPrice.Replace(",", "");

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 12),
                new SqlParameter("@AuctionNo",   pAuction.AuctionNo),
                new SqlParameter("@BidderID",    pUserId),
                new SqlParameter("@BidPrice",    pAuction.BidPrice),
            };

            DBAccess.ExcProc("PPT_Auction_insertBid", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region GetAuctionBiddingInfo : 참여한경매
        /// <summary>
        /// 참여한경매 조회
        /// </summary>
        public List<Auction> GetAuctionBiddingInfo(string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserID", pUserId)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getAuctionBiddingInfo", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region GetAuctionSellingInfo : 경매관리자
        /// <summary>
        /// 참여한경매 조회
        /// </summary>
        public List<Auction> GetAuctionSellingInfo(string pUserId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@UserID", pUserId)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getAuctionSellingInfo", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region SaveAuction : 물품등록 저장
        /// <summary>
        /// 물품등록 저장
        /// </summary>
        public string SaveAuction(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            string StartDate = "";
            string CloseDate = "";

            StartDate = pAuction.StartDateYear + pAuction.StartDateMonth + pAuction.StartDateDay + " " + pAuction.StartDateHour + ":" + pAuction.StartDateMinute;
            CloseDate = pAuction.CloseDateYear + pAuction.CloseDateMonth + pAuction.CloseDateDay + " " + pAuction.CloseDateHour + ":" + pAuction.CloseDateMinute;

            pAuction.StartPrice = pAuction.StartPrice.Replace(",","");
            pAuction.SellQuantity = pAuction.SellQuantity.Replace(",", "");
            pAuction.BidPriceUnit = pAuction.BidPriceUnit.Replace(",", "");
            pAuction.LowPriceUnit = pAuction.LowPriceUnit.Replace(",", "");

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 18),
                new SqlParameter("@AuctionNo",      pAuction.AuctionNo),
                new SqlParameter("@ItemNm",         pAuction.ItemNm),
                new SqlParameter("@UsePeriod",      Convert.ToInt64(pAuction.UsePeriod)),
                new SqlParameter("@StartPrice",     Convert.ToInt64(pAuction.StartPrice)),
                new SqlParameter("@StartDate",      StartDate),
                new SqlParameter("@CloseDate",      CloseDate),
                new SqlParameter("@ShippingCharge", pAuction.ShippingCharge),
                new SqlParameter("@ImageFile1",     pAuction.ImageFile1),
                new SqlParameter("@ImageFile2",     pAuction.ImageFile2),
                new SqlParameter("@ImageFile3",     pAuction.ImageFile3),
                new SqlParameter("@DetailInfo",     pAuction.DetailInfo),
                new SqlParameter("@SellQuantity",   Convert.ToInt64(pAuction.SellQuantity)),
                new SqlParameter("@RegUserID",      pUserId),	
                new SqlParameter("@AttId",          pAuction.AttId),
                new SqlParameter("@AuctionType",    pAuction.AuctionType),
                new SqlParameter("@BidPriceUnit",   Convert.ToInt64(pAuction.BidPriceUnit)),
                new SqlParameter("@LowPriceUnit",   Convert.ToInt64(pAuction.LowPriceUnit))
            };

            DBAccess.ExcProc("PPT_Auction_insertAuction", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region GetRegAuction : 물품등록 조회
        /// <summary>
        /// 물품등록 조회
        /// </summary>
        public Auction GetRegAuction(string pAuctionNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AuctionNo", pAuctionNo)
            };

            List<Auction> returnData = DBAccess.ListProcData<Auction>("PPT_Auction_getRegistrationDetail", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<Auction>();
        }
        #endregion

        #region SavePostNotice : 입찰공고
        /// <summary>
        /// 입찰공고 저장
        /// </summary>
        public string SavePostNotice(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 15),
                new SqlParameter("@AuctionNo",      pAuction.AuctionNo),
                new SqlParameter("@RegUserID",      pUserId),
            };

            DBAccess.ExcProc("PPT_Auction_SaveAuctionPostNotice", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region SaveAuctionStatusCancel : 입찰취소
        /// <summary>
        /// 입찰취소 저장
        /// </summary>
        public string SaveAuctionStatusCancel(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 15),
                new SqlParameter("@AuctionNo",      pAuction.AuctionNo),
                new SqlParameter("@RegUserID",      pUserId),
            };

            DBAccess.ExcProc("PPT_Auction_SaveAuctionStatusCancel", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region deleteAuctionRegistration : 물품등록삭제
        /// <summary>
        /// 물품등록삭제 저장
        /// </summary>
        public string deleteAuctionRegistration(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 15),
                new SqlParameter("@AuctionNo",      pAuction.AuctionNo),
                new SqlParameter("@RegUserID",      pUserId),
            };

            DBAccess.ExcProc("PPT_Auction_deleteAuctionRegistration", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion

        #region closeAuction : 낙찰
        /// <summary>
        /// 물품등록삭제 저장
        /// </summary>
        public string closeAuction(Auction pAuction, string pUserId)
        {
            string returnResult = "";

            SqlParameter[] sqlParam = new SqlParameter[] {
                DBUtil.MakeSqlParameter("@Result", "", ParameterDirection.Output, 15),
                new SqlParameter("@AuctionNo",      pAuction.AuctionNo),
                new SqlParameter("@RegUserID",      pUserId),
            };

            DBAccess.ExcProc("PPT_Auction_closeAuction", sqlParam, ConnStrNm.Main);

            returnResult = sqlParam[0].Value.ToString(); //결과값

            return returnResult;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Auction
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

        /// <summary>
        /// 작성자ID
        /// </summary>
        public string RegUserId { get; set; }

        /// <summary>
        /// 작성자명
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 첨부ID
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 등록번호
        /// </summary>
        public string RegNo { get; set; }
        #endregion

        #region 경매

        //경매번호
        public string AuctionNo { get; set; }

        //물품명
        public string ItemNm { get; set; }

        //현재가
        public string CurPrice { get; set; }

        //경매종료일시
        public string CloseDate { get; set; }

        //입찰수
        public string BidCnt { get; set; }

        //이미지
        public string ImageFile1 { get; set; }

        public string ImageFile2 { get; set; }

        public string ImageFile3 { get; set; }

        //등록일자
        public string RegDate { get; set; }

        //댓글
        public string CommentCnt { get; set; }

        public string UsePeriod { get; set; }

        public string StartPrice { get; set; }

        public string RemainTime { get; set; }

        public string ShippingCharge { get; set; }

        public string DetailInfo { get; set; }

        public string AuctionStatus { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string HtmlContent { get; set; }

        public string AuctionTypeCd { get; set; }

        public string AuctionRegUserId { get; set; }

        public string AuctionRegUserNm { get; set; }

        public string ServerTime { get; set; }

        public string CommentSeq { get; set; }

        public string CommentText { get; set; }

        public string HtmlComment { get; set; }

        public string CommentRegDate { get; set; }

        public string CommentRegUserID { get; set; }
        
        // 입찰
        public string BidPrice { get; set; }

        // 참여한경매
        public string SellRegUserNm { get; set; }

        public string MyBidPrice { get; set; }

        public string WinningBidPrice { get; set; }

        // 경매관리자
        public string BuyerNm { get; set; }

        public string CancelDate { get; set; }

        // 물품등록
        public string SellQuantity { get; set; }

        public string StartDate { get; set; }

        public string StartDateYear { get; set; }

        public string StartDateMonth { get; set; }

        public string StartDateDay { get; set; }

        public string StartDateHour { get; set; }

        public string StartDateMinute { get; set; }

        public string CloseDateYear { get; set; }

        public string CloseDateMonth { get; set; }

        public string CloseDateDay { get; set; }

        public string CloseDateHour { get; set; }

        public string CloseDateMinute { get; set; }
        
        public string AuctionType { get; set; }
        
        public string BidPriceUnit { get; set; }

        public string LowPriceUnit { get; set; }

        public string errormsg { get; set; }
        
        
        // 경매기록
        public string BidderID { get; set; }

        public string BidDate { get; set; }


        #endregion
    }
}

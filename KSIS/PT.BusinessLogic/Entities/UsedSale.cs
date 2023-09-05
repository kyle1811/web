using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class UsedSale
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

        #region 물품나눔
        
        //번호
        public string UsedSaleNo { get; set; }

        //제목
        public string ItemNm { get; set; }


        //상품상태
        public string ItemStatus { get; set; }


        //작성자
        public string Price { get; set; }

        //작성일
        public string RegDate { get; set; }

        //부서
        public string DeptNm { get; set; }

        //거래방법
        public string TradeWay { get; set; }

        //상세정보
        public string MoreInfo { get; set; }

        //기간 타입
        public string PeriodType { get; set; }
        
        //조회수
        public string Views { get; set; }

        //사용기간
        public string UsePeriod { get; set; }

        //댓글수
        public string Comment { get; set; }

        //댓글내용
        public string CommentText { get; set; }

        //댓글작성일자
        public string CommentRegDate { get; set; }

        //댓글작성자부서
        public string CommentDeptNm { get; set; }

        //댓글작성자이름
        public string CommentUserNm { get; set; }

        //이미지경로
        public string ImgSrc { get; set; }

        //이미지경로1
        public string ImgSrc1 { get; set; }

        //이미지경로2
        public string ImgSrc2 { get; set; }

        //이미지경로3
        public string ImgSrc3 { get; set; }

        public string Content { get; set; }

        public string HtmlContent { get; set; }

        //신규댓글여부
        public string CurrentYN { get; set; }

        //전화번호
        public string TelNo { get; set; }

        //배송방법
        public string ShippingMethod { get; set; }

        //이메일
        public string EMail { get; set; }

        //댓글순번
        public string CommentSeq { get; set; }

        //댓글순번
        public string UsedSaleSeq { get; set; }

        //사번
        public string RegUser { get; set; }

        //나눔유형
        public string SharingType { get; set; }

        //댓글삭제
        public string DeleteYn { get; set; }

        //댓글작성자ID
        public string CommentUserId { get; set; }

        //작성자 E-mail
        public string RegUserMail { get; set; }
        #endregion

    }
}

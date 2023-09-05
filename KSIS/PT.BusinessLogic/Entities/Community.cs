using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Community
    {
        #region Magazine : 사보
        
        /// <summary>
        /// 등록번호
        /// </summary>
        public string RegNo { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 등록일자
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 저장구분
        /// </summary>
        public string SaveMode { get; set; }

        /// <summary>
        /// 사보사진Url
        /// </summary>
        public string MegazinePhotoUrl { get; set; }


        /// <summary>
        /// 사보정보Url
        /// </summary>
        public string MegazineSummaryUrl { get; set; }

        /// <summary>
        /// 사보Url
        /// </summary>
        public string MegazineUrl { get; set; }

        /// <summary>
        /// 사보다운Url
        /// </summary>
        public string MegazineDownUrl { get; set; }

        /// <summary>
        /// 사보다운Url
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 행순서
        /// </summary>
        public string RowNum { get; set; }

        /// <summary>
        /// 행그룹
        /// </summary>
        public string RowGroup { get; set; }

        /// <summary>
        /// 파일url
        /// </summary>
        public string SitePhotoUrl { get; set; }

        /// <summary>
        /// 사보pdf url
        /// </summary>
        public string MagazinePdfUrl { get; set; }

        /// <summary>
        /// 사보명
        /// </summary>
        public string MagazineNm { get; set; }

        /// <summary>
        /// 사보 jpg url
        /// </summary>
        public string MagazineJpgUrl { get; set; }

        /// <summary>
        /// 등록년월
        /// </summary>
        public string Date { get; set; }
        #endregion
    }
}

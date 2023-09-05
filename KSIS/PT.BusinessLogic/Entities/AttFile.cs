using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class AttFile
    {
        #region AttFile : 첨부파일
        /// <summary>
        /// 첨부파일Id
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 순번
        /// </summary>
        public string Seq { get; set; }

        /// <summary>
        /// 폴더 경로
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 원래 파일명
        /// </summary>
        public string AttFileNm { get; set; }

        /// <summary>
        /// 실제 저장된 파일명
        /// </summary>
        public string SaveFileNm { get; set; }

        /// <summary>
        /// 파일크기
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 다운로드 URL
        /// </summary>
        public string URL { get; set; }
        #endregion
    }
}

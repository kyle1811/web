using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class User
    {
        #region 공통 : 그리드 및 함수값 관련
        /// <summary>
        /// 반환값
        /// </summary>
        public string ReturnVal { get; set; }

        /// <summary>
        /// 체크박스
        /// </summary>
        public string ChkBox { get; set; }
        #endregion

        #region User : 사용자정보

        /// <summary>
        /// 사용자접속Page
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// 사용자ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 패스워드
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        public string UserNm { get; set; }

        /// <summary>
        /// 이메일
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 부서코드
        /// </summary>
        public string DeptCd { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string DeptNm { get; set; }

        /// <summary>
        /// 직책코드
        /// </summary>
        public string TitleCd { get; set; }

        /// <summary>
        /// 직책명
        /// </summary>
        public string TitleNm { get; set; }

        /// <summary>
        /// 직급코드
        /// </summary>
        public string DutyCd { get; set; }

        /// <summary>
        /// 직급명
        /// </summary>
        public string DutyNm { get; set; }

        /// <summary>
        /// 사번
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 회사전화
        /// </summary>
        public string CompTel { get; set; }

        /// <summary>
        /// 회사주소
        /// </summary>
        public string CompAddress { get; set; }

        /// <summary>
        /// 자택전화
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 휴대폰
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 자택주소
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 직원사진Url
        /// </summary>
        public string UserPhotoUrl { get; set; }

        /// <summary>
        /// 근태상태
        /// </summary>
        public string DiligenceStateNm { get; set; }

        /// <summary>
        /// 담당업무
        /// </summary>
        public string JobNm { get; set; }

        /// <summary>
        /// 서버
        /// </summary>
        public string Server { get; set; }
        
        /// <summary>
        /// 권한여부
        /// </summary>
        public string AuthYn { get; set; }

        /// <summary>
        /// 이미지데이터
        /// </summary>
        public byte[] userimgpic { get; set; }

        /// <summary>
        /// 그룹사 구분
        /// </summary>
        public string SEQ { get; set; }

        /// <summary>
        /// 공통 이미지데이터
        /// </summary>
        public string COIMG { get; set; }


        /// <summary>
        /// 소속회사명
        /// </summary>
        public string CorpName { get; set; }

        /// <summary>
        /// 최근준공현장 코드
        /// </summary>
        public string SiteCd { get; set; }

        /// <summary>
        /// 최근준공현장 구분코드
        /// </summary>
        public string BizPartCd { get; set; }

        /// <summary>
        /// Admin 유저 구분
        /// </summary>
        public string AdminConfirm { get; set; }

        /// <summary>
        /// browser 구분
        /// </summary>
        public string browser { get; set; }
        #endregion

        #region Vacation : 연차사용현황 정보

        /// <summary>
        /// 사용월
        /// </summary>
        public string VacationMon { get; set; }

        /// <summary>
        /// 사용횟수
        /// </summary>
        public string VacationCnt { get; set; }

        /// <summary>
        /// 사용구분코드
        /// </summary>
        public string DNICD { get; set; }

        /// <summary>
        /// 차트색상
        /// </summary>
        public string VacationColor { get; set; }

        /// <summary>
        /// 부서원 차트색상
        /// </summary>
        public string VacationDeptColor { get; set; }

        /// <summary>
        /// 부서원 사용률 차트색상
        /// </summary>
        public string VacationDeptPColor { get; set; }

        /// <summary>
        /// 부서원연차사용합계
        /// </summary>
        public string VacationDeptCnt { get; set; }

        /// <summary>
        /// 부서원 이름
        /// </summary>
        public string VacationUser { get; set; }

        /// <summary>
        /// 부서 연차 사용률
        /// </summary>
        public string VacationDeptCntP { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string VacationUserP { get; set; }

        /// <summary>
        /// 부서별 사용실적 확인
        /// </summary>
        public string VacationInfoP { get; set; }
        
        /// <summary>
        /// 부서 인원 총연차
        /// </summary>
        public string VacationDeptTotCnt { get; set; }

        /// <summary>
        /// 부서 인원 사용연차
        /// </summary>
        public string VacationRsltColor { get; set; }

        /// <summary>
        /// 총연차명
        /// </summary>
        public string DeptTotCnt { get; set; }

        /// <summary>
        /// 실적연차명
        /// </summary>
        public string RsltCnt { get; set; }

        /// <summary>
        /// 부서인원체크
        /// </summary>
        public string PeoPleCnt { get; set; }

        /// <summary>
        /// 디바이스
        /// </summary>
        public string Device { get; set; }

        #endregion

        #region GetOffWorkTime : 퇴근 정보

        #region GetOffPersonWorkTime : 개인 퇴근정보
        /// <summary>
        /// 월
        /// </summary>
        public string Mon { get; set; }

            /// <summary>
            /// 색상
            /// </summary>
            public string AVRTIMEP { get; set; }

            /// <summary>
            /// 사번
            /// </summary>
            public string ChartCntP { get; set; }

            /// <summary>
            /// 이름
            /// </summary>
            public string Color { get; set; }

            /// <summary>
            /// 높이조절
            /// </summary>
            public string ChartCntGubun1 { get; set; }
        #endregion

        #region GetOffDeptWorkTime : 부서별 퇴근정보
                /// <summary>
                /// 부서코드
                /// </summary>
                public string DEPT_ID { get; set; }

                /// <summary>
                /// 부서병
                /// </summary>
                public string DEPT_NAME { get; set; }

                /// <summary>
                /// 카운트
                /// </summary>
                public string CNT { get; set; }

                /// <summary>
                /// 평균시간
                /// </summary>
                public string AVRTIME { get; set; }

                /// <summary>
                /// 차트수치
                /// </summary>
                public string ChartCnt { get; set; }

                /// <summary>
                /// 부서별 차트색상
                /// </summary>
                public string DEPT_COLOR { get; set; }

                /// <summary>
                /// 높이조절
                /// </summary>
                public string ChartCntGubun2 { get; set; }
        #endregion

        #region GetOffPersonDeptWorkTime : 부서원 퇴근정보
            /// <summary>
            /// 사번
            /// </summary>
            public string EMP_NO { get; set; }

            /// <summary>
            /// 성명
            /// </summary>
            public string USER_NAME { get; set; }

            /// <summary>
            /// 사번
            /// </summary>
            public string AVRTIMEDP { get; set; }

            /// <summary>
            /// 이름
            /// </summary>
            public string ChartCntDP { get; set; }

            /// <summary>
            /// 높이조절
            /// </summary>
            public string ChartCntGubun3 { get; set; }
        #endregion

        #endregion

        #region PLAnalysis : 개인 퇴근정보
        /// <summary>
        /// 월
        /// </summary>
        public string TOTAMT { get; set; }

        ///// <summary>
        ///// 색상
        ///// </summary>
        public string NUM { get; set; }

        ///// <summary>
        ///// 사번
        ///// </summary>
        //public string AVRTIMEP { get; set; }

        ///// <summary>
        ///// 이름
        ///// </summary>
        //public string Color { get; set; }

        ///// <summary>
        ///// 높이조절
        ///// </summary>
        //public string ChartCntGubun1 { get; set; }
        #endregion

    }
}

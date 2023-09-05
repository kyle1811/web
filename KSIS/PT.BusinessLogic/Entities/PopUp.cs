using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class PopUp
    {

        #region User : 사용자정보
        /// <summary>
        /// 사용자ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        public string UserNm { get; set; }

        /// <summary>
        /// 직급
        /// </summary>
        public string DutyNm { get; set; }

        /// <summary>
        /// 부서명
        /// </summary>
        public string DeptNm { get; set; }

        /// <summary>
        /// 사내번호
        /// </summary>
        public string CompTel { get; set; }

        /// <summary>
        /// 휴대폰
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 코드
        /// </summary>
        public string CD { get; set; }

        /// <summary>
        /// 코드명
        /// </summary>
        public string CodeNm { get; set; }

        /// <summary>
        /// 부서코드
        /// </summary>
        public string DeptCd { get; set; }

        /// <summary>
        /// 테이블 헤더 구분코드
        /// </summary>
        public string GubunCd { get; set; }
        #endregion

        #region 협력업체현황 상세
        /// <summary>
        /// 거래처코드
        /// </summary>
        public string VendorCd { get; set; }

        /// <summary>
        /// 거래처유형
        /// </summary>
        public string VendorKindNm { get; set; }
        
        /// <summary>
        /// 사업자구분
        /// </summary>
        public string VendorTypeCd { get; set; }

        /// <summary>
        /// 회사규모
        /// </summary>
        public string ComScaleCD { get; set; }

        /// <summary>
        /// 회사명
        /// </summary>
        public string VendorNm { get; set; }

        /// <summary>
        /// 회사설립일
        /// </summary>
        public string EstablishDate { get; set; }

        /// <summary>
        /// 사업자등록번호
        /// </summary>
        public string BizNo { get; set; }

        /// <summary>
        /// 법인등록번호
        /// </summary>
        public string CorpNo { get; set; }

        /// <summary>
        /// 업태
        /// </summary>
        public string BizType { get; set; }

        /// <summary>
        /// 업종
        /// </summary>
        public string BizKind { get; set; }

        /// <summary>
        /// 대표자명
        /// </summary>
        public string RepNm { get; set; }

        /// <summary>
        /// 직원수
        /// </summary>
        public string StaffCnt { get; set; }

        /// <summary>
        /// 주소
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 대표전화번호
        /// </summary>
        public string TelNo { get; set; }

        /// <summary>
        /// 대표 팩스번호
        /// </summary>
        public string FaxNo { get; set; }

        /// <summary>
        /// 대표 홈페이지
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// 대표 이메일
        /// </summary>
        public string RepEMail { get; set; }

        /// <summary>
        /// 대표사업장 여부
        /// </summary>
        public string HeadOfficeYn { get; set; }

        /// <summary>
        /// 대표 사업장 등록번호
        /// </summary>
        public string HeadOfficeBizNo { get; set; }

        /// <summary>
        /// 입찰담당자 이름
        /// </summary>
        public string BidChargeNm { get; set; }

        /// <summary>
        /// 입찰담당자 부서
        /// </summary>
        public string BidChargeDeptNm { get; set; }

        /// <summary>
        /// 입찰담당자 직위
        /// </summary>
        public string BidChargeJobClass { get; set; }

        /// <summary>
        /// 입찰담당자 번호
        /// </summary>
        public string BidChargeTelNo { get; set; }

        /// <summary>
        /// 입찰담당자 핸드폰번호
        /// </summary>
        public string BidChargeMobileNo { get; set; }

        /// <summary>
        /// 입찰담당자 이메일
        /// </summary>
        public string BidChargeEmail { get; set; }

        /// <summary>
        /// 계약담당자 이름
        /// </summary>
        public string ContractChargeNm { get; set; }

        /// <summary>
        /// 계약담당자 부서
        /// </summary>
        public string ContractChargeDeptNm { get; set; }

        /// <summary>
        /// 계약담당자 직위
        /// </summary>
        public string ContractChargeJobClass { get; set; }

        /// <summary>
        /// 계약담당자 번호
        /// </summary>
        public string ContractChargeTelNo { get; set; }

        /// <summary>
        /// 계약담당자 핸드폰번호
        /// </summary>
        public string ContractChargeMobileNo { get; set; }

        /// <summary>
        /// 계약담당자 이메일
        /// </summary>
        public string ContractChargeEmail { get; set; }

        /// <summary>
        /// 기성담당자 이름
        /// </summary>
        public string EstChargeNm { get; set; }

        /// <summary>
        /// 기성담당자 부서
        /// </summary>
        public string EstChargeDeptNm { get; set; }

        /// <summary>
        /// 기성담당자 직위
        /// </summary>
        public string EstChargeJobClass { get; set; }

        /// <summary>
        /// 기성담당자 번호
        /// </summary>
        public string EstChargeTelNo { get; set; }

        /// <summary>
        /// 기성담당자 핸드폰 번호
        /// </summary>
        public string EstChargeMobileNo { get; set; }

        /// <summary>
        /// 기성담당자 이메일
        /// </summary>
        public string EstChargeEmail { get; set; }

        /// <summary>
        /// 사업자등록증 사본 경로
        /// </summary>
        public string BizRegAttId { get; set; }

        /// <summary>
        /// 기타첨부파일 경로
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 분류명
        /// </summary>
        public string CdNm { get; set; }

        /// <summary>
        /// 분류코드
        /// </summary>
        public string RegJobTypeCd { get; set; }

        /// <summary>
        /// 등록상태코드
        /// </summary>
        public string RegStatusCd { get; set; }

        /// <summary>
        /// 등록상태명
        /// </summary>
        public string RegStatusNm { get; set; }

        /// <summary>
        /// 등록타입코드
        /// </summary>
        public string RegTargetTypeCd { get; set; }

        /// <summary>
        /// 등록타입명
        /// </summary>
        public string RegTargetTypeNm { get; set; }

        /// <summary>
        /// 요청내용
        /// </summary>
        public string RequestNm { get; set; }
        #endregion

        #region 현장현황 사진등록

        /// <summary>
        /// 현장코드
        /// </summary>
        public string SiteCd { get; set; }
        /// <summary>
        /// 등록번호
        /// </summary>
        public string RegNo { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 등록일
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 저장형태 코드
        /// </summary>
        public string SaveMode { get; set; }
        #endregion

        #region 동적 테이블 헤드 및 변수

        /// <summary>
        /// 테이블 상단 row
        /// </summary>
        public string TableHead { get; set; }

        /// <summary>
        /// 분기
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// 직급명
        /// </summary>
        public string DutyPop { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        public string UserPop { get; set; }

        /// <summary>
        /// 화면명
        /// </summary>
        public string PageNm { get; set; }

        /// <summary>
        /// 일자
        /// </summary>
        public string DateTime { get; set; }

            #region Depth 0 로그인, 로그아웃, 자동로그인
            /// <summary>
            /// 
            /// </summary>
            public string Login { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string AutoLogin { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Logout { get; set; }
            #endregion

            #region Depth 1 홈
            /// <summary>
            /// 
            /// </summary>
            public string Home { get; set; }
            #endregion

            #region Depth 2 경영정보, 현장정보, 인사정보, 관리정보, 자료실, 사우마당
            /// <summary>
            /// 경영정보
            /// </summary>
            public string Manage { get; set; }

            /// <summary>
            /// 현장정보
            /// </summary>
            public string Site { get; set; }

            /// <summary>
            /// 인사정보
            /// </summary>
            public string Person { get; set; }

            /// <summary>
            /// 관리정보
            /// </summary>
            public string Control { get; set; }

            /// <summary>
            /// 자료실
            /// </summary>
            public string Resource { get; set; }

            /// <summary>
            /// 사우마당
            /// </summary>
            public string Information { get; set; }
            #endregion

            #region Depth 3 수주현황 外
            /// <summary>
            /// 수주현황
            /// </summary>
            public string OrderResult { get; set; }
        
            /// <summary>
            /// 분양현황
            /// </summary>
            public string SalesPromotions { get; set; }

            /// <summary>
            /// 신용등급현황
            /// </summary>
            public string CreditGrade { get; set; }

            /// <summary>
            /// 현장현황
            /// </summary>
            public string SiteState { get; set; }

            /// <summary>
            /// 현장날씨정보
            /// </summary>
            public string SiteWeather { get; set; }

            /// <summary>
            /// 지도로보기
            /// </summary>
            public string Map { get; set; }

            /// <summary>
            /// 준공현황
            /// </summary>
            public string CompletionSite { get; set; }

            /// <summary>
            /// 경력개발
            /// </summary>
            public string CDP { get; set; }

            /// <summary>
            /// 조직도
            /// </summary>
            public string OrganizeChart { get; set; }

            /// <summary>
            /// 연차사용현황
            /// </summary>
            public string VacationState { get; set; }

            /// <summary>
            /// 본사퇴근현황
            /// </summary>
            public string GetOffWorkTime { get; set; }

            /// <summary>
            /// 협력업체현황
            /// </summary>
            public string Cooperator { get; set; }

            /// <summary>
            /// 가용예산
            /// </summary>
            public string Budget { get; set; }

            /// <summary>
            /// 일일입금현황
            /// </summary>
            public string DailyDeposit { get; set; }

            /// <summary>
            /// 업무추진비신청현황
            /// </summary>
            public string BizCost { get; set; }

            /// <summary>
            /// 사용통계
            /// </summary>
            public string KSISUsageStatistics { get; set; }

            /// <summary>
            /// 회의자료
            /// </summary>
            public string Meeting { get; set; }

            /// <summary>
            /// 사보
            /// </summary>
            public string Magazine { get; set; }


            /// <summary>
            /// 경매
            /// </summary>
            public string Auction { get; set; }

            /// <summary>
            /// 손익분석
            /// </summary>
            public string PLAnalysis { get; set; }

            /// <summary>
            /// 하자관리현황
            /// </summary>
            public string AsManage { get; set; }
            #endregion

            #region Depth 4 수주현황 세부 - OrderResult
            /// <summary>
            /// 수주실적(전체)
            /// </summary>
            public string ME010000 { get; set; }

            /// <summary>
            /// 수주실적(전체)
            /// </summary>
            public string ME020000 { get; set; }

            /// <summary>
            /// 신규수주현황
            /// </summary>
            public string ME030000 { get; set; }

                    #region Depth 5 신규수주현황(상세)
                    /// <summary>
                    /// 신규수주현황(상세)
                    /// </summary>
                    public string ME030100 { get; set; }
                    #endregion

            #endregion

            #region Depth 4 분양현황 세부 - SalesPromotions
            /// <summary>
            /// 분양현황
            /// </summary>
            public string ME040000 { get; set; }
            #endregion

            #region Depth 4 신용등급현황 세부 - CreditGrade
            /// <summary>
            /// 분양현황
            /// </summary>
            public string ME050000 { get; set; }
            #endregion

            #region Depth 4 현장현황 세부 - SiteState
            /// <summary>
            /// 현장현황(전체)
            /// </summary>
            public string SE010000 { get; set; }

            /// <summary>
            /// 현장현황(토목)
            /// </summary>
            public string SE020000 { get; set; }

            /// <summary>
            /// 현장현황(건축)
            /// </summary>
            public string SE030000 { get; set; }

            /// <summary>
            /// 현장현황(플랜트)
            /// </summary>
            public string SE040000 { get; set; }

                    #region Depth 5 현장현황(상세)
                    /// <summary>
                    ///현장현황(상세)
                    /// </summary>
                    public string SE020100 { get; set; }
                    #endregion

                    #region Depth 6 현장현황(기타)
                    /// <summary>
                    /// 현장현황(사진등록)
                    /// </summary>
                    public string SE020101 { get; set; }

                    /// <summary>
                    /// 현장현황(전체사진보기)
                    /// </summary>
                    public string SE020102 { get; set; }

                    /// <summary>
                    /// 현장현황(사진확대)
                    /// </summary>
                    public string SE020103 { get; set; }
                    #endregion

            #endregion

            #region Depth 4 현장날씨 세부 - SiteWeather    
            /// <summary>
            /// 현장날씨정보(토목)
            /// </summary>
            public string SE050000 { get; set; }

            /// <summary>
            /// 현장날씨정보(건축)
            /// </summary>
            public string SE060000 { get; set; }

            /// <summary>
            /// 현장날씨정보(플랜트)
            /// </summary>
            public string SE070000 { get; set; }

                    #region Depth 5 현장날씨정보(상세)
                    /// <summary>
                    ///현장날씨정보(상세)
                    /// </summary>
                    public string SE050100 { get; set; }
                    #endregion

            #endregion

            #region Depth 4 지도로보기 세부 - Map
            /// <summary>
            /// 지도로보기
            /// </summary>
            public string SE080000 { get; set; }
            #endregion

            #region Depth 4 경력개발 세부 - CDP
            /// <summary>
            /// 경력개발(개인경력사항)
            /// </summary>
            public string PN010000 { get; set; }

            /// <summary>
            /// 경력개발(조건별경력조회)
            /// </summary>
            public string PN020000 { get; set; }

                    #region Depth 5 경력개발(상세)
                    /// <summary>
                    ///경력개발(상세)
                    /// </summary>
                    public string PN020100 { get; set; }
                    #endregion

            #endregion

            #region Depth 4 조직도 세부 - OrganizeChart
            /// <summary>
            /// 조직도
            /// </summary>
            public string PN030000 { get; set; }

                #region Depth 5 조직도(상세)
                /// <summary>
                ///조직도(상세)
                /// </summary>
                public string PN030100 { get; set; }
                #endregion  

            #endregion

            #region Depth 4 연차사용현황 세부 - VacationState
            /// <summary>
            /// 연차사용현황
            /// </summary>
            public string PN040000 { get; set; }

                #region Depth 5 연차사용현황(구분)
                /// <summary>
                ///연차사용현황(구분)
                /// </summary>
                public string PN040100 { get; set; }
                #endregion

            #endregion

            #region Depth 4 본사퇴근현황 세부 - GetOffWorkTime
            /// <summary>
            /// 본사퇴근현황
            /// </summary>
            public string PN050000 { get; set; }
            #endregion

            #region Depth 4 협력업체현황 세부 - Cooperator
            /// <summary>
            /// 협력업체현황
            /// </summary>
            public string PN060000 { get; set; }

                #region Depth 5 협력업체현황(상세)
                /// <summary>
                ///협력업체현황(상세)
                /// </summary>
                public string PN060100 { get; set; }
                #endregion

            #endregion

            #region Depth 4 가용예산 세부 - Budget
            /// <summary>
            /// 가용예산
            /// </summary>
            public string CL010000 { get; set; }
            #endregion

            #region Depth 4 일일입금현황 세부 - DailyDeposit
            /// <summary>
            /// 일일입금현황
            /// </summary>
            public string CL020000 { get; set; }
            #endregion

            #region Depth 4 업무추진비신청현황 세부 - BizCost
            /// <summary>
            /// 업무추진비신청현황
            /// </summary>
            public string CL030000 { get; set; }

                #region Depth 5 업무추진비신청현황(상세)
                /// <summary>
                ///업무추진비신청현황(상세)
                /// </summary>
                public string CL030100 { get; set; }
                #endregion

            #endregion

            #region Depth 4 사용통계 세부 - KSISUsageStatistics
            /// <summary>
            /// KSISUsageStatistics
            /// </summary>
            public string CL040000 { get; set; }
            #endregion

            #region Depth 4 회의자료 세부 - Meeting
            /// <summary>
            /// 회의자료
            /// </summary>
            public string RE010000 { get; set; }

                #region Depth 5 회의자료(상세)
                /// <summary>
                ///회의자료(상세)
                /// </summary>
                public string RE010100 { get; set; }
                #endregion

                #region Depth 5 회의자료(작성)
                /// <summary>
                ///회의자료(작성)
                /// </summary>
                public string RE010200 { get; set; }
                #endregion

            #endregion

            #region Depth 4 사보 세부 - Magazine
            /// <summary>
            /// 사보
            /// </summary>
            public string IN010000 { get; set; }

                #region Depth 5 사보등록
                /// <summary>
                ///사보등록
                /// </summary>
                public string IN010100 { get; set; }
                #endregion

            #endregion

            #region Depth 4 경매 세부 - Auction
            /// <summary>
            /// 경매(진행중인경매)
            /// </summary>
            public string IN020000 { get; set; }

            /// <summary>
            /// 경매(참여한경매)
            /// </summary>
            public string IN030000 { get; set; }

            /// <summary>
            /// 경매(경매관리자)
            /// </summary>
            public string IN040000 { get; set; }

                #region Depth 5 경매(상세페이지조회)
                /// <summary>
                ///경매(상세페이지조회)
                /// </summary>
                public string IN020100 { get; set; }
        #endregion

                #region Depth 5 경매(물품등록추가)
                /// <summary>
                ///경매(물품등록추가)
                /// </summary>
                public string IN030100 { get; set; }
                #endregion

        #endregion

            #region Depth 4 준공현황 세부 - CompletionSite
            /// <summary>
            /// 준공현황
            /// </summary>
            public string SE090000 { get; set; }
            #endregion

            #region Depth 4 손익분석 세부 - PLAnalysis
            /// <summary>
            /// 손익분석(외주)
            /// </summary>
            public string ME060100 { get; set; }

            /// <summary>
            /// 손익분석(자재)
            /// </summary>
            public string ME060200 { get; set; }

            /// <summary>
            /// 손익분석(기간)
            /// </summary>
            public string ME060300 { get; set; }

            /// <summary>
            /// 손익분석(협력업체)
            /// </summary>
            public string ME060400 { get; set; }
            #endregion

            #region Depth 4 하자관리현황 세부 - AsManage
            /// <summary>
            /// 하자관리현황
            /// </summary>
            public string SE100000 { get; set; }

                #region Depth 5 하자관리현황(현장별)
                /// <summary>
                ///하자관리현황(현장별)
                /// </summary>
                public string SE100100 { get; set; }
                #endregion

            #endregion

        #endregion

    }
}

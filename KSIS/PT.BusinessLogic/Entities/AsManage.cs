using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.BusinessLogic.Entities
{
    public class AsManage
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
        #endregion

        #region AsManage : 하자관리현황
        /// <summary>
        /// 방문_접수
        /// </summary>
        public string RCPTV { get; set; }

        /// <summary>
        /// 방문_완료
        /// </summary>
        public string FINISHV { get; set; }

        /// <summary>
        /// 전화_접수
        /// </summary>
        public string RCPTP { get; set; }

        /// <summary>
        /// 전화_완료
        /// </summary>
        public string FINISHP { get; set; }

        /// <summary>
        /// 공문_접수
        /// </summary>
        public string RCPTD { get; set; }

        /// <summary>
        /// 공문_완료
        /// </summary>
        public string FINISHD { get; set; }

        /// <summary>
        /// 기타_접수
        /// </summary>
        public string RCPTE { get; set; }

        /// <summary>
        /// 기타_완료
        /// </summary>
        public string FINISHE { get; set; }

        /// <summary>
        /// 계_접수
        /// </summary>
        public string RCPTSUM { get; set; }

        /// <summary>
        /// 계_완료
        /// </summary>
        public string FINISHSUM { get; set; }

        /// <summary>
        /// 계_처리율
        /// </summary>
        public string HNDRATE { get; set; }

        /// <summary>
        /// 행번호
        /// </summary>
        public string RN { get; set; }

        /// <summary>
        /// 준비
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// 준비_코드
        /// </summary>
        public string CDP { get; set; }

        /// <summary>
        /// 품질점검
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// 품질점검_코드
        /// </summary>
        public string CDA { get; set; }

        /// <summary>
        /// 입주자사전점검
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// 입주자사전점검_코드
        /// </summary>
        public string CDB { get; set; }

        /// <summary>
        /// 입주
        /// </summary>
        public string ZERO { get; set; }

        /// <summary>
        /// 입주_코드
        /// </summary>
        public string CDZERO { get; set; }

        /// <summary>
        /// 2년차이하
        /// </summary>
        public string TWO { get; set; }

        /// <summary>
        /// 2년차이하_코드
        /// </summary>
        public string CDTWO { get; set; }

        /// <summary>
        /// 3년차
        /// </summary>
        public string THREE { get; set; }

        /// <summary>
        /// 3년차_코드
        /// </summary>
        public string CDTHREE { get; set; }

        /// <summary>
        /// 4년차
        /// </summary>
        public string FOUR { get; set; }

        /// <summary>
        /// 4년차_코드
        /// </summary>
        public string CDFOUR { get; set; }

        /// <summary>
        /// 5년차이상
        /// </summary>
        public string FIVE { get; set; }

        /// <summary>
        /// 5년차이상_코드
        /// </summary>
        public string CDFIVE { get; set; }

        /// <summary>
        /// 계_처리율
        /// </summary>
        public string SEVEN { get; set; }        

        /// <summary>
        /// 소송
        /// </summary>
        public string SUIT { get; set; }

        /// <summary>
        /// 소송_코드
        /// </summary>
        public string CDSUIT { get; set; }

        /// <summary>
        /// 권역
        /// </summary>
        public string SRVCNTRNM { get; set; }

        /// <summary>
        /// 권역_전화번호
        /// </summary>
        public string SRVCNTRTELNO { get; set; }

        /// <summary>
        /// 권역_팩스번호
        /// </summary>
        public string SRVCNTRFAXNO { get; set; }

        /// <summary>
        /// 권역_사무실위치
        /// </summary>
        public string SRVCNTROFFICELOC { get; set; }

        /// <summary>
        /// 권역_색
        /// </summary>
        public string SCHDCOLOR { get; set; }

        /// <summary>
        /// 부서코드
        /// </summary>
        public string DEPTCD { get; set; }

        /// <summary>
        /// 부서이름
        /// </summary>
        public string DEPTNM { get; set; }

        /// <summary>
        /// 프로젝트코드
        /// </summary>
        public string PJTCD { get; set; }

        /// <summary>
        /// 프로젝트명
        /// </summary>
        public string PJTNM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CCTRCD { get; set; }

        /// <summary>
        /// 진행단계ID
        /// </summary>
        public string PROGSTEPID { get; set; }

        /// <summary>
        /// 진행상태
        /// </summary>
        public string PROGSTATUSID { get; set; }

        /// <summary>
        /// 진행단계코드
        /// </summary>
        public string PROGSTEPCD { get; set; }

        /// <summary>
        /// 진행단계
        /// </summary>
        public string PROGSTEPNM { get; set; }

        /// <summary>
        /// 입주예정일
        /// </summary>
        public string OCCUEXPTDATE { get; set; }

        /// <summary>
        /// 입주확정일
        /// </summary>
        public string FIXOCCUDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DFCLOSINGDATE { get; set; }

        /// <summary>
        /// 입주 D-60일
        /// </summary>
        public string D_60BEGINDATE { get; set; }

        /// <summary>
        /// 입주 D-40일
        /// </summary>
        public string D_40BEGINDATE { get; set; }

        /// <summary>
        /// 입주 D-30일
        /// </summary>
        public string D_30BEGINDATE { get; set; }

        /// <summary>
        /// 입주 D-15일
        /// </summary>
        public string D_15BEGINDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PRODID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BIZDIVID { get; set; }

        /// <summary>
        /// 권역
        /// </summary>
        public string BIZDIVCD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HOUSEHOLDHCOBJYN { get; set; }

        /// <summary>
        /// 준비_현장명
        /// </summary>
        public string NMP { get; set; }

        /// <summary>
        /// 품질점검_현장명
        /// </summary>
        public string NMA { get; set; }

        /// <summary>
        /// 입주자사전점검_현장명
        /// </summary>
        public string NMB { get; set; }

        /// <summary>
        /// 입주_현장명
        /// </summary>
        public string NMZERO { get; set; }

        /// <summary>
        /// 2년차이하_현장명
        /// </summary>
        public string NMTWO { get; set; }

        /// <summary>
        /// 3년차_현장명
        /// </summary>
        public string NMTHREE { get; set; }

        /// <summary>
        /// 4년차_현장명
        /// </summary>
        public string NMFOUR { get; set; }

        /// <summary>
        /// 5년차_현장명
        /// </summary>
        public string NMFIVE { get; set; }

        /// <summary>
        /// 소송_현장명
        /// </summary>
        public string NMSUIT { get; set; }

        /// <summary>
        /// 준비_하자건수
        /// </summary>
        public string CNTP { get; set; }

        /// <summary>
        /// 품질점검_하자건수
        /// </summary>
        public string CNTA { get; set; }

        /// <summary>
        /// 입주자사전점검_하자건수
        /// </summary>
        public string CNTB { get; set; }

        /// <summary>
        /// 입주_하자건수
        /// </summary>
        public string CNTZERO { get; set; }

        /// <summary>
        /// 2년차이하_하자건수
        /// </summary>
        public string CNTTWO { get; set; }

        /// <summary>
        /// 3년차_하자건수
        /// </summary>
        public string CNTTHREE { get; set; }

        /// <summary>
        /// 4년차_하자건수
        /// </summary>
        public string CNTFOUR { get; set; }

        /// <summary>
        /// 5년차_하자건수
        /// </summary>
        public string CNTFIVE { get; set; }

        /// <summary>
        /// 소송_하자건수
        /// </summary>
        public string CNTSUIT { get; set; }

        /// <summary>
        /// 권역 ID
        /// </summary>
        public string SRVCNTRID { get; set; }

        /// <summary>
        /// 구분_코드
        /// </summary>
        public string kpicd { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string kpinm { get; set; }

        /// <summary>
        /// 정렬기준
        /// </summary>
        public string ordno { get; set; }

        /// <summary>
        /// 1년차부터
        /// </summary>
        public string e01 { get; set; }

        /// <summary>
        /// 품질점검
        /// </summary>
        public string a60 { get; set; }

        /// <summary>
        /// 입주자사전점검
        /// </summary>
        public string c30 { get; set; }

        /// <summary>
        /// 입주
        /// </summary>
        public string e00 { get; set; }

        /// <summary>
        /// 1년차
        /// </summary>
        public string s01 { get; set; }

        /// <summary>
        /// 2년차
        /// </summary>
        public string s02 { get; set; }

        /// <summary>
        /// 3년차
        /// </summary>
        public string s03 { get; set; }

        /// <summary>
        /// 4년차
        /// </summary>
        public string s04 { get; set; }

        /// <summary>
        /// 5년차
        /// </summary>
        public string s05 { get; set; }

        /// <summary>
        /// 7년차
        /// </summary>
        public string s07 { get; set; }

        /// <summary>
        /// 10년차
        /// </summary>
        public string s10 { get; set; }

        #endregion


        #region AsManageDetail : 현장별 하자현황
        /// <summary>
        /// 세대수
        /// </summary>
        public string TOTHOUSENO { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string DIV { get; set; }

        /// <summary>
        /// 입주 D-60
        /// </summary>
        public string D60 { get; set; }

        /// <summary>
        /// 입주 D-30
        /// </summary>
        public string D30 { get; set; }

        /// <summary>
        /// 입주
        /// </summary>
        public string MOVEIN { get; set; }

        /// <summary>
        /// 입주 2년차이하
        /// </summary>
        public string MOVEIN2 { get; set; }

        /// <summary>
        /// 입주 3년차
        /// </summary>
        public string MOVEIN3 { get; set; }

        /// <summary>
        /// 입주 4년차
        /// </summary>
        public string MOVEIN4 { get; set; }

        /// <summary>
        /// 입주 5년차이상
        /// </summary>
        public string MOVEIN5 { get; set; }

        /// <summary>
        /// 공종
        /// </summary>
        public string DFJOBTYPENM { get; set; }

        /// <summary>
        /// 업체명
        /// </summary>
        public string TRADENM { get; set; }

        /// <summary>
        /// 업체코드
        /// </summary>
        public string TRADEID { get; set; }
     
        /// <summary>
        /// 지적건
        /// </summary>
        public string RCPTCNT { get; set; }

        /// <summary>
        /// 세대당 지적건
        /// </summary>
        public string HOUSEINDICCNT { get; set; }

        /// <summary>
        /// 처리건수
        /// </summary>
        public string HNDCNT { get; set; }

        /// <summary>
        /// 미처리건수
        /// </summary>
        public string YETHNDCNT { get; set; }

        /// <summary>
        /// 재하자건수
        /// </summary>
        public string RERCPTCNT { get; set; }       

        /// <summary>
        /// 재하자율
        /// </summary>
        public string RERCPTRATE { get; set; }

        /// <summary>
        /// 대공종
        /// </summary>
        public string UPDFJOBTYPENM { get; set; }

        /// <summary>
        /// 공종별비율
        /// </summary>
        public string STEPRATE { get; set; }

        /// <summary>
        /// HNDTRADEID
        /// </summary>
        public string HNDTRADEID { get; set; }

        /// <summary>
        /// 업체명_약어
        /// </summary>
        public string TRADESHORTNM { get; set; }

        /// <summary>
        /// 그래프 색
        /// </summary>
        public string COLOR { get; set; }

        /// <summary>
        /// ORD
        /// </summary>
        public string ORD { get; set; }

        /// <summary>
        /// 하자원인ID
        /// </summary>
        public string DFCAUSEID { get; set; }

        /// <summary>
        /// 하자원인
        /// </summary>
        public string DFCAUSENM { get; set; }

        /// <summary>
        /// 지적건
        /// </summary>
        public string INDICCNT { get; set; }

        /// <summary>
        /// 비율
        /// </summary>
        public string CAUSERATE { get; set; }

        /// <summary>
        /// 순위
        /// </summary>
        public string RNK { get; set; }

        /// <summary>
        /// 위치
        /// </summary>
        public string LOCNM { get; set; }

        /// <summary>
        /// 유형
        /// </summary>
        public string DFNM { get; set; }

        /// <summary>
        /// 유형
        /// </summary>
        public string CNT { get; set; }

        /// <summary>
        /// 현재단계여부
        /// </summary>
        public string SELETEDYN { get; set; }

        /// <summary>
        /// 대공종
        /// </summary>
        public string DFLJOBTYPENM { get; set; }

        /// <summary>
        /// 대공종개수
        /// </summary>
        public string DFLJOBTYPECNT { get; set; }

        /// <summary>
        /// 시공업체
        /// </summary>
        public string CONSTRADEID { get; set; }

        /// <summary>
        /// 하자발생
        /// </summary>
        public string RCPTCNTCOMMA { get; set; }


        #endregion 현장별 하자현황
    }
}

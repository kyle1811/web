using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Site
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
        /// 휴대폰 번호
        /// </summary>
        public string UserMobileNo { get; set; }

        /// <summary>
        /// 순번그룹
        /// </summary>
        public string RowGroup { get; set; }
        #endregion

        #region Site : 현장정보
        /// <summary>
        /// 현장코드
        /// </summary>
        public string SiteCd { get; set; }

        /// <summary>
        /// 현장명
        /// </summary>
        public string SiteNm { get; set; }

        /// <summary>
        /// 현장사진Url
        /// </summary>
        public string SitePhotoUrl { get; set; }

        /// <summary>
        /// 현장비율(실행율,공정율,원가율)
        /// </summary>
        public string SiteRate { get; set; }

        /// <summary>
        /// 그래프색상
        /// </summary>
        public string GraphColor { get; set; }

        /// <summary>
        /// 부문
        /// </summary>
        public string BizPartNm { get; set; }

        /// <summary>
        /// 공사유형
        /// </summary>
        public string ConstFormNm { get; set; }

        /// <summary>
        /// 실행율
        /// </summary>
        public string BudgetRate { get; set; }

        /// <summary>
        /// 공사기간
        /// </summary>
        public string ConstDay { get; set; }

        /// <summary>
        /// 재해건수
        /// </summary>
        public string DisasterCnt { get; set; }

        /// <summary>
        /// 재해유형
        /// </summary>
        public string AccidentTypeNm { get; set; }

        /// <summary>
        /// 재해정도
        /// </summary>
        public string AccidentBizNm { get; set; }

        /// <summary>
        /// 재해일자
        /// </summary>
        public string AccidentDate { get; set; }

        /// <summary>
        /// 주소
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        /// 전화
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 부문코드
        /// </summary>
        public string BizPartCd { get; set; }

        /// <summary>
        /// 해당현장수
        /// </summary>
        public string TargetSiteCnt { get; set; }

        /// <summary>
        /// 1차기한내
        /// </summary>
        public string FirstDueDateCnt { get; set; }

        /// <summary>
        /// 2차기한내
        /// </summary>
        public string SecondDueDateCnt { get; set; }

        /// <summary>
        /// 기한초과
        /// </summary>
        public string OutofDueDateCnt { get; set; }

        /// <summary>
        /// 미작성
        /// </summary>
        public string OutofCnt { get; set; }

        /// <summary>
        /// 작업계획일
        /// </summary>
        public string WORKINGDAY { get; set; }

        /// <summary>
        /// 공사종목1_1
        /// </summary>
        public string lbl1_1 { get; set; }

        /// <summary>
        /// 공사종목1_2
        /// </summary>
        public string lbl1_2 { get; set; }

        /// <summary>
        /// 공사종목1_3
        /// </summary>
        public string lbl1_3 { get; set; }

        /// <summary>
        /// 공사종목1_4
        /// </summary>
        public string lbl1_4 { get; set; }

        /// <summary>
        /// 공사종목1_5
        /// </summary>
        public string lbl1_5 { get; set; }

        /// <summary>
        /// 공사종목1_6
        /// </summary>
        public string lbl1_6 { get; set; }

        /// <summary>
        /// 공사종목2_1
        /// </summary>
        public string lbl2_1 { get; set; }

        /// <summary>
        /// 공사종목2_2
        /// </summary>
        public string lbl2_2 { get; set; }

        /// <summary>
        /// 공사종목2_3
        /// </summary>
        public string lbl2_3 { get; set; }

        /// <summary>
        /// 공사종목2_4
        /// </summary>
        public string lbl2_4 { get; set; }

        /// <summary>
        /// 공사종목2_5
        /// </summary>
        public string lbl2_5 { get; set; }

        /// <summary>
        /// 공사종목3_1
        /// </summary>
        public string lbl3_1 { get; set; }

        /// <summary>
        /// 공사종목3_2
        /// </summary>
        public string lbl3_2 { get; set; }

        /// <summary>
        /// 공사종목3_3
        /// </summary>
        public string lbl3_3 { get; set; }

        /// <summary>
        /// 공사종목4_1
        /// </summary>
        public string lbl4_1 { get; set; }

        /// <summary>
        /// 공사종목4_2
        /// </summary>
        public string lbl4_2 { get; set; }

        /// <summary>
        /// 공사종목4_3
        /// </summary>
        public string lbl4_3 { get; set; }

        /// <summary>
        /// 공사종목4_4
        /// </summary>
        public string lbl4_4 { get; set; }

        /// <summary>
        /// 공사종목5_1
        /// </summary>
        public string lbl5_1 { get; set; }

        /// <summary>
        /// 공사종목5_2
        /// </summary>
        public string lbl5_2 { get; set; }

        /// <summary>
        /// 공사기간(도급)
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// 공사규모
        /// </summary>
        public string ConstructionScale { get; set; }

        /// <summary>
        /// 대지면적
        /// </summary>
        public string Land { get; set; }

        /// <summary>
        /// 건축면적
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// 발주처
        /// </summary>
        public string VendorNm { get; set; }

        /// <summary>
        /// 지분율
        /// </summary>
        public string Share { get; set; }

        /// <summary>
        /// 현장소장
        /// </summary>
        public string JobClassNm { get; set; }

        /// <summary>
        /// 상세내용
        /// </summary>
        public string BlockType { get; set; }

        /// <summary>
        /// 순번
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 등록일자
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public string Amout { get; set; }

        /// <summary>
        /// 항목
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string FLAG { get; set; }

        /// <summary>
        /// 누계계획금액
        /// </summary>
        public string ContractAmt { get; set; }

        /// <summary>
        /// 누계실적금액
        /// </summary>
        public string TotSaleTotAmt { get; set; }

        /// <summary>
        /// 누계달성율
        /// </summary>
        public string AccSiteRate { get; set; }

        /// <summary>
        /// 연간계획금액
        /// </summary>
        public string YearlySalePlanAmt { get; set; }

        /// <summary>
        /// 연간실적금액
        /// </summary>
        public string RsltAmt { get; set; }

        /// <summary>
        /// 마감일자
        /// </summary>
        public string FinishDate { get; set; }

        #endregion

        #region SiteCoordinates : 현장좌표
        /// <summary>
        /// 위도
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 경도
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 당사지분금액
        /// </summary>
        public string KCCSupplyAmt { get; set; }

        /// <summary>
        /// 총 도급금액
        /// </summary>
        public string TotSupplyAmt { get; set; }
        #endregion

        #region 분양관련
        /*/// <summary>
          /// 구분
          /// </summary>
          public string Gubun { get; set; }*/

        /// <summary>
        /// 월
        /// </summary>
        public string Mon { get; set; }

        /// <summary>
        /// 분양, 미분양
        /// </summary>
        public string Sales { get; set; }

        /// <summary>
        /// 입주, 미입주
        /// </summary>
        public string Movein { get; set; }

        /// <summary>
        /// 분양, 미분양 색깔매칭
        /// </summary>
        public string SalesColor { get; set; }

        /// <summary>
        /// 입주, 미입주 색깔매칭
        /// </summary>
        public string MoveinColor { get; set; }

        /// <summary>
        /// 총 분양 세대수
        /// </summary>
        public string TOTCNT { get; set; }

        /// <summary>
        /// 분양세대수
        /// </summary>
        public string UNSALECNT { get; set; }

        /// <summary>
        /// 분양율
        /// </summary>
        public string UNSALERATE { get; set; }

        /// <summary>
        /// 총 입주 세대수
        /// </summary>
        public string TOTSUPCNT { get; set; }

        /// <summary>
        /// 입주세대수
        /// </summary>
        public string UNMOVEINCNT { get; set; }

        /// <summary>
        /// 입주율
        /// </summary>
        public string UNMOVERATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string slice { get; set; }

        /// <summary>
        /// 월별 분양율
        /// </summary>
        public string MonSaleCnt { get; set; }

        /// <summary>
        /// 월별 입주율
        /// </summary>
        public string MonMoveinCnt { get; set; }

        /// <summary>
        /// 분양율
        /// </summary>
        public string ViewSale { get; set; }

        /// <summary>
        /// 입주율
        /// </summary>
        public string ViewMove { get; set; }

        #endregion

        #region SaleMoveinDetail : 분양입주상세
        /// <summary>
        /// 구분자 확인
        /// </summary>
        public string Chk { get; set; }

        /// <summary>
        /// 현장코드
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 현장명
        /// </summary>
        public string ProjectNm { get; set; }

        /// <summary>
        /// 총 공급세대
        /// </summary>
        public string TotCnt { get; set; }

        /// <summary>
        /// 연간 분양수
        /// </summary>
        public string YCnt { get; set; }

        /// <summary>
        /// 누계 분양수
        /// </summary>
        public string SCnt { get; set; }

        /// <summary>
        /// 미분양수
        /// </summary>
        public string UsCnt { get; set; }

        /// <summary>
        /// 미분양율
        /// </summary>
        public string UsRate { get; set; }

        /// <summary>
        /// 연간입주수
        /// </summary>
        public string YMCnt { get; set; }

        /// <summary>
        /// 누계입주수
        /// </summary>
        public string MCnt { get; set; }

        /// <summary>
        /// 미입주수
        /// </summary>
        public string UMCnt { get; set; }

        /// <summary>
        /// 미입주율
        /// </summary>
        public string UMRate { get; set; }
        #endregion

        #region 신용등급현황 관련

        /// <summary>
        /// 구분명
        /// </summary>
        public string TypeNm1 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급
        /// </summary>
        public string Grade1 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급일자
        /// </summary>
        public string CDate1 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급구분
        /// </summary>
        public string CType1 { get; set; }

        /// <summary>
        /// 한국기업평가 등급
        /// </summary>
        public string Grade2 { get; set; }

        /// <summary>
        /// 한국기업평가 등급일자
        /// </summary>
        public string CDate2 { get; set; }

        /// <summary>
        /// 한국기업평가 등급구분
        /// </summary>
        public string CType2 { get; set; }

        /// <summary>
        /// 한국신용평가 등급
        /// </summary>
        public string Grade3 { get; set; }

        /// <summary>
        /// 한국신용평가 등급일자
        /// </summary>
        public string CDate3 { get; set; }

        /// <summary>
        /// 한국신용평가 등급구분
        /// </summary>
        public string CType3 { get; set; }

        /// <summary>
        /// 구분명
        /// </summary>
        public string TypeNm2 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급
        /// </summary>
        public string Grade4 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급일자
        /// </summary>
        public string CDate4 { get; set; }

        /// <summary>
        /// 나이스신용평가 등급구분
        /// </summary>
        public string CType4 { get; set; }

        /// <summary>
        /// 한국기업평가 등급
        /// </summary>
        public string Grade5 { get; set; }

        /// <summary>
        /// 한국기업평가 등급일자
        /// </summary>
        public string CDate5 { get; set; }

        /// <summary>
        /// 한국기업평가 등급구분
        /// </summary>
        public string CType5 { get; set; }

        /// <summary>
        /// 한국신용평가 등급
        /// </summary>
        public string Grade6 { get; set; }

        /// <summary>
        /// 한국신용평가 등급일자
        /// </summary>
        public string CDate6 { get; set; }

        /// <summary>
        /// 한국신용평가 등급구분
        /// </summary>
        public string CType6 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum1 { get; set; }
        
        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade1 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate1 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum2 { get; set; }

        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade2 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate2 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum3 { get; set; }

        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade3 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate3 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum4 { get; set; }

        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade4 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate4 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum5 { get; set; }

        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade5 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate5 { get; set; }

        /// <summary>
        /// 차트 등급 숫자
        /// </summary>
        public string ChartGradeNum6 { get; set; }

        /// <summary>
        /// 차트 등급 영문
        /// </summary>
        public string ChartGrade6 { get; set; }

        /// <summary>
        /// 차트 평정일
        /// </summary>
        public string ChartCDate6 { get; set; }

        /// <summary>
        /// 도급순위
        /// </summary>
        public string Ranking { get; set; }

        /// <summary>
        /// 도급순위레벨
        /// </summary>
        public string RankingSEQ { get; set; }

        /// <summary>
        /// 도급순위
        /// </summary>
        public string RankingType { get; set; }

        /// <summary>
        /// 회사명
        /// </summary>
        public string CorpName { get; set; }

        /// <summary>
        /// 회사채
        /// </summary>
        public string CreditGrade1 { get; set; }

        /// <summary>
        /// 기업어음
        /// </summary>
        public string CreditGrade2 { get; set; }

        /// <summary>
        /// 당사 매칭 컬러
        /// </summary>
        public string CorpColor { get; set; }

        /// <summary>
        /// 신용등급 일자
        /// </summary>
        public string Fiscalyear { get; set; }

        #region 도급순위현황 테이블

        /// <summary>
        /// 도급순위1
        /// </summary>
        public string CorpName1 { get; set; }

        /// <summary>
        /// 회사명1
        /// </summary>
        public string Gubun1 { get; set; }

        /// <summary>
        /// 회사채1
        /// </summary>
        public string Gubun2 { get; set; }

        /// <summary>
        /// 기업어음1
        /// </summary>
        public string Gubun3 { get; set; }

        /// <summary>
        /// 도급순위2
        /// </summary>
        public string CorpName2 { get; set; }
        /// <summary>
        /// 회사명2
        /// </summary>
        public string Gubun21 { get; set; }

        /// <summary>
        /// 회사채2
        /// </summary>
        public string Gubun22 { get; set; }

        /// <summary>
        /// 기업어음2
        /// </summary>
        public string Gubun23 { get; set; }

        /// <summary>
        /// 도급순위3
        /// </summary>
        public string CorpName3 { get; set; }

        /// <summary>
        /// 회사명3
        /// </summary>
        public string Gubun31 { get; set; }

        /// <summary>
        /// 회사채3
        /// </summary>
        public string Gubun32 { get; set; }

        /// <summary>
        /// 기업어음3
        /// </summary>
        public string Gubun33 { get; set; }

        /// <summary>
        /// 평정일
        /// </summary>
        public string StandardDate { get; set; }

        /// <summary>
        /// 공백
        /// </summary>
        public string BL { get; set; }

        /// <summary>
        /// 공백
        /// </summary>
        public string BL2 { get; set; }
        #endregion

        #endregion

        #region Weather : 현장날씨정보
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
        /// 서버
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 권한여부
        /// </summary>
        public string AuthYn { get; set; }

        /// <summary>
        /// 그룹웨어계정
        /// </summary>
        public string EPUserId { get; set; }
        #endregion

        #region SiteDetail : 현장상세정보

        /// <summary>
        /// 현장명
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 지역/지구
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 대지면적
        /// </summary>
        public string LandArea { get; set; }

        /// <summary>
        /// 건축면적
        /// </summary>
        public string ConstArea { get; set; }

        /// <summary>
        /// 건폐율
        /// </summary>
        public string BldgToLandRatio { get; set; }

        /// <summary>
        /// 연면적
        /// </summary>
        public string GrossArea { get; set; }

        /// <summary>
        /// 면세면적
        /// </summary>
        public string TaxFreeArea { get; set; }

        /// <summary>
        /// 용적율적용면적
        /// </summary>
        public string FlSpaceIndexDivertArea { get; set; }

        /// <summary>
        /// 용적율
        /// </summary>
        public string FlSpaceIndex { get; set; }

        /// <summary>
        /// 조경면적
        /// </summary>
        public string GardeningArea { get; set; }

        /// <summary>
        /// 조경율
        /// </summary>
        public string GardeningRate { get; set; }

        /// <summary>
        /// 공사규모
        /// </summary>
        public string BlockTy { get; set; }

        /// <summary>
        /// 주차대수
        /// </summary>
        public string Parkingcnt { get; set; }

        /// <summary>
        /// 기타사항
        /// </summary>
        public string Etc { get; set; }

        /// <summary>
        /// 구조
        /// </summary>
        public string Structure { get; set; }

        /// <summary>
        /// 외장
        /// </summary>
        public string Exterior { get; set; }

        /// <summary>
        /// 난방방식
        /// </summary>
        public string HeatMeth { get; set; }

        /// <summary>
        /// 프로젝트코드
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 프로젝트명
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 현장별 종목 구분
        /// </summary>
        public string SiteBizpartCd { get; set; }

        #endregion
    }
}

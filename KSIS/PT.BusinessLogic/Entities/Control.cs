using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class Control
    {
        /// <summary>
        /// 반환값
        /// </summary>
        public string ReturnVal { get; set; }

        #region 가용예산 : 그리드 및 함수값 관련
        /// <summary>
        /// 체크박스
        /// </summary>
        public string ChkBox { get; set; }

        /// <summary>
        /// 계정코드
        /// </summary>
        public string Acctcd { get; set; }

        /// <summary>
        /// 계정명칭
        /// </summary>
        public string AcctNm { get; set; }

        /// <summary>
        /// 당월예산
        /// </summary>
        public string MonAmt { get; set; }

        /// <summary>
        /// 당월실적
        /// </summary>
        public string MonRsltAmt { get; set; }

        /// <summary>
        /// 가용예산
        /// </summary>
        public string RemainMonAmt { get; set; }
        #endregion
        
        #region 일일입금현황 : 그리드 및 함수값 관련
        /// <summary>
        /// 일자
        /// </summary>
        public string TRNX_DATE { get; set; }

        /// <summary>
        /// 전표번호
        /// </summary>
        public string JOURNAL_NUM { get; set; }

        /// <summary>
        /// 입금액
        /// </summary>
        public string AMOUNT { get; set; }

        /// <summary>
        /// 송금은행
        /// </summary>
        public string IN_BRANCH { get; set; }

        /// <summary>
        /// 입금계좌번호
        /// </summary>
        public string BANK_ACCOUNT_NUM { get; set; }

        /// <summary>
        /// 거래지점
        /// </summary>
        public string BANK_BRANCH_NAME { get; set; }

      /// <summary>
        /// 거래내역
        /// </summary>
        public string TRX_DESC { get; set; }

        /// <summary>
        /// 미반제
        /// </summary>
        public string DEPT_CODE { get; set; }

      /// <summary>
        /// 반제부서코드
        /// </summary>
        public string DEPT_DESC { get; set; }

        /// <summary>
        /// 반제부서
        /// </summary>
        public string AMOUNT_UNAPPLIED { get; set; }
        #endregion

        #region 일일입금현황(실시간) : 그리드 및 함수값 관련

        /// <summary>
        /// 구분
        /// </summary>
        public string TRAN_TYPE { get; set; }

        /// <summary>
        /// 거래일자
        /// </summary>
        public string TRAN_DATE { get; set; }

        /// <summary>
        /// 출금금액
        /// </summary>
        public string OUT_AMOUNT { get; set; }

        /// <summary>
        /// 입금금액
        /// </summary>
        public string IN_AMOUNT { get; set; }

        /// <summary>
        /// 거래 후 잔액
        /// </summary>
        public string REMAIN_AMOUNT { get; set; }

        /// <summary>
        /// 적요
        /// </summary>
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// 취급은행
        /// </summary>
        public string CUST_BANK { get; set; }
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
        #endregion

        #region 업무추진비 신청승인 조회

        /// <summary>
        /// 요청번호
        /// </summary>
        public string CostSeq { get; set; }
        
        /// <summary>
        /// 사전/사후
        /// </summary>
        public string PreYn { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string Gubun { get; set; }

        /// <summary>
        /// 요청일자
        /// </summary>
        public string UseDate { get; set; }

        /// <summary>
        /// 신청자
        /// </summary>
        public string RegUser { get; set; }

        /// <summary>
        /// 신청상태
        /// </summary>
        public string ApproveStatus { get; set; }
        #endregion

        #region 업무추진비 상세 조회

        /// <summary>
        /// 승인자 사번
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// 승인자
        /// </summary>
        public string ApproverNm { get; set; }

        /// <summary>
        /// 상태코드
        /// </summary>
        public string ApproveStat { get; set; }

        /// <summary>
        /// 신청부서코드
        /// </summary>
        public string CostCenter { get; set; }

        /// <summary>
        /// 신청부서명
        /// </summary>
        public string CostCenterNm { get; set; }

        /// <summary>
        /// 등록일자
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 등록자ID
        /// </summary>
        public string RegUserId { get; set; }

        /// <summary>
        /// 등록자
        /// </summary>
        public string RegUserNm { get; set; }

        /// <summary>
        /// 순서
        /// </summary>
        public string Seq { get; set; }

        /// <summary>
        /// 대상/비대상
        /// </summary>
        public string TargetYn { get; set; }

        /// <summary>
        /// 아이템
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 인원
        /// </summary>
        public string Persons { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public string Amt { get; set; }

        /// <summary>
        /// 사유
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// ERP 반영여부
        /// </summary>
        public string ErpYn { get; set; }

        /// <summary>
        /// 승인/반려 선택
        /// </summary>
        public string UpdateMode { get; set; }
        #endregion

        #region 사용자 통계 조회

        /// <summary>
        /// 사용자ID
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        public string USERNM { get; set; }

        /// <summary>
        /// 사용자명(상세)
        /// </summary>
        public string UserNmDetail { get; set; }

        /// <summary>
        /// 직급명
        /// </summary>
        public string DUTYNM { get; set; }

        /// <summary>
        /// 직급명(상세)
        /// </summary>
        public string DutyNmDetail { get; set; }

        /// <summary>
        /// 접속횟수
        /// </summary>
        public string RegCnt { get; set; }

        /// <summary>
        /// 사용자별 접속일자
        /// </summary>
        public string RegDateUser { get; set; }

        /// <summary>
        /// 직급코드
        /// </summary>
        public string DUTYCD { get; set; }

        /// <summary>
        /// 화면명
        /// </summary>
        public string PAGE { get; set; }
        
        /// <summary>
        /// 화면사용횟수
        /// </summary>
        public string PageCnt { get; set; }

        /// <summary>
        /// 화면명
        /// </summary>
        public string DEPTNM { get; set; }

        /// <summary>
        /// 사용자ID
        /// </summary>
        public string EPUserId { get; set; }

        /// <summary>
        /// 구분
        /// </summary>
        public string ParentNm { get; set; }

        /// <summary>
        /// 화면코드
        /// </summary>
        public string ScreenCd { get; set; }

        /// <summary>
        /// 화면명
        /// </summary>
        public string ScreenNm { get; set; }

        /// <summary>
        /// 직급코드
        /// </summary>
        public string TitleCd { get; set; }

        /// <summary>
        /// 직급
        /// </summary>
        public string TitleNm { get; set; }

        /// <summary>
        /// 접속일자
        /// </summary>
        public string DateDetail { get; set; }

        /// <summary>
        /// 1번째 달
        /// </summary>
        public string FirstM { get; set; }

        /// <summary>
        /// 2번째 달
        /// </summary>
        public string SecondM { get; set; }

        /// <summary>
        /// 3번째 달
        /// </summary>
        public string ThirdM { get; set; }

        /// <summary>
        /// 분기
        /// </summary>
        public string Quater { get; set; }

        /// <summary>
        /// 1분기
        /// </summary>
        public string FirstQuarter { get; set; }

        /// <summary>
        /// 2분기
        /// </summary>
        public string SecondQuarter { get; set; }

        /// <summary>
        /// 3분기
        /// </summary>
        public string ThirdQuarter { get; set; }

        /// <summary>
        /// 4분기
        /// </summary>
        public string FourthQuarter { get; set; }

        /// <summary>
        /// 총 계
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// 화면코드
        /// </summary>
        public string TbScreenCd { get; set; }

        /// <summary>
        /// 화면명
        /// </summary>
        public string TbScreenNm { get; set; }

        /// <summary>
        /// 직급코드
        /// </summary>
        public string TbDutyCd { get; set; }

        /// <summary>
        /// 직급명
        /// </summary>
        public string TbDutyNm { get; set; }

        /// <summary>
        /// 원형 차트 화면명
        /// </summary>
        public string CircleScreenNm { get; set; }

        /// <summary>
        /// 원형 차트 비중
        /// </summary>
        public string CirclePer { get; set; }

        /// <summary>
        /// 바 월별 
        /// </summary>
        public string BarMM { get; set; }

        /// <summary>
        /// 바 분기별 
        /// </summary>
        public string BarTerm { get; set; }

        /// <summary>
        /// 바 차트 화면명
        /// </summary>
        public string BarScreenNm { get; set; }

        /// <summary>
        /// 바 차트 비중
        /// </summary>
        public string BarPer { get; set; }

        /// <summary>
        /// 원 차트 색
        /// </summary>
        public string CircleColor { get; set; }

        /// <summary>
        /// 바 차트 색
        /// </summary>
        public string BarColor { get; set; }

        /// <summary>
        /// 원형 차트 값
        /// </summary>
        public string CircleValue { get; set; }

        /// <summary>
        /// 구분코드(중)
        /// </summary>
        public string ChildSc { get; set; }

        /// <summary>
        /// 구분(중)
        /// </summary>
        public string ChildSn { get; set; }

        /// <summary>
        /// 구분코드(소)
        /// </summary>
        public string CChildSc { get; set; }

        /// <summary>
        /// 구분(소)
        /// </summary>
        public string CChildSn { get; set; }
        
        /// <summary>
        /// 구분(중)
        /// </summary>
        public string ChildScrNm { get; set; }

        /// <summary>
        /// 전체 메뉴 클릭횟수
        /// </summary>
        public string TotalComma { get; set; }

        /// <summary>
        /// 전체 메뉴 사용비중
        /// </summary>
        public string TotalPercent { get; set; }

        /// <summary>
        /// 색깔 매칭
        /// </summary>
        public string AllBarColor { get; set; }

        /// <summary>
        /// 접속 브라우저
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 접속 기기
        /// </summary>
        public string Device { get; set; }
        #endregion
    }
}

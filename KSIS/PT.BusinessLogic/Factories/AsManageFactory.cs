using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Mvc;

using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class AsManageFactory
    {

        #region ListAsServiceCenter : 권역 콤보박스 조회
        /// <summary>
        /// 권역 콤보박스 조회
        /// </summary>
        public List<SelectListItem> ListAsServiceCenter()
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                //new SqlParameter("@PJTNM", pPJTNM)
            };

            List<AsManage> data = DBAccess.ListProcData<AsManage>("PPT_AS_listServiceCenter", sqlParams, ConnStrNm.As);

            List<SelectListItem> returnData = new List<SelectListItem>();

            //조회해온 코드 데이터를 SelectListItem List에 옮겨 담고, Selected를 설정한다.
            foreach (AsManage c in data)
            {
                returnData.Add(new SelectListItem
                {
                    Value = c.SRVCNTRID,
                    Text = c.SRVCNTRNM,
                    Selected = c.SRVCNTRID.Equals("*")
                });
            }
             
            return returnData;
        }
        #endregion

        #region ListFisrtAsReceiptSummary : 접수유형별 하자 현황 조회(페이지 첫 호출 시)
        /// <summary>
        /// 접수유형별 하자 현황 조회(페이지 첫 호출 시)
        /// </summary>
        public AsManage ListFisrtAsReceiptSummary(string pSRVCNTR)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SRVCNTRID", pSRVCNTR)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsReceiptSummary", sqlParams, ConnStrNm.As);

            return returnData.FirstOrDefault();
        }
        #endregion

        #region ListAsReceiptSummary : 접수유형별 하자 현황 조회(권역 조회 버튼 클릭 시)
        /// <summary>
        /// 접수유형별 하자 현황 조회(권역 조회 버튼 클릭 시)
        /// </summary>
        public List<AsManage> ListAsReceiptSummary(string pSRVCNTR)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SRVCNTRID", pSRVCNTR)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsReceiptSummary", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsStepSummary : 단계별 하자 현황 조회
        /// <summary>
        /// 단계별 하자 현황 조회
        /// </summary>
        public List<AsManage> ListAsStepSummary(string pSRVCNTR)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SRVCNTRID", pSRVCNTR)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsStepSummary", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsSummary : 단계별 상세 하자 현황 조회
        /// <summary>
        /// 단계별 하자 현황 조회
        /// </summary>
        public List<AsManage> ListAsSummary(string pSRVCNTR, string pGUBUN)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@SRVCNTRID", pSRVCNTR),
                new SqlParameter("@GUBUN", pGUBUN)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsSummary", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsStepSummaryDetail : 현장별 단계별 하자 현황 조회
        /// <summary>
        /// 현장별 단계별 하자 현황 조회
        /// </summary>
        public List<AsManage> ListAsStepSummaryDetail(string pPJTCD)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsStepSummaryDetail", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsProject : 현장조회
        /// <summary>
        /// 현장정보 상세조회
        /// </summary>
        public List<AsManage> ListAsProject(string pPJTNM)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTNM", pPJTNM)
            };

            List<AsManage> returnData = DBAccess.ListProcData<AsManage>("PPT_AS_listProject", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsVendor : 현장별 업체 조회
        /// <summary>
        /// 현장별 업체 조회
        /// </summary>
        public List<AsManage> ListAsVendor(string pPJTCD, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@PROGSTEPCD",  pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsVendor", sqlParams, ConnStrNm.As);

            return returnData ;
        }
        #endregion

        #region ListAsVendorDetail : 현장별 업체 조회 상세
        /// <summary>
        /// 현장별 업체 조회
        /// </summary>
        public List<AsManage> ListAsVendorDetail(string pPJTCD, string pCONSTRADEID, string pDFJOBTYPENM, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@CONSTRADEID", pCONSTRADEID),
                new SqlParameter("@DFJOBTYPENM", pDFJOBTYPENM),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsVendor_Detail", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsReason : 현장별 하자 원인
        /// <summary>
        /// 현장별 업체 조회
        /// </summary>
        public List<AsManage> ListAsReason(string pPJTCD, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsReason", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsItem : 현장별 하자 공종
        /// <summary>
        /// 현장별 하자 공종
        /// </summary>
        public List<AsManage> ListAsItem(string pPJTCD, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsItem", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsItemDetail : 현장별 하자 공종 조회 상세
        /// <summary>
        /// 현장별 업체 조회
        /// </summary>
        public List<AsManage> ListAsItemDetail(string pPJTCD, string pUPDFNM, string pDFNM, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@UPDFNM", pUPDFNM),
                new SqlParameter("@DFNM", pDFNM),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsItem_Detail", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsDistribution : 현장별 하자발생 분포 조회
        /// <summary>
        /// 현장별 하자발생 분포 조회
        /// </summary>
        public List<AsManage> ListAsDistribution(string pPJTCD, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsDistribution", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsActionRate : 현장별 하자보수 조치율 조회
        /// <summary>
        /// 현장별 하자보수 조치율 조회
        /// </summary>
        public List<AsManage> ListAsActionRate(string pPJTCD, string pPROGSTEP)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD),
                new SqlParameter("@PROGSTEPCD", pPROGSTEP)
            };

            List<AsManage> returnData = DBAccess.ListProcDataAs<AsManage>("PPT_AS_listAsActionRate", sqlParams, ConnStrNm.As);

            return returnData;
        }
        #endregion

        #region ListAsStep : 진행단계 콤보박스 조회
        /// <summary>
        /// 진행단계 콤보박스 조회
        /// </summary>
        public List<SelectListItem> ListAsStep(string pPJTCD)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@PJTCD", pPJTCD)
            };

            List<AsManage> data = DBAccess.ListProcData<AsManage>("PPT_AS_listStep", sqlParams, ConnStrNm.As);

            List<SelectListItem> returnData = new List<SelectListItem>();

            //조회해온 코드 데이터를 SelectListItem List에 옮겨 담고, Selected를 설정한다.
            foreach (AsManage c in data)
            {
                returnData.Add(new SelectListItem
                {
                    Value = c.PROGSTEPCD,
                    Text = c.PROGSTEPNM,
                    Selected = c.PROGSTEPCD.Equals("")
                    //Selected = c.SELETEDYN.Equals("Y")       // 20190507 콤보박스 기본 값을 현재 단계로 수정
                });
            }

            return returnData;
        }
        #endregion

    }
}

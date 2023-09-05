using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

using PT.Service;
using PT.BusinessLogic.Entities;

namespace PT.BusinessLogic.Factories
{
    public class CDPFactory : BaseController
    {
        #region GetPSNL : 인사정보조회
        /// <summary>
        /// 인사정보조회
        /// </summary>
        public CDP GetPSNL(string pEmpNo, string pEmpNm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo),
                new SqlParameter("@EmpNm", pEmpNm)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_getPSNL", sqlParams, ConnStrNm.Main);

            return returnData.FirstOrDefault<CDP>();
        }
        #endregion

        
        #region ListCDPSearchGrid : 경력사항조회
        /// <summary>
        /// 경력사항 그리드 조회
        /// </summary>
        public List<CDP> ListCDPSearchGrid(string pEmpNo, string pEmpNm, string pVendorNm, string pBizPartCd, string pConstTypeCdS, string pConsultContractAmt, string pEmpDuty, string pEmpClass, string pSiteNm)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo),
                new SqlParameter("@EmpNm", pEmpNm),
                new SqlParameter("@VendorNm", pVendorNm),
                new SqlParameter("@BizPartCd", pBizPartCd),
                new SqlParameter("@ConstTypeCdS", pConstTypeCdS),
                new SqlParameter("@ConsultContractAmt", pConsultContractAmt),
                new SqlParameter("@EmpDuty", pEmpDuty),
                new SqlParameter("@EmpClass", pEmpClass),
                new SqlParameter("@SiteNm", pSiteNm),
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_listCDPSearchGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListAppointCareerGrid : 발령기준 경력사항조회
        /// <summary>
        /// 경력사항 그리드 조회
        /// </summary>
        public List<CDP> ListAppointCareerGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_listAppoint_CAR", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListCareerGrid : 공사 경력사항조회
        /// <summary>
        /// 경력사항 그리드 조회
        /// </summary>
        public List<CDP> ListCareerGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_listCON_CAR", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListLicenseGrid : 자격면허조회
        /// <summary>
        /// 자격면허 그리드 조회
        /// </summary>
        public List<CDP> ListLicenseGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_listLicenseGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region ListGradeGrid : 등급사항
        /// <summary>
        /// 등급사항 그리드 조회
        /// </summary>
        public List<CDP> ListGradeGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_listGradeGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region ListEduBgrGrid : 학력사항
        /// <summary>
        /// 학력사항 그리드 조회
        /// </summary>
        public List<CDP> ListEduBgrGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_ListEduBgrGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion


        #region ListBefCareerGrid : 입사전경력사항
        /// <summary>
        /// 입사전경력 그리드 조회
        /// </summary>
        public List<CDP> ListBefCareerGrid(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_ListBefCareerGrid", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region ListBefCareerGrid2 : 입사전경력사항
        /// <summary>
        /// 입사전경력 그리드 조회
        /// </summary>
        public List<CDP> ListBefCareerGrid2(string pEmpNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@EmpNo", pEmpNo)
            };

            List<CDP> returnData = DBAccess.ListProcData<CDP>("PPT_CDP_ListBefCareerGrid2", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

    }
}

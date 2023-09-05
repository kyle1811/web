using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT.BusinessLogic.Entities
{
    public class CDP
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


        #region PSNL : 개인인적사항
        /// <summary>
        /// 
        /// </summary>
        public string EMP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMP_NM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SEMP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SEMP_NM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CORP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RES_REG_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMP_CLASS { get; set; }
        
        /// </summary>
        public string EMP_DUTY { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public string SAL_CLASS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMP_RANK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string POSITION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ENTER_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RETIRE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RETIRE_STD_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CSVC_STD_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string STATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMP_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PHOTO_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthYn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FULL_DEPT_NM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PRMT_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EPUserId { get; set; }
        
        #endregion


        #region CONSTRUCTION_CAREER : 공사경력사항
        /// <summary>
        /// 
        /// </summary>
        public string ICOMPANY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ISUBPOST { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DURATION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IPOSTDUTY { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ICONSTDIV { get; set; }
                /// <summary>
        /// 
        /// </summary>
        public string IKIND { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IMONEYDIV { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IORDER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IRANK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IHEADDIV { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ILOCA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ICARMON { get; set; }
        #endregion


        #region LISENCE : 자격면허사항
        /// <summary>
        /// 
        /// </summary>
        public string LICENSE_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LICENSE_LV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LICENSE_DATE { get; set; }

        #endregion


        #region 등급사항
        /// <summary>
        /// 
        /// </summary>
        public string ENGGRADE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PROMDATE { get; set; }

        #endregion


        #region 학력사항
        /// <summary>
        /// 
        /// </summary>
        public string SCHOOL_NM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MAJOR { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GRADUATION_DATE { get; set; }
        
        #endregion

        #region 입사전 경력사항
        /// <summary>
        /// 
        /// </summary>
        //public string ICOMPANY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string IHEADDIV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IMAINPOST { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ISUBPOST { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string IPOSTGRP { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string IPOSTROW { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string IPOSTTYPE { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        //public string IPOSTDUTY { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        //public string DURATION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ICARMON { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ICONSTDIV { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        //public string IKIND { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public string ISANUM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string IMONEYDIV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ILOCA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string IORDER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string IRANK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IPOSTGRADE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ISTRUC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ISQUARE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IFLOOR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IHOUSE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ILENGTH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IBRIDGE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ITUNNEL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IOTHER { get; set; }

        #endregion

        
        #region 발령기준 경력사항
        /// <summary>
        /// 
        /// </summary>
        public string APPOINT_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string APPOINT_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string APPOINT_ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FULL_DEPT_NM_APPOINT { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string DEPT_APPOINT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConstTermStartDay { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ConstTermFinishDay { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ConsultContractAmt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConstTerm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string BizPartNm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ConstTypeNm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string VendorNm { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string WORK_MONTH { get; set; }


        
        #endregion

        											


    }
}

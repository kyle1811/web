using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Cryptography;

namespace PT.Service
{
    /// <summary>
    /// 유틸
    /// </summary>
    public class Util
    {
        #region MakeMessage : 메세지를 만든다.
        /// <summary>
        /// 메세지를 만든다.(ModelStateDictionary로 부터)
        /// </summary>
        /// <param name="pModelState">Controller의 ModelState</param>
        /// <returns>Message string</returns>
        public static string MakeMessage(ModelStateDictionary pModelState)
        {
            StringBuilder sb = new StringBuilder("");

            IEnumerable<string> errors = pModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList();

            foreach (string e in errors)
            {
                sb.Append(e + @"\r\n");
            }

            //끝에 줄바꿈 삭제
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 메세지를 만든다.(문자열 배열로 부터)
        /// </summary>
        /// <param name="pMessages">문자열 배열</param>
        /// <returns></returns>
        public static string MakeMessage(string[] pMessages)
        {
            StringBuilder sb = new StringBuilder("");

            foreach (string s in pMessages)
            {
                sb.Append(s + @"\r\n");
            }

            //끝에 줄바꿈 삭제
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 메세지를 만든다.(Exception 객체으로 부터)
        /// </summary>
        /// <param name="pException">Exception 객체</param>
        /// <returns></returns>
        public static string MakeMessage(Exception pException)
        {
            return pException.Message.Replace("\r\n", @"\r\n").Replace("\n", @"\n");
        }
        #endregion

        #region MakePagePath : 현재 페이지의 경로를 만든다.
        /// <summary>
        /// 현재 페이지의 경로를 만든다.
        /// </summary>
        /// <param name="pRouteData">RouteData</param>
        /// <returns>현재 페이지 경로 문자열 ex) /SC/Common/Home</returns>
        public static string MakePagePath(RouteData pRouteData)
        {
            string returnPagePath = "";

            // 컨트롤러와 액션으로 페이지 경로 조합
            returnPagePath += "/" + pRouteData.Values["controller"].ToString() + "/" + pRouteData.Values["action"].ToString();

            return returnPagePath;
        }
        #endregion

        #region MakeDateTime : 현재 DateTime을 만든다.
        /// <summary>
        /// 현재 DateTime을 만든다.
        /// </summary>
        /// <param name="pDateType">pDateType</param>
        /// <returns>현재 DateTime ex) 2016/05/13, 2016</returns>
        public static string MakeDateTime(string pDateType)
        {
            string returnDateTime = "";

            if (pDateType == "YearMonDay")
            {
                returnDateTime = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "/");
            }
            else if (pDateType == "YearMon")
            {
                returnDateTime = DateTime.Now.ToString("yyyyMM");
            }
            else if (pDateType == "Year") {
                returnDateTime = DateTime.Now.ToString("yyyy");
            }
            else if (pDateType == "Mon")
            {
                returnDateTime = DateTime.Now.ToString("MM");
            }
            else if (pDateType == "Day")
            {
                returnDateTime = DateTime.Now.ToString("dd");
            }

            return returnDateTime;
        }
        #endregion

        #region ListForDDL : 드롭다운 리스트를 위한 데이터
        /// <summary>
        /// 드롭다운 리스트를 위한 데이터
        /// </summary>
        /// <param name="pItems">드롭다운 리스트의 항목으로 넣을 문자열의 배열</param>
        /// <param name="pHasAll">전체옵션 여부</param>
        /// <param name="pSelectedValue">기본선택값</param>
        /// <returns></returns>
        public static List<SelectListItem> ListForDDL(string[,] pItems, bool pHasAll = false, string pSelectedValue = "")
        {
            //리턴할 SelectListItem List 선언
            List<SelectListItem> returnData = new List<SelectListItem>();
            
            for (int i = 0; i < pItems.GetLength(1); i++)
            {
                returnData.Add(new SelectListItem
                {
                    Value = pItems[0, i],
                    Text = pItems[1, i],
                    Selected = pItems[0, i].Equals(pSelectedValue)
                });
            }

            //전체 옵션이 선택된 경우 전체 데이터를 넣어준다.           
            if (pHasAll)
            {
                returnData.Insert(0, new SelectListItem() { Value = "", Text = "", Selected = pSelectedValue.Equals("") });
            }

            return returnData;
        }
        #endregion

        #region MakeSHA256HashCode : SHA256 단방향 암호화 데이터를 만든다.
        /// <summary>
        /// SHA256 단방향 암호화 데이터를 만든다.
        /// </summary>
        public static string MakeSHA256HashCode(string pPwd)
        {
            HashAlgorithm hash = (HashAlgorithm)SHA256.Create();

            if (!typeof(byte[]).IsAssignableFrom(Encoding.Default.GetBytes(pPwd).GetType()))
            {
                throw new ArgumentException("Byte Channge Error");
            }

            if (!typeof(HashAlgorithm).IsAssignableFrom(hash.GetType()))
            {
                throw new ArgumentException("HashAlgorithm Error");
            }

            try
            {
                return Convert.ToBase64String(hash.ComputeHash(Encoding.Default.GetBytes(pPwd)));
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}

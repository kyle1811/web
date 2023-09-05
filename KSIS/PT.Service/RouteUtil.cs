using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace PT.Service
{
    /// <summary>
    /// 컨트롤러 관련 유틸
    /// </summary>
    public class RouteUtil
    {
        #region Message : 메세지 출력
        /// <summary>
        /// 메세지 출력
        /// </summary>
        /// <param name="pMessage">출력할 메세지</param>
        /// <returns></returns>
        public static ActionResult Message(string pMessage)
        {
            StringBuilder sb = new StringBuilder("/Route/Message");

            sb.Append("?pMessage=" + HttpUtility.UrlEncode(pMessage));

            return new RedirectResult(sb.ToString());
        }
        #endregion

        #region MessageAndClose : 메세지 출력, 현재 페이지 닫기
        /// <summary>
        /// 메세지 출력, 현재 페이지 닫기
        /// </summary>
        /// <param name="pMessage">출력할 메세지</param>
        /// <returns></returns>
        public static ActionResult MessageAndClose(string pMessage)
        {
            StringBuilder sb = new StringBuilder("/Route/MessageAndClose");

            sb.Append("?pMessage=" + HttpUtility.UrlEncode(pMessage));

            return new RedirectResult(sb.ToString());
        }
        #endregion

        #region MessageAndMove : 메세지 출력, 페이지 이동
        /// <summary>
        /// 메세지 출력, 페이지 이동
        /// </summary>
        /// <param name="pMessage">출력할 메세지</param>
        /// <param name="pReturnPage">이동할 페이지</param>
        /// <returns></returns>
        public static ActionResult MessageAndMove(string pMessage, string pReturnPage)
        {
            StringBuilder sb = new StringBuilder("/Route/MessageAndMove");

            sb.Append("?pMessage=" + HttpUtility.UrlEncode(pMessage));
            sb.Append("&pReturnPage=" + HttpUtility.UrlEncode(pReturnPage));

            return new RedirectResult(sb.ToString());
        }
        #endregion

        #region MessageAndCloseAndReloadParent : 메세지 출력, 현재 페이지 닫기, 부모창 리로드
        /// <summary>
        /// 메세지 출력, 현재 페이지 닫기, 부모창 리로드
        /// </summary>
        /// <param name="pMessage">출력할 메세지</param>
        /// <returns></returns>
        public static ActionResult MessageAndCloseAndReloadParent(string pMessage)
        {
            StringBuilder sb = new StringBuilder("/Route/MessageAndCloseAndReloadParent");

            sb.Append("?pMessage=" + HttpUtility.UrlEncode(pMessage));

            return new RedirectResult(sb.ToString());
        }
        #endregion
    }
}

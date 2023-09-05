using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Runtime.CompilerServices;

namespace PT.Service
{
    /// <summary>
    /// 로그
    /// </summary>
    public class Log
    {
        #region Write : 로그 기록
        /// <summary>
        /// 로그 기록
        /// </summary>
        /// <param name="pLog">기록할 로그 문자열</param>
        /// <param name="pEmail">현재 사용자의 이메일</param>
        /// <param name="pRouteData">RouteData</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Write(string pLog, string pUserId = "", RouteData pRouteData = null)
        {
            // 현재 페이지 경로
            string pagePath = "";

            pagePath = Util.MakePagePath(pRouteData);

            // 로그파일 경로
            string path = HttpContext.Current.Server.MapPath("~/Log/") + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            // 기록할 로그 : [일시 이메일 페이지경로]
            string log = "[" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff") + " " + pUserId + " " + pagePath + "]\r\n" + pLog + "\r\n";

            // 파일에 기록
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.Write(log);
            }
        }
        #endregion
    }
}

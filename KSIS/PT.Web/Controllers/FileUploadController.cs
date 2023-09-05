using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;
using PT.Service;

namespace PT.Web.Controllers
{
    public class FileUploadController : Controller
    {
        WeatherFactory _weatherFactory = new WeatherFactory();
        AttFileFactory _attFileFactory = new AttFileFactory();

        #region WeatherNowFileUpload : 날씨정보 파일 업로드 (현재)
        [HttpPost]
        public ActionResult WeatherNowFileUpload(HttpPostedFileBase data)
        {
            string strErrorState = "Success";

            StreamReader fi = new StreamReader(data.InputStream);
            string str = fi.ReadToEnd();
            string[] strSpl = new string[] { "\r\n" };
            string[] arr = str.Split(strSpl, StringSplitOptions.None);

            List<Weather> weather = new List<Weather>();

            // 기존 데이터를 지우기 위한 파라미터
            string strDate = "";
            string strtime = "";

            for (int i = 0; i < arr.Length; i++)
            {
                string[] arrSub = arr[i].Split('/');

                if (arrSub[0] != "")
                {
                    weather.Add(new Weather()
                    {
                        SiteCd = arrSub[0],
                        WeatherDate = arrSub[1],
                        WeatherTime = arrSub[2],
                        WeatherCode = arrSub[3],
                        Temp = arrSub[4],
                        WindDirection = arrSub[5],
                        WindSpeed = arrSub[6],
                        TimeRainfall = arrSub[7],
                        DayRainfallAcct = arrSub[8],
                        Humidity = arrSub[9],
                        Discomfort = arrSub[10],
                        PM100Value = arrSub[11],
                        PM100State = arrSub[12],
                        PM25Value = arrSub[13],
                        PM25State = arrSub[14],
                        Snow = arrSub[15],
                        SnowAcct = arrSub[16]
                    });

                    if (i == 0)
                    {
                        strDate = arrSub[1];
                        strtime = arrSub[2];
                    }
                }
            }

            try
            {
                // 저장폴더
                string saveDirectory = System.Web.HttpContext.Current.Server.MapPath("~/WeatherFiles/Now/" + DateTime.Now.ToString("yyyyMM"));

                // 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                data.SaveAs(saveDirectory + @"\Now" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(data.FileName));

                // 날씨정보 생성(현재)
                _weatherFactory.CreateWeatherNow(weather, strDate, strtime);
            }
            catch (Exception e)
            {
                strErrorState = "Now Weather Error";
            }

            ViewBag.State = strErrorState;

            return View();
        }
        #endregion

        #region WeatherThreeFileUpload : 날씨정보 파일 업로드 (3일 3시간 예보)
        [HttpPost]
        public ActionResult WeatherThreeFileUpload(HttpPostedFileBase data)
        {
            string strErrorState = "Success";

            StreamReader fi = new StreamReader(data.InputStream);
            string str = fi.ReadToEnd();
            string[] strSpl = new string[] { "\r\n" };
            string[] arr = str.Split(strSpl, StringSplitOptions.None);

            List<Weather> weather = new List<Weather>();

            // 기존 데이터를 지우기 위한 파라미터
            string strDate = "";

            for (int i = 0; i < arr.Length; i++)
            {
                string[] arrSub = arr[i].Split('/');

                if (arrSub[0] != "")
                {
                    weather.Add(new Weather()
                    {
                        SiteCd = arrSub[0],
                        WeatherDate = arrSub[1],
                        WeatherTime = arrSub[2],
                        WeatherCode = arrSub[3],
                        Temp = arrSub[4],
                        Rainfall = arrSub[5],
                        MinTemp = arrSub[6],
                        MaxTemp = arrSub[7],
                        RainfallPro = arrSub[8],
                        PM100Value = arrSub[9],
                        PM100State = arrSub[10],
                        Snow = arrSub[11]
                    });

                    if (i == 0)
                    {
                        strDate = arrSub[1];
                    }
                }
            }

            try
            {
                // 저장폴더
                string saveDirectory = System.Web.HttpContext.Current.Server.MapPath("~/WeatherFiles/Three/" + DateTime.Now.ToString("yyyyMM"));

                // 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                data.SaveAs(saveDirectory + @"\Three" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(data.FileName));

                // 날씨정보 생성(3일 3시간 예보)
                _weatherFactory.CreateWeatherThree(weather, strDate);
            }
            catch (Exception e)
            {
                strErrorState = "Three Weather Error";
            }

            ViewBag.State = strErrorState;

            return View();
        }
        #endregion

        #region WeatherWeekFileUpload : 날씨정보 파일 업로드 (일주일 예보)
        [HttpPost]
        public ActionResult WeatherWeekFileUpload(HttpPostedFileBase data)
        {
            string strErrorState = "Success";

            StreamReader fi = new StreamReader(data.InputStream);
            string str = fi.ReadToEnd();
            string[] strSpl = new string[] { "\r\n" };
            string[] arr = str.Split(strSpl, StringSplitOptions.None);

            List<Weather> weather = new List<Weather>();

            //기존 데이터를 지우기 위한 파라미터
            string strDate = "";

            for (int i = 0; i < arr.Length; i++)
            {
                string[] arrSub = arr[i].Split('/');

                if (arrSub[0] != "")
                {
                    weather.Add(new Weather()
                    {
                        SiteCd = arrSub[0],
                        WeatherDate = arrSub[1],
                        WeatherCode = arrSub[2],
                        MinTemp = arrSub[3],
                        MaxTemp = arrSub[4],
                        RainfallPro = arrSub[5],
                        PM100Value = arrSub[6],
                        PM100State = arrSub[7],

                    });

                    if (i == 0)
                    {
                        strDate = arrSub[1];
                    }
                }
            }

            try
            {
                //저장폴더
                string saveDirectory = System.Web.HttpContext.Current.Server.MapPath("~/WeatherFiles/Week/" + DateTime.Now.ToString("yyyyMM"));

                //폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                data.SaveAs(saveDirectory + @"\Week" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(data.FileName));

                //날씨정보 생성(일주일 예보)
                _weatherFactory.CreateWeatherWeek(weather, strDate);
            }
            catch (Exception e)
            {
                strErrorState = "Week Weather Error";
            }

            ViewBag.State = strErrorState;

            return View();
        }
        #endregion

        #region SE2ImageUpload : 게시판 이미지 파일 업로드
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SE2ImageUpload(IEnumerable<HttpPostedFileBase> upload, string callback_func)
        {
            string ImagePath = String.Empty;
            string saveFileNm = String.Empty;

            try
            {
                if(upload != null && upload.Where(x => x != null).Sum(x => x.ContentLength) > 0)
                {
                    var FileName = Guid.NewGuid().ToString();
                    saveFileNm = _attFileFactory.SE2ImageUpload(upload, callback_func);
                    ImagePath = Url.Action("DownloadImage", "Widget", new { orgFileName = saveFileNm, fileId = FileName });
                }
                
            }
            catch
            {
                return RouteUtil.Message("");
            }

            
            return Redirect("/Plugins/se2/photo_uploader/popup/callback.html?callback_func=" + callback_func + "&bNewLine=true&sFileURL=" + Server.UrlEncode(ImagePath) + "&sFileName=" + saveFileNm);
        }
        #endregion

    }
}
        
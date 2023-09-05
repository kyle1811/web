using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Web.Html.Utils;

using PT.Service;
using PT.BusinessLogic.Entities;
using PT.BusinessLogic.Factories;

namespace PT.Web.Controllers
{
    public class FileDecryptionController : Controller
    {
        #region GetViewerReturn : 암호화 파일 복호화 및 뷰어 페이지 리턴
        [HttpGet]
        public ContentResult GetViewerReturn(string pSystem, string pLocation, string pEncFileNm, string pOrgFileNm)
        {
            // http://ksisdev.kccworld.net/FileDecryption/GetViewerReturn?pSystem=dev&pLocation=MarkAny_Store201802&pEncFileNm=APP201802010338199460&pOrgFileNm=DRM작업이력_fi.xlsx
            // http://localhost:1150/FileDecryption/GetViewerReturn?pSystem=dev&pLocation=MarkAny_Store201802&pEncFileNm=APP201802010338199460&pOrgFileNm=DRM작업이력_fi.xlsx

            pOrgFileNm = HttpUtility.UrlDecode(pOrgFileNm);

            // 1. 파일명 생성 : (모듈코드 4자리 + 현재일시14자리 + 랜덤4자리 = 총22자리) + 원래 파일명
            Random r = new Random(Guid.NewGuid().GetHashCode());
            string regNo = "Drm_" + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(1000, 9999).ToString();
            string fileNm = regNo + "_" + pOrgFileNm;

            // 2. 원본 파일 찾기
            string encFile = Server.MapPath("~/DrmFiles/") + pLocation + "/" + pEncFileNm;

            // 3. 원본 파일 복사 디렉토리
            string encCopyDirectory = Server.MapPath("~/DrmDec/Enc");
            string decCopyDirectory = Server.MapPath("~/DrmDec/Dec");

            if (Directory.Exists(encCopyDirectory) == false)
            {
                Directory.CreateDirectory(encCopyDirectory);
            }

            if (Directory.Exists(decCopyDirectory) == false)
            {
                Directory.CreateDirectory(decCopyDirectory);
            }

            System.IO.File.Copy(encFile, encCopyDirectory + @"\" + pEncFileNm, true);

            // 4. 파일 복호화
            MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();
            Obj.sSourceFilePath = encCopyDirectory + @"\" + pEncFileNm; // 원본 암호화 파일명
            Obj.sDestFilePath = decCopyDirectory + @"\" + fileNm; // 복사본 복호화 파일명
            int nRet = Obj.iDecrypt(); // 원본파일 및 복사본 복호화

            // 5. 뷰어 데이터 생성 및 호출 / 리턴
            string data = "";
            string viewerUrl = (pSystem == "real" ? "https://viewer.kccworld.net:8443/" : "https://viewerdev.ekpdev.kccworld.net:8443/");
            string decFileUrl = (pSystem == "real" ? "https://ksis.kccworld.net/DrmDec/Dec/" : "http://ksisdev.kccworld.net/DrmDec/Dec/");

            try
            {
                // 파일 확장자 구하기
                string fileExtension = Path.GetExtension(decCopyDirectory + @"\" + fileNm).ToLower();
                string convertType = "";

                // 파일 확장자별 변환모드 설정
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".bmp" || fileExtension == ".tif" || fileExtension == ".gif")
                {
                    convertType = "0";
                }
                else if (fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".ppt" || fileExtension == ".pptx" || fileExtension == ".pdf" || fileExtension == ".hwp" || fileExtension == ".txt")
                {
                    convertType = "1";
                }
                else
                {
                    return Content("NotConvertContent", "text/html");
                }

                // 매개변수
                string[] arrParam = new string[4];
                arrParam[0] = regNo;
                arrParam[1] = convertType;
                arrParam[2] = Uri.EscapeDataString(decFileUrl + fileNm);
                //arrParam[2] = Uri.EscapeDataString(decFileUrl + fileNm).Replace("%20", "%2520");
                arrParam[3] = "UTF-8";

                // 웹요청(POST방식) 호출
                var js = JObject.Parse(HtmlUtil.PostHttpGetWebRequest(viewerUrl + "SynapDocViewServer/jobJson", "fid=" + arrParam[0] + "&convertType=" + arrParam[1] + "&fileType=URL&filePath=" + arrParam[2] + "&urlEncoding=" + arrParam[3]));

                // 호출 데이터 생성
                data = viewerUrl + "SynapDocViewServer/view/" + js["key"].ToString();

                return Content(data, "text/html");
            }
            catch
            {
                return Content("Error", "text/html");
            }
        }
        #endregion

        [HttpGet]
        public ContentResult GetFileDownload()
        {
            try
            {
                MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();
                Obj.sSourceFilePath = "\\\\70.71.19.14\\DevUploadFile\\CO\\201803\\1.견적(입찰) 결과서201803260947325241.pdf";
                Obj.sDestFilePath = "L:\\" + "1.견적(입찰) 결과서201803260947325241.pdf";
                int nRet = Obj.iEncrypt();
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            return Content("Error", "text/html");
        }
    }
}

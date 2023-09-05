using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.Transactions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using PT.Service;
using PT.BusinessLogic.Entities;


namespace PT.BusinessLogic.Factories
{

    /// <summary>
    /// KCC ERP Module Code
    /// </summary>
    public enum ModuleCd { PT, MG, FS };

    public class AttFileFactory
    {
        #region ListAttFile : 첨부파일 조회
        /// <summary>
        /// 첨부파일 조회
        /// </summary>
        /// <param name="pAttId"></param>
        /// <returns></returns>
        public List<AttFile> ListAttFile(string pAttId)
        {
            SqlParameter[] sqlParams = new SqlParameter[] {
                new SqlParameter("@AttId", pAttId)
            };

            List<AttFile> returnData = DBAccess.ListProcData<AttFile>("PPT_AttFile_listAttFile", sqlParams, ConnStrNm.Main);

            return returnData;
        }
        #endregion

        #region 파일저장 : 신규
        /// <summary>
        /// 파일저장 : 신규
        /// </summary>
        public string SaveFiles(IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, RouteData pRouteData, string pUserId)
        {
            // 저장할 파일이 없으면 빈문자열을 리턴한다.
            if (pFiles == null || pFiles.Where(x => x != null).Count() == 0)
            {
                return "";
            }

            // 저장할 파일 크기의 합계가 10MB보다 큰 경우 예외를 발생시킨다.
            if (pFiles.Where(x => x != null).Sum(x => x.ContentLength) > 10485760)
            {
                throw new Exception("파일 크기의 합계가 10MB 보다 큰 경우 저장할 수 없습니다.");
            }

            // 현재 페이지의 경로로 menuCd를 생성한다.
            string menuCd = Util.MakePagePath(pRouteData);

            // 저장년월
            string saveYearMonth = DateTime.Now.ToString("yyyyMM");

            // 저장폴더
            string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;
            
            //이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");

            //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
            if (Directory.Exists(tempDirectory) == false)
            {
                Directory.CreateDirectory(tempDirectory);
            }
            
            // 랜덤변수 생성
            Random r = new Random(Guid.NewGuid().GetHashCode());

            // AttId 생성 : 모듈코드 2자리 + 현재일시14자리 + 랜덤4자리 = 총20자리
            string attId = pModule.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(1000, 9999).ToString();

            // 저장된 파일내역
            List<AttFile> savedAttFiles = new List<AttFile>();

            // 파일내역 순번
            int seq = 0;

            // 파일목록의 파일들을 서버에 저장한다.
            foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
            {

                // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                string attFileNm = Path.GetFileName(file.FileName);

                // 파일을 서버에 저장한다.              
                
                file.SaveAs(tempDirectory + @"\" + savefileNm);
                
                MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장
                int num = nRet;
                System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제     
                
                if(nRet== 52315)
                {
                    file.SaveAs(saveDirectory + @"\" + savefileNm);
                }
                 

                // 파일내역 순번을 증가시킨다.
                seq++;

                // 저장된 파일내역에 추가한다.
                savedAttFiles.Add(new AttFile
                {
                    AttId = attId,
                    Seq = seq.ToString(),
                    FilePath = pModule.ToString() + "/" + saveYearMonth,
                    AttFileNm = attFileNm,
                    SaveFileNm = savefileNm,
                    FileSize = file.ContentLength.ToString()
                });
            }

            // 로그 기록
            Log.Write(attId, pUserId, pRouteData);

            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                // 파일헤더 저장
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@AttId", attId),
                    new SqlParameter("@ModuleCd", pModule.ToString()),
                    new SqlParameter("@MenuCd", menuCd),
                    new SqlParameter("@RegUserId", pUserId)
                };

                DBAccess.ExcProc("PPT_AttFile_createAtt", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (AttFile f in savedAttFiles)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", attId),
                        new SqlParameter("@Seq", f.Seq),
                        new SqlParameter("@FilePath", f.FilePath),
                        new SqlParameter("@AttFileNm", f.AttFileNm),
                        new SqlParameter("@SaveFileNm", f.SaveFileNm),
                        new SqlParameter("@FileSize", f.FileSize),
                        new SqlParameter("@RegUserId", pUserId)
                    };

                    DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }

            return attId;
        }
        #endregion

        #region 파일저장 : 수정
        /// <summary>
        /// 파일저장 : 수정
        /// </summary>
        public void SaveFiles(string pAttId, string pSavedFileSeqs, IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, string pUserId)
        {
            // 저장할 파일 크기의 합계가 10MB보다 큰 경우 예외를 발생시킨다.
            if (pFiles != null && pFiles.Where(x => x != null).Sum(x => x.ContentLength) > 10485760)
            {
                throw new Exception("파일 크기의 합계가 10MB 보다 큰 경우 저장할 수 없습니다.");
            }

            int savedMaxSeq = 0;

            // 매개변수로 받은 pSavedFileSeqs을 savedFileSeqs배열에 넣는다.
            // pSavedFileSeqs는 화면에서 Request["p아이디Seqs"]를 통해 넘겨받은 기존 파일내역의 순번이다.
            // 기존 파일을 화면에서 삭제한 경우를 처리하기 위해, 화면에서 넘겨받은 내역을 savedFileSeqs 배열에 담아둔다.
            string[] savedFileSeqs = null;

            if (pSavedFileSeqs != null)
            {
                savedFileSeqs = pSavedFileSeqs.Split(',');
            }

            // 기존에 저장된 파일내역
            List<AttFile> savedAttFiles = ListAttFile(pAttId);

            // 삭제대상 파일들
            List<string> deleteTargetSeqs = new List<string>();

            // 기존에 저장된 파일 중 삭제해야하는 파일 체크     
            foreach (AttFile f in savedAttFiles)
            {
                if (savedFileSeqs == null || savedFileSeqs.Contains(f.Seq) == false)
                {
                    deleteTargetSeqs.Add(f.Seq);
                }
            }

            // 새로 저장할 파일이 없고, 삭제할 파일도 없으면 리턴
            if ((pFiles == null || pFiles.Where(x => x != null).Count() == 0) && deleteTargetSeqs.Count() == 0)
            {
                return;
            }

            // 새로 저장할 파일이 있으면 추가로 저장한다.            
            List<AttFile> addedAttFiles = new List<AttFile>();

            if (pFiles != null)
            {
                // 기존에 저장된 Seq의 최고값을 구한다.
                if (savedAttFiles.Count() > 0)
                {
                    savedMaxSeq = int.Parse(savedAttFiles.Max(x => x.Seq));
                }

                // 저장년월
                string saveYearMonth = DateTime.Now.ToString("yyyyMM");

                // 저장폴더
                string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;

                // 이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");

                //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
                if (Directory.Exists(tempDirectory) == false)
                {
                    Directory.CreateDirectory(tempDirectory);
                }

                // 랜덤변수 생성
                Random r = new Random(Guid.NewGuid().GetHashCode());

                // 파일목록의 파일들을 서버에 저장한다.
                foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
                {
                    // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                    string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                        + "_"
                                        + DateTime.Now.ToString("yyyyMMddHHmmss")
                                        + r.Next(1000, 9999).ToString()
                                        + Path.GetExtension(file.FileName);

                    // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                    string attFileNm = Path.GetFileName(file.FileName);

                    // 파일을 서버에 저장한다.
                   

                    
           
                file.SaveAs(tempDirectory + @"\" + savefileNm);
                
                MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장
                    
                System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제    

                    if (nRet == 52315)
                    {
                        file.SaveAs(saveDirectory + @"\" + savefileNm);
                    }



                    // 파일내역 순번을 증가시킨다.
                    savedMaxSeq++;

                    // 저장된 파일내역에 추가한다.
                    addedAttFiles.Add(new AttFile
                    {
                        AttId = pAttId,
                        Seq = savedMaxSeq.ToString(),
                        FilePath = pModule.ToString() + "/" + saveYearMonth,
                        AttFileNm = attFileNm,
                        SaveFileNm = savefileNm,
                        FileSize = file.ContentLength.ToString()
                    });
                }
            }

            //트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams;

                // 추가된 파일내역 저장
                if (pFiles != null)
                {
                    foreach (AttFile f in addedAttFiles)
                    {
                        sqlParams = new SqlParameter[] {
                            new SqlParameter("@AttId", pAttId),
                            new SqlParameter("@Seq", f.Seq),
                            new SqlParameter("@FilePath", f.FilePath),
                            new SqlParameter("@AttFileNm", f.AttFileNm),
                            new SqlParameter("@SaveFileNm", f.SaveFileNm),
                            new SqlParameter("@FileSize", f.FileSize),
                            new SqlParameter("@RegUserId", pUserId)
                        };

                        DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                    }
                }

                // 삭제된 파일 반영
                foreach (string seq in deleteTargetSeqs)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", pAttId),
                        new SqlParameter("@Seq", seq)
                    };

                    DBAccess.ExcProc("PPT_AttFile_deleteAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region 파일저장 : 신규 (경영정보부 전용)
        /// <summary>
        /// 파일저장 : 신규
        /// </summary>
        public string SaveFilesMid(IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, RouteData pRouteData, string pUserId)
        {
            // 저장할 파일이 없으면 빈문자열을 리턴한다.
            if (pFiles == null || pFiles.Where(x => x != null).Count() == 0)
            {
                return "";
            }

            // 저장할 파일 크기의 합계가 10MB보다 큰 경우 예외를 발생시킨다.
            if (pFiles.Where(x => x != null).Sum(x => x.ContentLength) > 104857600)
            {
                throw new Exception("파일 크기의 합계가 100MB 보다 큰 경우 저장할 수 없습니다.");
            }

            // 현재 페이지의 경로로 menuCd를 생성한다.
            string menuCd = Util.MakePagePath(pRouteData);

            // 저장년월
            string saveYearMonth = DateTime.Now.ToString("yyyyMM");

            // 저장폴더
            string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;

            //이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");

            //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
            if (Directory.Exists(tempDirectory) == false)
            {
                Directory.CreateDirectory(tempDirectory);
            }

            // 랜덤변수 생성
            Random r = new Random(Guid.NewGuid().GetHashCode());

            // AttId 생성 : 모듈코드 2자리 + 현재일시14자리 + 랜덤4자리 = 총20자리
            string attId = pModule.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(1000, 9999).ToString();

            // 저장된 파일내역
            List<AttFile> savedAttFiles = new List<AttFile>();

            // 파일내역 순번
            int seq = 0;

            // 파일목록의 파일들을 서버에 저장한다.
            foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
            {

                // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                string attFileNm = Path.GetFileName(file.FileName);

                // 파일을 서버에 저장한다.              

                file.SaveAs(tempDirectory + @"\" + savefileNm);

                MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장
                int num = nRet;
                System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제     

                if (nRet == 52315)
                {
                    file.SaveAs(saveDirectory + @"\" + savefileNm);
                }


                // 파일내역 순번을 증가시킨다.
                seq++;

                // 저장된 파일내역에 추가한다.
                savedAttFiles.Add(new AttFile
                {
                    AttId = attId,
                    Seq = seq.ToString(),
                    FilePath = pModule.ToString() + "/" + saveYearMonth,
                    AttFileNm = attFileNm,
                    SaveFileNm = savefileNm,
                    FileSize = file.ContentLength.ToString()
                });
            }

            // 로그 기록
            Log.Write(attId, pUserId, pRouteData);

            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                // 파일헤더 저장
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@AttId", attId),
                    new SqlParameter("@ModuleCd", pModule.ToString()),
                    new SqlParameter("@MenuCd", menuCd),
                    new SqlParameter("@RegUserId", pUserId)
                };

                DBAccess.ExcProc("PPT_AttFile_createAtt", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (AttFile f in savedAttFiles)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", attId),
                        new SqlParameter("@Seq", f.Seq),
                        new SqlParameter("@FilePath", f.FilePath),
                        new SqlParameter("@AttFileNm", f.AttFileNm),
                        new SqlParameter("@SaveFileNm", f.SaveFileNm),
                        new SqlParameter("@FileSize", f.FileSize),
                        new SqlParameter("@RegUserId", pUserId)
                    };

                    DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }

            return attId;
        }
        #endregion

        #region 파일저장 : 수정 (경영정보부 전용)
        /// <summary>
        /// 파일저장 : 수정
        /// </summary>
        public void SaveFilesMid(string pAttId, string pSavedFileSeqs, IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, string pUserId)
        {
            // 저장할 파일 크기의 합계가 10MB보다 큰 경우 예외를 발생시킨다.
            if (pFiles != null && pFiles.Where(x => x != null).Sum(x => x.ContentLength) > 104857600)
            {
                throw new Exception("파일 크기의 합계가 100MB 보다 큰 경우 저장할 수 없습니다.");
            }

            int savedMaxSeq = 0;

            // 매개변수로 받은 pSavedFileSeqs을 savedFileSeqs배열에 넣는다.
            // pSavedFileSeqs는 화면에서 Request["p아이디Seqs"]를 통해 넘겨받은 기존 파일내역의 순번이다.
            // 기존 파일을 화면에서 삭제한 경우를 처리하기 위해, 화면에서 넘겨받은 내역을 savedFileSeqs 배열에 담아둔다.
            string[] savedFileSeqs = null;

            if (pSavedFileSeqs != null)
            {
                savedFileSeqs = pSavedFileSeqs.Split(',');
            }

            // 기존에 저장된 파일내역
            List<AttFile> savedAttFiles = ListAttFile(pAttId);

            // 삭제대상 파일들
            List<string> deleteTargetSeqs = new List<string>();

            // 기존에 저장된 파일 중 삭제해야하는 파일 체크     
            foreach (AttFile f in savedAttFiles)
            {
                if (savedFileSeqs == null || savedFileSeqs.Contains(f.Seq) == false)
                {
                    deleteTargetSeqs.Add(f.Seq);
                }
            }

            // 새로 저장할 파일이 없고, 삭제할 파일도 없으면 리턴
            if ((pFiles == null || pFiles.Where(x => x != null).Count() == 0) && deleteTargetSeqs.Count() == 0)
            {
                return;
            }

            // 새로 저장할 파일이 있으면 추가로 저장한다.            
            List<AttFile> addedAttFiles = new List<AttFile>();

            if (pFiles != null)
            {
                // 기존에 저장된 Seq의 최고값을 구한다.
                if (savedAttFiles.Count() > 0)
                {
                    savedMaxSeq = int.Parse(savedAttFiles.Max(x => x.Seq));
                }

                // 저장년월
                string saveYearMonth = DateTime.Now.ToString("yyyyMM");

                // 저장폴더
                string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;

                // 이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");

                //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
                if (Directory.Exists(tempDirectory) == false)
                {
                    Directory.CreateDirectory(tempDirectory);
                }

                // 랜덤변수 생성
                Random r = new Random(Guid.NewGuid().GetHashCode());

                // 파일목록의 파일들을 서버에 저장한다.
                foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
                {
                    // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                    string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                        + "_"
                                        + DateTime.Now.ToString("yyyyMMddHHmmss")
                                        + r.Next(1000, 9999).ToString()
                                        + Path.GetExtension(file.FileName);

                    // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                    string attFileNm = Path.GetFileName(file.FileName);

                    // 파일을 서버에 저장한다.




                    file.SaveAs(tempDirectory + @"\" + savefileNm);

                    MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                    Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                    Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                    int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장

                    System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제    

                    if (nRet == 52315)
                    {
                        file.SaveAs(saveDirectory + @"\" + savefileNm);
                    }



                    // 파일내역 순번을 증가시킨다.
                    savedMaxSeq++;

                    // 저장된 파일내역에 추가한다.
                    addedAttFiles.Add(new AttFile
                    {
                        AttId = pAttId,
                        Seq = savedMaxSeq.ToString(),
                        FilePath = pModule.ToString() + "/" + saveYearMonth,
                        AttFileNm = attFileNm,
                        SaveFileNm = savefileNm,
                        FileSize = file.ContentLength.ToString()
                    });
                }
            }

            //트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams;

                // 추가된 파일내역 저장
                if (pFiles != null)
                {
                    foreach (AttFile f in addedAttFiles)
                    {
                        sqlParams = new SqlParameter[] {
                            new SqlParameter("@AttId", pAttId),
                            new SqlParameter("@Seq", f.Seq),
                            new SqlParameter("@FilePath", f.FilePath),
                            new SqlParameter("@AttFileNm", f.AttFileNm),
                            new SqlParameter("@SaveFileNm", f.SaveFileNm),
                            new SqlParameter("@FileSize", f.FileSize),
                            new SqlParameter("@RegUserId", pUserId)
                        };

                        DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                    }
                }

                // 삭제된 파일 반영
                foreach (string seq in deleteTargetSeqs)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", pAttId),
                        new SqlParameter("@Seq", seq)
                    };

                    DBAccess.ExcProc("PPT_AttFile_deleteAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region 파일저장 : 신규, 등록년월지정 사보전용 savefiles
        /// <summary>
        /// 사보저장 : 신규
        /// </summary>
        public string SaveMagazine(IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, RouteData pRouteData, string pUserId, string pYearMon)
        {

            // 저장할 파일이 없으면 빈문자열을 리턴한다.
            if (pFiles == null || pFiles.Where(x => x != null).Count() == 0)
            {
                return "";
            }

            // 현재 페이지의 경로로 menuCd를 생성한다.
            string menuCd = Util.MakePagePath(pRouteData);

            // 저장년월
            string saveYearMonth = pYearMon.ToString().Substring(0,6);

            // 저장폴더
            string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;
                       
            //이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            /* DRM 처리*/ 
            string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");
            //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
            if (Directory.Exists(tempDirectory) == false)
            {
                Directory.CreateDirectory(tempDirectory);
            }
            /* DRM 처리 */

            // 랜덤변수 생성
            Random r = new Random(Guid.NewGuid().GetHashCode());

            // AttId 생성 : 모듈코드 2자리 + 현재일시14자리 + 랜덤4자리 = 총20자리
            string attId = pModule.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(1000, 9999).ToString();

            // 저장된 파일내역
            List<AttFile> savedAttFiles = new List<AttFile>();

            // 파일내역 순번
            int seq = 0;

            // 파일목록의 파일들을 서버에 저장한다.
            foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
            {

                // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                string attFileNm = Path.GetFileName(file.FileName);

                // 파일을 서버에 저장한다.
                

                file.SaveAs(tempDirectory + @"\" + savefileNm); //암호화 파일 저장이 선행된다
              
                MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX(); // 마크애니 ASP.NET 전용 DLL 파일 참조 선언
                
                Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm; // 암호화 파일이 저장된 경로 지정
                Obj.sDestFilePath = saveDirectory + @"\" + savefileNm; // 복호화 파일이 저장될 경로 지정

                int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장
               
                System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제

                if (nRet == 52315)
                {
                    file.SaveAs(saveDirectory + @"\" + savefileNm);
                }

                // 파일내역 순번을 증가시킨다.
                seq++;

                // 저장된 파일내역에 추가한다.d
                savedAttFiles.Add(new AttFile
                {
                    AttId = attId,
                    Seq = seq.ToString(),
                    FilePath = pModule.ToString() + "/" + saveYearMonth,
                    AttFileNm = attFileNm,
                    SaveFileNm = savefileNm,
                    FileSize = file.ContentLength.ToString()
                });
            }

            // 로그 기록
            Log.Write(attId, pUserId, pRouteData);

            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                // 파일헤더 저장
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@AttId", attId),
                    new SqlParameter("@ModuleCd", pModule.ToString()),
                    new SqlParameter("@MenuCd", menuCd),
                    new SqlParameter("@RegUserId", pUserId)
                };

                DBAccess.ExcProc("PPT_AttFile_createAtt", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (AttFile f in savedAttFiles)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", attId),
                        new SqlParameter("@Seq", f.Seq),
                        new SqlParameter("@FilePath", f.FilePath),
                        new SqlParameter("@AttFileNm", f.AttFileNm),
                        new SqlParameter("@SaveFileNm", f.SaveFileNm),
                        new SqlParameter("@FileSize", f.FileSize),
                        new SqlParameter("@RegUserId", pUserId)
                    };

                    DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }

            return attId;
        }
        #endregion

        #region 파일저장 : 신규, 현장현황 사진등록 이미지 용량 리사이징
        /// <summary>
        /// 이미지 업로드 시 용량 줄이기
        /// </summary>
        public string SaveResizePhoto(IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, RouteData pRouteData, string pUserId)
        {
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".jfif", ".jpeg 2000", ".exif", ".tiff", ".tif", ".gif", ".bmp", ".rle", ".dib", ".png", ".ppm", ".pgm", ".pbm", ".pnm"
            , ".heif", ".bpg", ".jfif", ".raw"};                       

            // 저장할 파일이 없으면 빈문자열을 리턴한다.
            if (pFiles == null || pFiles.Where(x => x != null).Count() == 0)
            {
                return "";
            }

            // 현재 페이지의 경로로 menuCd를 생성한다.
            string menuCd = Util.MakePagePath(pRouteData);
            var reImageSize = "";
            // 저장년월
            string saveYearMonth = DateTime.Now.ToString("yyyyMM");

            // 저장폴더
            string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;

            //이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
            if (Directory.Exists(saveDirectory) == false)
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");
            //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
            if (Directory.Exists(tempDirectory) == false)
            {
                Directory.CreateDirectory(tempDirectory);
            }
            
                // 랜덤변수 생성
                Random r = new Random(Guid.NewGuid().GetHashCode());

            // AttId 생성 : 모듈코드 2자리 + 현재일시14자리 + 랜덤4자리 = 총20자리
            string attId = pModule.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(1000, 9999).ToString();

            // 저장된 파일내역
            List<AttFile> savedAttFiles = new List<AttFile>();

            // 파일내역 순번
            int seq = 0;

            // 파일목록의 파일들을 서버에 저장한다.
            foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
            {
                
                    //저장할 파일의 확장자 체크
                    if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        throw new Exception("이미지 형식이 아니면 사진을 업로드 할 수 없습니다.");
                    }
                    // 저장할 파일 크기가 10MB보다 큰 경우 예외를 발생시킨다.
                    if (file.ContentLength > 52428800)
                    {
                        throw new Exception("파일 크기가 50MB 보다 큰 경우 저장할 수 없습니다.");
                    }
                

                // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                string resavefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                string attFileNm = Path.GetFileName(file.FileName);
                
                // 저장할 파일 크기가 1MB보다 큰 경우 리사이징
                if (file.ContentLength > 1048576)
                {
                    file.SaveAs(tempDirectory + @"\" + savefileNm);

                    MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                    Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                    Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                    int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장

                    System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제    

                    if (nRet == 52315)
                    {
                        file.SaveAs(saveDirectory + @"\" + savefileNm);
                    }

                    FileStream fi = new FileStream(saveDirectory + @"\" + savefileNm, FileMode.Open);

                    Bitmap img = new Bitmap(fi);

                    //Bitmap img = new Bitmap(file.InputStream);

                    int imgWidth = img.Width;
                    int imgHeight = img.Height;

                    int maxImgWidth = 900, maxImgHeight = 900;

                    int resizeImgWidth = 0, resizeImgHeight = 0;

                    if (imgWidth > maxImgWidth || imgHeight > maxImgHeight)
                    {
                        if (imgWidth > imgHeight)
                        {
                            resizeImgWidth = maxImgWidth;
                            resizeImgHeight = (imgHeight * resizeImgWidth) / imgWidth;
                        }
                        else
                        {
                            resizeImgHeight = maxImgHeight;
                            resizeImgWidth = (imgWidth * resizeImgHeight) / imgHeight;
                        }
                    }
                    else
                    {
                        resizeImgWidth = imgWidth;
                        resizeImgHeight = imgHeight;
                    }

                    Bitmap resizeImg = new Bitmap(resizeImgWidth, resizeImgHeight);

                    Graphics chgImg = Graphics.FromImage(resizeImg);
                    chgImg.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    chgImg.DrawImage(img, new Rectangle(0, 0, resizeImgWidth, resizeImgHeight));

                    resizeImg.Save(saveDirectory + @"\" + resavefileNm);

                    var info = new FileInfo(saveDirectory + @"\" + resavefileNm);

                    reImageSize = info.Length.ToString();

                    fi.Close();

                    System.IO.File.Delete(saveDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제

                    savefileNm = resavefileNm;

                }
                else
                {
                              
                    file.SaveAs(tempDirectory + @"\" + savefileNm);

                    MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                    Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                    Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                    int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장
                                     
                    System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제

                    if (nRet == 52315)
                    {
                        file.SaveAs(saveDirectory + @"\" + savefileNm);
                    }

                    reImageSize = file.ContentLength.ToString();

                }
                // 파일내역 순번을 증가시킨다.
                seq++;

                // 저장된 파일내역에 추가한다.
                savedAttFiles.Add(new AttFile
                {
                    AttId = attId,
                    Seq = seq.ToString(),
                    FilePath = pModule.ToString() + "/" + saveYearMonth,
                    AttFileNm = attFileNm,
                    SaveFileNm = savefileNm,
                    FileSize = reImageSize
                });
            }

            // 로그 기록
            Log.Write(attId, pUserId, pRouteData);

            // 트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                // 파일헤더 저장
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@AttId", attId),
                    new SqlParameter("@ModuleCd", pModule.ToString()),
                    new SqlParameter("@MenuCd", menuCd),
                    new SqlParameter("@RegUserId", pUserId)
                };

                DBAccess.ExcProc("PPT_AttFile_createAtt", sqlParams, ConnStrNm.Main);

                // 파일내역 저장
                foreach (AttFile f in savedAttFiles)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", attId),
                        new SqlParameter("@Seq", f.Seq),
                        new SqlParameter("@FilePath", f.FilePath),
                        new SqlParameter("@AttFileNm", f.AttFileNm),
                        new SqlParameter("@SaveFileNm", f.SaveFileNm),
                        new SqlParameter("@FileSize", f.FileSize),
                        new SqlParameter("@RegUserId", pUserId)
                    };

                    DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }

            return attId;
        }
        #endregion

        #region 파일저장 : 수정, 현장현황 사진등록 이미지 용량 리사이징
        /// <summary>
        /// 이미지 업로드 시 용량 줄이기
        /// </summary>
        public void SaveResizePhoto(string pAttId, string pSavedFileSeqs, IEnumerable<HttpPostedFileBase> pFiles, ModuleCd pModule, string pUserId)
        {
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".jfif", ".jpeg 2000", ".exif", ".tiff", ".tif", ".gif", ".bmp", ".rle", ".dib", ".png", ".ppm", ".pgm", ".pbm", ".pnm"
            , ".heif", ".bpg", ".jfif", ".raw"};

            int savedMaxSeq = 0;
            var reImageSize = "";

            // 매개변수로 받은 pSavedFileSeqs을 savedFileSeqs배열에 넣는다.
            // pSavedFileSeqs는 화면에서 Request["p아이디Seqs"]를 통해 넘겨받은 기존 파일내역의 순번이다.
            // 기존 파일을 화면에서 삭제한 경우를 처리하기 위해, 화면에서 넘겨받은 내역을 savedFileSeqs 배열에 담아둔다.
            string[] savedFileSeqs = null;

            if (pSavedFileSeqs != null)
            {
                savedFileSeqs = pSavedFileSeqs.Split(',');
            }

            // 기존에 저장된 파일내역
            List<AttFile> savedAttFiles = ListAttFile(pAttId);

            // 삭제대상 파일들
            List<string> deleteTargetSeqs = new List<string>();
            
            // 기존에 저장된 파일 중 삭제해야하는 파일 체크 및 파일 삭제               
            foreach (AttFile f in savedAttFiles)
            {
                if (savedFileSeqs == null || savedFileSeqs.Contains(f.Seq) == false)
                {
                    deleteTargetSeqs.Add(f.Seq);
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Files/" + f.FilePath) + @"\" + f.SaveFileNm);                       
                }
            }

            // 새로 저장할 파일이 없고, 삭제할 파일도 없으면 리턴
            if ((pFiles == null || pFiles.Where(x => x != null).Count() == 0) && deleteTargetSeqs.Count() == 0)
            {
                return;
            }

            // 새로 저장할 파일이 있으면 추가로 저장한다.            
            List<AttFile> addedAttFiles = new List<AttFile>();

            if (pFiles != null)
            {
                // 기존에 저장된 Seq의 최고값을 구한다.
                if (savedAttFiles.Count() > 0)
                {
                    savedMaxSeq = int.Parse(savedAttFiles.Max(x => x.Seq));
                }

                // 저장년월
                string saveYearMonth = DateTime.Now.ToString("yyyyMM");

                // 저장폴더
                string saveDirectory = HttpContext.Current.Server.MapPath("~/Files/" + pModule.ToString() + "/") + saveYearMonth;

                // 이번달 폴더가 아직 생성되지 않은 경우 폴더를 새로 만든다.
                if (Directory.Exists(saveDirectory) == false)
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/Temp");

                //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
                if (Directory.Exists(tempDirectory) == false)
                {
                    Directory.CreateDirectory(tempDirectory);
                }

                // 랜덤변수 생성
                Random r = new Random(Guid.NewGuid().GetHashCode());

                // 파일목록의 파일들을 서버에 저장한다.
                foreach (HttpPostedFileBase file in pFiles.Where(x => x != null))
                {
                   
                   // 저장할 파일 크기가 10MB보다 큰 경우 예외를 발생시킨다.
                   if (file.ContentLength > 52428800)
                   {
                            throw new Exception("파일 크기가 50MB 보다 큰 경우 저장할 수 없습니다.");
                    }

                    //저장할 파일의 확장자 체크
                    if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                    {
                        throw new Exception("이미지 형식이 아니면 사진을 업로드 할 수 없습니다.");
                    }
                    // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                    string savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                        + "_"
                                        + DateTime.Now.ToString("yyyyMMddHHmmss")
                                        + r.Next(1000, 9999).ToString()
                                        + Path.GetExtension(file.FileName);

                    string resavefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                   + "_"
                                   + DateTime.Now.ToString("yyyyMMddHHmmss")
                                   + r.Next(1000, 9999).ToString()
                                   + Path.GetExtension(file.FileName);

                    // 파일명(ie8에서는 FileName에 경로까지 있기 때문에 처리해줘야함)
                    string attFileNm = Path.GetFileName(file.FileName);
                    
                    // 파일을 서버에 저장한다.
                    if (file.ContentLength > 1048576)
                    {

                        file.SaveAs(tempDirectory + @"\" + savefileNm);

                        MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                        Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                        Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                        int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장

                        System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제    

                        if (nRet == 52315)
                        {
                            file.SaveAs(saveDirectory + @"\" + savefileNm);
                        }

                        FileStream fi = new FileStream(saveDirectory + @"\" + savefileNm, FileMode.Open);

                        Bitmap img = new Bitmap(fi);

                        //Bitmap img = new Bitmap(file.InputStream);

                        int imgWidth = img.Width;
                        int imgHeight = img.Height;

                        int maxImgWidth = 900, maxImgHeight = 900;

                        int resizeImgWidth = 0, resizeImgHeight = 0;

                        if (imgWidth > maxImgWidth || imgHeight > maxImgHeight)
                        {
                            if (imgWidth > imgHeight)
                            {
                                resizeImgWidth = maxImgWidth;
                                resizeImgHeight = (imgHeight * resizeImgWidth) / imgWidth;
                            }
                            else
                            {
                                resizeImgHeight = maxImgHeight;
                                resizeImgWidth = (imgWidth * resizeImgHeight) / imgHeight;
                            }
                        }
                        else
                        {
                            resizeImgWidth = imgWidth;
                            resizeImgHeight = imgHeight;
                        }

                        Bitmap resizeImg = new Bitmap(resizeImgWidth, resizeImgHeight);

                        Graphics chgImg = Graphics.FromImage(resizeImg);
                        chgImg.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        chgImg.DrawImage(img, new Rectangle(0, 0, resizeImgWidth, resizeImgHeight));

                        resizeImg.Save(saveDirectory + @"\" + resavefileNm);

                        var info = new FileInfo(saveDirectory + @"\" + resavefileNm);

                        reImageSize = info.Length.ToString();

                        fi.Close();

                        System.IO.File.Delete(saveDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제

                        savefileNm = resavefileNm;

                    }
                    else
                    {
                        file.SaveAs(tempDirectory + @"\" + savefileNm);

                        MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                        Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                        Obj.sDestFilePath = saveDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                        int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장

                        System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제    

                        if (nRet == 52315)
                        {
                            file.SaveAs(saveDirectory + @"\" + savefileNm);
                        }

                        reImageSize = file.ContentLength.ToString();

                    }

                    // 파일내역 순번을 증가시킨다.
                    savedMaxSeq++;

                    // 저장된 파일내역에 추가한다.
                    addedAttFiles.Add(new AttFile
                    {
                        AttId = pAttId,
                        Seq = savedMaxSeq.ToString(),
                        FilePath = pModule.ToString() + "/" + saveYearMonth,
                        AttFileNm = attFileNm,
                        SaveFileNm = savefileNm,
                        FileSize = reImageSize
                    });
                }
            }

            //트랜잭션 처리
            using (TransactionScope scope = new TransactionScope())
            {
                SqlParameter[] sqlParams;

                // 추가된 파일내역 저장
                if (pFiles != null)
                {
                    foreach (AttFile f in addedAttFiles)
                    {
                        sqlParams = new SqlParameter[] {
                            new SqlParameter("@AttId", pAttId),
                            new SqlParameter("@Seq", f.Seq),
                            new SqlParameter("@FilePath", f.FilePath),
                            new SqlParameter("@AttFileNm", f.AttFileNm),
                            new SqlParameter("@SaveFileNm", f.SaveFileNm),
                            new SqlParameter("@FileSize", f.FileSize),
                            new SqlParameter("@RegUserId", pUserId)
                        };

                        DBAccess.ExcProc("PPT_AttFile_createAttFile", sqlParams, ConnStrNm.Main);
                    }
                }

                // 삭제된 파일 반영
                foreach (string seq in deleteTargetSeqs)
                {

                    sqlParams = new SqlParameter[] {
                        new SqlParameter("@AttId", pAttId),
                        new SqlParameter("@Seq", seq)
                    };

                    DBAccess.ExcProc("PPT_AttFile_deleteAttFile", sqlParams, ConnStrNm.Main);
                }

                // 트랜잭션 반영
                scope.Complete();
            }
        }
        #endregion

        #region 파일삭제 : 게시물 삭제에 따른 파일삭제
        /// <summary>
        /// 실물 파일 삭제
        /// </summary>
        public void DeleteFileAll(string pAttId)
        {                  
            // 기존에 저장된 파일내역
            List<AttFile> savedAttFiles = ListAttFile(pAttId);

            // 기존에 저장된 파일 중 삭제해야하는 파일 체크 및 파일 삭제               
            foreach (AttFile f in savedAttFiles)
            {                              
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Files/" + f.FilePath) + @"\" + f.SaveFileNm);             
            }        
        }
        #endregion

        #region 파일저장 : 에디터 이미지 첨부
        /// <summary>
        /// 에디터 이미지 첨부
        /// </summary>
        public string SE2ImageUpload(IEnumerable<HttpPostedFileBase> upload, string callback_func)
        {
            string savefileNm = string.Empty;

            // 저장할 파일이 없으면 빈문자열을 리턴한다.
            if (upload == null || upload.Where(x => x != null).Count() == 0)
            {
                return "";
            }

            // 저장할 파일 크기의 합계가 1MB보다 큰 경우 예외를 발생시킨다.
            if (upload.Where(x => x != null).Sum(x => x.ContentLength) > 10485760)
            {
                throw new Exception("파일 크기의 합계가 10MB 보다 큰 경우 저장할 수 없습니다.");
            }

            // 임시 이미지 저장폴더
            string tempDirectory = HttpContext.Current.Server.MapPath("~/Files/TempImg");

            // 에디터 첨부 이미지 저장폴더
            string UploadsDirectory = HttpContext.Current.Server.MapPath("~/Files/Uploads/Image");

            //사용자 PC에 복호화 파일을 위한 임시폴더가 없는 경우 임시폴더 생성
            if (Directory.Exists(tempDirectory) == false)
            {
                Directory.CreateDirectory(tempDirectory);
            }

            //사용자 PC에 이미지 업로드 폴더가 없는 경우 폴더 생성
            if (Directory.Exists(UploadsDirectory) == false)
            {
                Directory.CreateDirectory(UploadsDirectory);
            }

            // 랜덤변수 생성
            Random r = new Random(Guid.NewGuid().GetHashCode());

            // 저장된 파일내역
            List<AttFile> savedAttFiles = new List<AttFile>();

            // 파일내역 순번
            int seq = 0;
            
            // 파일목록의 파일들을 서버에 저장한다.
            foreach (HttpPostedFileBase file in upload.Where(x => x != null))
            {

                // 저장파일명 : 파일명 + 현재일시14자리 + 랜덤4자리 + 확장자
                    savefileNm = Path.GetFileNameWithoutExtension(file.FileName)
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMddHHmmss")
                                    + r.Next(1000, 9999).ToString()
                                    + Path.GetExtension(file.FileName);

                    file.SaveAs(tempDirectory + @"\" + savefileNm);

                    MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

                    Obj.sSourceFilePath = tempDirectory + @"\" + savefileNm;  // SAMPLE ENCRYPT FILE PATH
                    Obj.sDestFilePath = UploadsDirectory + @"\" + savefileNm;            // CREATE DECRYPTION FILE PATH

                    int nRet = Obj.iDecrypt(); // 소스파일 복호화 후 지정폴더에 복호화 파일 저장

                    System.IO.File.Delete(tempDirectory + @"\" + savefileNm); // 임시폴더 복호화 파일 삭제

                if (nRet == 52315)
                    {
                        file.SaveAs(UploadsDirectory + @"\" + savefileNm);
                    }

                // 파일내역 순번을 증가시킨다.
                seq++;

            }

            return savefileNm;

        }
        #endregion
    }
}

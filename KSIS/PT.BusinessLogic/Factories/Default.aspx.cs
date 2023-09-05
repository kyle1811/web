
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

// ENCRYPTION

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {

        MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();
    
    Obj.sSourceFilePath = "C:\\TEST_FILE\\SAMPLE\\,NET\\test.txt";	    // SAMPLE FILE_PATE(SAUCE FILE NAME)
		Obj.sDestFilePath = "C:\\TEST_FILE\\SAMPLE\\,NET\\test_enc.txt";		// ENCRYPTION FILE_PATH(ENCRYPTION FILE NAME) 
		
    Obj.sUserId = "test";		                              // USER_ID
		Obj.sUserName = "test"; 		                          // USER_NAME
		Obj.iCanSave = 1;			                                //  (0:CAN NOT SAVE, 1:CAN SAVE) 
		Obj.iCanEdit = 1;			                                //  (0:CAN NOT EDIT, 1:CAN EDIT)
		Obj.sDocValidPeriod = "-99";	                        // EXPIRATION DATE (-99:unlimited)
		Obj.iDocPrintCount = -99;	                            // PRINT COUNT (-99:unlimited)
		Obj.sDocId = "12345";		                              // FILE_ID
		Obj.sFileName = "test.txt";	                          // FILE_NAME
		Obj.sEnterpriseId = "MARKANY";
		Obj.sCompanyId = "MARKANY";
		Obj.iEncryptPrevCipher = 0;
		Obj.sEnterpriseName = "MARKANY";
		Obj.sCompanyName = "MARKANY";
		Obj.sServerOrigin = "EDMS";                           // FROM SYETEM NAME EX)edms, gw, pms 
		Obj.iDocOpenCount = -99;				                      // OPEN COUNT (-99:unlimited)
		Obj.iImageSafer = 0;
		Obj.sDocExchangePolicy = "1";
		Obj.iDocSaveLog = 1;
		Obj.iDocOpenLog = 1;
		Obj.iDocPrintLog = 1;
		Obj.iClipboardOption = 0;
		Obj.iOnlineAclControl = 0;
		Obj.sEncryptedBy = "0";
		Obj.iVisiblePrint = 0;
		Obj.sEncryptedBy = "0";
        
        int nRet = Obj.iEncrypt();

     //   var lbReturn=;
        
       // lbReturn.Text = nRet.ToString();		
    }




//ENCRYPTION FILE CHECK 

    protected void btnEncFileCheck_Click(object sender, EventArgs e)
    {
        MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();
        
        Obj.sSourceFilePath = "C:\\TEST_FILE\\SAMPLE\\,NET\\test.txt";

        int nRet = Obj.iEncCheck();



      //  lbReturn32.Text = nRet.ToString();
		
    }

//Decryption 

    protected void btnDec_Click(object sender, EventArgs e)
    {
        MAFILECIPHERXLib.FileCipherX Obj = new MAFILECIPHERXLib.FileCipherX();

        Obj.sSourceFilePath = "C:\\TEST_FILE\\SAMPLE\\,NET\\test_enc.txt";	// SAMPLE ENCRYPT FILE PATH
		    Obj.sDestFilePath = "C:\\TEST_FILE\\SAMPLE\\,NET\\test_dec.txt";		// CREATE DECRYPTION FILE PATH

        int nRet = Obj.iDecrypt();



       // lbReturn33.Text = nRet.ToString();

    }
	


}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PT.Web
{
    public partial class image : System.Web.UI.Page
    {
        public static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KCCENCDB"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string EmpNo = "21300610";
            DataSet db = LoadData(EmpNo);
            DataRow dbrow = db.Tables[0].Rows[0];
            string imgtype = "ImageFormat.Gif";

            Response.Clear();
            Response.ContentType = "image/" + imgtype;
   

            Response.BinaryWrite((byte[])dbrow["userimgpic"]);
            */
        }


        private DataSet LoadData(string EmpNo)
        {
         
            DataSet db = new DataSet();
            conn.Open();

                string sql = "SELECT userimgpic FROM TPT_KCCUSER where EmpNum =" + EmpNo;
               
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(db);
         

            return db;
        }
          
    }
}
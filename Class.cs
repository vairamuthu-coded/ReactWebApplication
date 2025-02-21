using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;



namespace ReactWebApplication
{
    public static class Class
    {
        public enum WeekDays
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
      
        public static class Users
        {
           // public static string ConnectionString { get; set; } = "server=localhost;port=3306;user id=root;password=Password@123;database=hospital";
            public static DateTime Log { get; set; }
            public static Int16 Digit { get; set; }
            public static string BarCodeType { get; set; }
            public static string Month { get; set; }
            public static Int64 RemainingDays { get; set; }
            public static int ScreenVisible { get; set; }
            public static string HostelName { get; set; }
            public static bool ValidCheck { get; set; }
            public static string PayPeriod { get; set; }
            public static string Database { get; set; }
            public static bool Enabled { get; set; }
           
            public static Int64 Paramid { get; set; }
            public static string GridID { get; set; }
            public static string CompCode1 { get; set; }
            public static string NonGridID { get; set; }
            public static string PoNo { get; set; }
            public static string insert= "Record Saved Successfully";
            public static string update= "Record Updated Successfully";
            public static string child = "Child Record Found";
            public static string delete = "Record Deleted Successfully";
            public static string DocID { get; set; }
            public static string Paramlistivew { get; set; }
            public static string UniqueID { get; set; }
            public static string asptbltestmasid { get; set; }
            public static string asptbltestitemmasid { get; set; }
            public static string patientname { get; set; }
            public static string asptblregisterid { get; set; }
            public static string asptbldiagnosisid { get; set; }
            public static string tokenno { get; set; }
            public static bool bisconnected { get; set; }
           
            public static string Finyear { get; set; }
            public static string OSUser { get; set; }
            public static Int64 COMPCODE { get; set; }
            public static string DataBase { get; set; }
            public static string MySqlDataBase { get; set; }
            public static string ProjectID { get; set; }
            public static string SqlProjectID { get; set; }
           
            public static double Sequenceno { get; set; }
            public static string Intimation { get; set; }
            public static string Query { get; set; }
            public static string TableNameGrid { get; set; }
            public static string TableNameSubGrid { get; set; }
            public static string ConString { get; set; }
            public static string DatabaseType { get; set; }
            public static string ConString1 { get; set; }
            public static string HCompcode { get; set; }
            public static int LoginTime { get; set; }
            public static int ElectionTime { get; set; }
            public static int SessionID { get; set; }
            public static int UserTime { get; set; }
            public static string HUserName { get; set; }


            public static System.Drawing.Color Color1
            {
                get { return System.Drawing.Color.White; }
                set { Color1 = System.Drawing.Color.White; }
            }
           
            public static System.Drawing.Color Color2
            {
                get { return System.Drawing.Color.AliceBlue; }
                set { Color2 = System.Drawing.Color.AliceBlue; }
            }
            public static System.Drawing.Color BackColors
            {
                get;
                set;
            }
            public static System.Drawing.Color ForeColors
            {
                get;
                set;
            }
            public static string ColorID
            {
                get;
                set;
            }
            public static System.Drawing.Color BackColorssss
            {
                get;
                set;
            }
            public static string HGateName { get; set; }
            public static string HCompName { get; set; }
            public static string DSOURCE { get; set; }
            public static Int64 USERID { get; set; }
            public static string AppName { get; set; }
            public static string CSPWORD { get; set; }
            public static string PWORD { get; set; }
            public static string ScreenName { get; set; }
            public static string SysDate { get; set; }
            public static string SysTime { get; set; }
            public static string ADMIN { get; set; }
            public static DateTime CREATED { get; set; }

            public static string IPADDRESS { get; set; }
            public static bool screen { get; set; }
            public static bool LoginActive { get; set; }
            public static bool Bisconnectclear { get; set; }
            public static string CANTEENMENUNAME { get; set; }
            public static Int64 TOKENEMPID { get; set; }
            public static Int64 IDCARDNO { get; set; }
            public static string TOKENEMPNAME { get; set; }
            public static string Prefix { get; set; }
            public static string FieldName { get; set; }
            public static Int64 DoctorName { get; set; }
            public static Int64 PatientName { get; set; }
            public static string Description { get; set; }
            public static Byte[] StaticByts { get; set; }
            public static string TableName { get; set; }
            public static string SearchQuery { get; set; }
            public static string[] HideCols { get; set; }
            public static DataTable QrCodeArray { get; set; } = new DataTable();
            public static DataTable QrCode { get; set; } = new DataTable();
            public static string PortIP { get; internal set; }
            public static string PortNo { get; internal set; }

            public static DataTable dt ;
            public static DataTable dt1;
            public static DataTable dt2;
            public static DataSet ds;

        
            
        }

        public class Master
        {
            
            public static async Task<DataTable> company()
            {
                Class.Users.ds = null;
                string sel2 = " SELECT DISTINCT A.TBLCOMPMASID,A.COMPCODE FROM TBLCOMPMAS A    WHERE A.ACTIVE='T'    ORDER BY 1 ";
                Class.Users.ds =await Utility.ExecuteSelectQuery(sel2, "TBLCOMPMAS");
                Class.Users.ds.Tables[0].Rows[0].Delete();
                Class.Users.ds.Tables[0].AcceptChanges();
                DataTable dt2 = Class.Users.ds.Tables["TBLCOMPMAS"];
                return dt2;
            }
            public static async Task<DataTable> comuserrihts()
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  DISTINCT A.TBLCOMPMASID,A.COMPCODE FROM TBLCOMPMAS  A  JOIN ASPTBLUSERRIGHTS B ON A.TBLCOMPMASID=B.COMPCODE  ORDER BY 1 ";
                Class.Users.ds =await Utility.ExecuteSelectQuery(sel2, "TBLCOMPMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLCOMPMAS"];
                return dt2;
            }
            public static async Task<DataTable> company(int id)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  XX.TBLCOMPMASID,xx.COMPCODE,xx.COMPNAME FROM TBLCOMPMAS XX where  XX.ACTIVE='T'   AND xx.TBLCOMPMASID='" + id + "' ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLCOMPMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLCOMPMAS"];
                return dt2;
            }
            public static async Task<DataTable> MenuName()
            {
                Class.Users.ds = null;
                string sel2 = " SELECT  B.TBLSITMENUMASID,B.SITEMENU,B.NAVURL from  TBLSITMENUMAS B ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Navurl()
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.NAVURL from  TBLSITMENUMAS B   ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Parentmenu()
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.TBLPARMENUMASID,B.MAINMENU from  TBLPARMENUMAS B    ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLPARMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLPARMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Parentmenu(int id, string pwd)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.TBLPARMENUMASID,B.MAINMENU from  TBLPARMENUMAS B WHERE B.COMPCODE='" + id + "' and  B.EPWORD='" + pwd + "'  ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLPARMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLPARMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> EpWord(int id)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT DISTINCT A.EPWORD FROM  TBLAPPMAS A JOIN TBLCOMPMAS B ON A.COMPCODE=B.TBLCOMPMASID WHERE B.TBLCOMPMASID='" + id + "' ";
                Class.Users.ds =await Utility.ExecuteSelectQuery(sel2, "TBLAPPMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLAPPMAS"];
                return dt2;
            }
            public static async Task<DataTable> ParentMenuid(int id)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  A.TBLPARMENUMASID,A.MAINMENU  FROM TBLPARMENUMAS A JOIN TBLCOMPMAS B ON B.TBLCOMPMASID=A.COMPCODE   WHERE A.COMPCODE='" + id + "' ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLPARMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLPARMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> MenuName(int id)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.TBLSITMENUMASID,B.SITEMENU,B.NAVURL from  TBLSITMENUMAS B   where B.COMPCODE='" + id + "'  ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> MenuName(int id, string pwd)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.TBLSITMENUMASID,B.SITEMENU,B.NAVURL from  TBLSITMENUMAS B   where B.COMPCODE='" + id + "'  AND B.EPWORD='" + pwd + "' ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Navurl(int id, string pwd)
            {
                Class.Users.ds = null;
                string sel2 = "SELECT  B.NAVURL from  TBLSITMENUMAS B  where B.COMPCODE='" + id + "' AND B.EPWORD='" + pwd + "' ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Listboxload()
            {
                Class.Users.ds = null;
                string sel2 = "select  B.TBLSITMENUMASID,B.SITEMENU from   TBLSITMENUMAS B   ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
            public static async Task<DataTable> Listboxload(int id, string pwd)
            {
                Class.Users.ds = null;


                string sel2 = " select  B.TBLSITMENUMASID,B.SITEMENU from  TBLSITMENUMAS B  WHERE B.COMPCODE='" + id + "'  AND B.EPWORD='" + pwd + "' ";
                Class.Users.ds = await Utility.ExecuteSelectQuery(sel2, "TBLSITMENUMAS");
                DataTable dt2 = Class.Users.ds.Tables["TBLSITMENUMAS"];
                return dt2;
            }
        }
   
    }
}
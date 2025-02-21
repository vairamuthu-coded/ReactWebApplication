using System.Collections;
using System.Data;

namespace ReactWebApplication
{
    public class GlobalVariables
    {
        public static System.Collections.Specialized.NameValueCollection MyAppPath = System.Configuration.ConfigurationSettings.AppSettings;
        public const string MasUser = "ADMIN";
        public const string MasPassword = "Password@123";
        public static string CompanyUser { get; set; }

        public static string CompanyPassword { get; set; }

        public static int CustID { get; set; }
        public static int CompID { get; set; }
        public static string CompanyName { get; set; }
        public static int CmpStateCode { get; set; }
        public static object CurrentForm { get; set; }
        public static string SearchQuery { get; set; }
        public static string[] HideCols { get; set; }
        public static int CurrentUserID { get; set; }
        public static string CurrentUserRole { get; set; }
        public static string CurrentUser { get; set; }
        public static bool New_Flg { get; set; }
        public static Hashtable ParamTbl { get; set; }

        public static Int32[] WidthCols { get; set; }
        public static Int32 SizeCount { get; set; }
        public static string CurrentMenuName { get; set; }


        //public static Label HeaderName { get; set; }
        public static DataTable Dt { get; set; }
        public static DataTable Dt1 { get; set; }
        public static DataTable Dt2 { get; set; }
        //public static ToolStripButton News { get; set; }
        //public static ToolStripButton Saves { get; set; }
        //public static ToolStripButton Prints { get; set; }
        //public static ToolStripButton Searchs { get; set; }
        //public static ToolStripButton Deletes { get; set; }
        //public static ToolStripButton ReadOnlys { get; set; }
        //public static ToolStripButton Imports { get; set; }
        //public static ToolStripButton Pdfs { get; set; }
        //public static ToolStripButton DownLoads { get; set; }
        //public static ToolStripButton ChangeSkins { get; set; }
        //public static ToolStripButton ChangePasswords { get; set; }
        //public static ToolStripButton Logins { get; set; }
        //public static ToolStripButton GlobalSearchs { get; set; }
        //public static ToolStripButton TreeButtons { get; set; }
        //public static ToolStripButton Exit { get; set; }
        public static bool Req_Barcode { get; set; } = true;
        //public static BarcodeLib.TYPE Bar_EncodeType { get; set; } = BarcodeLib.TYPE.UNSPECIFIED;

        public static bool Reqd_DelPassword { get; set; } = false;
        public static bool Reqd_EditPassword { get; set; } = false;
        public static bool Reqd_Barcode { get; set; } = false;
        //public static ToolStrip Toolstrip1 { get; set; }
        //public static MenuStrip MenuStrip1 { get; set; }
        //public static Panel MdiPanel { get; set; }
        public static object MasterForm { get; set; }
        public static DateTime ServerDate { get; set; }
        public static DateTime InstallDate { get; set; }
        public static DateTime ExpirtyDate { get; set; }
       // public static VisualStudioTabControl TabCtrl { get; set; }
        public static int AccDebtorsID { get; set; }
        public static int AccCreditorsID { get; set; }
        public static int AccCashLedID { get; set; }
        //public static string ConnectionString { get; set; } = MyAppPath["ConnectionString"];
        //public static string ConnectionString { get; set; } = "server=192.168.0.101;port=3306;user id=root;password=Password@123;database=tipltrading";
        public static string ConnectionString { get; set; } = "server=localhost;port=3306;user id=root;password=Password@123;database=tipltrading";
        public static string MMISConnectionString { get; set; } = "Data Source=148.72.246.210,4998;Initial Catalog=MMIS;User ID=grgadmin;Password=Grg@9843;";
        public static string BackupPath { get; set; }
        public static int BackupMinutes { get; set; }
        public static string CompanyShortName { get; set; }
        public static string PartyID { get; set; }
    }

}

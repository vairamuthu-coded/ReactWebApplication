using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Registration
{
    public class AutoGenerateMaster : CommonClass
    {
   
         Int64 asptblautogeneratemasid;
         string sequenceid;
         string shortcode;
         string finyear;
         Int64 compcode;
         string compcode1;
         Int64 screen;
         Int64 sequenceno;
         Int64 barcode;
         string barcodetype;
         Int64 barcode1;
         string barcodemonth;


        public Int64 Asptblautogeneratemasid { get { return asptblautogeneratemasid; } set { asptblautogeneratemasid = value; } }
        public string Sequenceid { get { return sequenceid; } set { sequenceid = value; } }
        public string Shortcode { get { return shortcode; } set { shortcode = value; } }
        public string Finyear { get { return finyear; } set { finyear = value; } }
        public Int64 Compcode { get { return compcode; } set { compcode = value; } }
        public string Compcode1 { get { return compcode1; } set { compcode1 = value; } }
        public Int64 Screen { get { return screen; } set { screen = value; } }
        public Int64 Sequenceno { get { return sequenceno; } set { sequenceno = value; } }
        public Int64 Barcode { get { return barcode; } set { barcode = value; } }
        public string Barcodetype { get { return barcodetype; } set { barcodetype = value; } }
        public Int64 Barcode1 { get { return barcode1; } set { barcode1 = value; } }
        public string Barcodemonth { get { return barcodemonth; } set { barcodemonth = value; } }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override async Task<DataTable> GridLoad()
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override async Task InsertCommond()
        {
            string ins = "insert into asptblautogeneratemas(sequenceid,  finyear,   compcode,    screen,  shortcode,  sequenceno,  active,  compcode1,  username,  createdby,   modifiedby, ipaddress,barcode1,barcodetype,barcode)  VALUES('" + Sequenceid + "',  '" + Finyear + "',   '" + Compcode + "',    '" + Screen + "',   '" + Shortcode + "', '" + Sequenceno + "',  '" + Active + "',  '" + Compcode + "',  '" + Username + "',  '" + System.DateTime.Now.ToString() + "', '" + Class.Users.HUserName + "', '" + Class.Users.IPADDRESS + "','" + Convert.ToString(Barcode) + "','" + Barcodetype + "', '" + Convert.ToString(Barcode1) + "' )";
            await Utility.ExecuteNonQuery(ins);
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateCommond()
        {
            string up = "update  asptblautogeneratemas  set sequenceid='" + Sequenceid + "',finyear='" + Finyear + "',compcode='" + Compcode + "',screen='" + Screen + "',shortcode='" + Shortcode + "',sequenceno='" + Sequenceno + "',active='" + Active + "',compcode1='" + Compcode + "',username='" + Username + "',createdby='" + System.DateTime.Now.ToString() + "',modifiedby='" + Class.Users.HUserName + "', ipaddress='" + Class.Users.IPADDRESS + "',barcodetype='" + Barcodetype + "',barcode='" + Convert.ToString(Barcode) + "' ,barcode1='" + Convert.ToString(Barcode1) + "' where asptblautogeneratemasid='" + Asptblautogeneratemasid + "'";
            await Utility.ExecuteNonQuery(up);
        }

      
    }
}

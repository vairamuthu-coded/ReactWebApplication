using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{
    public class CompanyMaster : CommonClass
    {
        [Key]
        public Int64 gtcompmastid { get; set; }
        public Int64 gtcompmastid1 { get; set; }
        public string compcode { get; set; }
        public string compname { get; set; }
        public string devision { get; set; }
        public string shortcode { get; set; }
        public Int64 city { get; set; }
        public Int64 state { get; set; }
        public Int64 country { get; set; }
        public string ptransaction { get; set; }
        public string displayname { get; set; }
        public string address { get; set; }
        public Int64 pincode { get; set; }
        public string gstno { get; set; }
        public string gstdate { get; set; }
        public string phoneno { get; set; }
        public string accno { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string contactname { get; set; }   
        public Int64 bankname { get; set; }
        public Int64 branch { get; set; }
        public Int64 imagebytes { get; set; }
        
        public string ifsc { get; set; }
        public string accountholdername { get; set; }
        public byte[] ImageByte { get; set; }
        public string ImagePath { get; set; }
        public string images { get; set; }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }



        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad()
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override async Task<DataTable> SelectCommond()
        {
            string sel1 = "select MAX(gtcompmastid) ID    from  gtcompmast   WHERE compcode='" + compcode + "' ";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel1, "gtcompmast");
            DataTable dt1 = ds1.Tables["gtcompmast"];
            return dt1;
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateCommond()
        {
            string up = "update  gtcompmast  set  gtcompmastid1='" + gtcompmastid1 + "',compcode='" + compcode.ToUpper() + "',compname='" + compname.ToUpper() + "',displayname='" + displayname.ToUpper().Trim() + "', city='" + city + "', state='" + state + "',country='" + country + "' ,address='" + address + "',pincode='" + Convert.ToInt64("0" + pincode).ToString() + "',gstno='" + gstno.ToUpper() + "',gstdate='" + gstdate + "',phoneno='" + phoneno + "',email='" + email.ToUpper() + "',website='" + website.ToUpper() + "',contactname='" + contactname.ToUpper() + "', active='" + Active + "', modifiedby='" + Class.Users.HUserName + "',ipaddress='" + Class.Users.IPADDRESS + "' ,bankname='" + bankname + "' , accno='" + accno + "' , ifsc='" + ifsc + "' where  gtcompmastid='" + gtcompmastid + "'";
            await Utility.ExecuteNonQuery(up);
        }
    }

}

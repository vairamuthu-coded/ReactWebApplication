using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ReactWebApplication.Models
{

    public class CountryMaster:CommonClass
    {

        private readonly AppDBContext _appDBContext;
        private Int64 gtcountrymastid { get; set; }
        private string? countryname { get; set; }
        StringBuilder sel=new StringBuilder();

        public CountryMaster(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public CountryMaster()
        {
        }

        public Int64 Gtcountrymastid { get { return gtcountrymastid; } set { gtcountrymastid = value; } }

        public string? Countryname { get { return countryname; } set { countryname = value; } }

        public override async Task<DataTable> GridLoad()
        {
            sel.Append("select   a.Gtcountrymastid , a.Countryname ,a.Active from gtcountrymast a ");
            DataSet dscountry = await Utility.ExecuteSelectQuery(sel.ToString(), "gtcountrymast");
            DataTable dtcountry = dscountry.Tables["gtcountrymast"];            
            return dtcountry;
        }     

        public override async    Task  InsertCommond()
        {
            sel.Append("insert into GTCOUNTRYMAST(Countryname,active,createdby,modifiedby,ipaddress)  VALUES('" + Countryname.ToUpper() + "','" + Active + "','" + Username + "','" + Username + "','" + Ipaddress + "' )");
          await  Utility.ExecuteNonQuery(sel.ToString());
        }
        public override async Task<DataTable> SelectCommond()
        {
            sel.Append("select Gtcountrymastid    from  GTCOUNTRYMAST    WHERE Countryname='" + Countryname + "' and active='" + Active + "' ");
            DataSet ds =await Utility.ExecuteSelectQuery(sel.ToString(), "GTCOUNTRYMAST");
            DataTable dt = ds.Tables["GTCOUNTRYMAST"];
            return dt;
        }
        public override async   Task UpdateCommond()
        {
            sel.Append("update  GTCOUNTRYMAST  set   Countryname='" + Countryname.ToUpper() + "' , active='" + Active + "' , modifiedby='" + Username + "',ipaddress='" + Ipaddress + "' where Gtcountrymastid='" + Gtcountrymastid + "'");
            await Utility.ExecuteNonQuery(sel.ToString());
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }
    }


}

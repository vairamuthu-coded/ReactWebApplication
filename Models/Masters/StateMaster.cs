using Mono.TextTemplating;
using Org.BouncyCastle.Utilities.Net;

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace ReactWebApplication.Models.Masters
{

    public class StateMaster:CommonClass
    {
   
       
        private Int64 gtstatemastid { get; set; }

        [Required]
        [MaxLength(20)]
        private string statename { get; set; }
        private long country { get; set; }

        StringBuilder sel = new StringBuilder();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public string Statename { get { return statename; } set { statename = value; } }
        public long Country { get { return country; } set { country = value; } }
        public long Gtstatemastid { get { return gtstatemastid; } set { gtstatemastid = value; } }



        public override async Task<DataTable> GridLoad()
        {
          sel.Append("select b.gtstatemastid,b.statename,  a.Gtcountrymastid  as  gtcountrymastid , a.Countryname as country ,b.active from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country   ;");
            ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
            dt = ds.Tables["gtstatemast"];
            return dt;
        }
        public override async Task<DataTable> GridLoad(long id)
        {
            sel.Append("select b.gtstatemastid,b.statename ,  a.Gtcountrymastid , a.Countryname ,b.active from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country   where  b.gtstatemastid='" + id + "' ;");
            ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
            dt = ds.Tables["gtstatemast"];
            return dt;
        }
        
        public override async Task InsertCommond()
        {
            
            sel.Append("insert into gtstatemast(statename,country,active,createdby,modifiedby,ipaddress)  VALUES('" + Statename.ToUpper() + "','" + Country + "','" + Active + "','" +Username + "','" + Username + "','" + Ipaddress + "' )");
          await  Utility.ExecuteNonQuery(sel.ToString());
        }

        public override async Task<DataTable> SelectCommond()
        {
           
            sel.Append("select gtstatemastid    from  gtstatemast    WHERE statename='" + Statename + "' and country='" + Country + "' and active='" + Active + "'");
            ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
           dt = ds.Tables["gtstatemast"];
            return dt;
        }

        public override async Task UpdateCommond()
        {
            sel.Clear();
            sel.Append("update  gtstatemast SET statename='" + Statename + "' ,country='" + Country + "',active='" + Active + "' where gtstatemastid='" + Gtstatemastid + "'");
            await Utility.ExecuteNonQuery(sel.ToString());
        }

        internal async Task<DataTable> StateGridLoad()
        {
            sel.Append("select b.gtstatemastid,b.statename ,  a.Gtcountrymastid , a.Countryname ,b.active from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country   where  b.active='T'");
            ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
            dt = ds.Tables["gtstatemast"];
            return dt;
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }



        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

   



        //public override Task<DataTable> GridLoad()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task InsertCommond()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<DataTable> SelectCommond()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task UpdateCommond()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

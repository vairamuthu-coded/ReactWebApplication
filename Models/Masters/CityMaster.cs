using ReactWebApplication.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{

    public class CityMaster:CommonClass
    {        
        private Int64 gtcitymastid { get; set; }

        private string cityname { get; set; }
        private long state { get; set; }
        private long country { get; set; }
       
        
        public Int64 Gtcitymastid { get { return gtcitymastid; } set { gtcitymastid = value; } }
        public String Cityname  { get { return cityname; } set { cityname = value; }}
        public long State { get { return state; } set { state = value; } }
        public long Country { get { return country; } set { country = value; } }

      
     
        public override async Task<DataTable> GridLoad()
        {
            string sel1 = " select a.GTCITYMASTID,a.cityname, b.statename as state, c.countryname as country , a.active    from  GTCITYMAST a  join GTSTATEMAST b on a.state=b.GTSTATEMASTID   join GTCOUNTRYMAST c on b.country=c.GTCOUNTRYMASTID    order by 1";
            DataSet ds =await Utility.ExecuteSelectQuery(sel1, "GTCITYMAST");
            DataTable dt = ds.Tables["GTCITYMAST"];
           
            return dt;
        }
       
        
        public override async Task InsertCommond()
        {
            string sel = "insert into  gtcitymast(Cityname,state,Country,active) values('" + Cityname + "' ,'" + State + "' ,'" + Country + "' ,'" + Active + "')";
            await Utility.ExecuteNonQuery(sel.ToString());
        }

        public override async Task<DataTable> SelectCommond()
        {
            string sel = "select a.GTCITYMASTID    from  GTCITYMAST a join GTCOUNTRYMAST b on a.country=b.GTCOUNTRYMASTID   WHERE a.cityname='" + Cityname + "' and a.state='" + State + "' and a.country='" + Country + "' and a.active='" + Active + "' ";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "GTCITYMAST");
            DataTable dt = ds.Tables["GTCITYMAST"];
            return dt;
        }

        public override async Task UpdateCommond()
        {
            string sel="update  gtcitymast SET Cityname='" + Cityname + "' , state='" + State + "' ,Country='" + Country + "',active='" + Active + "' where gtcitymastid='" + Gtcitymastid + "'";
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

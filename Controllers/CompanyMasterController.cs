using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using ReactWebApplication.Models.Masters;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlConnector;
using ReactWebApplication.Models;


namespace ReactWebApplication.Controllers
{
    //public interface  IGridLoad
    //{
    //    Task<IActionResult> GridLoad(string id);
    //}

    [Route("api/[controller]")]
    [ApiController]  
    public class CompanyMasterController : ControllerBase,InterfaceClass
    {
        private const string V = "CompanyMaster/{compcode}";
        private StringBuilder sel = new StringBuilder(); bool msg = false;

  
        public DataTable dataTable { get; set; }
          public DataSet ds { get; set; }

        [HttpGet("CompanyMaster")]
        public async Task<IActionResult> CompanyMaster()
        {
            string sel = "select a.gtcompmastid,a.compcode from gtcompmast a join gtcitymast b on a.city=b.gtcitymastid join gtstatemast c on c.gtstatemastid=a.state  join gtcountrymast d on d.Gtcountrymastid=a.country ";
            ds = await Utility.ExecuteSelectQuery(sel, "gtcompmast");
            dataTable = ds.Tables["gtcompmast"];
            return new JsonResult(dataTable);
        }



        [HttpGet("GridLoad")]
        public  async Task<IActionResult> GridLoad()
        {
            string sel = "select a.gtcompmastid,a.compcode,a.compname,b.cityname,c.statename,d.Countryname,a.active from gtcompmast a join gtcitymast b on a.city=b.gtcitymastid join gtstatemast c on c.gtstatemastid=a.state  join gtcountrymast d on d.Gtcountrymastid=a.country ";
            ds =await Utility.ExecuteSelectQuery(sel, "gtcompmast");
            dataTable = ds.Tables["gtcompmast"];
            return new JsonResult(dataTable);
        }


        [HttpGet("GridLoad/{id}")]
        public async Task<IActionResult> GridLoad(string id)
        {
            DataTable dt = new DataTable();

            sel.Clear();
            sel.Append("select a.gtcompmastid,a.gtcompmastid1,a.displayname,a.compcode,a.compname,b.gtcitymastid,c.gtstatemastid,d.gtcountrymastid,a.address,a.gstno,a.gstdate,a.website,a.email,a.accno,a.bankname,a.ifsc,a.phoneno,a.contactname,a.active,a.companylogoo from gtcompmast a join gtcitymast b on a.city=b.gtcitymastid join gtstatemast c on c.gtstatemastid=a.state  join gtcountrymast d on d.Gtcountrymastid=a.country  where  a.compcode='" + id + "' ;");
            DataSet ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtcompmast");
            dt = ds.Tables["gtcompmast"];
            if (dt.Rows.Count > 0)
            {
                Class.Users.HCompcode = id;
                Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["gtcompmastid"].ToString());
            }

            return new JsonResult(dt);
        }

        [HttpPut("GridLoad/{id}")]
        public async Task<IActionResult> GridLoad(Int64 id)
        {
            sel.Clear();
            sel.Append("select a.gtcompmastid,a.gtcompmastid1,a.displayname,a.compcode,a.compname,b.gtcitymastid,c.gtstatemastid,d.gtcountrymastid,a.address,a.gstno,a.gstdate,a.website,a.email,a.accno,a.bankname,a.ifsc,a.phoneno,a.contactname,a.active,a.companylogoo from gtcompmast a join gtcitymast b on a.city=b.gtcitymastid join gtstatemast c on c.gtstatemastid=a.state  join gtcountrymast d on d.Gtcountrymastid=a.country  where  a.gtcompmastid='" + id + "' ;");
            DataSet ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtcompmast");
            DataTable dt = ds.Tables["gtcompmast"];
            return new JsonResult(dt);
        }

        [HttpDelete("DeleteCommond/{id}")]

        public async Task<IActionResult> DeleteCommond(Int64 id)
        {
            string msg = string.Empty;
            sel.Clear();
            sel.Append("delete from gtcompmast where gtcompmastid=" + id);
            await Utility.ExecuteNonQuery(sel.ToString());
            msg = "delete";
            return new JsonResult(msg);
        }


        [HttpPost("Saves")]
        public async Task<IActionResult> Saves(CompanyMaster p)
        {
            try
            {
                if (p.city > 0 && p.state > 0 && p.country > 0)
                {
                    Class.Users.COMPCODE = 1;

                    Class.Users.USERID = 1;
                    p.Username = Class.Users.USERID;
                    p.Ipaddress = GenFun.GetLocalIPAddress();
                    p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                    p.Createdby = Class.Users.HUserName;
                    p.Modifiedby = Class.Users.HUserName;
                    if (p.gtcompmastid == 0)
                    {

                        if (p.images != null)
                        {
                            DataTable dt1 = await p.SelectCommond();
                            string ins1 = "UPDATE  gtcompmast SET companylogoo=@companylogoo where  gtcompmastid='" + dt1.Rows[0]["ID"].ToString() + "'";
                            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(ins1, Utility.Connect());
                            cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@companylogoo", p.images));
                            cmd.ExecuteNonQuery();
                        }
                        sel.Append(Class.Users.insert);

                    }
                    else
                    {
                        sel.Clear();
                        await p.UpdateCommond();
                        if (p.images != null)
                        {
                            string up1 = "UPDATE  gtcompmast SET  companylogoo=@companylogoo,companylogoo=@companylogoo where  gtcompmastid='" + p.gtcompmastid + "'";
                            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(up1, Utility.Connect());
                            cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("@companylogoo", p.images));
                            cmd.ExecuteNonQuery();

                        }
                        sel.Append(Class.Users.update);
                    }
                }
            }
            catch (Exception ex)
            {
                sel.Clear();
                sel.Append(ex.Message);
            }

            return Ok(sel.ToString());
        }

        [NonAction]
        public Task<IActionResult> SelectCommond()
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public Task<IActionResult> SelectCommond(long id)
        {
           
            throw new NotImplementedException();
        }

     





        //public Task<IActionResult> SelectCommond(long id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

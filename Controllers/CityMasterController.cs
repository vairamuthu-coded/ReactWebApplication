using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;
using System.Data;
using System.Text;

namespace ReactWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityMasterController : ControllerBase,InterfaceClass
    {
        private StringBuilder sel = new StringBuilder(); bool msg = false;
        private readonly AppDBContext _context;
        CommonClass cityMaster=new Models.Masters.CityMaster();
        public CityMasterController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("GridLoad")]
        public  async Task<IActionResult> GridLoad()
        {
        
            DataTable dt = await cityMaster.GridLoad();
            return Ok(dt);
        }



        [HttpGet("GridLoad/{id}")]
        public async Task<IActionResult> GridLoad(Int64 id)
        {
            sel.Clear();
            sel.Append("select c.gtcitymastid ,c.Cityname , b.gtstatemastid , a.gtcountrymastid,c.active from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country join gtcitymast c on c.country=a.Gtcountrymastid and c.state=b.gtstatemastid  where  c.gtcitymastid='" + id + "' ;");
            DataSet ds = await Utility.ExecuteSelectQuery(sel.ToString(), "gtcountrymast");
            DataTable dt = ds.Tables["gtcountrymast"];
            return new JsonResult(dt);
        }

        //[HttpGet("GridLoad/{id}")]
        //public  async Task<IActionResult> GridLoad(Int64 id)
        //{
        //    sel.Clear();
        //    sel.Append("select  b.gtstatemastid , b.statename from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country join gtcitymast c on c.country=a.Gtcountrymastid and c.state=b.gtstatemastid  where  c.gtcitymastid='" + id + "' ;");
        //    DataSet dscountry = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
        //    DataTable dtcountry = dscountry.Tables["gtstatemast"];
        //   return Ok(dtcountry);
        //}

        [HttpDelete("DeleteCommond/{id}")]

        public async Task<IActionResult> DeleteCommond(Int64 id)
        {
            string msg = string.Empty;
            sel.Clear();
            sel.Append("delete from gtcitymast where gtcitymastid=" + id);
           await Utility.ExecuteNonQuery(sel.ToString());
            msg = "delete";
            return new JsonResult(msg);
        }

        //[NonAction]
        //public async Task<DataTable>  GetByName(CityMaster st)
        //{
        //    DataTable dt = await st.SelectCommond();
        //    return dt;
        //}

        //[HttpPost("CityMaster_Insert_Update")]
        [HttpPost("Saves")]
        public  async Task<IActionResult> Saves(CityMaster cou)
        {
         //  await GetByName(cou);
            sel.Clear();
            try
            {
                if (cou.State > 0 && cou.Country > 0)
                {
              
                    cou.Cityname = cou.Cityname.ToUpper();
                    cou.Username = Class.Users.USERID;
                    cou.Ipaddress = GenFun.GetLocalIPAddress();
                    cou.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                    cou.Createdby = Class.Users.HUserName;
                    cou.Modifiedby = Class.Users.HUserName;
                    //var result = _context.gtcitymast.AsEnumerable().Where(x => x.Cityname == cou.Cityname && x.State == cou.State && x.Country == cou.Country && x.Active == cou.Active).Count();
                    DataTable dt =await  cou.SelectCommond();
                    if (dt.Rows.Count != 0)
                    {
                        sel.Append(Class.Users.child);
                    }
                    else if (dt.Rows.Count != 0 && cou.Gtcitymastid == 0 || cou.Gtcitymastid == 0)
                    {
                     
                        await cou.InsertCommond();
                        sel.Append(Class.Users.insert);
                    }
                    else
                    {
                        sel.Clear();
                      await  cou.UpdateCommond();
                        sel.Clear();
                        sel.Append(Class.Users.update);
                    }
                }
                else
                {
                    sel.Clear();
                    sel.Append("Invalid");
                }
            }
            catch (Exception ex)
            {
               
                sel.Append(ex.Message);
            }

            return Ok(sel.ToString());


        }

        [HttpGet("GetData")]
        public void GetData(CommonClass cou)
        {


            cou.Username = 1;
            cou.Ipaddress = GenFun.GetLocalIPAddress();
            cou.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());
            cou.Createdby = "vairam";
            cou.Modifiedby = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy") + " " + System.DateTime.Now.ToLongTimeString());

        }

        [NonAction]
        public Task<IActionResult> GridLoad(string id)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public Task<IActionResult> SelectCommond()
        {
            throw new NotImplementedException();
        }


    }
}

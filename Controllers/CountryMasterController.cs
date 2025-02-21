
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;
using System;
using System.Data;
using System.Text;


namespace ReactWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryMasterController : ControllerBase,InterfaceClass
    {      
        Models.TreeView.userRights sm = new Models.TreeView.userRights();
        CountryMaster countryMaster=new Models.CountryMaster();
       
        public static CountryMaster Instance => (CountryMaster)(GlobalVariables.CurrentForm = "CountryMaster");
        private readonly AppDBContext _appDBContext;
        DataTable dtcountry = new DataTable();
        DataSet dscountry = new DataSet();
        private StringBuilder sel = new StringBuilder(); bool msg = false;
  

        [HttpGet("usercheck")]
        public async Task<IActionResult> usercheck()
        {
            Class.Users.ScreenName = "CountryMaster";
            DataTable dt1 =await sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
          
            return Ok(dt1);

        }

    

        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {

            await usercheck();
            DataTable dtcountry = await countryMaster.GridLoad();
            return new JsonResult(dtcountry);
        }

        //[HttpGet("CountryMaster_Data")]
        //public async Task<JsonResult> CountryMaster_Data()
        //{
        //    sel.Clear();

        //    DataTable dtcountry = await countryMaster.GridLoad();
        //    return new JsonResult(dtcountry);
        //    //var datarows = (from row in _context.gtcountrymast.AsEnumerable()
        //    //                where row.active == "T"
        //    //                select row);
        //    //return new JsonResult(datarows);      
        //}



        [HttpPost]
        [Route("Saves")]
        public async Task<IActionResult> Saves(CountryMaster cou)
        {
            sel.Clear();
            try
            {
                cou.Countryname = cou.Countryname.ToUpper();
                cou.Username = Class.Users.USERID;
                cou.Ipaddress = GenFun.GetLocalIPAddress();
                cou.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                cou.Createdby = Class.Users.HUserName;
                cou.Modifiedby = Class.Users.HUserName;
                cou.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                DataTable dt = await cou.SelectCommond();
                if (dt.Rows.Count != 0)
                {
                    sel.Append(Class.Users.child);
                }
                else if (dt.Rows.Count != 0 && cou.Gtcountrymastid == 0 || cou.Gtcountrymastid == 0)
                {
                 
                    await cou.InsertCommond();
                    sel.Append(Class.Users.insert);
                }
                else
                {
                  await  cou.UpdateCommond();
                    
                    sel.Append(Class.Users.update);
                }
            }
            catch (Exception ex)
            {

                sel.Append(ex.Message);

            }
            return Ok(sel.ToString());

        }


        [NonAction]
        public Task<IActionResult> GridLoad(Int64 id)
        {
            throw new NotImplementedException();
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

        [NonAction]
        public Task<IActionResult> DeleteCommond(Int64 id)
        {
            throw new NotImplementedException();
        }
    }
}

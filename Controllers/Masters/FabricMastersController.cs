using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models.Masters;

namespace ReactWebApplication.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public FabricMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/FabricMasters
        [HttpGet("GetFabricMaster")]
        public async Task<ActionResult<IEnumerable<FabricMaster>>> GetFabricMaster()
        {
            // string sel= "select a.asptblfabmasid,b.fabrictype,a.fabric,a.aliasname,a.hsn,c.yarnblend as yarnblend1 ,ifnull(C.asptblyarblemasid,0) as yarn1,b.asptblfabrictypemasid,ifnull(d.yarnblend,0) as yarnblend2,ifnull(d.asptblyarblemasid,0) as yarn2, ifnull(e.yarnblend,0) as yarnblend3,ifnull(e.asptblyarblemasid,0) as yarn3,ifnull(f.yarnblend,0) as yarnblend4,ifnull(f.asptblyarblemasid,0) as yarn4,ifnull(g.yarnblend,0) as yarnblend5,ifnull(g.asptblyarblemasid,0) as yarn5,a.per1,a.per2,a.per3,a.per4,a.per5,a.fabric,a.aliasname,a.hsn,a.active from asptblfabmas a join asptblfabrictypemas b on a.fabrictype=b.asptblfabrictypemasid left join asptblyarblemas c on c.asptblyarblemasid=a.yarnblend1 left join asptblyarblemas d on d.asptblyarblemasid=a.yarnblend2 left join asptblyarblemas e on e.asptblyarblemasid=a.yarnblend3 left join asptblyarblemas f on f.asptblyarblemasid=a.yarnblend4 left join asptblyarblemas g on g.asptblyarblemasid=a.yarnblend5  order by a.asptblfabmasid desc; ";
            string sel = "select a.asptblfabmasid,b.fabrictype,a.fabric,a.aliasname,a.hsn,c.yarnblend as yarnblend1 ,ifnull(C.asptblyarblemasid,'') as yarn1,b.asptblfabrictypemasid,ifnull(d.yarnblend,'') as yarnblend2,ifnull(d.asptblyarblemasid,'') as yarn2, ifnull(e.yarnblend,'') as yarnblend3,ifnull(e.asptblyarblemasid,'') as yarn3,ifnull(f.yarnblend,'') as yarnblend4,ifnull(f.asptblyarblemasid,'') as yarn4,ifnull(g.yarnblend,'') as yarnblend5,ifnull(g.asptblyarblemasid,'') as yarn5,ifnull(a.per1,'')as per1,ifnull(a.per2,'')as per2,ifnull(a.per3,'')as per3,ifnull(a.per4,'')as per4,ifnull(a.per5,'')as per5,a.fabric,a.aliasname,a.hsn,a.active ,a.per,a.organic from asptblfabmas a join asptblfabrictypemas b on a.fabrictype=b.asptblfabrictypemasid left join asptblyarblemas c on c.asptblyarblemasid=a.yarnblend1 left join asptblyarblemas d on d.asptblyarblemasid=a.yarnblend2 left join asptblyarblemas e on e.asptblyarblemasid=a.yarnblend3 left join asptblyarblemas f on f.asptblyarblemasid=a.yarnblend4 left join asptblyarblemas g on g.asptblyarblemasid=a.yarnblend5 order by a.asptblfabmasid desc;";

            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblfabmas");
           DataTable dt1 = ds1.Tables["asptblfabmas"];
            return Ok(dt1);
        }

        // GET: api/FabricMasters/5
        [HttpGet("GetFabricMaster/{id}")]
        public async Task<ActionResult<FabricMaster>> GetFabricMaster(long id)
        {
            string sel = "select a.asptblfabmasid,b.fabrictype,a.fabric,a.aliasname,a.hsn,c.yarnblend as yarnblend1 ,ifnull(C.asptblyarblemasid,'') as yarn1,b.asptblfabrictypemasid,ifnull(d.yarnblend,'') as yarnblend2,ifnull(d.asptblyarblemasid,'') as yarn2, ifnull(e.yarnblend,'') as yarnblend3,ifnull(e.asptblyarblemasid,'') as yarn3,ifnull(f.yarnblend,'') as yarnblend4,ifnull(f.asptblyarblemasid,'') as yarn4,ifnull(g.yarnblend,'') as yarnblend5,ifnull(g.asptblyarblemasid,'') as yarn5,ifnull(a.per1,'')as per1,ifnull(a.per2,'')as per2,ifnull(a.per3,'')as per3,ifnull(a.per4,'')as per4,ifnull(a.per5,'')as per5,a.fabric,a.aliasname,a.hsn,a.active,a.per,a.organic from asptblfabmas a join asptblfabrictypemas b on a.fabrictype=b.asptblfabrictypemasid left join asptblyarblemas c on c.asptblyarblemasid=a.yarnblend1 left join asptblyarblemas d on d.asptblyarblemasid=a.yarnblend2 left join asptblyarblemas e on e.asptblyarblemasid=a.yarnblend3 left join asptblyarblemas f on f.asptblyarblemasid=a.yarnblend4 left join asptblyarblemas g on g.asptblyarblemasid=a.yarnblend5 where a.asptblfabmasid='" + id+"'";
          //  string sel = "select a.asptblfabmasid,b.fabrictype,a.fabric,a.aliasname,a.hsn,c.yarnblend as yarnblend1 ,ifnull(C.asptblyarblemasid,0) as yarn1,b.asptblfabrictypemasid,ifnull(d.yarnblend,0) as yarnblend2,ifnull(d.asptblyarblemasid,0) as yarn2, ifnull(e.yarnblend,0) as yarnblend3,ifnull(e.asptblyarblemasid,0) as yarn3,ifnull(f.yarnblend,0) as yarnblend4,ifnull(f.asptblyarblemasid,0) as yarn4,ifnull(g.yarnblend,0) as yarnblend5,ifnull(g.asptblyarblemasid,0) as yarn5,a.per1,a.per2,a.per3,a.per4,a.per5,a.fabric,a.aliasname,a.hsn,a.active from asptblfabmas a join asptblfabrictypemas b on a.fabrictype=b.asptblfabrictypemasid left join asptblyarblemas c on c.asptblyarblemasid=a.yarnblend1 left join asptblyarblemas d on d.asptblyarblemasid=a.yarnblend2 left join asptblyarblemas e on e.asptblyarblemasid=a.yarnblend3 left join asptblyarblemas f on f.asptblyarblemasid=a.yarnblend4 left join asptblyarblemas g on g.asptblyarblemasid=a.yarnblend5  where a.asptblfabmasid='" + id+"'  ";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblfabmas");
            DataTable dt1 = ds1.Tables["asptblfabmas"];
            return Ok(dt1);

            //var fabricMaster = await _context.asptblfabmas.FindAsync(id);

            //if (fabricMaster == null)
            //{
            //    return NotFound();
            //}

            //return fabricMaster;
        }

        //      [HttpPost("PostFabricMaster")]
        //public async Task<ActionResult<FabricMaster>> PostFabricMaster(FabricMaster fabricMaster)
        //{
        //    try
        //    {
        //        fabricMaster.fabric = fabricMaster.fabric.ToUpper();
        //        fabricMaster.aliasname = fabricMaster.aliasname.ToUpper();
        //        fabricMaster.hsn = fabricMaster.hsn.ToUpper();
        //        fabricMaster.compcode = Class.Users.COMPCODE;
        //        fabricMaster.Username = Class.Users.USERID;
        //        fabricMaster.Ipaddress = GenFun.GetLocalIPAddress();
        //        fabricMaster.Createdby = Class.Users.HUserName;
        //        fabricMaster.Modifiedby = Class.Users.HUserName;
        //        fabricMaster.Createdon = Convert.ToDateTime(DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //        fabricMaster.Modifiedon = Convert.ToString(DateTime.Now.ToString());
        //        fabricMaster.Modifiedby = Class.Users.HUserName;
        //        if (fabricMaster.asptblfabmasid == 0)
        //        {
        //            _context.asptblfabmas.Add(fabricMaster);
        //            await _context.SaveChangesAsync();
        //            fabricMaster.asptblfabmasid = 0;
        //        }
        //        else
        //        {
        //            _context.Entry(fabricMaster).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FabricMasterExists(fabricMaster.asptblfabmasid))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return CreatedAtAction("GetFabricMaster", new { id = fabricMaster.asptblfabmasid }, fabricMaster);
        //}

        //// DELETE: api/FabricMasters/5
        //[HttpDelete("DeleteFabricMaster/{id}")]
        //public async Task<IActionResult> DeleteFabricMaster(long id)
        //{
        //    var fabricMaster = await _context.asptblfabmas.FindAsync(id);
        //    if (fabricMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblfabmas.Remove(fabricMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFabricMaster", new { id = id }, fabricMaster);
        //}

        //private bool FabricMasterExists(long id)
        //{
        //    return _context.asptblfabmas.Any(e => e.asptblfabmasid == id);
        //}
    }
}

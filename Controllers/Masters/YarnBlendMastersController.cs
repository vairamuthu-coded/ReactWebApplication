using System;
using System.Collections.Generic;
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
    public class YarnBlendMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public YarnBlendMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/YarnBlendMasters
        //[HttpGet("GetYarnBlendMaster")]
        //public async Task<ActionResult<IEnumerable<YarnBlendMaster>>> GetYarnBlendMaster()
        //{
        //    return await _context.asptblyarblemas.ToListAsync();
        //}

        //// GET: api/YarnBlendMasters/5
        //[HttpGet("GetYarnBlendMaster/{id}")]
        //public async Task<ActionResult<YarnBlendMaster>> GetYarnBlendMaster(long id)
        //{
        //    var yarnBlendMaster = await _context.asptblyarblemas.FindAsync(id);

        //    if (yarnBlendMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return yarnBlendMaster;
        //}

   

        // POST: api/YarnBlendMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostYarnBlendMaster")]
        public async Task<ActionResult<YarnBlendMaster>> PostYarnBlendMaster(YarnBlendMaster yarnBlendMaster)
        {
            yarnBlendMaster.yarnblend = yarnBlendMaster.yarnblend.ToUpper();
            yarnBlendMaster.compcode = Class.Users.COMPCODE;
            yarnBlendMaster.Username = Class.Users.USERID;
            yarnBlendMaster.Ipaddress = GenFun.GetLocalIPAddress();
            yarnBlendMaster.Createdby = Class.Users.HUserName;
            yarnBlendMaster.Modifiedby = Class.Users.HUserName;
            yarnBlendMaster.Createdon = Convert.ToDateTime(DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
            yarnBlendMaster.Modifiedon = Convert.ToString(DateTime.Now.ToString());

            yarnBlendMaster.Modifiedby = Class.Users.HUserName;
            try
            {
                //if (yarnBlendMaster.asptblyarblemasid == 0)
                //{
                //   // _context.asptblyarblemas.Add(yarnBlendMaster);
                //    await _context.SaveChangesAsync();
                //    yarnBlendMaster.asptblyarblemasid =0;
                //}
                //else
                //{
                //    _context.Entry(yarnBlendMaster).State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!YarnBlendMasterExists(yarnBlendMaster.asptblyarblemasid))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return CreatedAtAction("PostYarnBlendMaster", new { id = yarnBlendMaster.asptblyarblemasid }, yarnBlendMaster);
        }

        // DELETE: api/YarnBlendMasters/5
        [HttpDelete("DeleteYarnBlendMaster/{id}")]
        public async Task<IActionResult> DeleteYarnBlendMaster(long id)
        {
            //try
            //{
            //    var yarnBlendMaster = await _context.asptblyarblemas.FindAsync(id);
            //    if (yarnBlendMaster == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.asptblyarblemas.Remove(yarnBlendMaster);
            //    await _context.SaveChangesAsync();
            //}
            //catch { }
            return CreatedAtAction("DeleteYarnBlendMaster", new { ID = id });
        }

        //private bool YarnBlendMasterExists(long id)
        //{
        //    return _context.asptblyarblemas.Any(e => e.asptblyarblemasid == id);
        //}
    }
}

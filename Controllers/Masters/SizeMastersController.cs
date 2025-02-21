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
    public class SizeMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public SizeMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/SizeMasters
        //[HttpGet("GetSizeMaster")]
        //public async Task<ActionResult<IEnumerable<SizeMaster>>> GetSizeMaster()
        //{
        //    return await _context.asptblsizmas.ToListAsync();
        //}

        //// GET: api/SizeMasters/5
        //[HttpGet("GetSizeMaster/{id}")]
        //public async Task<ActionResult<SizeMaster>> GetSizeMaster(long id)
        //{
        //    var sizeMaster = await _context.asptblsizmas.FindAsync(id);

        //    if (sizeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return sizeMaster;
        //}



        //// PUT: api/SizeMasters/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("PutSizeMaster/{id}")]
        //public async Task<IActionResult> PutSizeMaster(long id, SizeMaster sizeMaster)
        //{
        //    if (id != sizeMaster.asptblsizmasid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sizeMaster).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SizeMasterExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SizeMasters
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("PostSizeMaster")]
        //public async Task<ActionResult<SizeMaster>> PostSizeMaster(SizeMaster sizeMaster)
        //{
        //    sizeMaster.compcode = Class.Users.COMPCODE;
        //    sizeMaster.Username = Class.Users.USERID;
        //    sizeMaster.Ipaddress = GenFun.GetLocalIPAddress();
        //    sizeMaster.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //    sizeMaster.Createdby = Class.Users.HUserName;
        //    sizeMaster.Modifiedby = Class.Users.HUserName;
        //    if (sizeMaster.asptblsizmasid == 0)
        //    {

        //        _context.asptblsizmas.Add(sizeMaster);
        //        await _context.SaveChangesAsync();
        //        sizeMaster.asptblsizmasid = 0;
        //    }
        //    else
        //    {
        //        _context.Entry(sizeMaster).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }

        //    return  CreatedAtAction("PostSizeMaster", new { ID = sizeMaster.asptblsizmasid }, sizeMaster);
        //}

        //// DELETE: api/SizeMasters/5
        //[HttpDelete("DeleteSizeMaster/{id}")]
        //public async Task<IActionResult> DeleteSizeMaster(long id)
        //{
        //    var sizeMaster = await _context.asptblsizmas.FindAsync(id);
        //    if (sizeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblsizmas.Remove(sizeMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("DeleteSizeMaster", new { ID = sizeMaster.asptblsizmasid }, sizeMaster);
        //}

        //private bool SizeMasterExists(long id)
        //{
        //    return _context.asptblsizmas.Any(e => e.asptblsizmasid == id);
        //}
    }
}

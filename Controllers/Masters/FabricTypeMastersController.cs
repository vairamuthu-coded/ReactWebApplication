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
    public class FabricTypeMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public FabricTypeMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/FabricTypeMasters
        //[HttpGet("GetFabricTypeMaster")]
        //public async Task<ActionResult<IEnumerable<FabricTypeMaster>>> GetFabricTypeMaster()
        //{
        //    return await _context.asptblfabrictypemas.ToListAsync();
        //}

        //// GET: api/FabricTypeMasters/5
        //[HttpGet("GetFabricTypeMaster/{id}")]
        //public async Task<ActionResult<FabricTypeMaster>> GetFabricTypeMaster(long id)
        //{
        //    var fabricTypeMaster = await _context.asptblfabrictypemas.FindAsync(id);

        //    if (fabricTypeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return fabricTypeMaster;
        //}



        // POST: api/FabricTypeMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("PostFabricTypeMaster")]
        //public async Task<ActionResult<FabricTypeMaster>> PostFabricTypeMaster(FabricTypeMaster fabricTypeMaster)
        //{
        //    try
        //    {
        //        fabricTypeMaster.fabrictype = fabricTypeMaster.fabrictype.ToUpper();
        //        fabricTypeMaster.compcode = Class.Users.COMPCODE;
        //        fabricTypeMaster.Username = Class.Users.USERID;
        //        fabricTypeMaster.Ipaddress = GenFun.GetLocalIPAddress();
        //        fabricTypeMaster.Createdby = Class.Users.HUserName;
        //        fabricTypeMaster.Modifiedby = Class.Users.HUserName;
        //        fabricTypeMaster.Createdon = Convert.ToDateTime(DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //        fabricTypeMaster.Modifiedon = Convert.ToString(DateTime.Now.ToString());

        //        fabricTypeMaster.Modifiedby = Class.Users.HUserName;
        //        if (fabricTypeMaster.asptblfabrictypemasid == 0)
        //        {
        //            _context.asptblfabrictypemas.Add(fabricTypeMaster);
        //            await _context.SaveChangesAsync();
        //            fabricTypeMaster.asptblfabrictypemasid = 0;
        //        }
        //        else
        //        {
        //            _context.Entry(fabricTypeMaster).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FabricTypeMasterExists(fabricTypeMaster.asptblfabrictypemasid))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("PostFabricTypeMaster", new { id = fabricTypeMaster.asptblfabrictypemasid }, fabricTypeMaster);
        //}

        //// DELETE: api/FabricTypeMasters/5
        //[HttpDelete("DeleteFabricTypeMaster/{id}")]
        //public async Task<IActionResult> DeleteFabricTypeMaster(long id)
        //{
        //    var fabricTypeMaster = await _context.asptblfabrictypemas.FindAsync(id);
        //    if (fabricTypeMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblfabrictypemas.Remove(fabricTypeMaster);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool FabricTypeMasterExists(long id)
        //{
        //    return _context.asptblfabrictypemas.Any(e => e.asptblfabrictypemasid == id);
        //}
    }
}

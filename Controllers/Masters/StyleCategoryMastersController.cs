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
    public class StyleCategoryMastersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StyleCategoryMastersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/StyleCategoryMasters
        //[HttpGet("Getasptblstycatmas")]
        //public async Task<ActionResult<IEnumerable<StyleCategoryMaster>>> Getasptblstycatmas()
        //{
        //    return await _context.asptblstycatmas.ToListAsync();
        //}

        //// GET: api/StyleCategoryMasters/5
        //[HttpGet("GetStyleCategoryMaster/{id}")]
        //public async Task<ActionResult<StyleCategoryMaster>> GetStyleCategoryMaster(long id)
        //{
        //    var styleCategoryMaster = await _context.asptblstycatmas.FindAsync(id);

        //    if (styleCategoryMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return styleCategoryMaster;
        //}

       
    
        // POST: api/StyleCategoryMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostStyleCategoryMaster")]
        public async Task<ActionResult<StyleCategoryMaster>> PostStyleCategoryMaster(StyleCategoryMaster styleCategoryMaster)
        {
            try
            {
                styleCategoryMaster.compcode = Class.Users.COMPCODE;
                styleCategoryMaster.Username = Class.Users.USERID;
                styleCategoryMaster.Ipaddress = GenFun.GetLocalIPAddress();
                styleCategoryMaster.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                styleCategoryMaster.Createdby = Class.Users.HUserName;
                styleCategoryMaster.Modifiedby = Class.Users.HUserName;
                if (styleCategoryMaster.asptblstycatmasid == 0)
                {
                   // _context.asptblstycatmas.Add(styleCategoryMaster);
                  //  await _context.SaveChangesAsync();
                    styleCategoryMaster.asptblstycatmasid = 0;
                }
                else
                {
                   // _context.Entry(styleCategoryMaster).State = EntityState.Modified;
//
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!StyleCategoryMasterExists(styleCategoryMaster.asptblstycatmasid))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }


            return CreatedAtAction("GetStyleCategoryMaster", new { id = styleCategoryMaster.asptblstycatmasid }, styleCategoryMaster);
        }

        // DELETE: api/StyleCategoryMasters/5
       // [HttpDelete("DeleteStyleCategoryMaster/{id}")]
        //public async Task<IActionResult> DeleteStyleCategoryMaster(long id)
        //{
        //    var styleCategoryMaster = await _context.asptblstycatmas.FindAsync(id);
        //    if (styleCategoryMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblstycatmas.Remove(styleCategoryMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStyleCategoryMaster", new { ID = styleCategoryMaster.asptblstycatmasid }, styleCategoryMaster);
        //}

        //private bool StyleCategoryMasterExists(long id)
        //{
        //    return _context.asptblstycatmas.Any(e => e.asptblstycatmasid == id);
        //}
    }
}

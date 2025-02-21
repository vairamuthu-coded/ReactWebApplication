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
    public class StyleGroupMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public StyleGroupMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/StyleGroupMasters
        [HttpGet("GetStyleGroupMaster")]
        public async Task<ActionResult<IEnumerable<StyleGroupMaster>>> GetStyleGroupMaster()
        {
            //  return await _context.asptblstygrpmas.ToListAsync();
           string sel= "select a.asptblstygrpmasid,a.stylegroup,b.stylecategory,a.productstylegroup,a.shortcode,a.active from  asptblstygrpmas a join asptblstycatmas b on a.stylecategory=b.asptblstycatmasid order by 1;";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblstygrpmas");
           DataTable dt1 = ds1.Tables["asptblstygrpmas"];

            return new JsonResult(dt1);
        }

        // GET: api/StyleGroupMasters/5
        [HttpGet("GetStyleGroupMaster/{id}")]
        public async Task<ActionResult<StyleGroupMaster>> GetStyleGroupMaster(long id)
        {
            //var styleGroupMaster = await _context.asptblstygrpmas.FindAsync(id);

            //if (styleGroupMaster == null)
            //{
            //    return NotFound();
            //}

            //return styleGroupMaster;
            string sel = "select a.asptblstygrpmasid,a.stylegroup,b.stylecategory,b.asptblstycatmasid,a.productstylegroup,a.shortcode,a.active from  asptblstygrpmas a join asptblstycatmas b on a.stylecategory=b.asptblstycatmasid where a.asptblstygrpmasid='"+id+"';";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblstygrpmas");
            DataTable dt1 = ds1.Tables["asptblstygrpmas"];

            return new JsonResult(dt1);
        }



        // POST: api/StyleGroupMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostStyleGroupMaster")]
        public async Task<ActionResult<StyleGroupMaster>> PostStyleGroupMaster(StyleGroupMaster styleGroupMaster)
        {
            try
            {
                styleGroupMaster.compcode = Class.Users.COMPCODE;
                styleGroupMaster.Username = Class.Users.USERID;
                styleGroupMaster.Ipaddress = GenFun.GetLocalIPAddress();
                styleGroupMaster.Createdon =Convert.ToDateTime(System.DateTime.Now.ToString());
                styleGroupMaster.Createdby = Class.Users.HUserName;
                styleGroupMaster.Modifiedby = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                if (styleGroupMaster.asptblstygrpmasid == 0)
                {
                    //_context.asptblstygrpmas.Add(styleGroupMaster);
                    //await _context.SaveChangesAsync();
                    styleGroupMaster.asptblstygrpmasid = 0;
                }
                else
                {
                    //_context.Entry(styleGroupMaster).State = EntityState.Modified;
                    //await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!StyleGroupMasterExists(styleGroupMaster.asptblstygrpmasid))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
            return CreatedAtAction("PostStyleGroupMaster", new { id = styleGroupMaster.asptblstygrpmasid }, styleGroupMaster);
        }

        // DELETE: api/StyleGroupMasters/5
        //[HttpDelete("DeleteStyleGroupMaster/{id}")]
        //public async Task<IActionResult> DeleteStyleGroupMaster(long id)
        //{
        //    var styleGroupMaster = await _context.asptblstygrpmas.FindAsync(id);
        //    if (styleGroupMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblstygrpmas.Remove(styleGroupMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("DeleteStyleGroupMaster", new { id = styleGroupMaster.asptblstygrpmasid }, styleGroupMaster);

        //}

        //private bool StyleGroupMasterExists(long id)
        //{
        //    return _context.asptblstygrpmas.Any(e => e.asptblstygrpmasid == id);
        //}
    }
}

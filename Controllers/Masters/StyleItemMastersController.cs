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
    public class StyleItemMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public StyleItemMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/StyleItemMasters
        [HttpGet("GetStyleItemMaster")]
        public async Task<ActionResult<IEnumerable<StyleItemMaster>>> GetStyleItemMaster()
        {
         
            string sel = "select a.asptblstygrpmasid,a.stylegroup,b.stylecategory,a.productstylegroup,a.shortcode,a.active from  asptblstygrpmas a join asptblstycatmas b on a.stylecategory=b.asptblstycatmasid order by 1;";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblstygrpmas");
            DataTable dt1 = ds1.Tables["asptblstygrpmas"];

            return new JsonResult(dt1);
        }




        [HttpPost("PostStyleItemMaster")]
        public async Task<ActionResult<StyleItemMaster>> PostStyleItemMaster(StyleItemMaster styleItemMaster)
        {
            try
            {
                styleItemMaster.compcode = Class.Users.COMPCODE;
                styleItemMaster.Username = Class.Users.USERID;
                styleItemMaster.Ipaddress = GenFun.GetLocalIPAddress();
                styleItemMaster.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());
                styleItemMaster.Createdby = Class.Users.HUserName;
                styleItemMaster.Modifiedby = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());

                if (styleItemMaster.asptblstyleitemmasid == 0)
                {
                    //_context.asptblstyleitemmas.Add(styleItemMaster);
                  //  await _context.SaveChangesAsync();
                    styleItemMaster.asptblstyleitemmasid = 0;
                }
                else
                {
                   // _context.Entry(styleItemMaster).State = EntityState.Modified;
                  //  await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) {

                //if (!StyleItemMasterExists(styleItemMaster.asptblstyleitemmasid))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
            return CreatedAtAction("GetStyleItemMaster", new { id = styleItemMaster.asptblstyleitemmasid }, styleItemMaster);
        }

        // DELETE: api/StyleItemMasters/5
        [HttpDelete("DeleteStyleItemMaster/{id}")]
        public async Task<IActionResult> DeleteStyleItemMaster(long id)
        {
            //var styleItemMaster = await _context.asptblstyleitemmas.FindAsync(id);
            //if (styleItemMaster == null)
            //{
            //    return NotFound();
            //}

           // _context.asptblstyleitemmas.Remove(styleItemMaster);
          //  await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool StyleItemMasterExists(long id)
        //{
        //    //return _context.asptblstyleitemmas.Any(e => e.asptblstyleitemmasid == id);
        //}
    }
}

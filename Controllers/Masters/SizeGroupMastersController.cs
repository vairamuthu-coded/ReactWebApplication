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
    public class SizeGroupMastersController : ControllerBase
    {
        //private readonly AppDBContext _context;

        //public SizeGroupMastersController(AppDBContext context)
        //{
        //    _context = context;
        //}

        //int i, j = 1;

        [HttpGet("GetSizeGroupMaster")]
        public async Task<ActionResult<IEnumerable<SizeGroupMaster>>> GetSizeGroupMaster()
        {
            DataTable dt1 = new DataTable();
            string sel = "select a.asptblsizgrpid,a.sizegroup from asptblsizgrp a  order by 1";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblsizgrp");
            dt1 = ds1.Tables["asptblsizgrp"];

            return new JsonResult(dt1);
        }

        //// GET: api/SizeGroupMasters/5
        [HttpGet("GetSizeGroupMaster/{id}")]
        public async Task<ActionResult<SizeGroupMaster>> GetSizeGroupMaster(long id)
        {
            DataTable dt1 = new DataTable();
            string sel = "select b.asptblsizgrpDetid,a.asptblsizgrpid,c.asptblsizmasid,c.sizename, a.sizegroup,b.notes from asptblsizgrp a  join asptblsizgrpdet b on a.asptblsizgrpid=b.asptblsizgrpid  join asptblsizmas c on c.ASPTBLSIZMASID=b.sizename  where b.asptblsizgrpDetid='" + id + "'";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblsizgrp");
            dt1 = ds1.Tables["asptblsizgrp"];

            return new JsonResult(dt1);
        }

        [HttpGet("GetSizeMaster/{id}")]
        public async Task<ActionResult> GetSizeMaster(Int64 id)
        {

            DataTable dt1 = new DataTable();
            string sel = "select b.asptblsizgrpDetid,a.asptblsizgrpid,c.asptblsizmasid,c.sizename, a.sizegroup,b.notes from asptblsizgrp a  join asptblsizgrpdet b on a.asptblsizgrpid=b.asptblsizgrpid  join asptblsizmas c on c.ASPTBLSIZMASID=b.sizename  where b.asptblsizgrpid='" + id + "'";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblsizgrp");
            dt1 = ds1.Tables["asptblsizgrp"];

            return new JsonResult(dt1);
        }


        [HttpGet("GetSizeGroupDetMaster/{id}")]
        public async Task<ActionResult> GetSizeGroupDetMaster(long id)
        {

            DataTable dt1 = new DataTable();
            string sel = "select b.asptblsizgrpDetid,a.asptblsizgrpid,c.asptblsizmasid as sizename, a.sizegroup,b.notes from asptblsizgrp a  join asptblsizgrpdet b on a.asptblsizgrpid=b.asptblsizgrpid  join asptblsizmas c on c.ASPTBLSIZMASID=b.sizename  where b.asptblsizgrpid='" + id + "'";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblsizgrp");
            dt1 = ds1.Tables["asptblsizgrp"];

            return new JsonResult(dt1);
        }


        //// PUT: api/SizeGroupMasters/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("PutSizeGroupMaster/{id}")]
        //public async Task<IActionResult> PutSizeGroupMaster(long id, SizeGroupMaster sizeGroupMaster)
        //{
        //    if (id != sizeGroupMaster.asptblsizgrpid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sizeGroupMaster).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SizeGroupMasterExists(id))
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

        //// POST: api/SizeGroupMasters
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("PostSizeGroupMaster")]
        //public async Task<ActionResult<SizeGroupMaster>> PostSizeGroupMaster(SizeGroupMaster sizeGroupMaster)
        //{
        //    sizeGroupMaster.compcode = Class.Users.COMPCODE;
        //    sizeGroupMaster.Username = Class.Users.USERID;
        //    sizeGroupMaster.Ipaddress = GenFun.GetLocalIPAddress();
        //    sizeGroupMaster.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //    sizeGroupMaster.Createdby = Class.Users.HUserName;
        //    sizeGroupMaster.Modifiedby = Class.Users.HUserName;
        //    if (sizeGroupMaster.asptblsizgrpid == 0)
        //    {
        //        _context.asptblsizgrp.Add(sizeGroupMaster);
        //        await _context.SaveChangesAsync();

        //    }
        //    else
        //    {
        //        _context.Entry(sizeGroupMaster).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //    }

        //    return CreatedAtAction("GetSizeGroupMaster", new { id = sizeGroupMaster.asptblsizgrpid }, sizeGroupMaster);
        //}

        [HttpGet("GetMaxSizeGroupID")]
        public async Task<ActionResult> GetMaxSizeGroupID()
        {

            DataTable dt1 = new DataTable();
            string sel = "select max(a.asptblsizgrpid)+1 as asptblsizgrpid   from asptblsizgrp a  ;";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblsizgrp");
            dt1 = ds1.Tables["asptblsizgrp"];

            return new JsonResult(dt1);
        }


        //[HttpPost("PostSizeGroupDetMaster/{i}")]
        //public async Task<ActionResult> PostSizeGroupDetMaster(SizeGroupDetMaster sizeGroupDet,int i)
        //{
        //    string sel = "";
        //    if (sizeGroupDet.asptblsizgrpdetid == 0)
        //    {
        //        _context.asptblsizgrpdet.Add(sizeGroupDet);
        //        await _context.SaveChangesAsync();
        //        sizeGroupDet.asptblsizgrpdetid = 0;
        //        j = +i;
        //        sel = j.ToString() + "-" + Class.Users.insert;
        //    }
        //    else
        //    {
        //        _context.Entry(sizeGroupDet).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        j = +i;
        //        sel = j.ToString() + "-" + Class.Users.update;
        //    }

        //    return Ok(sel.ToString()) ;
        //}


        //// DELETE: api/SizeGroupMasters/5
        //[HttpDelete("DeleteSizeGroupMaster/{id}")]
        //public async Task<IActionResult> DeleteSizeGroupMaster(long id)
        //{
        //    string sel = "";
        //    var sizeGroupMaster = await _context.asptblsizgrp.FindAsync(id);
        //    var sizeGroupMaster1 = await _context.asptblsizgrpdet.FindAsync(id);
        //    if (sizeGroupMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblsizgrp.Remove(sizeGroupMaster);
        //    _context.asptblsizgrpdet.Remove(sizeGroupMaster1);
        //    await _context.SaveChangesAsync();

        //    j = +i;
        //    sel = j.ToString() + "-" + Class.Users.delete;
        //    return Ok(sel.ToString());
        //}

        //private bool SizeGroupMasterExists(long id)
        //{
        //    return _context.asptblsizgrp.Any(e => e.asptblsizgrpid == id);
        //}


        //[HttpGet("GetSizeGroupDetMaster")]
        //public async Task<ActionResult<IEnumerable<SizeGroupDetMaster>>> GetSizeGroupDetMaster()
        //{
        //    return await _context.asptblsizgrpdet.ToListAsync();
        //}


    }
}

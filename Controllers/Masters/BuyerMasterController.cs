using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ReactWebApplication.Data;
using ReactWebApplication.Models.Masters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ReactWebApplication.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerMasterController : ControllerBase
    {
      //  private readonly AppDBContext _context;
        private StringBuilder sel = new StringBuilder(); bool msg = false; public DataTable dataTable { get; set; }
        public DataSet ds { get; set; }
        //public BuyerMasterController(AppDBContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet("GetBuyerMaster")]
        //public async Task<ActionResult<IEnumerable<BuyerMasterModel>>> GetBuyerMaster()
        //{
        //    return await _context.asptblbuymas.ToListAsync();
        //}

        [HttpGet("BuyerMaster")]
        public async Task<IActionResult> BuyerMaster()
        {
            string sel = "select a.asptblbuymasid,a.buyercode,a.buyername,a.buyingagent,b.cityname , a.active from asptblbuymas a join gtcitymast b on a.city=b.gtcitymastid join gtstatemast c on c.gtstatemastid=a.state  join gtcountrymast d on d.Gtcountrymastid=a.country order by a.asptblbuymasid desc; ";
            ds = await Utility.ExecuteSelectQuery(sel, "gtcompmast");
            dataTable = ds.Tables["gtcompmast"];
            return new JsonResult(dataTable);
        }



        [HttpGet("BuyerMaster/{id}")]
        public async Task<IActionResult> BuyerMaster(Int64 id)
        {
            sel.Clear();
            //DataTable dt = null;
            //sel.Append("select a.asptblbuymasid,a.asptblbuymasid1,b.compcode,b.compname,a.buyercode,a.buyername,a.buyingagent, c.gtcitymastid,d.gtstatemastid,e.Gtcountrymastid,a.address,a.website,a.email,a.phoneno,a.pincode,a.contactname,a.active from asptblbuymas a left join gtcompmast b on a.compcode=b.gtcompmastid join gtcitymast c on a.city=c.gtcitymastid  join gtstatemast d on d.gtstatemastid=a.state  join gtcountrymast e on e.Gtcountrymastid=a.country  where  a.asptblbuymasid='" + id + "' ;");

            //MySqlCommand cmd = new MySqlCommand(sel.ToString(), Utility.con);
            //using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
            //{

            //    DataTable schemaTable = await reader.GetSchemaTableAsync();
            //    dt = new DataTable();
            //    foreach (DataRow row in schemaTable.Rows)
            //        dt.Columns.Add(row.Field<string>("ColumnName"), row.Field<Type>("DataType"));


            //    while (await reader.ReadAsync())
            //    {
            //        DataRow dr = dt.Rows.Add();
            //        foreach (DataColumn col in dt.Columns)
            //            dr[col.ColumnName] = reader[col.ColumnName];
            //    }
            //}
            //return new JsonResult(dt);
            sel.Append("select a.asptblbuymasid,a.asptblbuymasid1,b.gtcompmastid,a.buyercode,a.buyername,a.buyingagent, c.gtcitymastid,d.gtstatemastid,e.Gtcountrymastid,a.address,a.website,a.email,a.phoneno,a.pincode,a.contactname,a.active from asptblbuymas a left join gtcompmast b on a.compcode=b.gtcompmastid join gtcitymast c on a.city=c.gtcitymastid  join gtstatemast d on d.gtstatemastid=a.state  join gtcountrymast e on e.Gtcountrymastid=a.country  where  a.asptblbuymasid='" + id + "' ;");
            DataSet ds = await  Utility.ExecuteSelectQuery(sel.ToString(), "gtcompmast");
            DataTable dt = ds.Tables["gtcompmast"];
            return new JsonResult(dt);
        }



        [HttpDelete("BuyerMaster_Delete/{id}")]

        public async Task<JsonResult> BuyerMaster_Delete(int id)
        {
            string msg = string.Empty;
            sel.Clear();
            sel.Append("delete from asptblbuymas where asptblbuymasid=" + id);
            await Utility.ExecuteNonQuery(sel.ToString());
            msg = "delete";
            return new JsonResult(msg);
        }


        //[HttpPost("BuyerMaster_Insert")]
        //public async Task<IActionResult> BuyerMaster_Insert(BuyerMasterModel p)
        //{
        //    try
        //    {
        //        if (p.city > 0 && p.state > 0 && p.country > 0)
        //        {
        //            Class.Users.COMPCODE = 1;

        //            Class.Users.USERID = 1;
        //            p.Username = Class.Users.USERID;
        //            p.Ipaddress = GenFun.GetLocalIPAddress();
        //            p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //            p.Createdby = Class.Users.HUserName;
        //            p.Modifiedby = Class.Users.HUserName;
        //            if (p.asptblbuymasid == 0)
        //            {
        //                _context.asptblbuymas.Add(p);
        //              await  _context.SaveChangesAsync();

        //                sel.Append(Class.Users.insert);

        //            }
        //            else
        //            {
        //                sel.Clear();
        //                string up = "update  asptblbuymas  set  compcode='" + p.compcode + "',asptblbuymasid1='" + p.asptblbuymasid1 + "',buyercode='" + p.buyercode.ToUpper() + "',buyername='" + p.buyername.ToUpper() + "',buyingagent='" + p.buyingagent.ToUpper() + "',city='" + p.city + "', state='" + p.state + "',country='" + p.country + "' ,address='" + p.address + "' ,phoneno='" + p.phoneNo + "',email='" + p.email.ToUpper() + "',website='" + p.website.ToUpper() + "',contactname='" + p.contactname.ToUpper() + "', active='" + p.Active + "', modifiedby='" + Class.Users.HUserName + "',ipaddress='" + Class.Users.IPADDRESS + "'  where  asptblbuymasid='" + p.asptblbuymasid + "'";
        //              await  Utility.ExecuteNonQuery(up);
                        
        //                sel.Append(Class.Users.update);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sel.Clear();
        //        sel.Append(ex.Message);
        //    }

        //    return Ok(sel.ToString());
        //}




        //// GET: api/BuyerMaster
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<BuyerMasterModel>>> GetBuyerMasterModel()
        //{
        //    return await _context.asptblbuymas.ToListAsync();
        //}

        //// GET: api/BuyerMaster/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BuyerMasterModel>> GetBuyerMasterModel(int id)
        //{
        //    var buyerMasterModel = await _context.asptblbuymas.FindAsync(id);

        //    if (buyerMasterModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return buyerMasterModel;
        //}

        //// PUT: api/BuyerMaster/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBuyerMasterModel(int id, BuyerMasterModel buyerMasterModel)
        //{
        //    if (id != buyerMasterModel.asptblbuymasid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(buyerMasterModel).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BuyerMasterModelExists(id))
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

        //// POST: api/BuyerMaster
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<BuyerMasterModel>> PostBuyerMasterModel(BuyerMasterModel buyerMasterModel)
        //{
        //    _context.asptblbuymas.Add(buyerMasterModel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBuyerMasterModel", new { id = buyerMasterModel.asptblbuymasid }, buyerMasterModel);
        //}

        //// DELETE: api/BuyerMaster/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBuyerMasterModel(int id)
        //{
        //    var buyerMasterModel = await _context.asptblbuymas.FindAsync(id);
        //    if (buyerMasterModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblbuymas.Remove(buyerMasterModel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool BuyerMasterModelExists(int id)
        //{
        //    return _context.asptblbuymas.Any(e => e.asptblbuymasid == id);
        //}
    }
}

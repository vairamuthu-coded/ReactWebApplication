using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;
using System.Data;

namespace ReactWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessMasterController : ControllerBase
    {
        private readonly AppDBContext _context;
        public ProcessMasterController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProcess")]
        public async Task<IActionResult>  GetProcess()
        {
            // return await _context.asptblpromas.ToListAsync();
            DataTable dt;
            string sel1 = "select a.asptblpromasid, a.processname from asptblpromas a where a.active='T'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblpromas");
            dt = ds.Tables["asptblpromas"];
            return new JsonResult(dt);
        }

        [HttpPost]
        [Route("AddProcess")]
        public async Task<IActionResult> AddProcess(ProcessMasterModel processMasterModel)
        {
            bool msg = false;
            if (processMasterModel.asptblpromasid == 0)
            {
               
                
                if (processMasterModel.asptblpromasid == 0 && processMasterModel.processname != "")
                {
                    msg = true;
                    string sel= "insert into asptblpromas (processname,active)values('" + processMasterModel.processname.ToUpper() + "','" + processMasterModel.active.ToUpper() + "')";
                    await Utility.ExecuteNonQuery(sel);
                
                }
                
              
               
            }
            if (processMasterModel.asptblpromasid >= 1)
            {
                msg = true;
                string sel = "update  asptblpromas  set processname='" + processMasterModel.processname.ToUpper() + "',active='" + processMasterModel.active.ToUpper() + "' where asptblpromasid=" + processMasterModel.asptblpromasid;
                await Utility.ExecuteNonQuery(sel);

            }

            return Ok(msg);
        }

        [HttpDelete]
        [Route("ProcessMaster_Delete/{id}")]
        public bool ProcessMaster_Delete(int id)
        {
            bool aa = false;
            //var processMaster = _context.asptblpromas.Find(id);
            //if (processMaster != null)
            //{
            //    aa = true;

            //    _context.Entry(asptblpromas).State = EntityState.Deleted;
            //    _context.SaveChanges();
            //}
            //else
            //{
            //    aa = false;
            //}
            return aa;
        }
    }
}

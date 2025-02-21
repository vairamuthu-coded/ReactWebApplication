using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Models.Masters;
using System.Data;

namespace ReactWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorMasterController : ControllerBase
    {
        [HttpGet]
        [Route("GetColor")]
        public async Task<IActionResult> GetColor()
        {
            string sel = "select a.asptblcolmasid ,a.colorname, a.active  from asptblcolmas a order by a.asptblcolmasid desc ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblcolmas");
            DataTable dt = ds.Tables["asptblcolmas"];
            return new JsonResult(dt);
        }

        [HttpGet]
        [Route("GetActiveColor")]
        public async Task<IActionResult> GetActiveColor()
        {

            string sel = "select a.asptblcolmasid ,a.colorname, a.active  from asptblcolmas a where a.active='T' order by a.asptblcolmasid desc ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblcolmas");
            DataTable dt = ds.Tables["asptblcolmas"];
            return new JsonResult(dt);

        }

        [HttpGet("GetColorMasterID/{color}")]
        public async Task<ActionResult<ColorMaster>> GetColorMasterID(string color)
        {
            string sel = "select a.asptblcolmasid from asptblcolmas a where a.colorname='" + color + "' ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblcolmas");
            DataTable dt = ds.Tables["asptblcolmas"];
            return new JsonResult(dt);
        }

        [HttpPost]
        [Route("ColorMasterInsertUpdate")]
        public async Task<IActionResult> ColorMasterInsertUpdate(ColorMaster processMasterModel)
        {
            bool msg = false;
            if (processMasterModel.asptblcolmasid == 0)
            {

                if (processMasterModel.asptblcolmasid == 0 && processMasterModel.colorname != "")
                {
                    msg = true;
                    string sel = "insert into asptblcolmas (colorname,active)values('" + processMasterModel.colorname.ToUpper() + "','" + processMasterModel.active.ToUpper() + "')";
                   await  Utility.ExecuteNonQuery(sel);

                }



            }
            if (processMasterModel.asptblcolmasid >= 1)
            {
                msg = true;
                string sel = "update  asptblcolmas  set colorname='" + processMasterModel.colorname.ToUpper() + "',active='" + processMasterModel.active.ToUpper() + "' where asptblcolmastid=" + processMasterModel.asptblcolmasid;
                await Utility.ExecuteNonQuery(sel);

            }

            return Ok(msg);
        }

    }
}

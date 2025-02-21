using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.TreeView;
using System.Data;
using System.Text;
using static ReactWebApplication.Class;

namespace ReactWebApplication.Controllers.TreeView
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuNameMasterController : ControllerBase
    {
        private StringBuilder sel = new StringBuilder(); bool msg = false;
        //private readonly AppDBContext _context;
        //public MenuNameMasterController(AppDBContext context)
        //{
        //    _context = context;
        //}

        [HttpGet("MenuNameMaster")]
        public  async Task<ActionResult<IEnumerable<MenuNameMaster>>> MenuNameMaster()
        {

            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.menunameid,a.menuname,a.parentmenuid,a.aliasname,a.active from asptblmenuname  a order by 1 ;");
            DataSet ds1 =await Utility.ExecuteSelectQuery(sel.ToString(), "asptblmenuname");
            dt1 = ds1.Tables["asptblmenuname"];

            return new JsonResult(dt1);
        }

        [HttpGet("MenuNameMasterData")]
        public async Task<JsonResult> MenuNameMasterData()
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.menunameid,a.menuname from asptblmenuname  a where a.active='T'  ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptblmenuname");
            dt1 = ds1.Tables["asptblmenuname"];
          
             return new JsonResult(dt1);

        }

        [HttpPost]
        [Route("MenuNameMasterInsertUpdate")]
        public IActionResult MenuNameMasterInsertUpdate(MenuNameMaster p)
        {
            sel.Clear();
            try
            {

                p.Username = Class.Users.USERID;
                p.Ipaddress = GenFun.GetLocalIPAddress();
                p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                p.Createdby = Class.Users.HUserName;
                p.Modifiedby = Class.Users.HUserName;
                p.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());

                var result = 0;// _context.asptblmenuname.AsEnumerable().Where(x => x.menuname == p.menuname && x.aliasname == p.aliasname && x.parentmenuid == p.parentmenuid && x.active == p.active).Count();
                if (result > 0)
                {
                    sel.Append(Class.Users.child);
                }
                else if (result <= 0 && p.Menunameid == 0)
                {
                    sel.Clear();
                    p.InsertCommond();
                    sel.Append(Class.Users.insert);
                }
                else
                {

                    p.UpdateCommond();
                    sel.Clear();
                    sel.Append(Class.Users.update);
                }
            }
            catch (Exception ex)
            {
                sel.Clear();
                sel.Append(ex.Message);

            }
            return Ok(sel.ToString());

        }

        [HttpDelete]
        [Route("MenuNameMaster/{id}")]
        public bool MenuNameMaster(long id)
        {
            bool aa = false;
            //var menunameMaster = _context.asptblmenuname.Find(id);

            //if (menunameMaster != null)
            //{
            //    aa = true;
            //    _context.Entry(menunameMaster).State = EntityState.Deleted;
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

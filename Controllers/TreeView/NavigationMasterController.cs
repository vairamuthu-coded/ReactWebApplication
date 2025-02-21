using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models.TreeView;
using System.Data;
using System.Text;

namespace ReactWebApplication.Controllers.TreeView
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationMasterController : ControllerBase
    {
        private StringBuilder sel = new StringBuilder(); bool msg = false;
        private readonly AppDBContext _context; int i = 1;
        public NavigationMasterController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("NavigationMaster")]
        public async Task<IActionResult> NavigationMaster()
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select  a.menuid,a.menuname,b.menuname as menunameid,a.navurl,a.parentmenuid,a.active,c.compcode,d.username from asptblnavigation a  join asptblmenuname b on a.menunameid=b.menunameid join gtcompmast c on c.gtcompmastid=a.compcode join asptblusermas d on d.userid=a.username order by a.menuid asc ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptblnavigation");
            dt1 = ds1.Tables["asptblnavigation"];

            return new JsonResult(dt1);
           
        }



        [HttpGet("NavigationMaster/{id}")]
        public async Task<IActionResult> NavigationMaster(Int64 id)
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.menuid,a.menuname,b.menuname as menunameid,a.navurl,a.parentmenuid,a.active,c.compcode,d.username from asptblnavigation a  join asptblmenuname b on a.menunameid=b.menunameid join gtcompmast c on c.gtcompmastid=a.compcode join asptblusermas d on d.userid=a.username  where a.menuid='" + id + "' ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptblnavigation");
            dt1 = ds1.Tables["asptblnavigation"];
            return new JsonResult(dt1);
        }

        [HttpGet]
        [Route("NavigationMasterData/{id}")]
        public async Task<IActionResult> NavigationMasterData(Int64 id)
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.menuid,a.menuname from asptblnavigation  a where a.menuid='"+id+"'  ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptblnavigation");
            dt1 = ds1.Tables["asptblnavigation"];
            return new JsonResult(dt1);             
        }

        //[HttpPost]
        //[Route("NavigationMasterInsertUpdate")]
        //public async Task<IActionResult> NavigationMasterInsertUpdate(NavigationMaster  navigationMaster)
        //{
        //    string sel = "";
        //    try
        //    {

        //        navigationMaster.Ipaddress = GenFun.GetLocalIPAddress();
        //        navigationMaster.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //        navigationMaster.Createdby = Class.Users.HUserName;
        //        navigationMaster.Modifiedby = Class.Users.HUserName;
        //        navigationMaster.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());

        //        var result = 0;
        //        //_context.asptblnavigation.AsEnumerable().Where(x => x.menuname == navigationMaster.menuname && x.menunameid == navigationMaster.menunameid 
        //        //&&  x.navurl == navigationMaster.navurl && x.parentmenuid == navigationMaster.parentmenuid
        //        //&& x.compcode == navigationMaster.compcode
        //        //&& x.username == navigationMaster.username && x.active == navigationMaster.active).Count();
        //        //if (result > 0)
        //        //{
        //        //    sel=Class.Users.child;
        //        //}
        //        if (navigationMaster.menuid == 0)
        //        {
        //            _context.asptblnavigation.Add(navigationMaster);
        //          await  _context.SaveChangesAsync();
        //            sel =Class.Users.insert;
        //        }
        //        else
        //        {
  
        //           string sel1="update  asptblnavigation SET menuname='" + navigationMaster.menuname + "' ,menunameid='" + navigationMaster.menunameid + "',navurl='" + navigationMaster.navurl + "', parentmenuid='" + navigationMaster.parentmenuid + "' ,compcode='" + navigationMaster.compcode + "',username='" + navigationMaster.Username + "',active='" + navigationMaster.Active + "' where menuid='" + navigationMaster.menuid + "'";
        //          await  Utility.ExecuteNonQuery(sel1);
                 
        //            sel=Class.Users.update;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        sel=ex.Message;

        //    }
        //    return Ok(sel);

        //}

      

        //[HttpDelete]
        //[Route("NavigationMaster_Delete/{id}")]
        //public async Task<IActionResult> NavigationMaster_Delete(long id)
        //{
        //    bool aa = false;
        //    var menunameMaster = _context.asptblnavigation.Find(id);
        //    if (menunameMaster != null)
        //    {
        //        aa = true;
        //        _context.Entry(menunameMaster).State = EntityState.Deleted;
        //      await  _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        aa = false;
        //    }
        //    return Ok(aa);

        //}

        //  Asptbluserrights user = new Asptbluserrights();
        // DataTable dt2 = (DataTable)JsonConvert.DeserializeObject(user.ToString(), (typeof(DataTable)));

        //user.ipaddress = GenFun.GetLocalIPAddress();
        //user.createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
        //user.createdby = Class.Users.HUserName;
        //user.modifiedby = Class.Users.HUserName;
        //user.modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());

        //string result = "select menuname  from asptbluserrights where menuid='" + user.menuid + "' and menuname='" + user.menuname + "' and navurl='" + user.navurl + "' and  parentmenuid='" + user.parentmenuid + "' and active='" + user.active + "' and news='" + user.news + "' and saves='" + user.saves + "' and  prints='" + user.prints + "' and  readonlys='" + user.readonlys + "' and  search='" + user.search + "' and  deletes='" + user.deletes + "' and treebutton='" + user.treebutton + "' and  globalsearch='" + user.globalsearch + "' and login='" + user.login + "' and changepassword='" + user.changepassword + "' and changeskin='" + user.changeskin + "' and download='" + user.download + "'and  contact='" + user.contact + "'and  pdf='" + user.pdf + "' and imports='" + user.imports + "' and  compcode='" + user.compcode + "' and  username='" + user.username + "' ;";
        //DataSet ds1 = Utility.ExecuteSelectQuery(result, "asptbluserrights");
        //DataTable dt1 = ds1.Tables["asptbluserrights"];


    }
}

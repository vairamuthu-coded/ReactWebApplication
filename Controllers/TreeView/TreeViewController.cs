using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebApplication.Data;
using ReactWebApplication.Models.TreeView;
using System.Data;

namespace ReactWebApplication.Controllers.TreeView
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeViewController : ControllerBase
    {
        private readonly AppDBContext _context; int i = 1; int j = 1; Models.TreeView.userRights sm = new Models.TreeView.userRights();
        public TreeViewController(AppDBContext context)
        {
            _context = context;
            Class.Users.ScreenName = "TreeViewMaster";
        }

        [HttpGet("usercheck")]
        public async Task<IActionResult> usercheck()
        {
            DataTable dt1 = await sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

            return Ok(dt1);

        }

        //Int64 parentID = 0; TreeNode parentNode = null;
        //private void treeload()
        //{
        //    string sel = "select a.userrightsid, a.menuid,  e.aliasname as  menuname ,a.parentmenuid  from  asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username    join asptblnavigation d on d.menuid=a.menuid   join asptblmenuname  e on e.menunameid=d.menunameid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'  and  a.active='T' and a.parentmenuid = 1 order by 1";// and a.parentmenuid = 1
        //    DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //    DataTable dt = ds.Tables["asptbluserrights"];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        parentNode = treeView1.Nodes.Add(dr["menuname"].ToString());
        //        PopulateTreeView(Convert.ToInt64(dr["menuid"].ToString()), parentNode);

        //    }
        //}

        //private void PopulateTreeView(Int64 parentId, TreeNode parentNode)
        //{

        //    DataTable dtchildc = sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, parentId);

        //    TreeNode childNode;
        //    foreach (DataRow dr in dtchildc.Rows)
        //    {
        //        Models.UserRights sm1 = new Models.UserRights();
        //        if (parentNode == null)
        //        {
        //            childNode = treeView1.Nodes.Add(dr["menuname"].ToString());

        //        }
        //        else
        //        {
        //            childNode = parentNode.Nodes.Add(dr["menuname"].ToString());

        //        }
        //        PopulateTreeView(Convert.ToInt32("0" + dr["menuid"].ToString()), childNode);
        //        sm1.MenuName = dr["menuname"].ToString();
        //        usr.Add(sm1);

        //    }
        //}



        [HttpGet("TreeViewMaster")]
        public async Task<IActionResult> TreeViewMaster()
        {
            string sel = "";
            DataTable dt1 = new DataTable();
            sel = "select ifnull('0',null) as userrightsid, a.menuid,a.menuname,b.menuname as menunameid,a.navurl,a.parentmenuid,a.active,c.compcode,d.username from asptblnavigation a  join asptblmenuname b on a.menunameid=b.menunameid join gtcompmast c on c.gtcompmastid=a.compcode join asptblusermas d on d.userid=a.username order by a.menuid asc ;";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblnavigation");
            dt1 = ds1.Tables["asptblnavigation"];
            return new JsonResult(dt1);
        }

        [HttpPost]
        [Route("TreeViewInsertUpdate/{i}")]
        public async Task<IActionResult> TreeViewInsertUpdate(UserRightsModel user, int i)
        {

            string sel = "";
            try
            {
                user.Ipaddress = GenFun.GetLocalIPAddress();
                user.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToShortTimeString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                user.Createdby = Class.Users.HUserName;
                user.Modifiedby = Class.Users.HUserName;
                user.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToShortTimeString());//&& x.menuname == user.menuname && x.parentmenuid == user.parentmenuid 

      
                //string sel0 = "select menuname  from asptbluserrights where menuid='" + user.menuid + "' and menuname='" + user.menuname + "' and  parentmenuid='" + user.parentmenuid + "'   and  compcode='" + user.compcode + "' and  username='" + user.username + "' ;";
                //DataSet ds1 = await Utility.ExecuteSelectQuery(sel0, "asptbluserrights");
                //DataTable dt1 = ds1.Tables["asptbluserrights"];
                //if (dt1.Rows.Count != 0) {  j = +i; sel = j.ToString() + "-" + Class.Users.child; }
                 if (user.userrightsid == 0)
                {
                    
                  //  _context.asptbluserrights.Add(user);       
                    await _context.SaveChangesAsync();
                    j = +i;
                    sel = j.ToString() + "-" + Class.Users.insert;
                }
                else
                {
                   // _context.Entry(user).State = EntityState.Modified;
                    try
                    {
                       // await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                    }
                    //string up = "update  asptbluserrights SET menuid='" + user.menuid + "' , menuname='" + user.menuname + "' ,navurl='" + user.navurl + "', parentmenuid='" + user.parentmenuid + "' ,active='" + user.active + "',news='" + user.news + "',saves='" + user.saves + "',prints='" + user.prints + "',readonlys='" + user.readonlys + "',search='" + user.search + "' ,deletes='" + user.deletes + "',treebutton='" + user.treebutton + "',globalsearch='" + user.globalsearch + "' ,login='" + user.login + "',changepassword='" + user.changepassword + "',changeskin='" + user.changeskin + "' ,download='" + user.download + "',contact='" + user.contact + "',pdf='" + user.pdf + "',imports='" + user.imports + "',compcode=" + user.compcode + ",username=" + user.username + " where userrightsid='" + user.userrightsid + "';";
                    //Utility.ExecuteNonQuery(up);
                   
                    j = +i;
                    sel = j.ToString() + "-" + Class.Users.update;
                }

            }
            catch (Exception ex) { sel = ex.Message; }

            return Ok(sel.ToString());
        }
    }
}

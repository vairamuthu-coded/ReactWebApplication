using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class UserRightsController : ControllerBase
    {
        private StringBuilder sel = new StringBuilder(); bool msg = false;
        private readonly AppDBContext _context; int i = 1; int j = 1;
        Models.TreeView.userRights sm = new Models.TreeView.userRights();
        public UserRightsController(AppDBContext context)
        {
            _context = context;
            Class.Users.ScreenName = "UserRights";
        }
        [HttpGet("usercheck")]
        public async Task<IActionResult> usercheck()
        {
            DataTable dt1 = await sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);

            return Ok(dt1);

        }

        [HttpGet("UserRightsCheck/{user}/{pass}")]
        public async Task<IActionResult> UserRightsCheck(string user, string pass)
        {
            bool chk = false;
            string SS = ""; Models.TreeView.userRights sm = new Models.TreeView.userRights();
            SS = sm.Encrypt(pass);
            Class.Users.PWORD = SS;
            string sel = "select  distinct a.compcode as cc ,b.userid, b.username ,a.compname ,b.gatename,a.gtcompmastid ,b.sessiontime  from gtcompmast  a join asptblusermas b on a.gtcompmastid = b.compcode   and a.compcode='" + Class.Users.HCompcode + "'      and b.username='" + user + "' and b.pasword = '" + SS + "' and  b.active='T'  order by 1";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblusermas");
            DataTable dt = ds.Tables["asptblusermas"];

            if (dt.Rows.Count > 0)
            {
                Class.Users.HUserName = dt.Rows[0]["username"].ToString();
                Class.Users.USERID = Convert.ToInt64(dt.Rows[0]["userid"].ToString());
                Class.Users.HGateName = dt.Rows[0]["gatename"].ToString();
                Class.Users.HCompName = dt.Rows[0]["compname"].ToString();
                Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["gtcompmastid"].ToString());
                chk = true;

            }

            return new JsonResult(chk);

        }

        [HttpGet("UserRightsCheck/{com}/{user}/{pass}")]
        public async Task<IActionResult> UserRightsCheck(string com, string user,string pass)
        {
            bool chk = false;
            string SS = ""; Models.TreeView.userRights sm = new Models.TreeView.userRights();
            SS =sm.Encrypt(pass);
            Class.Users.PWORD = SS; 
            string sel = "select  distinct a.compcode as cc ,b.userid, b.username ,a.compname ,b.gatename,a.gtcompmastid ,b.sessiontime  from gtcompmast  a join asptblusermas b on a.gtcompmastid = b.compcode   and a.compcode='" + com + "'      and b.username='" + user + "' and b.pasword = '" + SS + "' and  b.active='T'  order by 1";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblusermas");
            DataTable dt = ds.Tables["asptblusermas"];

            if (dt.Rows.Count > 0)
            {
                Class.Users.HUserName = dt.Rows[0]["username"].ToString();
                Class.Users.USERID = Convert.ToInt64(dt.Rows[0]["userid"].ToString());
                Class.Users.HGateName = dt.Rows[0]["gatename"].ToString();
                Class.Users.HCompName = dt.Rows[0]["compname"].ToString();
                Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["gtcompmastid"].ToString());
                chk = true;

            }

                return new JsonResult(chk);

        }

        [HttpGet("UserRightsDetails")]
        public async Task<IActionResult> UserRightsDetails()
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.gtcompmastid as compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid=a.menuid join asptblmenuname e on e.menunameid=d.menunameid where b.compcode='"+Class.Users.HCompcode+ "' and a.username='"+Class.Users.USERID+"' order by 5,2 ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
            dt1 = ds1.Tables["asptbluserrights"];

            return new JsonResult(dt1);

        }

        [HttpGet("userrightsMenuCheck/{com}/{user}/{screen}")]
        public async Task<IActionResult> userrightsMenuCheck(string com,string user,string screen)
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.gtcompmastid as compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid=a.menuid join asptblmenuname e on e.menunameid=d.menunameid where b.compcode='" + com + "' and c.username='" + user + "'  and e.menuname='" + screen + "' order by 5,2 ;");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
            dt1 = ds1.Tables["asptbluserrights"];

            return new JsonResult(dt1);

        }

        [HttpGet("UserRightsDetails/{id}")]
        public async Task<IActionResult> UserRightsDetails(Int64 id)
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.gtcompmastid as compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid=a.menuid join asptblmenuname e on e.menunameid=d.menunameid   where a.userrightsid='" + id + "' order by 6,2");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
            dt1 = ds1.Tables["asptbluserrights"];

            return new JsonResult(dt1);

        }

        [HttpGet("UserRightsFilter/{id}")]
        public async Task<IActionResult> UserRightsFilter(Int64 id)
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.userrightsid,a.menuid,a.menuname,e.menuname as menunameid, a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.gtcompmastid as compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid=a.menuid join asptblmenuname e on e.menunameid=d.menunameid   where a.username='" + id + "' order by 6,2 ");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
            dt1 = ds1.Tables["asptbluserrights"];

            return new JsonResult(dt1);

        }

        [HttpGet("menuname")]
        public async Task<IActionResult> menuname()
        {
            sel.Clear(); DataTable dt1 = new DataTable();
            sel.Append("select a.menuid,d.menuname,a.navurl,a.parentmenuid from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid=a.menuid join asptblmenuname e on e.menunameid=d.menunameid where a.parentmenuid>1 and a.compcode='"+Class.Users.COMPCODE+"' and a.username='"+Class.Users.USERID+"' order by 1 ");
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
            dt1 = ds1.Tables["asptbluserrights"];

            return new JsonResult(dt1);

        }


        





        [HttpPost]
        [Route("UserRightsInsertUpdate/{i}")]
        public async Task<IActionResult> UserRightsInsertUpdate(UserRightsModel user, int i)
        {

            string sel = "";
            try
            {
                user.Ipaddress = GenFun.GetLocalIPAddress();
                user.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToShortTimeString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                user.Createdby = Class.Users.HUserName;
                user.Modifiedby = Class.Users.HUserName;
                user.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToShortTimeString());//&& x.menuname == user.menuname && x.parentmenuid == user.parentmenuid 

           
                //string sel0 = "select menuname  from asptbluserrights where menuid='" + user.menuid + "' and menuname='" + user.menuname + "' and  parentmenuid='" + user.parentmenuid + "'  and active='" + user.active + "' and news='" + user.news + "' and saves='" + user.saves + "' and prints='" + user.prints + "' and readonlys='" + user.readonlys + "' and search='" + user.search + "' and deletes='" + user.deletes + "' and treebutton='" + user.treebutton + "'  and globalsearch='" + user.globalsearch + "'  and login='" + user.login + "' and changepassword='" + user.changepassword + "' and changeskin='" + user.changeskin + "'  and download='" + user.download + "' and contact='" + user.contact + "' and pdf='" + user.pdf + "' and imports='" + user.imports + "' and  compcode='" + user.compcode + "' and  username='" + user.username + "' ;";
                //DataSet ds1 = Utility.ExecuteSelectQuery(sel0, "asptbluserrights");
                //DataTable dt1 = ds1.Tables["asptbluserrights"];
        
                //if (dt1.Rows.Count > 0) { 
                //     j = +i;  sel = j.ToString() + "-" + Class.Users.child;
                //}
                if (Convert.ToInt32("0" + user.userrightsid) == 0 || user.userrightsid==0)
                {                  
                   // _context.asptbluserrights.Add(user);            
                    await _context.SaveChangesAsync();
                    j = +i;
                    sel = j.ToString() + "-" + Class.Users.insert;
                }
                else
                {
                   // _context.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
      

                    j = +i;
                    sel = j.ToString() + "-" + Class.Users.update;
                }

            }
            catch (Exception ex) { sel = ex.Message; }

            return Ok(sel.ToString());
        }


        [HttpDelete]
        [Route("UserRights_Delete/{i}/{ID}/{COM}/{USER}")]
        public async Task<IActionResult> UserRights_Delete(int i, Int64 ID, Int64 COM,Int64 USER)
        {
            string sel = "";
            try
            {
                await Utility.ExecuteNonQuery("DELETE FROM asptbluserrights a WHERE a.userrightsid ='" + ID + "' AND a.username='" + USER + "'  and  a.compcode='" + COM + "' ;");
                j = +i;
                sel = j.ToString() + "-" + Class.Users.delete;
            }
            catch(Exception ex) { sel = ex.Message;  }
            return Ok(sel.ToString());
        }


        //[HttpDelete]
        //[Route("UserRightsMaster_Delete/{users}")]
        //public bool UserRightsMaster_Delete(string users)
        //{
        //    bool aa = false;
        //    Asptbluserrights user = new Asptbluserrights();
        //    DataTable dt2 = (DataTable)JsonConvert.DeserializeObject(users, (typeof(DataTable)));
        //    for (int i = 0; i < dt2.Rows.Count; i++)
        //    {
        //        aa = false;
        //        DataRow dr = dt2.Rows[i];
        //        user.userrightsid = Convert.ToInt64("0" + dr["userrightsid"].ToString());
        //        aa = Utility.ExecuteNonQuery("DELETE FROM asptbluserrights WHERE userrightsid ='" + user.userrightsid + "';");
        //    }
        //    return aa;

        //}

    }
}

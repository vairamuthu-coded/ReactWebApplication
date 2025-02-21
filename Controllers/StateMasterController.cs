using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using NuGet.Protocol.Core.Types;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;

using System.Data;
using System.Text;

namespace ReactWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateMasterController : ControllerBase,InterfaceClass
    {
        private readonly AppDBContext _context;
       // private readonly DbSet<StateMaster> _entities;
        public static StateMaster Instance => (StateMaster)(GlobalVariables.CurrentForm = "StateMaster");
        public StateMasterController(AppDBContext context)
        {
            _context = context;

        }
        StringBuilder sel = new StringBuilder();
        StateMaster ss = new StateMaster();
        bool msg = false;
        

        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {
            
            DataTable dt = await ss.GridLoad();
            return new JsonResult(dt);
        }
      

        [HttpGet("SelectCommond")]
        public async Task<IActionResult> SelectCommond()
        {

            DataTable dt = await ss.StateGridLoad();
            return new JsonResult(dt);

        }
        
        [HttpGet("GridLoad/{id}")]
        public async Task<IActionResult> GridLoad(Int64 id)
        {
            //sel.Append("select b.gtstatemastid,b.statename ,  a.Gtcountrymastid , a.Countryname ,b.active from gtcountrymast a join gtstatemast b on a.Gtcountrymastid=b.country   where  b.gtstatemastid='" + id + "' ;");
            //DataSet dscountry = await Utility.ExecuteSelectQuery(sel.ToString(), "gtstatemast");
            //DataTable dtcountry = dscountry.Tables["gtstatemast"];

            DataTable dt = await ss.GridLoad(id);
            return new JsonResult(dt);
        }



        [HttpPost("Saves")]
        public async Task<IActionResult> Saves(StateMaster cou)
        {
            
            try
            {
                cou.Statename = cou.Statename.ToUpper();    
                cou.Username = Class.Users.USERID;
                cou.Ipaddress = GenFun.GetLocalIPAddress();
                cou.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                cou.Createdby = Class.Users.HUserName;
                cou.Modifiedby = Class.Users.HUserName;

                cou.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                // _context.gtstatemast.AsEnumerable().Where(x => x.statename == cou.statename && x.country == cou.country && x.active == cou.active).Count();
                DataTable dt = await cou.SelectCommond();
                if (dt.Rows.Count != 0)
                {
                   sel.Append(Class.Users.child);
                }
                else if (dt.Rows.Count != 0 && cou.Gtstatemastid == 0 || cou.Gtstatemastid == 0)
                {
                    await cou.InsertCommond();
                    sel.Append(Class.Users.insert);

                }
                else
                {

                    sel.Clear();
                   await cou.UpdateCommond();
                    sel.Clear();
                    sel.Append(Class.Users.update);
                }
            }
           catch(Exception ex)
            {
                sel.Clear();
                sel.Append(ex.Message);
            }
          
            return Ok(sel.ToString());
        }



        [HttpDelete]
        [Route("DeleteCommond/{id}")]
        public Task<IActionResult> DeleteCommond(Int64 id)
        {
            //bool aa = false;
            //var stateMaster = 0;// _context.gtstatemast.Find(id);

            //if (stateMaster != null)
            //{
            //    aa = true;
            //  //  _context.Entry(stateMaster).State = EntityState.Deleted;
            //  //  _context.SaveChanges();
            //}
            //else
            //{
            //    aa = false;
            //}
            //return aa;
            throw new NotImplementedException();
        }


    
        [NonAction]
        public Task<IActionResult> GridLoad(string id)
        {
            throw new NotImplementedException();
        }

   


        //public void News()
        //{
        //    Ok("News-Statemaster");
        //}

        //public void Saves()
        //{
        //    Ok("Saves-Statemaster");
        //}

        //public void Prints()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Searchs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Searchs(int EditID)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Deletes()
        //{
        //    throw new NotImplementedException();
        //}

        //public void ReadOnlys()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Imports()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Pdfs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void ChangePasswords()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DownLoads()
        //{
        //    throw new NotImplementedException();
        //}

        //public void ChangeSkins()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Logins()
        //{
        //    throw new NotImplementedException();
        //}

        //public void GlobalSearchs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void TreeButtons()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Exit()
        //{
        //    throw new NotImplementedException();
        //}

        //public void GridLoad()
        //{
        //    throw new NotImplementedException();
        //}
        //public async Task<StateMaster> GetStudentById(long id)
        //{
        //    return await _context.gtstatemast.AsNoTracking()
        //                .FirstOrDefaultAsync(student => student.gtstatemastid == id);
        //}
        //private bool ProductExists(long id)
        //{
        //    return _context.gtstatemast.Any(e => e.gtstatemastid == id);
        //}

    }
}

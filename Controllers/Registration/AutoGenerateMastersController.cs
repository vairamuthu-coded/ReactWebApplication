using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Registration;
using static ReactWebApplication.Class;

namespace ReactWebApplication.Controllers.Registration
{


    [Route("api/[controller]")]
    [ApiController]
    public class AutoGenerateMastersController : MainClass
    {
        private readonly AppDBContext _context;
        string menuname = "";
        AutoGenerateMaster  ag=new AutoGenerateMaster();

        public AutoGenerateMastersController(AppDBContext context)
        {
            _context = context;
        }


        [HttpGet("autonumberload/{finyear}/{compcode}/{shortcode}/{screen}")]
        public async Task<DataTable> autonumberload(string year, string com, string scode, Int64 sc)
        {
            DataTable dt = new DataTable();
            try
            {

                string query = "select max(a.asptblautogeneratemasid) as  id from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid  where  a.finyear='" + year + "' and b.compcode ='" + com + "' and a.shortcode ='" + scode + "' and a.screen ='" + sc + "' ";
                DataSet ds = await Utility.ExecuteSelectQuery(query, "asptblautogeneratemas");
                dt = ds.Tables["asptblautogeneratemas"];
                int cnt = dt.Rows.Count;
                if (dt.Rows[0]["id"].ToString() == "")
                {
                    ag.Shortcode= scode;
                  
                    ag.Sequenceid = year + "-" + com.Substring(0,3) + "-" + 1;
                    ag.Finyear = year;
                    //txtshortcode.Text = ""; txtseqid.Text = "";
                    //txtseqid.Text = combofinyear.Text + "-" + scode + "-" + 1;
                    //txtshortcode.Text = scode;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                
            }
            return dt;
        }

  
        [HttpGet("GridLoad")]
        public override async Task<IActionResult> GridLoad()
        {
          //  return await _context.asptblautogeneratemas.ToListAsync();
            string sel = "SELECT a.asptblautogeneratemasid, a.sequenceid,a.finyear,b.compcode,d.menuname as screen,a.shortcode,A.sequenceno,a.barcodetype, a.active  FROM  asptblautogeneratemas A JOIN GTCOMPMAST B ON A.COMPCODE=B.GTCOMPMASTID  join asptblnavigation d on d.menuid=a.screen  ORDER BY a.asptblautogeneratemasid desc";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblautogeneratemas");
            DataTable dt = ds.Tables["asptblautogeneratemas"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad/{id}")]  
        public override async Task<IActionResult> GridLoad(Int64 id)
        {
            //  return await _context.asptblautogeneratemas.ToListAsync();
            string sel = " select *  FROM  asptblautogeneratemas a   where   a.asptblautogeneratemasid='" + id+"'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblautogeneratemas");
            DataTable dt = ds.Tables["asptblautogeneratemas"];
            return new JsonResult(dt);
        }

        [HttpGet("Finyear")]
        public async Task<ActionResult> Finyear()
        {
            //  return await _context.asptblautogeneratemas.ToListAsync();
            string sel = "SELECT SUBSTRING(now(),1,4) AS finyear from dual  union all select distinct a.finyear from asptblautogeneratemas a where a.compcode='"+Class.Users.COMPCODE+"'"; 
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "dual");
            DataTable dt = ds.Tables["dual"];
            return new JsonResult(dt);
        }

        [HttpGet("BarCodeTypeParam")]
        public async Task<IActionResult> BarCodeTypeParam()
        {
             DataTable dt1 = new DataTable();
            string sel="select 'MONTHLY-WISE'  barcodetype from dual union all select 'SEQUENCE-WISE'  barcodetype from dual";
            DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "dual");
            dt1 = ds1.Tables["dual"];
            return new JsonResult(dt1);
        }

        // GET: api/AutoGenerateMasters/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<AutoGenerateMaster>> GetAutoGenerateMaster(long id)
        //{
        //    var autoGenerateMaster = await _context.asptblautogeneratemas.FindAsync(id);

        //    if (autoGenerateMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return autoGenerateMaster;
        //}

        // PUT: api/AutoGenerateMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAutoGenerateMaster(long id, AutoGenerateMaster autoGenerateMaster)
        //{
        //    if (id != autoGenerateMaster.Asptblautogeneratemasid)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(autoGenerateMaster).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AutoGenerateMasterExists(id))
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


        [HttpGet("getMenuname/{s}")]
        public async Task<string> getMenuname(Int64 s,Int64 compcode,Int64 user)
        {
            string sel = "select a.menuname as screen,b.compcode from asptblnavigation a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblmenuname e on e.Menunameid=a.menuid   where a.active='T' and a.menuid='" + s + "' and a.compcode='" + compcode + "'  and a.username='" + user + "' order by 1 ";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblnavigation");
            DataTable dt = ds.Tables["asptblnavigation"];
            menuname = "";
            menuname = Convert.ToString(dt.Rows[0]["screen"].ToString());
            ag.Compcode1 = dt.Rows[0]["compcode"].ToString();
            return menuname;
        }

        // POST: api/AutoGenerateMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Saves/{data}")]
        public override async Task<IActionResult> Saves(string data)
        {
            AutoGenerateMaster aa = new AutoGenerateMaster();
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(data.ToString(), (typeof(DataTable)));
            string SEL = "";
            try
            {
               await getMenuname(Convert.ToInt64("0" + dt.Rows[0]["screen"].ToString()), Convert.ToInt64("0" + dt.Rows[0]["compcode"].ToString()), Class.Users.USERID);
                Class.Users.ScreenName = menuname;
                if (Convert.ToInt64("0"+dt.Rows[0]["asptblautogeneratemasid"].ToString()) > 0) { aa.Asptblautogeneratemasid = Convert.ToInt64("0" + dt.Rows[0]["asptblautogeneratemasid"].ToString()); }
                else { aa.Asptblautogeneratemasid = 0; } 
                aa.Compcode1 = ag.Compcode1;
                aa.Finyear = Convert.ToString(dt.Rows[0]["Finyear"].ToString());
                aa.Sequenceid= Convert.ToString(dt.Rows[0]["Sequenceid"].ToString());
                aa.Sequenceno= Convert.ToInt64("0" + dt.Rows[0]["sequenceno"].ToString());
                aa.Shortcode = Convert.ToString(dt.Rows[0]["shortcode"].ToString());
                aa.Compcode = Convert.ToInt64("0" + dt.Rows[0]["compcode"].ToString());
                aa.Screen = Convert.ToInt64("0" + dt.Rows[0]["screen"].ToString());
                aa.Barcode= Convert.ToInt64("0" + dt.Rows[0]["barcode"].ToString());
                  aa.Barcode1=  Convert.ToInt64("0" + dt.Rows[0]["barcode1"].ToString());
                aa.Barcodetype = Convert.ToString(dt.Rows[0]["barcodetype"].ToString());
                aa.Active = dt.Rows[0]["active"].ToString()=="T" ?  "T" : "F";
               Class.Users.Finyear = ag.Finyear;
                aa.Username = Class.Users.USERID;
                aa.Ipaddress = GenFun.GetLocalIPAddress();
                aa.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());// Convert.ToDateTime("date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')");// Convert.ToString(System.DateTime.Now.ToString("yyyy-MMM-dd") + " " + System.DateTime.Now.ToLongTimeString());
                aa.Createdby = Class.Users.HUserName;
                aa.Modifiedby = Class.Users.HUserName;
                aa.Modifiedon = Convert.ToDateTime(System.DateTime.Now.ToString()).ToString();              
                string msg = string.Empty;
            
                 
                    GlobalVariables.Dt = Utility.MySqlQuery("select * from  asptblautogeneratemas  where    finyear='" + ag.Finyear + "' and   compcode='" + aa.Compcode + "' and    screen='" + aa.Screen + "' and shortcode='" + aa.Shortcode + "'  and   sequenceno='" + aa.Sequenceno + "' and  active='" + aa.Active + "' and  barcode='" + aa.Barcode + "'  and  barcode1='" + aa.Barcode1 + "' and barcodetype='" + aa.Barcodetype + "'");
                    if (GlobalVariables.Dt.Rows.Count != 0)
                    {
                        return new JsonResult("Child Record Found ");
                    }
                    else if (GlobalVariables.Dt.Rows.Count != 0 && Convert.ToInt64("0" + aa.Asptblautogeneratemasid) == 0 || Convert.ToInt64("0" + aa.Asptblautogeneratemasid) == 0)
                    {
                        await autonumberload(ag.Finyear, aa.Compcode1,aa.Shortcode, aa.Screen);
                        aa.Shortcode = ag.Compcode1.Substring(0,3);
                        aa.Sequenceid = ag.Sequenceid;
                   await aa.InsertCommond();
                        SEL="Record Saved Successfully ";
                       
                    }
                    else
                    {

                   await aa.UpdateCommond();
                               SEL="Record Updatged Successfully ";
                          
                            //string up = "update  asptblautogeneratemas  set sequenceid='" + aa.Sequenceid + "',finyear='" + ag.Finyear + "',compcode='" + aa.Compcode + "',screen='" + aa.Menuname + "',shortcode='" + aa.Shortcode + "',sequenceno='" +aa.Sequenceno + "',active='" + chk + "',barcode='" + aa.Barcode + "',compcode1='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',createdby='" + System.DateTime.Now.ToString() + "',modifiedby='" + Class.Users.HUserName + "', ipaddress='" + Class.Users.IPADDRESS + "',barcodetype='" + aa.Barcodetype + "',barcode='" + Convert.ToInt64("0" + aa.Barcode) + "' ,barcode1='" + Convert.ToInt64("0" + aa.Barcode1) + "' where asptblautogeneratemasid='" + aa.Asptblautogeneratemasid + "'";
                            //await Utility.ExecuteNonQuery(up);
                            //return new JsonResult("Record Updatged Successfully ");
                        
                      
                    }
             
            }
            catch (Exception ex)
            {

               // MessageBox.Show("Grade " + "        " + ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return new JsonResult(SEL);
        }

        // DELETE: api/AutoGenerateMasters/5
        [HttpDelete("DeleteCommond/{id}")]
        public override async Task<IActionResult> DeleteCommond(long id)
        {
            string msg = string.Empty;
           
            string sel= "delete from asptblautogeneratemas where asptblautogeneratemasid=" + id;
            await Utility.ExecuteNonQuery(sel);
            msg = "delete";
            return new JsonResult(msg);

        }

        //private bool AutoGenerateMasterExists(long id)
        //{
        //    return _context.asptblautogeneratemas.Any(e => e.Asptblautogeneratemasid == id);
        //}




   
    }
}

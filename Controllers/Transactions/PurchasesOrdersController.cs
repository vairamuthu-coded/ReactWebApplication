using System;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BarcodeLib;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NuGet.Protocol;
using QRCoder;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Masters;
using ReactWebApplication.Models.Transactions;


namespace ReactWebApplication.Controllers.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesOrdersController : ControllerBase
    {
        // private readonly AppDBContext _context;
        List<string> _siz = new List<string>();
        List<string> _col = new List<string>();
        List<string> _head = new List<string>();
        PurchasesOrder p1 = new PurchasesOrder();
        Models.Masters.Master mas = new Master();
        string coid = "", siid = ""; string msg = string.Empty;
        Int64 bar1 = 0, bar4 = 0; string bar = "", bar2 = ""; string bar3 = ""; byte[] stdbytes; Int64 std;
        string monthwise = "MONTHLY-WISE";
        string sequencewise = "SEQUENCE-WISE";
        Models.TreeView.userRights sm = new Models.TreeView.userRights();
        public List<string> SizeIndex
        {
            get
            { return _siz; }
            set { _siz = value; }
        }
        public List<string> ColorIndex
        {
            get
            { return _col; }
            set { _col = value; }
        }

        public List<string> GridHeader
        {
            get
            { return _head; }

        }
        public PurchasesOrdersController()
        {
            //  _context = context;
            Class.Users.ScreenName = "BarcodeGenerate";
            Class.Users.HCompcode = "SAATEX";
            Class.Users.HUserName = "VAIRAM";

        }

        // GET: api/PurchasesOrders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PurchasesOrder>>> GetPurchasesOrder()
        //{
        //    return await _context.asptblpur.ToListAsync();

        //}



        // GET: api/PurchasesOrders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PurchasesOrder>> GetPurchasesOrder(long id)
        //{
        //    var purchasesOrder = await _context.asptblpur.FindAsync(id);

        //    if (purchasesOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return purchasesOrder;
        //}
        [HttpGet("usercheck/{autotable}/{screen}")]
        public async Task<IActionResult> usercheck(string autotable,string screen)
        {
      
            DataTable dt1 = await sm.usercheck(autotable, screen);          
            return Ok(dt1);

        }

       

        [HttpGet("GridLoad/{compcode}/{tbl}/{screen}/{autotable}")]
        public async Task<IActionResult> GridLoad(Int64 compcode,string tbl,string screen,string autotable)
        {
            CommonDetails com = new CommonDetails();
            DataTable dt1 = new DataTable();
            string barindex = "";
            try
            {
                bar4 = 0; bar = "";bar1 = 0; bar2 = ""; bar3 = "";


                Class.Users.ScreenName = screen;
                p1.Monthwise = "MONTHLY-WISE"; 
                p1.Sequencewise = "SEQUENCE-WISE";          
                Class.Users.Month = mas.getAbbreviatedName(System.DateTime.Now.Month);
                p1.Barcodemonth = Class.Users.Month;
                p1.Finyear = Class.Users.Finyear;
                p1.Compcode = compcode;
                
                if (p1.Compcode > 0 && p1.Asptblpurid <= 0)
                {
                    Class.Users.Query = "select distinct a.Barcodetype from "+ autotable + " a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.Finyear='" + Class.Users.Finyear + "' and b.gtcompmastid='" + Class.Users.COMPCODE + "' AND c.Aliasname='" + Class.Users.ScreenName + "' and c.username='" + Class.Users.USERID + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds =await Utility.ExecuteSelectQuery(Class.Users.Query, autotable);
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    Class.Users.Query = "select max(a." + tbl + "1id)+1 as id from " + tbl + " a join gtcompmast b on a.Compcode = b.gtcompmastid  where a.Finyear='" + Class.Users.Finyear + "'   and a.Compcode='" + Class.Users.COMPCODE + "' and a.Barcodetype='" + dt.Rows[0]["Barcodetype"].ToString() + "' ;";
                    DataSet dsid = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    DataTable dtid = dsid.Tables[0];
                    Class.Users.Query = "";
                    if (p1.Sequencewise == dt.Rows[0]["Barcodetype"].ToString())
                    {
                        Class.Users.BarCodeType = dt.Rows[0]["Barcodetype"].ToString();
                        p1.Barcodetype= dt.Rows[0]["Barcodetype"].ToString();
                        dt1 = await com.autonumberload1(Class.Users.Finyear, compcode, Class.Users.ScreenName, tbl);
                        bar1 = Convert.ToInt64(dt1.Rows[0]["Barcode1"].ToString())+1;                       
                        bar4 = bar1;
                        p1.Barcode = bar1.ToString();
                        p1.Barcode1 = bar1.ToString();
                        p1.Barcode2 = bar1.ToString();
                        p1.Barcode3 = Convert.ToString(bar1);
                    }
                    if (p1.Monthwise == dt.Rows[0]["Barcodetype"].ToString())
                    {
                        Class.Users.BarCodeType = dt.Rows[0]["Barcodetype"].ToString();
                        p1.Barcodetype = dt.Rows[0]["Barcodetype"].ToString();
                        dt1 = await com.autonumberload2(Class.Users.Finyear, compcode, Class.Users.ScreenName, tbl);
                        bar = System.DateTime.Now.ToString("MM") + System.DateTime.Now.Year;
                        bar1 = Convert.ToInt64("0" + dt1.Rows[0]["Barcode1"].ToString());                        
                        barindex =Convert.ToString(bar1.ToString());
                       

                        if (barindex.ToString().Length == 8)
                        {

                            p1.Barcode3 = "0" + Convert.ToString(barindex);
                            bar4 = Convert.ToInt64(p1.Barcode3)+1;
                            p1.Barcode = Convert.ToString(p1.Barcode3);
                            p1.Barcode1 = Convert.ToString("0"+bar4);
                            p1.Barcode2 = Convert.ToString(p1.Barcode3);

                            barindex = ""; bar4 = 0;
                        }
                        else
                        {
                            p1.Barcode = Convert.ToString(bar + bar1);
                            bar4 = Convert.ToInt64(p1.Barcode);
                            p1.Barcode1 = bar4.ToString();
                            p1.Barcode2 = p1.Barcode.ToString();
                            p1.Barcode3 = Convert.ToString(p1.Barcode);

                            barindex = "";
                        }
                        

                    }

                    if (dt1.Rows.Count > 0)
                    {
                        //if (p.Sequencewise == Class.Users.BarCodeType)
                        //{
                        DataTable dt2 = await com.shortcode1(Class.Users.Finyear, Class.Users.BarCodeType, compcode, Class.Users.ScreenName, tbl);
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) <= 0)
                        {

                            p1.Asptblpur1id = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
                            p1.Shortcode = dt1.Rows[0]["shortcode"].ToString();
                            p1.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
                        }
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) > 0)
                        {

                            p1.Asptblpur1id = Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString());
                            p1.Shortcode = dt2.Rows[0]["shortcode"].ToString();
                            p1.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dtid.Rows[0]["id"].ToString();
                        }
                        if (dt1.Rows[0]["Barcode1"].ToString() != "" && p1.Monthwise == Class.Users.BarCodeType)
                        {
                            p1.Autono = p1.Barcode3;

                        }
                        if (dt1.Rows[0]["Barcode1"].ToString() != "" && p1.Sequencewise == Class.Users.BarCodeType)
                        {
                            p1.Barcode3 = dt1.Rows[0]["Barcode1"].ToString();
                            bar3 = p1.Barcode3;
                            p1.Autono = p1.Barcode3;

                        }
                        //  else { txtautono.Text = dt2.Rows[0]["Barcode1"].ToString(); }
                        p1.Pono = Convert.ToString(p1.Pono);
                        p1.Asptblpur1id = Convert.ToInt64("0" + p1.Asptblpur1id);



                    }

                }
          
            }
            catch (Exception EX) { throw new Exception(EX.Message); }
            return new JsonResult(p1.Pono);

        }

        [HttpGet("auto/{compcode}/{Asptblpurid}/{Pono}")]
        public async Task<IActionResult> auto(Int64 compcode, Int64 Asptblpurid, string Pono)
        {
            CommonDetails com = new CommonDetails();
            DataTable dt1 = new DataTable();
            string barindex = "";
            try
            {


                Class.Users.ScreenName = "BarcodeGenerate";
                p1.Monthwise = "MONTHLY-WISE"; bar1 = 0; bar2 = "";
                p1.Sequencewise = "SEQUENCE-WISE"; string tbl = "Asptblpur";
                Class.Users.Finyear = System.DateTime.Now.Year.ToString();
                Class.Users.Month = mas.getAbbreviatedName(System.DateTime.Now.Month);
                p1.Barcodemonth = Class.Users.Month;
                p1.Finyear = Class.Users.Finyear;
                p1.Compcode = compcode;

                if (p1.Compcode > 0 && Asptblpurid <= 0)
                {
                    Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.Finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    Class.Users.Query = "select max(a." + tbl + "1id)+1 as id from " + tbl + " a join gtcompmast b on a.Compcode = b.gtcompmastid  where a.Finyear='" + Class.Users.Finyear + "'   and a.Compcode='" + Class.Users.COMPCODE + "' ;";
                    DataSet dsid = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    DataTable dtid = dsid.Tables[0];
                    Class.Users.Query = "";
                    if (p1.Sequencewise == dt.Rows[0]["Barcodetype"].ToString())
                    {
                        Class.Users.BarCodeType = dt.Rows[0]["Barcodetype"].ToString();
                        dt1 = await com.autonumberload1(Class.Users.Finyear,compcode, Class.Users.ScreenName, tbl);
                        bar1 = Convert.ToInt64("0" + dt1.Rows[0]["Barcode1"].ToString());
                        bar2 = dt1.Rows[0]["Barcode1"].ToString();
                    }
                    if (p1.Monthwise == dt.Rows[0]["Barcodetype"].ToString())
                    {
                        Class.Users.BarCodeType = dt.Rows[0]["Barcodetype"].ToString();
                        dt1 = await com.autonumberload2(Class.Users.Finyear, compcode, Class.Users.ScreenName, tbl);
                        bar2 = System.DateTime.Now.ToString("MM") + System.DateTime.Now.Year;
                        bar1 = Convert.ToInt64("0" + dt1.Rows[0]["Barcode1"].ToString());
                        barindex = bar2 + bar1.ToString();
                        if (barindex.ToString().Length == 9)
                        {
                           
                            bar1 = Convert.ToInt64(barindex)+1;
                            if (bar1.ToString().Length == 8)
                            {
                                p1.Barcode = bar2;
                                p1.Barcode2 = bar1.ToString();
                                p1.Barcode3 = "0" + Convert.ToString(bar1); 
                                bar3 = p1.Barcode3;
                                bar4= Convert.ToInt64(barindex); barindex = "";
                            }
                            else
                            {
                                p1.Barcode3 = Convert.ToString(bar1);
                                bar3 = p1.Barcode3;
                                bar4 = Convert.ToInt64(p1.Barcode3); barindex = "";
                            }
                        }
                        else
                        {
                            p1.Barcode3 = Convert.ToString(dt1.Rows[0]["Barcode1"].ToString()); 
                            bar3 = p1.Barcode3;
                            bar4 = Convert.ToInt64(p1.Barcode3); barindex = "";
                        }

                    }

                    if (dt1.Rows.Count > 0)
                    {
                        //if (p.Sequencewise == Class.Users.BarCodeType)
                        //{
                        DataTable dt2 = await com.shortcode1(Class.Users.Finyear, Class.Users.BarCodeType, compcode, Class.Users.ScreenName, tbl);
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) <= 0)
                        {

                            p1.Asptblpur1id = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
                            p1.Shortcode = dt1.Rows[0]["shortcode"].ToString();
                            p1.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
                        }
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) > 0)
                        {

                            p1.Asptblpur1id = Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString());

                            p1.Shortcode = dt2.Rows[0]["shortcode"].ToString();
                            p1.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dtid.Rows[0]["id"].ToString();
                        }
                        if (dt1.Rows[0]["Barcode1"].ToString() != "" && p1.Monthwise == Class.Users.BarCodeType)
                        {
                            p1.Autono = p1.Barcode3;

                        }
                        if (dt1.Rows[0]["Barcode1"].ToString() != "" && p1.Sequencewise == Class.Users.BarCodeType)
                        {
                            p1.Barcode3 = dt1.Rows[0]["Barcode1"].ToString();
                            bar3 = p1.Barcode3;
                            p1.Autono = p1.Barcode3;

                        }
                        //  else { txtautono.Text = dt2.Rows[0]["Barcode1"].ToString(); }
                        p1.Pono = Convert.ToString(p1.Pono);
                        p1.Asptblpur1id = Convert.ToInt64("0" + p1.Asptblpur1id);



                    }

                }
                else
                {
                    Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.Finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    p1.Pono = Pono;
                    p1.Asptblpurid = Asptblpurid;
                    p1.Barcodetype = dt.Rows[0]["Barcodetype"].ToString();
                }
            }
            catch (Exception EX) { throw new Exception(EX.Message); }
            return new JsonResult(p1.Pono);

        }

        [HttpGet("colorid/{s}")]
        public async Task<string> colorid(string s)
        {

            string sel = "select asptblcolmasid,colorname from  asptblcolmas where active='T' and colorname='" + s + "'  order by 2";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblcolmas");
            DataTable dt = ds.Tables["asptblcolmas"];
            coid = "";
            coid = Convert.ToString(dt.Rows[0]["asptblcolmasid"].ToString());

            return coid;

        }


        [HttpGet("sizeid/{s}")]
        public async Task<string> sizeid(string s)
        {

            string sel = "select asptblsizmasid from  asptblsizmas where sizename='" + s + "' ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblsizmas");
            DataTable dt = ds.Tables["asptblsizmas"];
            if (dt.Rows.Count > 0)
            {
                siid = "";
                siid = Convert.ToString(dt.Rows[0]["asptblsizmasid"].ToString());
            }

            return siid;
        }

        [HttpGet("PonoDetails/{tbl}")]
        public async Task<IActionResult> PonoDetails(string tbl)
        {

            string sel = "select distinct Pono from  " + tbl + " ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, tbl);
            DataTable dt = ds.Tables[tbl];
            return new JsonResult(dt);
        }


        [HttpGet("PonoDetails/{tbl}/{com}")]
        public async Task<IActionResult> PonoDetails(string tbl, Int64 com)
        {

            string sel = "select Pono from  "+tbl+" where compcode='" + com + "' ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, tbl);
            DataTable dt = ds.Tables[tbl];
            return new JsonResult(dt);
        } 



        [HttpGet("PonoDetailss/{com}/{processtype}/{pono}")]
        public async Task<IActionResult> PonoDetailss(string com, string processtype, string pono )
        {
            string sel1 = "SELECT '' as Asptblpurid, a.Compcode,a.Podate,a.Pono,'"+ processtype + "' as Processtype,a.Buyer,a.Stylename,a.Sizegroup, a.Processname,a.Orderqty,a.Styleref,a.Orderno, a.active FROM  Asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblsizgrp e on e.asptblsizgrpid=a.Sizegroup join asptblstymas f on f.asptblstymasid=a.Stylename  where a.Compcode='" + com + "' and a.Pono='"+pono+"'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            DataTable dt = ds.Tables["Asptblpur"];
            return new JsonResult(dt);
        }

        //[NonAction]
        //public  string[] Add(this string[] myArray, string StringToAdd)
        //{
        //    var list = myArray.ToList();
        //    list.Add(StringToAdd);
        //    return list.ToArray();
        //}

        [HttpGet("PonoDetails/{com}/{sizegroup}/{pono}")]
        public async Task<IActionResult> PonoDetails(string com, string sizegroup, string pono)
        {
            string sel1 = "SELECT distinct  d.ASPTBLSIZMASID,d.Sizename FROM  Asptblpur a join Asptblpurdet b on a.Compcode=b.Compcode and a.Asptblpurid=b.Asptblpurid  join asptblsizgrp c on c.asptblsizgrpid=a.Sizegroup join asptblsizmas d on d.ASPTBLSIZMASID=b.Sizename  where a.Compcode='" + com + "' and a.Sizegroup='" + sizegroup + "' and a.Pono='" + pono + "' order by 1";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            DataTable dt = ds.Tables["Asptblpur"];
            return new JsonResult(dt);
        }

        [HttpGet("PonoPortionDetails/{s}/{ss}/{sss}/{ssss}")]
        public async Task<IActionResult> PonoPortionDetails(string s, string ss, string sss,string ssss)
        {
            string sel1 = "SELECT   b.Portion FROM  Asptblpur a join Asptblpurdet b on a.Compcode=b.Compcode and a.Asptblpurid=b.Asptblpurid  join asptblsizgrp c on c.asptblsizgrpid=a.Sizegroup join asptblsizmas d on d.ASPTBLSIZMASID=b.Sizename join asptblcolmas e on e.asptblcolmasid=b.Colorname  where a.Compcode='" + s + "' and a.Sizegroup='" + ss + "' and a.Pono='" + sss + "' and b.Asptblpurdetid='" + ssss + "' order by 1 ";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            DataTable dt = ds.Tables["Asptblpur"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {
            //string sel1 = "SELECT a.Asptblpurid, b.Compcode,a.Podate,a.Pono,f.Stylename,e.Sizegroup,a.Orderqty,a.Styleref,a.Orderno, a.active FROM  Asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblsizgrp e on e.asptblsizgrpid=a.Sizegroup join asptblstymas f on f.asptblstymasid=a.Stylename   order by  a.Asptblpurid desc";
            //DataSet ds =await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            //DataTable dt = ds.Tables["Asptblpur"];
            DataTable dt = await p1.GridLoad();
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad/{id}/{compcode}")]
        public async Task<IActionResult> GridLoad(Int64 id,string compcode)
        {
            string sel1 = "SELECT a.Asptblpurid,a.Asptblpur1id,a.Compcode,a.Podate,a.Processtype,a.Pono,a.Stylename,a.Sizegroup,a.Buyer,a.Processname, a.Orderqty,a.excessqty,a.Styleref,a.Orderno, a.active FROM  Asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblsizgrp e on e.asptblsizgrpid=a.Sizegroup join asptblstymas f on f.asptblstymasid=a.Stylename   where a.Asptblpurid='" + id+ "' and  b.Compcode='" + compcode + "'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            DataTable dt = ds.Tables["Asptblpur"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad/{id}/{compcode}/{Pono}")]
        public async Task<IActionResult> GridLoad(Int64 id, string compcode,string Pono)
        {
            
            string sel2 = "select distinct   e.Sizename,e.asptblsizmasid from Asptblpurdet a join Asptblpur b on a.Asptblpurid=b.Asptblpurid join gtcompmast c on c.gtcompmastid=b.Compcode join asptblcolmas d on d.asptblcolmasid=a.Colorname  join asptblsizmas e on e.asptblsizmasid=a.Sizename  where c.Compcode='" + compcode + "' and a.Pono='" + Pono + "'  and a.Asptblpurid='" + id + "' order by 2";
            DataSet ds2 =await Utility.ExecuteSelectQuery(sel2, "Asptblpur");
            DataTable dt21 = ds2.Tables["Asptblpur"];           
            return new JsonResult(dt21);
        }

        [HttpGet("GridLoadColor/{id}/{Pono}/{compcode}")]
        public async Task<JsonResult> GridLoadColor(Int64 id, string Pono,string compcode)        
        {
          
            string sel20 = "  select distinct   x.Colorname from (select  a.Asptblpurdetid,  d.Colorname  from Asptblpurdet a join Asptblpur b on a.Asptblpurid=b.Asptblpurid and a.Pono=b.Pono join gtcompmast c on c.gtcompmastid=b.Compcode join asptblcolmas d on d.asptblcolmasid=a.Colorname    where c.Compcode='" + compcode + "' and a.Asptblpurid='" + id + "' and a.Pono='" + Pono + "' order by 1) x ";
            DataSet ds20 = await Utility.ExecuteSelectQuery(sel20, "Asptblpur");
            DataTable dt20 = ds20.Tables["Asptblpur"];
  
            return new JsonResult(dt20);
        }

        [HttpGet("GridLoadColor/{Pono}/{compcode}")]
        public async Task<IActionResult> GridLoadColor(string Pono, string compcode)
        {
            DataTable dtportion = new DataTable();
            DataTable dt20 = new DataTable(); var sel20 = "";
            try
            {
                 sel20 = "select distinct  x.Colorname from (select  a.Asptblpurdetid,  d.Colorname  from Asptblpurdet a join Asptblpur b on a.Asptblpurid=b.Asptblpurid and a.Pono=b.Pono join gtcompmast c on c.gtcompmastid=b.Compcode join asptblcolmas d on d.asptblcolmasid=a.Colorname    where a.Compcode='" + compcode + "'  and a.Pono='" + Pono + "' order by 1) x ";
                DataSet ds20 = await Utility.ExecuteSelectQuery(sel20, "Asptblpur");
                dt20 = ds20.Tables["Asptblpur"];
                
                if (dtportion.Columns.Count <= 0)
                {
                    dtportion.Columns.Add("Colorname");
                    dtportion.Columns.Add("Portion");

                }
                int cnt = 0;
                foreach (DataRow dr in dt20.Rows)
                {
                    sel20 = "";
                    sel20 = "SELECT distinct e.Colorname,  b.Portion FROM  Asptblpur a join Asptblpurdet b on a.Compcode=b.Compcode and a.Asptblpurid=b.Asptblpurid  join asptblsizgrp c on c.asptblsizgrpid=a.Sizegroup join asptblsizmas d on d.ASPTBLSIZMASID=b.Sizename join asptblcolmas e on e.asptblcolmasid=b.Colorname  where a.Compcode='" + compcode + "'  and a.Pono='" + Pono + "' and e.Colorname='" + dr[0].ToString() + "' order by 1 ";
                    DataSet ds = await Utility.ExecuteSelectQuery(sel20, "Asptblpur");
                    DataTable dt = ds.Tables["Asptblpur"];
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        dtportion.Rows.Add();

                        dtportion.Rows[cnt]["Colorname"] = dr1.ItemArray[0].ToString();
                        dtportion.Rows[cnt]["Portion"] = dr1.ItemArray[1].ToString();
                    }
                    cnt++;
                }
            }catch(Exception EX) { }
            finally { 
                if (dtportion.Rows.Count <= 0) {
                    dtportion.Columns.Add("Colorname");
                    dtportion.Columns.Add("Portion");
                    dtportion.Rows.Add();
                    dtportion.Rows[0]["Colorname"] = "invalid";
                    dtportion.Rows[0]["Portion"] = "Function Name :  GridLoadColor(string Pono, string compcode)" + sel20.ToString();
                } 
            }
            return new JsonResult(dtportion);
        }

        [HttpGet("GridLoadSize/{id}/{Pono}/{compcode}")]
        public async Task<ActionResult> GridLoadSize(Int64 id, string Pono, string compcode)
        {
            string sel2 = "select  a.Asptblpurdetid, e.asptblsizmasid,a.Portion, e.Sizename, d.Colorname,a.Orderqty   from Asptblpurdet a join Asptblpur b on a.Asptblpurid=b.Asptblpurid join gtcompmast c on c.gtcompmastid=b.Compcode join asptblcolmas d on d.asptblcolmasid=a.Colorname  join asptblsizmas e on e.asptblsizmasid=a.Sizename  where c.Compcode='" + compcode + "' and a.Asptblpurid='" + id + "' and a.Pono='" + Pono + "'  order by 1,2";
            DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "Asptblpur");
            DataTable dt21 = ds2.Tables["Asptblpur"];
            return new JsonResult(dt21);
        }

        [HttpPost]
        [Route("PostPurchasesOrderNonGrid")]
        public async Task<JsonResult> PostPurchasesOrderNonGrid(PurchasesOrder p)
        {
             if (p.Asptblpurid > 0) { await auto(p.Compcode, p.Asptblpurid, p.Pono);  } else { await GridLoad(p.Compcode,"asptblpur", "BarCodeGenerate", "asptblautogeneratemas"); p.Pono = Convert.ToString(p1.Pono); }
           
            Int64 maxid = 0;
            Class.Users.BarCodeType = p1.Barcodetype;
            p.Barcodetype = p1.Barcodetype;
                string month = mas.getAbbreviatedName(System.DateTime.Now.Month); 
                p.Asptblpurid = Convert.ToInt64("0" + p.Asptblpurid);
                p.Asptblpur1id = Convert.ToInt64("0" + p1.Asptblpur1id);
                p.Shortcode = Convert.ToString(p1.Shortcode);
                p.Finyear = Convert.ToString(Class.Users.Finyear);
                p.Pono = Convert.ToString(p1.Pono);
                p.Podate = Convert.ToDateTime(p.Podate).ToString("yyyy-MM-dd").Substring(0, 10);               
                p.Compcode = Convert.ToInt64("0" + p.Compcode);
                p.Orderqty = Convert.ToInt64("0" + p.Orderqty);
                p.Excessqty = Convert.ToInt64("0" + p.Excessqty);
                p.Sizegroup = Convert.ToInt64("0" + p.Sizegroup);
                p.Stylename = Convert.ToInt64("0" + p.Stylename);
                p.Buyer = Convert.ToInt64("0" + p.Buyer);
                p.Processname = Convert.ToInt64("0" + p.Processname);
                p.Processtype = Convert.ToString(p.Processtype);
                p.Issuetype = "PANEL MISTAKE";
                p.Panelmistake = "F";
                p.Compcode1 = Class.Users.COMPCODE;
            p.Active = p.Active; p.Pocancel = p.Pocancel;
            p.Username = Class.Users.USERID;
                p.Createdby = Convert.ToString(Class.Users.HUserName);
                p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString());
                p.Modified = Convert.ToString(System.DateTime.Now.ToString());
                p.Modified1 = p.Podate.ToString();
                p.Modifiedby = Class.Users.HUserName;
                p.Modifiedon = Convert.ToString(System.DateTime.Now.ToString());
                p.Ipaddress = GenFun.GetLocalIPAddress();
            if (bar == "") { p.Barcode = Convert.ToString(bar1); }
            else
            {
                p.Barcode = Convert.ToString(p1.Barcode3);
            }
            p.Barcode1 = Convert.ToString(p1.Barcode1);
            p.Barcode2 = Convert.ToString(p1.Barcode2);
            p.Barcode3 = Convert.ToString(p1.Barcode3);

            p.Barcodetype = Class.Users.BarCodeType;
                p.Barcodemonth = month;
                p.Orderno = Convert.ToString(p.Orderno);
                p.Styleref = Convert.ToString(p.Styleref);
                
    
         DataTable dt0= await p.SelectCommond(p.Pono,p.Barcode,p.Barcode3,p.Barcodetype,p.Asptblpur1id, p.Shortcode ,p.Finyear,p.Podate,p.Stylename,p.Orderqty,p.Buyer, p.Processname,p.Processtype,p.Pocancel,p.Active,p.Excessqty,p.Orderno,p.Styleref);
            if (p.Asptblpurid <= 0) { p.Asptblpurid = 0; }
            else
            {
                p.Asptblpurid = Convert.ToInt64("0" + p.Asptblpurid);
            }
            if (dt0.Rows.Count != 0) { }
            else if (dt0.Rows.Count != 0 && p.Asptblpurid == 0 || p.Asptblpurid == 0)
            {
                await p.InsertCommond();
                DataTable dt2 = await p.SelectCommond(p.Compcode, p.Finyear,p.Pono);
                maxid = Convert.ToInt64("0" + dt2.Rows[0]["id"].ToString());
                if (dt2.Rows.Count > 0 && stdbytes != null)
                {
                    MySqlCommand cmd;
                    string ins1 = "UPDATE  Asptblpur SET imagebytes='" + stdbytes.Length.ToString() + "',garmentimage=@garmentimage where  Asptblpur1id='" + dt2.Rows[0]["ID"].ToString() + "'";
                    cmd = new MySqlCommand(ins1, Utility.Connect());
                    cmd.Parameters.Add(new MySqlParameter("@garmentimage", stdbytes));
                    cmd.ExecuteNonQuery();
                }
                msg = Class.Users.insert;
            }
            else
            {
                string up = "update  Asptblpur  set   compcode1='" + Class.Users.COMPCODE + "',Barcodemonth='" + Class.Users.Month + "', username='" + Class.Users.USERID + "', Modifiedby='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "',active='" + p.Active + "' ,pocancel='" + p.Pocancel + "' ,orderno='" + p.Orderno + "',styleref='" + p.Styleref + "' ,Modified1=date_format('" + p.Modified1.ToString().Substring(0, 10) + "','%Y-%m-%d') where Asptblpurid='" + p.Asptblpurid + "'";
                await Utility.ExecuteNonQuery(up);
                maxid = Convert.ToInt64("0" + p.Asptblpurid);
                MySqlCommand cmd;
                    if (stdbytes != null)
                    {
                        string ins1 = "UPDATE  Asptblpur SET imagebytes='" + stdbytes.Length.ToString() + "',garmentimage=@garmentimage where  Asptblpur1id='" + p.Asptblpurid + "'";
                        cmd = new MySqlCommand(ins1, Utility.Connect());
                        cmd.Parameters.Add(new MySqlParameter("@garmentimage", stdbytes));
                        cmd.ExecuteNonQuery();
                    }
                    msg = Class.Users.update;
                }
            DataTable dtmax = await p.SelectCommond(p.Compcode, p.Finyear, p.Pono);
            if (dtmax != null && dtmax.Rows.Count > 0)
            {
                p.Asptblpurid = Convert.ToInt64("0" + dtmax.Rows[0]["id"].ToString());
            }

            return new JsonResult(p.Pono + "," + p.Asptblpurid + "," + p.Compcode + "," + p.Finyear + "," + p.Asptblpur1id);
        }

        [HttpPost]
        [Route("PostPurchasesOrder/{users}/{pos}/{uniqueAsptblpurid}/{uniquecompcode}/{uniquefinyear}")]
        public async Task<IActionResult> PostPurchasesOrder(string users,string pos, Int64 uniqueAsptblpurid, Int64 uniquecompcode, string uniquefinyear)
        {
             int i = 0, j = 0;
            PurchasesOrderDetails p2 = new PurchasesOrderDetails();
            try
            {
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(users.ToString(), (typeof(DataTable)));

                if (Class.Users.QrCodeArray.Rows.Count > 0)
                {
                    Class.Users.QrCodeArray.Rows.Clear();
                    Class.Users.QrCodeArray.Columns.Clear();
                }
                p2.Pono = pos;
                p2.Asptblpurid = uniqueAsptblpurid;
                p2.Compcode = uniquecompcode;
                p2.Finyear = Class.Users.Finyear;
                p2.Barcodetype = Class.Users.BarCodeType;
                coladd(dt);
                rowadd(dt);
                DataTable dt2 = new DataTable();
                if (Class.Users.BarCodeType == sequencewise)
                {
                    //string sel2 = "select max(Asptblpurid) id ,max(Asptblpur1id) id1 ,max(Barcode1) Barcode1   from  Asptblpur   where  compcode='" + Class.Users.COMPCODE + "'  and finyear='" + Class.Users.Finyear + "' and Barcodetype='" + Class.Users.BarCodeType + "'  and Pono='" + pos + "'";
                    //DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "Asptblpur");
                    //dt2 = ds2.Tables["Asptblpur"];
                    dt2 = await p2.SelectCommond1(p2.Compcode, p2.Finyear, p2.Barcodetype,p2.Pono);
                    if (p2.Asptblpurid <= 0)
                    {

                        p2.Barcode3 = Convert.ToString(dt2.Rows[0]["Barcode1"].ToString());
                        p1.Sequencewise = Class.Users.BarCodeType;
                    }
                    else
                    {
                      
                        DataTable dt4 = await p2.SelectCommond1(Class.Users.COMPCODE, Class.Users.BarCodeType, p2.Finyear);
                        p1.Sequencewise = Class.Users.BarCodeType;
                        p2.Barcode3 = Convert.ToString(dt4.Rows[0]["Barcode1"].ToString());
                    }
                }

                if (Class.Users.BarCodeType == monthwise)
                {
                   
                   // DataTable dt2 = await com.autonumberload2(Class.Users.Finyear, uniquecompcode, Class.Users.ScreenName, "asptblpur");

                    dt2 = await p2.SelectCommond(Class.Users.COMPCODE, p2.Finyear, Class.Users.BarCodeType, Class.Users.Month,p2.Pono);
                    if (p2.Asptblpurid < 0)
                    {
                        bar1 = Convert.ToInt64(dt2.Rows[0]["Barcode1"].ToString());
                        p1.Monthwise = Class.Users.BarCodeType;
                    }
                    else
                    {
                        string sel4 = "select max(barcode) barcode ,max(barcode1) barcode1,max(barcode3) as barcode3   from  asptblpur   where  compcode='" + Class.Users.COMPCODE + "'  and finyear='" + Class.Users.Finyear + "' and barcodetype='" + Class.Users.BarCodeType + "' and barcodemonth='" + Class.Users.Month + "'";
                        DataSet ds4 = await Utility.ExecuteSelectQuery(sel4, "asptblpur");
                        DataTable dt4 = ds4.Tables["asptblpur"];
                        bar1 = Convert.ToInt64(dt4.Rows[0]["barcode1"].ToString());
                        p1.Monthwise = Class.Users.BarCodeType;

                        //DataTable dt4 = await p2.SelectCommond(Class.Users.COMPCODE, p2.Finyear, Class.Users.BarCodeType, Class.Users.Month);
                       // p2.Barcode3 = Convert.ToString(dt4.Rows[0]["Barcode1"].ToString());
                       // p1.Monthwise = Class.Users.BarCodeType;
                        
                    }

                }
            
                for (i = 0; i < dt.Rows.Count; i++)
                {
               
                    for (j = 0; j < dt.Columns.Count-2; j++)
                    {

                        if (Class.Users.BarCodeType == sequencewise)
                        {
                            bar1 = Convert.ToInt64(p2.Barcode3);
                            p2.Barcode = bar1.ToString();
                            p2.Barcode1 = "0" + bar1.ToString();
                        }
                        if (Class.Users.BarCodeType == monthwise)
                        {
                            if (bar1.ToString().Length < Class.Users.Digit)
                            {
                                bar1 = Convert.ToInt64(bar1)+1;
                                p2.Barcode = "0" + bar1.ToString();
                                p2.Barcode1 = "0" + bar1.ToString();

                            }
                            if (bar1.ToString().Length == Class.Users.Digit)
                            {
                                bar1 = Convert.ToInt64(bar1) +1;
                                p2.Barcode = bar1.ToString();
                                p2.Barcode1 = bar1.ToString();
                            }
                        }

                        await colorid(dt.Rows[i]["Colorname"].ToString());
                        p2.Colorname = coid;
                        if (Convert.ToInt64("0" + dt.Rows[i]["Portion"].ToString()) < 0)
                        {
                            p2.Portion = 1;
                        }                     
                        await sizeid(dt.Columns[j + 2].ToString());
                        p2.Sizename = Convert.ToString(siid);
                        p2.Portion = Convert.ToInt32("0" + dt.Rows[i]["Portion"].ToString());
                        p2.Orderqty = Convert.ToInt64("0" + dt.Rows[i][j + 2].ToString());
                        string sssss = dt.Rows[i]["Colorname"].ToString();
                        string  s1 = dt.Rows[i]["Portion"].ToString();
                        string s2 = "sizename"+dt.Columns[j + 2].ToString();
                        string s3="orderqty"+dt.Rows[i][j + 2].ToString();

                        p2.Asptblpurid = Convert.ToInt64("0" + dt2.Rows[0]["id"].ToString());
                        p2.Asptblpur1id = Convert.ToInt64("0"+dt2.Rows[0]["id1"].ToString());
                        p2.Pono = pos;
                        p2.JJ = i;
                        string sel5 = "select ifnull(Asptblpurdetid,0) as Asptblpurdetid  from  Asptblpurdet   where Pono='" + p2.Pono + "' and colorname='" + p2.Colorname + "' and sizename='" + p2.Sizename + "'    and  compcode='" + uniquecompcode + "'  and finyear='" + uniquefinyear + "'";
                        DataSet ds5 = await Utility.ExecuteSelectQuery(sel5, "Asptblpurdet");
                        DataTable dt5 = ds5.Tables["Asptblpurdet"];
                        if (dt5.Rows.Count != 0)
                        {
                             p2.Asptblpurdetid = Convert.ToInt64("0" + dt5.Rows[0]["Asptblpurdetid"].ToString());
                        }
                        else
                        {
                            p2.Asptblpurdetid = 0;
                        }
                        if (p2.Asptblpurdetid == 0)
                        {
                        await   p2.InsertCommond();
                           // p2.InsertCommond(p2.Barcode, p2.Barcode, p2.Asptblpurid, p2.Asptblpur1id, Class.Users.COMPCODE, p2.Pono, p2.Colorname, p2.Portion, p2.Sizename, p2.Orderqty, Class.Users.Finyear, i);
                            msg = Class.Users.insert;
                        }
                        else
                        {

                            string up = "update  Asptblpurdet  set   compcode='" + Class.Users.COMPCODE + "' ,finyear='" + Class.Users.Finyear + "', portion='" + p2.Portion + "' ,sizename='" + p2.Sizename + "' , orderqty='" + p2.Orderqty + "' where Asptblpurdetid='" + p2.Asptblpurdetid + "'  ";
                            await Utility.ExecuteNonQuery(up);
                            msg = Class.Users.update;
                        }
                    }

                }
            }catch(Exception ex) { msg = ex.Message; }
           

            return new JsonResult(p2.Pono + "," + p2.Asptblpurid + "," + p2.Compcode + "," + p2.Finyear);
        }

        [HttpPost]
        [Route("Barcodeload/{Ponos}/{uniqueAsptblpurid}/{uniquecompcode}/{uniquefinyear}/{UniqueAsptblpur1id}")]
        public async Task<IActionResult> Barcodeload(string Ponos, Int64 uniqueAsptblpurid, Int64 uniquecompcode, string uniquefinyear,Int64 UniqueAsptblpur1id)
        {
            PurchasesModel1detail p3 = new PurchasesModel1detail();
            CommonDetails com = new CommonDetails();
            int i = 0;           
         
            p3.Compcode=uniquecompcode;
            p3.Finyear = uniquefinyear;
            p3.Asptblpurid = uniqueAsptblpurid;
            p3.Pono = Ponos;

            Class.Users.Query = "select Asptblpurdet1id,Asptblpurdetid,Asptblpurid,Asptblpur1id,compcode,Pono,colorname,sizename,orderqty,portion,colorname1,qrcode,Barcode,Barcode1 from Asptblpurdet1  where Asptblpurdet1id<0" + ":'" + Class.Users.COMPCODE + "':'" + uniquefinyear + "':'" + Ponos + "':'Asptblpurdet1'";
            string[] sarray = Class.Users.Query.Split(':');
            Class.Users.QrCode =await com.select(sarray[0], sarray[4]); //com.select(sarray[0], sarray[4]);
            int n = 2;
            int cnt = 0, k = 0, cnt1 = 0, cnt2 = 1, row = 0, col = 0, rowcount = 0, tot = 0;          
         
            col = Class.Users.QrCodeArray.Columns.Count - 2;
            int totcount = 0;
            rowcount = Class.Users.QrCodeArray.Rows.Count;
            int cc = 0;
            try
            {
             DataTable   dt1 = await com.autonumberload2(Class.Users.Finyear, uniquecompcode, Class.Users.ScreenName, "asptblpur");
                bar4 =Convert.ToInt64("0"+ dt1.Rows[0]["barcode1"].ToString());
              
                for (int i1 = 0; i1 < rowcount; i1++)
                {
                    tot = 0; row = 0; cc++;
                    cnt1 = 0; row = 2; int cnt5 = 0;
                    for (i = 0; i < col; i++)
                    {
                        if (col != tot)
                        {
                            cnt2 = cc - 1; cnt1 = i + row; cnt = 0; int cnt4 = 0;
                            int cnt3 = Convert.ToInt32("0" + Class.Users.QrCodeArray.Rows[cnt2]["Portion"].ToString());
                            cnt = Convert.ToInt32("0" + Class.Users.QrCodeArray.Rows[cnt2]["Portion"].ToString());
                            cnt5 = Convert.ToInt32("0" + Class.Users.QrCodeArray.Rows[cnt2][Class.Users.QrCodeArray.Columns[cnt1].ToString()].ToString());
                            if (cnt >= 1) 
                            {
                                k++;
                                int id1 = 0, id2 = 0, id3 = 0; string id4 = "";
                                Class.Users.Query = "select a.Asptblpurdetid ,a.Asptblpurid,b.Asptblpur1id,b.Pono,a.Barcode1   from  Asptblpurdet a join Asptblpur b on a.Asptblpurid=b.Asptblpurid join asptblsizmas  c on c.asptblsizmasid=a.Sizename  where  b.Compcode=" + sarray[1] + "  and b.Finyear=" + sarray[2] + " and b.Pono=" + sarray[3] + " and c.Sizename='" + Class.Users.QrCodeArray.Columns[cnt1].ToString() + "' ";
                                Class.Users.dt = await com.select(Class.Users.Query, "Asptblpurdet");
                                if (Class.Users.dt.Rows.Count > 0)
                                {
                                   
                                    id1 = Convert.ToInt32(Class.Users.dt.Rows[i1]["Asptblpurdetid"].ToString());
                                    id2 = Convert.ToInt32(Class.Users.dt.Rows[i1]["Asptblpurid"].ToString());
                                    id3 = Convert.ToInt32(Class.Users.dt.Rows[i1]["Asptblpur1id"].ToString());
                                    id4 = Convert.ToString(Class.Users.dt.Rows[i1]["Pono"].ToString());
                                   

                                }
                                int mi = 1; k = 1;
                                if (cnt3 == 1 || cnt5 == 1) { cnt4 = cnt5; } else { cnt4 = 0; cnt4 = cnt5; }
                              
                                for (int a = 0; a < cnt4; a++)
                                {
                                    int totcount1 = 0;
                                    for (int b = 0; b < cnt3; b++)
                                    {

                                        if (totcount1 < cnt3)
                                        {
                                            totcount++; totcount1++;
                                            Class.Users.QrCode.Rows.Add(0);
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][1] = id1;
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][2] = id2;
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][3] = id3;
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][4] = p3.Compcode;
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][5] = id4;
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][6] = Class.Users.QrCodeArray.Rows[i1]["Colorname"].ToString(); //GenerateQrCode(Class.Users.QrCodeArray.Rows[i]["Colorname"].ToString());
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][7] = Class.Users.QrCodeArray.Columns[cnt1].ToString();
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][8] = Class.Users.QrCodeArray.Rows[cnt2][Class.Users.QrCodeArray.Columns[cnt1].ToString()].ToString();
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][8] = Class.Users.QrCodeArray.Rows[cnt2][Class.Users.QrCodeArray.Columns[cnt1].ToString()].ToString();
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][9] = Class.Users.QrCodeArray.Rows[cnt2]["Portion"].ToString();
                                            Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][10] = Class.Users.QrCodeArray.Rows[i1]["Colorname"].ToString() + " " + i1 + " " + k.ToString() + " " + Class.Users.QrCodeArray.Columns[cnt1].ToString() + " " + Class.Users.QrCodeArray.Rows[cnt2][Class.Users.QrCodeArray.Columns[cnt1].ToString()].ToString() + " " + totcount;

                                            if (bar4.ToString().Length == 8)
                                            {
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][11] = "0" + bar4;
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][12] = "0" + bar4;
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][13] = "0" + bar4;
                                            }
                                            else
                                            {
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][11] = bar4;
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][12] = bar4;
                                                Class.Users.QrCode.Rows[Class.Users.QrCode.Rows.Count - mi][13] = bar4;
                                            }


                                        }

                                    }
                                    bar4++;  k++;
                                }
                                tot++;

                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                
            }

            try
            {
                string sel3 = "select   Asptblpurdetid  from  Asptblpurdet1   where  compcode='" + p3.Compcode + "'  and finyear='" + p3.Finyear + "' and Asptblpurid='"+p3.Asptblpurid+"' and Pono='" + p3.Pono + "'";
                DataSet ds3 = await Utility.ExecuteSelectQuery(sel3, "Asptblpur");
                DataTable dt3 = ds3.Tables["Asptblpur"];
                if (dt3.Rows.Count > 0)
                {
                   await p3.DeleteCommond();
              
                }


                for (int m = 0; m < Class.Users.QrCode.Rows.Count; m++)
                {


                    p3.Sno = Convert.ToInt64("0" + m.ToString());
                    p3.Asptblpurdetid = Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["Asptblpurdetid"].ToString());
                    p3.Asptblpurid = Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["Asptblpurid"].ToString());
                    p3.Asptblpur1id = Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["Asptblpur1id"].ToString());
                    p3.Compcode = Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["compcode"].ToString());
                    p3.Pono = Class.Users.QrCode.Rows[m]["Pono"].ToString();
                    p3.Colorname = Class.Users.QrCode.Rows[m]["Colorname"].ToString();
                    p3.Sizename = Class.Users.QrCode.Rows[m]["sizename"].ToString();
                    p3.Portion = Convert.ToInt32("0" + Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["Portion"].ToString()));
                    p3.Orderqty = Convert.ToInt64("0" + Class.Users.QrCode.Rows[m]["orderqty"].ToString());
                    p3.Colorname1 = Convert.ToString(Class.Users.QrCode.Rows[m]["colorname1"].ToString());
                    p3.Barcode = Convert.ToString(Class.Users.QrCode.Rows[m]["Barcode"].ToString());
                    p3.Barcode3 = Convert.ToString(Class.Users.QrCode.Rows[m]["Barcode1"].ToString());
                    p3.Cutting = "F";
                    p3.Stitching = "F";
                    p3.Checking = "F";
                    p3.Restitching = "F";
                    p3.Rechecking = "F";
                    p3.Inward = "F";
                    p3.Delivery = "F";
                    p3.Panelmistake = "F";
                    p3.Modified = System.DateTime.Now.ToString("yyyy-MM-dd");
                    await p3.InsertCommond();
                    //Class.Users.Query = "insert into Asptblpurdet1(Asptblpurdetid,Asptblpurid,Asptblpur1id,compcode,Pono,colorname,portion,sizename,orderqty,colorname1,finyear,cutting,stitching,checking,restitching,rechecking,sno,inward,delivery,panelmistake,Barcode,Barcode1,processcheck,ISSUETYPE,Modified) values('" + p3.Asptblpurdetid + "','" + p3.Asptblpurid + "', '" + p3.Asptblpur1id + "' ,'" + p3.Compcode + "' ,'" + p3.Pono + "' , '" + p3.Colorname + "','" + p3.Portion + "','" + p3.Sizename + "','" + p3.Orderqty + "','" + p3.Colorname1 + "','" + p3.Finyear + "','F','F','F','" + p3.Restitching + "','" + p3.Rechecking + "','" + p3.Sno + "','" + p3.Inward + "','" + p3.Delivery + "','" + p3.Panelmistake + "','" + p3.Barcode + "','" + p3.Barcode3 + "','F','CUTTING',date_format('" + p3.Modified + "','%Y-%m-%d'))";
                    //await Utility.ExecuteNonQuery(Class.Users.Query);
                    msg = Class.Users.insert;


                }


            }
            catch (Exception err)
            {

            }
            finally
            {
               // Class.Users.Query = "select max(a.Asptblpurid) as Asptblpurid from Asptblpur a where a.Compcode='" + p3.Compcode + "' and a.Finyear='" + p3.Finyear + "' and a.Pono='" + p3.Pono + "' ";
                DataTable dt = await p3.SelectCommond(p3.Compcode, p3.Finyear, p3.Pono);
                p3.Asptblpurid =Convert.ToInt64("0"+dt.Rows[0]["id"].ToString());
                DataTable dts0 = await p3.SelectCommond(p3.Compcode, Class.Users.BarCodeType, p3.Finyear, p3.Asptblpurid);            

                string up = "update  Asptblpur  set  Barcode='" + dts0.Rows[0]["Barcode"].ToString() + "' , Barcode1='" +  dts0.Rows[0]["Barcode1"].ToString() + "'  where compcode='" + p3.Compcode + "' and finyear='" + p3.Finyear + "' and Asptblpurid='" + dt.Rows[0]["id"].ToString() + "'";
                await Utility.ExecuteNonQuery(up);
                if (Class.Users.BarCodeType == monthwise)
                {
                    Class.Users.QrCode = null;
                    Class.Users.QrCode = await p3.SelectCommond(p3.Compcode, p3.Finyear,p3.Pono, p3.Asptblpurid, Class.Users.BarCodeType);
                  
                }
                if (Class.Users.BarCodeType == sequencewise)
                {
                    Class.Users.QrCode = null;
                    Class.Users.QrCode = await p3.SelectCommond(p3.Compcode, p3.Finyear, p3.Pono, p3.Asptblpurid, Class.Users.BarCodeType);
                   
                }
                string comit = "commit";
                await Utility.ExecuteNonQuery(comit);


                //Pinnacle.ReportFormate.Lyla.BarcodeReport ly = new ReportFormate.Lyla.BarcodeReport();
                //ly.Show();
            }
                return new JsonResult(msg);
        }

        private void coladd(DataTable grid)
        {
            foreach (DataColumn col1 in grid.Columns)
            {
                Class.Users.QrCodeArray.Columns.Add(col1.ColumnName);
            }
        }
        private void rowadd(DataTable grid)
        {
            int inx, cel;
            //for (inx = 0; inx < grid.Rows.Count; inx++)
           // {
              
                foreach (DataRow  cell in grid.Rows)
                {
                    cel = 0; Class.Users.QrCodeArray.Rows.Add();
                    for (int inxx = 0; inxx < cell.ItemArray.Length; inxx++)
                    {
                       
                        Class.Users.QrCodeArray.Rows[Class.Users.QrCodeArray.Rows.Count - 1][grid.Columns[cel].ColumnName] = cell[inxx].ToString();
                        cel++;
                    }
                }
            //}
           
        }

        // DELETE: api/PurchasesOrders/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePurchasesOrder(long id)
        //{
        //    var purchasesOrder = await _context.asptblpur.FindAsync(id);
        //    if (purchasesOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.asptblpur.Remove(purchasesOrder);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        [HttpGet("GetJsonValue/{data1}/{colors}")]
        public IActionResult GetJsonValue(string data1, string colors)
        {
            SizeIndex.Clear();
            string[] ss = data1.Split(',');
            int i = 0;
            foreach (string row in ss)
            {

                if (ss[i].ToString() == "Colorname")
                {
                    ColorIndex.Add(ss[i].ToString().Trim() + ":" + colors.ToString());
                }
                else
                {
                    ColorIndex.Add(ss[i].ToString().Trim() + ":" + "");
                }
                i++;
            }
            string json = JsonConvert.SerializeObject(ColorIndex);
            return new JsonResult(json);
        }



        //private bool PurchasesOrderExists(long id)
        //{
        //    return _context.asptblpur.Any(e => e.Asptblpurid == id);
        //}


    }
}

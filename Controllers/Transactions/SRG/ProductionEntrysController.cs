
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using BarcodeLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Macs;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using ReactWebApplication.Models.Transactions.SRG;

namespace ReactWebApplication.Controllers.Transactions.SRG
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionEntrysController : ControllerBase
    {
        //private readonly AppDBContext _context;
        Models.TreeView.userRights sm = new Models.TreeView.userRights();  string msg = string.Empty;
        string coid = "", siid = "", fabid = ""; 
        List<string> _siz = new List<string>();
        List<string> _col = new List<string>();
        List<string> _head = new List<string>(); int aa = 0, a3 = 1, a1 = 0, cnt2 = 1, rowcount = 0;
        Models.Transactions.SRG.ProductionLot p = new ProductionLot(); CommonDetails cc = new CommonDetails();
        Models.Transactions.SRG.ProductionLotDet pp = new ProductionLotDet();
        Models.Masters.Master mas = new Models.Masters.Master();
        DataTable copyDataTable;

        [HttpGet("usercheck/{autotable}/{screen}")]
        public async Task<IActionResult> usercheck(string autotable,string screen)
        {           
            DataTable dt1 = await sm.usercheck(autotable,screen);
            return Ok(dt1);
        }


        [HttpGet("autos/{fin}/{ccode}/{screen}/{tblname}")]
        public async Task<IActionResult> autos(string fin, string ccode,string screen, string tblname)
        {

            fin = Class.Users.Finyear;
            DataTable dt1 = new DataTable(); 
            if (ccode != "")
            {
                CommonDetails CC = new CommonDetails();
                dt1 = await CC.autonumberload(fin, ccode, screen, tblname);
                if (dt1.Rows.Count < 0)
                {
                    dt1 = await CC.shortcode(fin, ccode, screen, tblname);
                }
                if (dt1.Rows.Count < 0) { }

            }


            return new JsonResult(dt1);
        }

        [HttpGet("BarCodeGenrate/{qrtext}")]
        public IActionResult BarCodeGenrate(string qrtext)
        {
            Barcode barcode = new Barcode();
            Image img = barcode.Encode(TYPE.CODE128, qrtext, Color.Black, Color.White, 250, 100);
            var data = ImageToByteArray(img);
            return new JsonResult(data);
             // return File(data, "image/bmp");
        }

            [HttpGet("QRCodeGenerate/{qrtext}")]
        public IActionResult QRCodeGenerate(string qrtext)
        {
            QRCoder.QRCodeGenerator qc = new QRCoder.QRCodeGenerator();
            var mydata = qc.CreateQrCode(qrtext, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var code = new QRCoder.QRCode(mydata);        
            Image img = code.GetGraphic(10, Color.Black, Color.White, true);

               var qrbytes = ImageToByteArray(img);
              return File(qrbytes,"image/bmp");

        }


        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {
            //string sel1 = "SELECT a.asptblprolotid,a.Proddate, b.Compcode,a.Prodno, ''Buyername,a.Pono,d.stylename,a.lotno,a.orderqty,a.Processtype,e.styleref, a.active FROM  asptblprolot a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblstymas d on d.asptblstymasid=a.stylename  join asptblprolot e on e.Pono=a.Pono and e.Compcode=a.Compcode  order by  a.asptblprolotid desc";
            //DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblprolot");
            DataTable dt = await p.GridLoad();
            return new JsonResult(dt);
        }


        [HttpGet("GridLoad/{id}/{compcode}")]
        public async Task<IActionResult> GridLoad(Int64 id, string compcode)
        {
            // string sel1 = "SELECT a.asptblprolotid,a.asptblprolot1id,a.Compcode,b.Compname,a.Podate,a.Processtype,a.Pono,a.stylename,a.sizegroup,a.Buyer,a.Processname, a.orderqty,a.styleref,a.orderno, a.active FROM  asptblprolot a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblsizgrp e on e.asptblsizgrpid=a.sizegroup join asptblstymas f on f.asptblstymasid=a.stylename   where a.asptblprolotid='" + id + "' and  b.Compcode='" + compcode + "'";
            string sel1 = "SELECT a.asptblprolotid,a.asptblprolot1id,a.Compcode,b.Compname,a.Prodno,a.Proddate, a.Buyer,a.Pono,a.stylename,c.orderno,c.Styleref,a.active,a.Processtype,a.notes,a.docid FROM  asptblprolot a join gtcompmast b on a.Compcode=b.gtcompmastid join asptblpur c on c.pono=a.pono and c.compcode=a.compcode where a.asptblprolotid= '" + id + "' and b.Compcode = '" + compcode + "'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblprolot");
            DataTable dt = ds.Tables["asptblprolot"];
            return new JsonResult(dt);
        }

        

        //[NonAction]
        //private DataTable BindGrid(DataTable grid, DataTable dt1, StringBuilder barcode, int a2)
        //{



        //    aa = 0; a3 = 1; a1 = 0; rowcount = 0; cnt2 = 0;
        //    int colcount = 0;
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        grid.Rows.Add();
        //        rowcount = grid.Rows.Count - 1;
        //        colcount = grid.Columns.Count - 1;
        //        foreach (DataColumn col in dt1.Columns)
        //        {
        //            grid.Rows[colcount][col.ColumnName] = dt1.Rows[i][col.ColumnName].ToString();
        //            colcount++;
        //        }
        //            cnt2++; rowcount++;
        //    }
           

        //    return grid;

        //}


        [HttpGet("auto/{compcode}/{asptblprolotid}/{pono}")]
        public async Task<IActionResult> auto(Int64 compcode, Int64 asptblprolotid, string pono)
        {
            CommonDetails com = new CommonDetails();
            DataTable dt1 = new DataTable();
            string barindex = ""; 
            try
            {


                Class.Users.ScreenName = "ProductionEntry";
                string tbl = "asptblprolot";
                //Class.Users.Finyear = System.DateTime.Now.Year.ToString();
                Class.Users.Month = mas.getAbbreviatedName(System.DateTime.Now.Month);

                p.Finyear = Class.Users.Finyear;
                p.Compcode = compcode;

                if (p.Compcode > 0 && asptblprolotid <= 0)
                {
                    Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    Class.Users.Query = "select max(a." + tbl + "1id)+1 as id from " + tbl + " a join gtcompmast b on a.Compcode = b.gtcompmastid  where a.finyear='" + Class.Users.Finyear + "'   and a.Compcode='" + Class.Users.COMPCODE + "' ;";
                    DataSet dsid = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    DataTable dtid = dsid.Tables[0];
                    Class.Users.Query = "";

                    if (dt1.Rows.Count > 0)
                    {
                        //if (p.Sequencewise == Class.Users.BarCodeType)
                        //{
                        DataTable dt2 = await com.shortcode1(Class.Users.Finyear, Class.Users.BarCodeType, compcode, Class.Users.ScreenName, tbl);
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) <= 0)
                        {

                            p.Asptblprolot1id = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
                            p.Shortcode = dt1.Rows[0]["shortcode"].ToString();
                            p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
                        }
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) > 0)
                        {
                            p.Asptblprolot1id = Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString());
                            p.Shortcode = dt2.Rows[0]["shortcode"].ToString();
                            p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dtid.Rows[0]["id"].ToString();
                        }


                        p.Pono = Convert.ToString(p.Pono);
                        p.Asptblprolot1id = Convert.ToInt64("0" + p.Asptblprolot1id);



                    }

                }
                else
                {
                    Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    p.Pono = pono;
                    p.Asptblprolotid = asptblprolotid;

                }
            }
            catch (Exception EX) { throw new Exception(EX.Message); }
            return new JsonResult(p.Pono);

        }


        [HttpPost]
        [Route("ProductionEntrys/{OBJ1}")]
        public async Task<IActionResult> ProductionEntrys(string OBJ1)
        {
              DataTable dt = (DataTable)JsonConvert.DeserializeObject(OBJ1.ToString(), (typeof(DataTable)));
          
           
            try
            {




              //  if (dt.Rows[0]["Pono"].ToString() != "" && Convert.ToInt32(dt.Rows[0]["Gridrowcount"].ToString()) >= 0)
               // {
                    DataTable dt20 = await p.DocIdDetails();
                    p.Docid = Convert.ToInt64(dt20.Rows[0]["docid"].ToString());
                    p.Asptblprolotid = Convert.ToInt64("0" + dt.Rows[0]["Asptblprolotid"].ToString());
                    p.Asptblprolot1id = Convert.ToInt64("0" + dt.Rows[0]["Asptblprolot1id"].ToString());
                    p.Shortcode = Convert.ToString(dt.Rows[0]["Shortcode"].ToString());
                p.Finyear = Class.Users.Finyear;// Convert.ToString(dt.Rows[0]["FinYear"].ToString());
                    p.Orderno = Convert.ToString(dt.Rows[0]["Orderno"].ToString());
                    p.Proddate = Convert.ToDateTime(dt.Rows[0]["Proddate"].ToString().Substring(0, 10)).ToString("yyyy-MM-dd");
                    p.Prodno = Convert.ToString(dt.Rows[0]["Prodno"].ToString());
                    p.Compcode = Convert.ToInt64("0" + dt.Rows[0]["Compcode"].ToString());
                    p.Buyer = Convert.ToInt64("0" + dt.Rows[0]["Buyer"].ToString());
                    p.Pono = Convert.ToString(dt.Rows[0]["Pono"].ToString());
                    p.Stylename = Convert.ToInt64("0" + dt.Rows[0]["Stylename"].ToString());
                    p.Processname = Convert.ToInt64("0" + dt.Rows[0]["Processname"].ToString());
                    p.Processtype = Convert.ToString(dt.Rows[0]["Processtype"].ToString());
                    p.Panelmistake = Convert.ToString(dt.Rows[0]["Processtype"].ToString());
                    p.Notes = Convert.ToString(dt.Rows[0]["Notes"].ToString());
                    p.Gridrowcount = Convert.ToInt32(dt.Rows[0]["Gridrowcount"].ToString());
                    if (p.Processtype == "INWARD") { p.Inward1 = "INWARD"; p.Stitching = "T"; p.Inward = "T"; p.Issuetype = "INWARD"; p.Rechecking = "F"; } else { p.Inward = "F"; }
                    if (p.Processtype == "DELIVERY") { p.Delivery1 = "DELIVERY"; p.Stitching = "T"; p.Delivery = "T"; p.Issuetype = "DELIVERY"; p.Rechecking = "F"; } else { p.Delivery = "F"; }
                    if (p.Processtype == "REWORK") { p.Rework1 = "REWORK"; p.Inward = "T"; p.Stitching = "T"; p.Restitching = "T"; p.Issuetype = "REWORK"; p.Delivery = "T"; p.Rechecking = "F"; }
                    else
                    { p.Restitching = "F"; }

                    p.Compcode1 = Class.Users.COMPCODE;
                    p.Username = Class.Users.USERID;
                    p.Createdby = Convert.ToString(Class.Users.HUserName);
                    p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    p.Modifiedon = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd"));
                    p.Modifiedby = Class.Users.HUserName;
                    p.Ipaddress = Class.Users.IPADDRESS;
                    if (p.Active == "true") { p.Active = dt.Rows[0]["Active"].ToString(); } else { p.Active = dt.Rows[0]["Active"].ToString(); }
               // DataTable dataTable = await p.SelectCommond();
                    Class.Users.dt = await p.select(p.Asptblprolotid, p.Asptblprolot1id, p.Compcode, p.Finyear, p.Shortcode, p.Proddate, p.Prodno, p.Buyer, p.Pono, p.Orderqty, p.Stylename, p.Lotno, p.Bundle, p.Processname, p.Processtype, p.Productioncancel, p.Active, p.Issuetype, p.Restitching, p.Rechecking, p.Inward, p.Delivery);
                    if (Class.Users.dt.Rows.Count != 0)
                    { }
                    else if (Class.Users.dt.Rows.Count != 0 && p.Asptblprolotid == 0 || p.Asptblprolotid == 0)
                    {
                        await auto(p.Compcode, p.Asptblprolotid, p.Pono);
                    //await InterfaceClass.InsertCommond();
                   await p.InsertCommond();
                  //await  p.InsertCommond(p.Asptblprolot1id, p.Docid, p.Finyear, p.Shortcode, p.Proddate, p.Prodno, p.Compcode, p.Buyer, p.Pono, p.Orderqty, p.Stylename, p.Lotno, p.Bundle, p.Gridrowcount, p.Processname, p.Processtype, p.Productioncancel, p.Active, p.Compcode1, p.Username, p.Createdby, p.Createdon, p.Modifiedon, p.Modifiedby, p.Ipaddress, p.Issuetype, p.Restitching, p.Rechecking, p.Inward, p.Delivery, p.Notes);

                    }
                    else
                    {
                        //string up = "update  asptblprolot   set proddate=date_format('" + p.Modifiedby + "', '%Y-%m-%d'), compcode1='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',modified=date_format('" + p.Modifiedby + "','%Y-%m-%d'), modifiedby='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "' where asptblprolotid='" + p.Asptblprolotid + "'";
                        //await Utility.ExecuteNonQuery(up);
                    await p.UpdateCommond();
                    }
                //}
                //else
                //{
                //    msg = "Invalid Grid Record  No Data Found";

                //}
            }
            catch (Exception ex)
            {

                string comit1 = "commit";
                await Utility.ExecuteNonQuery(comit1);
                msg = ex.Message;

            }

            //string selmax = "select max(asptblprolotid) as asptblprolotid   from  asptblprolot   where pono='" + p.Pono + "'  and finyear ='" + p.Finyear + "' and compcode ='" + p.Compcode + "'";
            //    DataSet dsmax = await Utility.ExecuteSelectQuery(selmax, "asptblprolot");
            DataTable dtmax = await p.SelectCommond();
                if (dtmax != null && Convert.ToInt64("0" + dtmax.Rows[0]["asptblprolotid"].ToString()) > 0)
                {
                    p.Asptblprolotid = Convert.ToInt64("0" + dtmax.Rows[0]["asptblprolotid"].ToString());
                }
          

            return new JsonResult(p.Pono + "," + p.Asptblprolotid + "," + p.Compcode + "," + p.Finyear + "," + p.Asptblprolot1id);
        }

        string maxid = "";

        [NonAction]
        public  async Task colorid(string s)
        {
            try
            {
                

                DataTable dt = await cc.select("select asptblcolmasid,colorname from  asptblcolmas where active='T' and colorname='" + s + "'  order by 2", "asptblcolmas");
                coid = "";
                pp.Colorname = Convert.ToString(dt.Rows[0]["asptblcolmasid"].ToString());


            }
            catch (Exception EX)
            { }
           
        }

        [NonAction]
        public async Task sizeid(string s)
        {
            try
            {
                
                DataTable dt =  await cc.select("select asptblsizmasid from  asptblsizmas where active='T' AND sizename='" + s + "' order by 1", "asptblsizmas");

                if (dt.Rows.Count > 0)
                {
                    siid = "";
                    pp.Sizename = Convert.ToString(dt.Rows[0]["asptblsizmasid"].ToString());
                }
            }
            catch (Exception EX)
            { }
        }

        [HttpPost]
        [Route("ProductionEntrysss/{users}/{pos}/{uniqueasptblpurid}/{uniquecompcode}/{uniquefinyear}")]
        public async Task<IActionResult> ProductionEntrysss(string users, string pos, Int64 uniqueasptblpurid, Int64 uniquecompcode, string uniquefinyear)
        {
           
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(users.ToString(), (typeof(DataTable)));
            int i = 0, j = 1; int cnt = dt.Rows.Count;
            if (cnt >= 0)
            {
                string sel2 = "select max(asptblprolotid) id   from  asptblprolot   where  compcode='" + uniquecompcode + "'  and finyear='" + Class.Users.Finyear + "' ";
                DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "asptblprolot");
                DataTable dt2 = ds2.Tables["asptblprolot"];
                Int64 maxid = Convert.ToInt64("0" + dt2.Rows[0]["id"].ToString());
                string sel3 = "select *   from  asptblprolot   where  compcode='" + uniquecompcode + "'  and finyear='" + Class.Users.Finyear + "' AND asptblprolotid='" + maxid + "'  ";
                DataSet ds3 = await Utility.ExecuteSelectQuery(sel3, "asptblprolot");
                DataTable dt3 = ds3.Tables["asptblprolot"];
                if (dt3.Rows.Count > 0)
                {
                   
                    pp.Pono = dt3.Rows[0]["Pono"].ToString();                  
                    pp.Compcode = Convert.ToInt64("0" + dt3.Rows[0]["Compcode"].ToString());
                    pp.Finyear = Class.Users.Finyear;
                    pp.Asptblprolotid = Convert.ToInt64("0" + dt3.Rows[0]["Asptblprolotid"].ToString());
                    pp.Asptblprolot1id = Convert.ToInt64("0" + dt3.Rows[0]["Asptblprolot1id"].ToString());
                    pp.Asptblpurid = Convert.ToInt64("0" + dt.Rows[0]["Asptblpurid"].ToString());
                    pp.Orderqty = Convert.ToInt64("0" + dt.Rows[0]["pcs"].ToString());
                    pp.Process = Convert.ToString(dt3.Rows[0]["Processname"].ToString());                 
                    pp.Processname = Convert.ToInt64("0" + dt3.Rows[0]["Processname"].ToString());
                    pp.Issuetype = Convert.ToString(dt3.Rows[0]["Processtype"].ToString());
                    pp.Processtype = Convert.ToString(dt3.Rows[0]["Processtype"].ToString());
                    pp.Panelmistake = Convert.ToString(dt3.Rows[0]["Processtype"].ToString());                   
                    if (pp.Processtype == "INWARD") { pp.Inward1 = "INWARD"; pp.Stitching = "T"; pp.Inward = "T"; pp.Issuetype = "INWARD"; pp.Rechecking = "F"; } else { pp.Inward = "F"; }
                    if (pp.Processtype == "DELIVERY") { pp.Delivery1 = "DELIVERY"; pp.Stitching = "T"; pp.Delivery = "T"; pp.Issuetype = "DELIVERY"; pp.Rechecking = "F"; } else { pp.Delivery = "F"; }
                    if (pp.Processtype == "REWORK") { pp.Rework1 = "REWORK"; pp.Inward = "T"; pp.Stitching = "T"; pp.Restitching = "T"; pp.Issuetype = "REWORK"; pp.Delivery = "T"; pp.Rechecking = "F"; } else { pp.Restitching = "F"; }
                    pp.Modifiedby = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd"));

                    for (i = 0; i < cnt; i++)
                    {
                        pp.Sb.Clear();
                        pp.Asptblprolotdetid = Convert.ToInt64("0" + dt.Rows[i]["Asptblprolotdetid"].ToString());
                        pp.Asptblpurdetid = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                        pp.Asptblpurdet1id = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                        pp.Barcode = Convert.ToString(dt.Rows[i]["QrCode"].ToString());

                       await colorid(dt.Rows[i]["colorname"].ToString());                      
                      await  sizeid(dt.Rows[i]["sizename"].ToString());                              
                        pp.Processcheck = "T";
                     
                        string sel1 = "select asptblprolotdetid   from  asptblprolotdet   where   asptblpurdet1id='" + pp.Asptblpurdet1id + "' and barcode='" + pp.Barcode + "' and asptblpurdetid='" + pp.Asptblpurdetid + "' and asptblpurid='" + pp.Asptblpurid + "' and asptblprolotid='" + pp.Asptblprolotid + "' and asptblprolot1id='" + pp.Asptblprolot1id + "' and compcode='" + pp.Compcode + "' and  pono='" + pp.Pono + "'  and  colorname='" + pp.Colorname + "' and sizename='" + pp.Sizename + "' and processtype='" + pp.Processtype + "' and processname='" + pp.Process + "' and  processcheck='" + pp.Processcheck + "' and  finyear='" + pp.Finyear + "' and    issuetype='" + pp.Issuetype + "' and  restitching ='" + pp.Restitching + "' and rechecking ='" + pp.Rechecking + "' and inward='" + pp.Inward + "' and delivery='" + pp.Delivery + "'  and panelmistake='" + pp.Panelmistake + "' ";
                        DataSet ds1 = await Utility.ExecuteSelectQuery(sel1, "asptblprolot");
                        DataTable dt1 = ds1.Tables["asptblprolot"];
                        if (dt1.Rows.Count != 0 && pp.Asptblprolotdetid == 0 || pp.Asptblprolotdetid == 0)
                        {
                            //  await pp.InsertCommond(pp.Asptblpurdet1id,pp.Barcode,pp.Asptblpurdetid,pp.Asptblpurid,pp.Asptblprolotid,pp.Asptblprolot1id,pp.Compcode,pp.Pono,pp.Colorname,pp.Sizename,pp.Orderqty,pp.Process,pp.Processcheck,pp.Finyear,pp.Processtype,pp.Issuetype,pp.Restitching,pp.Rechecking,pp.Inward,pp.Delivery,pp.Panelmistake,pp.Inward1,pp.Rework1,pp.Delivery1,pp.Modifiedby,pp.Notes);
                            await pp.InsertCommond();
                            // await pp.UpdateCommond();
                            string up1 = "update asptblpurdet1 set  panelmistake='" + pp.Panelmistake + "',issuetype='" + pp.Issuetype + "',RESTITCHING= '" + pp.Restitching + "',STITCHING= '" + pp.Stitching + "',rechecking='" + pp.Rechecking + "',inward='" + pp.Inward + "',delivery='" + pp.Delivery + "' , processcheck='T' where barcode='" + pp.Barcode + "' AND PONO='" + pp.Pono + "' AND COMPCODE='" + pp.Compcode + "' AND FINYEAR='" + pp.Finyear + "'";
                            await Utility.ExecuteNonQuery(up1);
                            pp.Sb.Clear();
                            pp.Sb.Append("Data Saved Successfully");
                        }
                        else
                        {
                             pp.Sb.Clear();
                            pp.Sb.Append("Updated");
                        }
              
                    }
                }
            }

            return new JsonResult(pp.Pono + "," + pp.Asptblpurid + "," + pp.Compcode + "," + pp.Finyear+","+pp.Sb.ToString());
        }



        [HttpGet("BarcodeChange/{compcode}/{processtype}/{pono}/{txtbarcode}")]
        public async Task<IActionResult> BarcodeChange(string compcode, string processtype, string pono, string txtbarcode)
        {
            StringBuilder barscanning = new StringBuilder();DataTable dt =null;
            int rowcount = 0;string lblcount = ""; int totalcount = 0; copyDataTable = null;
            if (txtbarcode.Length >= Class.Users.Digit)
            {
                string source = ""; Class.Users.UserTime = 0;
                source = txtbarcode.Trim();
                barscanning.Clear();
                rowcount = 0;
                string[] data1 = source.Split('-'); Int64 aa = 0; int cnt2 = 1;
                if (data1.Length == 2)
                {  
                    Int64 a1 = Convert.ToInt64(data1[0]);
                    Int64 a2 = Convert.ToInt64(data1[1]);
                    Int64 a3 = 1;
                    a3 += a2 - a1; Class.Users.dt1 = null;
                    for (aa = a1; aa <= a2; aa++)
                    {
                        barscanning.Clear();
                        try
                        {
                            if (aa.ToString().Length == Class.Users.Digit) { barscanning.Append(aa.ToString()); } else { barscanning.Append("0"+aa.ToString()); }
                            dt = await Utility.SQLQuery("select count(asptblprolotdetid) barcode from asptblprolotdet  where  barcode='" + barscanning.ToString() + "'  and compcode='" + compcode + "' and  pono='" + pono + "'  and  processcheck='T' and  issuetype='" + processtype + "' and  restitching ='T' and rechecking ='F' AND INWARD='T'  and delivery='F' ");
                            string s = dt.Rows[0]["barcode"].ToString();
                            if (Convert.ToInt32(s) >= 1)
                            {
                                 lblcount = "Child Record Found  .  " + a2.ToString()+  a3 + " of " + cnt2.ToString(); cnt2++;
                                
                            }
                            else
                            {

                                copyDataTable = await barcode(compcode, processtype,pono, barscanning.ToString());

                                if (copyDataTable.Rows.Count > 0)
                                {
                                    if (Class.Users.dt1 == null)
                                    {
                                        Class.Users.dt1 = new DataTable();                                        
                                        Class.Users.dt1 = cc.CopyColumn(Class.Users.dt1, copyDataTable);
                                    }
                                    Class.Users.dt1= await cc.CopyRows(Class.Users.dt1, copyDataTable, barscanning);
                                    lblcount = a3 + " of " + cnt2.ToString();
                                    rowcount++; cnt2++;
                                    barscanning.Clear();

                                }
                                else
                                {
                                    lblcount = "Invalid BarCode  : "+a3 + " of " + cnt2.ToString();
                                    rowcount++; cnt2++;
                                    if (a3 == cnt2)
                                    {

                                        lblcount = " Invalid BarCode ";
                                        txtbarcode = "";  barscanning.Clear();
                                    }
                                }

                            }
                        }
                        catch (Exception ex) { lblcount=ex.Message.ToString(); }
                    }
                   
                }
                else
                {
                    if (txtbarcode.Length == Class.Users.Digit)
                    {
                        barscanning.Clear();
                        barscanning.Append(txtbarcode);
                        try
                        {
                             dt = await Utility.SQLQuery("select count(asptblprolotdetid) barcode from asptblprolotdet  where  barcode='" + barscanning + "'  and compcode='" + compcode + "' and  pono='" + pono + "'  and  processcheck='T' and  issuetype='" + processtype + "' and  restitching ='F' and rechecking ='F' AND INWARD='T'  and delivery='F' ");
                            string s = dt.Rows[0]["barcode"].ToString();
                            if (Convert.ToInt32(s) >= 1)
                            { 
                                lblcount= "Child Record Found  .  " + data1[0].ToString(); 
                            }
                            else
                            {
                                 Class.Users.dt1 = null;
                                Class.Users.dt1 = await barcode(compcode, processtype,pono, txtbarcode.ToString());

                                if (Class.Users.dt1.Rows.Count > 0)
                                {
                                   
                                    lblcount = "Count :" +lblcount  + " Of " + totalcount.ToString();
                                    
                                 
                                    barscanning.Clear();
                                }
                                else
                                {
                                    lblcount = "Count :" + lblcount + " Of " + totalcount.ToString();
                                   
                                }

                            }
                        }
                        catch (Exception ex) { lblcount=ex.Message.ToString(); }
                    }
                  
                }


            }
            else
            {
                
                lblcount = "Invalid Barcode " + txtbarcode;
            }
            if (Class.Users.dt1 != null) {  } else {
                Class.Users.dt1 = new DataTable();
                Class.Users.dt1 = cc.CopyColumn(Class.Users.dt1, dt);
                Class.Users.dt1.Rows.Add();
                Class.Users.dt1.Rows[0]["barcode"] = lblcount;
            }
            return new JsonResult(Class.Users.dt1);
        }



        [HttpGet("GridLoadColor/{Asptblprolotid}/{Compcode}")]
        public async Task<JsonResult> GridLoadColor(Int64 Asptblprolotid,  string Compcode)
        {

         DataTable   dt1 =await Utility.SQLQuery("select c.asptblprolotdetid,a.asptblprolotid,a.asptblprolot1id,b.compcode,a.pono,c.barcode,c.asptblpurdetid,c.asptblpurid, g.colorname,h.sizename,c.orderqty,c.processname from asptblprolot a join gtcompmast b on a.compcode=b.gtcompmastid  join asptblprolotdet c on c.asptblprolotid=a.asptblprolotid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid join asptblcolmas g on g.asptblcolmasid=c.colorname join asptblsizmas h on h.ASPTBLSIZMASID=c.sizename  where  b.compcode='" + Compcode + "'  and a.asptblprolotid='" + Asptblprolotid + "'");

            return new JsonResult(dt1);
        }

        [HttpGet("PonoDetails/{ccode}/{type}")]
        public async Task<IActionResult> PonoDetails(string ccode, string type)
        {
            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();
            if (type == "INWARD")
            {
                dt=await CC.select("select DISTINCT a.Asptblpurid,a.Pono from asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid  LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  a.Active='T' and b.gtcompmastid='" + ccode + "'  order by 1 desc", "asptblpur");
            }
            if (type == "DELIVERY")
            {
                dt = await CC.select("select DISTINCT a.Asptblpurid,a.Pono from asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid JOIN asptblprolotdet C ON A.PONO=C.PONO AND A.COMPCODE=C.COMPCODE AND C.inward1='INWARD' LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  a.Active='T' and b.gtcompmastid='" + ccode + "'  order by 1 desc", "asptblpur");
            }
            if (type== "REWORK")
            {
                dt = await CC.select("select DISTINCT a.Asptblpurid,a.Pono from asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid JOIN asptblcutpanretdet C ON A.PONO=C.PONO AND A.COMPCODE=C.COMPCODE AND C.issuetype='CHECKING MISTAKE' AND C.remarks='STITCHING MISTAKE' OR   C.issuetype='STITCHING MISTAKE' AND C.remarks='STITCHING MISTAKE'  join asptblprolot d on d.Compcode=a.Compcode and d.Pono=a.Pono and d.Pono=c.Pono LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  a.Active='T' and b.gtcompmastid='" + ccode + "'  order by 1 desc", "asptblpur");
            }
            return new JsonResult(dt);
        }

        [HttpGet("ponochange/{ccode}/{pono}")]
        public async Task<IActionResult> ponochange(string ccode,string pono)
        {
            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();
         
            dt = await CC.select("select distinct MIN(A.barcode) AS  MINID , MAX(A.barcode) MAXID,count(a.barcode) cnt,c.Orderno,c.Styleref,d.asptblstymasid from asptblpurdet1 a join gtcompmast b on a.compcode=b.gtcompmastid  join asptblpur c on c.Asptblpurid=a.Asptblpurid join asptblstymas d on d.asptblstymasid=c.Stylename where   b.gtcompmastid='" + ccode + "' and a.Pono='" + pono + "'  group by c.Orderno,c.Styleref,d.asptblstymasid ", "asptblpurdet1");
            if (dt.Rows[0]["MINID"].ToString() == "")
            {
                return new JsonResult("No Data Found..");
            }
            
            return new JsonResult(dt);
        }




        [HttpGet("barcode/{combocompcode}/{type}/{pono}/{bar}")]
        public  async  Task<DataTable> barcode(string combocompcode, string type, string pono, string bar)
        {
            CommonDetails CC = new  CommonDetails();
 
            if (type == "INWARD")
            {
                copyDataTable = await CC.select("select distinct '' as SNo, f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.Sizename,f.Orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid  where  b.gtcompmastid='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND F.CUTTING='F'  AND F.STITCHING= 'F' AND F.CHECKING='F' ORDER BY 1", "asptblpur");
            }
            if (type == "DELIVERY")
            {
                copyDataTable = await CC.select("select DISTINCT '' as SNo, f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.Sizename,f.Orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid JOIN asptblpur g on g.Pono=a.Pono and g.Pono=f.Pono JOIN asptblpurdet h on h.Asptblpurid=g.Asptblpurid and h.Pono=g.Pono   where  b.gtcompmastid='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND  F.STITCHING= 'T' and f.issuetype='INWARD' AND F.PANELMISTAKE='INWARD' ORDER BY 1", "asptblpur");

            }
            if (type == "REWORK")
            {
                copyDataTable = await CC.select("select  DISTINCT  f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.Sizename,f.Orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid   where  b.Compcode='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND f.issuetype='CHECKING MISTAKE' AND f.REMARKS='STITCHING MISTAKE' and F.RECHECKING= 'T' union all select  DISTINCT  f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid   where  b.Compcode='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND f.issuetype='STITCHING MISTAKE' AND f.REMARKS='STITCHING MISTAKE' and F.RESTITCHING= 'F' union all select  DISTINCT  f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid   where  b.Compcode='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND f.issuetype='CHECKING MISTAKE' AND f.REMARKS='REWORK' and F.RESTITCHING= 'F' union all select  DISTINCT  f.Barcode, c.Asptblpurdetid,a.Asptblpurid,a.Pono,d.Colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.Compcode=b.gtcompmastid   join asptblpurdet c on c.Asptblpurid=a.Asptblpurid  and c.Compcode=a.Compcode and c.Compcode=b.gtcompmastid  join asptblcolmas d on d.Asptblcolmasid=c.Colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.Sizename join asptblpurdet1 f on f.Asptblpurdetid=c.Asptblpurdetid and f.Asptblpurid=a.Asptblpurid   where  b.gtcompmastid='" + combocompcode + "'  and a.Pono='" + pono + "' and f.Barcode='" + bar + "' AND f.issuetype='REWORK' AND f.REMARKS='REWORK' and F.RESTITCHING= 'T' AND NOT F.PANELMISTAKE='CHECKING REWORK' AND  NOT F.PANELMISTAKE='CHECKING DELIVERY' ORDER BY 1", "asptblpur");
            }
           
            return copyDataTable;
        }


        [NonAction]
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

     





    }

    
}

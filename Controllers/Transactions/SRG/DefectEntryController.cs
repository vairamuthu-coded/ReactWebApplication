
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReactWebApplication.Models.Transactions.SRG;
using System.Data;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Humanizer;
using System.Runtime.ConstrainedExecution;

namespace ReactWebApplication.Controllers.Transactions.SRG
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefectEntryController : ControllerBase
    {
        Models.TreeView.userRights sm = new Models.TreeView.userRights();
        Models.Transactions.SRG.DefectEntry p = new DefectEntry();
        CommonDetails cc = new CommonDetails();
        Models.Transactions.SRG.DefectEntryDet pp = new DefectEntryDet();
        Models.Masters.Master mas = new Models.Masters.Master();
        Models.CommonClasss comClass = new Models.CommonClasss();
        DataTable copyDataTable;
        StringBuilder barscanning = new StringBuilder();
        string coid = "", siid = "", fabid = "";
        List<string> _siz = new List<string>();
        List<string> _col = new List<string>();
        List<string> _head = new List<string>();
        string screen = ""; int aa = 0, a3 = 1, a1 = 0, cnt2 = 1, rowcount=0;

        [HttpGet("usercheck/{autotable}/{screen}")]
        public async Task<IActionResult> usercheck(string autotable, string screen)
        {
            DataTable dt1 = await sm.usercheck(autotable, screen);
            return Ok(dt1);
        }

        [HttpGet("ponochange/{compcode}/{pono}")]
        public async Task<IActionResult> ponochange(string compcode, string pono)
        {
            DataTable dt = new DataTable();

            CommonDetails CC = new CommonDetails();
            dt = await Utility.SQLQuery("SELECT  MIN(A.barcode) AS  MINID , MAX(A.barcode1) MAXID ,f.stylename, f.asptblstymasid, g.processname,g.asptblpromasid ,a.orderno,a.styleref FROM   asptblpur  a join gtcompmast c on a.compcode=c.gtcompmastid join asptblbuymas d on d.asptblbuymasid=a.buyer  join asptblsizgrp e on e.asptblsizgrpid=a.sizegroup  join asptblstymas f on f.asptblstymasid=a.stylename join asptblpromas g on g.asptblpromasid=a.processname  where  c.gtcompmastid='" + compcode + "' and a.pono='" + pono + "' group by f.stylename, f.asptblstymasid, g.processname,g.asptblpromasid ,a.orderno,a.styleref ");
            //dt =  await Utility.SQLQuery("SELECT a.asptblpurid,a.asptblpur1id,a.pono,a.shortcode, a.podate,a.orderqty,a.bundle,a.size, c.compcode,c.compname,c.gtcompmastid, d.buyercode as buyer,d.buyercode, d.asptblbuymasid,e.sizegroup,e.asptblsizgrpid,f.stylename, f.asptblstymasid, g.processname,g.asptblpromasid ,a.orderqty, a.lotno,a.processtype,a.orderno,a.styleref,a.garmentimage FROM   asptblpur  a join gtcompmast c on a.compcode=c.gtcompmastid join asptblbuymas d on d.asptblbuymasid=a.buyer  join asptblsizgrp e on e.asptblsizgrpid=a.sizegroup join asptblstymas f on f.asptblstymasid=a.stylename join asptblpromas g on g.asptblpromasid=a.processname where a.PONO='" + pono + "' and c.gtcompmastid='" + compcode + "'");
            //if (dt.Rows[0]["MINID"].ToString() == "")
            //{
            //    return new JsonResult("No Data Found..");
            //}

            return new JsonResult(dt);
        }




        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {
            string sel1 = "SELECT a.asptblcutpanretid,a.asptblcutpanret1id,c.asptblpurid,c.asptblpur1id,a.docid,a.panelno ,a.cutpaneldate, a.Compcode,b.Compname,a.Buyer,a.Pono,a.stylename,c.orderno,c.Styleref,a.active,a.Issuetype,a.notes FROM  asptblcutpanret a join gtcompmast b on a.Compcode=b.gtcompmastid join asptblpur c on c.pono=a.pono and c.compcode=a.compcode";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblcutpanret");
            DataTable dt = ds.Tables["asptblcutpanret"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad/{id}/{compcode}")]
        public async Task<IActionResult> GridLoad(Int64 id, string compcode)
        {
            string sel1 = "SELECT a.asptblcutpanretid,a.asptblcutpanret1id,c.asptblpurid,c.asptblpur1id, a.Compcode,b.Compname,a.shortcode, a.panelno ,a.cutpaneldate,a.orderno, a.Buyer,a.Pono,a.stylename,c.orderno,c.Styleref,a.active,a.Issuetype,a.notes,a.docid,A.DEFECTTYPE,d.processname,concat(concat(c.Barcode,'-'),c.barcode1) as barcode FROM  asptblcutpanret a join gtcompmast b on a.Compcode=b.gtcompmastid join asptblpur c on c.pono=a.pono and c.compcode=a.compcode join asptblpromas d on d.asptblpromasid=c.Processname where a.asptblcutpanretid= '" + id + "' and b.gtcompmastid = '" + compcode + "'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblcutpanret");
            DataTable dt = ds.Tables["asptblcutpanret"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoadColor/{asptblcutpanretid}/{compcode}")]
        public async Task<JsonResult> GridLoadColor(Int64 Asptblcutpanretid, string Compcode)
        {

            DataTable dt1 = await Utility.SQLQuery("select c.asptblcutpanretdetid,a.asptblcutpanretid,a.asptblcutpanret1id,b.compcode,a.pono,c.barcode,c.asptblpurdetid,c.asptblpurid, g.colorname,h.sizename,c.pcs from asptblcutpanret a join gtcompmast b on a.compcode=b.gtcompmastid  join asptblcutpanretdet c on c.asptblcutpanretid=a.asptblcutpanretid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid join asptblcolmas g on g.asptblcolmasid=c.colorname join asptblsizmas h on h.ASPTBLSIZMASID=c.sizename  where  b.gtcompmastid='" + Compcode + "'  and a.asptblcutpanretid='" + Asptblcutpanretid + "'");

            return new JsonResult(dt1);
        }

        [HttpGet("BarcodeChange/{compcode}/{processtype}/{pono}/{txtbarcode}")]
        public async Task<IActionResult> BarcodeChange(string compcode, string processtype, string pono, string txtbarcode)
        {
            StringBuilder barscanning = new StringBuilder(); copyDataTable = null;
            DataTable dt = null;
            int rowcount = 0; string lblcount = ""; int totalcount = 0; 
            if (txtbarcode.Length >= Class.Users.Digit)
            {
                string source = ""; Class.Users.UserTime = 0;
                source = txtbarcode.Trim();
                barscanning.Clear();
                rowcount = 0;
                string[] data1 = source.Split('-'); Int64 aa = 0; int cnt2 = 1;
                if (data1.Length == 2)
                {
                    Class.Users.dt1 = null;
                    Int64 a1 = Convert.ToInt64(data1[0]);
                    Int64 a2 = Convert.ToInt64(data1[1]);
                    Int64 a3 = 1;
                    a3 += a2 - a1;
                    for (aa = a1; aa <= a2; aa++)
                    {
                        barscanning.Clear();
                        try
                        {
                            if (aa.ToString().Length == Class.Users.Digit) { barscanning.Append("0" + aa.ToString()); } else { barscanning.Append("0"+aa.ToString()); }
                            dt = await Utility.SQLQuery("select count(asptblcutpanretdetid) barcode from  asptblcutpanretdet   where  barcode='" + barscanning + "' and  compcode='" + compcode + "' and  pono='" + pono + "'  and Issuetype='" + processtype + "' ");

                            string s = dt.Rows[0]["barcode"].ToString();
                            if (Convert.ToInt32(s) >= 1)
                            {
                                lblcount = "Child Record Found  .  " + a2.ToString() + " of " + cnt2.ToString(); cnt2++;
                            }
                            else
                            {
                                copyDataTable = await barcode(compcode, processtype, pono, barscanning.ToString());
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
                                    lblcount = "Invalid BarCode : " +a3 + " of " + cnt2.ToString();
                                    rowcount++; cnt2++;
                                    if (a3 == cnt2)
                                    {
                                        lblcount = " Invalid BarCode ";
                                        txtbarcode = ""; barscanning.Clear();
                                    }
                                }

                            }
                        }
                        catch (Exception ex) { lblcount = ex.Message.ToString(); }
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

                            dt = await Utility.SQLQuery("select count(asptblcutpanretdetid) barcode from  asptblcutpanretdet   where  barcode='" + barscanning + "' and  compcode='" + compcode + "' and  pono='" + pono + "'  and Issuetype='" + processtype + "' ");
                            string s = dt.Rows[0]["barcode"].ToString();
                            if (Convert.ToInt32(s) >= 1)
                            {
                               
                                lblcount = "Child Record Found  .  " + data1[0].ToString();
                                
                            }
                            else
                            {
                                Class.Users.dt1 = null;
                                Class.Users.dt1 = await barcode(compcode, processtype, pono, txtbarcode.ToString());

                                if (Class.Users.dt1.Rows.Count > 0)
                                {
                                    lblcount = "Count :" + lblcount + " Of " + totalcount.ToString();


                                    barscanning.Clear();
                                }
                                else
                                {
                                    lblcount = "Count :" + lblcount + " Of " + totalcount.ToString();

                                }

                            }
                        }
                        catch (Exception ex) { 
                            lblcount = ex.Message.ToString();

                        }
                    }

                }


            }
            else
            {
                
                lblcount = "Invalid Barcode " + txtbarcode;
            }

            if (Class.Users.dt1 != null) { } else {
                Class.Users.dt1 = new DataTable();
                Class.Users.dt1 = cc.CopyColumn(Class.Users.dt1, dt);               
                Class.Users.dt1.Rows.Add();
                Class.Users.dt1.Rows[0]["barcode"] = lblcount; }
            return new JsonResult(Class.Users.dt1);
        }

        [HttpGet("PonoDetails/{com}/{type}")]
        public async Task<IActionResult> PonoDetails(string com, string type)
        {
            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();
            if (type == "STITCHING MISTAKE")
            {
                dt = await CC.select(" select distinct x.asptblpurid, x.pono from ( select a.asptblpurid,a.pono,B.compcode,C.ISSUETYPE from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblprolot c on c.pono=a.PONO   AND B.GTCOMPMASTID=C.COMPCODE  AND a.active='T' and c.issuetype='INWARD' LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  b.gtcompmastid='" + com + "'  UNION ALL select a.asptblpurid,a.pono,B.compcode,C.ISSUETYPE from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblchk c on c.pono=a.PONO AND B.GTCOMPMASTID=C.COMPCODE  AND a.active='T' and c.processtype='DELIVERY' LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  b.gtcompmastid='" + com + "'   ) X     order by x.asptblpurid desc", "asptblpur");
            }
            if (type == "CHECKING MISTAKE")
            {
                dt = await CC.select("select distinct A.asptblpurID, A.pono,B.compcode from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblchk c on c.pono=a.PONO   AND B.GTCOMPMASTID=C.COMPCODE   AND a.active='T' JOIN ASPTBLCHK d on d.pono=a.pono and d.pono=c.pono  LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  B.gtcompmastid='" + com + "' AND C.ISSUETYPE='INWARD' order by A.asptblpurID desc", "asptblpur");

                if (Class.Users.dt.Rows.Count <= 0)
                {

                    dt = await CC.select("select distinct C.asptblpurID,C.pono,B.compcode from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblpurdet1 c on c.pono=a.PONO   AND B.GTCOMPMASTID=C.COMPCODE   AND a.active='T' JOIN ASPTBLCHK d on d.pono=a.pono and d.pono=c.pono LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where   E.PONO IS NULL and  B.gtcompmastid='" + com + "' AND C.DELIVERY='T' and C.ISSUETYPE='INWARD' order by C.pono desc", "asptblpur");
                }
            }
          
            return new JsonResult(dt);
        }

        [HttpGet("barcode/{com}/{type}/{pono}/{bar}")]
        public async Task<DataTable> barcode(string com, string type, string pono, string bar)
        {
           // CommonDetails CC = new CommonDetails();

            if (type == "STITCHING MISTAKE")
            {
                  copyDataTable = await Utility.SQLQuery("select  f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.STITCHING='T' AND F.Issuetype='INWARD' AND NOT F.panelmistake='CHECKING INWARD' UNION ALL select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.RESTITCHING='T' AND F.Issuetype='REWORK'  AND NOT F.PANELMISTAKE='REWORK' AND NOT F.PANELMISTAKE='CHECKING DELIVERY' UNION ALL select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.restitching='T' AND F.Issuetype='STITCHING MISTAKE' UNION ALL select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "'  AND F.Issuetype='DELIVERY' AND NOT F.PANELMISTAKE='CHECKING DELIVERY' AND NOT F.PANELMISTAKE='DELIVERY'");

            }
            if (type == "CHECKING MISTAKE")
            {
                copyDataTable = await Utility.SQLQuery("select f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid   where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.Issuetype='INWARD' AND F.CHECKING='T' UNION ALL select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid   where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.Issuetype='REWORK' AND F.RECHECKING='T' AND NOT F.PANELMISTAKE='CHECKING DELIVERY'  UNION ALL select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.asptblpur1id,a.compcode, a.pono,d.colorname,e.SIZENAME from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  a.compcode='" + com + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND  F.Issuetype='DELIVERY' AND NOT F.PANELMISTAKE='CHECKING DELIVERY' AND NOT F.PANELMISTAKE='DELIVERY'");

            }

            return copyDataTable;
        }




        [HttpGet("Remarks")]
        public async Task<IActionResult> Remarks()
        {

            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();

            dt = await comClass.select("select asptblremmasid,remarks from asptblremmas WHERE active='T'  order by 2", "asptblremmas");
            if (dt.Rows[0]["remarks"].ToString() == "")
            {
                return new JsonResult("No Data Found..");
            }

            return new JsonResult(dt);

        }

        [HttpGet("autos/{fin}/{ccode}")]
        public async Task<IActionResult> autos(string fin, string ccode)
        {
            Class.Users.ScreenName = "DefectEntry";
            fin = Class.Users.Finyear;

            DataTable dt1 = new DataTable(); DataTable dt11 = new DataTable();
            if (ccode != "")
            {
                CommonDetails CC = new CommonDetails();

                dt1 = await CC.autonumberload(fin, ccode, Class.Users.ScreenName, "asptblcutpanret");
                if (dt1.Rows.Count > 0)

                    dt11 = await CC.shortcode(fin, ccode, Class.Users.ScreenName, "asptblcutpanret");
                if (dt11.Rows.Count < 0) { }

            }


            return new JsonResult(dt11);
        }



        [HttpGet("autos/{fin}/{ccode}/{screen}/{tblname}")]
        public async Task<IActionResult> autos(string fin, string ccode, string screen, string tblname)
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

        [HttpGet("auto/{compcode}/{Asptblcutpanretid}/{pono}")]
        public async Task<IActionResult> auto(Int64 compcode, Int64 Asptblcutpanretid, string pono)
        {

            DataTable dt1 = new DataTable();
            string barindex = "";
            try
            {


                Class.Users.ScreenName = "DefectEntry";
                string tbl = "asptblcutpanret";
              
                Class.Users.Month = mas.getAbbreviatedName(System.DateTime.Now.Month);

                p.Finyear = Class.Users.Finyear;
                p.Compcode = compcode;

                if (p.Compcode > 0 && Asptblcutpanretid <= 0)
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
                        DataTable dt2 = await comClass.shortcode1(Class.Users.Finyear, Class.Users.BarCodeType, Class.Users.HCompcode, Class.Users.ScreenName, tbl);
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) <= 0)
                        {

                            p.Asptblcutpanretid = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
                            p.Shortcode = dt1.Rows[0]["shortcode"].ToString();
                            p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
                        }
                        if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) > 0)
                        {
                            p.Asptblcutpanretid = Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString());
                            p.Shortcode = dt2.Rows[0]["shortcode"].ToString();
                            p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dtid.Rows[0]["id"].ToString();
                        }


                        p.Pono = Convert.ToString(p.Pono);
                        p.Asptblcutpanret1id = Convert.ToInt64("0" + p.Asptblcutpanret1id);



                    }

                }
                else
                {
                    Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
                    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
                    DataTable dt = ds.Tables[0];
                    Class.Users.Query = "";
                    p.Pono = pono;
                    p.Asptblcutpanretid = Asptblcutpanretid;

                }
            }
            catch (Exception EX) { throw new Exception(EX.Message); }
            return new JsonResult(p.Pono);

        }


        [HttpPost]
        [Route("DefectEntry/{OBJ1}")]
        public async Task<IActionResult> DefectEntry(string OBJ1)
        {
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(OBJ1.ToString(), (typeof(DataTable)));


            try
            {
                if (dt.Rows.Count > 0) {         
                
             
                    string sel20 = "select count(a.docid)+1 docid from asptblcutpanret  a   where  a.PONO='" + dt.Rows[0]["pono"].ToString() + "' and a.compcode='" + dt.Rows[0]["compcode"].ToString() + "' and a.finyear='" + Class.Users.Finyear + "';";
                    DataSet ds20 = await Utility.ExecuteSelectQuery(sel20, "asptblcutpanret");
                    DataTable dt20 = ds20.Tables["asptblcutpanret"];

                    string sel21 = "select  stylename,processname from asptblpur  a   where  a.PONO='" + dt.Rows[0]["pono"].ToString() + "' and a.compcode='" + dt.Rows[0]["compcode"].ToString() + "' and a.finyear='" + Class.Users.Finyear + "';";
                    DataSet ds21 = await Utility.ExecuteSelectQuery(sel21, "asptblpur");
                    DataTable dt21 = ds21.Tables["asptblpur"];

                    p.Docid = Convert.ToInt64(dt20.Rows[0]["docid"].ToString());
                    p.Asptblcutpanretid = Convert.ToInt64("0" + dt.Rows[0]["Asptblcutpanretid"].ToString());
                    p.Asptblcutpanret1id = Convert.ToInt64("0" + dt.Rows[0]["Asptblcutpanret1id"].ToString());
                    p.Finyear = Class.Users.Finyear;
                    p.Panelno = Convert.ToString(dt.Rows[0]["Panelno"].ToString());
                    p.Shortcode = Convert.ToString(dt.Rows[0]["Shortcode"].ToString());
                    p.Cutpaneldate = Convert.ToDateTime(dt.Rows[0]["Cutpaneldate"].ToString().Substring(0, 10)).ToString("yyyy-MM-dd");               
                    p.Compcode = Convert.ToInt64("0" + dt.Rows[0]["Compcode"].ToString());
                    p.Buyer = Convert.ToInt64("0" + dt.Rows[0]["Buyer"].ToString());
                    p.Pono = Convert.ToString(dt.Rows[0]["Pono"].ToString());
                    p.Stylename = Convert.ToInt64("0"+ dt21.Rows[0]["stylename"].ToString());
                    p.Lotno = Convert.ToString(dt.Rows[0]["lotno"].ToString());
                    p.Bundle = Convert.ToString(dt.Rows[0]["bundle"].ToString());
                    p.Processname = Convert.ToInt64("0" + dt21.Rows[0]["processname"].ToString());
                    p.Issuetype = Convert.ToString(dt.Rows[0]["issuetype"].ToString());
                    p.Remarks = Convert.ToString(dt.Rows[0]["issuetype"].ToString());
                    p.Notes = Convert.ToString(dt.Rows[0]["notes"].ToString());
                    p.DefectType = Convert.ToString(dt.Rows[0]["defecttype"].ToString());
                    if (dt.Rows[0]["Issuetype"].ToString().Trim() == "STITCHING MISTAKE") { p.Delivery = "F"; p.Stitching = "T"; p.Restitching = "T"; p.Issuetype = "STITCHING MISTAKE"; } else { p.Stitching = "F"; }
                    if (dt.Rows[0]["Issuetype"].ToString().Trim() == "CHECKING MISTAKE") { p.Checking = "F"; p.Rechecking = "T"; p.Issuetype = "CHECKING MISTAKE"; } else { p.Checking = "F"; }
                    p.Orderqty = Convert.ToInt64("0" + dt.Rows[0]["orderqty"].ToString());
                    p.Username = Class.Users.USERID;
                    p.Createdby = Convert.ToString(Class.Users.HUserName);
                    p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToLongTimeString());
                    p.Modifiedon = Convert.ToDateTime(System.DateTime.Now.ToLongTimeString()).ToString("yyyy-MM-dd hh:mm:ss");
                    p.Modifiedby = Class.Users.HUserName;
                    p.Ipaddress = Class.Users.IPADDRESS;
                    DataTable dt0 = await comClass.select("select asptblcutpanretid   from  asptblcutpanret   where  asptblcutpanret1id='" + p.Asptblcutpanretid + "' and  compcode='" + p.Compcode + "' and  pono='" + p.Pono + "'  AND buyer='" + p.Buyer + "' AND processname='" + p.Processname + "' AND lotno='" + p.Lotno + "' AND bundle='" + p.Bundle + "' and  remarks='" + p.Remarks + "' and  Issuetype='" + p.Issuetype + "' and  cutting='" + p.Cutting + "' and  STITCHING='" + p.Stitching + "' and  checking='" + p.Checking + "' and  restitching='" + p.Restitching + "' and  rechecking='" + p.Rechecking + "' and  DEFECTTYPE='" + p.DefectType + "' ", "asptblcutpanret");
                    if (dt0.Rows.Count != 0) { }
                    else if (dt0.Rows.Count != 0 && p.Asptblcutpanretid == 0 || p.Asptblcutpanretid == 0)
                    {

                        await auto(p.Compcode, p.Asptblcutpanretid, p.Pono);

                        string ins = "insert into asptblcutpanret(asptblcutpanret1id,docid,shortcode,panelno,finyear,cutpaneldate,compcode,stylename,orderqty,pono,buyer,processname,lotno,Issuetype,remarks,cutting,STITCHING,checking,restitching,rechecking,compcode1,username,createdby,modifiedby,ipaddress,DEFECTTYPE,notes,modified)  VALUES('" + p.Asptblcutpanret1id + "','" + p.Docid + "','" + p.Shortcode + "','" + p.Panelno + "','" + p.Finyear + "',date_format('" + p.Cutpaneldate + "','%Y-%m-%d'),'" + p.Compcode + "','" + p.Stylename + "','" + p.Orderqty + "','" + p.Pono + "','" + p.Buyer + "','" + p.Processname + "','" + p.Lotno + "','" + p.Issuetype + "','" + p.Remarks + "','" + p.Cutting + "','" + p.Stitching + "','" + p.Checking + "','" + p.Restitching + "','" + p.Rechecking + "','" + p.Compcode1 + "','" + p.Username + "','" + p.Createdby + "','" + p.Modifiedby + "','" + p.Ipaddress + "','" + p.DefectType + "','" + p.Notes + "',date_format('" + p.Modifiedon + "','%Y-%m-%d %H:%i:%s'))";
                        await Utility.ExecuteNonQuery(ins);

                        DataTable dtmax = await p.SelectCommond();
                        if (dtmax != null && Convert.ToInt64("0" + dtmax.Rows[0]["Asptblcutpanretid"].ToString()) > 0)
                        {
                            p.Asptblcutpanretid = Convert.ToInt64("0" + dtmax.Rows[0]["Asptblcutpanretid"].ToString());
                            p.Asptblcutpanret1id = Convert.ToInt64("0" + dtmax.Rows[0]["Asptblcutpanret1id"].ToString());
                        }
                    }
                    else
                    { 
                        string up = "update  asptblcutpanret   set defecttype='" + p.DefectType+ "', notes='"+p.Notes+"', cutpaneldate=date_format('" + p.Modifiedon + "', '%Y-%m-%d'), compcode='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',modified=date_format('" + p.Modifiedon + "','%Y-%m-%d'), modifiedby='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "' where asptblcutpanretid='" + p.Asptblcutpanretid + "' and compcode='" + p.Compcode + "'";
                     await Utility.ExecuteNonQuery(up);
                     
                    }




                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
               

            }
           
            return new JsonResult(p.Pono + "," + p.Asptblcutpanretid + "," + p.Compcode + "," + p.Finyear + "," + p.Asptblcutpanret1id);
        }


        [HttpPost]
        [Route("DefectEntrysss/{users}/{pos}/{uniqueasptblpurid}/{uniquecompcode}/{uniquefinyear}")]
        public async Task<IActionResult> DefectEntrysss(string users, string pos, Int64 uniqueasptblpurid, Int64 uniquecompcode, string uniquefinyear)
        {

            DataTable dt = (DataTable)JsonConvert.DeserializeObject(users.ToString(), (typeof(DataTable)));
            int i = 0, j = 1; int cnt = dt.Rows.Count;
            if (cnt >= 0)
                {
                              Int64 maxid = uniqueasptblpurid;
          
                string sel3 = "select *   from  asptblcutpanret   where  compcode='" + uniquecompcode + "'  and finyear='" + Class.Users.Finyear + "' AND asptblcutpanretid='" + maxid + "'  ";
                DataSet ds3 = await Utility.ExecuteSelectQuery(sel3, "asptblcutpanretdet");
                DataTable dt3 = ds3.Tables["asptblcutpanretdet"];




                if (dt.Rows.Count > 0) 
                {

                   

                    if (cnt >= 0)
                    {
                        pp.Pono = dt3.Rows[0]["Pono"].ToString();
                        pp.Compcode = Convert.ToInt64("0" + dt3.Rows[0]["Compcode"].ToString());
                        pp.Finyear = dt3.Rows[0]["Finyear"].ToString();
                        pp.Asptblcutpanretid = Convert.ToInt64("0" + dt3.Rows[0]["Asptblcutpanretid"].ToString());
                        pp.Asptblcutpanret1id = Convert.ToInt64("0" + dt3.Rows[0]["Asptblcutpanret1id"].ToString());
                        pp.Asptblpurid = Convert.ToInt64("0" + dt.Rows[0]["Asptblpurid"].ToString());
                        pp.Orderqty = Convert.ToInt64("0" + dt.Rows[0]["pcs"].ToString());
                        p.Processname = Convert.ToInt64("0" + dt3.Rows[0]["processname"].ToString());
                        p.Issuetype = Convert.ToString(dt3.Rows[0]["issuetype"].ToString());
                        p.Remarks = Convert.ToString(dt3.Rows[0]["issuetype"].ToString());
                        p.Notes = Convert.ToString(dt3.Rows[0]["notes"].ToString());
                        p.DefectType = Convert.ToString(dt3.Rows[0]["DefectType"].ToString());
                        p.Modifiedon = Convert.ToDateTime(System.DateTime.Now.ToLongTimeString()).ToString("yyyy-MM-dd hh:mm:ss");
                        if (dt3.Rows[0]["Issuetype"].ToString().Trim() == "STITCHING MISTAKE") { p.Delivery = "F"; p.Rechecking = "F"; p.Stitching = "T"; p.Restitching = "T"; p.Issuetype = "STITCHING MISTAKE"; } else { p.Stitching = "F"; }
                        if (dt3.Rows[0]["Issuetype"].ToString().Trim() == "CHECKING MISTAKE") { p.Checking = "F"; p.Rechecking = "T"; p.Issuetype = "CHECKING MISTAKE"; } else { p.Checking = "F"; }

                        for (i = 0; i < cnt; i++)
                        {
                            barscanning.Clear(); 
                            pp.Asptblcutpanretdetid = Convert.ToInt64("0" + dt.Rows[i]["Asptblcutpanretdetid"].ToString());
                            pp.Asptblpurdetid = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                            pp.Asptblpurdet1id = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                            pp.Barcode = Convert.ToString(dt.Rows[i]["QrCode"].ToString());
                            await colorid(dt.Rows[i]["colorname"].ToString());
                            await sizeid(dt.Rows[i]["sizename"].ToString());
                            pp.Processcheck = "T";
                            pp.Pcs = 1;
                            DataTable dt1 = await comClass.select("select asptblcutpanretdetid   from  asptblcutpanretdet   where  barcode='" + dt.Rows[i]["Asptblcutpanretdetid"].ToString() + "' and  compcode='" + p.Compcode + "' and  pono='" + p.Pono + "'  and  colorname='" + pp.Colorname + "' and sizename='" + pp.Sizename + "' and Issuetype='" + pp.Issuetype + "' and  finyear='" + p.Finyear + "' and remarks='" + p.Remarks + "' and  cutting='" + p.Cutting + "' and  STITCHING='" + p.Stitching + "' and  checking='" + p.Checking + "' and  restitching='" + p.Restitching + "' and  rechecking='" + p.Rechecking + "' and modified=date_format('" + p.Modifiedon + "','%Y-%m-%d %H:%i:%s') ", "asptblcutpanretdet");
                            if (dt1.Rows.Count != 0) { }
                            else if (dt1.Rows.Count != 0 && pp.Asptblcutpanretdetid == 0 || pp.Asptblcutpanretdetid == 0)
                            {
                                string ins1 = "insert into asptblcutpanretdet(asptblcutpanretid,asptblcutpanret1id,asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,finyear,compcode,pono,colorname,sizename,pcs,Issuetype,processcheck,Remarks,cutting,STITCHING,checking,restitching,rechecking,notes,modified) values('" + pp.Asptblcutpanretid + "' ,'" + pp.Asptblcutpanret1id + "' ,'" + pp.Barcode + "' ,'" + pp.Barcode + "','" + pp.Asptblpurdetid + "','" + pp.Asptblpurid + "','" + Class.Users.Finyear + "', '" + pp.Compcode + "' ,'" + pp.Pono + "' , '" + pp.Colorname + "','" + pp.Sizename + "','" + pp.Pcs + "','" + p.Issuetype + "','T','" + p.Remarks + "','" + p.Cutting + "','" + p.Stitching + "','" + p.Checking + "','" + p.Restitching + "','" + p.Rechecking + "','" + p.Notes + "',date_format('" + p.Modifiedon + "','%Y-%m-%d %H:%i:%s'));";
                                await Utility.ExecuteNonQuery(ins1);

                                if (p.Issuetype.Trim() == "STITCHING MISTAKE")
                                {
                                    string up1 = "update  asptblpurdet1  set  remarks='" + p.Remarks + "', panelmistake='" + p.Issuetype + "',Issuetype='" + p.Issuetype + "',restitching='F',delivery='T', processcheck='F'  where barcode='" + pp.Barcode + "' and compcode='" + pp.Compcode + "' and finyear='" + pp.Finyear + "' and pono='" + pp.Pono + "'";
                                    await Utility.ExecuteNonQuery(up1);
                                }
                                if (p.Issuetype.Trim() == "CHECKING MISTAKE")
                                {
                                    string up1 = "update  asptblpurdet1  set remarks='" + p.Remarks + "', panelmistake='" + p.Issuetype + "',Issuetype='" + p.Issuetype + "',rechecking='" + p.Rechecking + "' ,delivery='T'  , processcheck='F' where barcode='" + pp.Barcode + "' and compcode='" + pp.Compcode + "' and finyear='" + pp.Finyear + "' and pono='" + pp.Pono + "'";
                                    await Utility.ExecuteNonQuery(up1);
                                }
                                pp.Sb.Clear();
                                pp.Sb.Append("Data Saved Successfully");

                            }
                            else
                            {
                                string up = "update  asptblcutpanretdet   set  notes='" + p.Notes + "' , modified=date_format('" + p.Modifiedon + "','%Y-%m-%d') where asptblcutpanretdetid='" + pp.Asptblcutpanretdetid + "'";
                             await   Utility.ExecuteNonQuery(up);
                                p.Sb.Clear();
                                p.Sb.Append("Updated");
                            }


                        }
                    }

                }
            }
            
            return new JsonResult(pp.Pono + "," + pp.Asptblpurid + "," + pp.Compcode + "," + pp.Finyear + "," + p.Sb.ToString());
        }


        string maxid, maxid1 = "";

        [NonAction]
        public async Task colorid(string s)
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

                DataTable dt = await cc.select("select asptblsizmasid from  asptblsizmas where active='T' AND sizename='" + s + "' order by 1", "asptblsizmas");

                if (dt.Rows.Count > 0)
                {
                    siid = "";
                    pp.Sizename = Convert.ToString(dt.Rows[0]["asptblsizmasid"].ToString());
                }
            }
            catch (Exception EX)
            { }
        }

   


    }
}

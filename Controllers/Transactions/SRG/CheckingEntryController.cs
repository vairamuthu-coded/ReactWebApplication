using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ReactWebApplication.Models.Transactions.SRG;
using System.Data;
using System.Reflection.Emit;
using System.Text;

namespace ReactWebApplication.Controllers.Transactions.SRG
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckingEntryController : ControllerBase
    {
        Models.TreeView.userRights sm = new Models.TreeView.userRights();
        Models.Transactions.SRG.CheckingEntry p = new CheckingEntry();
        CommonDetails cc = new CommonDetails();
        Models.Transactions.SRG.CheckingEntryDet pp = new CheckingEntryDet();
        Models.Masters.Master mas = new Models.Masters.Master();
        Models.CommonClasss comClass = new Models.CommonClasss();
        DataTable copyDataTable;
        StringBuilder barscanning = new StringBuilder();
        string coid = "", siid = "", fabid = "";
        List<string> _siz = new List<string>();
        List<string> _col = new List<string>();
        List<string> _head = new List<string>();
        string screen = ""; int aa = 0, a3 = 1, a1 = 0, cnt2 = 1, rowcount = 0;

  
        [HttpGet("usercheck/{autotable}/{screen}")]
        public async Task<IActionResult> usercheck(string autotable, string screen)
        {
            DataTable dt1 = await sm.usercheck(autotable, screen);
            return Ok(dt1);
        }

        //public async Task<IActionResult> usercheck()
        //{

        //    Class.Users.ScreenName = "CheckingEntry";
        //    screen = Class.Users.ScreenName; GlobalVariables.New_Flg = false;
        //    string sel0 = "select a.finyear from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where  b.gtcompmastid='" + Class.Users.COMPCODE + "' AND c.aliasname='" + Class.Users.ScreenName + "' and c.username='" + Class.Users.USERID + "' and c.compcode='" + Class.Users.COMPCODE + "'";
        //    DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, "asptblautogeneratemas");
        //    DataTable dt0 = ds0.Tables[0]; Class.Users.Digit = 9;
        //    if (dt0.Rows.Count > 0)
        //    {
        //        Class.Users.Finyear = dt0.Rows[0]["finyear"].ToString();
        //    }
        //    DataTable dt1 = await sm.headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, Class.Users.ScreenName);
        //    return Ok(dt1);

        //}


        [HttpGet("PonoDetails/{com}")]
        public async Task<IActionResult> PonoDetails(Int64 com)
        {

            string sel = "select Pono from  asptblpur where compcode='" + com + "' ;";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblpur");
            DataTable dt = ds.Tables["asptblpur"];
            return new JsonResult(dt);
        }

        [HttpGet("PonoDetails/{ccode}/{type}")]
        public async Task<IActionResult> PonoDetails(string ccode, string type)
        {
            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();
            if (type == "INWARD")
            {
                dt = await CC.select("select DISTINCT X.asptblpurID, X.PONO from( select DISTINCT A.asptblpurID, A.PONO from asptblpur A  join gtcompmast b on a.compcode=b.gtcompmastid  join asptblprolot c on c.pono=a.pono and c.compcode=b.gtcompmastid  LEFT JOIN  asptblordclomas D ON A.PONO=D.PONO AND D.ACTIVE='T' WHERE D.PONO IS NULL  AND  C.DELIVERY='T' and c.processtype='DELIVERY' and b.gtcompmastid='" + ccode + "' UNION ALL select DISTINCT A.asptblpurID, A.PONO from asptblpur A join gtcompmast b on a.compcode=b.gtcompmastid join asptblprolot c on c.pono=a.pono and c.compcode=b.gtcompmastid  LEFT JOIN  asptblordclomas D ON A.PONO=D.PONO AND D.ACTIVE='T' WHERE D.PONO IS NULL  AND  C.DELIVERY='T' and  c.processtype='REWORK' and b.gtcompmastid='" + ccode + "') X  ORDER BY X.asptblpurID DESC", "ASPTBLPUR");
            }
            if (type == "DELIVERY")
            {
                dt = await CC.select("select distinct a.asptblpurid,a.pono from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblprolot c on c.pono=a.pono and c.compcode=b.gtcompmastid   RIGHT JOIN  asptblchk D ON D.PONO=C.PONO AND  D.compcode=A.compcode and D.issuetype='INWARD' LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where  a.active='T' AND E.PONO IS NULL and b.gtcompmastid='" + ccode + "' order by a.asptblpurid desc", "ASPTBLPUR");
            }
            if (type == "REWORK")
            {
                dt = await CC.select("SELECT distinct X.asptblpurid, X.PONO FROM (select distinct a.asptblpurid, a.pono from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblprolot c on c.pono=a.pono and c.compcode=a.compcode and c.compcode=b.gtcompmastid  JOIN asptblcutpanretdet D ON D.pono=A.pono and D.issuetype='CHECKING MISTAKE'  AND  D.REMARKS='STITCHING MISTAKE'   AND  C.DELIVERY='T'  LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where  a.active='T' AND E.PONO IS NULL  and b.gtcompmastid='" + ccode + "'  union all select distinct a.asptblpurid,a.pono from asptblpur a join gtcompmast b on a.compcode=b.gtcompmastid join asptblprolot c on c.pono=a.pono and c.compcode=a.compcode and c.compcode=b.gtcompmastid   JOIN asptblcutpanret D ON D.pono=A.pono and D.issuetype='CHECKING MISTAKE'  AND  D.REMARKS='CHECKING MISTAKE' LEFT JOIN  asptblordclomas E ON E.PONO=A.PONO AND E.ACTIVE='T' where  E.PONO IS NULL AND  b.gtcompmastid='" + ccode + "' ) X order by  x.asptblpurid desc ", "ASPTBLPUR");
            }
            return new JsonResult(dt);
        }

        [HttpGet("ponochange/{compcode}/{pono}")]
        public async Task<IActionResult> ponochange(string compcode, string pono)
        {


            DataTable dt = new DataTable();
            CommonDetails CC = new CommonDetails();

            dt = await comClass.select("select MIN(A.barcode) AS  MINID , MAX(A.barcode) MAXID,count(a.barcode) cnt,b.orderno,b.styleref,c.asptblpromasid, b.asptblpurid,b.stylename from asptblpurdet1 a join asptblpur b on a.Asptblpurid=a.Asptblpurid and a.Pono=b.Pono and a.Compcode=b.Compcode and a.Asptblpur1id=b.Asptblpur1id join asptblpromas c on c.asptblpromasid=b.Processname where a.pono='" + pono + "' group by b.orderno,b.styleref,c.asptblpromasid ,b.asptblpurid,b.stylename", "asptblpurdet1");
            if (dt.Rows[0]["MINID"].ToString() == "")
            {
                return new JsonResult("No Data Found..");
            }

            return new JsonResult(dt);

        }

        [HttpGet("GridLoad")]
        public async Task<IActionResult> GridLoad()
        {
            string sel1 = "SELECT a.Asptblchkid,a.Asptblchk1id,c.asptblpurid,c.asptblpur1id,a.docid,a.checkno ,a.wdate, a.Compcode,b.Compname,a.Buyer,a.Pono,a.stylename,c.orderno,c.Styleref,a.active,a.Issuetype,a.notes FROM  Asptblchk a join gtcompmast b on a.Compcode=b.gtcompmastid join asptblpur c on c.pono=a.pono and c.compcode=a.compcode";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblchk");
            DataTable dt = ds.Tables["Asptblchk"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoad/{id}/{compcode}")]
        public async Task<IActionResult> GridLoad(Int64 id, string compcode)
        {
            string sel1 = "SELECT a.Asptblchkid,a.Asptblchk1id,c.asptblpurid,c.asptblpur1id, a.Compcode,b.Compname,a.shortcode, a.checkno ,a.wdate,a.orderno, a.Buyer,a.Pono,a.stylename,c.orderno,c.Styleref,a.active,a.Issuetype,a.notes,a.docid,A.DEFECTTYPE,d.processname,concat(concat(c.Barcode,'-'),c.barcode1) as barcode FROM  Asptblchk a join gtcompmast b on a.Compcode=b.gtcompmastid join asptblpur c on c.pono=a.pono and c.compcode=a.compcode join asptblpromas d on d.asptblpromasid=c.Processname where a.Asptblchkid= '" + id + "' and b.gtcompmastid = '" + compcode + "'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblchk");
            DataTable dt = ds.Tables["Asptblchk"];
            return new JsonResult(dt);
        }

        [HttpGet("GridLoadColor/{Asptblchkid}/{compcode}")]
        public async Task<JsonResult> GridLoadColor(Int64 Asptblchkid, string Compcode)
        {

            DataTable dt1 = await Utility.SQLQuery("select c.Asptblchkdetid,a.Asptblchkid,a.Asptblchk1id,b.compcode,a.pono,c.barcode,c.asptblpurdetid,c.asptblpurid, g.colorname,h.sizename,c.pcs from Asptblchk a join gtcompmast b on a.compcode=b.gtcompmastid  join Asptblchkdet c on c.Asptblchkid=a.Asptblchkid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid join asptblcolmas g on g.asptblcolmasid=c.colorname join asptblsizmas h on h.ASPTBLSIZMASID=c.sizename  where  b.gtcompmastid='" + Compcode + "'  and a.Asptblchkid='" + Asptblchkid + "'");

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
                            if (aa.ToString().Length == Class.Users.Digit) { barscanning.Append("0" + aa.ToString()); } else { barscanning.Append("0" + aa.ToString()); }
                            dt = await Utility.SQLQuery("select count(Asptblchkdetid) barcode from  Asptblchkdet   where  barcode='" + barscanning + "' and  compcode='" + compcode + "' and  pono='" + pono + "'  and Issuetype='" + processtype + "' ");

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

                                    Class.Users.dt1 = await cc.CopyRows(Class.Users.dt1, copyDataTable, barscanning);
                                    lblcount = a3 + " of " + cnt2.ToString();
                                    rowcount++; cnt2++;
                                    barscanning.Clear();
                                }
                                else
                                {
                                    lblcount = "Invalid BarCode  : " + a3 + " of " + cnt2.ToString();
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

                            dt = await Utility.SQLQuery("select count(Asptblchkdetid) barcode from  Asptblchkdet   where  barcode='" + barscanning + "' and  compcode='" + compcode + "' and  pono='" + pono + "'  and Issuetype='" + processtype + "' ");
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
                        catch (Exception ex)
                        {
                            lblcount = ex.Message.ToString();

                        }
                    }

                }


            }
            else
            {

                lblcount = "Invalid Barcode " + txtbarcode;
            }

            if (Class.Users.dt1 != null) { }
            else
            {
                Class.Users.dt1 = new DataTable();
                Class.Users.dt1 = cc.CopyColumn(Class.Users.dt1, dt);
                Class.Users.dt1.Rows.Add();
                Class.Users.dt1.Rows[0]["barcode"] = lblcount;
            }
            return new JsonResult(Class.Users.dt1);
        }


        [HttpGet("barcode/{com}/{type}/{pono}/{bar}")]
        public async Task<DataTable> barcode(string com, string type, string pono, string bar)
        {
            // CommonDetails CC = new CommonDetails();

            if (type == "INWARD")
            {
                Class.Users.Query = "select  DISTINCT f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "'  AND F.ISSUETYPE='REWORK' AND F.REMARKS='STITCHING MISTAKE' AND F.RESTITCHING='T' AND F.CHECKING='F' UNION ALL select  DISTINCT f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid  where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "'  AND F.ISSUETYPE='DELIVERY' AND F.PANELMISTAKE='DELIVERY' AND F.STITCHING='T' AND F.CHECKING='F' ORDER BY 1 ";
               copyDataTable = await cc.select(Class.Users.Query, "asptblpurdet1");
            }
            if (type == "DELIVERY")
            {
                Class.Users.Query = "select distinct f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid    where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "'  AND F.CHECKING='T' AND F.ISSUETYPE='INWARD' ORDER BY 1";
                copyDataTable = await cc.select(Class.Users.Query, "asptblpurdet1");
            }

            if (type == "REWORK")
            {
                Class.Users.Query = "select distinct  f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid JOIN asptblcutpanret g on g.pono=f.pono  where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND F.RECHECKING='T' AND f.ISSUETYPE='CHECKING MISTAKE' AND f.remarks='CHECKING MISTAKE'  UNION ALL select distinct  f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid JOIN asptblcutpanret g on g.pono=f.pono  where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND F.RECHECKING='F' AND F.DELIVERY='T' AND F.restitching='T' AND f.ISSUETYPE='DELIVERY' AND f.remarks='REWORK' UNION ALL select distinct  f.barcode, c.asptblpurdetid,a.asptblpurid,a.pono,d.colorname,e.SIZENAME,f.orderqty from  asptblpur a  join gtcompmast b on a.compcode=b.gtcompmastid   join asptblpurdet c on c.asptblpurid=a.asptblpurid  and c.compcode=a.compcode and c.compcode=b.gtcompmastid  join asptblcolmas d on d.asptblcolmasid=c.colorname  join asptblsizmas e on e.ASPTBLSIZMASID=c.sizename join asptblpurdet1 f on f.asptblpurdetid=c.asptblpurdetid and f.asptblpurid=a.asptblpurid JOIN asptblcutpanret g on g.pono=f.pono  where  b.compcode='" + Class.Users.HCompcode + "'  and a.pono='" + pono + "' and f.barcode='" + bar + "' AND f.ISSUETYPE='REWORK' AND f.remarks='STITCHING MISTAKE' AND F.RESTITCHING='T' AND F.RECHECKING='F' AND F.CHECKING='T' ORDER BY 1";
                copyDataTable = await cc.select(Class.Users.Query, "asptblpurdet1");
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

        [HttpGet("auto/{compcode}/{asptblchkid}/{pono}")]
        public async Task<IActionResult> auto(Int64 compcode, Int64 asptblchkid, string pono)
        {
            CommonDetails com = new CommonDetails();
            DataTable dt1 = new DataTable();
            string barindex = "";
            //try
            //{



            //    string tbl = "asptblchk";
            //    Class.Users.Month = mas.getAbbreviatedName(System.DateTime.Now.Month);

            //    p.Finyear = Class.Users.Finyear;
            //    p.Compcode = compcode;

            //    if (p.Compcode > 0 && asptblchkid <= 0)
            //    {
            //        Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
            //        DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
            //        DataTable dt = ds.Tables[0];
            //        Class.Users.Query = "";
            //        Class.Users.Query = "select max(a." + tbl + "1id)+1 as id from " + tbl + " a join gtcompmast b on a.Compcode = b.gtcompmastid  where a.finyear='" + Class.Users.Finyear + "'   and a.Compcode='" + Class.Users.COMPCODE + "' ;";
            //        DataSet dsid = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            //        DataTable dtid = dsid.Tables[0];
            //        Class.Users.Query = "";

            //        if (dt1.Rows.Count > 0)
            //        {
            //            //if (p.Sequencewise == Class.Users.BarCodeType)
            //            //{
            //            DataTable dt2 = await com.shortcode1(Class.Users.Finyear, Class.Users.BarCodeType, compcode, Class.Users.ScreenName, tbl);
            //            if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) <= 0)
            //            {

            //                p.Asptblchk1id = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
            //                p.Shortcode = dt1.Rows[0]["shortcode"].ToString();
            //                p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
            //            }
            //            if (Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString()) > 0)
            //            {
            //                p.Asptblchk1id = Convert.ToInt64("0" + dtid.Rows[0]["id"].ToString());
            //                p.Shortcode = dt2.Rows[0]["shortcode"].ToString();
            //                p.Pono = Class.Users.Finyear + "-" + dt2.Rows[0]["shortcode"].ToString() + "-" + dtid.Rows[0]["id"].ToString();
            //            }


            //            p.Pono = Convert.ToString(p.Pono);
            //            p.Asptblchk1id = Convert.ToInt64("0" + p.Asptblchk1id);



            //        }

            //    }
            //    else
            //    {
            //        Class.Users.Query = "select distinct a.Barcodetype from asptblautogeneratemas a join gtcompmast b on a.Compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + Class.Users.Finyear + "'  AND c.aliasname='" + Class.Users.ScreenName + "' and c.Compcode='" + Class.Users.COMPCODE + "'";
            //        DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblautogeneratemas");
            //        DataTable dt = ds.Tables[0];
            //        Class.Users.Query = "";
            //        p.Pono = pono;
            //        p.Asptblchkid = asptblchkid;

            //    }
            //}
            //catch (Exception EX) { throw new Exception(EX.Message); }
            screen = "CheckingEntry";
            dt1 = await comClass.autonumberload(Class.Users.Finyear, Class.Users.HCompcode, screen, "asptblchk");
            if (dt1.Rows.Count > 0)
            {
                
                DataTable dt11 = await comClass.shortcode(Class.Users.Finyear, Class.Users.HCompcode, screen, "asptblchk");
                if (dt11.Rows.Count > 0)
                {
                    p.Asptblchk1id = Convert.ToInt64("0" + dt11.Rows[0]["sequenceno"].ToString());
                    p.Shortcode = dt11.Rows[0]["shortcode"].ToString();
                    p.Pono = Class.Users.Finyear + "-" + dt11.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
                    p.Checkno = Convert.ToString(dt11.Rows[0]["sequenceno"].ToString());
                    p.Docid = Convert.ToInt64("0"+dt11.Rows[0]["sequenceno"].ToString());
                }


                //p.checkno = Convert.ToString(dt11.Rows[0]["sequenceno"].ToString());
                //combocompname.Text = dt11.Rows[0]["COMPNAME"].ToString();
                //txtshortcode.Text = dt11.Rows[0]["shortcode"].ToString();
                //txtcheckid1.Text = dt1.Rows[0]["id"].ToString();

                //txtcheckno.Text = Class.Users.Finyear + "-" + txtshortcode.Text + "-" + txtcheckid1.Text;
                //p.asptblchk1id = Convert.ToInt64("0" + txtcheckid1.Text);
                //p.checkno = Convert.ToString(txtcheckno.Text);


            }
            //else
            //{
            //    p.Asptblchk1id = Convert.ToInt64("0" + dt1.Rows[0]["id"].ToString());
            //    p.Shortcode = dt1.Rows[0]["shortcode"].ToString();
            //    p.Pono = Class.Users.Finyear + "-" + dt1.Rows[0]["shortcode"].ToString() + "-" + dt1.Rows[0]["id"].ToString();
            //}
            return new JsonResult(p.Pono);

        }


        [HttpPost]
        [Route("CheckingEntry/{OBJ1}")]
        public async Task<IActionResult> CheckingEntry(string OBJ1)
        {
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(OBJ1.ToString(), (typeof(DataTable)));
            
            p.Sb.Clear();
            try
            {

                if (dt.Rows[0]["Notes"].ToString() == "")
                {

                    p.Sb.Append("Notes Field Should not be Empty");
                }

                if (dt.Rows[0]["Notes"].ToString() != "" && dt.Rows[0]["pono"].ToString() != "" && dt.Rows[0]["processtype"].ToString() != "" && dt.Rows.Count > 0)
                {

                    string sel20 = "select count(a.docid)+1 docid from asptblchk  a   where  a.PONO='" + dt.Rows[0]["pono"].ToString() + "';";
                    DataSet ds20 = await  Utility.ExecuteSelectQuery(sel20, "asptblchk");
                    DataTable dt20 = ds20.Tables["asptblchk"];                   
                    p.Asptblpurid = Convert.ToInt64("0" + dt.Rows[0]["asptblpurid"].ToString());
                    p.Docid = Convert.ToInt64("0" + dt20.Rows[0]["docid"].ToString());
                    p.Asptblchkid = Convert.ToInt64("0" + dt.Rows[0]["asptblchkid"].ToString());
                    p.Asptblchk1id = Convert.ToInt64("0" + dt.Rows[0]["asptblchk1id"].ToString());
                    p.Finyear = Class.Users.Finyear;
                    p.Shortcode = Convert.ToString(dt.Rows[0]["shortcode"].ToString());
                    p.Wdate = Convert.ToString(dt.Rows[0]["wdate"].ToString().Substring(0, 10));
                    p.Checkno = Convert.ToString(dt.Rows[0]["checkno"].ToString());
                    p.Compcode = Convert.ToInt64("0" + dt.Rows[0]["compcode"].ToString());
                    p.Buyer = Convert.ToInt64("0" + dt.Rows[0]["buyer"].ToString());
                    p.Pono = Convert.ToString(dt.Rows[0]["pono"].ToString());
                    p.Stylename = Convert.ToInt64("0" + dt.Rows[0]["stylename"].ToString());
                    p.Lotno = Convert.ToString(dt.Rows[0]["lotno"].ToString().ToUpper());
                    p.Bundle = Convert.ToString(dt.Rows[0]["bundle"].ToString());
                    p.Processname = Convert.ToInt64("0" + dt.Rows[0]["processname"].ToString());
                    p.Processtype = Convert.ToString(dt.Rows[0]["processtype"].ToString());
                    p.Issuetype = Convert.ToString(dt.Rows[0]["issuetype"].ToString());                  
                    p.Notes = Convert.ToString(dt.Rows[0]["notes"].ToString());
                    if (dt.Rows[0]["processtype"].ToString().Trim() == "INWARD") { p.Inward = "T"; p.Checking = "T"; }
                    else
                    {
                        p.Inward = "F";
                    }
                    if (dt.Rows[0]["processtype"].ToString().Trim().Trim() == "DELIVERY") { p.Delivery = "T"; p.Checking = "T"; }
                    else
                    {
                        p.Delivery = "F";
                    }
                    if (dt.Rows[0]["processtype"].ToString().Trim().Trim() == "REWORK") { p.Rechecking = "T"; p.Checking = "T"; p.Delivery = "T"; }
                    else
                    {
                        p.Rechecking = "F";
                    }

                    //p.Sizename = Convert.ToString(dt.Rows[0]["sizename"].ToString().ToUpper());
                    p.Compcode1 = Class.Users.COMPCODE;
                    p.Orderqty = Convert.ToInt64("0" + dt.Rows[0]["orderqty"].ToString());
                    p.Username = Class.Users.USERID;
                    p.Createdby = Convert.ToString(Class.Users.HUserName);
                    p.Createdon = Convert.ToDateTime(System.DateTime.Now.ToLongTimeString());
                    p.Modifiedon = Convert.ToString(System.DateTime.Now.ToLongTimeString());
                    p.Modifiedby = Class.Users.HUserName;
                    p.Ipaddress = Class.Users.IPADDRESS;
                    if (dt.Rows[0]["active"].ToString() == "T") { p.Active = "T"; } else { p.Active = "F"; }

                    Class.Users.Query = "select asptblchkid   from  asptblchk   where  asptblchk1id='" + p.Asptblchk1id + "' and issuetype='" + p.Issuetype + "' and  restitching='" + p.Restitching + "' and rechecking='" + p.Rechecking + "' and inward='" + p.Inward + "' and delivery='" + p.Delivery + "' and compcode='" + p.Compcode + "' and finyear='" + p.Finyear + "' ";
                    Class.Users.dt1 = await comClass.select(Class.Users.Query, "asptblchk");
                    if (Class.Users.dt1.Rows.Count != 0)
                    {
                    }
                    else if (Class.Users.dt1.Rows.Count != 0 && p.Asptblchkid == 0 || p.Asptblchkid == 0)
                    {
                       await auto(p.Compcode, p.Asptblchkid, p.Pono);
                        string ins = "insert into asptblchk(asptblchk1id,docid,finyear,shortcode,wdate,checkno,compcode,buyer,pono,orderqty,stylename,lotno,bundle,size,processname,processtype,issuetype,active,restitching,rechecking,inward,delivery, compcode1,username,createdby,createdon,modified,modifiedby,ipaddress,notes,asptblpurid)  VALUES('" + p.Asptblchk1id + "','" + p.Docid + "','" + p.Finyear + "','" + p.Shortcode + "',date_format('" + p.Wdate.ToString().Substring(0, 10) + "','%Y-%m-%d'),'" + p.Checkno + "','" + p.Compcode + "','" + p.Buyer + "','" + p.Pono + "','" + p.Orderqty + "','" + p.Stylename + "','" + p.Lotno + "','" + p.Bundle + "','" + p.Sizename + "','" + p.Processname + "','" + p.Processtype + "','" + p.Issuetype + "','" + p.Active + "','" + p.Restitching + "','" + p.Rechecking + "','" + p.Inward + "','" + p.Delivery + "','" + p.Compcode1 + "','" + p.Username + "','" + p.Createdby + "','" + Convert.ToDateTime(p.Createdon).ToString("yyyy-MM-dd hh:mm:ss") + "',date_format('" + p.Modifiedon.ToString().Substring(0, 10) + "','%Y-%m-%d'),'" + p.Modifiedon + "','" + p.Ipaddress + "','" + p.Notes + "','" + p.Asptblpurid + "');";
                       await Utility.ExecuteNonQuery(ins);
                        DataTable dtmax = await p.SelectCommond();
                        if (dtmax != null && Convert.ToInt64("0" + dtmax.Rows[0]["Asptblchkid"].ToString()) > 0)
                        {
                            p.Asptblchkid = Convert.ToInt64("0" + dtmax.Rows[0]["Asptblchkid"].ToString());
                            p.Asptblchk1id = Convert.ToInt64("0" + dtmax.Rows[0]["Asptblchk1id"].ToString());
                        }
                    }
                    else
                    {
                        string up = "update  asptblchk   set  modified=date_format('" + p.Modifiedon.ToString().Substring(0, 10) + "','%Y-%m-%d'),notes='" + p.Notes + "' where asptblchkid='" + p.Asptblchkid + "'";
                       await Utility.ExecuteNonQuery(up);
                    }
                   
                   
                    
                }
                else
                {
                    p.Sb.Clear();
                    p.Sb.Append("Invalid Data");
                }

            }
            catch (Exception ex)
            {
                
                string comit1 = "commit";
               await Utility.ExecuteNonQuery(comit1);

            }
            string comit = "commit";
           await Utility.ExecuteNonQuery(comit);
          
            return new JsonResult(p.Pono + "," + p.Asptblchkid + "," + p.Compcode + "," + p.Finyear + "," + p.Asptblchk1id, p.Sb);
        }


        [HttpPost]
        [Route("CheckingEntrysss/{users}/{pos}/{uniqueasptblpurid}/{uniquecompcode}/{uniquefinyear}")]
        public async Task<IActionResult> CheckingEntrysss(string users, string pos, Int64 uniqueasptblpurid, Int64 uniquecompcode, string uniquefinyear)
        {
            string lblcount = "";
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(users.ToString(), (typeof(DataTable)));
            int i = 0, j = 1; int cnt = dt.Rows.Count;
            if (cnt >= 0)
            {
                Int64 maxid = uniqueasptblpurid;

                string sel3 = "select *   from  Asptblchk   where  compcode='" + uniquecompcode + "'  and finyear='" + Class.Users.Finyear + "' AND Asptblchkid='" + maxid + "'  ";
                DataSet ds3 = await Utility.ExecuteSelectQuery(sel3, "Asptblchkdet");
                DataTable dt3 = ds3.Tables["Asptblchkdet"];

                if (dt.Rows.Count > 0)
                {


                    string sel2 = "select max(asptblchkid) id    from  asptblchk   where  compcode='" + dt3.Rows[0]["compcode"].ToString() + "'  and finyear='" + Class.Users.Finyear + "' and PONO='" + dt3.Rows[0]["pono"].ToString() + "' ";
                    DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "asptblchk");
                    DataTable dt2 = ds2.Tables["asptblchk"];
                    maxid = Convert.ToInt64("0" + dt2.Rows[0]["id"].ToString());
                    for (i = 0; i < cnt; i++)
                    {
                        p.Sb.Clear();


                        pp.Asptblpurdetid = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                        pp.Asptblpurid = Convert.ToInt64("0" + dt.Rows[i]["Asptblpurdetid"].ToString());
                        p.Asptblchkid = Convert.ToInt64("0" + maxid);
                        p.Asptblchk1id = Convert.ToInt64("0" + dt.Rows[i]["asptblchk1id"].ToString());
                        p.Compcode = Convert.ToInt64("0" + dt.Rows[i]["compcode"].ToString());
                        p.Pono = Convert.ToString(dt.Rows[i]["pono"].ToString());
                        colorid(dt.Rows[i]["colorname"].ToString());
                        pp.Colorname = coid;
                        sizeid(dt.Rows[i]["sizename"].ToString());
                        pp.Sizename = siid;
                        pp.Orderqty = Convert.ToInt64("0" + dt.Rows[i]["orderqty"].ToString());
                        pp.Processname = Convert.ToInt64(dt.Rows[i]["processname"].ToString());
                        pp.Processcheck = "T";
                        string sel1 = "select asptblchkdetid   from  asptblchkdet   where  barcode='" + pp.Barcode + "'  and asptblchkid='" + p.Asptblchkid + "' and asptblchk1id='" + p.Asptblchk1id + "' and compcode='" + p.Compcode + "' and  pono='" + p.Pono + "'  and  colorname='" + pp.Colorname + "' and sizename='" + pp.Sizename + "' and processname='" + pp.Processname + "' and  processcheck='" + pp.Processcheck + "' and issuetype='" + p.Issuetype + "' and processtype='" + p.Processtype + "' and restitching='" + p.Restitching + "' and rechecking='" + p.Rechecking + "' and inward='" + p.Inward + "' and delivery='" + p.Delivery + "' and compcode='" + p.Compcode + "' and finyear='" + p.Finyear + "' ";
                        DataSet ds1 = await Utility.ExecuteSelectQuery(sel1, "asptblchkdet");
                        DataTable dt1 = ds1.Tables["asptblchkdet"];
                        if (dt1.Rows.Count != 0 && pp.Asptblchkdetid == 0 || pp.Asptblchkdetid == 0 || p.Asptblchkid == 0)
                        {
                            string ins1 = "insert into asptblchkdet(asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,asptblchkid,asptblchk1id,compcode,pono,colorname,sizename,orderqty,processname,processcheck,processtype,issuetype,restitching,rechecking,inward,delivery,notes,modified) values('" + pp.Barcode + "','" + pp.Barcode + "','" + pp.Asptblpurdetid + "','" + p.Asptblpurid + "','" + p.Asptblchkid + "','" + p.Asptblchk1id + "' , '" + p.Compcode + "' ,'" + p.Pono + "' , '" + pp.Colorname + "','" + pp.Sizename + "','" + pp.Orderqty + "','" + pp.Processname + "','" + pp.Processcheck + "','" + p.Processtype + "','" + p.Issuetype + "','" + p.Restitching + "','" + p.Rechecking + "','" + p.Inward + "','" + p.Delivery + "','" + p.Notes + "',date_format('" + p.Modifiedon + "','%Y-%m-%d'))";
                            await Utility.ExecuteNonQuery(ins1);
                            if (dt.Rows[i]["processtype"].ToString().Trim() == "INWARD")
                            {
                                string up1 = "update asptblpurdet1 set inward='" + p.Inward + "' ,checking='" + p.Checking + "',remarks='" + p.Issuetype + "' ,issuetype='" + p.Issuetype + "', panelmistake='CHECKING INWARD'  where barcode='" + pp.Barcode + "' AND PONO='" + p.Pono + "' AND COMPCODE='" + p.Compcode + "' AND FINYEAR='" + p.Finyear + "'";
                                await Utility.ExecuteNonQuery(up1);
                            }
                            if (dt.Rows[i]["processtype"].ToString().Trim() == "DELIVERY")
                            {
                                string up1 = "update asptblpurdet1 set  delivery='" + p.Delivery + "' ,remarks='" + p.Issuetype + "' ,issuetype='" + p.Issuetype + "' ,panelmistake='CHECKING DELIVERY'  where barcode='" + pp.Barcode + "' AND PONO='" + p.Pono + "' AND COMPCODE='" + p.Compcode + "' AND FINYEAR='" + p.Finyear + "'";
                                await Utility.ExecuteNonQuery(up1);
                            }
                            if (dt.Rows[i]["processtype"].ToString().Trim() == "REWORK")
                            {
                                string up1 = "update asptblpurdet1 set  rechecking='" + p.Rechecking + "' ,remarks='" + p.Issuetype + "' ,issuetype='" + p.Issuetype + "' , delivery='" + p.Delivery + "',panelmistake='CHECKING DELIVERY' where barcode='" + pp.Barcode + "' AND PONO='" + p.Pono + "' AND COMPCODE='" + p.Compcode + "' AND FINYEAR='" + p.Finyear + "'";
                                await Utility.ExecuteNonQuery(up1);
                            }

                            p.Sb.Append("Inserted " + cnt + " of " + j.ToString());
                            j++;
                        }
                        else
                        {
                            string up1 = "update  asptblchkdet  set notes='" + dt.Rows[0]["notes"].ToString() + "' , modified=date_format('" + p.Modifiedon + "','%Y-%m-%d') where asptblchkdetid='" + pp.Asptblchkdetid + "'";
                            await Utility.ExecuteNonQuery(up1);

                            p.Sb.Append("Updated " + cnt + " of " + i.ToString());
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

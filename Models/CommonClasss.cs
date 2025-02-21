using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ReactWebApplication.Models
{
    public class CommonClasss
    {
      
            public virtual async Task<string> HideButton(string pono, string tbl)
            {
                Class.Users.Query = "select distinct a.pono from " + tbl + " a  where a.pono='" + pono + "'";
                Class.Users.dt2 =await select(Class.Users.Query, tbl);
                if (Class.Users.dt2.Rows.Count > 0)
                {
                    pono = Class.Users.dt2.Rows[0]["pono"].ToString();
                }
                else
                {
                    pono = "";
                }
                return pono;
            }
            public virtual string query { get; set; }
            public virtual DataSet ds { get; set; }
            public virtual DataTable dt { get; set; }

            public virtual async Task<DataTable> select(string qry, string tbl)
            {
                this.query = qry;
                ds = await Utility.ExecuteSelectQuery(query, tbl);
                dt = ds.Tables[tbl];
                return dt;
            }
            public virtual async Task<DataSet> select1(string qry, string tbl)
            {
                this.query = qry;
                ds = await Utility.ExecuteSelectQuery(query, tbl);
                return ds;
            }
            public virtual async Task<DataTable> autonumberload1(string y, string com1, string scr, string tbl)
            {

                Class.Users.Query = "select max(a." + tbl + "1id)+1 as id,max(a.barcode1) as barcode1 from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and a.barcodetype='" + Class.Users.BarCodeType + "'  and b.compcode='" + com1 + "' ;";
                ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[0];
                if (dt.Rows[0]["barcode1"].ToString() == "")
                {
                    Class.Users.Query = "select max(a.sequenceno) as id,a.barcode1, a.shortcode,a.finyear,b.compcode,b.compname,c.aliasname from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and b.compcode='" + com1 + "' AND a.barcodetype='" + Class.Users.BarCodeType + "' AND c.aliasname='" + scr + "' group by a.barcode1,a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname";
                    ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    dt = ds.Tables[0];
                }



                return dt;
            }
            public virtual async Task<DataTable> autonumberload(string y, string com1, string scr, string tbl)
            {
                Class.Users.Query = "";
                Class.Users.Query = "select    max(a." + tbl + "1id)+1 as id from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and b.compcode='" + com1 + "' ";
                this.ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                this.dt = this.ds.Tables[0];
                if (dt.Rows[0]["id"].ToString() == "")
                {
                    Class.Users.Query = "";
                    Class.Users.Query = "select max(a.sequenceno)+1 as id,a.shortcode,a.finyear,b.compcode,b.compname from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and b.compcode='" + com1 + "' AND c.aliasname='" + scr + "' group by a.barcode,a.shortcode,a.finyear,b.compcode,b.compname";
                    this.ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    this.dt = this.ds.Tables[0];
                }

                return dt;

            }
            public virtual async Task<DataTable> autonumberload2(string y, string com1, string scr, string tbl)
            {
                Class.Users.Query = "";


                Class.Users.Query = "select max(a." + tbl + "1id)+1 as id,max(a.barcode1) as barcode1,max(a.barcode) as barcode  from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and b.compcode='" + com1 + "' and a.barcodetype='" + Class.Users.BarCodeType + "'  and a.barcodemonth='" + Class.Users.Month + "' ;";
                ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[0];
                if (dt.Rows[0]["barcode"].ToString() == "")
                {
                    Class.Users.Query = "select max(a.sequenceno)+1 as id,a.barcode1, a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname,max(a.barcode) as barcode from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and b.compcode='" + com1 + "' AND c.aliasname='" + scr + "' group by a.barcode1,a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname";
                    ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                    dt = ds.Tables[0];
                }
                return dt;
            }
            public virtual async Task<DataTable> shortcode1(string y, string barcodetype, string com1, string scr, string tbl)
            {
                Class.Users.Query = "";
                Class.Users.Query = "select distinct a.shortcode,a.sequenceno,a.barcode1 from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.barcodetype='" + barcodetype + "' and b.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
                this.ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                this.dt = this.ds.Tables[0];
                return dt;
            }
            public virtual async Task<DataTable> shortcode2(string y, string barcodetype, string com1, string scr, string tbl)
            {
                Class.Users.Query = "";
                Class.Users.Query = "select distinct a.shortcode,a.sequenceno,a.barcode1,a.finyear from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.barcodetype='" + barcodetype + "' and b.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
                this.ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                this.dt = this.ds.Tables[0];
                return dt;
            }
            public virtual async Task<DataTable> shortcode(string y, string com1, string scr, string tbl)
            {
                Class.Users.Query = "";
                Class.Users.Query = "select distinct a.shortcode,a.sequenceno,b.compcode,b.COMPNAME,a.barcode1,a.finyear from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and b.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
                this.ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                this.dt = this.ds.Tables[0];
                return dt;
            }

            public virtual string findFinyear(string tbl)
            {
                // Class.Users.Query= "select A.gtfinancialyearid, A.FINYEAR from gtfinancialyear A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID WHERE B.COMPCODE = '" + Class.Users.HCompcode + "' AND A.CURRENTFINYR = 'T'";
                //this.ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                //this.dt = this.ds.Tables[0];
                return dt.Rows[0]["finyear"].ToString();
            }

       
    }
}

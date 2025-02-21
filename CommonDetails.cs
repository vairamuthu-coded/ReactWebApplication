using BarcodeLib;
using System.Data;
using System.Text;

namespace ReactWebApplication
{
    public  class CommonDetails
    {
        public abstract class BaseClassEvent
        {

            public abstract DataTable Tables(string s, string ss);
        }
        public virtual async Task<string> HideButton(string pono, string tbl)
        {
            Class.Users.Query = "select distinct a.pono from " + tbl + " a  where a.pono='" + pono + "'";
            Class.Users.dt2 = await select(Class.Users.Query, tbl);
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
            query = qry;
            ds = await Utility.ExecuteSelectQuery(query, tbl);
            dt = ds.Tables[tbl];
            return dt;
        }
        public virtual async Task<DataSet> select1(string qry, string tbl)
        {
            query = qry;
            ds = await Utility.ExecuteSelectQuery(query, tbl);
            return ds;
        }
        public virtual async Task<DataTable> autonumberload1(string y, long com1, string scr, string tbl)
        {

            Class.Users.Query = "select max(a." + tbl + "1id)+1 as id,max(a.barcode1) as barcode1 from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and a.barcodetype='" + Class.Users.BarCodeType + "'  and a.compcode='" + com1 + "' ;";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            if (dt.Rows[0]["barcode1"].ToString() == "")
            {
                Class.Users.Query = "select max(a.sequenceno) as id,a.barcode1, a.shortcode,a.finyear,b.compcode,b.compname,c.aliasname from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.compcode='" + com1 + "' AND a.barcodetype='" + Class.Users.BarCodeType + "' AND c.aliasname='" + scr + "' group by a.barcode1,a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname";
                ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[0];
            }



            return dt;
        }
        public virtual async Task<DataTable> autonumberload(string y, string com1, string scr, string tbl)
        {
            Class.Users.Query = "";
            Class.Users.Query = "select    max(a." + tbl + "1id)+1 as id,a.shortcode,a.finyear,b.compcode,b.compname,b.gtcompmastid from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and a.compcode='" + com1 + "' group by a.shortcode,a.finyear,b.compcode,b.compname,b.gtcompmastid ";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            if (dt.Rows.Count <= 0)
            {
                Class.Users.Query = "";
                Class.Users.Query = "select max(a.sequenceno)+1 as id,a.shortcode,a.finyear,b.compcode,b.compname,b.gtcompmastid from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.compcode='" + com1 + "' AND c.aliasname='" + scr + "' group by a.barcode,a.shortcode,a.finyear,b.compcode,b.compname";
                ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[0];
            }

            return dt;

        }
        public virtual async Task<DataTable> autonumberload2(string y, long com1, string scr, string tbl)
        {
            Class.Users.Query = "";


            Class.Users.Query = "select max(a." + tbl + "1id)+1 as id,max(a.barcode1) as barcode1,max(a.barcode) as barcode  from " + tbl + " a join gtcompmast b on a.compcode = b.gtcompmastid  where a.finyear='" + y + "' and a.compcode='" + com1 + "' and a.barcodetype='" + Class.Users.BarCodeType + "'  and a.barcodemonth='" + Class.Users.Month + "' ;";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            if (dt.Rows[0]["barcode"].ToString() == "")
            {
                Class.Users.Query = "select max(a.sequenceno)+1 as id,a.barcode1, a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname,max(a.barcode) as barcode from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.compcode='" + com1 + "' AND c.aliasname='" + scr + "' group by a.barcode1,a.shortcode,a.finyear,b.compcode,b.compname,a.barcodetype,c.aliasname";
                ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
                dt = ds.Tables[0];
            }
            return dt;
        }
        public virtual async Task<DataTable> shortcode1(string y, string barcodetype, long com1, string scr, string tbl)
        {
            Class.Users.Query = "";
            Class.Users.Query = "select distinct a.shortcode,a.sequenceno,a.barcode1,b.gtcompmastid from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.barcodetype='" + barcodetype + "' and a.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            return dt;
        }
        public virtual async Task<DataTable> shortcode2(string y, string barcodetype, long com1, string scr, string tbl)
        {
            Class.Users.Query = "";
            Class.Users.Query = "select distinct a.shortcode,a.sequenceno,a.barcode1,b.gtcompmastid from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.barcodetype='" + barcodetype + "' and a.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            return dt;
        }
        public virtual async Task<DataTable> shortcode(string y, string com1, string scr, string tbl)
        {
            Class.Users.Query = "";
            Class.Users.Query = "select distinct a.shortcode,a.sequenceno,b.compcode,b.COMPNAME,a.barcode1,b.gtcompmastid from asptblautogeneratemas a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where a.finyear='" + y + "' and a.compcode='" + com1 + "' AND c.aliasname='" + scr + "'";
            ds = await Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            dt = ds.Tables[0];
            return dt;
        }

        public virtual string findFinyear(string tbl)
        {
            // Class.Users.Query= "select A.gtfinancialyearid, A.FINYEAR from gtfinancialyear A JOIN GTCOMPMAST B ON A.COMPCODE = B.GTCOMPMASTID WHERE B.COMPCODE = '" + Class.Users.HCompcode + "' AND A.CURRENTFINYR = 'T'";
            //this.ds = Utility.ExecuteSelectQuery(Class.Users.Query, tbl);
            //this.dt = this.ds.Tables[0];
            return dt.Rows[0]["finyear"].ToString();
        }
        public Task<DataTable> CopyRows(DataTable grid, DataTable dt1 ,StringBuilder barcode)
        {
            int rowcount = 0;
            bool chk = checkduplicate1(1, grid, barcode);
            if (chk == true) {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    grid.Rows.Add();
                    rowcount = grid.Rows.Count - 1;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        grid.Rows[rowcount][col.ColumnName] = dt1.Rows[0][col.ColumnName].ToString();
                    }
                    rowcount++;
                }
            }
            else
            {
               
            }
            return Task.FromResult(grid);
        }

        internal DataTable CopyColumn(DataTable dt1, DataTable copyDataTable)
        {
            foreach (DataColumn col in copyDataTable.Columns)
            {
                dt1.Columns.Add(col.ColumnName);
            }
            return dt1;
        }

        public bool checkduplicate1(int index, DataTable dgrid, StringBuilder s)
        {
          //   dgrid = new DataTable();
            foreach (DataRow row in dgrid.Rows)
            {
                if (row.ItemArray[0].ToString() == null)
                {
                    continue;
                }
                if (row.ItemArray[0].ToString() != null && row.ItemArray[0].ToString() == s.ToString())
                {
                    s.Clear();
                    return false;
                }
            }
            return true;
        }
        public bool checkduplicate(int index, DataTable dgrid)
        {
            // dgrid = new DataTable();
            foreach (DataRow row in dgrid.Rows)
            {
                if (row.ItemArray[0].ToString() == dgrid.Rows[index][row.ItemArray[0].ToString()].ToString())
                {
                    continue;

                }
                if (dgrid.Rows[index][row.ItemArray[index].ToString()].ToString() == null)
                {
                    continue;

                }
                if (row.ItemArray[index].ToString() != null && row.ItemArray[index].ToString() == dgrid.Rows[index][row.ItemArray[0].ToString()].ToString())
                {
                   row.ItemArray[index] = "";
                    return false;
                }
                else
                {
                    // dgrid.AllowUserToAddRows = true;
                }
            }
            return true;
        }

    }
}

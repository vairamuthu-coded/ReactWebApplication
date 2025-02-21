using Microsoft.AspNetCore.Http.HttpResults;

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ReactWebApplication.Models.Transactions.SRG
{

    public abstract class Abstract
    {
        public abstract Task<DataTable> GridLoad();
        public abstract Task InsertCommond();
        public abstract Task UpdateCommond();
        public abstract Task<DataTable> SelectCommond();
        public string Active { get; set; }
        public long? Username { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }
        public string Modifiedon { get; set; }
        public string Modifiedby { get; set; }
        public string Ipaddress { get; set; }
    }
        public class ProductionLot : Abstract
    {

      

        public Int64 Asptblprolotid { get; set; }

        public Int64 Docid { get; set; }

        public Int64 Asptblprolot1id { get; set; }

        public Int64 Asptblpurid { get; set; }
        public string Shortcode { get; set; }
        public string Finyear { get; set; }
        public string Proddate { get; set; }
        public string Prodno { get; set; }
        public string Styleref { get; set; }
        public string Orderno { get; set; }
        public Int64 Compcode { get; set; }
        public Int64 Orderqty { get; set; }
        public Int64 Stylename { get; set; }
        public Int64 Buyer { get; set; }
        public string Pono { get; set; }
        public string Lotno { get; set; }
        public string Bundle { get; set; }
        public string Sizename { get; set; }
        public Int64 Processname { get; set; }
        public string Processtype { get; set; }
        public string Productioncancel { get; set; }
        public string Issuetype { get; set; }
        public string Stitching { get; set; }
        public string Inward { get; set; }
        public string Delivery { get; set; }
        public string Restitching { get; set; }
        public string Rechecking { get; set; }
        public string Panelmistake { get; set; }
        public Int64 Compcode1 { get; set; }
        public string Inward1 { get; set; }
        public string Rework1 { get; set; }
        public string Delivery1 { get; set; }
        public string Notes { get; set; }
        public Int32 Gridrowcount { get; set; }
        public async Task<string> HideButton(string pono, string tbl)
        {
            CommonDetails common = new CommonDetails();
            Class.Users.Query = "select distinct a.pono from " + tbl + " a  where a.pono='" + pono + "'";
            Class.Users.dt2 = await common.select(Class.Users.Query, tbl);
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
        public async Task<DataTable> select(long asptblprolotid, long asptblprolot1id, long compcode, string finyear, string shortcode, string proddate, string prodno, long buyer, string pono, long orderqty, long stylename, string lotno, string bundle, long processname, string processtype, string productioncancel, string active, string issuetype, string restitching, string rechecking, string inward, string delivery)
        {
            Class.Users.Query = "select distinct asptblprolotid   from  asptblprolot   where  asptblprolotid='" + asptblprolotid + "' and asptblprolot1id='" + asptblprolot1id + "'  and compcode='" + compcode + "' and finyear='" + finyear + "' and shortcode='" + shortcode + "' and proddate=date_format('" + proddate + "','%Y-%m-%d') and prodno='" + prodno + "' and buyer='" + buyer + "'  and  pono='" + pono + "'  and  orderqty='" + orderqty + "' and stylename='" + stylename + "' and bundle='" + bundle + "' and lotno = '" + lotno + "' and processname='" + processname + "' and processtype='" + processtype + "' and productioncancel='" + productioncancel + "' and active='" + active + "' and issuetype='" + issuetype + "'  and restitching='" + restitching + "' and rechecking='" + rechecking + "' and inward='" + inward + "' and delivery='" + delivery + "'";// and orderqty = '" + pp.orderqty + "'
            DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblprolot");
            System.Data.DataTable dt = ds.Tables["asptblprolot"];
            return dt;
        }

        public virtual async Task<DataTable> DocIdDetails()
        {
            string sel20 = "select count(a.docid)+1 docid from asptblprolot  a   where  a.PONO='" +Pono + "';";
            DataSet ds20 = await Utility.ExecuteSelectQuery(sel20, "asptblprolot");
            DataTable dt20 = ds20.Tables["asptblprolot"];
            return dt20;
        }
        //public virtual async Task InsertCommond()
        //{
        //    string ins = "insert into asptblprolot(asptblprolot1id,docid, finyear,shortcode,proddate,prodno,compcode,buyer,pono,orderqty,stylename,lotno,bundle,gridrowcount,processname,processtype, productioncancel,active,compcode1,username,createdby,createdon,modified,modifiedby,ipaddress,issuetype,restitching,rechecking,inward,delivery,notes)  VALUES('" + Asptblprolot1id + "','" + Docid + "','" + Finyear + "','" + Shortcode + "',date_format('" + Proddate + "','%Y-%m-%d'),'" + Prodno + "','" + Compcode + "','" + Buyer + "','" + Pono + "','" + Orderqty + "','" + Stylename + "','" + Lotno + "','" + Bundle + "','" + Gridrowcount + "','" + Processname + "','" + Processtype + "','" + Productioncancel + "','" + Active + "','" + Compcode1 + "','" + Username + "','" + Createdby + "','" + Convert.ToDateTime(Createdon).ToString("yyyy-MM-dd hh:mm:ss") + "',date_format('" + Modifiedon + "','%Y-%m-%d'),'" + Modifiedby + "','" + Ipaddress + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Notes + "');";
        //    await Utility.ExecuteNonQuery(ins);
        //}
        //public async Task InsertCommond(long asptblprolot1id, long docid, string finyear, string shortcode, string proddate, string prodno, long compcode, long buyer, string pono, long orderqty, long stylename, string lotno, string bundle, int gridrowcount, long processname, string processtype, string productioncancel, string active, long compcode1, long? username, string createdby, DateTime? createdon, string modifiedon, string modifiedby, string ipaddress, string issuetype, string restitching, string rechecking, string inward, string delivery, string notes)
        //{
        //    string ins = "insert into asptblprolot(asptblprolot1id,docid, finyear,shortcode,proddate,prodno,compcode,buyer,pono,orderqty,stylename,lotno,bundle,gridrowcount,processname,processtype, productioncancel,active,compcode1,username,createdby,createdon,modified,modifiedby,ipaddress,issuetype,restitching,rechecking,inward,delivery,notes)  VALUES('" + asptblprolot1id + "','" + docid + "','" + finyear + "','" + shortcode + "',date_format('" + proddate + "','%Y-%m-%d'),'" + prodno + "','" + compcode + "','" + buyer + "','" + pono + "','" + orderqty + "','" + stylename + "','" + lotno + "','" + bundle + "','" + gridrowcount + "','" + processname + "','" + processtype + "','" + productioncancel + "','" + active + "','" + compcode1 + "','" + username + "','" + createdby + "','" + Convert.ToDateTime(createdon).ToString("yyyy-MM-dd hh:mm:ss") + "',date_format('" + modifiedon + "','%Y-%m-%d'),'" + modifiedby + "','" + ipaddress + "','" + issuetype + "','" + restitching + "','" + rechecking + "','" + inward + "','" + delivery + "','" + notes + "');";
        //    await Utility.ExecuteNonQuery(ins);
          
        //}

        //public async Task Insert()
        //{
        //            string ins = "insert into asptblprolot(asptblprolot1id,docid, finyear,shortcode,proddate,prodno,compcode,buyer,pono,orderqty,stylename,lotno,bundle,gridrowcount,processname,processtype, productioncancel,active,compcode1,username,createdby,createdon,modified,modifiedby,ipaddress,issuetype,restitching,rechecking,inward,delivery,notes)  VALUES('" + Asptblprolot1id + "','" + Docid + "','" + Finyear + "','" + Shortcode + "',date_format('" + Proddate + "','%Y-%m-%d'),'" + Prodno + "','" + Compcode + "','" + Buyer + "','" + Pono + "','" + Orderqty + "','" + Stylename + "','" + Lotno + "','" + Bundle + "','" + Gridrowcount + "','" + Processname + "','" + Processtype + "','" + Productioncancel + "','" + Active + "','" + Compcode1 + "','" + Username + "','" + Createdby + "','" + Convert.ToDateTime(Createdon).ToString("yyyy-MM-dd hh:mm:ss") + "',date_format('" + Modifiedon + "','%Y-%m-%d'),'" + Modifiedby + "','" + Ipaddress + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Notes + "');";
        //            await Utility.ExecuteNonQuery(ins);
        //}

        //public async  Task Update()
        //{
        //    string up = "update  asptblprolot   set proddate=date_format('" + Modifiedby + "', '%Y-%m-%d'), compcode1='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',modified=date_format('" + Modifiedby + "','%Y-%m-%d'), modifiedby='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "' where asptblprolotid='" + Asptblprolotid + "'";
        //    await Utility.ExecuteNonQuery(up);
        //}

        public override async Task<DataTable> GridLoad()
        {
            string sel1 = "SELECT a.asptblprolotid,a.Proddate, b.Compcode,a.Prodno, ''Buyername,a.Pono,d.stylename,a.lotno,a.orderqty,a.Processtype,e.styleref, a.active FROM  asptblprolot a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblstymas d on d.asptblstymasid=a.stylename  join asptblprolot e on e.Pono=a.Pono and e.Compcode=a.Compcode  order by  a.asptblprolotid desc";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblprolot");
            DataTable dt = ds.Tables["asptblprolot"];
            return dt;
        }

        public override async Task InsertCommond()
        {
            string ins = "insert into asptblprolot(asptblprolot1id,docid, finyear,shortcode,proddate,prodno,compcode,buyer,pono,orderqty,stylename,lotno,bundle,gridrowcount,processname,processtype, productioncancel,active,compcode1,username,createdby,createdon,modified,modifiedby,ipaddress,issuetype,restitching,rechecking,inward,delivery,notes)  VALUES('" + Asptblprolot1id + "','" + Docid + "','" + Finyear + "','" + Shortcode + "',date_format('" + Proddate + "','%Y-%m-%d'),'" + Prodno + "','" + Compcode + "','" + Buyer + "','" + Pono + "','" + Orderqty + "','" + Stylename + "','" + Lotno + "','" + Bundle + "','" + Gridrowcount + "','" + Processname + "','" + Processtype + "','" + Productioncancel + "','" + Active + "','" + Compcode1 + "','" + Username + "','" + Createdby + "','" + Convert.ToDateTime(Createdon).ToString("yyyy-MM-dd hh:mm:ss") + "',date_format('" + Modifiedon + "','%Y-%m-%d'),'" + Modifiedby + "','" + Ipaddress + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Notes + "');";
            await Utility.ExecuteNonQuery(ins);
        }

        public override async Task UpdateCommond()
        {
            string up = "update  asptblprolot   set proddate=date_format('" + Modifiedby + "', '%Y-%m-%d'), compcode1='" + Class.Users.COMPCODE + "',username='" + Class.Users.USERID + "',modified=date_format('" + Modifiedby + "','%Y-%m-%d'), modifiedby='" + System.DateTime.Now.ToString() + "',ipaddress='" + Class.Users.IPADDRESS + "' where asptblprolotid='" + Asptblprolotid + "'";
            await Utility.ExecuteNonQuery(up);
        }

        public override async Task<DataTable> SelectCommond()
        {
            string selmax = "select max(asptblprolotid) as asptblprolotid   from  asptblprolot   where pono='" + Pono + "'  and finyear ='" + Finyear + "' and compcode ='" + Compcode + "'";
            DataSet dsmax = await Utility.ExecuteSelectQuery(selmax, "asptblprolot");
            DataTable dtmax = dsmax.Tables["asptblprolot"];
            return dtmax;
        }



       
    }
    public class ProductionLotDet : ProductionLot
    {
        public StringBuilder Sb = new StringBuilder();

        public override Task<DataTable> GridLoad()
        {
            return base.GridLoad();
        }
        public override async Task InsertCommond()
        {
            string ins1 = "insert into asptblprolotdet(asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,asptblprolotid,asptblprolot1id,compcode,pono,colorname,sizename,orderqty,processname,processcheck,finyear,processtype,issuetype,restitching,rechecking,inward,delivery,panelmistake,inward1,rework1,delivery1,modified,notes) values('" + Asptblpurdet1id + "','" + Barcode + "','" + Asptblpurdetid + "','" + Asptblpurid + "','" + Asptblprolotid + "' ,'" + Asptblprolot1id + "' , '" + Compcode + "' ,'" + Pono + "' , '" + Colorname + "','" + Sizename + "','" + Orderqty + "','" + Process + "','" + Processcheck + "','" + Finyear + "','" + Processtype + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Panelmistake + "','" + Inward1 + "','" + Rework1 + "','" + Delivery1 + "',date_format('" + Modifiedby + "','%Y-%m-%d'),'" + Notes + "')";
            await Utility.ExecuteNonQuery(ins1);
        }
        public override async Task UpdateCommond()
        {
            string up1 = "update asptblpurdet1 set  panelmistake='" + Panelmistake + "',issuetype='" + Issuetype + "',RESTITCHING= '" + Restitching + "',STITCHING= '" + Stitching + "',rechecking='" + Rechecking + "',inward='" + Inward + "',delivery='" + Delivery + "' , processcheck='T' where barcode='" + Barcode + "' AND PONO='" + Pono + "' AND COMPCODE='" + Compcode + "' AND FINYEAR='" + Finyear + "'";
            await Utility.ExecuteNonQuery(up1);
        }

        public Int64 Asptblprolotdetid { get; set; }
        public Int64 Asptblpurdet1id { get; set; }
        public string Barcode { get; set; }
        public Int64 Asptblpurdetid { get; set; }
        public string Colorname { get; set; }
        public Int64 Comqty { get; set; }
        public Int64 Lotqty { get; set; }
        public Int64 Balqty { get; set; }
        public string Process { get; set; }
        public string Processcheck { get; set; }

        //public new async Task Insert()
        //{
        //    string ins1 = "insert into asptblprolotdet(asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,asptblprolotid,asptblprolot1id,compcode,pono,colorname,sizename,orderqty,processname,processcheck,finyear,processtype,issuetype,restitching,rechecking,inward,delivery,panelmistake,inward1,rework1,delivery1,modified,notes) values('" + Asptblpurdet1id + "','" + Barcode + "','" + Asptblpurdetid + "','" + Asptblpurid + "','" + Asptblprolotid + "' ,'" + Asptblprolot1id + "' , '" + Compcode + "' ,'" + Pono + "' , '" + Colorname + "','" + Sizename + "','" + Orderqty + "','" + Process + "','" + Processcheck + "','" + Finyear + "','" + Processtype + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Panelmistake + "','" + Inward1 + "','" + Rework1 + "','" + Delivery1 + "',date_format('" + Modifiedby + "','%Y-%m-%d'),'" + Notes + "')";
        //    await Utility.ExecuteNonQuery(ins1);
        //}

        //public new async Task Update()
        //{
        //    string up1 = "update asptblpurdet1 set  panelmistake='" + Panelmistake + "',issuetype='" + Issuetype + "',RESTITCHING= '" + Restitching + "',STITCHING= '" + Stitching + "',rechecking='" + Rechecking + "',inward='" + Inward + "',delivery='" + Delivery + "' , processcheck='T' where barcode='" + Barcode + "' AND PONO='" + Pono + "' AND COMPCODE='" + Compcode + "' AND FINYEAR='" + Finyear + "'";
        //    await Utility.ExecuteNonQuery(up1);
        //}



        //public override async Task InsertCommond()
        //{
        //    string ins1 = "insert into asptblprolotdet(asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,asptblprolotid,asptblprolot1id,compcode,pono,colorname,sizename,orderqty,processname,processcheck,finyear,processtype,issuetype,restitching,rechecking,inward,delivery,panelmistake,inward1,rework1,delivery1,modified,notes) values('" + Asptblpurdet1id + "','" + Barcode + "','" + Asptblpurdetid + "','" + Asptblpurid + "','" + Asptblprolotid + "' ,'" + Asptblprolot1id + "' , '" + Compcode + "' ,'" + Pono + "' , '" + Colorname + "','" + Sizename + "','" + Orderqty + "','" + Process + "','" + Processcheck + "','" + Finyear + "','" + Processtype + "','" + Issuetype + "','" + Restitching + "','" + Rechecking + "','" + Inward + "','" + Delivery + "','" + Panelmistake + "','" + Inward1 + "','" + Rework1 + "','" + Delivery1 + "',date_format('" + Modifiedby + "','%Y-%m-%d'),'" + Notes + "')";
        //    await Utility.ExecuteNonQuery(ins1);
        //}

        //internal async Task InsertCommond(long asptblpurdet1id, string barcode, long asptblpurdetid, long asptblpurid, long asptblprolotid, long asptblprolot1id, long compcode, string pono, string colorname, string sizename, long orderqty, string process, string processcheck, string finyear, string processtype, string issuetype, string restitching, string rechecking, string inward, string delivery, string panelmistake, string inward1, string rework1, string delivery1, string modifiedby, string notes)
        //{
        //    string ins1 = "insert into asptblprolotdet(asptblpurdet1id,barcode,asptblpurdetid,asptblpurid,asptblprolotid,asptblprolot1id,compcode,pono,colorname,sizename,orderqty,processname,processcheck,finyear,processtype,issuetype,restitching,rechecking,inward,delivery,panelmistake,inward1,rework1,delivery1,modified,notes) values('" + asptblpurdet1id + "','" + barcode + "','" + asptblpurdetid + "','" + asptblpurid + "','" + asptblprolotid + "' ,'" + asptblprolot1id + "' , '" + compcode + "' ,'" + pono + "' , '" + colorname + "','" + sizename + "','" + orderqty + "','" + process + "','" + processcheck + "','" + finyear + "','" + processtype + "','" + issuetype + "','" + restitching + "','" + rechecking + "','" + inward + "','" + delivery + "','" + panelmistake + "','" + inward1 + "','" + rework1 + "','" + delivery1 + "',date_format('" + modifiedby + "','%Y-%m-%d'),'" + notes + "')";
        //    await Utility.ExecuteNonQuery(ins1);
        //}


    }
}

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Emit;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static ReactWebApplication.Class;

namespace ReactWebApplication.Models.Transactions
{
    public abstract class Abstract
    {
        public abstract Task<DataTable> GridLoad();
        public abstract Task InsertCommond(); 
        public abstract Task UpdateCommond();
        public abstract Task<DataTable> SelectCommond();
        public   enum Level
        {
            ORDERQTY,EXCCESSQTY           
        }
        public string Active { get; set; }
        public long? Username { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }
        public string Modifiedon { get; set; }
        public string Modifiedby { get; set; }
        public string Ipaddress { get; set; }
    }
    public  class PurchasesOrder : CommonClass
    {


        public  void Text()
        {
            Console.WriteLine("a");
        }
        public Int64 Asptblpurid { get; set; }
        public Int64 Asptblpur1id { get; set; }
        public string Finyear { get; set; }
        public string Podate { get; set; }
        public Int64 Compcode { get; set; }
        public Int64 Compcode1 { get; set; }
        public Int64 Compname { get; set; }
        public string Shortcode { get; set; }
        public string Pono { get; set; }
        public Int64 Orderqty { get; set; }
        public Int64 Stylename { get; set; }
        public Int64 Sizegroup { get; set; }
        public Int64 Excessqty { get; set; }
        public string Pocancel { get; set; }
        public Int64 Buyer { get; set; }
        public string Lotno { get; set; }
        public Int64 Process { get; set; }
        public Int64 Processname { get; set; }
        public string Processtype { get; set; }
        public string Bundle { get; set; }
        public Int64 Size { get; set; }
        public string Issuetype { get; set; }
        public string Barcode { get; set; }
        public string Barcode1 { get; set; }
        public string  Barcode2 { get; set; }
        public string Barcode3 { get; set; } 
        public string Barcodetype { get; set; }
    public string Orderno { get; set; }
    public string Styleref { get; set; }
    public string Colorname { get; set; }
 
    public string Barcodemonth { get; set; }
    public string Monthwise { get; set; }
    public string Sequencewise { get; set; }
    public string Panelmistake { get; set; }
    public string Modified1 { get; set; }
    public Int64 Garmentimage { get; set; }
    public Int64 Imagebytes { get; set; }
    public string Modified { get; set; }
       
        
    public string Autono { get; set; }

        public override async Task<DataTable> GridLoad()
        {
           
            string sel1 = "SELECT a.Asptblpurid, b.Compcode,a.Podate,a.Pono,f.Stylename,e.Sizegroup,a.Orderqty, a.excessqty, a.Styleref,a.Orderno, a.active FROM  Asptblpur a join gtcompmast b on a.Compcode=b.gtcompmastid  join asptblsizgrp e on e.asptblsizgrpid=a.Sizegroup join asptblstymas f on f.asptblstymasid=a.Stylename   order by  a.Asptblpurid desc";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "Asptblpur");
            DataTable dt = ds.Tables["Asptblpur"];
            return dt;
        }

        

        //public override async Task  InsertCommond()
        //{
        //    string ins = "insert into Asptblpur(Asptblpur1id,shortcode,finyear,podate,compcode,stylename,orderqty,Pono,buyer,sizegroup,processname,processtype,pocancel,active,compcode1,username,createdby,Modifiedby,Modified,ipaddress,issuetype,excessqty,Barcode,Barcode1,Barcodetype, orderno,styleref,Modified1,Barcodemonth)  VALUES('" + Asptblpur1id + "','" + Shortcode + "','" + Finyear + "','" + Podate + "','" + Compcode + "','" + Stylename + "','" + Orderqty + "','" + Pono + "','" + Buyer + "','" + Sizegroup + "','" + Processname + "','" + Processtype + "','" + Pocancel + "','" + Active + "','" + Compcode1 + "','" + Username + "','" + Createdby + "','" + Modifiedby + "','" + Modified + "','" + Ipaddress + "','" + Issuetype + "','" + Excessqty + "','" + Barcode + "','" + Barcode3 + "','" + Barcodetype + "','" + Orderno + "','" + Styleref + "',date_format('" + Modified1 + "','%Y-%m-%d'),'" + Class.Users.Month + "')";
        //    await Utility.ExecuteNonQuery(ins);
        //}

        public override async Task InsertCommond()
        {
            string ins = "insert into Asptblpur(Asptblpur1id,shortcode,finyear,podate,compcode,stylename,orderqty,Pono,buyer,sizegroup,processname,processtype,pocancel,active,compcode1,username,createdby,Modifiedby,Modified,ipaddress,issuetype,excessqty,Barcode,Barcode1,Barcode2,Barcode3,Barcodetype, orderno,styleref,Modified1,Barcodemonth)  VALUES('" + Asptblpur1id + "','" + Shortcode + "','" + Finyear + "','" + Podate + "','" + Compcode + "','" + Stylename + "','" + Orderqty + "','" + Pono + "','" + Buyer + "','" + Sizegroup + "','" + Processname + "','" + Processtype + "','" + Pocancel + "','" + Active + "','" + Compcode1 + "','" + Username + "','" + Createdby + "','" + Modifiedby + "','" + Modified + "','" + Ipaddress + "','" + Issuetype + "','" + Excessqty + "','" + Barcode + "','" + Barcode1 + "','" + Barcode2 + "','" + Barcode3 + "','" + Barcodetype + "','" + Orderno + "','" + Styleref + "',date_format('" + Modified1 + "','%Y-%m-%d'),'" + Class.Users.Month + "')";
            await Utility.ExecuteNonQuery(ins);
        }

      

        public Task<DataTable> PonoDetails(string pono)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> PonoDetails(string compcode, string pono)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> PonoDetails(string compcode, string pono, string sizegroup)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> PonoDetailss(string compcode, string pono, string ordertype)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Saves()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
        }



        //public override Task<DataTable> SelectCommond()
        //{
        //    throw new NotImplementedException();
        //}



        //public override Task UpdateCommond()
        //{
        //    throw new NotImplementedException();
        // }

        //internal virtual async Task<DataTable> SelectCommond(string pono)
        //{
        //    string sel0 = "select Asptblpurid    from  asptblpur   where Pono='" + Pono + "'  and  Barcode='" + Barcode + "' and Barcode1='" + Barcode3 + "' and Barcodetype='" +Barcodetype + "' and Asptblpur1id='" + Asptblpur1id + "' and shortcode='" + Shortcode + "' and finyear ='" + Finyear + "' and podate='" + Podate + "'  and stylename='" + Stylename + "' and orderqty ='" +Orderqty + "' and  buyer='" + Buyer + "'  and processname ='" + Processname + "' and processtype='" + Processtype + "'  and pocancel ='" + Pocancel + "' and active='" + Active + "' and excessqty='" + Excessqty + "' and orderno='" + Orderno + "' and styleref='" + Styleref + "'";
        //    DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, "asptblpur");
        //    DataTable dt0 = ds0.Tables["asptblpur"];
        //    return dt0;
        //}

        internal async Task<DataTable> SelectCommond(long compcode, string finyear, string pono)
        {
            string sel2 = "select max(Asptblpurid) id    from  asptblpur   where  compcode='" + compcode + "'  and finyear='" + finyear + "' and Pono='" + pono + "' ";
            DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "asptblpur");
            DataTable dt2 = ds2.Tables["asptblpur"];
            return dt2;
        }

        internal async Task<DataTable> SelectCommond(string pono, string barcode, string barcode3, string barcodetype, long asptblpur1id, string shortcode, string finyear, string podate, long stylename, long orderqty, long buyer, long processname, string processtype, string pocancel, string active, long excessqty, string orderno, string styleref)
        {
            string sel0 = "select Asptblpurid    from  asptblpur   where Pono='" + pono + "'  and  Barcode='" + barcode + "' and Barcode1='" + barcode3 + "' and Barcodetype='" + barcodetype + "' and Asptblpur1id='" + asptblpur1id + "' and shortcode='" + shortcode + "' and finyear ='" + finyear + "' and podate='" + podate + "'  and stylename='" + stylename + "' and orderqty ='" + orderqty + "' and  buyer='" + buyer + "'  and processname ='" + processname + "' and processtype='" + processtype + "'  and pocancel ='" + pocancel + "' and active='" + active + "' and excessqty='" + excessqty + "' and orderno='" + orderno + "' and styleref='" + styleref + "'";
            DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, "asptblpur");
            DataTable dt0 = ds0.Tables["asptblpur"];
            return dt0;
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }
    }
    public   class PurchasesOrderDetails :PurchasesOrder
    {
        //Method Hiding
  public new void Text()
        {
            base.Text();
            Console.WriteLine("b");
        }
        public Int64 Asptblpurdetid { get; set; } 
        public string Colorname1 { get; set; }
        public int Portion { get; set; }
        public string Sizename { get; set; }
        public string Remarks { get; set; }
        public int JJ { get; set; }
       
        public override async Task InsertCommond()
        {
            string ins1 = "insert into Asptblpurdet(Barcode,Barcode1,Asptblpurid,Asptblpur1id,compcode,Pono,colorname,portion,sizename,orderqty,finyear,sno) values('" + Barcode + "','" + Barcode1 + "' ,'" + Asptblpurid + "' ,'" + Asptblpur1id + "' , '" + Class.Users.COMPCODE + "' ,'" + Pono + "' , '" + Colorname + "','" + Portion + "','" + Sizename + "','" + Orderqty + "','" + Class.Users.Finyear + "','" + JJ + "');";
            await Utility.ExecuteNonQuery(ins1);
        }

        internal async Task<DataTable> SelectCommond(long cOMPCODE, string finyear, string barCodeType, string month)
        {
            string sel4 = "select  max(Barcode1) Barcode1 ,max(Barcode) Barcode   from  Asptblpur   where  compcode='" + cOMPCODE + "'  and finyear='" + finyear + "' and Barcodetype='" + barCodeType + "' and Barcodemonth='" + month + "'";
            DataSet ds4 = await Utility.ExecuteSelectQuery(sel4, "Asptblpur");
            DataTable dt4 = ds4.Tables["Asptblpur"];
  
            return dt4;
        }
 
        internal async Task<DataTable> SelectCommond(long cOMPCODE, string finyear, string barCodeType, string month, string pono)
        {

            string sel2 = "select max(Asptblpurid) id,max(Asptblpur1id) id1 ,max(Barcode1) Barcode1   from  Asptblpur   where  compcode='" + cOMPCODE + "'  and finyear='" + finyear + "' and Barcodetype='" + barCodeType + "' and Barcodemonth='" + month + "'  and Pono='" + pono + "'";
            DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "Asptblpur");
           DataTable dt2 = ds2.Tables["Asptblpur"];
       
            return dt2;
        }

        internal async Task<DataTable> SelectCommond1(long cOMPCODE, string finyear, string barCodeType, string pono)
        {
            string sel2 = "select max(Asptblpurid) id,max(Asptblpur1id)id1 ,max(Barcode1) Barcode1   from  Asptblpur   where  compcode='" + cOMPCODE + "'  and finyear='" + finyear + "' and Barcodetype='" + barCodeType + "'  and Pono='" + pono + "'";
            DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "Asptblpur");
            DataTable dt2 = ds2.Tables["Asptblpur"];
        
            return dt2;
        }

        internal async Task<DataTable> SelectCommond1(long cOMPCODE, string barCodeType, string finyear)
        {
            string sel4 = "select max(Barcode1) Barcode1 ,max(Barcode) Barcode   from  Asptblpur   where  compcode='" + cOMPCODE + "' and Barcodetype='" + barCodeType + "' and finyear='" + finyear + "'";
            DataSet ds4 = await Utility.ExecuteSelectQuery(sel4, "Asptblpur");
            DataTable dt4 = ds4.Tables["Asptblpur"];
            return dt4;
        }
    }


    public   class PurchasesModel1detail: PurchasesOrderDetails
    {

      

        public Int64 Asptblpurdet1id { get; set; }
        public Int64 Pcs { get; set; }
        public Int64 Sno { get; set; }
        public string Cutting { get; set; }
        public string Stitching { get; set; }
        public string Checking { get; set; }
        public string Restitching { get; set; }
        public string Rechecking { get; set; }
        public string Inward { get; set; }
        public string Delivery { get; set; }



        public override async Task InsertCommond()
        {
            Class.Users.Query = "insert into Asptblpurdet1(Asptblpurdetid,Asptblpurid,Asptblpur1id,compcode,Pono,colorname,portion,sizename,orderqty,colorname1,finyear,cutting,stitching,checking,restitching,rechecking,sno,inward,delivery,panelmistake,Barcode,Barcode1,processcheck,ISSUETYPE,Modified) values('" + Asptblpurdetid + "','" + Asptblpurid + "', '" + Asptblpur1id + "' ,'" + Compcode + "' ,'" + Pono + "' , '" + Colorname + "','" + Portion + "','" + Sizename + "','" + Orderqty + "','" + Colorname1 + "','" + Finyear + "','F','F','F','" + Restitching + "','" + Rechecking + "','" + Sno + "','" + Inward + "','" + Delivery + "','" + Panelmistake + "','" + Barcode + "','" + Barcode3 + "','F','CUTTING',date_format('" + Modified + "','%Y-%m-%d'))";
            await Utility.ExecuteNonQuery(Class.Users.Query);
        }

        internal virtual async Task DeleteCommond()
        {
            string del1 = "delete from Asptblpurdet1  where compcode='" + Compcode + "'  and finyear='" + Finyear + "' and Asptblpurid='" + Asptblpurid + "' and Pono='" + Pono + "'";
            await Utility.ExecuteNonQuery(del1);
        }

        internal async Task<DataTable> SelectCommond(long compcode, string barCodeType, string finyear, long asptblpurid)
        {
            string ss = "select   min(a.Barcode1) as Barcode ,max(a.Barcode1) as Barcode1 from Asptblpurdet1 a join Asptblpur b on a.Asptblpurid=b.Asptblpurid where a.Compcode='" + compcode + "' and b.Barcodetype='" + barCodeType + "'   and a.Finyear='" + finyear + "' and  a.Asptblpurid='" +asptblpurid + "'";
            DataSet ds0 = await Utility.ExecuteSelectQuery(ss, "asptblpur");
            DataTable dt0 = ds0.Tables["asptblpur"];
            return dt0;
        }

        internal async Task<DataTable> SelectCommond(long compcode, string finyear, string pono, long asptblpurid,string barCodeType)
        {
          
            string ss = "select cast(a.Barcode as char) AS QRCODE,a.Asptblpurdetid,a.Asptblpurid,a.Asptblpur1id,a.Compcode,a.Pono,a.Colorname,a.Sizename,a.Orderqty,a.Portion,a.Colorname1,a.Barcode1 as Pono1 from Asptblpurdet1 a  join Asptblpur b on a.Asptblpurid=b.Asptblpurid  where a.Compcode='" + compcode + "'  and a.Finyear='" + finyear + "'  and a.Pono='" + pono + "' AND a.Asptblpurid='" + asptblpurid + "' and b.Barcodetype='"+barCodeType+"'";
            DataSet ds0 = await Utility.ExecuteSelectQuery(ss, "asptblpur");
            DataTable dt0 = ds0.Tables["asptblpur"];
            return dt0;
        }

        //public async Task<string> PurchasesModel1details(string barcode, string barcode3, long asptblpurdetid, long asptblpurid, long asptblpur1id, long compcode, string pono, string colorname, int portion, long sizename, long orderqty, string colorname1, string finyear, string cutting, string stitching, string checking, string restitching, string rechecking, long sno, string inward, string delivery, object panelmistake1, object modified2)
        //{
        //    this.barcode = barcode;
        //    this.barcode3 = barcode3;
        //    this.asptblpurdetid = asptblpurdetid;
        //    this.asptblpurid = asptblpurid;
        //    this.asptblpur1id = asptblpur1id;
        //    this.compcode = compcode;
        //    this.pono = pono;
        //    this.colorname = colorname;
        //    this.portion = portion;
        //    this.sizename = sizename;
        //    this.orderqty = orderqty;
        //    this.colorname1 = colorname1;
        //    this.finyear = finyear;
        //    this.cutting = cutting;
        //    this.stitching = stitching;
        //    this.checking = checking;
        //    this.restitching = restitching;
        //    this.rechecking = rechecking;
        //    this.sno = sno;
        //    this.inward = inward;
        //    this.delivery = delivery;
        //    panelmistake = panelmistake;
        //    modified = modified;
        //    Class.Users.Query = "update  asptblpurdet1  set barcode='" + barcode + "', panelmistake='" + panelmistake + "', colorname='" + colorname + "', portion='" + portion + "', sizename='" + sizename + "', orderqty='" + orderqty + "' , restitching='" + restitching + "' , rechecking='" + rechecking + "',inward='" + inward + "' , delivery='" + delivery + "', panelmistake='" + panelmistake + "', processcheck='F',ISSUETYPE='CUTTING',modified=date_format('" + modified + "','%Y-%m-%d') where  asptblpurid='" + asptblpurid + "' AND  compcode='" + compcode + "'  AND pono='" + pono + "' ";
        //   await Utility.ExecuteNonQuery(Class.Users.Query);
        //}


        //internal async Task<System.Data.DataTable> Select(string barcode, long asptblpurdetid, long asptblpurid, long asptblpur1id, long compcode, string pono, string colorname, long portion, string sizename, long sno, long orderqty)
        //{
        //    this.asptblpurdetid = asptblpurdetid;
        //    this.asptblpurid = asptblpurid;
        //    this.asptblpur1id = asptblpur1id;
        //    this.compcode = compcode;
        //    this.pono = pono;
        //    this.colorname = colorname;
        //    this.portion = portion;
        //    this.sizename = sizename;
        //    this.sno = sno;
        //    this.orderqty = orderqty;
        //    this.barcode = barcode;
        //    Class.Users.Query = "select distinct asptblpurdet1id   from  asptblpurdet1   where  barcode='" + barcode + "' and asptblpurdetid='" + asptblpurdetid + "' and asptblpurid='" + asptblpurid + "' AND   asptblpur1id='" + asptblpur1id + "' and compcode='" + compcode + "' and  pono='" + pono + "'  and  colorname='" + colorname + "' and sizename='" + sizename + "' and sno='" + sno + "' and orderqty = '" + orderqty + "'  ";// and orderqty = '" + pp.orderqty + "'
        //    DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptblpurdet1");
        //    System.Data.DataTable dt = ds.Tables["asptblpurdet1"];
        //    return dt;
        //}

        //public async Task<System.Data.DataTable>  PurchasesModel1details(string barcode, long asptblpurid, long asptblpurdetid, long compcode, string pono, string modified, long asptblpurdet1id)
        //{
        //    this.barcode = barcode;
        //    this.asptblpurid = asptblpurid;
        //    this.asptblpurdetid = asptblpurdetid;
        //    this.compcode = compcode;
        //    this.pono = pono;
        //    this.modified = modified;
        //    this.asptblpurdet1id = asptblpurdet1id;
        //    Class.Users.Query = "update  asptblpurdet1  set barcode='" + barcode + "' ,asptblpurid='" + asptblpurid + "',pono='" + pono + "',asptblpurdetid='" + asptblpurdetid + "',modified=date_format('" + modified + "','%Y-%m-%d') where   asptblpurdet1id='" + asptblpurdet1id + "'";
        //   await Utility.ExecuteNonQuery(Class.Users.Query);
        //}

    }
}

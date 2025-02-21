using System.Data;
using System.Text;

namespace ReactWebApplication.Models.Transactions.SRG
{
    public class DefectEntry:CommonClass
    {
        public StringBuilder Sb = new StringBuilder();
        public Int64 Asptblcutpanretid {get;set;}
        public Int64 Asptblcutpanret1id { get; set; }       
        public Int64 Asptblpurdet1id { get; set; }
        public Int64 Asptblpurdetid { get; set; }
        public Int64 Asptblpurid { get; set; }        
        public Int64 Docid { get; set; }
        public string Shortcode { get; set; }
        public string Panelno { get; set; }
        public string Finyear { get; set; }
        public string Cutpaneldate { get; set; }
        public Int64 Compcode { get; set; }
        public string Pono { get; set; }
        public Int64 Orderqty { get; set; }       
        public Int64 Buyer { get; set; }
        public string Lotno { get; set; }
        public string Bundle { get; set; }
        public Int64 Stylename { get; set; }       
        public string Issuetype { get; set; }
        public string Panelmistake { get; set; }
        public Int64 Processname { get; set; }
        public string Remarks { get; set; }
        public string Cutting { get; set; }
        public string Delivery { get; set; }
        public string Stitching { get; set; }
        public string Checking { get; set; }
        public string Restitching { get; set; }
        public string Rechecking { get; set; }
        public string DefectType { get; set; }
        public string Notes { get; set; }
        public Int64 Compcode1 { get; set; }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override async Task<DataTable> SelectCommond()
        {
            string selmax = "select max(Asptblcutpanretid) as Asptblcutpanretid,max(Asptblcutpanret1id) as Asptblcutpanret1id  from  Asptblcutpanret   where pono='" + Pono + "'  and finyear ='" + Finyear + "' and compcode ='" + Compcode + "'";
            DataSet dsmax = await Utility.ExecuteSelectQuery(selmax, "Asptblcutpanret");
            DataTable dtmax = dsmax.Tables["Asptblcutpanret"];
            return dtmax;
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
        }
    }

    public class DefectEntryDet:DefectEntry
    {
        public Int64 Asptblcutpanretdetid { get; set; }  
        public string Barcode { get; set; }
        public string Colorname { get; set; }
        public string Sizename { get; set; }
        public int Pcs { get; set; }


        public string Processcheck { get; set; }

    }
}

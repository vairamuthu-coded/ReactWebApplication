using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{
    public class BuyerMasterModel:CommonClass
    {
        [Key]
        public Int64 asptblbuymasid { get; set; }
        public Int64 asptblbuymasid1 { get; set; }
        public string compcode { get; set; }
        public string compname { get; set; }
        public string buyercode { get;set; }
        public string buyername { get; set; }
        public string buyingagent { get; set; }
        public string address { get; set; }
        public Int64 city { get; set; }
        public Int64 state { get; set; }
        public Int64 country { get; set; }
        public Int64 pinCode { get; set; }        
        public Int64 phoneNo { get; set; }    
        public string email { get; set; }
        public string website { get; set; }
        public string contactname { get; set; }

        public override Task DeleteCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override async Task<DataTable> GridLoad()
        {
            DataTable dt = new DataTable();
            return dt;
        }

        public override Task<DataTable> GridLoad(long id)
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond(long id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
        }

        //public override Task<DataTable> GridLoad()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task InsertCommond()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<DataTable> SelectCommond()
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task UpdateCommond()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

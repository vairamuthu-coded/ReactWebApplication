using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{
    public class FabricMaster:CommonClass
    {
        [Key]
        public Int64 asptblfabmasid { get; set; }
        public Int64 fabrictype { get; set; }
        public int per { get; set; }
        public string organic { get; set; }
        public  string per1 { get; set; }
        public Int64 yarnblend1 { get; set; }
        public string per2 { get; set; }
        public Int64 yarnblend2 { get; set; }
        public string per3 { get; set; }
        public Int64 yarnblend3 { get; set; }
        public string per4{ get; set; }
        public Int64 yarnblend4 { get; set; }
        public string per5 { get; set; }
        public Int64 yarnblend5 { get; set; }
        public string fabric { get; set; }
        public string aliasname { get; set; }
        public string hsn { get; set; }
        public Int64 compcode { get; set; }

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

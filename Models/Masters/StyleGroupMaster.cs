using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{
    public class StyleGroupMaster:CommonClass
    {
        [Key]
        public Int64 asptblstygrpmasid { get; set; }
        public string stylegroup { get; set; }
        public Int64 stylecategory { get; set; }
        public string productstylegroup { get; set; }
        public string shortcode { get; set; }
        public Int64 compcode { get; set; }



        public override async Task<DataTable> GridLoad()
        {
            DataTable dt = new DataTable();
            return dt;
        }
       
        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
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

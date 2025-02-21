using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ReactWebApplication.Models.TreeView
{
    public class NavigationMaster:CommonClass
    {
        [Key]
        public Int64 menuid { get; set; }
        public Int64 menunameid { get; set; }
        public string? menuname { get; set; }
        public string? navurl { get; set; }
        public Int64? parentmenuid { get; set; }
        public Int64? compcode { get; set; }

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

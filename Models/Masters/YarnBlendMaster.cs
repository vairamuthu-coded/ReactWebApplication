using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.Masters
{
    public class YarnBlendMaster:CommonClass
    {
        [Key]
        public Int64 asptblyarblemasid { get; set; }

        [Required]
        [MaxLength(100)]
        public string? yarnblend { get; set; }
        public Int64? compcode { get; set; }

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
    }
}

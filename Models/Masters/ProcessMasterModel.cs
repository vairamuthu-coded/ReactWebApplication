using System.ComponentModel.DataAnnotations;

namespace ReactWebApplication.Models.Masters
{
    public class ProcessMasterModel
    {
        [Key] 
        public Int64 asptblpromasid { get; set; }
        public string processname { get; set; }
        public string active { get; set; }
    }
}

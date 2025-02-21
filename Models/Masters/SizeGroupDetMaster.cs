using System.ComponentModel.DataAnnotations;

namespace ReactWebApplication.Models.Masters
{
    public class SizeGroupDetMaster
    {

        [Key]
        public Int64 asptblsizgrpdetid { get; set; }

        public Int64 asptblsizgrpid { get; set; }
      

        public Int64 sizename { get; set; }

        public string sizegroup { get; set; }
        public string notes { get; set; }




    }
}

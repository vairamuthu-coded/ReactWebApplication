using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ReactWebApplication.Models.Masters
{
    public class ColorMaster
    {

     
        public int asptblcolmasid { get; set; }
        public string colorname { get; set; }
        public string active { get; set; }
        //public Int64 username { get; set; }
        //public string createdby { get; set; }
        //public string createdon { get; set; }

        //public string modifiedon { get; set; }

        //public string modifiedby { get; set; }
        //public string ipaddress { get; set; }
    }
}

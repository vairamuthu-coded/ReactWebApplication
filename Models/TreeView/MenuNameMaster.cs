using ReactWebApplication.Models.Masters;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ReactWebApplication.Models.TreeView
{
    public class MenuNameMaster :CommonClass
    {

      
        private Int64 menunameid { get; set; }
        private string? menuname { get; set; }
        private Int64? parentmenuid { get; set; }
        private string? aliasname { get; set; }

        public Int64 Menunameid { get { return menunameid; } set { menunameid = value; } }
        public string? Menuname { get { return menuname; } set { menuname = value; } }
        public Int64? Parentmenuid { get { return parentmenuid; } set { parentmenuid = value; } }
        public string? Aliasname { get { return aliasname; } set { aliasname = value; } }

   




        public override async Task UpdateCommond()
        {
            string up = "update  asptblmenuname  set  menuname='" + Menuname + "' ,aliasname='" + Aliasname + "' , active='" + Active + "' , parentmenuid=" + Parentmenuid + ",createdby='" + Createdby + "',modifiedon='" + Modifiedon + "',ipaddress='" + Ipaddress + "' where menunameid=" + Menunameid;
            await Utility.ExecuteNonQuery(up);
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override async Task InsertCommond()
        {
            string ins = "insert into asptblmenuname  (menuname,aliasname,active,parentmenuid,createon,createdby,modifiedon,ipaddress) values('" + Menuname + "','" + Aliasname + "','" + Active + "', " + Parentmenuid + ",'" + Createdon + "','" + Createdby + "','" + Modifiedon + "','" + Ipaddress + "')";
            await Utility.ExecuteNonQuery(ins);
        }

        public override async Task<DataTable> GridLoad()
        {
            DataTable dt = new DataTable();
            return dt;
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
}

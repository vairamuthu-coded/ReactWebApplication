using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Emit;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using ReactWebApplication.Models.Masters;

namespace ReactWebApplication.Models.TreeView
{
    public class UserMaster
    {

        [Key]
        public Int64 Userid { get; set; }
            public Int64 finyear { get; set; }
        public Int64 compcode { get; set; }
        public string empno { get; set; }
        public Int64 dept { get; set; } = 0;
        public string username { get; set; }

        public string Password { get; set; }
        public string active { get; set; }
        public Int64 empname { get; set; }
         
        
        public string gatename { get; set; }
           
            public string NewPassword { get; set; }
            public string SessionTime { get; set; }

      

        public string createdby { get; set; }
        public string createdon { get; set; }

        public string modifiedon { get; set; }

        public string modifiedby { get; set; }
        public string ipaddress { get; set; }

        internal async Task<DataTable> Select(Int64 compCode, Int64 eMPNAME, Int64 dept, string userName, string gateName, string password, string active, string sessiontime)
        {
            compcode = compCode; empname = eMPNAME; dept = dept; userName = userName; gatename = gateName; Password = password;
            active = active; SessionTime = sessiontime;
            string sel = "select  username  from asptblusermas where   compcode='" + compCode + "' AND EMPNAME='" + empname + "' AND DEPT='" + dept + "' " +
            "and  username='" + username + "'  and  gatename='" + gateName + "'  and  pasword='" + Password + "'  and  active='" + active + "' and  SessionTime='" + SessionTime + "'";
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblusermas");
            DataTable dt = ds.Tables["asptblusermas"];
            return dt;
        }
        public UserMaster(long finYear, Int64 compcode, long eMPNAME, long dept, string username, string gateName, string password, string active, string ipaddress, string createdon, string sessionTime)
        {
            finyear = finYear;
            compcode = compcode;
            empname = eMPNAME;
            dept = dept;
            username = username;
            gatename = gateName;
            Password = password;
            active = active;
            this.ipaddress = ipaddress;
            createdon = createdon;
            SessionTime = sessionTime;
            string ins = "insert into asptblusermas (finyear,  compcode,  empname ,  dept ,  username ,gatename,  pasword ,newpassword,  active ,  ipaddress,createdon)values('" + finyear + "','" + compcode + "'," + empname + ",'" + dept + "','" + username + "','" + gatename + "','" + Password + "','" + NewPassword + "','" + active + "','" + ipaddress + "','" + createdon + "','" + SessionTime + "')";
            Utility.ExecuteNonQuery(ins);
        }

        public UserMaster(long finYear, Int64 compcode, long eMPNAME, long dept, string username, string gateName, string password, string active, string ipaddress, string createdon, string sessionTime, long userid)
        {
            finyear = finYear;
            compcode = compcode;
            empname = eMPNAME;
            dept = dept;
            username = username;
            gatename = gateName;
            Password = password;
            active = active;
            this.ipaddress = ipaddress;
            createdon = createdon;
            SessionTime = sessionTime;
            string up = "update asptblusermas set finyear='" + finyear + "',  compcode='" + compcode + "',   empname=" + empname + ",   dept='" + dept + "',   username='" + username + "', gatename='" + gateName + "',  pasword='" + Password + "',  active='" + active + "',   ipaddress='" + ipaddress + "', createdon='" + createdon + "' ,SessionTime='" + sessionTime + "' where userid='" + Userid + "'";
            Utility.ExecuteNonQuery(up);
        }

        public UserMaster()
        {
        }

        public async Task<DataTable> Select(string pro,string com,string use,string pas)
        {
            Class.Users.ProjectID = pro; Class.Users.HCompcode = com; Class.Users.HUserName = use; Class.Users.PWORD = pas;
            string sel = "select  distinct a.compcode ,b.userid, b.username ,a.compname ,b.gatename,a.gtcompmastid ,b.sessiontime  from   " + Class.Users.ProjectID + ".gtcompmast  a " +
                "join asptblusermas b on a.gtcompmastid = b.compcode    where a.compcode='" + Class.Users.HCompcode + "'      and b.username='" + Class.Users.HUserName + "'  and b.pasword = '" + Class.Users.PWORD + "' and  b.active='T'  order by 1";//and b.pasword = '" + Class.Users.PWORD + "'
            DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptblusermas");
            DataTable dt = ds.Tables["asptblusermas"];

            Class.Users.HUserName = dt.Rows[0]["username"].ToString();
            Class.Users.USERID = Convert.ToInt64(dt.Rows[0]["userid"].ToString());
            Class.Users.HGateName = System.DateTime.Now.Year + "/" + dt.Rows[0]["gatename"].ToString();
            Class.Users.HCompName = dt.Rows[0]["compname"].ToString();
            Class.Users.LoginTime = Convert.ToInt32("0" + dt.Rows[0]["sessiontime"].ToString());
            Class.Users.COMPCODE = Convert.ToInt64(dt.Rows[0]["gtcompmastid"].ToString());
            Class.Users.Log = System.DateTime.Now.AddDays(1);

            return dt;
        }

    }

}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ReactWebApplication.Models.TreeView
{

    public class UserRightsModel : CommonClass
    {
        [Key]
        public Int64 userrightsid { get; set; }
        public Int64 menuid { get; set; }
        public string? menuname { get; set; }
        public string? navurl { get; set; }
        public Int64? parentmenuid { get; set; }
        public string? news { get; set; }
        public string? saves { get; set; }
        public string? prints { get; set; }

        public string? readonlys { get; set; }
        public string? search { get; set; }
        public string? imports { get; set; }
        public string? deletes { get; set; }
        public string? treebutton { get; set; }
        public string? globalsearch { get; set; }
        public string? login { get; set; }
        public string? changepassword { get; set; }

        public string? changeskin { get; set; }
        public string? download { get; set; }

        public string? contact { get; set; }
        public string? pdf { get; set; }
        public string? aliasname { get; set; }
        public Int64? compcode { get; set; }
        public int? sno { get; set; }



        public override Task InsertCommond()
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> SelectCommond()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateCommond()
        {
            throw new NotImplementedException();
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
    public class userRights()
    {
        //    public async Task<DataTable> treefilter()
        //    {
        //        Class.Users.Query = "select a.menuid,e.alisname as menuname,a.navurl,a.parentmenuid from asptbluserrights a  join gtcompmast b on a.compcode=b.compcode  and a.active='T'   join asptblnavigation d on d.menuid=a.menuid   join asptblmenuname  e on e.menunameid=d.menunameid  order by 4,2; ";
        //        DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptbluserrights");
        //        DataTable dt1 = ds.Tables["asptbluserrights"];
        //        return dt1;
        //    }
        //    public async Task<DataTable> treefilter(string s, string ss)
        //    {
        //        Class.Users.Query = "select a.menuid,e.aliasname as menuname,a.navurl,a.parentmenuid from asptbluserrights a  join gtcompmast b on a.compcode=b.compcode   join asptblnavigation d on d.menuid=a.menuid   join asptblmenuname  e on e.menunameid=d.menunameid where b.compcode='" + s + "' and a.username='" + ss + "' and a.active='T' order by 4,2; ";
        //        DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "asptbluserrights");
        //        DataTable dt1 = ds.Tables["asptbluserrights"];
        //        return dt1;
        //    }

        public async Task<DataTable> usercheck(string autotable,string screen)
        {
            string sel0 = "select a.finyear from " + autotable + " a join gtcompmast b on a.compcode = b.gtcompmastid join asptbluserrights c on c.menuid=a.screen where  b.gtcompmastid='" + Class.Users.COMPCODE + "' AND c.aliasname='" + screen.Trim() + "' and c.username='" + Class.Users.USERID + "' and c.compcode='" + Class.Users.COMPCODE + "'";
            DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, autotable);
            DataTable dt0 = ds0.Tables[0];
            if (dt0.Rows.Count > 0)
            {
                Class.Users.Finyear = dt0.Rows[0]["finyear"].ToString(); Class.Users.Digit = 9;
            }
            DataTable dt1 = await headerdropdowns(Class.Users.HCompcode, Class.Users.HUserName, screen);
     
            return dt1;

        }
        public string Encrypt(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];
            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        //    public async Task<DataTable> headerdropdowns()
        //    {
        //        //string sel = "select 0 as menuid, '----select-----'  as menuname from dual union all select  a.menuid,  d.menuname   from  asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username join asptblmenuname d on d.menunameid = a.menuid and c.userid=a.username  where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.Husername + "'  and  a.active='T'  and a.parentmenuid>1 order by 2";// and a.parentmenuid = 1
        //        //DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //        //DataTable dt1 = ds.Tables["asptbluserrights"];

        //        Class.Users.Query = "SELECT distinct a.menuid, e.aliasname as menuname FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.USERID=A.username JOIN ASPTBLNAVIGATION D ON D.MENUID = A.MENUID AND C.USERID=A.username JOIN ASPTBLmenuname E ON E.menuname=D.menuname WHERE B.COMPCODE='" + Class.Users.HCompcode + "' AND C.username='" + Class.Users.Husername + "' AND A.active='T' and a.parentmenuid>1  ORDER BY 1 ";
        //        DataSet ds0 = await Utility.ExecuteSelectQuery(Class.Users.Query, "ASPTBLUSERRIGHTS");
        //        DataTable dt1 = ds0.Tables["ASPTBLUSERRIGHTS"];
        //        return dt1;
        //    }
        //    public async Task<DataTable> TreeView(string s, string ss, int id)
        //    {
        //        //string sel = "select 0 as menuid, '----select-----'  as menuname from dual union all select  a.menuid,  d.menuname   from  asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username join asptblmenuname d on d.menunameid = a.menuid and c.userid=a.username  where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.Husername + "'  and  a.active='T'  and a.parentmenuid>1 order by 2";// and a.parentmenuid = 1
        //        //DataSet ds = Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //        //DataTable dt1 = ds.Tables["asptbluserrights"];

        //        Class.Users.Query = "SELECT a.menuid, e.aliasname as  menuname ,a.parentmenuid FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.USERID=A.username JOIN ASPTBLNAVIGATION D ON D.MENUID = A.MENUID AND C.USERID=A.username JOIN ASPTBLmenuname E ON E.menuname=D.menuname WHERE B.COMPCODE='" + s + "' AND C.username='" + ss + "' and  a.parentmenuid='" + id + "' AND A.active='T'   ORDER BY 1 ";
        //        DataSet ds0 = await Utility.ExecuteSelectQuery(Class.Users.Query, "ASPTBLUSERRIGHTS");
        //        DataTable dt1 = ds0.Tables["ASPTBLUSERRIGHTS"];
        //        return dt1;
        //    }
        //    public async Task<DataTable> headerdropdowns(string s, string ss)
        //    {
        //        Class.Users.Query = " SELECT A.USERRIGHTSID,D.MENUID,E.aliasname as  menuname,D.NAVURL ,A.parentmenuid,A.active,A.saves,A.prints,A.readonlys,A.search,A.DELETES,A.news,A.treebutton,A.globalsearch,A.login,A.changepassword,A.changeskin,A.download,A.CONTACT,A.pdf,A.imports,B.COMPCODE,C.username FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON   B.gtcompmastid = A.COMPCODE JOIN asptblusermas C ON C.USERID = A.username JOIN ASPTBLNAVIGATION D ON D.MENUID=A.MENUID    JOIN asptblmenuname e on E.menunameID=D.menunameID   WHERE B.COMPCODE = '" + s + "' AND C.username = '" + ss + "' order by 1";
        //        DataSet ds = await Utility.ExecuteSelectQuery(Class.Users.Query, "ASPTBLUSERRIGHTS");
        //        DataTable dt1 = ds.Tables["ASPTBLUSERRIGHTS"];

        //        return dt1;
        //    }

        public async Task<DataTable> headerdropdowns(string s, string ss, string sss)
        {
            try
            {
                Class.Users.Query = "SELECT e.aliasname as menuname, A.NAVURL,A.active,A.news, A.saves,A.prints,A.readonlys,A.search,A.deletes,A.treebutton,A.globalsearch,A.login,A.changepassword,  A.changeskin,A.download,A.pdf,A.imports,A.contact, A.compcode,A.username FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.compcode JOIN ASPTBLUSERMAS C ON C.USERID=A.username JOIN ASPTBLNAVIGATION D ON D.MENUID = A.MENUID AND C.USERID=A.username JOIN ASPTBLmenuname E ON E.menunameID=D.menunameID WHERE B.compcode='" + s.ToString().Trim() + "' AND C.username='" + ss.ToString().Trim() + "' AND A.active='T' AND E.aliasname='" + sss.ToString().Trim() + "'    ORDER BY 1 ";
                DataSet ds0 = await Utility.ExecuteSelectQuery(Class.Users.Query, "ASPTBLUSERRIGHTS");
                DataTable dt0 = ds0.Tables["ASPTBLUSERRIGHTS"];
                if (dt0 == null)
                {

                    //  GlobalVariables.Toolstrip1.Visible = false;

                }
                return dt0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
               }
            public async Task<DataTable> headerdropdowns(string s, string ss, Int64 sss)
        {
            try
            {
                //change-12-10
                // string f = "SELECT A.menuid,a.parentmenuid, e.menuname FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.compcode JOIN ASPTBLUSERMAS C ON C.USERID=A.username JOIN ASPTBLNAVIGATION D ON D.MENUID = A.MENUID AND C.USERID=A.username JOIN ASPTBLmenuname E ON E.menunameID=D.menunameID  WHERE B.compcode='" + s + "' AND C.username='" + ss + "' AND A.active='T' AND a.PARENTMENUID='" + sss + "'    ORDER BY 3 ";

                string f = "SELECT A.menuid,a.parentmenuid, e.aliasname as menuname FROM ASPTBLUSERRIGHTS A JOIN GTCOMPMAST B ON B.GTCOMPMASTID=A.COMPCODE JOIN ASPTBLUSERMAS C ON C.USERID=A.username JOIN ASPTBLNAVIGATION D ON D.MENUID = A.MENUID AND C.USERID=A.username JOIN ASPTBLmenuname E ON E.menunameID=D.menunameID  WHERE B.COMPCODE='" + s + "' AND C.username='" + ss + "' AND A.active='T' AND a.PARENTMENUID='" + sss + "'    ORDER BY 3 ";
                DataSet ds0 = await Utility.ExecuteSelectQuery(f, "ASPTBLUSERRIGHTS");
                DataTable dt0 = ds0.Tables["ASPTBLUSERRIGHTS"];

                return dt0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //    public async Task<DataTable> userid()
        //    {
        //        DataTable dt1 = new DataTable();
        //        //if (Class.Users.HCompcode == "SCTS")
        //        //{
        //        //    string sel = "  select distinct b.userid,b.username   from  asptblschoolmas a join  asptblusermas b on a.asptblschoolmasid= b.compcode  ";
        //        //    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        //    dt1 = ds1.Tables["asptblusermas"];
        //        //}
        //        //else
        //        //{
        //        string sel = "select distinct b.userid,b.username   from  gtcompmast a join  asptblusermas b on a.gtcompmastid= b.compcode  ";
        //        DataSet ds1 =await Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        dt1 = ds1.Tables["asptblusermas"];
        //        // }
        //        return dt1;
        //    }
        //    public async Task<DataTable> userid(string id)
        //    {
        //        DataTable dt1 = new DataTable();
        //        //if (Class.Users.HCompcode == "SCTS")
        //        //{
        //        //    string sel = "  select distinct b.userid,b.username   from  asptblschoolmas a join  asptblusermas b on a.asptblschoolmasid= b.compcode where a.compcode='" + id + "'  ";
        //        //    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        //    dt1 = ds1.Tables["asptblusermas"];
        //        //}
        //        //else
        //        //{
        //        string sel = "select distinct b.userid,b.username   from  gtcompmast a join  asptblusermas b on a.gtcompmastid= b.compcode where a.compcode='" + id + "'  ";
        //        DataSet ds1 =await Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        dt1 = ds1.Tables["asptblusermas"];
        //        //}
        //        return dt1;
        //    }

        //    public async Task<DataTable> userid(string id, string idd)
        //    {
        //        DataTable dt1 = new DataTable();
        //        //if (Class.Users.HCompcode == "SCTS")
        //        //{
        //        //    string sel = "select distinct b.userid,b.username   from  asptblschoolmas a join  asptblusermas b on a.asptblschoolmasid= b.compcode where a.compcode='" + id + "' and b.username='" + idd + "' ";
        //        //    DataSet ds1 = Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        //    dt1 = ds1.Tables["asptblusermas"];
        //        //}
        //        //else
        //        //{
        //        string sel = "select distinct b.userid,b.username   from  gtcompmast a join  asptblusermas b on a.gtcompmastid= b.compcode where a.compcode='" + id + "' and b.username='" + idd + "' ";
        //        DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptblusermas");
        //        dt1 = ds1.Tables["asptblusermas"];
        //        //}
        //        return dt1;
        //    }
        //    public async Task<DataTable> passid(string id)
        //    {

        //        string sel = "select  distinct b.pasword from  gtcompmast a join  asptblusermas b on a.gtcompmastid= b.compcode where a.username='" + id + "'  ";
        //        DataSet ds1 = await Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //        DataTable dt1 = ds1.Tables["asptbluserrights"];
        //        return dt1;
        //    }

        //    internal async Task<DataTable> select(Int64 menuID, string menuname, string navurl, Int64 parentMenuID, string active, string news, string save, string print, string readonlys, string search, string delete, Int64 compCode, Int64 username, string treebutton, string globalsearch, string login, string changepassword, string changeskin, string download, string contact, string pdf, string imports)
        //    {
        //        string sel = " select   userrightsid  from  asptbluserrights  where  menuid=" + menuID + " and menuname='" + menuname + "' and navurl='" + navurl + "' and parentmenuid=" + parentMenuID + " and  active='" + active + "'   and  news='" + news + "' and  saves='" + save + "'  and  prints='" + print + "'  and readonlys='" + readonlys + "'  and  search='" + search + "'   and  deletes='" + delete + "' and  compcode=" + compCode + "  and username=" + username + " and treebutton='" + treebutton + "' and globalsearch='" + globalsearch + "' and login='" + login + "' and changepassword='" + changepassword + "' and changeskin='" + changeskin + "' and download='" + download + "' and contact='" + contact + "' and pdf='" + pdf + "' and imports='" + imports + "'";
        //        DataSet ds =await Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //        DataTable dt = ds.Tables["asptbluserrights"];
        //        return dt;
        //    }
        //    internal async Task<DataTable> select(Int64 menuID, string menuname, Int64 parentMenuID, Int64 compCode, Int64 username)
        //    {
        //        string sel = " select userrightsid  from  asptbluserrights  where  menuid=" + menuID + " and menuname='" + menuname + "' and parentmenuid=" + parentMenuID + " and  compcode=" + compCode + "  and username=" + username;
        //        DataSet ds = await Utility.ExecuteSelectQuery(sel, "asptbluserrights");
        //        DataTable dt = ds.Tables["asptbluserrights"];
        //        return dt;
        //    }

        }

    }

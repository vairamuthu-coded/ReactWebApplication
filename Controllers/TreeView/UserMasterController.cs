using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using ReactWebApplication.Data;
using ReactWebApplication.Models.TreeView;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ReactWebApplication.Controllers.TreeView
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {

        private StringBuilder sel = new StringBuilder(); bool msg = false;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UserMasterController> _logger;
        public UserMasterController(ILogger<UserMasterController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment; Class.Users.ScreenName = "UserMaster";
        }

        //[HttpGet("UserRightsDetails")]
        //public async Task<ActionResult> UserRightsDetails()
        //{
        //    sel.Clear(); DataTable dt1 = new DataTable();
        //    sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.changepassword,a.changeskin, d.username,c.compcode,if(passwords, 'T','F') as passwords  from asptbluserrights a join gtcompmast c on c.gtcompmastid=a.compcode join asptblusermas d on d.userid=a.username;");
        //    DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
        //    dt1 = ds1.Tables["asptbluserrights"];

        //    return new JsonResult(dt1);

        //}

        //[HttpGet("UserRightsDetails/{id}")]
        //public async Task<IActionResult> UserRightsDetails(Int64 id)
        //{
        //    sel.Clear(); DataTable dt1 = new DataTable();
        //    sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.gtcompmastid as compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  where a.userrightsid='" + id + "'");
        //    DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
        //    dt1 = ds1.Tables["asptbluserrights"];

        //    return new JsonResult(dt1);

        //}

        //[HttpGet("UserRightsFilter/{id}")]
        //public async Task<IActionResult> UserRightsFilter(Int64 id)
        //{
        //    sel.Clear(); DataTable dt1 = new DataTable();
        //    sel.Append("select a.userrightsid,a.menuid,a.menuname,a.navurl,a.parentmenuid,a.active,a.news,a.saves,a.prints,a.readonlys,a.search,a.deletes,a.download,a.contact,a.pdf,a.imports,a.treebutton,a.globalsearch,a.login,a.aliasname,a.changepassword,a.changeskin, c.username,b.compcode,if(passwords, 'T','F') as passwords,a.sno  from asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  where a.username='" + id + "'");
        //    DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptbluserrights");
        //    dt1 = ds1.Tables["asptbluserrights"];

        //    return new JsonResult(dt1);

        //}


        //[HttpGet("NavigationMaster/{id}")]
        //public async Task<IActionResult> NavigationMaster(Int64 id)
        //{
        //    sel.Clear(); DataTable dt1 = new DataTable();
        //    sel.Append("select a.menuid,a.menuname,b.menuname as menunameid,a.navurl,a.parentmenuid,a.active,c.compcode,d.username from asptblnavigation a  join asptblmenuname b on a.menunameid=b.menunameid join gtcompmast c on c.gtcompmastid=a.compcode join asptblusermas d on d.userid=a.username  where a.menuid='" + id + "' ;");
        //    DataSet ds1 = await Utility.ExecuteSelectQuery(sel.ToString(), "asptblnavigation");
        //    dt1 = ds1.Tables["asptblnavigation"];
        //    return new JsonResult(dt1);
        //}



        [HttpGet("UserMaster")]
        public async Task<IActionResult> UserMaster()
        {
            DataTable dt; string sel1 = "";

            sel1 = "SELECT A.userid ,A.finyear,C.compcode,A.empno,d.department,A.username,A.pasword,A.active,A.gatename,A.empname,A.newpassword,A.sessiontime  FROM asptblusermas A join gtcompmast c on c.gtcompmastid = a.compcode join asptbldeptmas d on asptbldeptmasid=a.dept  where c.compcode='SAATEX' order by 4 ";// 

            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblusermas");
            dt = ds.Tables["asptblusermas"];

            return new JsonResult(dt);
        }


        [HttpGet("UserMaster/{com}")]
        public async Task<IActionResult> UserMaster(int com)
        {
            DataTable dt; string sel1 = "";


            sel1 = "SELECT A.userid ,A.finyear,C.compcode,A.empno,d.department,A.username,A.pasword,A.active,A.gatename,A.empname,A.newpassword,A.sessiontime  FROM asptblusermas A join gtcompmast c on c.gtcompmastid = a.compcode join asptbldeptmas d on asptbldeptmasid=a.dept  where c.gtcompmastid='" + com + "'  order by 4 ";// 


            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblusermas");
            dt = ds.Tables["asptblusermas"];

            return new JsonResult(dt);
        }

        [HttpGet("UserMaster/{com}/{user}")]
        public async Task<IActionResult> UserMaster(string com, string user)
        {

            DataTable dt; string sel1 = "";
            sel1 = "SELECT A.userid ,b.compcode,b.gtcompmastid,A.username FROM asptblusermas A join gtcompmast b on b.gtcompmastid = a.compcode    where b.compcode='" + com + "' and a.username='" + user + "'   order by 1 ";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblusermas");
            dt = ds.Tables["asptblusermas"];
            Class.Users.HCompcode = dt.Rows[0]["compcode"].ToString();
            Class.Users.COMPCODE = Convert.ToInt64("0" + dt.Rows[0]["gtcompmastid"].ToString());
            Class.Users.USERID = Convert.ToInt64("0" + dt.Rows[0]["userid"].ToString());
            Class.Users.HUserName = dt.Rows[0]["username"].ToString();
            return new JsonResult(dt);
        }


        [HttpGet("Department")]
        public async Task<ActionResult> Department()
        {
            DataTable dt;
            string sel1 = "select a.asptbldeptmasid,a.department from asptbldeptmas a where a.active='T' ";// 
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptbldeptmas");
            dt = ds.Tables["asptbldeptmas"];

            return new JsonResult(dt);

        }


        [HttpGet("ScreenName/{com}/{user}")]
        public async Task<ActionResult> ScreenName(string com, string user)
        {
            DataTable dt;
            await UserMaster(com, user);
            string sel1 = "select a.menuid,a.parentmenuid, a.menuname  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + com + "'      and c.username='" + user + "' and a.parentmenuid>1 and  a.active='T' order by 3,1";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptbluserrights");
            dt = ds.Tables["asptbluserrights"];

            return new JsonResult(dt);

        }

        [HttpGet]
        [Route("Headings/{com}/{user}")]
        public async Task<IActionResult> Headings(string com, string user)
        {
            DataTable dt;
            //string sel1 = "select a.menunameid, a.menuname from asptblnavigation a where a.parentmenuid=1  and a.active='T' order by 1";// 
            string sel1 = "select a.menuid as menunameid, a.menuname ,a.parentmenuid  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + com + "'      and c.username='" + user + "'   and  a.active='T' and  a.parentmenuid=1   order by 1,2";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptbluserrights");
            dt = ds.Tables["asptbluserrights"];

            return new JsonResult(dt);

        }


        [HttpGet("ScreenNameHeadingss")]
        public async Task<ActionResult> ScreenNameHeadingss()
        {
            DataTable dt4 = new DataTable();

            int i = 1;
            if (dt4.Columns.Count <= 0)
            {

                dt4.Columns.Add("menuid");
                dt4.Columns.Add("title");
                dt4.Columns.Add("menunameid");
                dt4.Columns.Add("menuname");
                dt4.Columns.Add("check");

            }
            if (dt4.Columns.Count > 1)
            {

                DataTable dt; DataTable dt2 = new DataTable();
                string sel1 = "select a.menuid,a.parentmenuid,a.aliasname as title, a.menuname  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "' and a.parentmenuid>1 and  a.active='T'   order by 1,3";
                DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptbluserrights");
                dt = ds.Tables["asptbluserrights"];
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["title"].ToString() != "")
                    {
                        dt4.Rows.Add(dr["menuid"].ToString(), dr["title"].ToString(), dr["parentmenuid"].ToString(), dr["menuname"].ToString(), i.ToString());

                    }
                    else
                    {
                        string sel2 = "select distinct a.menuid,a.parentmenuid,a.aliasname as title, a.menuname  from   asptbluserrights a  join gtcompmast b on b.gtcompmastid=a.compcode join asptblusermas c on c.userid=a.username  join asptblnavigation d on d.menuid = a.menuid where  b.compcode='" + Class.Users.HCompcode + "'      and c.username='" + Class.Users.HUserName + "'   and  a.active='T' and  a.menuid='" + dr.ItemArray[1].ToString() + "'  order by 1,3";
                        DataSet ds2 = await Utility.ExecuteSelectQuery(sel2, "asptbluserrights");
                        dt2 = ds2.Tables["asptbluserrights"];
                        dt4.Rows.Add(dr["menuid"].ToString(), dt2.Rows[0]["menuname"].ToString(), dr["parentmenuid"].ToString(), dr["menuname"].ToString(), i.ToString());
                    }
                    i++;
                }
            }
            return new JsonResult(dt4);
        }


        [HttpGet("FindScreenName/{Headings}")]
        public async Task<IActionResult> FindScreenName(string Headings)
        {
            DataTable dt;
            string sel1 = "select a.menuid, a.parentmenuid, a.menuname,b.companylogoo from asptbluserrights a join gtcompmast b on a.compcode=b.gtcompmastid  where a.parentmenuid='" + Headings + "'  and a.compcode='" + Class.Users.COMPCODE + "' and a.username='" + Class.Users.USERID + "'  and a.active='T' order by 1";
            DataSet ds = await Utility.ExecuteSelectQuery(sel1, "asptblmenuname");
            dt = ds.Tables["asptblmenuname"];
            return new JsonResult(dt);

        }


        [HttpPost("UserMaster_Insert_Update")]
        public async Task<JsonResult> UserMaster_Insert_Update(Models.TreeView.UserMaster c)
        {
            c = new Models.TreeView.UserMaster();
            string msg = string.Empty;
            c.compcode = Class.Users.COMPCODE;
            c.username = Class.Users.HUserName;
            c.ipaddress = GenFun.GetLocalIPAddress();
            c.createdon = "date_format('" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')";
            c.createdby = Class.Users.HUserName;
            c.modifiedby = Convert.ToString(System.DateTime.Now.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToLongTimeString());
            DataTable dt1 = await c.Select(c.compcode, c.empname, c.dept, c.username, c.gatename, c.Password, c.active, c.SessionTime);
            if (dt1.Rows.Count != 0)
            {
                msg = Class.Users.child;
            }
            if (dt1.Rows.Count != 0 && Convert.ToInt64(c.Userid) == 0 || Convert.ToInt64(c.Userid) == 0)
            {
                c = new Models.TreeView.UserMaster(c.finyear, c.compcode, c.empname, c.dept, c.username, c.gatename, c.Password, c.active, c.ipaddress, c.createdon, c.SessionTime);
                msg = Class.Users.insert;
            }
            if (c.Userid >= 1)
            {
                c = new Models.TreeView.UserMaster(c.finyear, c.compcode, c.empname, c.dept, c.username, c.gatename, c.Password, c.active, c.ipaddress, c.createdon, c.SessionTime, c.Userid);
                msg = Class.Users.update;
            }

            return new JsonResult(msg);

        }

        [HttpDelete]
        [Route("UserMaster/{id}")]
        public bool UserMaster(long id)
        {
            bool aa = false;
            //var menunameMaster = _context.UserMaster.Find(id);

            //if (menunameMaster != null)
            //{
            //    aa = true;
            //    _context.Entry(menunameMaster).State = EntityState.Deleted;
            //    _context.SaveChanges();
            //}
            //else
            //{
            //    aa = false;
            //}
            return aa;

        }

        //[HttpGet("GenerateReport/{reporttype}")]
        //public async Task<IActionResult> GenerateReport(string reporttype)
        //{
        //   // if (!ModelState.IsValid) { return View("Index", null); }

        //    string renderFormate;
        //    string extension;
        //    string mimeType;
        //    switch (reporttype.ToUpper())
        //    {
        //        case "PDF":
        //            renderFormate = "PDF";
        //            extension = "pdf";
        //            mimeType = "application/pdf";
        //            break;
        //        case "DOCX":
        //            renderFormate = "WORDOPENXML";
        //            extension = "docx";
        //            mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //            break;
        //        case "XLSX":
        //            renderFormate = "EXCELOPENXML";
        //            extension = "xlsx";
        //            mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            break;
        //        case "HTML":
        //            renderFormate = "HTML5";
        //            extension = "html";
        //            mimeType = "text/html";
        //            break;
        //        default: return BadRequest("Invalid Return Type");
        //    }

        //    return await PreparedReport(renderFormate, extension, mimeType);
        //}

        [HttpGet("GenerateReport/{id}")]
        public async Task<IActionResult> GenerateReport(int id)
        {


            string renderFormate = "PDF";
            string extension;
            string mimeType;

            renderFormate = "PDF";
            extension = "pdf";
            mimeType = "application/pdf";

              return await PreparedReport(renderFormate, extension, mimeType, id);

        }

        [NonAction]
        private async Task<IActionResult> PreparedReport(string renderFormate, string extension, string mimeType,int id)
        {

            LocalReport report = new LocalReport();
            DataTable dt1;
            if (id <= 0) { dt1 = await GetUserlList(); }
            else
            {
                dt1 = await GetUserlList(id);
            }
            report.DataSources.Add(new ReportDataSource("DataSet1", dt1));
            var parameters = new[] { new ReportParameter("param1", "RDLC SUB-REPORT") };
            report.ReportPath = $"{this._webHostEnvironment.ContentRootPath}\\Reports\\Report1.rdlc";
            report.SetParameters(parameters);
            var result = report.Render(renderFormate);
            return new JsonResult(result);
            //return File(result, mimeType, $"report.{extension}");

        }

        [NonAction]
        public static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }



        [HttpGet("DownLoadFile")]
        public async Task<IActionResult> DownLoadFile()
        {         

            var DownloadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data//Generatedpdffiles", "C:/Users/vairam/Downloads/rationcard.pdf");
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(DownloadFilePath,out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(DownloadFilePath);
            return new JsonResult(bytes);
            //return File(bytes, contentType, Path.GetFileName(DownloadFilePath));
          

      }


        //[HttpGet("GenerateUrlFromUrl/{urlLink}")]
        //public async Task<IActionResult> GenerateUrlFromUrl(string urlLink)
        //{
        //    var renderer = new ChromePdfRenderer();
        //    var pdf = renderer.RenderUrlAsPdf(urlLink);

        //    string pdffilename = DateTime.Now.Ticks.ToString() + ".pdf";
        //    pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "Data//Generatedpdffiles", pdffilename));
        //    return await DownLoadFile(pdffilename);


        //}

        //private  void Report_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    int userid = int.Parse(e.Parameters["userid"].Values[0].ToString());
        //    DataTable dt0 = GetUserDetailsList().Select("userid" + userid).CopyToDataTable();
        //    ReportDataSource reportDataSource = new ReportDataSource("DataSet2", dt0);
        //    e.DataSources.Add(reportDataSource);
        //}
        [NonAction]
        private async Task<DataTable> GetUserlList()
        {
            string sel0 = "select a.userid,a.username,b.companylogo as images,b.compcode,a.sessiontime,a.active,a.pasword from asptblusermas a join gtcompmast b on a.compcode=b.gtcompmastid  order by 1";
            System.Data.DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, "asptblusermas");
            DataTable dt0 = ds0.Tables[0];

            return dt0;
        }

        [NonAction]
        private   async Task<DataTable> GetUserlList(int id)
        {
            string sel0 = "select a.userid,a.username,b.companylogo as images,b.compcode,a.sessiontime,a.active,a.pasword from asptblusermas a join gtcompmast b on a.compcode=b.gtcompmastid where a.userid='"+id+"' order by 1";
            System.Data.DataSet ds0 = await Utility.ExecuteSelectQuery(sel0, "asptblusermas");
            DataTable dt0 = ds0.Tables[0];         
           
            return dt0;
        }
        //private    DataTable GetUserDetailsList()
        //{
      
        //    var dt1 = new DataTable();
        //    if (dt1.Columns.Count <= 0)
        //    {
        //        dt1.Columns.Add("userid");
        //        dt1.Columns.Add("username");
        //        dt1.Columns.Add("compcode");
        //    }
        //    DataRow row;
        //    for (int i = 50; i < 100; i++)
        //    {
        //        for (int j = 1; j < 3; j++)
        //        {
        //            row = dt1.NewRow();
        //            row["userid"] = i;
        //            row["username"] = "Child Report Details" + j;
        //            row["compcode"] = "Child Report Details" + j;
        //            dt1.Rows.Add(row);
        //        }
        //    }
        //    return  dt1;
        //}
       



    }
}

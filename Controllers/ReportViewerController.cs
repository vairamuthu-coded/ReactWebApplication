
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BoldReports.Web.ReportViewer;

namespace ReactWebApplication.Controllers
{
    // [EnableCors(origins:"*",headers:"*",methods:"*")]
    //[Route("api/[controller]/[action]")]
    public class ReportViewerController : ControllerBase,IReportController
    {

        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReportViewerController(IMemoryCache cache, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _cache = cache;
        }

        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [NonAction]
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string basepath = Path.Combine(_hostingEnvironment.WebRootPath, "//Reports//Report2.rdlc");
            string reportpath = Path.Combine(basepath, reportOption.ReportModel.ReportPath);
            FileStream fileStream = new(reportpath, FileMode.Open, FileAccess.Read);
            MemoryStream reportStream = new MemoryStream();
            fileStream.CopyTo(reportStream);
            reportStream.Position = 0;
            fileStream.Close();
            reportOption.ReportModel.Stream = reportStream;

            reportOption.ReportModel.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "list", Value = ProductList.GetData() });

        }

        [NonAction]
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            if (reportOption.SubReportModel != null)
            {
                reportOption.SubReportModel.DataSources = new BoldReports.Web.ReportDataSourceCollection();
                reportOption.SubReportModel.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "list", Value = ProductList.GetData() });
            }
            //if (reportOption.SubReportModel != null)
            //{
            //    reportOption.SubReportModel.Parameters = new BoldReports.Web.ReportParameterInfoCollection();
            //    reportOption.SubReportModel.Parameters.Add(new BoldReports.Web.ReportParameterInfo()
            //    {
            //        Name = "SalesPersonID",
            //        Values = new List<string>() { "2" }
            //    });
            //}
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }

        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonResult)
        {
            return ReportHelper.ProcessReport(jsonResult, this, _cache);
        }

    }
    }

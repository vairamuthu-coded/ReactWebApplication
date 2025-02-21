using Microsoft.AspNetCore.Mvc;
using ReactWebApplication.Models.Masters;
using System.Data;

namespace ReactWebApplication.Models
{


    public interface InterfaceClass
    {


       

       Task<IActionResult> GridLoad();
        Task<IActionResult> GridLoad(Int64 id);

        Task<IActionResult> SelectCommond();
        Task<IActionResult> DeleteCommond(Int64 id);
       

        //Task<DataTable> PonoDetails(string pono);
        //Task<DataTable> PonoDetails(string compcode, string pono);
        //Task<DataTable> PonoDetails(string compcode, string pono, string sizegroup);
        //Task<DataTable> PonoDetailss(string compcode, string pono, string ordertype);

        //  Task Saves();

        //IEnumerable<CityMaster> Gets();
        //CityMaster GetById(int id);

        //void Prints();
        //void Exit();
        //void ReadOnlys();
        //void Imports();
        //void Pdfs();
        //void ChangePasswords();
        //void DownLoads();
        //void ChangeSkins();
        //void Logins();
        //void GlobalSearchs();
        //void TreeButtons();

    }





}

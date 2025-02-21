using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace ReactWebApplication.Models
{
    public abstract class MainClass: ControllerBase
    { 
        public abstract Task<IActionResult> GridLoad();
        public abstract Task<IActionResult> GridLoad(Int64 id);
        public abstract Task<IActionResult> Saves(string a);
        public abstract Task<IActionResult> DeleteCommond(Int64 id);
    }
    public abstract class AbstractClass
    {    
        public abstract Task<DataTable> SelectCommond();
        public abstract Task InsertCommond() ;
        public abstract Task UpdateCommond();
        public abstract Task<DataTable> GridLoad();  
    } 

}

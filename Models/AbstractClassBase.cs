using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ReactWebApplication.Models
{
    public abstract class AbstractClassBase: ControllerBase
    {
        public abstract Task<IActionResult> GridLoad();
        public abstract Task<IActionResult> GridLoad(Int64 id);
        public abstract Task<IActionResult> Deletes(Int64 id);
        public abstract Task<DataTable> SelectCommond(Int64 id);
        public abstract Task<DataTable> SelectCommond();
        public abstract Task InsertCommond();
        public abstract Task UpdateCommond();
        public void Exits() { }
    }
}
using Microsoft.AspNetCore.Mvc;
using ReactWebApplication.Models.Transactions.SRG;

namespace ReactWebApplication.Models
{
    public  interface ToolStripAccess
    {
       
    Task<JsonResult> News(ProductionLot productionEntry);
        Task<JsonResult> Saves(ProductionLot productionEntry);
    }
}

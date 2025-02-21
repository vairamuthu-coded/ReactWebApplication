using Microsoft.AspNetCore.Mvc;

namespace ReactWebApplication.Models.Service
{
    public interface IService
    {
        public Task<IActionResult> InsertAsync();
        public Task<IActionResult> UpdateAsync();
    }
}

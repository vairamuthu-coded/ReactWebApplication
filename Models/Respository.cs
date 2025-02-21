

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ReactWebApplication.Data;

namespace ReactWebApplication.Models
{
    public class Respository 
    {
        private readonly AppDBContext _context;
     
        public Respository(AppDBContext context)
        {
            _context = context;

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}

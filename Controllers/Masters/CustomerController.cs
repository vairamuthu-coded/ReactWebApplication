using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ReactWebApplication.Data;
using ReactWebApplication.Models;
using System.Security.Cryptography.Xml;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactWebApplication.Controllers.Masters
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDBContext _context;
        // GET: api/<CustomerController>
        public CustomerController(AppDBContext context)
        {
            _context = context;
        }


    }
}

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetapp;
using Newtonsoft.Json;

namespace aspnetapp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        /// <returns></returns>
        [HttpPost("message")]
        public string Message()
        {
            try
            {
                Console.WriteLine("success");
                StreamReader reader = new StreamReader(Request.Body);
                string indata = reader.ReadToEnd();
                Console.WriteLine(indata);
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
        }
    }
}

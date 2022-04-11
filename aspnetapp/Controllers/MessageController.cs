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
            Console.WriteLine("转发消息成功了");
            return "";
        }
    }
}

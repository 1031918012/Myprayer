#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetapp;

namespace aspnetapp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost("message")]
        public string Message()
        {
            Console.WriteLine("消息接受成功");
            return "";
        }
    }
}

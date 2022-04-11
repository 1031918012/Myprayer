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
        public string Message([FromBody] MessageModel messageModel)
        {
            try
            {
                Console.WriteLine("success");
                Console.WriteLine(JsonConvert.SerializeObject(messageModel));
                StreamReader reader = new StreamReader(Request.Body);
                string indata = reader.ReadToEndAsync().Result;
                Console.WriteLine(indata);
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
        }
        public class MessageModel
        {
            public string ToUserName { get; set; }
            public string FromUserName { get; set; }
            public long CreateTime { get; set; }
            public string MsgType { get; set; }
            public string Content { get; set; }
            public long MsgId { get; set; }
        }
    }
}

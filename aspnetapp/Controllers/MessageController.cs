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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqGetAbnormal"></param>
        /// <returns></returns>
        [HttpPost("GetAbnormalPage")]
        public string GetAbnormalPage()
        {
            Console.WriteLine("这个接口接受消息成功了");
            return "1";
        }

        /// <returns></returns>
        [HttpPost("message")]
        public string Message([FromBody]MessageModel messageModel)
        {
            var a = JsonConvert.SerializeObject(messageModel);
            Console.WriteLine(a);
            return a;
        }
        public class MessageModel
        {
            public string ToUserName { get; set; }
            public string FromUserName { get; set; }
            public string CreateTime { get; set; }
            public string MsgType { get; set; }
            public string Content { get; set; }
            public string MsgId { get; set; }
        }
    }
}

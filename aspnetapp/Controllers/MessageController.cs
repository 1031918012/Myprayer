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
    public class CounterController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqGetAbnormal"></param>
        /// <returns></returns>
        [HttpPost("GetAbnormalPage")]
        public async Task<string> GetAbnormalPage()
        {
            Console.WriteLine("����ӿڽ�����Ϣ�ɹ���");
            return "";
        }
    }
}

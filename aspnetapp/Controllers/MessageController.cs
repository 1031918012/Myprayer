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
using System.Net;
using System.Text;

namespace aspnetapp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private static HttpClient _httpClient = new HttpClient ();
        /// <returns></returns>
        [HttpPost("message")]
        public string Message([FromBody] MessageModel messageModel)
        {
            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(messageModel));
                if (messageModel.MsgType == "text")
                {
                    Console.WriteLine(JsonConvert.SerializeObject(messageModel));
                    var weixinAPI = "https://api.weixin.qq.com/cgi-bin/message/custom/send";
                    var content = new StringContent(JsonConvert.SerializeObject(new { touser = messageModel.FromUserName, msgtype = "text", text = new { content = "云托管接收消息推送成功" + JsonConvert.SerializeObject(messageModel) } }), Encoding.UTF8, "application/json");
                    var response = _httpClient.PostAsync(weixinAPI, content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<dynamic>(result);
                    Console.WriteLine(result);
                }
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
            /// <summary>
            /// 开发者微信号
            /// </summary>
            public string ToUserName { get; set; }
            /// <summary>
            /// 发送方帐号（一个OpenID）
            /// </summary>
            public string FromUserName { get; set; }
            /// <summary>
            /// 消息创建时间 （整型）
            /// </summary>
            public long CreateTime { get; set; }
            /// <summary>
            /// 消息类型，文本为text
            /// </summary>
            public string MsgType { get; set; }
            /// <summary>
            /// 文本消息内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 消息id，64位整型
            /// </summary>
            public long MsgId { get; set; }
            /// <summary>
            /// 图片链接（由系统生成）
            /// </summary>
            public string PicUrl { get; set; }
            /// <summary>
            /// 图片消息媒体id，可以调用获取临时素材接口拉取数据。
            /// </summary>
            public long MediaId { get; set; }
            /// <summary>
            /// 语音格式
            /// </summary>
            public string Format { get; set; }
            /// <summary>
            /// 语音识别结果
            /// </summary>
            public string Recognition { get; set; }
        }
    }
}

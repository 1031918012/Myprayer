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
                    var content = new StringContent(JsonConvert.SerializeObject(new { touser = messageModel.FromUserName, msgtype = "text", text = new { content = "���йܽ�����Ϣ���ͳɹ�" + JsonConvert.SerializeObject(messageModel) } }), Encoding.UTF8, "application/json");
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
            /// ������΢�ź�
            /// </summary>
            public string ToUserName { get; set; }
            /// <summary>
            /// ���ͷ��ʺţ�һ��OpenID��
            /// </summary>
            public string FromUserName { get; set; }
            /// <summary>
            /// ��Ϣ����ʱ�� �����ͣ�
            /// </summary>
            public long CreateTime { get; set; }
            /// <summary>
            /// ��Ϣ���ͣ��ı�Ϊtext
            /// </summary>
            public string MsgType { get; set; }
            /// <summary>
            /// �ı���Ϣ����
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// ��Ϣid��64λ����
            /// </summary>
            public long MsgId { get; set; }
            /// <summary>
            /// ͼƬ���ӣ���ϵͳ���ɣ�
            /// </summary>
            public string PicUrl { get; set; }
            /// <summary>
            /// ͼƬ��Ϣý��id�����Ե��û�ȡ��ʱ�زĽӿ���ȡ���ݡ�
            /// </summary>
            public long MediaId { get; set; }
            /// <summary>
            /// ������ʽ
            /// </summary>
            public string Format { get; set; }
            /// <summary>
            /// ����ʶ����
            /// </summary>
            public string Recognition { get; set; }
        }
    }
}

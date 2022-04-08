using FineEx.Express.SDK.Exceptions;
using FineEx.Express.SDK.Model.Enum;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace FineEx.Express.SDK.Helper
{
    public class HttpHelper
    {
        /// <summary>
        /// Http Post(json)
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="postData">JSON数据</param>
        /// <param name="timeout">超时时间 默认没有</param>
        /// <param name="gzip">0：不压缩 1：gzip压缩 2：deflate压缩</param>
        /// <returns></returns>
        public static string HttpJsonPost(string postUrl, string postData, GZipType gzipType = GZipType.None, int timeout = 5000)
        {
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes(postData);
                request = (HttpWebRequest)WebRequest.Create(postUrl);
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentLength = requestData.Length;
                    request.ContentType = "application/json;charset=utf-8";
                    request.KeepAlive = false;

                    //添加压缩 deflate或gzip
                    if (gzipType == GZipType.GZip)
                    {
                        request.Headers.Add("Accept-Encoding", "gzip");
                    }
                    else if (gzipType == GZipType.Deflate)
                    {
                        request.Headers.Add("Accept-Encoding", "deflate");
                    }

                    //是否使用代理
                    request.Proxy = null;
                    //最大连接数 
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    //取消100等待
                    request.ServicePoint.Expect100Continue = false;
                    //数据是否缓冲
                    request.AllowWriteStreamBuffering = false;

                    if (timeout > 0)
                    {
                        request.Timeout = timeout;
                    }
                    //写入信息
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                        requestStream.Close();
                    }
                    //读取信息
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ContentEncoding != null)
                        {
                            if (response.ContentEncoding.ToLower().Contains("gzip"))
                            {
                                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                                {
                                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                    {
                                        result = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                    stream.Close();
                                }
                            }
                            else if (response.ContentEncoding.ToLower().Contains("deflate"))
                            {
                                using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                                {
                                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                    {
                                        result = reader.ReadToEnd();
                                        reader.Close();
                                    }
                                    stream.Close();
                                }
                            }
                        }
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            reader.Close();
                        }
                        response.Close();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SDKException("HTTP请求异常：" + ex.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        /// <summary>
        /// Http Post(json)
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="postData">JSON数据</param>
        /// <param name="timeout">超时时间 默认没有</param>
        /// <returns></returns>
        public static string HttpJsonPost(string postUrl, string postData, int timeout = 5000)
        {
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes(postData);
                request = (HttpWebRequest)WebRequest.Create(postUrl);
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentLength = requestData.Length;
                    request.ContentType = "application/json;charset=utf-8";
                    request.KeepAlive = false;

                    //是否使用代理
                    request.Proxy = null;
                    //最大连接数 
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    //取消100等待
                    request.ServicePoint.Expect100Continue = false;
                    //数据是否缓冲
                    request.AllowWriteStreamBuffering = false;

                    if (timeout > 0)
                    {
                        request.Timeout = timeout;
                    }
                    //写入信息
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                        requestStream.Close();
                    }
                    //读取信息
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            reader.Close();
                        }

                        response.Close();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new SDKException("HTTP请求异常：" + ex.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        /// <summary>
        /// Http Post(form)
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="postData">提交数据</param>
        /// <param name="timeout">超时时间 默认没有</param>
        /// <returns></returns>
        public static string HttpFormPost(string postUrl, string postData, int timeout = 5000)
        {
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes(postData);
                // 设置参数
                request = (HttpWebRequest)WebRequest.Create(postUrl);
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    request.ContentLength = requestData.Length;
                    request.KeepAlive = false;
                    //是否使用代理
                    request.Proxy = null;
                    //最大连接数 
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    //取消100等待
                    request.ServicePoint.Expect100Continue = false;
                    //数据是否缓冲
                    request.AllowWriteStreamBuffering = false;

                    if (timeout > 0)
                    {
                        request.Timeout = timeout;
                    }

                    //写入信息
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                        requestStream.Close();
                    }
                    //读取信息
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            reader.Close();
                        }
                        response.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SDKException("HTTP请求异常：" + ex.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        /// <summary>
        /// Http Post(xml)
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="postData">提交数据</param>
        /// <param name="timeout">超时时间 默认没有</param>
        /// <returns></returns>
        public static string HttpXmlPost(string postUrl, string postData, GZipType gzipType = GZipType.None, int timeout = 5000)
        {
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes(postData);
                // 设置参数
                request = WebRequest.Create(postUrl) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentType = "application/xml;charset=utf-8";
                    request.ContentLength = requestData.Length;

                    //添加压缩 deflate或gzip
                    if (gzipType == GZipType.GZip)
                    {
                        request.Headers.Add("Accept-Encoding", "gzip");
                    }
                    else if (gzipType == GZipType.Deflate)
                    {
                        request.Headers.Add("Accept-Encoding", "deflate");
                    }

                    request.KeepAlive = false;
                    //是否使用代理
                    request.Proxy = null;
                    //最大连接数 
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    //取消100等待
                    request.ServicePoint.Expect100Continue = false;
                    //数据是否缓冲
                    request.AllowWriteStreamBuffering = false;

                    if (timeout > 0)
                    {
                        request.Timeout = timeout;
                    }

                    //写入信息
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                        requestStream.Close();
                    }

                    //读取信息
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ContentEncoding.ToLower().Contains("gzip"))
                        {
                            using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    result = reader.ReadToEnd();
                                    reader.Close();
                                }
                                stream.Close();
                            }
                        }
                        else if (response.ContentEncoding.ToLower().Contains("deflate"))
                        {
                            using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    result = reader.ReadToEnd();
                                    reader.Close();
                                }
                                stream.Close();
                            }
                        }
                        else
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                                reader.Close();
                            }
                        }
                        response.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SDKException("HTTP请求异常：" + ex.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        /// <summary>
        /// Http Post(xml)
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="postData">提交数据</param>
        /// <param name="timeout">超时时间 默认没有</param>
        /// <returns></returns>
        public static string HttpXmlPost(string postUrl, string postData, int timeout = 5000)
        {
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes(postData);
                // 设置参数
                request = WebRequest.Create(postUrl) as HttpWebRequest;
                if (request != null)
                {
                    request.Method = "POST";
                    request.ContentType = "application/xml;charset=utf-8";
                    request.ContentLength = requestData.Length;

                    request.KeepAlive = false;
                    //是否使用代理
                    request.Proxy = null;
                    //最大连接数 
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    //取消100等待
                    request.ServicePoint.Expect100Continue = false;
                    //数据是否缓冲
                    request.AllowWriteStreamBuffering = false;

                    if (timeout > 0)
                    {
                        request.Timeout = timeout;
                    }

                    //写入信息
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                        requestStream.Close();
                    }

                    //读取信息
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                            reader.Close();
                        }
                        response.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SDKException("HTTP请求异常：" + ex.Message);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }
    }
}

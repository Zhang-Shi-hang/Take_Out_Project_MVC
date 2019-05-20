using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Take_Out_Project_MVC.Controllers
{
    public class MZGUserController : Controller
    {
        public string yzm { get; set; }
        // GET: MZGUser
        public ActionResult MZGUser()
        {
            return View();
        }
        public ActionResult MZGShow()
        {
            return View();
        }
        public string YanZheng(string Phone)
        {
            yzm = SJS();
            ViewBag.yzm = yzm;
            HttpCookie cok = new HttpCookie("yzm");
            cok.Value = Server.UrlEncode(yzm);
            cok.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cok);
            string url = "http://v.juhe.cn/sms/send";
            var parameters = new Dictionary<string, string>();
            parameters.Add("mobile", Phone); //接受短信的用户手机号码
            parameters.Add("tpl_id", "159258"); //您申请的短信模板ID，根据实际情况修改
            parameters.Add("tpl_value", "#code#="+yzm); //您设置的模板变量，根据实际情况修改
            parameters.Add("key", "372d980ef4f4c8549b88dea0439860e6");//你申请的key

            string result = sendPost(url, parameters, "post");

            // 代码中JsonObject类下载地址:http://download.csdn.net/download/gcm3206021155665/7458439
            // JsonObject newObj = new JsonObject(result);
          var  newObj =JsonConvert.DeserializeObject(result);
            //String errorCode = newObj["error_code"].Value;
            var c = JsonConvert.DeserializeObject<B>(result);
            if (c.error_code == "0")
            {
                Debug.WriteLine("成功");
                Debug.WriteLine(c);
            }
            else
            {
                //Debug.WriteLine("请求异常");
                Debug.WriteLine(c.error_code + ":" + c.reason);
            }
            return yzm;
        }
       // / <summary>
		/// Http(GET/POST)
		/// </summary>
		/// <param name = "url" > 请求URL </ param >

        /// < param name="parameters">请求参数</param>
		/// <param name = "method" > 请求方法 </ param >

       /// < returns > 响应内容 </ returns >

        static string sendPost(string url, IDictionary<string, string> parameters, string method)
        {
            if (method.ToLower() == "post")
            {
                HttpWebRequest req = null;
                HttpWebResponse rsp = null;
                System.IO.Stream reqStream = null;
                try
                {
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = method;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    req.Timeout = 60000;
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                    byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                    reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (reqStream != null) reqStream.Close();
                    if (rsp != null) rsp.Close();
                }
            }
            else
            {
                //创建请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

                //GET请求
                request.Method = "GET";
                request.ReadWriteTimeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                //返回内容
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;
            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }
        //随机验证码
        public string SJS()
        {
            string s = "";
            int n= 6;
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
               s+= r.Next(0,9);

            }
            return s;
        }
        public class B
        {
            public string error_code { get; set; }
            public string reason { get; set; }
        }
    }
}
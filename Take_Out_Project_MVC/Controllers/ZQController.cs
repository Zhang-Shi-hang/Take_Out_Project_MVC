using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Take_Out_Project_MVC.Models;
using Newtonsoft.Json;
using System.Web.Caching;
using Take_Out_Project_MVC.Filter;

namespace Take_Out_Project_MVC.Controllers
{
    public class ZQController : Controller
    {

        // GET: ZQ
        [AuthorFilter]
        public ActionResult UptShow(string userid)
        {
            var json = HttpClientHelper.Sender("get", "ZQApi/Show?userid=" + userid);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        /// <summary>
        /// 我的显示
        /// </summary>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult ZQIndex()
        {
            HttpCookie cookiePhone = Request.Cookies["UserId"];
            string userid = cookiePhone.Value;
            var json = HttpClientHelper.Sender("get", "ZQApi/Show?userid=" + userid);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        /// <summary> 
        /// 优惠劵显示
        /// </summary>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult ZQYH()
        {
            ViewBag.tb = 0;
            ViewBag.count = 0;
            //string i = id.ToString();
            ViewBag.s = 0;
            ViewBag.ss = 0;
            HttpCookie co = Request.Cookies["UserId"];
            string id = co.Value;
            var json = HttpClientHelper.Sender("get", "ZQApi/ShowYH/" + id);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
     
        public ActionResult ZQJF(int jf)
        {
            ViewBag.j = jf;
            return View();
        }
        public ActionResult Wei()
        {
            ViewBag.ss = 0;
            ViewBag.s = 0;
            ViewBag.sum = 0;
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = Server.UrlDecode(cookie.Value);
            ViewBag.uid = UserId;
            string json = HttpClientHelper.Sender("get", "ZQApi/Wei?UserId=" + UserId);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            list = list.Where(s => s.OrderStatic == 0).ToList();
            return View(list);
        }

    }
}
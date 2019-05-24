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
        public ActionResult UptShow(string phone)
        {
            var json = HttpClientHelper.Sender("get", "ZQApi/Show?phone=" + phone);
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
            HttpCookie cookiePhone = Request.Cookies["Phone"];
            string phone = cookiePhone.Value;
            var json = HttpClientHelper.Sender("get", "ZQApi/Show?phone=" + phone);
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
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = Server.UrlDecode(cookie.Value);
            ViewBag.uid = UserId;
            string json = HttpClientHelper.Sender("get", "Wqb/OrderParticulars?UserId=" + UserId);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            list = list.Where(s => s.OrderStatic == 0).ToList();
            return View(list);
        }

    }
}
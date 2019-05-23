using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Take_Out_Project_MVC.Filter;
using Take_Out_Project_MVC.Models;

namespace Take_Out_Project_MVC.Controllers
{
    public class WqbController : Controller
    {
        private object cache;

        public object Cache { get; private set; }

        // GET: Wqb
        [AuthorFilter]
        public ActionResult Main()
        {
            return View();
        }
        /// <summary>
        /// 全部订单显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult OrderShow()
        {
            HttpCookie cookie =Request.Cookies["UserId"];
            string UserId = cookie.Value;
            Session["id"] = UserId;
            return View();
        }
        /// <summary>
        /// 评价界面
        /// </summary>
        /// < returns ></ returns >
        [AuthorFilter]
        public ActionResult Comment()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = cookie.Value;
            return View();
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult OrderParticulars(string UserId)
        {
            HttpCookie cookie = Request.Cookies["UserId"];
             UserId = Server.UrlDecode(cookie.Value);
            string json = HttpClientHelper.Sender("get", "Wqb/OrderParticulars?UserId=" + UserId);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        /// <summary>
        /// 待评价界面显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult CommentShow()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = Server.UrlDecode(cookie.Value);
            Session["id"] = UserId;
            return View();
        }
        /// <summary>
        /// 退款界面显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult RefundShow()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = Server.UrlDecode(cookie.Value);
            Session["id"] = UserId;
            return View();
        }
        /// <summary>
        /// 退款提交页面
        /// </summary>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult Refund()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = cookie.Value;
            return View();
        }
    }
}
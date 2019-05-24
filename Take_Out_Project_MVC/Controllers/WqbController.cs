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
            //从Cookie中获取uid
            HttpCookie uid = Request.Cookies["UserId"];
            ViewBag.uid = Server.UrlDecode(uid.Value);
            //从Cookie中获取Phone
            HttpCookie phone = Request.Cookies["Phone"];
            ViewBag.phone = Server.UrlDecode(phone.Value);

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
        public ActionResult Comment(string OrderId)
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            ViewBag.uid = Server.UrlDecode(cookie.Value);

            HttpCookie Orid = new HttpCookie("OrderId");
            Orid.Value = OrderId;
            ViewBag.oid = Orid.Value;

            return View();
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult OrderParticulars(string OrderId)
        {
            HttpCookie orid = new HttpCookie("OrderId");
            orid.Value = Server.UrlEncode(OrderId);
            string json = HttpClientHelper.Sender("get", "Wqb/OrderParticulars?OrderId=" + OrderId);
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
            //用户ID传输
            HttpCookie cookie = Request.Cookies["UserId"];
            string UserId = Server.UrlDecode(cookie.Value);
            ViewBag.uid = UserId;


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
            ViewBag.uid = UserId;
            return View();
        }
        /// <summary>
        /// 退款提交页面
        /// </summary>
        /// <returns></returns>
        [AuthorFilter]
        public ActionResult Refund(string OrderId)
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            ViewBag.uid = cookie.Value;

            HttpCookie Orid = new HttpCookie("OrderId");
            Orid.Value = OrderId;
            ViewBag.oid = Orid.Value;
            return View();
        }
    }
}
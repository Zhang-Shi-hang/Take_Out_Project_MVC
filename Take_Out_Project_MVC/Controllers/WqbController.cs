using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Take_Out_Project_MVC.Models;

namespace Take_Out_Project_MVC.Controllers
{
    public class WqbController : Controller
    {
        // GET: Wqb
        /// <summary>
        /// 全部订单显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public ActionResult OrderShow(string id = "283799B4-CA3A-44EC-8482-84B07A30A38F")
        {
            Session["id"] = id;
            return View();
        }
        /// <summary>
        /// 评价界面
        /// </summary>
        /// < returns ></ returns >
        public ActionResult Comment()
        {
            return View();
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public ActionResult OrderParticulars(Guid UserId)
        {
            string json = HttpClientHelper.Sender("get", "OrderManage/OrderParticulars?UserId=" + UserId);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        /// <summary>
        /// 待评价界面显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public ActionResult CommentShow(string id = "283799B4-CA3A-44EC-8482-84B07A30A38F")
        {
            Session["id"] = id;
            return View();
        }
        /// <summary>
        /// 退款界面显示
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public ActionResult Refund(string id = "283799B4-CA3A-44EC-8482-84B07A30A38F")
        {
            Session["id"] = id;
            return View();
        }
    }
}
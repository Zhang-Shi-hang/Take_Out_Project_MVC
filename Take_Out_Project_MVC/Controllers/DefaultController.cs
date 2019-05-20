using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Take_Out_Project_MVC.Models;

namespace Take_Out_Project_MVC.Controllers
{
    public class DefaultController : Controller
    {
        public string UserId = "";
        // GET: Default
        public ActionResult Template()
        {
            return View();
        }
        //首页
        public ActionResult Home()
        {
            return View();
        }
        //订单页面
        public ActionResult Classify()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            UserId = Server.UrlDecode(cookie.Value);
            return View();
        }
        //订单备注信息
        public ActionResult Order_notes()
        {
            return View();
        }
        public ActionResult PlanB(string TypeName= "精选扒类")
        //确认支付
        public ActionResult Payment()
        {
            string json = HttpClientHelper.Sender("get", "Zrw/GetGreensInType?TypeName=" + TypeName);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        //支付结果
        public ActionResult Payment_results()
        {
            return View();
        }
        public ActionResult demo()
        {
            string TypeName = "营养套餐";
            string json = HttpClientHelper.Sender("get", "Zrw/GetGreensInType？TypeName=" + TypeName);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
    }
}
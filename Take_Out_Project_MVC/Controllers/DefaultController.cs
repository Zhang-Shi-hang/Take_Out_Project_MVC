using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Newtonsoft.Json;
using Take_Out_Project_MVC.Filter;
using Take_Out_Project_MVC.Models;

namespace Take_Out_Project_MVC.Controllers
{
    public class DefaultController : Controller
    {
        public string UserId = "";
        // GET: Default
        [AuthorFilter]
        public ActionResult Template()
        {
            return View();
        }
        //首页
        //[AuthorFilter]
        public ActionResult Home()
        {
            string result = HttpClientHelper.Sender("get", "/api/Zrw/GetGreens");
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(result);
            list = list.Where(s => s.GreensPrice < 150).ToList();
            return View(list);
        }
        //订单页面
        //[AuthorFilter]
        public ActionResult Classify(string UserId)
        {
            return View();
        }
        //订单备注信息
        public ActionResult Order_notes(string Bianhao,string uid)
        {
            Session["bh"] = Bianhao;
            Session["uid"] = uid;
            return View();
        }
        //确认支付
        public ActionResult Payment()
        {
            Session["bh"] = oen;
            return View();
        }
        //支付结果
        public ActionResult Payment_results()
        {
            Session["bh"] = oen;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Take_Out_Project_MVC.Controllers
{
    public class DefaultController : Controller
    {
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
        public ActionResult Payment(string oen)
        {
            Session["bh"] = oen;
            return View();
        }
        //支付结果
        public ActionResult Payment_results(string oen)
        {
            Session["bh"] = oen;
            return View();
        }


    }
}
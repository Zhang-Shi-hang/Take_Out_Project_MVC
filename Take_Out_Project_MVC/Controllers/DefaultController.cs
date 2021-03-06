﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Newtonsoft.Json;
using Take_Out_Project_MVC.Filter;
using Take_Out_Project_MVC.Models;
using System.IO;

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
        public ActionResult Home(string UserId = "", string Phone = "",int vv=0)
        {
            
            if (UserId != "" && Phone != "")
            {
                HttpCookie cookiePhone = new HttpCookie("Phone");
                cookiePhone.Value = Server.UrlEncode(Phone);
                Response.Cookies.Add(cookiePhone);
                Session["UserId"] = UserId;
                Session["Phone"] = Phone;
                HttpCookie cookie = new HttpCookie("UserId");
                cookie.Value = Server.UrlEncode(UserId);
                Response.Cookies.Add(cookie);
                ViewBag.vv = vv;
                string result = HttpClientHelper.Sender("get", "/api/Zrw/GetGreens");
                var list = JsonConvert.DeserializeObject<List<ViewModel>>(result);
                list = list.Where(s => s.GreensPrice < 150).ToList();
                return View(list);
            }
            else
            {
                Response.Redirect("/MZGUser/MZGUser");
                return null;
            }
        }

        //地图页面
        public ActionResult Map()
        {
            HttpCookie cokUser = Request.Cookies["UserId"];
            ViewBag.UserId = cokUser.Value;
            return View();
        }

        //订单页面
        [AuthorFilter]
        public ActionResult Classify()
        {
            HttpCookie cokUser = Request.Cookies["UserId"];
            ViewBag.UserId = cokUser.Value;
            return View();
        }
        //订单备注信息
        [AuthorFilter]
        public ActionResult Order_notes(string Oen,string GreensIds)
        {
            HttpCookie cokUser = Request.Cookies["UserId"];
            HttpCookie cokGreens = new HttpCookie("GreenIds");
            cokGreens.Value = Server.UrlEncode(GreensIds);
            ViewBag.uid = cokUser.Value;
            ViewBag.Oen = Oen;
            
            return View();
        }
        //确认支付
        [AuthorFilter]
        public ActionResult Payment(string oen,string DiscountsId,string uid)
        {
            ViewBag.uid = uid;
            ViewBag.DiscountsId = DiscountsId;
            ViewBag.oen = oen;
            return View();
        }
        //支付结果
        [AuthorFilter]
        public ActionResult Payment_results(string oen,HttpPostedFileBase file)
        {
            HttpCookie cokUser = Request.Cookies["UserId"];
            ViewBag.uid = Server.UrlDecode(cokUser.Value);
            HttpCookie cookiePhone = Request.Cookies["Phone"];
            ViewBag.phone = Server.UrlDecode(cookiePhone.Value);
            ViewBag.oen = oen;
            string result = HttpClientHelper.Sender("get", "/api/Zrw/GetOrder?Oen=" + oen);
            var mo = JsonConvert.DeserializeObject<List<ViewModel>>(result).FirstOrDefault();
            if (mo.RepastWay == true)
            {
                try
                {
                    HttpCookie cok = Request.Cookies["Phone"];
                    ViewBag.phone = Server.UrlDecode(cok.Value);
                    HttpCookie cok2 = Request.Cookies["UserId"];
                    ViewBag.uid = Server.UrlDecode(cok2.Value);
                    string filename = "/ThumbnailsImages/";
                    QRCode.GetBarCode(oen, Server.MapPath(filename+"a.png"));
                    ViewBag.RepastWay = mo.RepastWay;
                    ViewBag.img = "/ThumbnailsImages/a.png";
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('图片路径错误')</script>");
                    throw;
                }
            }
            return View();
        }
        public void A(string GreensIds)
        {

        }
    }
}
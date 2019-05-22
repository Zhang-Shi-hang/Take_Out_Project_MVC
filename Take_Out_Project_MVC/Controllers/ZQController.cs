using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Take_Out_Project_MVC.Models;
using Newtonsoft.Json;

namespace Take_Out_Project_MVC.Controllers
{
    public class ZQController : Controller
    {

        // GET: ZQ
        public ActionResult UptShow(string phone)
        {
            var json = HttpClientHelper.Sender("get", "ZQApi/Show?phone=" + phone);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);


            return View(list);
        }
        /// <summary>
        /// 我的  显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ZQIndex(string phone = "17600496586")
        {

            var json = HttpClientHelper.Sender("get", "ZQApi/Show?phone=" + phone);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        /// <summary> 
        /// 优惠劵显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ZQYH(string id= "317C1E7F-24E5-479E-8B4E-57867ADC6757")
        {
            //string i = id.ToString();
            var json = HttpClientHelper.Sender("get", "ZQApi/ShowYH/" + id);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
     
        public ActionResult ZQJF(int jf=888)
        {
            ViewBag.j = jf;
            return View();
        }

    }
}
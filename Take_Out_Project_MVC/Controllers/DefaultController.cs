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
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["UserId"];
            UserId = Server.UrlDecode(cookie.Value);
            return View();
        }
        public ActionResult Template()
        {
            return View();
        }
        public ActionResult PlanB(string TypeName= "精选扒类")
        {
            string json = HttpClientHelper.Sender("get", "Zrw/GetGreensInType?TypeName=" + TypeName);
            var list = JsonConvert.DeserializeObject<List<ViewModel>>(json);
            return View(list);
        }
        public ActionResult Map()
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Take_Out_Project_MVC.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Template()
        {
            return View();
        }
        public ActionResult PlanB()
        {
            return View();
        }
        public ActionResult PlanC()
        {
            return View();
        }

    }
}
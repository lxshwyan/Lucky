using Lucky.Core.Repository;
using Lucky.MVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Lucky.MVCTest.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public HomeController(IRepository<CustomerModel> _IRepository)
        {

        }

        public ActionResult Index()
        {
          //  bool bi = ModelState.IsValid;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}
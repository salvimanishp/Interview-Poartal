using InterviewManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            //Session["Password"] = CL.Password;
            return View();
        }

        public ActionResult List()
        {

            Method db = new Method();
            List<Master> obj = db.GetCompanyShowData();
            return View(obj);
        }

       
      

      
    }
}
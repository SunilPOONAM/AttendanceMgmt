using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendenceMgmt.Models;

namespace AttendenceMgmt.Controllers
{
    public class HomeController : Controller
    {
        public DbManager dbmanager;
        public HomeController()
        {
            dbmanager = new DbManager();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sign_In()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sign_In(SignIn_tbl dtls)
        {
            var tid = dbmanager.GetLogindeatils(dtls);
            if (tid.Count == 1)
            {
                Session["UserName"] =tid[0].UserName;
                Session["UserType"] = tid[0].Usertype; ;
                Session["Department"] = tid[0].Department; ;
                if (tid[0].Usertype == "Admin")
                {
                    Response.Write("<script>window.location='/Admin/Index';</script>");
                }
                else if (tid[0].Usertype == "Head")
                {
                    Response.Write("<script>window.location='/Employee/Dashboard';</script>");
                }

                else
                {
                    return RedirectToAction("Dashboard", "Flock_Management");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid UserId And Password');window.location='/Home/Sign_In';</script>");
            }
            return View();
        }
        

        public ActionResult Sign_Up()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sign_Up(CompanyRegistration_tbl dtls, SignIn_tbl dtls1)
        {
            try
            {
                var tid = dbmanager.SaveCompanyDetails(dtls, dtls1);
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Home/Sign_Up';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Home/Sign_Up';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                Session["UserName"] = null;
                Response.Write("<script>alert('Good Byee...');window.location='/Home/Sign_In';</script>");
            }
            catch (Exception ex)
            {

            }
            return View();
        }
       
    }
}
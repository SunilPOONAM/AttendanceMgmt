using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendenceMgmt.Models;
using System.Data;
using PoultryCRM.Models;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;

namespace AttendenceMgmt.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
          public DbManager dbmanager;
          AttendenceMgmtEntities db = new AttendenceMgmtEntities();

          public AdminController()
        {
            dbmanager = new DbManager();
           
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeDetails_tbl dtls, SignIn_tbl dtls1)
        {
            try
            {
                var tid = dbmanager.SaveEmployeeDetails(dtls, dtls1);
                if (tid != 0)
                {

                    Response.Write("<script>alert('Saved Success.....');window.location='/Admin/AddEmployee';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Not Saved.....');window.location='/Admin/AddEmployee';</script>");
                }
            }
            catch (Exception ex)
            {

            }
            return View();

        }


        public ActionResult Department()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Department(Department_Master_tbl dtls)
        {
            try
            {
                var Department = db.Department_Master_tbl.AsQueryable().AsEnumerable().Where(x => x.Department == dtls.Department).Select(x => x.Department = x.Department).ToList();
                if (Department.Count == 0)
                {
                    dtls.CompanyName = "Techno Craft Solutions Lucknow";
                    dtls.Status = true;
                    db.Department_Master_tbl.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Admin/Department';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Admin/Department';</script>");
            return View();
        }
        public ActionResult Designation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Designation(Designation_Master_tbl dtls)
        {
            try
            {
                var Designation = db.Designation_Master_tbl.AsQueryable().AsEnumerable().Where(x => x.Designation == dtls.Designation).Select(x => x.Designation = x.Designation).ToList();
                if (Designation.Count == 0)
                {
                    dtls.CompanyName = "Techno Craft Solutions Lucknow";
                    dtls.Status = true;
                    db.Designation_Master_tbl.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Admin/Designation';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Admin/Designation';</script>");
            return View();
        }
        public ActionResult Shift()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Shift(Shift_Master_tbl dtls)
        {
            try
            {
                var shift = db.Shift_Master_tbl.AsQueryable().AsEnumerable().Where(x => x.Shift == dtls.Shift).Select(x => x.Shift = x.Shift).ToList();
                if (shift.Count == 0)
                {
                    dtls.CompanyName = "Techno Craft Solutions Lucknow";
                    dtls.Status = true;
                    db.Shift_Master_tbl.Add(dtls);
                    db.SaveChanges();
                    Response.Write("<script>alert('Saved Success.....');window.location='/Admin/Shift';</script>");
                }

            }
            catch (Exception ex)
            {

            }
            Response.Write("<script>alert('Alredy Exist....');window.location='/Admin/Shift';</script>");
          
            return View();
        
        }

        public ActionResult GetDepartment()
        {
            var result = db.Department_Master_tbl.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Department, value = x.Id .ToString()}).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDesignation()
        {
            var result = db.Designation_Master_tbl.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Designation, value = x.Id.ToString() }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetShift()
        {
            var result = db.Shift_Master_tbl.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Shift, value = x.Id.ToString() }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TrackEmp()
        {
            return View();
        }
        public ActionResult Employee_Reports()
        {
            return View();
        }
        public ActionResult TrackEmpLocation(string data)
        {
            string msg = "";
            DataTable dt = dbmanager.getfilltername(data);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    msg += "<option>"+dt.Rows[i]["Name"]+"</option>";
                }
            }
            return Json(msg,JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult getlatlang(string D1, string D2, string Name)
        {


            ArrayList Al = new ArrayList();
            DataTable dt = dbmanager.getlatlomg(D1, D2, Name);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] arr = { Name, dt.Rows[i]["Lat"].ToString(), dt.Rows[i]["Long"].ToString() };
                    Al.Add(arr);
                }
            }
            
            var msg = Al;
            
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    
        //public bool SaveImage(string base64String)
        //{
        //    Random rn=new Random();
        //   string ImgName = "base64" + rn.Next(9999);
        //     byte[] imageBytes = Convert.FromBase64String(base64String);
        //    //Check if directory exist
        //    //if (!System.IO.Directory.Exists(path))
        //    //{
        //    //    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //    //}
        //    string imageName = ImgName + ".jpg";
        //    //set the image path
        //    string imgPath = Path.Combine(Server.MapPath("~/Resources/images"), imageName);
        //    System.IO.File.WriteAllBytes(imgPath, imageBytes);
        //    return true;
        //}
	}
}
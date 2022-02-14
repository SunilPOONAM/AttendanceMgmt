using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendenceMgmt.Models;
using System.Data;



namespace AttendenceMgmt.Controllers
{
    public class EmployeeController : Controller
    {
        //public
        AttendenceMgmtEntities db = new AttendenceMgmtEntities();
        DbManager dbmanager;
        public EmployeeController()
        {
            dbmanager = new DbManager();
        }
        // GET: /Employee/
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
          //  var usertype = Session["UserType"].ToString();
            return View();
        }
        public ActionResult GetEmployeeDetails(EmployeeDetails_tbl dtls)
        {
            var data = dbmanager.GetEmployeedeatils(dtls);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFiiterEmpData(string department, string dt1, string dt2)
        {
            string data = "";
            DataTable dt = dbmanager.GetDailyFillterAtteData(department,dt1, dt2);
          if (dt.Rows.Count > 0)
          {int j=0;
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  j++;
                  data += "  <tr class='hid'><td>" +j+ "</td><td>" + dt.Rows[i]["Department"] + "</td><td>" + dt.Rows[i]["Name"] + "</td><td>" + dt.Rows[i]["TimeIn"] + "</td><td>" + dt.Rows[i]["TimeOut"] + "</td></tr>";
              }
          }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetAttendanceData(string department)
        //{
        //    string data = "";
        //    DataTable dt = dbmanager.GetDailyAtteData(department);
        //  if (dt.Rows.Count > 0)
        //  {int j=0;
        //      for (int i = 0; i < dt.Rows.Count; i++)
        //      {
        //          data += "  <tr class='hid'><td>" + j++ + "</td><td>" + dt.Rows[i]["Department"] + "</td><td>" + dt.Rows[i]["Name"] + "</td><td>" + dt.Rows[i]["TimeIn"] + "</td><td>" + dt.Rows[i]["TimeOut"] + "</td></tr>";
        //      }
        //  }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult daily_attendance()
        {
            return View();
        }
        public ActionResult EmployeReport()
        {
            return View();
        }
        public ActionResult GetEmployeeAttendance()
        {
            return View();
        }
        public ActionResult GetAttData(string department)
        {
            string data = "";
            DataTable dt = dbmanager.GetDailyAtte(department);
          if (dt.Rows.Count > 0)
          {int j=0;
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  j++;
                  data += "  <tr class='hid'><td>" +j+ "</td><td>" + dt.Rows[i]["Department"] + "</td><td>" + dt.Rows[i]["Name"] + "</td><td>" + dt.Rows[i]["TimeIn"] + "</td><td>" + dt.Rows[i]["TimeOut"] + "</td></tr>";
              }
          }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        ////////////////////////////////////////////

        public ActionResult ViewEmpDetails()
        {
            return View();
        }
        public ActionResult BindDatalist()
        {

            var result = db.EmployeeDetails_tbl.AsQueryable().AsEnumerable().Select(x => new customSelectList { text = x.Name, value = x.Id.ToString() }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult BindAttemdence(string EmpName)
        //{
        //    var lst = dbmanager.GetEmployeeName(EmpName);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult BindAttemdence(string EmpName)
        {

            string data = "";
            DataTable dt = dbmanager.GetDailyAtte(EmpName);
            if (dt.Rows.Count > 0)
            {
                int j = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    j++;
                   
                    data += "  <tr class='hid'><td>" + j + "</td><td>" + dt.Rows[i]["TimeIn"] + "</td><td>" + dt.Rows[i]["Date"] + "</td><td>" + dt.Rows[i]["TimeOut"] + "</td></tr>";
                }
            }
            else
            {
                data +="<tr class='hid'><td>Absent</td></tr>";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}
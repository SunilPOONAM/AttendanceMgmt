using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace AttendenceMgmt.Models
{
    public class DbManager
    {
       AttendenceMgmtEntities db = new AttendenceMgmtEntities();
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyString"].ConnectionString);

        //Save All Data
        public long SaveCompanyDetails(CompanyRegistration_tbl dtls,SignIn_tbl dtls1)
        {
            try
            {
                dtls.Status = true;
                db.CompanyRegistration_tbl.Add(dtls);
                db.SaveChanges();
                return dtls.Id;
                dtls1.Status = true;
                dtls1.UserName = dtls.Email;
                dtls1.Usertype = "Admin";
                dtls1.Password = dtls.Password;
                db.SignIn_tbl.Add(dtls1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
        public long SaveEmployeeDetails(EmployeeDetails_tbl dtls, SignIn_tbl dtls1)
        {
            try
            {
               
               
                dtls.Status = true;
                dtls.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                dtls.Company_Name = "TCS";
                db.EmployeeDetails_tbl.Add(dtls);
                db.SaveChanges();
                dtls1.Status = true;
                dtls1.UserName = dtls.Email;
                dtls1.Usertype = "Employee";
                dtls1.Password = dtls.Password;
                db.SignIn_tbl.Add(dtls1);
                db.SaveChanges();
                return dtls.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public DataTable getfilltername(string a)
        {
            string qury = "select * from EmployeeDetails_tbl where Name like '"+a+"%'";
            SqlCommand com = new SqlCommand(qury, con);
            SqlDataAdapter sa = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            return dt;
        }
        public List<SignIn_tbl> GetLogindeatils(SignIn_tbl dtls)
        {
            try
            {
                var registration = db.SignIn_tbl.AsEnumerable().Where(x =>
                     x.Password == dtls.Password && x.UserName == dtls.UserName ).Select(
                    x => new SignIn_tbl
                    {
                        UserName = x.UserName,
                        Password = x.Password,
                        Usertype = x.Usertype,
                        Department = x.Department

                    }).ToList();
               
                return registration;

            }
            catch (Exception ex)
            { }

            return new List<SignIn_tbl>();

        }

        public List<EmployeeDetails_tbl> GetEmployeedeatils(EmployeeDetails_tbl dtls)
        {
            try
            {
                var registration = db.EmployeeDetails_tbl.AsEnumerable().Where(x =>
                     x.MobileNo == "7458950577").Select(
                    x => new EmployeeDetails_tbl
                    {
                        Name = x.Name,
                        
                    }).ToList();

                return registration;

            }
            catch (Exception ex)
            { }

            return new List<EmployeeDetails_tbl>();

        }
        public List<EmployeeDetails_tbl> GetFiiterempData(EmployeeDetails_tbl dtls)
        {
            try
            {
                var registration = db.EmployeeDetails_tbl.AsEnumerable().Where(x =>
                     x.MobileNo == "7458950577").Select(
                    x => new EmployeeDetails_tbl
                    {
                        Name = x.Name,

                    }).ToList();

                return registration;

            }
            catch (Exception ex)
            { }

            return new List<EmployeeDetails_tbl>();

        }
        public DataTable GetEmployeeReport()
        {
          
            SqlCommand com = new SqlCommand("select * from  EmployeeDetails_tbl  order by Name asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "EmployeeDetails_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDepartmentReport()
        {
          
            SqlCommand com = new SqlCommand("select * from  Department_Master_tbl  order by CompanyName asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Department_Master_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDesignationReport()
        {
            
            SqlCommand com = new SqlCommand("select * from  Designation_Master_tbl  order by CompanyName asc  ", con);
         SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Designation_Master_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetShiftReport()
        {
          
            SqlCommand com = new SqlCommand("select * from  Shift_Master_tbl  order by CompanyName asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            ds.TableName = "Shift_Master_tbl";
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDailyAtteData(string department)
        {

            SqlCommand com = new SqlCommand("SELECT * FROM DailyAttendence_tbl INNER JOIN EmployeeDetails_tbl ON DailyAttendence_tbl.EmployeeId=EmployeeDetails_tbl.Id where EmployeeDetails_tbl.Department='" + department + "';", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDailyFillterAtteData(string department,string dt1,string dt2)
        {
            SqlCommand com = new SqlCommand("SELECT * FROM DailyAttendence_tbl INNER JOIN EmployeeDetails_tbl ON DailyAttendence_tbl.EmployeeId=EmployeeDetails_tbl.Id where EmployeeDetails_tbl.Department='"+department+"' and DailyAttendence_tbl.Date between '"+dt1+"' and '"+dt2+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        public DataTable GetEmpData()
        {
          
            SqlCommand com = new SqlCommand("select * from  EmployeeDetails_tbl   order by Company_Name asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        public DataTable getlatlomg(string D1, string D2, string Name)
        {
            string cmd = "select * from EmployeeTrack_tbl where UserID in (select id from EmployeeDetails_tbl where Name='"+Name+"')";
            SqlCommand com =new SqlCommand(cmd,con);
            SqlDataAdapter sa=new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            return dt;
        }
        // 
        public DataTable GetDailyAtte(string department)
        {
            SqlCommand com = new SqlCommand("select * from  DailyAttendence_tbl where EmployeeId ='" + department + "' order by EmployeeId asc  ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        //public List<DailyAtt> GetEmployeeName(DailyAtt dtls)
        //{
        //    try
        //    {
        //        var registration = db.DailyAttendence_tbl.AsEnumerable().Where(p => p.Id == dtls.Id).Select(
        //            p => new DailyAtt
        //            {
        //                TimeIn = 
        //                TimeOut = p.TimeOut,
        //                Date = Convert.ToDateTime(p.Date),
        //                Id = p.Id,
        //            }).ToList();

        //        return registration;

        //    }
        //    catch (Exception ex)
        //    { }

        //    return new List<DailyAtt>();

        //}
       
    }
}
using employee1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace employee1.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult EmpList()
        {
            employeeEntities db = new employeeEntities();
            var listemployee = db.t_employee.ToList();
            return View(listemployee);
        }
        public ActionResult EmpCreate()
        {

            t_employee tc = new t_employee();
            return View(tc);
        }
        [HttpPost]
        public ActionResult EmpCreate(t_employee tp)
        {
            employeeEntities db = new employeeEntities();
            db.t_employee.Add(tp);
            db.SaveChanges();
            return RedirectToAction("EmpList");
        }

        [HttpPost]
        public ActionResult createStudent(t_employee tp)
        {
            Test(tp.emp_name,tp.emp_email,tp.emp_phone,tp.emp_designation,tp.emp_experience,tp.emp_address,"insert");
           
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult EmpEdit(int id)
        {
            employeeEntities db = new employeeEntities();
            var editemployee = db.t_employee.Where(x => x.id == id).FirstOrDefault();
            return View(editemployee);
        }
        [HttpPost]
        public ActionResult EmpEdit(t_employee tp)
        {
            employeeEntities db = new employeeEntities();
            var editemployee = db.t_employee.Where(x => x.id == tp.id).FirstOrDefault();
            editemployee.emp_name = tp.emp_name;
            editemployee.emp_email = tp.emp_email;
            editemployee.emp_phone = tp.emp_phone;
            editemployee.emp_designation = tp.emp_designation;
            editemployee.emp_experience = tp.emp_experience;
            editemployee.emp_address = tp.emp_address;
            db.SaveChanges();
            return RedirectToAction("EmpList");
        }
        public ActionResult EmpDelete(int id)
        {
            employeeEntities db = new employeeEntities();
            var deleteemployee = db.t_employee.Where(x => x.id == id).FirstOrDefault();
            return View(deleteemployee);
        }
        [HttpPost]
        public ActionResult EmpDelete(t_employee tp)
        {
            employeeEntities db = new employeeEntities();
            var deleteemployee = db.t_employee.Where(x => x.id == tp.id).FirstOrDefault();
            db.t_employee.Remove(deleteemployee);
            db.SaveChanges();
            return RedirectToAction("EmpList");
        }
        public ActionResult EmpDetails(int id)
        {
            Test("Abhishek", "abc@gmail.com", "8878787457", "Software Developer", "2 years", "noida","insert");
            employeeEntities db = new employeeEntities();
            var detailsemployee = db.t_employee.Where(x => x.id == id).FirstOrDefault();
            return View(detailsemployee);
        }
       

            SqlConnection sqlCon = null;
            String SqlconString = ConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString;
            public void Test(string name, string mail, string phn, string dsgn, string exp, string addrs,string statementtype)
            {
                using (sqlCon = new SqlConnection(SqlconString))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("insertupdatedelete", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@emp_name", SqlDbType.NVarChar).Value = name;
                    sql_cmnd.Parameters.AddWithValue("@emp_email", SqlDbType.NVarChar).Value = mail;
                    sql_cmnd.Parameters.AddWithValue("@emp_phone", SqlDbType.NVarChar).Value = phn;
                    sql_cmnd.Parameters.AddWithValue("@emp_designation", SqlDbType.NVarChar).Value = dsgn;
                    sql_cmnd.Parameters.AddWithValue("@emp_experience", SqlDbType.NVarChar).Value = exp;
                    sql_cmnd.Parameters.AddWithValue("@emp_address", SqlDbType.NVarChar).Value = addrs;
                    sql_cmnd.Parameters.AddWithValue("@StatementType", SqlDbType.NVarChar).Value = statementtype;

                    sql_cmnd.ExecuteNonQuery();
                    sqlCon.Close();
                }

            }
        public ActionResult form()
        {
            
            return View();
        }

        
    }
}
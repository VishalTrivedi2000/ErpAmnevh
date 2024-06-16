using AMNEVH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMNEVH.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee good
        BusinessLayer businessLayer;
        public EmployeeController()
        {
            businessLayer = new BusinessLayer();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult HomeCheck()
        {
            UserType userType = new UserType();
            List<SelectListItem> list = new List<SelectListItem>();
            if (Session["EMPID"] != null)
            {

                string str = "SELECT SUBSTRING(name, 0, CHARINDEX(' ', name)) as Names  FROM EmpInfo where EMPID='" + Session["EMPID"].ToString() + "'";
                string userName = Convert.ToString(businessLayer.getTableQ(str));
                str = "SELECT e.loginFor,e.loginForView,e.sessionName FROM EmpLoginFor e INNER JOIN EmpLoginInfo l ON e.loginForID=l.loginForID WHERE l.EMPID='" + Session["EMPID"].ToString() + "' AND l.loginForID NOT IN ('5')";


                string welcome = "";
                if (DateTime.Now.Hour < 12)
                {
                    welcome = "Good Morning : " + userName;
                }
                else if (DateTime.Now.Hour < 17)
                {
                    welcome = "Good Afternoon : " + userName;
                }
                else
                {
                    welcome = "Good Evening : " + userName;
                }
                userType.welcomeText = welcome;


                DataTable dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    list = new List<SelectListItem>();

                    if (Request.QueryString["CT"] == "CT")
                    {
                        list.Add(new SelectListItem() { Value = "0", Text = "Select" });
                        list.Add(new SelectListItem() { Value = "1", Text = "Teacher" });
                        list.Add(new SelectListItem() { Value = "2", Text = "Class Teacher" });
                        list.Add(new SelectListItem() { Value = "3", Text = "Employee" });
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            list.Add(new SelectListItem() { Value = "" + (4 + i), Text = "" + dt.Rows[i]["loginForView"].ToString().Trim() + "" });
                        }
                    }
                    else
                    {
                        list.Add(new SelectListItem() { Value = "0", Text = "Select" });
                        list.Add(new SelectListItem() { Value = "1", Text = "Employee" });

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            list.Add(new SelectListItem() { Value = "" + (2 + i), Text = "" + dt.Rows[i]["loginForView"].ToString().Trim() + "" });
                        }
                    }

                }
                else
                {
                    if (Request.QueryString["CT"] == "CT")
                    {
                        list.Add(new SelectListItem() { Value = "0", Text = "Select" });
                        list.Add(new SelectListItem() { Value = "1", Text = "Teacher" });
                        list.Add(new SelectListItem() { Value = "2", Text = "Class Teacher" });
                        list.Add(new SelectListItem() { Value = "3", Text = "Employee" });
                    }
                    else
                    {
                        list = new List<SelectListItem>();
                        list.Add(new SelectListItem() { Value = "0", Text = "Select" });
                        list.Add(new SelectListItem() { Value = "1", Text = "Employee" });

                    }
                }

            }
            ViewBag.ddlType = list;
            return View(userType);
        }
        [HttpPost]
        public ActionResult HomeCheck(UserType userType)
        {

            if (userType.ddlType == "Teacher")
            {
                string str = "select emp.Name,emp.EMPID,ts.CLT from EmpInfo emp inner join EmpLoginInfo elf on emp.EMPID=elf.EMPID inner join TeacherSubject ts on emp.EMPID=ts.EMPID where ts.EMPID='" + Session["EMPID"].ToString() + "' AND emp.StatUs='Active'  and ts.CLT='No' ";
               DataTable dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["CLT"].ToString() == "No")
                    {
                        //Session["userid"] = dt.Rows[0]["userid"].ToString();
                        Session["TCID"] = dt.Rows[0]["EMPID"].ToString();
                        Session["Name"] = dt.Rows[0]["Name"].ToString();
                        
                        return RedirectToAction("Home" ,"Teacher");
                    }
                    else
                    {
                        return Content(" <script> alert('Invalid Login Panel') </script>");
                    }
                }
                //----------------------------------
                str = "select emp.Name,emp.EMPID,ts.CLT from EmpInfo emp inner join EmpLoginInfo elf on emp.EMPID=elf.EMPID inner join TeacherSubject ts on emp.EMPID=ts.EMPID where ts.EMPID='" + Session["EMPID"].ToString() + "' AND emp.StatUs='Active'  and ts.CLT='Yes' ";
                 dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    //Session["userid"] = dt.Rows[0]["userid"].ToString();
                    Session["TCID"] = dt.Rows[0]["EMPID"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    return RedirectToAction("Home", "Teacher");

                }
                else
                {
                    return Content(" <script> alert('Invalid Login Panel') </script>");
                }
            }
            else if (userType.ddlType == "Class Teacher")
            {
                string str = "select emp.Name,emp.EMPID,ts.CLT from EmpInfo emp inner join EmpLoginInfo elf on emp.EMPID=elf.EMPID inner join TeacherSubject ts on emp.EMPID=ts.EMPID where ts.EMPID='" + Session["EMPID"].ToString() + "' AND emp.StatUs='Active' and ts.CLT='Yes' ";
               DataTable dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["CLT"].ToString() == "Yes")
                    {
                        //Session["userid"] = dt.Rows[0]["userid"].ToString();
                        Session["CTID"] = dt.Rows[0]["EMPID"].ToString();
                        Session["Name"] = dt.Rows[0]["Name"].ToString();
                        return RedirectToAction("Home", "ClassTeacher");
                    }
                    else
                    {
                        return Content(" <script> alert('Invalid Login Panel') </script>");
                    }
                }
                else
                {
                    return Content(" <script> alert('Invalid Login Panel') </script>");
                }
            }
            else if (userType.ddlType == "Employee")
            {
                string str = "select status1,status2,status3,status4,status5 from EmpInfo where EMPID='" + Session["EMPID"].ToString() + "'";
                DataTable dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "0" || Convert.ToInt32(dt.Rows[0][1].ToString()) < 5 || Convert.ToInt32(dt.Rows[0][2].ToString()) < 3 || dt.Rows[0][3].ToString() == "0" || dt.Rows[0][4].ToString() == "0")
                    {
                        return Redirect("EmpInfo?id=" + Session["EMPID"].ToString() + "");
                    }
                    else
                    {
                        return Redirect("Home?PR=H");
                    }
                }
            }
            else
            {
                string str = "SELECT e.loginFor,e.loginForView,e.sessionName FROM EmpLoginFor e where  e.loginForView='" + userType.ddlTypeID + "'";
                DataTable dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    Session["" + dt.Rows[0]["sessionName"].ToString() + ""] = Session["EMPID"].ToString();
                    str = "select Name from EmpInfo where EMPID='" + Session["EMPID"].ToString() + "'";
                    Session["Name"] = Convert.ToString(businessLayer.getTableQ(str));
                    return RedirectToAction("Home", dt.Rows[0]["loginFor"].ToString());
                }

            }
            return View(userType);
        }
        public ActionResult Home(string PR)
        {
            return View();
        }
        public ActionResult EmpInfo(string id)
        {







            return View();
        }
    }
}
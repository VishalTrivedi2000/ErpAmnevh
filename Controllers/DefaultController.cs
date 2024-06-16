using AMNEVH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMNEVH.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        BusinessLayer businessLayer;
        public DefaultController()
        {
            businessLayer=new BusinessLayer();
        }
        [HttpGet]
        public ActionResult Login() { return View(); }
        [HttpPost]
        public ActionResult Login(User user)
        {
            DataTable dt = businessLayer.getTable("vip__GetUserByUserNameAndPass", user.UserName, user.Password);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["StatUs"].ToString() == "Active")
                {
                    Session["userid"] = dt.Rows[0]["userid"].ToString();
                    Session["" + dt.Rows[0]["sessionName"].ToString() + ""] = dt.Rows[0]["EMPID"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();

                   return RedirectToAction("HomeCheck","" + dt.Rows[0]["loginFor"].ToString() );
                }
                else
                {
                    return Content("Invalid Login id or Password");
                }
            }
            else
            {
                string str = "select *  from adminlogin where User_name='" + user.UserName + "' and Password='" + user.Password + "'";
                dt = businessLayer.getTableQ(str);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["type"].ToString() == "admin")
                    {
                        Session["admin"] = dt.Rows[0]["UID"].ToString();
                        Response.Redirect("admin/Home");
                    }
                    else
                    {
                        Session["EMPID"] = dt.Rows[0]["UID"].ToString();
                        Session["name"] = dt.Rows[0]["User_name"].ToString();
                        Response.Redirect("guest/Home.aspx");
                    }
                }
                else
                {
                    return Content("Invalid Login id   or Password");
                }
            }
            return View();
        }

    }
}
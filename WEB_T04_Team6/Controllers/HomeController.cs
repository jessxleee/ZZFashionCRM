using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEB_T04_Team6.Models;
using WEB_T04_Team6.DAL;

namespace WEB_T04_Team6.Controllers
{
    public class HomeController : Controller
    {     
        StaffDAL staffcontext;
        MemberDAL membercontext;

        public HomeController()
        {
            staffcontext = new StaffDAL();
            membercontext = new MemberDAL();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult userLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            string username = formData["loginID"].ToString();
            string password = formData["userPassword"].ToString();

            Staff staff = null;
            Member member = null;

            if (username.Length > 1)
            {
                if (username.Substring(0,1).Equals("M") && Int32.TryParse(username.Substring(1,1), out int _))
                {
                    member = membercontext.memberLogin(username, password);                  
                }
                else
                {
                    staff = staffcontext.staffLogin(username, password);
                }
            }
            else
            {
                TempData["Message"] = "Invalid Login Credentials!";
                return RedirectToAction("Index");
            }

            if (member != null )
            {
                if (password == member.MPassword)
                {
                    HttpContext.Session.SetString("Username", member.MName);
                    HttpContext.Session.SetString("MemberID", member.MemberID);
                    HttpContext.Session.SetString("Password", password);
                    HttpContext.Session.SetString("Role", "Member");
                    return RedirectToAction("MemberMain");
                }
                else
                {
                    TempData["Message"] = "Invalid Login Credentials!";
                    return RedirectToAction("Index");
                }

            }
            else if (staff != null)
            {
                if (staff.SAppt == "Marketing Personnel")
                {
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetString("Role", "Marketing Personnel");
                    HttpContext.Session.SetString("StaffID", staff.StaffID);
                    return RedirectToAction("MarketingMain");
                }
                else if (staff.SAppt == "Sales Personnel")
                {
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetString("Role", "Sales Personnel");
                    return RedirectToAction("SalesMain");
                }
                else
                {
                    TempData["Message"] = "Invalid Login Credentials!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Invalid Login Credentials!";
                return RedirectToAction("Index");
            } 
        }
        public ActionResult SalesMain()
        {
            return View();
        }
        public ActionResult MarketingMain()
        {
            return View();
        }
        public ActionResult MemberMain()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult LogOut()
        {
            //Compute login duration
            DateTime startTime = Convert.ToDateTime(HttpContext.Session.GetString("LoggedInTime"));
            DateTime endTime = DateTime.Now;
            TimeSpan loginDuration = endTime - startTime;

            //Format display of login duration
            string strLoginDuration = "";
            if (loginDuration.Days > 0)
                strLoginDuration += loginDuration.Days.ToString() + " day(s) ";
            if (loginDuration.Hours > 0)
                strLoginDuration += loginDuration.Hours.ToString() + " hour(s) ";
            if (loginDuration.Minutes > 0)
                strLoginDuration += loginDuration.Minutes.ToString() + " minute(s) ";
            if (loginDuration.Seconds > 0)
                strLoginDuration += loginDuration.Seconds.ToString() + " seconds";

            TempData["LoginDuration"] = "You have logged in for " + strLoginDuration;

            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }
    
    }
}

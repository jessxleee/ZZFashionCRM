using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_T04_Team6.DAL;
using WEB_T04_Team6.Models;

namespace WEB_T04_Team6.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackDAL staffContext = new FeedbackDAL();
        private FeedbackDAL memberContext = new FeedbackDAL();

        // GET: FeedbackController
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Feedback> feedbackList = staffContext.GetAllFeedback();
            return View(feedbackList);
        }

        public ActionResult FeedbackMember()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string memberID = HttpContext.Session.GetString("MemberID");
            List<Feedback> feedbackList = memberContext.GetMemberFeedback(memberID);
            return View(feedbackList);

        }

        // GET: FeedbackController/Details/5
        public ActionResult Details()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string memberID = HttpContext.Session.GetString("MemberID");
            List<Feedback> feedbackList = memberContext.GetMemberFeedback(memberID);
            return View(feedbackList);
        }

        // GET: FeedbackController/Create
        public ActionResult CreateMember()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: FeedbackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            
            string memberID = HttpContext.Session.GetString("MemberID");
            if (ModelState.IsValid)
            {
                //Add staff record to database

                feedback.MemberID = memberID;
                feedback.FeedbackID = memberContext.Add(feedback);


                //Redirect user to Staff/Index view
                return RedirectToAction("FeedbackMember","Feedback");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                TempData["Message"] = "Create feedback was not successful! Please try again!";
                return View(feedback);
            }
        }

        // GET: FeedbackController/Edit/5
        public ActionResult Edit(string? feedbackid)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (feedbackid == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            Feedback feedback = memberContext.GetDetails(feedbackid);
            if (feedback == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // POST: FeedbackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                //Update staff record to database
                staffContext.Update(feedback);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(feedback);
            }
        }

        public ActionResult Delete(string? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("FeedbackMember", "Feedback");
            }
            if (id == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Feedback feedback = memberContext.GetDetails(id);
            if(feedback == null)
            {
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // POST: FeedbackController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Feedback feedback)
        {

            string memberID = HttpContext.Session.GetString("MemberID");
            feedback.MemberID = memberID;
            //Delete the staff record from database

            memberContext.Delete(feedback.FeedbackID);
            return RedirectToAction("FeedbackMember", "Feedback");
        }
    }
}

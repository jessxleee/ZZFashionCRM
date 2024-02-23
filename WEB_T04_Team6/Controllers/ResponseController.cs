using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_T04_Team6.DAL;
using WEB_T04_Team6.Models;

namespace WEB_T04_Team6.Controllers
{
    public class ResponseController : Controller
    {
        private ResponseDAL responseContext = new ResponseDAL();
        private FeedbackDAL feedbackContext = new FeedbackDAL();

        // GET: ResponseController
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Response> responseList = responseContext.GetAllResponse();
            return View(responseList);
        }

        // GET: ResponseController/Details/5
        public ActionResult Details(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Response> responseList = responseContext.GetAllResponse();
            return View(responseList);
        }

        // GET: ResponseController/Create
        public ActionResult Create()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
               (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if ((HttpContext.Session.GetString("Role") == "Marketing Personnel"))
            {
                List<Response> responsesList = responseContext.GetAllResponse();
                return View(responsesList);
            }
            return View();
        }

        // POST: ResponseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection formdata)
        {
            if (ModelState.IsValid)
            {
                if (formdata["responseText"].ToString() != "")
                {
                    Response response = new Response
                    {
                        FeedbackID = Convert.ToInt32(formdata["feedbackid"].ToString()),
                        StaffID = formdata["staffid"].ToString(),
                        Text = formdata["responseText"].ToString()
                    };
                    responseContext.PostResponse(response);
                    return RedirectToAction("MarketingMain", "Home");
                }
                else
                {
                    TempData["Alert"] = "Invalid Post!";
                    return RedirectToAction("Index", "Feedback");
                }
            }
            else
            {
                return View();
                //return RedirectToAction("MarketingMain", "Home");
            }
        }

        //29/7/22 01:27
        // GET: ResponseController/View/5
        public ActionResult View(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Marketing" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }


            Response response = responseContext.GetDetails(id);
            ResponseViewModel responseVM = MapToResponseVM(response);
            return View(responseVM);
        }



        //29/7/22 02:00
        public ResponseViewModel MapToResponseVM(Response response)
        {
            string feedbacktitle = "";
            string feedbacktext = "";
            if (response.FeedbackID != 0)
            {
                List<Feedback> feedbackList = feedbackContext.GetAllFeedback();
                foreach (Feedback feedback in feedbackList)
                {
                    if (response.FeedbackID == feedback.FeedbackID)
                    {
                        feedbacktitle = feedback.Title;
                        feedbacktext = feedback.Text;
                        break;
                    }
                }
            }
            ResponseViewModel responseVM = new ResponseViewModel
            {
                ResponseID = response.ResponseID,
                FeedbackID = response.FeedbackID,
                MemberID = response.MemberID,
                StaffID = response.StaffID,
                DateTimePosted = response.DateTimePosted,
                FeedbackTitle = feedbacktitle,
                FeedbackText = feedbacktext,
                Text = response.Text

            };

            return responseVM;
        }

    }
}

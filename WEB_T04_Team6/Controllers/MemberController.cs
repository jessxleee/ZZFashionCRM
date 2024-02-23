using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_T04_Team6.Models;
using WEB_T04_Team6.DAL;



namespace WEB_T04_Team6.Controllers
{
    public class MemberController : Controller
    {
        private MemberDAL memberContext = new MemberDAL();
        // GET: Member
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
           (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Member> memberList = memberContext.GetAllMember();
            return View(memberList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(IFormCollection formData)
        {
            List<string> memberidList = memberContext.GetMemberID();
            string memberid = formData["MemberID"].ToString();
            if (memberidList.Contains(memberid))
            {
                Member member = memberContext.GetSelectedMember(memberid);
                return RedirectToAction("MemberDetails", "Member", new { member.MemberID });
            }
            else
            {
                TempData["IdNotFound"] = "MemberID not found!";
                return RedirectToAction("Index");
            }

        }
        
        public ActionResult MemberDetails(string memberid)
        {
            Member member = memberContext.GetSelectedMember(memberid);
            return View(member);
        }
        
        // GET: Member/Create
        public ActionResult Create()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CountryList"] = GetCountries();
            return View();
        }

        private List<SelectListItem> GetCountries()
        {
            List<string> countryList = new List<string>();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo country = new RegionInfo(culture.LCID);
                if (!countryList.Contains(country.DisplayName.ToString()))
                {
                    countryList.Add(country.DisplayName.ToString());
                }
            }
            countryList.Sort();

            List<SelectListItem> countries = new List<SelectListItem>();
            foreach (string country in countryList)
            {
                countries.Add(new SelectListItem
                {
                    Value = country,
                    Text = country
                });
            }
            return countries;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            ViewData["CountryList"] = GetCountries();

            if (ModelState.IsValid)
            {
                memberContext.Add(member);
                return RedirectToAction("SalesMain", "Home");
            }
            else
            {
                TempData["CreationUnsuccessful"] = "Input is invalid. Try again.";
                return View(member);

            }
        }
        public ActionResult Details(string id)
        {
            {
                if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Sales Personnel"))
                {
                    return RedirectToAction("Index", "Home");
                }

                Member member = memberContext.GetDetails(id);
                MemberViewModel memberVM = MapToMemberVM(member);
                return View(memberVM);
            }
        }

        public MemberViewModel MapToMemberVM(Member member)
        {

            MemberViewModel memberVM = new MemberViewModel
            {
                MemberID = member.MemberID,
                Name = member.MName,
                Gender = member.MGender,
                BirthDate = member.MBirthDate,
                Address = member.MAddress,
                Country = member.MCountry,
                TelNo = member.MTelNo,
                EmailAddr = member.MEmailAddr

            };

            return memberVM;
        }

        public ActionResult UpdateInformation()
        {

            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string memberID = HttpContext.Session.GetString("MemberID");

            Member member = memberContext.GetDetails(memberID);
            return View(member);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInformation(Member member)
        {

            if (ModelState.IsValid)
            {
                memberContext.UpdateMember(member);
                TempData["Message"] = "Update was successful!";
                return RedirectToAction("UpdateInformation", "Member");
            }
            else
            {

                TempData["Message"] = "Update was not successful! Please try again!";
                //return RedirectToAction("UpdateInformation", "Member");
                return View(member);
            }
        }
    }
}


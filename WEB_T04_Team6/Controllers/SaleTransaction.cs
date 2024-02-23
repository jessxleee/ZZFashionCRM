using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_T04_Team6.DAL;
using WEB_T04_Team6.Models;

namespace WEB_T04_Team6.Controllers
{
    public class SaleTransactionController : Controller
    {
        private SaleTransactionDAL salesTransactionContext = new SaleTransactionDAL();

        // GET: SalesTransaction
        public ActionResult Index()
        {
            //Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<SaleTransaction> transactionList = salesTransactionContext.GetAllSalesTransaction();
            return View(transactionList);
        }

        // GET: SalesTransaction/Edit/5
        public IActionResult Issues(string id)
        {
            //ViewData["MemberID"] = id;
            if (id.Equals(string.Empty))
            {
                TempData["Alert"] = "Invalid Issues! No Member ID available";
                return RedirectToAction("Index", "SaleTransaction");
            }
            else
            {
                if (!salesTransactionContext.ValidateRepeat(id))
                {
                    SaleTransaction transactions = salesTransactionContext.GetSpecificTransaction(id);
                    salesTransactionContext.GetVoucher(transactions);
                    return RedirectToAction("Index", "SaleTransaction");
                }
                else
                {
                    TempData["InvaildMessage"] = "Invalid Issues!";
                    return RedirectToAction("Index", "SaleTransaction");
                }
            }

        }

        public ActionResult RepeatedVoucher()
        {
            return View();
        }

        //// POST: SalesTransaction/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Issues(string id)
        //{
        //    try
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: SalesTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesTransaction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
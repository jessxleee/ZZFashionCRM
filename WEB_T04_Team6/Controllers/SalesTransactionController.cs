﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_T04_Team6.DAL;
using WEB_T04_Team6.Models;

namespace WEB_T04_Team6.Controllers
{
    public class SalesTransactionController : Controller
    {
        private SalesTransactionDAL salesTransactionContext = new SalesTransactionDAL();

        // GET: SalesTransaction
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<SaleTransaction> transactionList = salesTransactionContext.GetAllSalesTransaction();
            return View(transactionList);
        }

        // GET: SalesTransaction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesTransaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesTransaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: SalesTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesTransaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

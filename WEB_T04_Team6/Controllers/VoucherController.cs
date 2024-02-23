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
    public class VoucherController : Controller
    {

        private VoucherDAL voucherContext = new VoucherDAL();

        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("SalesMain", "Home");
            }
            List<Voucher> voucherList = voucherContext.GetCashVoucher();
            return View(voucherList);

        }

        public ActionResult Collect(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            Voucher voucher = voucherContext.GetDetails(id);
            VoucherViewModel voucherVM = MapToVoucherVM(voucher);
            ViewData["VoucherStatusList"] = VoucherCollect();
            return View(voucher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Collect(IFormCollection formData, Voucher voucher)
        {
            ViewData["VoucherStatusList"] = VoucherCollect();
            string vouchersn = formData["VoucherSN"].ToString();
            if (vouchersn != "")
            {
                voucherContext.Collect(voucher);
                return RedirectToAction("SalesMain", "Home");
            }
            else
            {
                TempData["UpdateUnsuccessful"] = "Update unsuccessful. Please try again.";
                return View(voucher);
            }
        }
        private List<SelectListItem> VoucherCollect()
        {
            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem
            {
                Value = "0",
                Text = "0 - Issued"
            });
            status.Add(new SelectListItem
            {
                Value = "1",
                Text = "1 - Collected"
            });


            return status;
        }
        public ActionResult Redeem()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("SalesMain", "Home");
            }
            ViewData["VoucherStatusList"] = VoucherRedeem();

            return View();
        }
        private List<SelectListItem> VoucherRedeem()
        {
            List<SelectListItem> status = new List<SelectListItem>();
            status.Add(new SelectListItem
            {
                Value = "1",
                Text = "1 - Collected"
            });
            status.Add(new SelectListItem
            {
                Value = "2",
                Text = "2 - Redeemed"
            });
          

            return status;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Redeem(IFormCollection formData, Voucher voucher)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            string vouchersn = formData["VoucherSN"].ToString();
            if (vouchersn != "" || vouchersn != null)
            {
                if (voucherContext.Redeemable(vouchersn) == true)
                {
                    voucher = voucherContext.GetSelectedCashVoucher(vouchersn);
                    voucherContext.Redeem(voucher);
                    return RedirectToAction("Salesmain", "Home");
                }
                else
                {
                    TempData["UnableToIssued"] = "Cash Voucher has been issued more than a year";
                    return View();
                }
            }
            else
            {
                TempData["RedemptionUnccessful"] = "Unable to redeem cash voucher, try again.";
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Sales Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            Voucher voucher = voucherContext.GetDetails(id);
            VoucherViewModel voucherVM = MapToVoucherVM(voucher);
            return View(voucherVM);
        }
        public VoucherViewModel MapToVoucherVM(Voucher voucher)
        {
            VoucherViewModel voucherVM = new VoucherViewModel
            {
                IssuingID = voucher.IssuingID,
                MemberID = voucher.MemberID,
                Amount = voucher.Amount,
                MonthIssuedFor = voucher.MonthIssuedFor,
                YearIssuedFor = voucher.YearIssuedFor,
                DateTimeIssued = voucher.DateTimeIssued,
                VoucherSN = voucher.VoucherSN,
                Status = voucher.Status,
                DateTimeRedeemed = voucher.DateTimeRedeemed,
            };

            return voucherVM;
        }

        public ActionResult DisplayIssuedVoucher()
        {
           if ((HttpContext.Session.GetString("Role") == null) ||
           (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Voucher> voucherList = voucherContext.GetCashVoucher();
            return View(voucherList);
        }

        public ActionResult MemberVoucher()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            //List<Voucher> voucherList = voucherContext.GetMemberDetails(id.);
            return View();

        }
    }
}

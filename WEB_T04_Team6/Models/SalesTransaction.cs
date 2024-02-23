using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class SalesTransaction
    {
        [Display(Name = "Transaction ID")]
        public int TransactionID { get; set; }

        [Display(Name = "Store ID")]
        public string StoreID { get; set; }

        [Display(Name = "Member ID")]
        public string? MemberID { get; set; }

        [Display(Name = "Sub Total")]
        public double SubTotal { get; set; }

        public float Tax { get; set; }

        [Display(Name = "Discount Percent")]
        public double DiscountPercent { get; set; }

        [Display(Name = "Discount Amount")]
        public double DiscountAmt { get; set; }

        public double Total { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "“{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}

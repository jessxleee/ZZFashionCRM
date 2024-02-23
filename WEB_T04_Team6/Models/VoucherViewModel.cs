using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class VoucherViewModel
    {
        [Display(Name = "Issuing ID")]
        public int IssuingID { get; set; }
        [Display(Name = "Member ID")]
        public string MemberID { get; set; }
        public decimal Amount { get; set; }
        public int MonthIssuedFor { get; set; }
        public int YearIssuedFor { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]

        public DateTime DateTimeIssued { get; set; }

        //[RegularExpression(@"^\d{4}-\d{3}$-\d{6}$", ErrorMessage = "Invalid Vucher Serial No")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? VoucherSN { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [Display(Name = "Voucher Status")]
        public string Status { get; set; }

        [Display(Name = "Date time Redeemed")]
        [DataType(DataType.Date)]
        public DateTime? DateTimeRedeemed { get; set; }
    }
}

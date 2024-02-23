using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WEB_T04_Team6.Models
{
    public class Voucher
    {
        [Required]
        [Display(Name = "Issuing ID")]
        public int IssuingID { get; set; }
        [Required]
        [Display(Name = "Member ID")]
        public string MemberID { get; set; }
        public decimal Amount { get; set; }
        public int MonthIssuedFor { get; set; }
        public int YearIssuedFor { get; set; }

        [Display(Name = "Date of Issue")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "“{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeIssued { get; set; }

        //[RegularExpression(@"^\d{4}-\d{3}$-\d{6}$", ErrorMessage = "Invalid Vucher Serial No")]
        public string? VoucherSN { get; set; }

        [Display(Name = "Voucher Status")]
        [StringLength(1)]
        public string Status { get; set; }

        [Display(Name = "Date time Redeemed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "“{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateTimeRedeemed { get; set; }
    }
}

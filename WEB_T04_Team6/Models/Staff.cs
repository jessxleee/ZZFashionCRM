using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "You must enter an ID")]
        [Display(Name ="Staff ID")]
        public string StaffID { get; set; }

        [Display(Name = "Store ID")]
        public string? StoreID { get; set; }

        [Required(ErrorMessage = "Please enter staff name")]
        [Display(Name = "Staff Name")]
        public string SName { get; set; }

        [Required(ErrorMessage = "Please select a gender")]
        public string SGender { get; set; }

        [Required(ErrorMessage = "Please enter staff appointment")]
        public string SAppt { get; set; }

        [RegularExpression(@"[689]\d{7}|\+65[689]\d{7}$", ErrorMessage = "Invalid Singapore Phone Number")]
        public string STelNo { get; set; }

        [Display(Name = "Staff Email Address")]
        [EmailAddress]
        //[RegularExpression(@"[A-Za-z09._%+-]+@[A-Za-z0-9.-]\.[A-Za-z]{2,4}")]
        public string SEmailAddr { get; set; }

        [Required(ErrorMessage = "Please enter passsword")]
        public string SPassword { get; set; }
    }
}
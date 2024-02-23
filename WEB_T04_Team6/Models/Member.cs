using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class Member
    {
        [Display(Name = "MemberID")]
        [Required(ErrorMessage = "Enter a valid ID")]
        [StringLength(9)]
        public string MemberID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter a Name")]
        [StringLength(255)]
        public string MName { get; set; }

        [Display(Name = "Gender")]
        public char MGender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime MBirthDate { get; set; }

        [Display(Name = "Address")]
        [StringLength(250)]
        public string? MAddress { get; set; }

        [Display(Name = "Country")]
        public string MCountry { get; set; }

        [Display(Name = "Telephone Number")]
        //[Required(ErrorMessage = "Please type number")]
        [ValidateTelNoExists]
        public string? MTelNo { get; set; }

        [Display(Name = "Email Address")]
        //[EmailAddress(ErrorMessage ="Please enter a valid email address.")]
        [ValidateEmailExists]
        public string? MEmailAddr { get; set; }

        public string MPassword { get; set; }

    }
}

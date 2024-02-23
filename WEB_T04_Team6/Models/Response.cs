using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class Response
    {
        [Display(Name = "Response ID")]
        public int ResponseID { get; set; }

        [Display(Name = "Feedback ID")]
        public int FeedbackID { get; set; }

        [Display(Name = "Member ID")]
        public string? MemberID { get; set; }

        [Display(Name = "Staff")]
        public string? StaffID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateTimePosted { get; set; }

        [Required(ErrorMessage = "Invalid Submit! Please enter your response")]
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_T04_Team6.Models
{
    public class MemberViewModel
    {
        [Display(Name = "MemberID")]
        public string MemberID { get; set; }

        public string Name { get; set; }
        public char Gender { get; set; }

        
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        
        public string? Address { get; set; }

        
        public string Country { get; set; }

        
        public string? TelNo { get; set; }
        
        [Display(Name = "Email Address")]

        public string? EmailAddr { get; set; }


    }
}

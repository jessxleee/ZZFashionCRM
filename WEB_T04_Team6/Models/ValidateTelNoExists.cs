using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WEB_T04_Team6.DAL;

namespace WEB_T04_Team6.Models
{
    public class ValidateTelNoExists : ValidationAttribute
    {
        private MemberDAL memberContext = new MemberDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string telNo = Convert.ToString(value);

            // Casting the validation context to the "Staff" model class
            Member member = (Member)validationContext.ObjectInstance;

            // Get the Staff Id from the staff instance
            string memberId = member.MemberID;
            if (memberContext.IsTelNoExist(telNo, memberId))
                return new ValidationResult
                ("Number already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
    }
}

using BO.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace BO.DTO
{
    // This class is a simple POCO to wrap the input from the client with data used to later calculate the various taxes and net income
    public class TaxPayer : BaseTax   
    {

        [Required(ErrorMessage = ValidationMessages.FULL_NAME_REQUIRED)]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = ValidationMessages.FULL_NAME_CHARS_ONLY)]
        public string FullName { get; set; }
             
        
        [Required(ErrorMessage = ValidationMessages.SSN_REQUIRED)]
        [StringLength(10, ErrorMessage = ValidationMessages.SSN_LENGTH, MinimumLength = 5)]
        public string SSN { get; set; }

        public DateTime DateOfBirth { get; set; }


        public string GetCustomHash()
        {
            return $"{GrossIncome.ToString()}:{CharitySpent.ToString()}";
        }

    }
}

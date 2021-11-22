using BO.Constants;
using System.ComponentModel.DataAnnotations;

namespace BO.DTO
{
    public class BaseTax
    {
        [Required(ErrorMessage = ValidationMessages.GROSS_INCOME_REQUIRED)]
        public decimal? GrossIncome { get; set; }

        public decimal? CharitySpent { get; set; }
    }
}

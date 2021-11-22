namespace BO.DTO
{
    public class TaxConfiguration
    {
        public decimal NonTaxableIncome { get; set;  }
        public decimal TopSocialTaxIncome { get; set; } 

        public decimal TopCharityRate { get; set; }

        public decimal TaxRate { get; set; }

        public decimal SocialTaxRate { get; set; }
    }
}

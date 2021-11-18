namespace BO.DTO
{
    // This class is plain POCO to return data to the client with calculated IncomeTax, SocialTax, TotalTax and NetIncome based on input
    // Input is GrossIncome and CharitySpent
    public class Taxes
    {
        public Taxes(TaxPayer taxPayer)
        {
            GrossIncome = taxPayer.GrossIncome;
            NetIncome = taxPayer.GrossIncome;
            CharitySpent = taxPayer.CharitySpent;
            IncomeTax = 0;
            SocialTax = 0;
            TotalTax = 0;
        }

        public decimal GrossIncome { get; set; }

        public decimal CharitySpent{ get; set; }

        public decimal NetIncome { get; set; }

        public decimal IncomeTax { get; set; }

        public decimal SocialTax { get; set; }

        public decimal TotalTax { get; set; }
    }
}

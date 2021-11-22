using BO.DTO;

namespace BO
{
    // The class that takes care of the actual tax calculations - business logic
    public class TaxCalculator
    {       
        public TaxCalculator(TaxPayer taxPayer, TaxConfiguration taxConfig) 
        {
            TaxPayer = taxPayer;
            TaxConfig = taxConfig;
        }

        /// <summary>
        /// Method to calculate the taxes required based on gross income and charity spend
        /// </summary>
        /// <returns>Taxes object with calculated taxes</returns>
        public Taxes CalculateTax()
        {
            Taxes taxes = new Taxes(TaxPayer);

            // In case GrossIncome is less or equal to non-taxable income, exit with the defaults (net income equal to gross, no taxes
            if (taxes.GrossIncome <= TaxConfig.NonTaxableIncome)
            {
                return taxes;
            }

            // Charity maxes up at TOP_CHARITY_RATE (currently 10%) - so cap that at 10% max
            decimal? actualCharitySpent = taxes.CharitySpent > taxes.GrossIncome * TaxConfig.TopCharityRate ?
                                    taxes.GrossIncome * TaxConfig.TopCharityRate :
                                    taxes.CharitySpent;

            decimal? actualGross = taxes.GrossIncome - actualCharitySpent; // we need to work with gross without charities
            decimal? taxableDelta = actualGross - TaxConfig.NonTaxableIncome; // taxable income is calculated by substracting NON_TAXABLE_INCOME (1000) from GrossIncome
            decimal? effectiveTax = (taxableDelta * TaxConfig.TaxRate);
            
            // Cap social tax at TOP_SOCIAL_TAX_INCOME (3000)
            decimal? socialTax = taxableDelta <= (TaxConfig.TopSocialTaxIncome - TaxConfig.NonTaxableIncome) ?
                                    taxableDelta * TaxConfig.SocialTaxRate :
                                    (TaxConfig.TopSocialTaxIncome - TaxConfig.NonTaxableIncome) * TaxConfig.SocialTaxRate;
          
            taxes.NetIncome = taxes.GrossIncome - effectiveTax - socialTax;
            taxes.IncomeTax = effectiveTax;
            taxes.SocialTax = socialTax;
            taxes.TotalTax = taxes.IncomeTax + taxes.SocialTax;

            return taxes;
        }

        private TaxPayer TaxPayer { get; set; } = new TaxPayer();
        private TaxConfiguration TaxConfig { get; set; } = new TaxConfiguration();
    }
}

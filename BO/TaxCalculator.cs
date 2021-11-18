using BO.DTO;
using System.Text.RegularExpressions;

namespace BO
{
    // The class that takes care of the actual tax calculations - business logic
    public class TaxCalculator
    {
        // All values are constants and *not* hard coded, this way we can easily change the logic if
        // different values are required by changing the constants here
        // In a real-world scenario this would be a configuration file
        private const decimal NON_TAXABLE_INCOME = 1000;        
        private const decimal TOP_SOCIAL_TAX_INCOME = 3000;
        private const decimal TOP_CHARITY_RATE = 0.10M;
        private const decimal TAX_RATE = 0.10M;
        private const decimal SOCIAL_TAX_RATE = 0.15M;
        public TaxCalculator(TaxPayer taxPayer) 
        {
            TaxPayer = taxPayer;
        }

        /// <summary>
        /// Method to calculate the taxes required based on gross income and charity spend
        /// </summary>
        /// <returns>Taxes object with calculated taxes</returns>
        public Taxes CalculateTax()
        {
            Taxes taxes = new Taxes(TaxPayer);

            // In case GrossIncome is less or equal to non-taxable income, exit with the defaults (net income equal to gross, no taxes
            if (taxes.GrossIncome <= NON_TAXABLE_INCOME)
            {
                return taxes;
            }

            // Charity maxes up at TOP_CHARITY_RATE (currently 10%) - so cap that at 10% max
            decimal actualCharitySpent = taxes.CharitySpent > taxes.GrossIncome * TOP_CHARITY_RATE ?
                                    taxes.GrossIncome * TOP_CHARITY_RATE :
                                    taxes.CharitySpent;

            decimal actualGross = taxes.GrossIncome - actualCharitySpent; // we need to work with gross without charities
            decimal taxableDelta = actualGross - NON_TAXABLE_INCOME; // taxable income is calculated by substracting NON_TAXABLE_INCOME (1000) from GrossIncome
            decimal effectiveTax = (taxableDelta * TAX_RATE);
            
            // Cap social tax at TOP_SOCIAL_TAX_INCOME (3000)
            decimal socialTax = taxableDelta <= (TOP_SOCIAL_TAX_INCOME - NON_TAXABLE_INCOME) ?
                                    taxableDelta * SOCIAL_TAX_RATE :
                                    (TOP_SOCIAL_TAX_INCOME - NON_TAXABLE_INCOME) * SOCIAL_TAX_RATE;
          
            taxes.NetIncome = taxes.GrossIncome - effectiveTax - socialTax;
            taxes.IncomeTax = effectiveTax;
            taxes.SocialTax = socialTax;
            taxes.TotalTax = taxes.IncomeTax + taxes.SocialTax;

            return taxes;
        }

        /// <summary>
        /// Method to validate the input
        /// </summary>
        /// <returns>A string with validation errors (cummulative, all errors combined) or empty string if validation passes correctly</returns>
        public string ValidateTaxes()
        {
            string errorMessage = string.Empty;

            string fullName = TaxPayer.FullName.Trim();

            // FullName - Letters and spaces only
            Match fullNameMatch = Regex.Match(fullName, "^[A-Za-z ]+$", RegexOptions.IgnoreCase);
            if (!fullNameMatch.Success)
                errorMessage += "FullName does not validate. Need letters and spaces only. \n";
            if (!fullName.Contains(' ')) // make sure we have at least one space after the regex validation to make sure at least two words in FullName
                errorMessage += "FullName does not validate. At least two words (first and last name). \n";

           
            // SSN should be numbers only, 5-10 numbers length, e.g. '123456"
            Match ssnMatch = Regex.Match(TaxPayer.SSN, "^[0-9]{5,10}$", RegexOptions.IgnoreCase);
            if (!ssnMatch.Success)
                errorMessage += "SSN does not validate. Number only, 5-10 numbers length. \n";

            if (TaxPayer.GrossIncome < 0)
                errorMessage += "GrossIncome does not validate. Should be bigger than 0. \n";

            if (TaxPayer.CharitySpent < 0 || TaxPayer.CharitySpent > TaxPayer.GrossIncome)
                errorMessage += "CharitySpent does not validate. Should be bigger than 0 and less than GrossIncome. \n";


            return errorMessage;
        }

        private TaxPayer TaxPayer { get; set; } = new TaxPayer();
    }
}

using System;

namespace BO.DTO
{
    // This class is a simple POCO to wrap the input from the client with data used to later calculate the various taxes and net income
    public class TaxPayer
    {

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal GrossIncome { get; set; }

        public string SSN { get; set; }

        public decimal CharitySpent { get; set; }

    }
}

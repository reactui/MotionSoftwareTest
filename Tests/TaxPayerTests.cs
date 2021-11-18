using BO;
using BO.DTO;
using NUnit.Framework;
using System;

namespace Tests
{    
    public class TaxPayerTests
    {
        // Setup gets called once per initialization of test cases, setup data here
        [SetUp]
        public void Setup()
        {
            SetUpTaxPayer980(); // setup a test taxpayer with income 980
            SetUpTaxPayer3400(); // setup a test taxpayer with income 3400
        }


        // Test user with 980 income is setup correctly, there should be no validation message
        [Test]
        public void Test_TaxCalc980_Validation()
        {
            string validationMessage = TaxCalc980.ValidateTaxes();
            Assert.IsTrue(String.IsNullOrEmpty(validationMessage), $"Validation for TaxPayer980 did not pass with message {validationMessage}");
        }

        // Test expected net income for gross income 980
        [Test]
        public void Test_TaxCalc980_NetIncome()
        {
            Taxes taxes = TaxCalc980.CalculateTax();
            Assert.AreEqual(Taxes980.NetIncome, 980, $"NetIcome is not the expected value of 980, instead it is {Taxes980.NetIncome}");
        }

        // Test user with 3400 income is setup correctly, there should be no validation message
        [Test]
        public void Test_TaxCalc3400_Validation()
        {

            string validationMessage = TaxCalc3400.ValidateTaxes();
            Assert.IsTrue(String.IsNullOrEmpty(validationMessage), $"Validation for TaxPayer3400 did not pass with message {validationMessage}");
        }

        // Test  net income for gross income 3400
        [Test]
        public void Test_TaxCalc3400_NetIncome()
        {            
            Assert.AreEqual(Taxes3400.NetIncome, 2860, $"NetIcome is not the expected value of 2860, instead it is {Taxes3400.NetIncome}");
        }

        // Test social tax for gross income 3400
        [Test]
        public void Test_TaxCalc3400_SocialTax()
        {            
            Assert.AreEqual(Taxes3400.SocialTax, 300, $"SocialTax is not the expected value of 300, instead it is {Taxes3400.SocialTax}");
        }
        
        // Text income tax for gross income 3400
        [Test]
        public void Test_TaxCalc3400_IncomeTax()
        {            
            Assert.AreEqual(Taxes3400.IncomeTax, 240, $"IncomeTax is not the expected value of 2860, instead it is {Taxes3400.IncomeTax}");
        }


        // Private methods to setup sample taxpayers
        private void SetUpTaxPayer980()
        {
            TaxPayer980 = new TaxPayer()
            {
                GrossIncome = 980,
                DateOfBirth = new DateTime(1990, 11, 1),
                FullName = "John Smith",
                CharitySpent = 0,
                SSN = "12345678"
            };
            TaxCalc980 = new TaxCalculator(TaxPayer980);
            Taxes980= TaxCalc980.CalculateTax();
        }

        private void SetUpTaxPayer3400()
        {
            TaxPayer3400 = new TaxPayer()
            {
                GrossIncome = 3400,
                DateOfBirth = new DateTime(1980, 11, 1),
                FullName = "Joe Doe",
                CharitySpent = 0,
                SSN = "1345678"
            };
            TaxCalc3400 = new TaxCalculator(TaxPayer3400);
            Taxes3400 = TaxCalc3400.CalculateTax();
        }

        TaxPayer TaxPayer980 { get; set; } 
        TaxCalculator TaxCalc980 { get; set; }
        Taxes Taxes980 { get; set; }

        TaxPayer TaxPayer3400 { get; set; }
        TaxCalculator TaxCalc3400 { get; set; }
        Taxes Taxes3400 { get; set; }
    }
}
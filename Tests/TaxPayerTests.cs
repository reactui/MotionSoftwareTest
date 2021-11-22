using BO;
using BO.DTO;
using NUnit.Framework;
using System;

namespace Tests
{
    public class TaxPayerTests
    {
        TaxConfiguration _taxConfig;

        // Setup gets called once per initialization of test cases, setup data here
        [SetUp]
        public void Setup()
        {
            _taxConfig = new TaxConfiguration
            {
                NonTaxableIncome = 1000,
                TopSocialTaxIncome = 3000,
                SocialTaxRate = 0.15M,
                TaxRate = 0.10M,
                TopCharityRate = 0.10M                
            };            


            SetUpTaxPayer980(); // setup a test taxpayer with income 980, Charity 0
            SetUpTaxPayer3400(); // setup a test taxpayer with income 3400, Charity 0 
            SetUpTaxPayer2500(); // setup a test taxpayer with income 2500, Charity 150
            SetUpTaxPayer3600(); // setup a test taxpayer with income 3600, Charity 520
        }

       
        // Test expected net income for gross income 980
        [Test]
        public void Test_TaxCalc980_NetIncome()
        {            
            Assert.AreEqual(Taxes980.NetIncome, 980, $"NetIcome is not the expected value of 980, instead it is {Taxes980.NetIncome}");
        }

        // Test expected net income for gross income 980
        [Test]
        public void Test_TaxCalc980_SocialTax()
        {            
            Assert.AreEqual(Taxes980.SocialTax, 0, $"Social Taxes is not the expected value of 0, instead it is {Taxes980.SocialTax}");
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

        // Test  net income for gross income 2500
        [Test]
        public void Test_TaxCalc2500_NetIncome()
        {
            Assert.AreEqual(Taxes2500.NetIncome, 2162.5, $"NetIcome is not the expected value of 2162.5, instead it is {Taxes2500.NetIncome}");
        }

        // Test social tax for gross income 2500
        [Test]
        public void Test_TaxCalc2500_SocialTax()
        {
            Assert.AreEqual(Taxes2500.SocialTax, 202.5, $"SocialTax is not the expected value of 202.5, instead it is {Taxes2500.SocialTax}");
        }

        // Text income tax for gross income 2500
        [Test]
        public void Test_TaxCalc2500_IncomeTax()
        {
            Assert.AreEqual(Taxes2500.IncomeTax, 135, $"IncomeTax is not the expected value of 135 , instead it is {Taxes2500.IncomeTax}");
        }

        // Test  net income for gross income 3600
        [Test]
        public void Test_TaxCalc3600_NetIncome()
        {
            Assert.AreEqual(Taxes3600.NetIncome, 3076, $"NetIcome is not the expected value of 3076, instead it is {Taxes3600.NetIncome}");
        }

        // Test social tax for gross income 3600
        [Test]
        public void Test_TaxCalc3600_SocialTax()
        {
            Assert.AreEqual(Taxes3600.SocialTax, 300, $"SocialTax is not the expected value of 300, instead it is {Taxes3600.SocialTax}");
        }

        // Text income tax for gross income 3600
        [Test]
        public void Test_TaxCalc3600_IncomeTax()
        {
            Assert.AreEqual(Taxes3600.IncomeTax, 224, $"IncomeTax is not the expected value of 224 , instead it is {Taxes3600.IncomeTax}");
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
            TaxCalc980 = new TaxCalculator(TaxPayer980, _taxConfig);
            Taxes980 = TaxCalc980.CalculateTax();
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
            TaxCalc3400 = new TaxCalculator(TaxPayer3400, _taxConfig);
            Taxes3400 = TaxCalc3400.CalculateTax();
        }


        private void SetUpTaxPayer2500()
        {
            TaxPayer2500 = new TaxPayer()
            {
                GrossIncome = 2500,
                DateOfBirth = new DateTime(1980, 11, 1),
                FullName = "Joe Doe",
                CharitySpent = 150,
                SSN = "1345678"
            };
            TaxCalc2500 = new TaxCalculator(TaxPayer2500, _taxConfig);
            Taxes2500 = TaxCalc2500.CalculateTax();
        }

        private void SetUpTaxPayer3600()
        {
            TaxPayer3600 = new TaxPayer()
            {
                GrossIncome = 3600,
                DateOfBirth = new DateTime(2000, 11, 1),
                FullName = "Joe Doe",
                CharitySpent = 520,
                SSN = "1345678"
            };
            TaxCalc3600 = new TaxCalculator(TaxPayer3600, _taxConfig);
            Taxes3600 = TaxCalc3600.CalculateTax();
        }
       

        TaxPayer TaxPayer980 { get; set; } 
        TaxCalculator TaxCalc980 { get; set; }
        Taxes Taxes980 { get; set; }

        TaxPayer TaxPayer3400 { get; set; }
        TaxCalculator TaxCalc3400 { get; set; }
        Taxes Taxes3400 { get; set; }


        TaxPayer TaxPayer2500 { get; set; }
        TaxCalculator TaxCalc2500 { get; set; }
        Taxes Taxes2500 { get; set; }

        TaxPayer TaxPayer3600 { get; set; }
        TaxCalculator TaxCalc3600 { get; set; }
        Taxes Taxes3600 { get; set; }
        public object AppSettings { get; private set; }
    }
}
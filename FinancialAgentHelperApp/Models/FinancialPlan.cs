// Models/FinancialPlan.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancialAgentHelperApp.Models
{
    public class FinancialPlan
    {
        [Required]
        public string ApplicantName { get; set; }
        [Range(18, 100)]
        public int Age { get; set; }
        [Range(10000, 10000000)]
        public decimal LifeInsuranceCoverage { get; set; }
        [Range(0, 10000)]
        public decimal TotalMonthlyPremium { get; set; }
        [Range(0, 10000)]
        public decimal TotalMonthlyInvestment { get; set; }
        [Range(1, 50)]
        public int PayMonthlyForYears { get; set; }
        [Range(0, 20)]
        public decimal RateOfInterest { get; set; }
        public List<YearlyProjection> FirstPeriod { get; set; } = new List<YearlyProjection>();
        public List<YearlyProjection> SecondPeriod { get; set; } = new List<YearlyProjection>();
        public List<YearlyProjection> ThirdPeriod { get; set; } = new List<YearlyProjection>();
        public decimal TotalPremiumPaid { get; set; }
    }

    public class YearlyProjection
    {
        public int PolicyYear { get; set; }
        public int Age { get; set; }
        public decimal AnnualPayment { get; set; }
        public decimal AnnualInsuranceCost { get; set; }
        public decimal AmountInvested { get; set; }
        public decimal Growth { get; set; }
        public decimal TotalValue { get; set; }
    }
}
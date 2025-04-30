// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using FinancialAgentHelperApp.Models;
using System;
using System.Collections.Generic;

namespace FinancialAgentHelperApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new FinancialPlan
            {
                Age = 30,
                LifeInsuranceCoverage = 100000,
                TotalMonthlyPremium = 100,
                TotalMonthlyInvestment = 400,
                PayMonthlyForYears = 25,
                RateOfInterest = 9,
                FirstPeriod = new List<YearlyProjection>(),
                SecondPeriod = new List<YearlyProjection>(),
                ThirdPeriod = new List<YearlyProjection>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Results(FinancialPlan plan)
        {
            ModelState.Remove("FirstPeriod");
            ModelState.Remove("SecondPeriod");
            ModelState.Remove("ThirdPeriod");

            if (!ModelState.IsValid)
            {
                ViewData["ValidationErrors"] = true;
                plan.FirstPeriod ??= new List<YearlyProjection>();
                plan.SecondPeriod ??= new List<YearlyProjection>();
                plan.ThirdPeriod ??= new List<YearlyProjection>();
                return View("Index", plan);
            }

            plan.FirstPeriod = new List<YearlyProjection>();
            plan.SecondPeriod = new List<YearlyProjection>();
            plan.ThirdPeriod = new List<YearlyProjection>();
            decimal annualPayment = (plan.TotalMonthlyPremium + plan.TotalMonthlyInvestment) * 12;
            decimal annualInsuranceCost = plan.TotalMonthlyPremium * 12;
            decimal annualInvestment = plan.TotalMonthlyInvestment * 12;
            decimal totalValue = 0;
            plan.TotalPremiumPaid = annualPayment * plan.PayMonthlyForYears;

            // First Period (Years 1 to PayMonthlyForYears)
            for (int year = 1; year <= plan.PayMonthlyForYears; year++)
            {
                var projection = new YearlyProjection
                {
                    PolicyYear = year,
                    Age = plan.Age + year,
                    AnnualPayment = annualPayment,
                    AnnualInsuranceCost = annualInsuranceCost,
                    AmountInvested = annualInvestment
                };
                // Growth calculation: Interest on the previous total plus current investment
                // For Year 1, assume mid-year investment (half-year interest)
                if (year == 1)
                {
                    projection.Growth = Math.Round(projection.AmountInvested * (plan.RateOfInterest / 100) / 2, 2);
                }
                else
                {
                    projection.Growth = Math.Round((totalValue + projection.AmountInvested) * (plan.RateOfInterest / 100), 2);
                }
                totalValue = totalValue + projection.AmountInvested + projection.Growth;
                projection.TotalValue = Math.Round(totalValue, 2);
                plan.FirstPeriod.Add(projection);
            }

            // Second Period (Next 10 years)
            for (int year = plan.PayMonthlyForYears + 1; year <= plan.PayMonthlyForYears + 10; year++)
            {
                var projection = new YearlyProjection
                {
                    PolicyYear = year,
                    Age = plan.Age + year,
                    AnnualPayment = 0,
                    AnnualInsuranceCost = annualInsuranceCost,
                    AmountInvested = totalValue - annualInsuranceCost
                };
                projection.Growth = Math.Round(projection.AmountInvested * (plan.RateOfInterest / 100), 2);
                totalValue = projection.AmountInvested + projection.Growth;
                projection.TotalValue = Math.Round(totalValue, 2);
                plan.SecondPeriod.Add(projection);
            }

            // Third Period (Remaining years up to age 100)
            decimal currentInvestment = totalValue;
            for (int year = plan.PayMonthlyForYears + 11; plan.Age + year <= 100; year++)
            {
                var projection = new YearlyProjection
                {
                    PolicyYear = year,
                    Age = plan.Age + year,
                    AnnualPayment = 0,
                    AnnualInsuranceCost = 0,
                    AmountInvested = currentInvestment
                };
                projection.Growth = Math.Round(projection.AmountInvested * (plan.RateOfInterest / 100), 2);
                currentInvestment = projection.AmountInvested + projection.Growth;
                projection.TotalValue = Math.Round(currentInvestment, 2);
                plan.ThirdPeriod.Add(projection);
            }

            return View(plan);
        }
    }
}
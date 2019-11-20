using BikeRental.Models;
using BikeRental.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace BikeRentalTests
{
    [TestClass]
    public class BikeRental
    {
        [TestMethod]
        public void TestCostCalculation()
        {
            ICostCalculator calculator = new CostCalculator();

            var price = calculator.CalculateCosts(new DateTime(2019, 2, 14, 8, 15, 0), new DateTime(2019, 2, 14, 10, 30, 0), 3, 5);
            Assert.AreEqual(13, price);

            price = calculator.CalculateCosts(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 45, 0), 3, 100);
            Assert.AreEqual(3, price);

            price = calculator.CalculateCosts(new DateTime(2018, 2, 14, 8, 15, 0), new DateTime(2018, 2, 14, 8, 25, 0), 20, 100);
            Assert.AreEqual(0, price);

            }
        }
}
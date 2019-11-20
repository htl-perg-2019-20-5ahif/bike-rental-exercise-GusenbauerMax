using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeRental.Models;

namespace BikeRental.Services
{
    public class CostCalculator : ICostCalculator
    {
        public decimal CalculateCosts(DateTime Begin, DateTime End, Decimal RentalBasic, Decimal RentalExcessive)
        {
            double difTime = End.Subtract(Begin).TotalMinutes;
            decimal cost = 0;
            if (difTime <= 15) { return cost; }
            else
            {
                cost += RentalBasic;
                difTime -= 60;
                while (difTime > 0)
                {
                    cost += RentalExcessive;
                    difTime -= 60;
                }
                return cost;
            }
        }
    }
}

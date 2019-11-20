using BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Services
{
    public interface ICostCalculator
    {
        public decimal CalculateCosts(DateTime Begin, DateTime End, Decimal RentalBasic, Decimal RentalExcessive);
    }
}

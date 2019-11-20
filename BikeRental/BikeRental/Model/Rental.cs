using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public int BikeID { get; set; }

        public Bike Bike { get; set; }

        [Required]
        public DateTime Begin { get; set; }

        [Required]
        public DateTime End 
        { 
            get { return End;  }
            set
            {
                if (value < Begin)
                {
                    throw new ArgumentException("RentalEnd must be later than RentalBegin");
                }
                End = value;
            }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public decimal TotalCost { get; set; }

        public Boolean Paid { get; set; }
    }
}

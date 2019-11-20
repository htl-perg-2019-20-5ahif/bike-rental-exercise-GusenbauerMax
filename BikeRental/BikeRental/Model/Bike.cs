using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public enum Category
    {
        Standard_bike,
        Mountenbike,
        Trecking_bike,
        Racing_Bike
    }

    public class Bike
    {
        public int BikeID { get; set; }

        [Required]
        [MaxLength(25)]
        public String Brand { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public String Notes { get; set; }

        public DateTime LastService { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public decimal RentalBasic { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [RegularExpression(@"\d*.\d{1,2}", ErrorMessage = "Wrong format")]
        public decimal RentalExcessive { get; set; }

        public Category Category { get; set; }

        public List<Rental> Rentals { get; set; }

    }
}

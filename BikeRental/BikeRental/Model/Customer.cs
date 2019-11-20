using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }

    public class Customer
    {
        public int CustomerID { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public String LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(75)]
        public String Street { get; set; }

        [MaxLength(10)]
        public String HouseNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public String ZipCode { get; set; }

        [Required]
        [MaxLength(75)]
        public String Town { get; set; }

        public List<Rental> Rentals { get; set; }
    }
}

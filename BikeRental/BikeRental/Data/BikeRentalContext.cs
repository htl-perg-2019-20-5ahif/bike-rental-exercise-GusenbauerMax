using BikeRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BikeRental
{
    public class BikeRentalContext : DbContext
    {
        public BikeRentalContext(DbContextOptions<BikeRentalContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasKey(r => new { r.RentalID });

            modelBuilder.Entity<Bike>()
                .HasMany(b => b.Rentals)
                .WithOne(r => r.Bike);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Customer)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Rental> Rentals { get; set; }
    }
}

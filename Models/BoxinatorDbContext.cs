using boxinator.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models
{
    public class BoxinatorDbContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxType> BoxTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<User> Users { get; set; }

        public BoxinatorDbContext(IConfiguration config)
        {
            configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = configuration.GetSection("ConnectionStrings").GetSection("localSQLBoxinatorDB").Value;
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zone>().HasData(
                new Zone()
                {
                    Id = 1,
                    Name = "Europe",
                    CountryMultiplier = 100,
                }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name = "Finland",
                    ZoneId = 1
                }
            );

            modelBuilder.Entity<BoxType>().HasData(
                new BoxType()
                {
                    Id = 1,
                    Name = "Premium",
                    Weight = 8
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Email = "awesomemartta@gs.com",
                    CountryId = 1,
                    ZipCode = "610650",
                    PhoneNumber = "16064650210",
                    AccountType = "REGISTERED_USER"
                }
            );

            modelBuilder.Entity<Shipment>().HasData(
                new Shipment()
                {
                    Id = 1,
                    ReceiverName = "Petteri Smith",
                    CountryId = 1,
                    Cost = 100.00,
                    UserId = 1
                }
            );

            modelBuilder.Entity<Box>().HasData(
                new Box()
                {
                    Id = 1,
                    Color = "(32,178,170)",
                    BoxTypeId = 1,
                    ShipmentId = 1
                }
            );
            modelBuilder.Entity<ShipmentStatus>().HasData(
                new ShipmentStatus()
                {
                    Id = 1,
                    ShipmentId = 1,
                    Status = "CREATED",
                    Date = DateTime.Now

                }
            );
        }
    }
}

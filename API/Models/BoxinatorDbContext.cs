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
        public DbSet<ShipmentStatusLog> ShipmentStatusLogs { get; set; }
        public DbSet<Status> Statuses { get; set; }
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
            modelBuilder.Entity<ShipmentStatusLog>()
                .HasOne(m => m.Shipment)
                .WithMany(f => f.ShipmentStatusLogs)
                .HasForeignKey(m => m.ShipmentId);

            modelBuilder.Entity<ShipmentStatusLog>()
                .HasOne(m => m.Status)
                .WithMany(m => m.ShipmentStatusLogs)
                .HasForeignKey(m => m.StatusId);
            
            modelBuilder.Entity<Country>()
                .HasOne(m => m.Zone)
                .WithMany(c => c.Countries)
                .HasForeignKey(m => m.ZoneId);

            modelBuilder.Entity<Box>()
                .HasOne(m => m.BoxType)
                .WithMany(c => c.Boxes)
                .HasForeignKey(m => m.BoxTypeId);

            modelBuilder.Entity<Box>()
                .HasOne(m => m.Shipment)
                .WithMany(c => c.Boxes)
                .HasForeignKey(s => s.ShipmentId);

            modelBuilder.Entity<User>()
                .HasOne(m => m.Country)
                .WithMany(c => c.Users)
                .HasForeignKey(m => m.CountryId);

            modelBuilder.Entity<Zone>().HasData(
                new Zone()
                {
                    Id = 1,
                    Name = "Source Zone",
                    CountryMultiplier = 1,
                },
                new Zone()
                {
                    Id = 2,
                    Name = "Europe",
                    CountryMultiplier = 10,
                },
                new Zone()
                {
                    Id = 3,
                    Name = "Asia, Australia & Oceania",
                    CountryMultiplier = 20,
                },
                new Zone()
                {
                    Id = 4,
                    Name = "Americas",
                    CountryMultiplier = 30,
                },
                new Zone()
                {
                    Id = 5,
                    Name = "Africa",
                    CountryMultiplier = 40,
                }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name = "Finland",
                    ZoneId = 2
                },
                new Country()
                {
                    Id = 2,
                    Name = "Sweden",
                    ZoneId = 1
                },
                new Country()
                {
                    Id = 3,
                    Name = "Norway",
                    ZoneId = 1
                },
                new Country()
                {
                    Id = 4,
                    Name = "USA",
                    ZoneId = 4
                },
                new Country()
                {
                    Id = 5,
                    Name = "Germany",
                    ZoneId = 2
                },
                new Country()
                {
                    Id = 6,
                    Name = "China",
                    ZoneId = 3
                },
                new Country()
                {
                    Id = 7,
                    Name = "Australia",
                    ZoneId = 3
                },
                new Country()
                {
                    Id = 8,
                    Name = "Egypt",
                    ZoneId = 5
                },
                new Country()
                {
                    Id = 9,
                    Name = "Brazil",
                    ZoneId = 4
                }
            );

            modelBuilder.Entity<BoxType>().HasData(
                new BoxType()
                {
                    Id = 1,
                    Name = "Premium",
                    Weight = 8
                },
                new BoxType()
                {
                    Id = 2,
                    Name = "Deluxe",
                    Weight = 5
                },
                new BoxType()
                {
                    Id = 3,
                    Name = "Humble",
                    Weight = 2
                },
                new BoxType()
                {
                    Id = 4,
                    Name = "Basic",
                    Weight = 1
                }

            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Email = "awesomemartta@gmail.com",
                    Address = "Kungsgatan 54",
                    CountryId = 2,
                    ZipCode = "11122",
                    PhoneNumber = "16064650210",
                    AccountType = "REGISTERED_USER"
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Peppi",
                    LastName = "Mäkelä",
                    Email = "peppi.makela@gmail.com",
                    Address = "Pirjontie 10",
                    CountryId = 9,
                    ZipCode = "00200",
                    PhoneNumber = "0504055679",
                    AccountType = "ADMINISTRATOR"
                },
                new User()
                {
                    Id = 3,
                    FirstName = "Sebastian",
                    LastName = "Söderholm",
                    Email = "developer.musetech@gmail.com",
                    Address = "Kaivokatu 10",
                    CountryId = 1,
                    ZipCode = "06100",
                    PhoneNumber = "0400959078",
                    AccountType = "ADMINISTRATOR"
                },
                new User()
                {
                    Id = 4,
                    FirstName = "Jani",
                    LastName = "Vihervuori",
                    Email = "XXX",
                    Address = "Tie 10",
                    CountryId = 8,
                    ZipCode = "06830",
                    PhoneNumber = "040123456",
                    AccountType = "ADMINISTRATOR"
                }
            );

            modelBuilder.Entity<Shipment>().HasData(
                new Shipment()
                {
                    Id = 1,
                    FirstName = "Petteri",
                    LastName = "Smith",
                    Address = "Keskuskatu 1",
                    ZipCode = "00100",
                    Cost = 100.00,
                    CountryId = 1,
                    UserId = 1
                },
                new Shipment()
                {
                    Id = 2,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Address = "Kungsgatan 54",
                    ZipCode = "11122",
                    Cost = 200.00,
                    CountryId = 2,
                    UserId = 1
                },
                new Shipment()
                {
                    Id = 3,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Address = "Kungsgatan 54",
                    ZipCode = "11122",
                    Cost = 150.00,
                    CountryId = 2,
                    UserId = 2
                },
                new Shipment()
                {
                    Id = 4,
                    FirstName = "Peppi",
                    LastName = "Mäkelä",
                    Address = "Pirjontie 10",
                    ZipCode = "11122",
                    Cost = 258.00,
                    CountryId = 9,
                    UserId = 2
                },
                new Shipment()
                {
                    Id = 5,
                    FirstName = "Sebastian",
                    LastName = "Söderholm",
                    Address = "Kaivokatu 10",
                    ZipCode = "06100",
                    Cost = 4000.00,
                    CountryId = 1,
                    UserId = 2
                },
                new Shipment()
                {
                    Id = 6,
                    FirstName = "Peppi",
                    LastName = "Mäkelä",
                    Address = "Pirjontie 10",
                    ZipCode = "11122",
                    Cost = 156.00,
                    CountryId = 9,
                    UserId = 2
                },
                new Shipment()
                {
                    Id = 7,
                    FirstName = "Peppi",
                    LastName = "Mäkelä",
                    Address = "Pirjontie 10",
                    ZipCode = "11122",
                    Cost = 987.00,
                    CountryId = 9,
                    UserId = 3
                },
                new Shipment()
                {
                    Id = 8,
                    FirstName = "Peppi",
                    LastName = "Mäkelä",
                    Address = "Pirjontie 10",
                    ZipCode = "11122",
                    Cost = 248.00,
                    CountryId = 9,
                    UserId = 4
                },
                new Shipment()
                {
                    Id = 9,
                    FirstName = "Sebastian",
                    LastName = "Söderholm",
                    Address = "Kaivokatu 10",
                    ZipCode = "06100",
                    Cost = 4006.00,
                    CountryId = 1,
                    UserId = 4
                },
                new Shipment()
                {
                    Id = 10,
                    FirstName = "Sebastian",
                    LastName = "Söderholm",
                    Address = "Kaivokatu 10",
                    ZipCode = "06100",
                    Cost = 824.00,
                    CountryId = 1,
                    UserId = 3
                },
                new Shipment()
                {
                    Id = 11,
                    FirstName = "Petteri",
                    LastName = "Smith",
                    Address = "Keskuskatu 1",
                    ZipCode = "00100",
                    Cost = 36.00,
                    CountryId = 1,
                    UserId = 4
                },
                new Shipment()
                {
                    Id = 12,
                    FirstName = "Petteri",
                    LastName = "Smith",
                    Address = "Keskuskatu 1",
                    ZipCode = "00100",
                    Cost = 51.00,
                    CountryId = 1,
                    UserId = 3
                },
                new Shipment()
                {
                    Id = 13,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Address = "Kungsgatan 54",
                    ZipCode = "11122",
                    Cost = 364.00,
                    CountryId = 2,
                    UserId = 3
                },
                new Shipment()
                {
                    Id = 14,
                    FirstName = "Martta",
                    LastName = "Johnsson",
                    Address = "Kungsgatan 54",
                    ZipCode = "11122",
                    Cost = 645.00,
                    CountryId = 2,
                    UserId = 3
                }
            );

            modelBuilder.Entity<Box>().HasData(
                new Box()
                {
                    Id = 1,
                    Color = "rgb(32,178,170)",
                    BoxTypeId = 1,
                    ShipmentId = 1
                },
                new Box()
                {
                    Id = 2,
                    Color = "rgb(123,765,3)",
                    BoxTypeId = 1,
                    ShipmentId = 1
                },
                new Box()
                {
                    Id = 3,
                    Color = "rgb(235, 64, 52)",
                    BoxTypeId = 2,
                    ShipmentId = 2
                },
                new Box()
                {
                    Id = 4,
                    Color = "rgb(235, 223, 52)",
                    BoxTypeId = 3,
                    ShipmentId = 3
                },
                new Box()
                {
                    Id = 5,
                    Color = "rgb(232, 52, 235)",
                    BoxTypeId = 4,
                    ShipmentId = 3
                },
                new Box()
                {
                    Id = 6,
                    Color = "rgb(235, 201, 52)",
                    BoxTypeId = 4,
                    ShipmentId = 3
                },
                new Box()
                {
                    Id = 7,
                    Color = "rgb(129, 163, 36)",
                    BoxTypeId = 2,
                    ShipmentId = 4
                },
                new Box()
                {
                    Id = 8,
                    Color = "rgb(163, 36, 97)",
                    BoxTypeId = 3,
                    ShipmentId = 4
                },
                new Box()
                {
                    Id = 9,
                    Color = "rgb(230, 197, 213)",
                    BoxTypeId = 3,
                    ShipmentId = 5
                },
                new Box()
                {
                    Id = 10,
                    Color = "rgb(89, 76, 76)",
                    BoxTypeId = 1,
                    ShipmentId = 6
                },
                new Box()
                {
                    Id = 11,
                    Color = "rgb(31, 98, 128)",
                    BoxTypeId = 1,
                    ShipmentId = 6
                },
                new Box()
                {
                    Id = 12,
                    Color = "rgb(207, 198, 171)",
                    BoxTypeId = 3,
                    ShipmentId = 6
                },
                new Box()
                {
                    Id = 13,
                    Color = "rgb(136, 3, 252)",
                    BoxTypeId = 4,
                    ShipmentId = 7
                },
                new Box()
                {
                    Id = 14,
                    Color = "rgb(248, 252, 3)",
                    BoxTypeId = 2,
                    ShipmentId = 8
                },
                new Box()
                {
                    Id = 15,
                    Color = "rgb(51, 0, 31)",
                    BoxTypeId = 2,
                    ShipmentId = 8
                },
                new Box()
                {
                    Id = 16,
                    Color = "rgb(54, 62, 74)",
                    BoxTypeId = 4,
                    ShipmentId = 9
                },
                new Box()
                {
                    Id = 17,
                    Color = "rgb(133, 133, 133)",
                    BoxTypeId = 3,
                    ShipmentId = 9
                },
                new Box()
                {
                    Id = 18,
                    Color = "rgb(255, 186, 186)",
                    BoxTypeId = 1,
                    ShipmentId = 9
                },
                new Box()
                {
                    Id = 19,
                    Color = "rgb(168, 76, 57)",
                    BoxTypeId = 2,
                    ShipmentId = 10
                },
                new Box()
                {
                    Id = 20,
                    Color = "rgb(35, 87, 20)",
                    BoxTypeId = 2,
                    ShipmentId = 11
                },
                new Box()
                {
                    Id = 21,
                    Color = "rgb(8, 46, 48)",
                    BoxTypeId = 3,
                    ShipmentId = 12
                },
                new Box()
                {
                    Id = 22,
                    Color = "rgb(0, 10, 84)",
                    BoxTypeId = 4,
                    ShipmentId = 13
                },
                new Box()
                {
                    Id = 23,
                    Color = "rgb(255, 0, 187)",
                    BoxTypeId = 4,
                    ShipmentId = 14
                },
                new Box()
                {
                    Id = 24,
                    Color = "rgb(255, 0, 0)",
                    BoxTypeId = 1,
                    ShipmentId = 14
                },
                new Box()
                {
                    Id = 25,
                    Color = "rgb(0, 0, 255)",
                    BoxTypeId = 2,
                    ShipmentId = 14
                }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status()
                {
                    Id = 1,
                    Name = "CREATED"
                },
                new Status()
                {
                    Id = 2,
                    Name = "RECIEVED"
                },
                new Status()
                {
                    Id = 3,
                    Name = "INTRANSIT"
                },
                new Status()
                {
                    Id = 4,
                    Name = "COMPLETED"
                },
                new Status()
                {
                    Id = 5,
                    Name = "CANCELLED"
                }
            );

            modelBuilder.Entity<ShipmentStatusLog>().HasData(
                new ShipmentStatusLog()
                {
                    Id = 1,
                    ShipmentId = 1,
                    StatusId = 1,
                    Date = DateTime.Now

                },
                new ShipmentStatusLog()
                {
                    Id = 2,
                    ShipmentId = 1,
                    StatusId = 2,
                    Date = DateTime.Now

                },
                new ShipmentStatusLog()
                {
                    Id = 3,
                    ShipmentId = 2,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 4,
                    ShipmentId = 2,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 5,
                    ShipmentId = 3,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 6,
                    ShipmentId = 3,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 7,
                    ShipmentId = 3,
                    StatusId = 3,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 8,
                    ShipmentId = 4,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 9,
                    ShipmentId = 4,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 10,
                    ShipmentId = 5,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 11,
                    ShipmentId = 5,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 12,
                    ShipmentId = 5,
                    StatusId = 3,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 13,
                    ShipmentId = 5,
                    StatusId = 4,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 14,
                    ShipmentId = 6,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 15,
                    ShipmentId = 6,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 16,
                    ShipmentId = 7,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 17,
                    ShipmentId = 8,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 18,
                    ShipmentId = 8,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 19,
                    ShipmentId = 9,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 20,
                    ShipmentId = 10,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 21,
                    ShipmentId = 10,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 22,
                    ShipmentId = 10,
                    StatusId = 3,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 23,
                    ShipmentId = 11,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 24,
                    ShipmentId = 11,
                    StatusId = 5,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 25,
                    ShipmentId = 12,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 26,
                    ShipmentId = 13,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 27,
                    ShipmentId = 13,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 28,
                    ShipmentId = 13,
                    StatusId = 3,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 29,
                    ShipmentId = 13,
                    StatusId = 4,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 30,
                    ShipmentId = 14,
                    StatusId = 1,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 31,
                    ShipmentId = 14,
                    StatusId = 2,
                    Date = DateTime.Now
                },
                new ShipmentStatusLog()
                {
                    Id = 32,
                    ShipmentId = 14,
                    StatusId = 5,
                    Date = DateTime.Now
                }
            );
                
        }
    }
}

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
                },
                new Country()
                {
                    Id = 2,
                    Name = "Sweden",
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
                    FirstName = "Petteri",
                    LastName = "Smith",
                    Address = "Keskuskatu 1",
                    ZipCode = "00100",
                    Cost = 100.00,
                    CountryId = 1,
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

            modelBuilder.Entity<Status>().HasData(
                new Status()
                {
                    Id = 1,
                    Name = "CREATED"
                }
            );

            modelBuilder.Entity<ShipmentStatusLog>().HasData(
                new ShipmentStatusLog()
                {
                    Id = 1,
                    ShipmentId = 1,
                    StatusId = 1,
                    Date = DateTime.Now

                }
            );
                
        }
    }
}

using AutoMapper;
using boxinator.Controllers;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Services;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BoxinatorAPITests
{
    public class ShipmentsControllerTests
    {
        private readonly Mock<IShipmentService> _serviceMock = new Mock<IShipmentService>();
        private readonly ITestOutputHelper _testOutputHelper;

        public ShipmentsControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Get_Get_ReturnsShipmentAsync()
        {
            // Arrange
            //Mapper configuration
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Shipment, ShipmentReadDTO>();
                cfg.CreateMap<ShipmentReadDTO, Shipment>();
            });
            var mapper = config.CreateMapper();

            ShipmentsController controller = new ShipmentsController(_serviceMock.Object, mapper);

            var shipment = new Shipment
            {
                Id = 1,
                ReceiverName = "Petteri Smith",
                Cost = 19.99,
                User = new User() { Id = 1 },
                UserId = 1,
                Country = new Country() { Id = 1 },
                CountryId = 1,
                Boxes = new List<Box>(),
                ShipmentStatusLogs = new List<ShipmentStatusLog>()
            };

            _serviceMock.Setup(p => p.Get(1)).ReturnsAsync(shipment);

            // Act
            var result = await controller.Get(1);
            _testOutputHelper.WriteLine(result.Value.ReceiverName);

            // Assert
            Assert.True("Petteri Smith".Equals(result.Value.ReceiverName));
        }
    }
}
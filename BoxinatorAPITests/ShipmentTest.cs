using AutoMapper;
using boxinator.Controllers;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BoxinatorAPITests
{
    public class ShipmentsControllerTests
    {
        private ShipmentsController _shipmentsController;
        private readonly Mock<IShipmentService> _serviceMock = new Mock<IShipmentService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ITestOutputHelper _testOutputHelper;
        public ShipmentsControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _shipmentsController = new ShipmentsController(_serviceMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task Get_GetAllCurrent_ShouldReturnListOfShipments()
        {
            // Arrange
            var list = new List<ShipmentStatusLog>();
            var shipmentStatusLog = new ShipmentStatusLog
            {
                Id = 1,
                ShipmentId = 1,
                StatusId = 1,
                Status = new Status(),
                Shipment = new Shipment(),
                Date = DateTime.Now,
            };
            _serviceMock.Setup(x => x.GetAllCurrent()).ReturnsAsync(list);
            // Act
            var actual = await _shipmentsController.GetAllCurrent();
            // Assert
            Assert.Equal(2, (int)actual.Value.Count);
        }
        [Fact]
        public async Task Get_Get_ReturnsShipmentAsync()
        {
            // Arrange
            var shipmentId = 1;
            var receiverName = "Testi Pettri";
            var shipmentDTO = new Shipment
            {
                Id = 1,
                ReceiverName = "Testi Petteri",
                Cost = 19.99,
                User = new User() { Id = 1 },
                UserId = 1,
                Country = new Country() { Id = 1 },
                CountryId = 1,
                Boxes = new List<Box>(),
                ShipmentStatusLogs = new List<ShipmentStatusLog>()
            };
            _serviceMock.Setup(x => x.Get(shipmentId)).ReturnsAsync(shipmentDTO);
            // Act
            var actual = await _shipmentsController.Get(shipmentId);
            _testOutputHelper.WriteLine(actual.ToString()); // test print
            // Assert
            Assert.Equal(receiverName, actual.Value.ReceiverName);
        }
    }
}
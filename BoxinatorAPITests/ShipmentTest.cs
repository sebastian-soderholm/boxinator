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
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BoxinatorAPITests
{
    public class ShipmentsControllerTests
    {
        private readonly ShipmentsController _shipmentController;
        private readonly Mock<IShipmentService> _serviceMock = new Mock<IShipmentService>();
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TestData _testData = new TestData();
        private Mapper _mapper;

        public ShipmentsControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mapper = (Mapper)new MapperConfiguration(cfg => {
                cfg.CreateMap<ShipmentStatusLog, ShipmentStatusLogReadDTO>();
                cfg.CreateMap<ShipmentStatusLogReadDTO, ShipmentStatusLog>();
                cfg.CreateMap<Shipment, ShipmentReadDTO>();
                cfg.CreateMap<ShipmentReadDTO, Shipment>();
                cfg.CreateMap<Shipment, ShipmentCreateDTO>();
                cfg.CreateMap<ShipmentCreateDTO, Shipment>();
            }).CreateMapper();
            _shipmentController = new ShipmentsController(_serviceMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_Get_ReturnsShipmentById()
        {
            // Arrange
            int shipmentId = 1;
            _serviceMock.Setup(p => p.Get(shipmentId)).ReturnsAsync(_testData.Shipment);

            // Act
            var actual = await _shipmentController.Get(shipmentId);
            _testOutputHelper.WriteLine(actual.Value.ReceiverName);

            // Assert
            Assert.True(_testData.Shipment.ReceiverName.Equals(actual.Value.ReceiverName));
        }

        [Fact]
        public async Task Get_GetByUser_ReturnsShipmentsByUserId()
        {
            // Arrange
            _serviceMock.Setup(p => p.GetByUser(1)).ReturnsAsync(_testData.ShipmentList);

            var expected = _mapper.Map<List<ShipmentReadDTO>>(_testData.ShipmentList);

            // Act
            var actual = await _shipmentController.GetByUser(1);
            _testOutputHelper.WriteLine(actual.Value[0].ReceiverName.ToString());

            // Assert
            Assert.Equal(expected.Count, actual.Value.Count);

        }

        [Fact]
        public async Task Get_GetAllCurrent_ReturnsCurrentShipments()
        {
            // Arrange
            _serviceMock.Setup(p => p.GetAllCurrent()).ReturnsAsync(_testData.StatusLogList);
            var expected = _mapper.Map<List<ShipmentStatusLogReadDTO>>(_testData.StatusLogList);

            // Act
            var actual = await _shipmentController.GetAllCurrent();

            // Assert
            Assert.Equal(expected.Count, actual.Value.Count);

        }

        [Fact]
        public async Task Get_GetAllComplete_ReturnsCompletedShipments()
        {
            // Arrange
            _serviceMock.Setup(p => p.GetAllComplete()).ReturnsAsync(_testData.StatusLogList);
            var expected = _mapper.Map<List<ShipmentStatusLogReadDTO>>(_testData.StatusLogList);

            // Act
            var actual = await _shipmentController.GetAllComplete();

            // Assert
            Assert.Equal(expected.Count, actual.Value.Count);

        }

        [Fact]
        public async Task Get_GetAllCancelled_ReturnsCancelledShipments()
        {
            // Arrange
            _serviceMock.Setup(p => p.GetAllCancelled()).ReturnsAsync(_testData.StatusLogList);
            var expected = _mapper.Map<List<ShipmentStatusLogReadDTO>>(_testData.StatusLogList);

            // Act
            var actual = await _shipmentController.GetAllCancelled();

            // Assert
            Assert.Equal(expected.Count, actual.Value.Count);

        }

        [Fact]
        public async Task Delete_Delete_ReturnsTrue()
        {
            // Arrange
            int shipmentId = 4;
            _serviceMock.Setup(x => x.Delete(shipmentId)).ReturnsAsync(true);

            // Act
            var actual = await _shipmentController.Delete(shipmentId);

            // Assert
            Assert.True(true.Equals(actual.Value));

        }

        [Fact]
        public async Task Post_Add_ReturnsAddedShipment()
        {
            //Arrange
            var newShipment = _mapper.Map<ShipmentCreateDTO>(_testData.NewShipment);

            // Act
            var actual = await _shipmentController.Add(newShipment);

            //Assert
            Assert.True(_testData.NewShipment.ReceiverName.Equals(actual.Value.ReceiverName));
        }

        [Fact]
        public async Task Put_Update_ReturnsUpdatedShipment()
        {
            //Arrange
            var newShipment = _mapper.Map<ShipmentCreateDTO>(_testData.NewShipment);
            newShipment.ReceiverName = "New Jukka";

            // Act
            var actual = await _shipmentController.Add(newShipment);

            //Assert
            Assert.True(_testData.NewShipment.ReceiverName.Equals(actual.Value.ReceiverName));
        }


    }

}
//using AutoMapper;
//using boxinator.Controllers;
//using boxinator.Models;
//using boxinator.Models.Domain;
//using boxinator.Models.DTO.Country;
//using boxinator.Models.DTO.User;
//using boxinator.Services.Interfaces;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;
//using Xunit.Abstractions;

//namespace BoxinatorAPITests
//{
//    public class SettingsTest
//    {
//        private readonly SettingsController _settingsController;
//        private readonly Mock<ISettingsService> _serviceMock = new Mock<ISettingsService>();
//        private readonly ITestOutputHelper _testOutputHelper;
//        private readonly TestData _testData = new TestData();
//        private Mapper _mapper;

//        public SettingsTest(ITestOutputHelper testOutputHelper)
//        {
//            _testOutputHelper = testOutputHelper;
//            _mapper = (Mapper)new MapperConfiguration(cfg => {
//                cfg.CreateMap<Country, CountryReadDTO>();
//                cfg.CreateMap<CountryCreateDTO, Country>();
//                cfg.CreateMap<CountryEditDTO, Country>();
//                cfg.CreateMap<Country, CountryEditDTO>();

//            }).CreateMapper();

//            _settingsController = new SettingsController(_serviceMock.Object, _mapper);
//        }

//        [Fact]
//        public async Task Get_GetAll_ReturnsAllCountries()
//        {
//            // Arrange
//            _serviceMock.Setup(p => p.GetAll()).ReturnsAsync(_testData.CountryList);

//            var expected = _mapper.Map<List<CountryReadDTO>>(_testData.CountryList);

//            // Act
//            var actual = await _settingsController.GetAll();

//            // Assert
//            Assert.Equal(expected.Count, actual.Value.Count);

//        }

//        [Fact]
//        public async Task Post_Add_ReturnsAddedCountry()
//        {
//            //Arrange
//            var newCountry = _mapper.Map<CountryCreateDTO>(_testData.Country);

//            // Act
//            var actual = await _settingsController.Add(newCountry);

//            //Assert
//            Assert.True(_testData.NewUser.Email.Equals(actual.Value));
//        }

//        [Fact]
//        public async Task Put_Update_ReturnsUpdatedCountry()
//        {
//            //Arrange
//            int countryId = 1;
//            var udpatedCountry = _mapper.Map<CountryEditDTO>(_testData.Country);
//            udpatedCountry.Name = "Norway";

//            // Act
//            var actual = await _settingsController.Update(countryId, udpatedCountry);

//            //Assert
//            Assert.True(_testData.NewUser.Email.Equals(actual.Value));
//        }
//    }
//}

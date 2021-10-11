using AutoMapper;
using boxinator.Controllers;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BoxinatorAPITests
{
    public class AccountTest
    {
        private readonly AccountController _accountController;
        private readonly Mock<IAccountService> _serviceMock = new Mock<IAccountService>();
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TestData _testData = new TestData();
        private Mapper _mapper;

        public AccountTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _mapper = (Mapper)new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserReadDTO>();
                cfg.CreateMap<UserCreateDTO, User>();

            }).CreateMapper();

            _accountController = new AccountController(_serviceMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_Get_ReturnsUserById()
        {
            // Arrange
            int accountId = 1;
            _serviceMock.Setup(p => p.Get(accountId)).ReturnsAsync(_testData.User);

            // Act
            var actual = await _accountController.Get(accountId);
            _testOutputHelper.WriteLine(actual.Value.Email);

            // Assert
            Assert.True(_testData.User.Email.Equals(actual.Value.Email));
        }

        [Fact]
        public async Task Delete_Delete_ReturnsTrue()
        {
            // Arrange
            int userId = 4;
            _serviceMock.Setup(x => x.Delete(userId)).ReturnsAsync(true);

            // Act
            var actual = await _accountController.Delete(userId);

            // Assert
            Assert.True(true.Equals(actual.Value));

        }

        [Fact]
        public async Task Post_Add_ReturnsAddedUser()
        {
            //Arrange
            var newUser = _mapper.Map<UserCreateDTO>(_testData.NewUser);

            // Act
            var actual = await _accountController.Add(newUser);

            //Assert
            Assert.True(_testData.NewUser.Email.Equals(actual.Value.Email));
        }

        [Fact]
        public async Task Put_Update_ReturnsUpdatedUser()
        {
            //Arrange
            var newUser = _mapper.Map<UserCreateDTO>(_testData.NewUser);
            newUser.Email = "updatedemail@test.com";

            // Act
            var actual = await _accountController.Add(newUser);

            //Assert
            Assert.True(_testData.NewUser.Email.Equals(actual.Value.Email));
        }
    }
}

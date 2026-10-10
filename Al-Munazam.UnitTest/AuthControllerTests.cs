using NUnit.Framework;
using API;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using Application.Entities;
using Al_Munazam.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Al_Munazam.Controller;
using Infrastructure.Mongo;
using Application.IRepositories;
using AutoMapper;

namespace Al_Munazam.UnitTest
{
    public class AuthControllerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IModuleRepository> _moduleRepositoryMock;
        private AuthController _authController;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _moduleRepositoryMock = new Mock<IModuleRepository>();

            _configurationMock.Setup(c => c["JwtSettings:SecretKey"]).Returns("supersecretkeyyoushouldnotcommittogithub");
            _configurationMock.Setup(c => c["JwtSettings:Issuer"]).Returns("issuer");
            _configurationMock.Setup(c => c["JwtSettings:Audience"]).Returns("audience");

            _authController = new AuthController(
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _moduleRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        //[Test]
        //public async Task Login_WithValidCredentials_ReturnsOkResult()
        //{
        //    // Arrange
        //    var user = new Users
        //    {
        //        Email = "test@test.com",
        //        Password = BCrypt.Net.BCrypt.HashPassword("password"),
        //        Modules = null,
        //        Type = "User",
        //        ID = MongoDB.Bson.ObjectId.GenerateNewId()
        //    };

        //    _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync("test@test.com")).ReturnsAsync(user);
        //    _moduleRepositoryMock.Setup(repo => repo.GetAllModulesAsync(It.IsAny<string>())).ReturnsAsync(new List<Modules>());

        //    // Act
        //    var result = await _authController.Login(new LoginRequest
        //    {
        //        Username = "test@test.com",
        //        Password = "password"
        //    });

        //    // Assert
        //    Assert.IsInstanceOf<OkObjectResult>(result);
        //    var okResult = result as OkObjectResult;
        //    Assert.NotNull(okResult?.Value);
        //}

        //[Test]
        //public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        //{
        //    // Arrange
        //    _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync("test@test.com")).ReturnsAsync((Users)null);

        //    // Act
        //    var result = await _authController.Login(new LoginRequest
        //    {
        //        Username = "test@test.com",
        //        Password = "wrongpassword"
        //    });

        //    // Assert
        //    Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
        //    var unauthorizedResult = result as UnauthorizedObjectResult;
        //    Assert.AreEqual("Invalid email or password.", unauthorizedResult?.Value);
        //}

        //[Test]
        //public async Task Register_WithExistingUser_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var existingUser = new Users { Email = "test@test.com" };
        //    _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync("test@test.com")).ReturnsAsync(existingUser);

        //    // Act
        //    var result = await _authController.Register(new UserDto { Email = "test@test.com" });

        //    // Assert
        //    Assert.IsInstanceOf<BadRequestObjectResult>(result);
        //    var badRequestResult = result as BadRequestObjectResult;
        //    Assert.AreEqual("User already exists.", badRequestResult?.Value);
        //}

        //[Test]
        //public async Task Register_WithNewUser_ReturnsOkResult()
        //{
        //    // Arrange
        //    _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync("newuser@test.com")).ReturnsAsync((Users)null);

        //    // Act
        //    var result = await _authController.Register(new UserDto { Email = "newuser@test.com" });

        //    // Assert
        //    Assert.IsInstanceOf<OkObjectResult>(result);
        //    var okResult = result as OkObjectResult;
        //    Assert.AreEqual(new { Message = "User registered successfully." }, okResult.Value);
        //}
    }
}

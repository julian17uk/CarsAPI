using System;
using CarsAPI.Controllers;
using CarsAPI.Models;
using CarsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace CarsAPITest.ControllerTests
{
    public class CarControllerShould
    {
        private Mock<ICarService> mockedService;
        private CarsController carController;
        private Car testcar = new Car();

        public CarControllerShould()
        {
            mockedService = new Mock<ICarService>();
            carController = new CarsController(mockedService.Object);
        }

        [Fact]
        public void AddReturnsOK()
        {
            var Response = carController.Post(testcar);

            Response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void CallServiceCreate()
        {
            Car expectedCreatedCar = new Car();

            carController.Post(testcar);

            mockedService.Verify(mock => mock.CreateCar(testcar), Times.Once());
        }
    }

}

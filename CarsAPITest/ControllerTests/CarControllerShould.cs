﻿using System;
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
            carController.Post(testcar);

            mockedService.Verify(mock => mock.CreateCar(testcar), Times.Once());
        }

        [Fact]
        public void ReturnsCreatedCar()
        {
            var expectedCreatedCar = new Car();

            mockedService.Setup(mock => mock.CreateCar(testcar)).Returns(expectedCreatedCar);

            var response = carController.Post(testcar);

            var okResult = response.Result as OkObjectResult;

            okResult.Value.ShouldBe(expectedCreatedCar);
        }

        [Fact]
        public void DeleteReturnOk()
        {
            var response = carController.Delete(1);

            response.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public void DeleteCallServiceDelete()
        {
            carController.Delete(1);

            mockedService.Verify(mock => mock.DeleteCar(1), Times.Once());
        }
    }

}

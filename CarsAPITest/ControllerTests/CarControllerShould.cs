﻿using System;
using System.Collections.Generic;
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
        public void ReturnsRetrievedCar()
        {
            var expectedRetrievedCar = new Car();

            var expectedAllcars = new List<Car>()
            {
                expectedRetrievedCar
            };

            mockedService.Setup(mockedService => mockedService.GetAll()).Returns(expectedAllcars);

            var response = carController.Get();

            var okResult = response.Result as OkObjectResult;

            okResult.Value.ShouldBe(expectedAllcars);

        }

        [Fact]
        public void GetReturnsOK()
        {
            var Response = carController.Get();

            Response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetAllIsCalled()
        {
            carController.Get();

            mockedService.Verify(mockedService => mockedService.GetAll(), Times.Once());




        }
    }

}

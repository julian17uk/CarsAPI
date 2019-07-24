using System;
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

        // Update data
		static string make = "fake";
		static string model = "50";
		static string colour = "green";
		static int year = 2019;

		Car carUpdateData = new Car() { Make = make, Model = model, Colour = colour, Year = year };

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

        [Fact]
        public void DeleteThrowNotFound()
        {
            int id = 99;

            mockedService.Setup(mock => mock.DeleteCar(id)).Throws<KeyNotFoundException>();

            var response = carController.Delete(id);

            response.ShouldBeOfType<NotFoundResult>();
        }

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

        [Fact]
        public void UpdateReturnOK()
		{
			var Response = carController.Put(1, testcar);

			Response.Result.ShouldBeOfType<OkObjectResult>();
		}

		[Fact]
		public void UpdateCallServiceUpdate()
		{
			int id = 1;

			carController.Put(id, testcar);

			mockedService.Verify(mock => mock.UpdateCar(id, testcar), Times.Once());
		}

        [Fact]
        public void UpdateReturnsUpdatedCar()
		{
			int id = 1;

			Car expectedUpdatedCar = new Car() { Id = id, Make = make, Model = model, Colour = colour, Year = year };

			mockedService.Setup(mock => mock.UpdateCar(id, carUpdateData)).Returns(expectedUpdatedCar);

			var response = carController.Put(id, carUpdateData);
			var result = response.Result as OkObjectResult;

			result.Value.ShouldBe(expectedUpdatedCar);
		}

        [Fact]
        public void UpdateThrowNotFound()
		{
			int id = 99;

			mockedService.Setup(mock => mock.UpdateCar(id, carUpdateData)).Throws<KeyNotFoundException>();

			var response = carController.Put(id, carUpdateData);

			response.ShouldBeOfType<NotFoundResult>();
		}
	}

}

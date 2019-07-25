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
    public class CarControllerTests
    {
        protected Mock<ICarService> mockedService;
        protected CarsController carController;
        protected Car testCar = new Car();

        public CarControllerTests()
        {
            mockedService = new Mock<ICarService>();
            carController = new CarsController(mockedService.Object);
        }
    }

    public class CreateShould : CarControllerTests
    {
        [Fact]
        public void ReturnOk()
        {
            var response = carController.Post(testCar);

            response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void CallServiceCreate()
        {
            carController.Post(testCar);

            mockedService.Verify(m => m.CreateCar(testCar), Times.Once());
        }

        [Fact]
        public void ReturnCreatedCar()
        {
            var expectedCreatedCar = new Car();

            mockedService.Setup(mock => mock.CreateCar(testCar)).Returns(expectedCreatedCar);

            var response = carController.Post(testCar);

            var okResult = response.Result as OkObjectResult;

            okResult.Value.ShouldBe(expectedCreatedCar);
        }
    }

    public class DeleteShould : CarControllerTests
    {
        [Fact]
        public void ReturnOk()
        {
            var response = carController.Delete(1);

            response.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public void CallServiceDelete()
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
    }

    public class GetShould : CarControllerTests
    {
        [Fact]
        public void ReturnOk()
        {
            var response = carController.Get();

            response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void CallServiceGetAll()
        {
            carController.Get();

            mockedService.Verify(m => m.GetAll(), Times.Once());
        }

        [Fact]
        public void ReturnAllCars()
        {
            var expectedAllCars = new List<Car>();

            mockedService.Setup(m => m.GetAll()).Returns(expectedAllCars);

            var response = carController.Get();

            var okResult = response.Result as OkObjectResult;

            okResult.Value.ShouldBe(expectedAllCars);
        }
    }

    public class UpdateShould : CarControllerTests
    {
        private static readonly int _id = 1;
        private static readonly int _falseId = 99;

        // Static data properties
        private static readonly string _make = "Mercedes";
        private static readonly string _model = "Some-nice-car-name";
        private static readonly string _colour = "Pink";
        private static readonly int _year = 2000;

        // Update data for car
        private Car _carUpdateData = new Car()
        {
            Make = _make,
            Model = _model,
            Colour = _colour,
            Year = _year
        };

        // Expected updated car
        private Car _expectedUpdatedCar = new Car()
        {
            Id = _id,
            Make = _make,
            Model = _model,
            Colour = _colour,
            Year = _year
        };

        [Fact]
        public void ReturnOk()
        {
            var response = carController.Put(_id, testCar);

            response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void CallServiceUpdate()
        {
            carController.Put(_id, testCar);

            mockedService.Verify(m => m.UpdateCar(_id, testCar), Times.Once());
        }

        [Fact]
        public void ReturnUpdatedCar()
        {
            mockedService.Setup(m => m.UpdateCar(_id, _carUpdateData)).Returns(_expectedUpdatedCar);

            var response = carController.Put(_id, _carUpdateData);

            var result = response.Result as OkObjectResult;

            result.Value.ShouldBe(_expectedUpdatedCar);
        }

        [Fact]
        public void ReturnNotFound()
        {
            mockedService.Setup(m => m.UpdateCar(_falseId, _carUpdateData)).Throws<KeyNotFoundException>();

            var response = carController.Put(_falseId, _carUpdateData);

            response.Result.ShouldBeOfType<NotFoundResult>();
        }
    }

    public class GetByIdShould : CarControllerTests
    {
        private static readonly int _id = 1;
        private static readonly int _falseId = 99;

        // Expected car retrieved
        private Car _expectedRetrievedCar = new Car()
        {
            Id = _id,
            Make = "Renault",
            Model = "80-Animal",
            Colour = "silver",
            Year = 2009
        };

        [Fact]
        public void ReturnOk()
        {
            var response = carController.Get(_id);

            response.Result.ShouldBeOfType<OkObjectResult>();
        }

        [Fact]
        public void CallServiceGetCar()
        {
            carController.Get(_id);

            mockedService.Verify(m => m.GetCar(_id), Times.Once());
        }

        [Fact]
        public void ReturnSingleCar()
        {
            mockedService.Setup(m => m.GetCar(_id)).Returns(_expectedRetrievedCar);

            var response = carController.Get(_id);

            var okResult = response.Result as OkObjectResult;

            okResult.Value.ShouldBe(_expectedRetrievedCar);
        }

        [Fact]
        public void ReturnNotFound()
        {
            mockedService.Setup(m => m.GetCar(_falseId)).Throws<KeyNotFoundException>();

            var response = carController.Get(_falseId);

            response.Result.ShouldBeOfType<NotFoundResult>();
        }
    }
}

using System;
using Moq;
using Xunit;
using CarsAPI.Models;
using CarsAPI.Services;
using CarsAPI.Controllers;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarsAPI.Repositories;

namespace CarsAPITest.ServiceTests
{
    public class CarServiceShould
    {
        private Mock<ICarRepository> mockedRepository;
        private CarService carService;
        private Car testcar = new Car();




        public CarServiceShould()
        {
            mockedRepository = new Mock<ICarRepository>();
            carService = new CarService(mockedRepository.Object);
        }


        [Fact]
        public void RepositoryGetAllCarsIsCalled()
        {
            carService.GetAll();

            mockedRepository.Verify(mockedRepository => mockedRepository.GetAll(), Times.Once());

        }



        [Fact]
        public void RepositoryGetAllCarsReturnsList()
        {
            var ExpectedCarList = new List<Car>();

            mockedRepository.Setup(mockedRepository => mockedRepository.GetAll()).Returns(ExpectedCarList);

            var response=carService.GetAll();

            response.ShouldBe(ExpectedCarList);



        }

        [Fact]
        public void RepositoryGetCarIsCalled()
        {
            var ExpectedCar = new Car();

            mockedRepository.Setup(mockedRepository => mockedRepository.GetCar(5)).Returns(ExpectedCar);

            carService.GetCar(5);

            mockedRepository.Verify(mockedRepository => mockedRepository.GetCar(5), Times.Once());

        }

        [Fact]
        public void RepositoryGetCarReturnsCar()
        {
            var ExpectedCar = new Car();

            mockedRepository.Setup(mockedRepository => mockedRepository.GetCar(5)).Returns(ExpectedCar);

            var response=carService.GetCar(5);

            response.ShouldBe(ExpectedCar);
        }

        [Fact]
        public void GetCarNotFound()
        {
            Should.Throw<KeyNotFoundException>(()  => carService.GetCar(57)).Message.ShouldBe("Car not found");


        }

        [Fact]
        public void RepositoryUpdateCarIsCalled()
        {

            var newCar = new Car();

            mockedRepository.Setup(mockedRepository => mockedRepository.UpdateCar(5, newCar)).Returns(newCar);

            carService.UpdateCar(5, newCar);

            mockedRepository.Verify(mockedRepository => mockedRepository.UpdateCar(5, newCar), Times.Once());

        }

        [Fact]
        public void RepositoryUpdateCarReturnsCar()
        {
            var newCar = new Car();

            mockedRepository.Setup(mockedRepository => mockedRepository.UpdateCar(5, newCar)).Returns(newCar);

            var response = carService.UpdateCar(5, newCar);

            response.ShouldBe(newCar);
        }

        [Fact]
        public void UpdateCarNotFound()
        {
            var newCar = new Car();

            Should.Throw<KeyNotFoundException>(() => carService.UpdateCar(5, newCar)).Message.ShouldBe("Car not found");

        }

    }
}

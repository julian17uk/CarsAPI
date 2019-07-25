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
    public class CarServiceTests
    {
        protected CarService carService;
        protected Mock<ICarRepository> mockedCarRepo;

        public CarServiceTests()
        {
            mockedCarRepo = new Mock<ICarRepository>();
            carService = new CarService(mockedCarRepo.Object);
        }
    }

    public class CreateShould : CarServiceTests
    {
        // Car creation data
        private static string _make = "new-car";
        private static string _model = "mx-nModel";
        private static string _colour = "pink";
        private static int _year = 2019;

        // Car creation template
        static Car carCreationData = new Car()
        {
            Make = _make,
            Model = _model,
            Colour = _colour,
            Year = _year
        };

        // Expected created car
        static Car expectedCarCreated = new Car()
        {
            Id = 1,
            Make = _make,
            Model = _model,
            Colour = _colour,
            Year = _year
        };

        [Fact]
        public void CallRepoCreateCar()
        {
            carService.CreateCar(carCreationData);

            mockedCarRepo.Verify(m => m.CreateCar(carCreationData), Times.Once());
        }

        [Fact]
        public void ReturnCreatedCar()
        {
            mockedCarRepo.Setup(m => m.CreateCar(carCreationData)).Returns(expectedCarCreated);

            Car createdCar = carService.CreateCar(carCreationData);

            createdCar.ShouldBe(expectedCarCreated);
        }
    }

    public class DeleteShould : CarServiceTests
    {
        [Fact]
        public void CallRepoDeleteCar()
        {
            int id = 5;

            carService.DeleteCar(id);

            mockedCarRepo.Verify(m => m.DeleteCar(id));
        }
    }
    
    public class CarServiceShould {
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
    }
}

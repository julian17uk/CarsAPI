using System;
using CarsAPI.Models;
using CarsAPI.Repositories;
using CarsAPI.Services;
using Moq;
using Shouldly;
using Xunit;

namespace CarsAPITest.ServiceTests
{
    public class CarServiceShould
    {
        static CarService carService;
        static Mock<ICarRepository> mockedCarRepo;

        public class CarCreateShould : CarServiceShould
        {
            public CarCreateShould()
            {
                mockedCarRepo = new Mock<ICarRepository>();
                carService = new CarService(mockedCarRepo.Object);
            }

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

        public class CarDeleteShould : CarServiceShould
        {
            CarDeleteShould()
            {
                mockedCarRepo = new Mock<ICarRepository>();
                carService = new CarService(mockedCarRepo.Object);
            }

            [Fact]
            public void CallRepoDeleteCar()
            {
                int id = 5;

                carService.DeleteCar(id);

                mockedCarRepo.Verify(m => m.DeleteCar(id));
            }
        }
    }
}

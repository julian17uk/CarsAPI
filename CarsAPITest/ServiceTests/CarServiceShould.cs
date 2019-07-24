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

        public CarServiceShould()
        {
            mockedCarRepo = new Mock<ICarRepository>();
            carService = new CarService(mockedCarRepo.Object);
        }

        public class CarCreateShould : CarServiceShould
        {
            // Car creation data
            static string make = "new-car";
            static string model = "mx-nModel";
            static string colour = "pink";
            static int year = 2019;

            // Car creation template
            static Car carCreationData = new Car()
            {
                Make = make,
                Model = model,
                Colour = colour,
                Year = year
            };

            // Expected created car
            static Car expectedCarCreated = new Car()
            {
                Id = 1,
                Make = make,
                Model = model,
                Colour = colour,
                Year = year
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
                Car createdCar = carService.CreateCar(carCreationData);

                mockedCarRepo.Setup(m => m.CreateCar(carCreationData)).Returns(expectedCarCreated);

                createdCar.ShouldBe(expectedCarCreated);
            }
        }
    }
}

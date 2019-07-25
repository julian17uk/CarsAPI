using Moq;
using Xunit;
using CarsAPI.Models;
using CarsAPI.Services;
using Shouldly;
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
        private Car _carToDelete = new Car();

        [Fact]
        public void CallRepoDeleteCar()
        {
            int id = 5;

            // Must stub `GetCar(int id)` as it is relied upon by Delete
            mockedCarRepo.Setup(m => m.GetCar(id)).Returns(_carToDelete);

            carService.DeleteCar(id);

            mockedCarRepo.Verify(m => m.DeleteCar(id));
        }

        [Fact]
        public void ThrowKeyNotFoundException()
        {
            Should.Throw<KeyNotFoundException>(() =>
            {
                carService.DeleteCar(99);
            }
            ).Message.ShouldBe("Car with id 99 not found");
        }
    }

    public class GetAllShould : CarServiceTests
    {
        private List<Car> _expectedCarList = new List<Car>();

        [Fact]
        public void CallRepoGetAll()
        {
            carService.GetAll();

            mockedCarRepo.Verify(m => m.GetAll(), Times.Once());
        }

        [Fact]
        public void ReturnCarList()
        {
            mockedCarRepo.Setup(m => m.GetAll()).Returns(_expectedCarList);

            var actualCarList = carService.GetAll();

            actualCarList.ShouldBe(_expectedCarList);
        }
    }

    public class GetCarShould : CarServiceTests
    {
        private static readonly int _id = 1;
        private Car _expectedCar = new Car();

        [Fact]
        public void CallRepoGetCar()
        {
            mockedCarRepo.Setup(m => m.GetCar(_id)).Returns(_expectedCar);

            carService.GetCar(_id);

            mockedCarRepo.Verify(m => m.GetCar(_id), Times.Once());
        }

        [Fact]
        public void ReturnCar()
        {
            mockedCarRepo.Setup(m => m.GetCar(_id)).Returns(_expectedCar);

            var actualCar = carService.GetCar(_id);

            actualCar.ShouldBe(_expectedCar);
        }
        
        [Fact]
        public void ThrowKeyNotFoundException()
        {
            Should.Throw<KeyNotFoundException>(() =>
            {
                carService.GetCar(99);
            }
            ).Message.ShouldBe("Car with id 99 not found");
        }
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

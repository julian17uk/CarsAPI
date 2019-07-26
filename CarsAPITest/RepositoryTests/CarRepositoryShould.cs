using System;
using System.Linq;
using CarsAPI.Models;
using CarsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace CarsAPITest.RepositoryTests
{
    public class CarRepositoryShould
    {
        DbContextOptions options;

        public CarRepositoryShould()
        {
            options = new DbContextOptionsBuilder<SQLCarRepository>()
                .UseInMemoryDatabase(databaseName: "TestCarDB").Options;
            using (var repo = new SQLCarRepository(options))
            {
   //             repo.Database.EnsureDeleted();
            }

        }

        public class CreateShould : CarRepositoryShould
        {
            Car CarToCreate = new Car()
            {
                Make = "Ford",
                Model = "F150",
                Colour = "Silver",
                Year = 2017
            };

            Car ExpectedCar = new Car()
            {
                Id = 1,
                Make = "Ford",
                Model = "F150",
                Colour = "Silver",
                Year = 2017
            };

            [Fact]
            public void AddCarToTable()
            {
                using (var Repo = new SQLCarRepository(options))
                {

                    Repo.Cars.Count().ShouldBe(0);

                    Repo.CreateCar(CarToCreate);
                }

                using (var Repo = new SQLCarRepository(options))
                {
                    Repo.Cars.Count().ShouldBe(1);

                    Repo.Cars.Single().Equals(ExpectedCar).ShouldBeTrue();
                }
            }

            [Fact]
            public void AddCarToTableReturn()
            {
                using (var Repo = new SQLCarRepository(options))
                {
                    var MadeCar = Repo.CreateCar(CarToCreate);

                    MadeCar.Equals(ExpectedCar).ShouldBeTrue();
                }

            }
        }

        public class GetAllShould : CarRepositoryShould
        {
            Car CarToCreate = new Car()
            {
                Make = "Ford",
                Model = "F150",
                Colour = "Silver",
                Year = 2017
            };

            Car ExpectedCar = new Car()
            {
                Id = 1,
                Make = "Ford",
                Model = "F150",
                Colour = "Silver",
                Year = 2017
            };

            [Fact]
            public void GetAllCarsFromTable()
            {
                using (var Repo = new SQLCarRepository(options))
                {
                    Repo.Add(CarToCreate);

                    var GetAllCars = Repo.GetAll();

                    GetAllCars.Contains(ExpectedCar).ShouldBeTrue();

                }
            }


        }

    }
}



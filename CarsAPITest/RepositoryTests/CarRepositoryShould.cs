using System;
using System.Linq;
using CarsAPI.Models;
using CarsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace CarsAPITest.RepositoryTests
{
    public class CarRepositoryShould
    {
        protected DbContextOptions options; 

        public CarRepositoryShould()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<SQLCarRepository>();
            builder.UseInMemoryDatabase("test-db").UseInternalServiceProvider(serviceProvider);

            options = builder.Options;
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

    //public class GetAllShould : CarRepositoryShould
    //{
    //    Car CarToCreate = new Car()
    //    {
    //        Make = "Ford",
    //        Model = "F150",
    //        Colour = "Silver",
    //        Year = 2017
    //    };

    //    Car ExpectedCar = new Car()
    //    {
    //        Id = 1,
    //        Make = "Ford",
    //        Model = "F150",
    //        Colour = "Silver",
    //        Year = 2017
    //    };

    //    [Fact]
    //    public void GetAllCarsFromTable()
    //    {
    //        using (var Repo = new SQLCarRepository(options))
    //        {
    //            Repo.Add(CarToCreate);

    //            var GetAllCars = Repo.GetAll();

    //            GetAllCars.Contains(ExpectedCar).ShouldBeTrue();

    //        }
    //    }


    //}
}



using System;
using CarsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
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


        }

        public class CreateShould : CarRepositoryShould
        {
            [Fact]
            public void AddCarToTable()
            {
                using (var Repo = new SQLCarRepository(options))
                {

                }
            }
        }

    }
}



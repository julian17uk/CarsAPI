using System;
using System.Collections.Generic;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Repositories
{
    public class SQLCarRepository : DbContext, ICarRepository
    {
        public virtual DbSet<Car> Cars { get; set; }


        public SQLCarRepository(DbContextOptions<SQLCarRepository> options) : base(options)
        {
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localhost;Database=cardb;Trusted_Connection=True;");

            }
        }

        public IEnumerable<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public Car GetCar(int id)
        {
            throw new NotImplementedException();
        }

        public Car CreateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public Car UpdateCar(int id, Car car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(int id)
        {
            throw new NotImplementedException();
        }

        public SQLCarRepository()
        {
        }
    }
}

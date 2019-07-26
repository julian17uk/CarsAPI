using System;
using System.Collections.Generic;
using System.Linq;
using CarsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Repositories
{
    public class SQLCarRepository : DbContext, ICarRepository
    {
        public virtual DbSet<Car> Cars { get; set; }


        public SQLCarRepository(DbContextOptions options) : base(options)
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
            return Cars;
        }

        public Car GetCar(int id)
        {
                var cars = Cars.Find(id);
                return cars;
           
        }

        public Car CreateCar(Car car)
        {
            Cars.Add(car);
            SaveChanges();
            return car;
        }

        public Car UpdateCar(Car car)
        {
            Cars.Update(car);
            SaveChanges();
            return car;

        }

        public void DeleteCar(int id)
        {
            Car carToDelete = Cars.SingleOrDefault(q => q.Id == id);
            Cars.Remove(carToDelete);
            SaveChanges();
        }

        public SQLCarRepository()
        {
        }
    }
}

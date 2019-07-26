using System;
using System.Collections.Generic;
using CarsAPI.Models;

namespace CarsAPI.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car GetCar(int id);
        Car CreateCar(Car car);
        Car UpdateCar(Car car);
        void DeleteCar(int id);
    }
}

using System;
using System.Collections.Generic;
using CarsAPI.Models;

namespace CarsAPI.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetAll();
        Car GetCar(int id);
        Car CreateCar(int id);
        Car UpdateCar(int id);
        void DeleteCar(int id);

    }
}

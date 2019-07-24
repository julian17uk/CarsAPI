﻿using System;
using System.Collections.Generic;
using CarsAPI.Models;
using CarsAPI.Repositories;

namespace CarsAPI.Services
{
    public class CarService : ICarService
    {
        ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
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
            carRepository.CreateCar(car);

            return null;
        }

        public Car UpdateCar(int id, Car car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
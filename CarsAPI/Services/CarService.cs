using System;
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

            var response = carRepository.GetAll();

            return response;

        }

        public Car GetCar(int id)
        {

            var response = carRepository.GetCar(id);

            if (response == null)
            {
                var message = String.Format($"Car with id {id} not found");

                throw new KeyNotFoundException(message);

            }
            return response;

        }

        public Car CreateCar(Car car)
        {
            Car createdCar = carRepository.CreateCar(car);

            return createdCar;
        }

        public Car UpdateCar(int id, Car car)
        {
            var response = carRepository.UpdateCar(id, car);

            if (response == null)
            {
                var message = String.Format("Car not found");

                throw new KeyNotFoundException(message);
            }

            return response;

        }

        public void DeleteCar(int id)
        {
            GetCar(id);

            carRepository.DeleteCar(id);
        }
    }
}
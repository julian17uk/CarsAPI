using System;
using System.Collections.Generic;
using CarsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CarsAPI.Services;

namespace CarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            var Allcars = carService.GetAll();

            return Ok(Allcars);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                var cars = carService.GetCar(id);
                return Ok(cars);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();

            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Car> Post(Car car)
        {
            var createdcar = carService.CreateCar(car);


            return Ok(createdcar);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, Car updateData)
        {
            try
            {
                Car updatedCar = carService.UpdateCar(id, updateData);

                return Ok(updatedCar);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                carService.DeleteCar(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

using System;
using CarsAPI.Controllers;
using CarsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarsAPITest.ControllerTests
{
    public class CarControllerShould
    {
        private CarsController carController = new CarsController();
        private Car testcar = new Car();



        [Fact]
        public void AddReturnsOK()
        {
            var Response = carController.Post(testcar);
        }
    }

}

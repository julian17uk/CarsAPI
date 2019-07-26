using System;
namespace CarsAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Car)) {
                Car car = obj as Car;
                return car.Id == Id &&
                    car.Make == Make &&
                    car.Model == Model &&
                    car.Colour == Colour &&
                    car.Year == Year;
            }
            return false;
        }

    }


}

using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace DataAccess.Concrete
{

    public class InMemoryCarDal 
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
         {

             new Car { Id = 1, BrandId = 1 , ColorId=1, DailyPrice=2000, Description="VolksWagen Passat", ModelYear=2022 },
             new Car { Id = 2, BrandId = 3 , ColorId=1, DailyPrice=4000, Description="Bmw 5.20i Yeni Kasa", ModelYear=2024 },
             new Car { Id = 3, BrandId = 4 , ColorId=1, DailyPrice=1000, Description= "Renault Clio", ModelYear=2015 }

         };



        }
        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var deleteCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(deleteCar);
        }
        public void Update(Car car)
        {
            var updateCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            updateCar.Id = car.Id;
            updateCar.BrandId = car.BrandId;
            updateCar.Description = car.Description;
            updateCar.DailyPrice = car.DailyPrice;
            updateCar.ModelYear = car.ModelYear;
            updateCar.ColorId = car.ColorId;
            _cars.Add(updateCar);
        }
        public List<Car> GetById()
        {
            foreach (var c in _cars)
            {
                Console.WriteLine(c.Id);
            }
            return _cars;
        }
    }
}

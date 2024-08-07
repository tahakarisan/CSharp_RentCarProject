using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;

namespace ConsoleCarProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager  = new CarManager(new InMemoryCarDal());

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("ARABA KİRALAMA");
                Console.WriteLine("Model yılı: {0}",car.ModelYear);
                Console.WriteLine("Açıklama: {0}",car.Description);
                Console.WriteLine("Günlük Ücret: {0}",car.DailyPrice);
                Console.WriteLine("**********************************");
            }

            Console.ReadLine();
        }
        
    }
}

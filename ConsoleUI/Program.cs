using Business.Abstract;
using Business.Concrete;
using Core.Entities;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

             //AddCarTest();

            //CarAllList();
            //Console.WriteLine("\n");
            //ColorList();
            //Console.WriteLine("\n");
            //UserList();
            //Console.WriteLine("\n");
            //CustomerList();
            //Console.WriteLine("\n");
            //RentalList();
            //Console.WriteLine("\n");
            //AddToRental();

        }
        private static void AddToRental()
        {
            Console.WriteLine("***** KİRALAMA EKLEME *****\n");

            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            string _tempCustomer;
            int _carId, _customerId;
            DateTime _rentDate;
            DateTime? _returnDate;

            Console.Write("Kiralaması yapacak 'Müşteri ID' : ");
            _tempCustomer = Console.ReadLine();
            if (_tempCustomer != null)
            {
                Console.Write("Kiralanacak Araç ID         : ");
                _carId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Kiralama Tarihi[aa/gg/yyyy] : ");
                _rentDate = Convert.ToDateTime(Console.ReadLine());
                _returnDate = null;
                _customerId = Convert.ToInt32(_tempCustomer);

                Rental rental = new Rental
                {
                    CarId = _carId,
                    CustomerId = _customerId,
                    RentDate = _rentDate,
                    ReturnDate = _returnDate
                };

                var result = rentalManager.Add(rental);
                Console.WriteLine(result.Message);

            }
        }

        private static void RentalList()
        {
            Console.WriteLine("***** KİRALAMA LİSTESİ *****\n");
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //rentalManager.Add(new Rental {Id=3,CarId=2,CustomerId=2,RentDate=DateTime.Now, ReturnDate=null });
            foreach (var rental in rentalManager.GetAll().Data)
            {
                Console.WriteLine("Kiralama Id : "+rental.Id + " " +"Customer Id : "+rental.CustomerId+" "+"Car Id : "+rental.CarId +" " + "Kiralama Tarihi :"+rental.RentDate + " " + "Geri geliş tarihi: "+rental.ReturnDate);
            }
        }

        //private static void UserList()
        //{
        //    Console.WriteLine("***** KULLANICILARIN LİSTESİ *****\n");
        //    UserManager usermanager = new UserManager(new EfUserDal());
        //    foreach (var user in usermanager.GetAll().Data)
        //    {
        //        Console.WriteLine(user.FirstName + " " + user.LastName + " " + user.Email);
        //    }
        //}

        private static void ColorList()
        {
            Console.WriteLine("***** RENKLERİN LİSTESİ *****\n");
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorId + " - " + color.ColorName);
            }
        }

        private static void CustomerList()
        {
            Console.WriteLine("***** MÜŞTERİLERİN LİSTESİ***** ");
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.CustomerId + " - " + customer.CompanyName);
            }
        }

        private static void CarAllList()
        {
            Console.WriteLine("***** ARABALARIN LİSTESİ *****\n");
            CarManager carManager = new CarManager(new EfCarDal());

            var result = carManager.GetAll();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Car ID: "+car.CarId + " --- " +car.Description+ " "+ "Brand ID: "+car.BrandId+"  Günlük Ücret: "+ car.DailyPrice+" Color ID : "+car.ColorId);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void AddCarTest()
        {
           
            ICarService carManager = new CarManager(new EfCarDal());
           carManager.Add(new Car { BrandId = 8, ColorId = 1, DailyPrice = 300, ModelYear = "2006", Description = "Nissan" });
            
   
            var result = carManager.GetAll();
            if (result.Success)
            {
                foreach (var car in result.Data)
                {

                    Console.WriteLine(car.Description);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
           
        }
    }
}

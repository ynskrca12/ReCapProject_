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

            // AddCarTest();

            CarAllList();
            Console.WriteLine("\n");
            ColorList();
            Console.WriteLine("\n");
            UserList();
            Console.WriteLine("\n");
            CustomerList();
            Console.WriteLine("\n");
            RentalList();
            Console.WriteLine("\n");
            AddToRental();

        }
        private static void AddToRental()
        {
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
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //rentalManager.Add(new Rental {Id=3,CarId=2,CustomerId=2,RentDate=DateTime.Now, ReturnDate=null });
            foreach (var rental in rentalManager.GetAll().Data)
            {
                Console.WriteLine(rental.Id + " " +rental.CustomerId+" "+rental.CarId +" " + rental.RentDate + " " + rental.ReturnDate);
            }
        }

        private static void UserList()
        {
            UserManager usermanager = new UserManager(new EfUserDal());
            foreach (var user in usermanager.GetAll().Data)
            {
                Console.WriteLine(user.FirstName + " " + user.LastName + " " + user.Email);
            }
        }

        private static void ColorList()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            foreach (var color in colorManager.GetAll())
            {
                Console.WriteLine(color.ColorId + " - " + color.ColorName);
            }
        }

        private static void CustomerList()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.CustomerId + " - " + customer.CompanyName);
            }
        }

        private static void CarAllList()
        {
            CarManager carManager = new CarManager(new EfCarDal(), new RuleManager());

            var result = carManager.GetAll();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Car ID: "+car.CarId + " --- " +car.Description + "Brand ID: "+car.BrandId+"  Günlük Ücret: "+ car.DailyPrice+" Color ID : "+car.ColorId);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void AddCarTest()
        {
            IRuleService<IEntity> rulesService = new RuleManager();

            //carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 300, ModelYear = "2006", Description = "AUDI" });
            Car car1 = new Car
            {   
                BrandId = 2,
                ColorId = 2,
                DailyPrice = 100,
                ModelYear = "2017",
                Description = "Tesla"
            };

            ICarService carManager = new CarManager(new EfCarDal(), new RuleManager());

            carManager.Add(car1);
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

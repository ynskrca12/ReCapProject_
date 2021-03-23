using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentDal;

        public RentalManager(IRentalDal rentaldal)
        {
            _rentDal = rentaldal;
        }
        public IResult Add(Rental rental)
        {
            var result = CheckReturnDate(rental.CarId);
            if (!result.Success)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            _rentDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult CheckReturnDate(int carId)
        {
            var result = _rentDal.GetCarRentalDetails(r => r.CarId == carId && r.ReturnDate == null);
            if (result.Count>0)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentDal.GetAll(), Messages.RentalotListed);
        }

        public IDataResult<List<CarRentalDetailDto>> GetRentalDetailDto()
        {
            return new SuccessDataResult<List<CarRentalDetailDto>>(_rentDal.GetCarRentalDetails());
        }

        public IResult UpdateReturnDate(Rental rental)
        {
            _rentDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Uptdate(Rental rental)
        {
            var result = _rentDal.GetAll(r => r.Id == rental.Id);
            var updateRental = result.LastOrDefault();

            if (updateRental.ReturnDate!=null)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }
            updateRental.ReturnDate = rental.ReturnDate;
            _rentDal.Update(updateRental);
            return new SuccessResult(Messages.RentalUpdated);
        }
    }
}

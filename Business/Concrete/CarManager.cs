using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IRuleService<IEntity> _rules;
       
        public CarManager(ICarDal carDal, IRuleService<IEntity> ruleService)
        {
            _carDal = carDal;
            _rules = ruleService;

        }


        public IResult Add(Car entity)
        {
            //if (entity.Description.Length < 2)
            //{
            //    return new ErrorResult(Messages.CarNameInVAlid);
            //}

            _rules.MinNameRule(entity);
            _rules.MinPriceRule(entity);
            _carDal.Add(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(),Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>( _carDal.GetCarDetails());
        }
    }
}

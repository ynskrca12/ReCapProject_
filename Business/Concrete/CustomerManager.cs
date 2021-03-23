﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            if (customer.UserId != 0)
            {
                _customerDal.Add(customer);
                return new SuccessResult(Messages.CustomerAdded);
            }
            return new ErrorResult(Messages.CustomerNotAdded);
        }

        public IResult Delete(Customer customer)
        {
            if (customer.CustomerId!=0)
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.CustomerDeleted);
            }
            return new ErrorResult(Messages.CustomerNotDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomerListed);

        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IResult Uptade(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
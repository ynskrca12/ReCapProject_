using Core.Utilities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Uptade(Customer customer);
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}

﻿using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Delet(User user);
        IResult Update(User user);
        IDataResult<List<User>> GetAll();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestingSystem.BusinessModel.Service
{
    public interface IEntityService<TDTO> where TDTO : class, new()
    {
        IEnumerable<TDTO> GetAll();
        IEnumerable<TDTO> FindBy(Expression<Func<TDTO, bool>> predicate);
        TDTO Get(int id);
        TDTO AddOrUpdate(TDTO obj);
        TDTO Delete(TDTO obj);
        Task<IEnumerable<TDTO>> GetAllAsync();
        Task<IEnumerable<TDTO>> FindByAsync(Expression<Func<TDTO, bool>> predicate);
        Task<TDTO> GetAsync(int id);
        Task<TDTO> AddOrUpdateAsync(TDTO obj);
        Task<TDTO> DeleteAsync(TDTO obj);
    }
}

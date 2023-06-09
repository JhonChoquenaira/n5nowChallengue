﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5NowChallengue.DataService.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}

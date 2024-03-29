﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteComposite(int firstId, int secondId);
    }
}

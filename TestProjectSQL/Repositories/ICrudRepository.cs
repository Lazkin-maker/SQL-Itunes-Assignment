﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSQL.Repositories
{
    public interface ICrudRepository<T, Id>
    {
        IEnumerable<T> GetAll();
        T GetById(Id id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Id id);
    }
}


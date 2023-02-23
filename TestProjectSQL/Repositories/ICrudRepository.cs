﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSQL.Models;

namespace TestProjectSQL.Repositories
{
    public interface ICrudRepository<T, Id>
    {
        /// <summary>
        /// Retrieves all instances from the database.
        /// </summary>
        /// <returns>returns IEnumerable containing instances</returns>
        IEnumerable<T> GetAll();
        
        
        /// <summary>
        /// Retrieves a particular instance from the database by its ID.
        /// </summary>
        /// <param name="id">unique Id for passed instance</param>
        /// <returns>returns instance of T</returns>
        T GetById(Id id);

        /// <summary>
        /// Inserts a new row into the database based on the paramter.
        /// </summary>
        /// <param name="entity">instance of T</param>
        /// <exception cref="Exception"></exception>
        void Add(T entity);


        /// <summary>
        /// Updates an existing row based on the provided parameters.
        /// </summary>
        /// <param name="entity">instance of type T</param>
        /// <exception cref="Exception"></exception>
        void Update(T entity);
    }
}


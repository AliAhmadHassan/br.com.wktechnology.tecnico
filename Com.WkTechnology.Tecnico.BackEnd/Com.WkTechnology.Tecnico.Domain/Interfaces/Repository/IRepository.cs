using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Domain.Interfaces.Repository 
{
    /// <summary>
    /// The basic CRUD.
    /// </summary>
    /// <typeparam name="T">Where <c>T</c> is object, entity (inherited from <c>BaseEntity</c>).</typeparam>
    /// <typeparam name="U"><c>U</c> is the type referring to the primary key</typeparam>
    public interface IRepository<T, U> where T: BaseEntity 
    {
        /// <summary>
        /// Method to insert record into the database.
        /// </summary>
        /// <param name="entity">A record of type <c>T</c></param>
        /// <returns>The object of type <c>T</c> with its respective <c>ID</c> field</returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Method to alter record data into the database.
        /// </summary>
        /// <param name="entity">A record of type <c>T</c></param>
        /// <returns>The object of type <c>T</c> with its respective <c>ID</c> field</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Method to remove record into the database.
        /// </summary>
        /// <param name="id">The record <c>ID</c> field, being of type <c>U</c></param>
        Task DeleteAsync(U id);

        /// <summary>
        /// Method to alter record data into the database.
        /// </summary>
        /// <param name="id">The record <c>ID</c> field, being of type <c>U</c></param>
        /// <returns>The object of type <c>T</c> with its respective <c>ID</c> field</returns>
        Task<T> SelectByIdAsync(U id);

        /// <summary>
        /// returns records from the <c>T</c> table.
        /// </summary>
        /// <param name="isLazyLoading">If it is to bring the <c>T</c> data with your dependents</param>
        /// <param name="rowsPerPage">Pagination: Number of records per page</param>
        /// <param name="pageNumber">Pagination: Page number</param>
        /// <returns>List of <c>T</c></returns>
        Task<IEnumerable<T>> SelectAsync();
        
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Domain.Interfaces.Repository 
{
    /// <summary>
    /// Class responsible for interacting with the database for the <c>Product</c> object.
    /// </summary>
    public interface IProductRepository: IRepository<ProductEntity, Int64>
    {
        /// <summary>
        /// Returns all records from the <c>Product</c> table, filtered through the idCategory field.
        /// </summary>
        /// <param name="idCategory"></param>
        /// <returns>List of <c>Product</c></returns>
        Task<IEnumerable<ProductEntity>> SelectByIdCategoryAsync(Int64 idCategory);
    }
}

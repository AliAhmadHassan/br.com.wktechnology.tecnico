using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Domain.Interfaces.Services 
{
    /// <summary>
    /// Business rules about <c>Product</c>.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Business rules for insert a new <c>Product</c>.
        /// </summary>
        /// <param name="entity">Entity object of type <c>Product</c> </param>
        /// <returns>The <c>Product</c> with primary key</returns>
        Task<ResponseDTO> InsertAsync(ProductDTO entity);

        /// <summary>
        /// Business rules for alter a <c>Product</c> data.
        /// </summary>
        /// <param name="entity">Entity object of type <c>Product</c> </param>
        /// <returns>The <c>Product</c></returns>
        Task<ResponseDTO> UpdateAsync(ProductDTO entity);

        /// <summary>
        /// Business rules for remove a <c>Product</c> register.
        /// </summary>
        /// <param name="id">Primary key about <c>Product</c> </param>
        Task DeleteAsync (Int64 id);

        /// <summary>
        /// Returns the record in the <c>Product</c> table of the respective Id.
        /// </summary>
        /// <param name="id">Primary key about <c>Product</c> </param>
        /// <returns>The <c>Product</c></returns>
        Task<ResponseDTO> SelectByIdAsync(Int64 id);

        /// <summary>
        /// returns records from the <c>Product</c> table.
        /// </summary>
        /// <returns>List of <c>Product</c></returns>
        Task<ResponseDTO> SelectAsync();
        /// <summary>
        /// Returns all records from the <c>Product</c> table, filtered through the idCategory field.
        /// </summary>

        /// <returns>List of <c>Product</c></returns>
        Task<ResponseDTO> SelectByIdCategoryAsync(Int64 idCategory);
    }
}

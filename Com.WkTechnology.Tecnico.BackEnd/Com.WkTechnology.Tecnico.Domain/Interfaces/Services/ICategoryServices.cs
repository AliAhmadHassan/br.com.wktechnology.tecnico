using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Domain.Interfaces.Services 
{
    /// <summary>
    /// Business rules about <c>Category</c>.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Business rules for insert a new <c>Category</c>.
        /// </summary>
        /// <param name="entity">Entity object of type <c>Category</c> </param>
        /// <returns>The <c>Category</c> with primary key</returns>
        Task<ResponseDTO> InsertAsync(CategoryDTO entity);

        /// <summary>
        /// Business rules for alter a <c>Category</c> data.
        /// </summary>
        /// <param name="entity">Entity object of type <c>Category</c> </param>
        /// <returns>The <c>Category</c></returns>
        Task<ResponseDTO> UpdateAsync(CategoryDTO entity);

        /// <summary>
        /// Business rules for remove a <c>Category</c> register.
        /// </summary>
        /// <param name="id">Primary key about <c>Category</c> </param>
        Task DeleteAsync (Int64 id);

        /// <summary>
        /// Returns the record in the <c>Category</c> table of the respective Id.
        /// </summary>
        /// <param name="id">Primary key about <c>Category</c> </param>
        /// <returns>The <c>Category</c></returns>
        Task<ResponseDTO> SelectByIdAsync(Int64 id);

        /// <summary>
        /// returns records from the <c>Category</c> table.
        /// </summary>
        /// <returns>List of <c>Category</c></returns>
        Task<ResponseDTO> SelectAsync();
        
    }
}

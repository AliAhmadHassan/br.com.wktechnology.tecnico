using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Domain.Interfaces.Repository 
{
    /// <summary>
    /// Class responsible for interacting with the database for the <c>Category</c> object.
    /// </summary>
    public interface ICategoryRepository: IRepository<CategoryEntity, Int64>
    {
        
    }
}

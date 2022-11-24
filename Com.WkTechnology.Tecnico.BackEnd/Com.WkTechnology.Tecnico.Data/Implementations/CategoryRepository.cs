using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Text;
using Dapper;
using Com.WkTechnology.Tecnico.Domain.Entities;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Com.WkTechnology.Tecnico.Data.Implementations
{


    public class CategoryRepository: Domain.Interfaces.Repository.ICategoryRepository
    {

        Domain.Settings.IDatabaseSettings databaseSettings;

        public CategoryRepository(Domain.Settings.IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings;
        }


        public Task DeleteAsync(Int64 id)
        {
            Task result;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                result = conn.ExecuteAsync("spd_category", new { id_in = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<CategoryEntity> InsertAsync(CategoryEntity entity)
        {
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    name_in = entity.Name,
                    description_in = entity.Description
                };

                entity.Id = await conn.ExecuteScalarAsync<int>("spi_category", values, commandType: System.Data.CommandType.StoredProcedure);
            }
            return entity;
        }
        public async Task<IEnumerable<CategoryEntity>> SelectAsync()
        {
            IEnumerable<CategoryEntity> entities = null;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {};
                entities = await conn.QueryAsync<CategoryEntity>("sps_category", param: values, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            return entities;
        }
        public async Task<CategoryEntity> SelectByIdAsync(Int64 id)
        {
            CategoryEntity entity = null;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    id_in = id
                };

                entity = (await conn.QueryAsync<CategoryEntity>("sps_category_by_id", param: values, 
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
            }
            return entity;
        }
        public async Task<CategoryEntity> UpdateAsync(CategoryEntity entity)
        {
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    name_in = entity.Name,
                    description_in = entity.Description,
                    id_in = entity.Id
                };

                entity.Id = await conn.ExecuteScalarAsync<int>("spu_category", values, commandType: System.Data.CommandType.StoredProcedure);
            }
            return entity;
        }
    }
}
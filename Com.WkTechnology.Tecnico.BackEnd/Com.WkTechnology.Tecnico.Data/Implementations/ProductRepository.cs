using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Com.WkTechnology.Tecnico.Domain.Entities;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Com.WkTechnology.Tecnico.Data.Implementations
{


    public class ProductRepository: Domain.Interfaces.Repository.IProductRepository
    {

        Domain.Settings.IDatabaseSettings databaseSettings;

        public ProductRepository(Domain.Settings.IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings;
        }


        public Task DeleteAsync(Int64 id)
        {
            Task result;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                result = conn.ExecuteAsync("spd_product", new { id_in = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            return result;
        }

        public async Task<ProductEntity> InsertAsync(ProductEntity entity)
        {
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    name_in = entity.Name,
                    description_in = entity.Description,
                    unit_price_in = entity.UnitPrice,
                    amount_in = entity.Amount,
                    idcategory_in = entity.Category != null? (long?)entity.Category.Id : null
                };

                entity.Id = await conn.ExecuteScalarAsync<int>("spi_product", values, commandType: System.Data.CommandType.StoredProcedure);
            }
            return entity;
        }
        public async Task<IEnumerable<ProductEntity>> SelectAsync()
        {
            IEnumerable<ProductEntity> entities = null;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {};
                entities = await conn.QueryAsync<ProductEntity, CategoryEntity, ProductEntity>("sps_product", param: values, map: (product, category)=>{
                        product.Category = category;
                                    return product;
                    }, 
                    splitOn: "product_split",
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            return entities;
        }
        public async Task<ProductEntity> SelectByIdAsync(Int64 id)
        {
            ProductEntity entity = null;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    id_in = id
                };

                entity = (await conn.QueryAsync<ProductEntity, CategoryEntity, ProductEntity>("sps_product_by_id", param: values, map: (product, category)=>{
                        product.Category = category;
                                    return product;
                    }, 
                    splitOn: "product_split",
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
            }
            return entity;
        }
        public async Task<IEnumerable<ProductEntity>> SelectByIdCategoryAsync(Int64 idCategory)
        {
        
            IEnumerable<ProductEntity> entities = null;
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    idcategory_in =  idCategory,
                };

                entities = await conn.QueryAsync<ProductEntity, CategoryEntity, ProductEntity>("sps_product_by_idcategory", param: values, map: (product, category)=>{
                        product.Category = category;
                                    return product;
                    }, 
                    splitOn: "product_split",
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            return entities;

        }
        public async Task<ProductEntity> UpdateAsync(ProductEntity entity)
        {
            using (MySqlConnection conn = new MySqlConnection(databaseSettings.ConnectionString))
            {
                var values = new {
                    name_in = entity.Name,
                    description_in = entity.Description,
                    unit_price_in = entity.UnitPrice,
                    amount_in = entity.Amount,
                    idcategory_in = entity.Category.Id,
                    id_in = entity.Id
                };

                entity.Id = await conn.ExecuteScalarAsync<int>("spu_product", values, commandType: System.Data.CommandType.StoredProcedure);
            }
            return entity;
        }
    }
}
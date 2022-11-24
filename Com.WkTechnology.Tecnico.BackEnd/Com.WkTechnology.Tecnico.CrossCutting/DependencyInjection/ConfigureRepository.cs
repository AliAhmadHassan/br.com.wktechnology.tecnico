using Com.WkTechnology.Tecnico.Data.Implementations;
using Com.WkTechnology.Tecnico.Domain.Interfaces;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dapper.FluentMap;

namespace Com.WkTechnology.Tecnico.CrossCutting.DependencyInjection
{
    public class ConfigureRepository 
    {
        public static void ConfigureDependenciesRepository (IServiceCollection serviceCollection) {

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new Data.DataMapper.CategoryMap());
                config.AddMap(new Data.DataMapper.ProductMap());
            });
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository> ();
            serviceCollection.AddScoped<IProductRepository, ProductRepository> ();
        }
    }
}
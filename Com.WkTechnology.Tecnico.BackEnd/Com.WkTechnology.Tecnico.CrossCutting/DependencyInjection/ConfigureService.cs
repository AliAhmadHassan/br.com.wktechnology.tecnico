using AutoMapper;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using Com.WkTechnology.Tecnico.Service.Mapper;
using Com.WkTechnology.Tecnico.Service.Services;
using Com.WkTechnology.Tecnico.Service.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Com.WkTechnology.Tecnico.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService (IServiceCollection serviceCollection) {
            serviceCollection.AddScoped<ICategoryService, CategoryService> ();
            serviceCollection.AddScoped<IProductService, ProductService> ();
            serviceCollection.AddTransient<IValidator<CategoryDTO>, CategoryValidation>();
            serviceCollection.AddTransient<IValidator<ProductDTO>, ProductValidation>();
            var configMapper = new MapperConfiguration(x => {
                x.AddProfile(new DTOToEntityProfile());
            });
            IMapper mapper = configMapper.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
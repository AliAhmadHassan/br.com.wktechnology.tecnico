using AutoMapper;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.Entities;

namespace Com.WkTechnology.Tecnico.Service.Mapper
{
    public class DTOToEntityProfile: Profile
    {
        public DTOToEntityProfile()
        {
            CreateMap<CategoryDTO, CategoryEntity>().ReverseMap();
            CreateMap<ProductDTO, ProductEntity>().ReverseMap();
        }
    }
}

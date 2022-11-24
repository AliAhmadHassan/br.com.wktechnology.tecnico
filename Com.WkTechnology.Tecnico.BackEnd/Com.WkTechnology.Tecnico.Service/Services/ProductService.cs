using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;
using Com.WkTechnology.Tecnico.Domain.Entities;
using Com.WkTechnology.Tecnico.Domain.Interfaces;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using System.Linq;
using FluentValidation;
using AutoMapper;

namespace Com.WkTechnology.Tecnico.Service.Services {
    public class ProductService : IProductService {
        private Domain.Interfaces.Repository.IProductRepository _repository;
        private readonly IValidator<ProductDTO> _validator;
        private readonly IMapper _mapper;
        public ProductService (Domain.Interfaces.Repository.IProductRepository repository,
            IValidator<ProductDTO> validator,
            IMapper mapper) {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public Task DeleteAsync (Int64 id) {
            return _repository.DeleteAsync(id);
        }

        public async Task<ResponseDTO> SelectByIdAsync(Int64 id) {
            var product = await _repository.SelectByIdAsync(id);
            ResponseDTO response = new ResponseDTO();
            response.TotalRows = product == null ? 0 : 1;
            response.HTTPStatusCode = 200;
            response.Data = product;
            response.Message = "ok";
            response.Successfully = true;

            return response;
        }

        public async Task<ResponseDTO> SelectAsync() {
            IEnumerable<ProductEntity> products = await _repository.SelectAsync();
            ResponseDTO response = new ResponseDTO();
            response.TotalRows = products.Count();
            response.HTTPStatusCode = 200;
            response.Data = products;
            response.Message = "ok";
            response.Successfully = true;
            return response;
        }

        public async Task<ResponseDTO> InsertAsync(ProductDTO entity) {
            var resultValidator = _validator.Validate(entity);
            if (!resultValidator.IsValid)
            {
                return new ResponseDTO()
                {
                    TotalRows = 1,
                    HTTPStatusCode = 400,
                    Successfully = false,
                    Message = String.Join(',', resultValidator.Errors)
                };
            }

            var result = await _repository.InsertAsync(_mapper.Map<ProductEntity>(entity));

            ResponseDTO response = new ResponseDTO()
            {
                TotalRows = 1,
                HTTPStatusCode = 200,
                Data = result,
                Successfully = true,
                Message = "1 row inserted"
            };

            return response;
        }

        public async Task<ResponseDTO> UpdateAsync(ProductDTO entity) {
            var resultValidator = _validator.Validate(entity);
            if (!resultValidator.IsValid)
            {
                return new ResponseDTO()
                {
                    TotalRows = 1,
                    HTTPStatusCode = 400,
                    Successfully = false,
                    Message = String.Join(',', resultValidator.Errors)
                };
            }

            var result = await _repository.UpdateAsync(_mapper.Map<ProductEntity>(entity));

            ResponseDTO response = new ResponseDTO()
            {
                TotalRows = 1,
                HTTPStatusCode = 200,
                Data = result,
                Successfully = true,
                Message = "1 row updated"
            };

            return response;
        }

        public async Task<ResponseDTO> SelectByIdCategoryAsync(Int64 idCategory)
        {
            IEnumerable<ProductEntity> products = await _repository.SelectByIdCategoryAsync(idCategory);
            ResponseDTO response = new ResponseDTO();
            response.TotalRows = products.Count();
            response.HTTPStatusCode = 200;
            response.Data = products;
            response.Message = "ok";
            response.Successfully = true;
            return response;

        }
                    

    }
}

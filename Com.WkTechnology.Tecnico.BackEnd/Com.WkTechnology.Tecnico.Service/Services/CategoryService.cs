using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;
using Com.WkTechnology.Tecnico.Domain.Entities;
using Com.WkTechnology.Tecnico.Domain.Interfaces;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using FluentValidation;
using System.Linq;

namespace Com.WkTechnology.Tecnico.Service.Services {
    public class CategoryService : ICategoryService {
        private Domain.Interfaces.Repository.ICategoryRepository _repository;
        private readonly IValidator<CategoryDTO> _validator;
        private readonly IMapper _mapper;
        public CategoryService(Domain.Interfaces.Repository.ICategoryRepository repository,
            IValidator<CategoryDTO> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public Task DeleteAsync (Int64 id) {
            return _repository.DeleteAsync(id);
        }

        public async Task<ResponseDTO> SelectByIdAsync(Int64 id) {
            var category = await _repository.SelectByIdAsync(id);
            ResponseDTO response = new ResponseDTO();
            response.TotalRows = category == null ? 0 : 1;
            response.HTTPStatusCode = 200;
            response.Data = category;
            response.Message = "ok";
            response.Successfully = true;
            return response;
        }

        public async Task<ResponseDTO> SelectAsync() {
            IEnumerable<CategoryEntity> categories = await _repository.SelectAsync();
            ResponseDTO response = new ResponseDTO();
            response.TotalRows = categories.Count();
            response.HTTPStatusCode = 200;
            response.Data = categories;
            response.Message = "ok";
            response.Successfully = true;
            return response;
        }

        public async Task<ResponseDTO> InsertAsync(CategoryDTO entity) {
            var resultValidator = _validator.Validate(entity);
            if (!resultValidator.IsValid) {
                return new ResponseDTO()
                {
                    TotalRows = 1,
                    HTTPStatusCode = 400,
                    Successfully = false,
                    Message = String.Join(',',resultValidator.Errors)
                };
            }

            var result = await _repository.InsertAsync(_mapper.Map<CategoryEntity>(entity));

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

        public async Task<ResponseDTO> UpdateAsync(CategoryDTO entity) {
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

            var result = await _repository.UpdateAsync(_mapper.Map<CategoryEntity>(entity));

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


    }
}

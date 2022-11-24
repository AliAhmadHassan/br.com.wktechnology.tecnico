using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Com.WkTechnology.Tecnico.Domain.Entities;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;

namespace Com.WkTechnology.Tecnico.Application.Controllers
{

    /// <summary>
    /// Endpoint of <c>Category</c>.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private ICategoryService _service;
        public CategoryController(ICategoryService service, ILogger<CategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Method to return last 1000 records of Address repository
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>IEnumerable<AddressEntity></returns>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryEntity>))]
        public async Task<ActionResult> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", ModelState);
                return BadRequest(ModelState); //400 bad request - solicitação inválida
            }

            try
            {
                ResponseDTO response = await _service.SelectAsync();

                if (response.Successfully)
                {
                    if(response.TotalRows == 0)
                    {
                        _logger.LogInformation($"GetAll() Is Successfully but not found registers", response);
                        return NotFound(response);
                    }
                    else 
                    {
                        _logger.LogInformation($"GetAll() Is Successfully", response);
                        return Ok(response);
                    }
                    
                }
                else
                {
                    _logger.LogInformation($"GetAll() Is not Successfully", response);
                    return BadRequest(response);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to CategoryController.GetAll()");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Method to return the record of <c>Category</c>, filtered by <c>ID</c> field.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>CategoryEntity</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(CategoryEntity))]
        public async Task<ActionResult> GetAsync(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                ResponseDTO response = await _service.SelectByIdAsync(id);
                if(response == null || !response.Successfully || response.TotalRows == 0)
                {
                    _logger.LogInformation($"Get({id}) Is Successfully but not found", ModelState);
                    return NotFound(response);
                } 
                else 
                {
                    _logger.LogInformation($"Get({id}) Is Successfully", ModelState);
                    return Ok(response);
                }
                
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to CategoryController.Get({id})");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }


        /// <summary>
        /// Method insert record of <c>Category</c> on Data Base.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>CategoryEntity</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(CategoryEntity))]
        public async Task<ActionResult> PostAsync([FromBody] CategoryDTO ov)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", ov);
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _service.InsertAsync(ov);
                if (result != null && result.Successfully)
                {
                    _logger.LogInformation($"category created successfully.", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"CategorysController.Post() wad BadRequest", ov);
                    return BadRequest(result);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to CategoryController.Post()", ov);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Method alter record data of <c>Category</c> on Data Base.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>CategoryEntity</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(CategoryEntity))]
        public async Task<ActionResult> PutAsync([FromBody] CategoryDTO ov)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", ov);
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateAsync(ov);
                if (result != null && result.Successfully)
                {
                    _logger.LogInformation($"category updated successfully.", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"CategoryController.Post() wad BadRequest", ov);
                    return BadRequest(result);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to CategoryController.Put()", ov);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Method remove record of <c>Category</c> on Data Base with <c>ID</c>.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>CategoryEntity</returns>
        
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteAsync(Int64 id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", id);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation($"category deleted successfully.", id);
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error to CategoryController.Delete({id})");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}


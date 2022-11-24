using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Com.WkTechnology.Tecnico.Domain.Entities;
using Com.WkTechnology.Tecnico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Com.WkTechnology.Tecnico.Domain.DTO.Product;
using Com.WkTechnology.Tecnico.Domain.DTO.Response;

namespace Com.WkTechnology.Tecnico.Application.Controllers
{

    /// <summary>
    /// Endpoint of <c>Product</c>.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductService _service;
        public ProductController(IProductService service, ILogger<ProductController> logger)
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductEntity>))]
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
                if(!response.Successfully)
                {
                    _logger.LogError($"Error to ProductController.GetAll() Error {response.Message}");
                    return BadRequest(response);
                } 
                else if(response.TotalRows == 0)
                {
                    _logger.LogInformation($"GetAll() Is Successfully but not found records", ModelState);
                    return NotFound(response);
                }
                else { 
                    _logger.LogInformation($"GetAll() Is Successfully", ModelState);
                    return Ok(response);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.GetAll()");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Method to return the record of <c>Product</c>, filtered by <c>ID</c> field.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>ProductEntity</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(ProductEntity))]
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
                if (response.Successfully) { 
                    _logger.LogInformation($"Get({id}) Is Successfully", ModelState);
                    return Ok(response);
                }
                else if (response.TotalRows == 0)
                {
                    _logger.LogInformation($"Get({id}) Is Successfully but not found", ModelState);
                    return NotFound(response);
                }
                else
                {
                    _logger.LogInformation($"Get({id}) Error {response.Message}", ModelState);
                    return BadRequest(response);
                }
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.Get({id})");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }


        /// <summary>
        /// Method to return the record of <c>Product</c>, filtered by <c>ID</c> field.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>ProductEntity</returns>
        [HttpGet]
        [Route("{idCategory}/idcategory")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(ProductEntity))]
        public async Task<ActionResult> GetByIdCategoryAsync(Int64 idCategory)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState Is Invalid", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                ResponseDTO response = await _service.SelectByIdCategoryAsync(idCategory);
                if (!response.Successfully)
                {
                    _logger.LogError($"Error to ProductController.GetByIdCategoryAsync({idCategory}) Error {response.Message}");
                    return BadRequest(response);
                }
                else if (response.TotalRows == 0)
                {
                    _logger.LogInformation($"GetByIdCategoryAsync({idCategory}) Is Successfully but not found records", ModelState);
                    return NotFound(response);
                }
                else
                {
                    _logger.LogInformation($"GetByIdCategoryAsync({idCategory}) Is Successfully", ModelState);
                    return Ok(response);
                }
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.GetByIdCategoryAsync({idCategory})");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        /// <summary>
        /// Method insert record of <c>Product</c> on Data Base.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>ProductEntity</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(ProductEntity))]
        public async Task<ActionResult> PostAsync([FromBody] ProductDTO ov)
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
                    _logger.LogInformation($"product created successfully.", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"ProductsController.Post() wad BadRequest", ov);
                    return BadRequest(result);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.Post()", ov);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Method alter record data of <c>Product</c> on Data Base.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>ProductEntity</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(200, Type = typeof(ResponseDTO))]
        public async Task<ActionResult> PutAsync([FromBody] ProductDTO ov)
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
                    _logger.LogInformation($"product updated successfully.", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"ProductController.Post() wad BadRequest", ov);
                    return BadRequest(result);
                }

            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.Put()", ov);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Method remove record of <c>Product</c> on Data Base with <c>ID</c>.
        /// </summary>
        /// <exception cref="400">BadRequest</exception>
        /// <exception cref="401">Unauthorized</exception>
        /// <exception cref="404">Not Found</exception>
        /// <exception cref="500">Internal Server Error</exception>
        /// <returns>ProductEntity</returns>
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
                _logger.LogInformation($"product deleted successfully.", id);
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, $"Error to ProductController.Delete({id})");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}


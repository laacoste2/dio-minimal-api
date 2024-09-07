using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Context;
using Products.Models;
using Products.Services;
using Products.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Products.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsManagerController : ControllerBase
    {
        private readonly string[] _allowedNationalities = { "BR", "DE", "US", "FR", "AO" };

        private IProductsManagerService _service;

        public ProductsManagerController(IProductsManagerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var entities = await _service.GetProductsServiceAsync();

            return entities;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            var entity = await _service.GetProductByIdServiceAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProductAsync(ProductDTO product)
        {
            var entity = await _service.PostProductServiceAsync(product);

            if (!_allowedNationalities.Contains(product.Nationality))
            {
                return BadRequest("Nationality: Acronym not allowed.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetProductById", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProductAsync(Guid id, ProductDTO product)
        {
            var entity = await _service.PutProductServiceAsync(id, product);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProductAsync(Guid id)
        {
            var entity = await _service.DeleteProductServiceAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

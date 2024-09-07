using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Products.Context;
using Products.Models;
using Products.DTOs;

namespace Products.Services
{
    public class ProductsManagerService : IProductsManagerService
    {
        public ProductsContext _context;

        public ProductsManagerService(ProductsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsServiceAsync()
        {
            var entities = await _context.Products.ToListAsync();

            return entities;
        }

        public async Task<Product> GetProductByIdServiceAsync(Guid id)
        {
            var entity = await _context.Products.FindAsync(id);

            return entity!;
        }

        public async Task<Product> PostProductServiceAsync(ProductDTO product)
        {
            var entity = new Product(
                product.Name,
                product.Description,
                product.Price,
                product.Nationality
            );

            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> PutProductServiceAsync(Guid id, ProductDTO product)
        {
            var entity = await _context.Products.FindAsync(id);

            entity!.Name = product.Name;
            entity.Description = product.Description;
            entity.Price = product.Price;
            entity.Nationality = product.Nationality;

            await _context.SaveChangesAsync();

            return entity!;
        }

        public async Task<Product> DeleteProductServiceAsync(Guid id)
        {
            var entity = await _context.Products.FindAsync(id);

            _context.Products.Remove(entity!);
            await _context.SaveChangesAsync();

            return entity!;
        }

    }
}

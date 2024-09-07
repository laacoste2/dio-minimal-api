using Products.DTOs;
using Products.Models;

namespace Products.Services
{
    public interface IProductsManagerService
    {
        public Task<IEnumerable<Product>> GetProductsServiceAsync();
        public Task<Product> GetProductByIdServiceAsync(Guid id);
        public Task<Product> PostProductServiceAsync(ProductDTO product);
        public Task<Product> PutProductServiceAsync(Guid id, ProductDTO product);
        public Task<Product> DeleteProductServiceAsync(Guid id);
    }
}

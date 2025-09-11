using MyWebApp.DomainLayer.Entities;

namespace MyWebApp.ApplicationLayer.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Product?> UpdateAsync(Product product);
    }
}

using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ProductRepository : BaseRepository<Entities.Product>, IProductRepository
{
    private readonly MyDbContext DbSet;
    public ProductRepository(MyDbContext context) : base(context)
    {
        DbSet = context;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await DbSet.Products.Where(x=>x.Isdeleted == false).Include(y=>y.Category).ToListAsync();
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        return await DbSet.Products.Where(x=>x.Isdeleted == false).Include(y=>y.Category).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteProductById(Guid productId)
    {
        var product = await DbSet.Products.FindAsync(productId);
        if (product == null)
        {
            return false;
        }

        product.Isdeleted = true;
        var result = await DbSet.SaveChangesAsync();
        if(result >0){
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var existingProduct = await DbSet.Products.FindAsync(product.Id);
        if (existingProduct == null)
        {
            return false;
        }

        DbSet.Update(product);
        
        var result = await DbSet.SaveChangesAsync();
        if(result >0){
            return true;
        }
        return false;
    }
}

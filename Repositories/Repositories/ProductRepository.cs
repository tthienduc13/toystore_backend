using Repositories.Context;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class ProductRepository : BaseRepository<Entities.Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context)
    {
    }
}
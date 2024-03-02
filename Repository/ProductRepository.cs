using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Data;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper){
            _ctx = context;
            _mapper = mapper;
        }
        
        public bool CreateProduct(ProductModel product, List<int> categoryIds, int brandId)
        {

            var brand = _ctx.Brands.Where(b => b.BrandId == brandId).FirstOrDefault() ?? throw new Exception($"Brand {brandId} not found");

            foreach (var id in categoryIds)
            {
                var category = _ctx.Categories.Where(c => c.CategoryId == id).FirstOrDefault() ?? throw new Exception($"Category {id} not found");

                var categoryProduct = new CategoryProductModel()
                {
                    Category = category,
                    Product = product
                };
                
                _ctx.Add(categoryProduct);
            }
            product.Brand = brand;
            _ctx.Add(product);
            return Save();
        }

        public IList<ProductModel> GetProducts()
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .ToList();
        }

        public ProductModel GetProduct(int id)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.ProductId == id)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
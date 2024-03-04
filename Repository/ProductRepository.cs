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
        
        public bool CreateProduct(
            ProductModel product, 
            List<int> categoryIds, 
            int brandId,
            List<SkuModel> skus
            )
        {
            using var transaction = _ctx.Database.BeginTransaction();
            try
            {                
                var brand = _ctx.Brands
                    .Where(b => b.BrandId == brandId)
                    .FirstOrDefault() ?? throw new Exception($"Brand {brandId} not found");

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
                _ctx.Products.Add(product);
                Save();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
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

        public ProductModel GetProductById(int id)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.ProductId == id)
                .FirstOrDefault();
        }
        public IList<ProductModel> GetProductByCategory(int id)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.CategoryProduct.Any(cp=>cp.CategoryId == id))
                .ToList();
        }
        public ProductModel GetProductBySlug(string slug)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.Slug == slug)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
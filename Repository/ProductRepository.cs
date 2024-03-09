using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto.Request;
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
        public IList<ProductModel> GetProductsByCategory(int id)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.CategoryProduct.Any(cp=>cp.CategoryId == id))
                .ToList();
        }
        public IList<ProductModel> GetProductsByBrand(int id)
        {
            return _ctx.Products
                .AsNoTracking()
                .Include(prod=> prod.Brand)
                .Include(p=>p.CategoryProduct)
                .Include(p=>p.Skus)
                .Where(product => product.Brand.BrandId == id)
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
        public ProductModel UpdateProduct(ProductUpdateReqDto productDto, int productId)
        {
            var product = _ctx.Products
                .Include(p=>p.Skus)
                .Where(p=>p.ProductId == productId)
                .FirstOrDefault();
            var categoryProducts = _ctx.CategoryProduct
                .AsNoTracking()
                .Where(cp=>cp.ProductId == productId)
                .ToList();
            var brand = _ctx.Brands
                .AsNoTracking()
                .Where(b=> b.BrandId == productDto.BrandId)
                .FirstOrDefault();

            product.Brand = brand;
            product.CategoryProduct = categoryProducts;
            product.Name = productDto.Name;
            product.Slug = productDto.Slug;

            _ctx.Products.Update(product);
            Save();
            return GetProductById(productId);
        }

        public bool DeleteProduct(int id)
        {
            var product = _ctx.Products
                .Where(b=>b.ProductId == id)
                .FirstOrDefault();
            _ctx.Products.Remove(product);
            return Save();
        }
        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }

    }
}
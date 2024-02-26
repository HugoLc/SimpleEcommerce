using AutoMapper;
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
        //TODO: nao pode ser simples assim. preciso pegar a informação da categoria para adicionar no modelo
        public bool CreateProduct(ProductModel product, List<int> categoryIds)
        {

            foreach (var id in categoryIds)
            {
                var category = _ctx.Categories.Where(a => a.CategoryId == id).FirstOrDefault() ?? throw new Exception($"Category {id} not found");

                var categoryProduct = new CategoryProductModel()
                {
                    Category = category,
                    Product = product
                };

                _ctx.Add(categoryProduct);
            }
            
            _ctx.Add(product);
            return Save();
        }

        public IList<ProductModel> GetProducts()
        {
            return [.. _ctx.Products];
        }

        public ProductModel GetProduct(int id)
        {
            return _ctx.Products.Where(category => category.ProductId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
using AutoMapper;
using SimpleEcommerce.Data;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper){
            _ctx = context;
            _mapper = mapper;
        }

        public CategoryModel CreateCategory(CategoryModel category)
        {
            _ctx.Categories.Add(category);
            Save();
            return category;
        }

        public IList<CategoryModel> GetCategories()
        {
            return [.. _ctx.Categories];
            
        }

        public CategoryModel GetCategory(int id)
        {
            return _ctx.Categories.Where(category => category.CategoryId == id).FirstOrDefault();
        }
        public CategoryModel UpdateCategory(CategoryModel category)
        {
            var categoryProducts = _ctx.CategoryProduct.Where(cp=>cp.CategoryId ==category.CategoryId).ToList();
            category.CategoryProduct = categoryProducts;
            _ctx.Categories.Update(category);
            Save();
            return GetCategory(category.CategoryId);

        }
        public bool DeleteCategory(int id)
        {
            var category = _ctx.Categories
                .Where(c=>c.CategoryId == id)
                .FirstOrDefault();
            _ctx.Categories.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
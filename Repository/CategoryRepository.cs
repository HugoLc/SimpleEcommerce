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

        public bool CreateCategory(CategoryModel category)
        {
            _ctx.Add(category);
            return Save();
        }

        public IList<CategoryModel> GetCategories()
        {
            return [.. _ctx.Categories];
            
        }

        public CategoryModel GetCategory(int id)
        {
            return _ctx.Categories.Where(category => category.CategoryId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
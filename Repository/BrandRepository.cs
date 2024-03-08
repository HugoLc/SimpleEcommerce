using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleEcommerce.Data;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public BrandRepository(AppDbContext context, IMapper mapper){
            _ctx = context;
            _mapper = mapper;
        }
        public bool CreateBrand(BrandModel brand)
        {
            _ctx.Add(brand);
            return Save();
        }
        public BrandModel UpdateBrand(BrandModel brand)
        {
            var brandProducts = _ctx.Products.Where(p=> p.Brand.BrandId == brand.BrandId).ToList();
            brand.Products = brandProducts;
            _ctx.Brands.Update(brand);
            Save();
            return GetBrand(brand.BrandId);
        }

        public BrandModel GetBrand(int id)
        {
            return _ctx.Brands.Where(brand => brand.BrandId == id).FirstOrDefault();
        }

        public IList<BrandModel> GetBrands()
        {
            // return _ctx.Brands.ToList();
            return [.. _ctx.Brands];
        }
        public bool DeleteBrand(int id)
        {
            var brand = _ctx.Brands
                .Where(b=>b.BrandId == id)
                .FirstOrDefault();
            _ctx.Brands.Remove(brand);
            return Save();
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }

    }
}
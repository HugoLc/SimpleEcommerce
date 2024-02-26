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

        public BrandModel GetBrand(int id)
        {
            return _ctx.Brands.Where(brand => brand.BrandId == id).FirstOrDefault();
        }

        public IList<BrandModel> GetBrands()
        {
            // return _ctx.Brands.ToList();
            return [.. _ctx.Brands];
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
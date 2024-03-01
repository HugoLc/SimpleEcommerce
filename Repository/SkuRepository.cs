using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Data;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Interfaces;
using SimpleEcommerce.Models;

namespace SimpleEcommerce.Repository
{
    public class SkuRepository : ISkuRepository
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper _mapper;

        public SkuRepository(AppDbContext context, IMapper mapper){
            _ctx = context;
            _mapper = mapper;
        }

        public bool CreateSku(SkuModel skuModel, int productId)
        {
            var product = _ctx.Products.Where(p=>p.ProductId == productId).FirstOrDefault() ?? throw new Exception($"Product {productId} not found");;

            skuModel.Product = product;
            _ctx.Add(skuModel);
            return Save();
        }

        public SkuModel GetSku(int id)
        {
            return _ctx.Skus
                .AsNoTracking()
                .Where(s=> s.SkuId == id)
                .Include(s=>s.Product)
                .FirstOrDefault();
        }

        public IList<SkuModel> GetSkus()
        {
            return [.. _ctx.Skus];
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }
    }
}
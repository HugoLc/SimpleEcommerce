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

        public SkuModel CreateSku(SkuModel skuModel, int productId)
        {
            var product = _ctx.Products.Where(p=>p.ProductId == productId).FirstOrDefault() ?? throw new Exception($"Product {productId} not found");;

            skuModel.Product = product;
            _ctx.Add(skuModel);
            Save();
            return skuModel;
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
            return [.. _ctx.Skus
                .AsNoTracking()
                .Include(s=>s.Product)];
        }
        public SkuModel UpdateSku(SkuModel sku)
        {
            var skuEntity = _ctx.Skus
                .AsNoTracking()
                .Where(s=> s.SkuId == sku.SkuId)
                .FirstOrDefault() 
                ?? throw new Exception("Sku not found");
            sku.Product = skuEntity.Product;
            _ctx.Skus.Update(sku);
            Save();
            return GetSku(sku.SkuId);

        }
        public bool DeleteSku(int id)
        {
            var sku = _ctx.Skus
                    .Include(s => s.Product)
                    //É usado quando você espera que haja exatamente um elemento na sequência e deseja garantir isso.
                    .SingleOrDefault(s => s.SkuId == id) 
                    ?? throw new Exception("SKU not found.");
            if (sku.Product.Skus.Count == 1)
                throw new Exception("Unique SKU. Cannot be deleted.");
            _ctx.Skus.Remove(sku);
            return Save();
        }
        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0;
        }

        
    }
}
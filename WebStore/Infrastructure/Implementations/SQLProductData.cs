using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Infrastructure.Implementations
{
    public class SQLProductData : IProductData
    {
        private readonly WebStoryContext _db;

        public SQLProductData(WebStoryContext db)
        {
            _db = db;
        }

        public int GetBrandProductCount(int brandId)
        {
            return _db.Products.Count(p => p.BrandId == brandId);
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _db.Brands.AsEnumerable();
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections.AsEnumerable();
        }

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null)
        {
            if (productFilter is null)
                return _db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Section)
                    .AsEnumerable();

            IQueryable<Product> result = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsQueryable();

            if (productFilter.BrandId != null)
                result = result.Where(p => p.BrandId == productFilter.BrandId);

            if (productFilter.SectionId != null)
                result = result.Where(p => p.SectionId == productFilter.SectionId);

            return result.AsEnumerable();
        }

        public Product GetProductById(int id)
        {
            return _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}

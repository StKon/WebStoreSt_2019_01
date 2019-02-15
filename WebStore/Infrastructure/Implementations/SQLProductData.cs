using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStory.DAL.Context;
using WebStory.DomainCorr;
using WebStore.Infrastructure.Interfaces;
using WebStory.DomainCorr.Entities;
using WebStory.DomainCorr.Entities.Filters;

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

        public IEnumerable<Product> GetProducts(ProductFilter productFilter)
        {
            if (productFilter is null)
                return _db.Products.AsEnumerable();

            IQueryable<Product> result = _db.Products.AsQueryable();

            if (productFilter.BrandId != null)
                result = result.Where(p => p.BrandId == productFilter.BrandId);

            if (productFilter.SectionId != null)
                result = result.Where(p => p.SectionId == productFilter.SectionId);

            return result.AsEnumerable();
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections.AsEnumerable();
        }
    }
}

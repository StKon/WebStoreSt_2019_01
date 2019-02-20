using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Data;

namespace WebStore.Infrastructure.Implementations
{
    class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter productFilter)
        {
            List<Product> product = TestData.Products;

            if (productFilter.SectionId.HasValue)
                product = product.Where(p => p.SectionId.Equals(productFilter.SectionId)).ToList();

            if (productFilter.BrandId.HasValue)
                product = product.Where(p => p.BrandId.Equals(productFilter.BrandId)).ToList();

            return product;
        }

        public int GetBrandProductCount(int brandId)
        {
            return TestData.Products.Count(p => p.BrandId == brandId);
        }
    }
}

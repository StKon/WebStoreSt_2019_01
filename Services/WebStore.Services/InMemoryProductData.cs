using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Services.Data;
using WebStore.Services.Map;
using WebStore.Domain.Dto;

namespace WebStore.Services
{
    class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Brand GetBrandById(int id)
        {
            return TestData.Brands.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public Section GetSectionById(int id)
        {
            return TestData.Sections.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null)
        {
            List<Product> product = TestData.Products;

            if (productFilter is null) return product.Select(p => p.Map());

            if (productFilter.SectionId.HasValue)
                product = product.Where(p => p.SectionId.Equals(productFilter.SectionId)).ToList();

            if (productFilter.BrandId.HasValue)
                product = product.Where(p => p.BrandId.Equals(productFilter.BrandId)).ToList();

            return product.Select(p => p.Map());
        }

        public ProductDto GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == id).Map();
        }

        public int GetBrandProductCount(int brandId)
        {
            return TestData.Products.Count(p => p.BrandId == brandId);
        }

        public ProductDto UpdateProduct(ProductDto prod)
        {
            Product oldProd = TestData.Products.FirstOrDefault(p => p.Id == prod.Id);
            oldProd.Name = prod.Name;
            oldProd.ImageUrl = prod.ImageUrl;
            oldProd.Order = prod.Order;
            oldProd.Price = prod.Price;
            oldProd.BrandId = prod.Brand?.Id;
            oldProd.SectionId = prod.Section.Id;
            return oldProd.Map();
        }

        public ProductDto AddProduct(ProductDto product)
        {
            Product prod = product.Map();

            //определяем id
            prod.Id = TestData.Products.Max(e => e.Id) + 1;
            TestData.Products.Add(prod);
            return prod.Map();
        }

        public void DeleteProduct(int id)
        {
            Product prod = TestData.Products.FirstOrDefault(p => p.Id == id);
            TestData.Products.Remove(prod);
        }
    }
}

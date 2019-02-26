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

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null)
        {
            List<Product> product = TestData.Products;

            if (productFilter is null) return product;

            if (productFilter.SectionId.HasValue)
                product = product.Where(p => p.SectionId.Equals(productFilter.SectionId)).ToList();

            if (productFilter.BrandId.HasValue)
                product = product.Where(p => p.BrandId.Equals(productFilter.BrandId)).ToList();

            return product;
        }

        public Product GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == id);
        }

        public int GetBrandProductCount(int brandId)
        {
            return TestData.Products.Count(p => p.BrandId == brandId);
        }

        public Product UpdateProduct(Product prod)
        {
            Product oldProd = TestData.Products.FirstOrDefault(p => p.Id == prod.Id);
            oldProd.Name = prod.Name;
            oldProd.ImageUrl = prod.ImageUrl;
            oldProd.Order = prod.Order;
            oldProd.Price = prod.Price;
            oldProd.BrandId = prod.BrandId;
            oldProd.SectionId = prod.SectionId;
            return oldProd;
        }

        public Product AddProduct(Product prod)
        {
            //определяем id
            prod.Id = TestData.Products.Max(e => e.Id) + 1;
            TestData.Products.Add(prod);
            return prod;
        }

        public void DeleteProduct(Product prod)
        {
            TestData.Products.Remove(prod);
        }
    }
}

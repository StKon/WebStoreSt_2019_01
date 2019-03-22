using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Dto;
using WebStore.Services.Map;

namespace WebStore.Services
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

        public Brand GetBrandById(int id)
        {
            return _db.Brands.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Section> GetSections()
        {
            return _db.Sections.AsEnumerable();
        }

        public Section GetSectionById(int id)
        {
            return _db.Sections.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null)
        {
            if (productFilter is null)
                return _db.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Section)
                    .Select(p => p.Map())
                    .AsEnumerable();

            IQueryable<Product> result = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsQueryable();

            if (productFilter.BrandId != null)
                result = result.Where(p => p.BrandId == productFilter.BrandId);

            if (productFilter.SectionId != null)
                result = result.Where(p => p.SectionId == productFilter.SectionId);

            return result.Select(p => p.Map()).AsEnumerable();
        }

        public ProductDto GetProductById(int id)
        {
            return _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id)
                .Map();
        }

        public ProductDto UpdateProduct(ProductDto product)
        {
            Product prod = product.Map();

            Product oldProd = _db.Products.FirstOrDefault(p => p.Id == prod.Id);
            if (oldProd is null) return prod.Map();

            oldProd.Name = prod.Name;
            oldProd.ImageUrl = prod.ImageUrl;
            oldProd.Order = prod.Order;
            oldProd.Price = prod.Price;
            oldProd.BrandId = prod.BrandId;
            oldProd.SectionId = prod.SectionId;

            _db.SaveChanges();

            return prod.Map();
        }

        public ProductDto AddProduct(ProductDto product)
        {
            Product prod = product.Map();

            prod.Id = 0;
            _db.Products.Add(prod);
            _db.SaveChanges();
            return prod.Map();
        }

        public void DeleteProduct(int id)
        {
            Product oldProd = _db.Products.FirstOrDefault(p => p.Id == id);
            if (oldProd is null) return;

            _db.Products.Remove(oldProd);
            _db.SaveChanges();
        }
    }
}

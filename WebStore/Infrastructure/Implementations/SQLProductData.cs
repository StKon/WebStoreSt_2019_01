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

        public Product UpdateProduct(Product prod)
        {
            Product oldProd = _db.Products.FirstOrDefault(p => p.Id == prod.Id);
            if (oldProd is null) return prod;

            oldProd.Name = prod.Name;
            oldProd.ImageUrl = prod.ImageUrl;
            oldProd.Order = prod.Order;
            oldProd.Price = prod.Price;
            oldProd.BrandId = prod.BrandId;
            oldProd.SectionId = prod.SectionId;

            _db.SaveChanges();

            return prod;
        }

        public Product AddProduct(Product prod)
        {
            prod.Id = 0;
            _db.Products.Add(prod);
            _db.SaveChanges();
            return prod;
        }

        public void DeleteProduct(Product prod)
        {
            Product oldProd = _db.Products.FirstOrDefault(p => p.Id == prod.Id);
            if (oldProd is null) return;

            _db.Products.Remove(prod);
            _db.SaveChanges();
        }
    }
}

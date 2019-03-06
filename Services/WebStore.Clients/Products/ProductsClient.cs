using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
            ServicesAddress = "api/products";
        }

        public ProductDto AddProduct(ProductDto prod)
        {
            var url = $"{ServicesAddress}/Add";
            var response = Post<ProductDto>(url, prod);
            var result = response.Content.ReadAsAsync<ProductDto>().Result;
            return result;
        }

        public void DeleteProduct(ProductDto prod)
        {
            var url = $"{ServicesAddress}/{prod.Id}";
            Delete(url);
        }

        public int GetBrandProductCount(int brandId)
        {
            var url = $"{ServicesAddress}/ProductCount/{brandId}";
            var result = Get<int>(url);
            return result;
        }

        public IEnumerable<Brand> GetBrands()
        {
            var url = $"{ServicesAddress}/brands";
            var result = Get<List<Brand>>(url);
            return result;
        }

        public ProductDto GetProductById(int id)
        {
            var url = $"{ServicesAddress}/{id}";
            var result = Get<ProductDto>(url);
            return result;
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null)
        {
            var url = $"{ServicesAddress}";
            if (productFilter is null) productFilter = new ProductFilter();
            var response = Post<ProductFilter>(url, productFilter);
            var result = response.Content.ReadAsAsync<IEnumerable<ProductDto>>().Result;
            return result;
        }

        public IEnumerable<Section> GetSections()
        {
            var url = $"{ServicesAddress}/sections";
            var result = Get<List<Section>>(url);
            return result;
        }

        public ProductDto UpdateProduct(ProductDto prod)
        {
            var url = $"{ServicesAddress}";
            var response = Put<ProductDto>(url, prod);
            var result = response.Content.ReadAsAsync<ProductDto>().Result;
            return result;
        }
    }
}

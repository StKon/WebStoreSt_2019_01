using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Produces("application/json")]
    public class ProductsController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData) => _productData = productData;

        [HttpPost("Add"), ActionName("Post")]
        public ProductDto AddProduct([FromBody] ProductDto prod)
        {
            return _productData.AddProduct(prod);
        }

        [HttpDelete("{id}"), ActionName("Delete")]
        public void DeleteProduct(int id)
        {
            _productData.DeleteProduct(id);
        }

        [HttpGet("ProductCount/{brandId}")]
        public int GetBrandProductCount(int brandId)
        {
            return _productData.GetBrandProductCount(brandId);
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public ProductDto GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody] ProductFilter productFilter = null)
        {
            return _productData.GetProducts(productFilter);
        }

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpPut]
        public ProductDto UpdateProduct([FromBody] ProductDto prod)
        {
            return _productData.UpdateProduct(prod);
        }
    }
}
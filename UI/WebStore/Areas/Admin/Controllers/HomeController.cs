using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Interfaces;
using System.Reflection;
using WebStore.Services.Map;
using WebStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = WebStore.Domain.Entities.User.AdminRole)]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList(string searchString)
        {

            var prod = _productData.GetProducts().Products.Select(p => p.Map());

            //ФИЛЬТР
            if (! string.IsNullOrEmpty(searchString))
            {
                prod = prod.Where(p => p.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
            }

            var prodViewModel = prod.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Order = p.Order,
                Price = p.Price,
                BrandId = p.BrandId ?? 0,
                Brand = p.Brand?.Name ?? string.Empty,
                SectionId = p.SectionId,
                Section = p.Section?.Name ?? string.Empty
            }).AsEnumerable();

            return View(prodViewModel);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product prod = _productData.GetProductById(id).Map();
            if (prod is null) return NotFound();

            var notParentSections = _productData.GetSections().Where(s => s.ParentId != null);
            var brands = _productData.GetBrands();

            var prodViewModel = new ProductViewModel
            {
                Id = prod.Id,
                Name = prod.Name,
                ImageUrl = prod.ImageUrl,
                Order = prod.Order,
                Price = prod.Price,
                BrandId = prod.BrandId ?? 0,
                Brand = prod.Brand?.Name ?? string.Empty,
                SectionId = prod.SectionId,
                Section = prod.Section?.Name ?? string.Empty,
                Sections = new SelectList(notParentSections, "Id", "Name"),
                Brands = new SelectList(brands, "Id", "Name")
            };

            return View(prodViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductViewModel p)
        {
            if (!ModelState.IsValid)
                return View(p);  //состояние модели

            Product prod = new Product
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Order = p.Order,
                Price = p.Price,
                BrandId = p.BrandId,
                SectionId = p.SectionId
            };

            Product oldProd = _productData.GetProductById(p.Id).Map();
            if (oldProd is null) return NotFound();

            _productData.UpdateProduct(prod.Map());

            return RedirectToAction("ProductList");
        }

        [HttpGet, ActionName("DeleteProduct")]
        public IActionResult DeleteProductGet(int id)
        {
            Product prod = _productData.GetProductById(id).Map();
            if (prod is null) return NotFound();
            return View(prod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Product prod)
        {
            if (prod is null) NotFound();
            _productData.DeleteProduct(prod.Id);
            return RedirectToAction("ProductList");
        }

        public IActionResult DetailsProduct(int id)
        {
            Product prod = _productData.GetProductById(id).Map();
            if (prod is null) return NotFound();
            return View(prod);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            Product prod = new Product();
            return View(prod);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product prod)
        {
            _productData.AddProduct(prod.Map());
            return RedirectToAction("ProductList");
        }
    }
}
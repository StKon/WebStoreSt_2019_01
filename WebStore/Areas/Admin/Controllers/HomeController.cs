using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult ProductList()
        {
            return View(_productData.GetProducts());
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product prod = _productData.GetProductById(id);
            if (prod is null) return NotFound();
            return View(prod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product p)
        {
            if (!ModelState.IsValid)
                return View(p);  //состояние модели


            Product oldProd = _productData.GetProductById(p.Id);
            if (oldProd is null) return NotFound();

            oldProd.Name = p.Name;
            oldProd.ImageUrl = p.ImageUrl;
            oldProd.Order = p.Order;
            oldProd.Price = p.Price;
            oldProd.SectionId = p.SectionId;
            oldProd.BrandId = p.BrandId;
            
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct()
        {
            return View();
        }

        public IActionResult DetailsProduct()
        {
            return View();
        }
    }
}
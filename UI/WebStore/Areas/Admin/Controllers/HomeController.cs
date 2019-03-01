using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Filters;
using WebStore.Infrastructure.Interfaces;
using System.Reflection;

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

        public IActionResult ProductList(string sortOrder, string sortOrderOld, string searchString)
        {
            //СОРТИРОВКА
            //Первый раз
            if (sortOrder is null) sortOrder = "Id";
            if (sortOrderOld is null) sortOrderOld = "";

            if (sortOrder == sortOrderOld)
                sortOrder = sortOrder + "_desc";
            //else if(ViewBag.SortOrder == (sortOrder + "_desc"))

            var prod = _productData.GetProducts();

            //ФИЛЬТР
            if (! string.IsNullOrEmpty(searchString))
            {
                prod = prod.Where(p => p.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
            }
            
            
            bool Flag = false;
            if (sortOrder != "")
            {
                Type ObjProd = typeof(Product);
                foreach (var p in ObjProd.GetProperties())
                {
                    //_property = p;

                    if (sortOrder == p.Name)
                    {
                        ViewBag.SortOrder = sortOrder;
                        prod = prod.OrderBy(e => p.GetValue(e));
                        Flag = true;
                        break;
                    }
                    else if (sortOrder == (p.Name + "_desc"))
                    {
                        ViewBag.SortOrder = sortOrder;
                        prod = prod.OrderByDescending(e => p.GetValue(e));
                        Flag = true;
                        break;
                    }
                }
            }

            if (!Flag)
            {
                sortOrder = "Id";
                ViewBag.SortOrder = sortOrder;
                prod.OrderBy(e => e.Id);
            }
            return View(prod);
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

            _productData.UpdateProduct(p);

            return RedirectToAction("ProductList");
        }

        [HttpGet, ActionName("DeleteProduct")]
        public IActionResult DeleteProductGet(int id)
        {
            Product prod = _productData.GetProductById(id);
            if (prod is null) return NotFound();
            return View(prod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            Product prod = _productData.GetProductById(id);
            if (prod is null) return NotFound();
            _productData.DeleteProduct(prod);
            return RedirectToAction("ProductList");
        }

        public IActionResult DetailsProduct(int id)
        {
            Product prod = _productData.GetProductById(id);
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
            _productData.AddProduct(prod);
            return RedirectToAction("ProductList");
        }
    }
}
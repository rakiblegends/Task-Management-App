using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.Data;
using ProductCRUD.Models;
using ProductCRUD.Repository;
using System;

namespace ProductCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        //private readonly ApplicatonDbContext _context;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        //GET Products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync(); // Fetch all products
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }



        // Create new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            
            if (string.IsNullOrEmpty(product.Name) || product.Name.Length>100)
            {
                ModelState.AddModelError("name", "Name should not be null and Too Large");
            }
            
            if (product.Price<=0)
            {
                ModelState.AddModelError("price","The price should be greater than 0");
            }
            if (product.StockQuantity<0)
            {
                ModelState.AddModelError("stockquantity", "The quantiy should not be negative");
            }
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                TempData["success"] ="The product added successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View(product);
            }
            
        }


        //GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(id);
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if(id!= product.Id)
            {
                return BadRequest("Product Mismatch");
            }
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            _productRepository.UpdateAsync(product);
            TempData["success"] ="The product Edited successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound(id);
            }

            _productRepository.DeleteAsync(product);
            TempData["success"] ="The product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

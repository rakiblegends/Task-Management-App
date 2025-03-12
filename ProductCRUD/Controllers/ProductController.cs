using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.Data;
using ProductCRUD.Models;
using System;

namespace ProductCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicatonDbContext _context;

        public ProductController(ApplicatonDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync(); // Fetch all products
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                TempData["success"] ="The product added successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(id);
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            TempData["success"] ="The product Edited successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound(id);
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            TempData["success"] ="The product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
